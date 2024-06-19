using AutoMapper;
using MedicalCards.Application.Abstractions.ExternalProviders;
using MedicalCards.Application.Abstractions.Messaging;
using MedicalCards.Application.Abstractions.Persistence.Repository.Read;
using MedicalCards.Application.Abstractions.Persistence.Repository.Writing;
using MedicalCards.Application.Abstractions.Service;
using MedicalCards.Application.Caches.Appointment;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Enums;
using MedicalCards.Domain.Errors;
using MedicalCards.Domain.Exceptions.Base;
using MedicalCards.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace MedicalCards.Application.Handlers.Appointment.Commands.CreateAppointment;

internal class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand, CreateAppointmentDto>
{
    private readonly IBaseWriteRepository<Domain.Appointment> _writeAppointmentRepository;
    private readonly IManageUsersProviders _applicationUsersProviders;
    private readonly IBaseReadRepository<Domain.MedicalCard> _readMedicalCardsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAppointmentCommandHandler> _logger;
    private readonly AppointmentsListMemoryCache _listCache;
    private readonly AppointmentsCountMemoryCache _countCache;
    private readonly AppointmentMemoryCache _patientMemoryCache;
    private readonly ICurrentUserService _currentUserService;

    public CreateAppointmentCommandHandler(
        IBaseWriteRepository<Domain.Appointment> writeAppointmentRepository,
        IBaseReadRepository<Domain.MedicalCard> readMedicalCardsRepository,
        IMapper mapper,
        ILogger<CreateAppointmentCommandHandler> logger, IManageUsersProviders applicationUsersProviders,
        AppointmentsListMemoryCache listCache,
        AppointmentsCountMemoryCache countCache,
        AppointmentMemoryCache patientMemoryCache, ICurrentUserService currentUserService)

    {

        _writeAppointmentRepository = writeAppointmentRepository;
        _mapper = mapper;
        _logger = logger;
        _readMedicalCardsRepository = readMedicalCardsRepository;
        _listCache = listCache;
        _countCache = countCache;
        _patientMemoryCache = patientMemoryCache;
        _currentUserService = currentUserService;
        _applicationUsersProviders = applicationUsersProviders;

    }

    public async Task<Result<CreateAppointmentDto>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        // only doctors can add appointment 
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Doctor))
        {
            throw new ForbiddenException();
        }
        var medicalCard = await _readMedicalCardsRepository.AsAsyncRead().SingleOrDefaultAsync(pt => pt.PatientId == request.PatientId, cancellationToken);
        if (medicalCard is null)
        {
            // TODO edit result
            return Result.Failure<CreateAppointmentDto>(DomainErrors.MedicalCard.MedicalCardNotFound(request.PatientId));
        }

        var doctor = await _applicationUsersProviders.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null)
        {
            return Result.Failure<CreateAppointmentDto>(DomainErrors.Appointment.AppointmentDoctorNotFound(request.DoctorId));
        }
        var patient = await _applicationUsersProviders.GetPatientByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            return Result.Failure<CreateAppointmentDto>(DomainErrors.Appointment.AppointmentPatientNotFound(medicalCard.PatientId));
        }

        var newAppointmentGuid = Guid.NewGuid();


        var appointment = Domain.Appointment.Create(
            newAppointmentGuid,
            request.DescriptionEpicrisis,
            request.DescriptionAnamnesis,
            doctor.DoctorFirstName,
            doctor.DoctorLastName,
            doctor.DoctorPatronymic,
            doctor.Speciality,
            medicalCard.Id,
            request.DoctorId,
            medicalCard.PatientId,
            patient.FirstName,
            patient.LastName,
            patient.Patronymic
        );



        appointment = await _writeAppointmentRepository.AddAsync(appointment, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _patientMemoryCache.Clear();
        _logger.LogInformation($"New Appointment {newAppointmentGuid} created.");

        return _mapper.Map<CreateAppointmentDto>(appointment);
    }
}
