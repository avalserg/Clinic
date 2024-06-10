using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientTickets.Application.Abstractions.ExternalProviders;
using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.Abstractions.Persistence.Repository.Writing;
using PatientTickets.Application.Abstractions.Service;
using PatientTickets.Application.Caches;
using PatientTickets.Application.DTOs;
using PatientTickets.Domain.Entities;
using PatientTickets.Domain.Exceptions.Base;
using PatientTickets.Domain.Shared;

namespace PatientTickets.Application.Handlers.Commands.CreatePatientTicket;

internal class CreatePatientTicketCommandHandler : ICommandHandler<CreatePatientTicketCommand, CreatePatientTicketDto>
{

    private readonly IBaseWriteRepository<PatientTicket> _patientTicketRepository;
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
        PatientTicketsListMemoryCache listCache, ICurrentUserService currentUserService, IManageUsersProviders applicationUsersProviders)
    {
        _mapper = mapper;
        _logger = logger;
        _patientTicketRepository = patientTicketRepository;
        _patientTicketMemoryCache = patientTicketMemoryCache;
        _countCache = countCache;
        _listCache = listCache;
        _currentUserService = currentUserService;
        _applicationUsersProviders = applicationUsersProviders;
    }

    public async Task<Result<CreatePatientTicketDto>> Handle(CreatePatientTicketCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _applicationUsersProviders.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null)
        {
            // TODO Result
            throw new ArgumentException();
        }
        var newPatientTicketGuid = Guid.NewGuid();
        // TODO check if user Patient role have

        if (request.PatientId != _currentUserService.CurrentUserId)
        {
            throw new ForbiddenException();
        }

        if (!int.TryParse(request.HoursAppointment, out var hoursAppointment))
        {
            throw new ArgumentException();
        }

        if (!int.TryParse(request.MinutesAppointment, out var minutesAppointment))
        {
            throw new ArgumentException();
        }


        var date = request.DateAppointment.AddHours(hoursAppointment);
        var newDate = date.AddMinutes(minutesAppointment);

        var patientTicket = PatientTicket.Create(
            newPatientTicketGuid,
            request.PatientId,
            newDate,
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