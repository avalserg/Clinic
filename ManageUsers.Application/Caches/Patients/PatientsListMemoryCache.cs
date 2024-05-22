using ManageUsers.Application.BaseRealizations;
using ManageUsers.Application.DTOs;
using ManageUsers.Application.DTOs.Patient;

namespace ManageUsers.Application.Caches.Patients;

public class PatientsListMemoryCache : BaseCache<BaseListDto<GetPatientDto>>;