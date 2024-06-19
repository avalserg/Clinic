using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.Caches.Prescription;
using MedicalCards.Application.DTOs.Prescription;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Handlers.Prescription.Commands.CreatePrescription
{
    public class CreatePrescriptionCommandHandler : ICommandHandler<CreatePrescriptionCommand, CreatePrescriptionDto>
    {
        private readonly IBaseWriteRepository<Domain.Prescription> _writePrescriptionRepository;
        private readonly IManageUsersProviders _applicationUsersProviders;
        private readonly IBaseReadRepository<Domain.Appointment> _readAppointmentsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePrescriptionCommandHandler> _logger;
        private readonly PrescriptionsListMemoryCache _listCache;
        private readonly PrescriptionsCountMemoryCache _countCache;
        private readonly PrescriptionMemoryCache _patientMemoryCache;
        private readonly ICurrentUserService _currentUserService;

        public CreatePrescriptionCommandHandler(
            IBaseWriteRepository<Domain.Prescription> writePrescriptionRepository,
            IManageUsersProviders applicationUsersProviders,
            IBaseReadRepository<Domain.Appointment> readAppointmentsRepository,
            IMapper mapper,
            ILogger<CreatePrescriptionCommandHandler> logger,
            PrescriptionsListMemoryCache listCache,
            PrescriptionsCountMemoryCache countCache,
            PrescriptionMemoryCache patientMemoryCache,
            ICurrentUserService currentUserService)
        {
            _writePrescriptionRepository = writePrescriptionRepository;
            _applicationUsersProviders = applicationUsersProviders;
            _readAppointmentsRepository = readAppointmentsRepository;
            _mapper = mapper;
            _logger = logger;
            _listCache = listCache;
            _countCache = countCache;
            _patientMemoryCache = patientMemoryCache;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Create Prescription by appointment
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ForbiddenException"></exception>
        public async Task<Result<CreatePrescriptionDto>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            // only doctors can add appointment 
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor))
            {
                throw new ForbiddenException();
            }

            var appointment = await _readAppointmentsRepository.AsAsyncRead()
                .FirstOrDefaultAsync(a => a.PatientId == request.PatientId, cancellationToken);
            if (appointment is null)
            {
                // TODO RESULT correct
                return Result.Failure<CreatePrescriptionDto>(DomainErrors.Appointment.AppointmentNotFound(request.PatientId));
            }
            var doctor = await _applicationUsersProviders.GetDoctorByIdAsync(appointment.DoctorId, cancellationToken);
            if (doctor is null)
            {
                return Result.Failure<CreatePrescriptionDto>(DomainErrors.Appointment.AppointmentDoctorNotFound(appointment.DoctorId));
            }
            var patient = await _applicationUsersProviders.GetPatientByIdAsync(appointment.PatientId, cancellationToken);
            if (patient is null)
            {
                return Result.Failure<CreatePrescriptionDto>(DomainErrors.Appointment.AppointmentPatientNotFound(appointment.PatientId));
            }

            var newPrescriptionGuid = Guid.NewGuid();


            var prescription = Domain.Prescription.Create(
                newPrescriptionGuid,
                request.MedicineName,
                request.ReleaseForm,
                request.Amount,
                DateTime.Now,
                appointment.Id,
                appointment.DoctorId,
                doctor.DoctorFirstName,
                doctor.DoctorLastName,
                doctor.DoctorPatronymic,
                appointment.PatientId,
                patient.FirstName,
                patient.LastName,
                patient.Patronymic
            );



            prescription = await _writePrescriptionRepository.AddAsync(prescription, cancellationToken);


            _listCache.Clear();
            _countCache.Clear();
            _patientMemoryCache.Clear();
            _logger.LogInformation($"New Prescription {newPrescriptionGuid} created.");
            var prescriptionDto = _mapper.Map<CreatePrescriptionDto>(prescription);



            return prescriptionDto;
        }
    }
}
