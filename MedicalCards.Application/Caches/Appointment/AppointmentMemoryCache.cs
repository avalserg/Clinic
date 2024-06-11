using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Caches.Appointment;

public class AppointmentMemoryCache : BaseCache<Result<GetAppointmentDto>> { };