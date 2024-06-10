using PatientTickets.Application.Abstractions.Messaging;
using PatientTickets.Application.DTOs;

namespace PatientTickets.Application.Handlers.Commands.CreatePatientTicket;

public sealed record CreatePatientTicketCommand(
    Guid PatientId,
    DateTime DateAppointment,
    Guid DoctorId,
    string HoursAppointment,
    string MinutesAppointment

   ) : ICommand<CreatePatientTicketDto>;
