using MedicalCards.Application.BaseRealizations;
using MedicalCards.Application.DTOs;
using MedicalCards.Application.DTOs.Appointment;
using MedicalCards.Domain.Shared;

namespace MedicalCards.Application.Caches.Appointment;

public class AppointmentsListMemoryCache : BaseCache<Result<BaseListDto<GetAppointmentDto>>>
{
};