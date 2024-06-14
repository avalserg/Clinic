using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientTickets.Application.Abstractions.ExternalProviders;
using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Read;
using PatientTickets.Application.Abstractions.Persistence.Repository.Writing;
using PatientTickets.Application.Abstractions.Service;
using PatientTickets.Application.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Enums;
using PatientTickets.Domain.Errors;
using PatientTickets.Domain.Shared;
using System.Globalization;

namespace PatientTickets.Application.Handlers.Commands.CreatePatientTicket;

internal class CreatePatientTicketCommandHandler : ICommandHandler<CreatePatientTicketCommand, CreatePatientTicketDto>
{

    private readonly IBaseWriteRepository<PatientTicket> _patientTicketRepository;
    private readonly IBaseReadRepository<PatientTicket> _patientTicketReadRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly PatientTicketsListMemoryCache _listCache;
    private readonly ILogger<CreatePatientTicketCommandHandler> _logger;
    private readonly PatientTicketsCountMemoryCache _countCache;
    private readonly PatientTicketMemoryCache _patientTicketMemoryCache;
    private readonly IManageUsersProviders _applicationUsersProviders;
    public CreatePatientTicketCommandHandler(
        IMapper mapper,
        ILogger<CreatePatientTicketCommandHandler> logger,
        IBaseWriteRepository<PatientTicket> patientTicketRepository,
        PatientTicketMemoryCache patientTicketMemoryCache,
        PatientTicketsCountMemoryCache countCache,
        PatientTicketsListMemoryCache listCache, ICurrentUserService currentUserService, IManageUsersProviders applicationUsersProviders, IBaseReadRepository<PatientTicket> patientTicketReadRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _patientTicketRepository = patientTicketRepository;
        _patientTicketMemoryCache = patientTicketMemoryCache;
        _countCache = countCache;
        _listCache = listCache;
        _currentUserService = currentUserService;
        _applicationUsersProviders = applicationUsersProviders;
        _patientTicketReadRepository = patientTicketReadRepository;
    }

    public async Task<Result<CreatePatientTicketDto>> Handle(CreatePatientTicketCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _applicationUsersProviders.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null)
        {
            return Result.Failure<CreatePatientTicketDto>(DomainErrors.PatientTicket.DoctorForPatientTicketNotFound(request.DoctorId));
        }
        var newPatientTicketGuid = Guid.NewGuid();

        var isUserRolePatient = _currentUserService.UserInRole(ApplicationUserRolesEnum.Patient);
        // only patient can create ticket
        if (request.PatientId != _currentUserService.CurrentUserId && !isUserRolePatient)
        {
            return Result.Failure<CreatePatientTicketDto>(DomainErrors.PatientTicket.CreatorIsNotInRolePatient(request.DoctorId));
        }

        if (!int.TryParse(request.HoursAppointment, out var hoursAppointment))
        {
            throw new ArgumentException();
        }

        if (!int.TryParse(request.MinutesAppointment, out var minutesAppointment))
        {
            throw new ArgumentException();
        }



        var dateWitHours = request.DateAppointment.AddHours(hoursAppointment);

        var completeDate = dateWitHours.AddMinutes(minutesAppointment);
        var timeAppointmentIsBusy = await _patientTicketReadRepository.AsAsyncRead().AnyAsync(t =>
            t.DateAppointment == completeDate && t.DoctorId == request.DoctorId, cancellationToken);
        if (timeAppointmentIsBusy)
        {
            return Result.Failure<CreatePatientTicketDto>(DomainErrors.PatientTicket.PatientTicketTimeIsBusy(completeDate.ToString(CultureInfo.CurrentCulture)));
        }
        var patientTicket = PatientTicket.Create(
            newPatientTicketGuid,
            request.PatientId,
            completeDate,
            request.DoctorId,
            doctor.FirstName,
            doctor.LastName,
            doctor.Patronymic,
            doctor.CabinetNumber,
            doctor.Speciality
            );



        patientTicket = await _patientTicketRepository.AddAsync(patientTicket, cancellationToken);


        _listCache.Clear();
        _countCache.Clear();
        _patientTicketMemoryCache.Clear();
        _logger.LogInformation($"New patientTicket {patientTicket.Id} created.");

        return _mapper.Map<CreatePatientTicketDto>(patientTicket);
    }
}