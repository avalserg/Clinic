using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.Caches.Prescription;
using MedicalCards.Application.DTOs.Prescription;
using MedicalCards.Domain.Errors;
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
        public async Task<Result<CreatePrescriptionDto>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            //// only doctors can add appointment 
            //if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor))
            //{
            //    throw new ForbiddenException();
            //}
            var appointment = await _readAppointmentsRepository.AsAsyncRead().SingleOrDefaultAsync(pt => pt.Id == request.AppointmentId, cancellationToken);
            if (appointment is null)
            {
                return Result.Failure<CreatePrescriptionDto>(DomainErrors.Appointment.AppointmentNotFound(request.AppointmentId));
            }

            //var doctor = await _applicationUsersProviders.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
            //if (doctor is null)
            //{
            //    return Result.Failure<CreatePrescriptionDto>(DomainErrors.Appointment.AppointmentDoctorNotFound(request.DoctorId));
            //}
            //var patient = await _readAppointmentsRepository.AsAsyncRead().SingleOrDefaultAsync(p=>p.PatientId == , cancellationToken);
            //if (patient is null)
            //{
            //    return Result.Failure<CreatePrescriptionDto>(DomainErrors.Appointment.AppointmentPatientNotFound(medicalCard.PatientId));
            //}
            // TODO only doctor can create appointment
            var newPrescriptionGuid = Guid.NewGuid();


            var prescription = Domain.Prescription.Create(
                newPrescriptionGuid,
                request.MedicineName,
                request.ReleaseForm,
                request.Amount,
                DateTime.Now,
                appointment.Id,
                appointment.DoctorId,
                appointment.PatientId

            );



            prescription = await _writePrescriptionRepository.AddAsync(prescription, cancellationToken);


            _listCache.Clear();
            _countCache.Clear();
            _patientMemoryCache.Clear();
            _logger.LogInformation($"New Prescription {newPrescriptionGuid} created.");
            var prescriptionDto = _mapper.Map<CreatePrescriptionDto>(prescription);

            //prescriptionDto.PatientFirstName = appointment.PatientFirstName;
            //prescriptionDto.PatientLastName = appointment.PatientLastName;
            //prescriptionDto.PatientPatronymic = appointment.PatientPatronymic;
            //prescriptionDto.DoctorFirstName = appointment.DoctorFirstName;
            //prescriptionDto.DoctorLastName = appointment.DoctorLastName;
            //prescriptionDto.DoctorPatronymic = appointment.DoctorPatronymic;
            //prescriptionDto.Speciality = appointment.Speciality;


            return prescriptionDto;
        }
    }
}
