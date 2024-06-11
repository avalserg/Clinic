using MedicalCards.Domain.Shared;

namespace MedicalCards.Domain.Errors;

public static class DomainErrors
{

    public static class MedicalCard
    {
        public static readonly Func<Guid, Error> MedicalCardAlreadyExist = id => new Error(
              "MedicalCard.MedicalCardAlreadyExist",
              $"The MedicalCard Patient with {id} is already exist");

        public static readonly Func<Guid, Error> MedicalCardPatientNotFound = id => new Error(
            $"MedicalCard.MedicalCardPatientNotFound",
            $"The Patient with the identifier {id} was not found.");
        public static readonly Func<Guid, Error> MedicalCardDoctorNotFound = id => new Error(
            $"MedicalCard.MedicalCardDoctorNotFound",
            $"The Doctor with the identifier {id} was not found.");
        public static readonly Func<Guid, Error> MedicalCardNotFound = id => new Error(
            $"MedicalCard.MedicalCardNotFound",
            $"The MedicalCard with the identifier {id} was not found.");
    }
    public static class Appointment
    {
        public static readonly Func<Guid, Error> AppointmentAlreadyExist = id => new Error(
              "Appointment.AppointmentAlreadyExist",
              $"The Appointment Patient with {id} is already exist");

        public static readonly Func<Guid, Error> AppointmentPatientNotFound = id => new Error(
            $"Appointment.AppointmentPatientNotFound",
            $"The Patient with the identifier {id} was not found.");
        public static readonly Func<Guid, Error> AppointmentDoctorNotFound = id => new Error(
            $"Appointment.AppointmentDoctorNotFound",
            $"The Doctor with the identifier {id} was not found.");
        public static readonly Func<Guid, Error> AppointmentNotFound = id => new Error(
            $"Appointment.AppointmentNotFound",
            $"The Appointment with the identifier {id} was not found.");
    }
    public static class Prescription
    {
        public static readonly Func<Guid, Error> PrescriptionAlreadyExist = id => new Error(
              "Prescription.PrescriptionAlreadyExist",
              $"The Prescription Patient with {id} is already exist");

        public static readonly Func<Guid, Error> PrescriptionPatientNotFound = id => new Error(
            $"Prescription.PrescriptionPatientNotFound",
            $"The Patient with the identifier {id} was not found.");
        public static readonly Func<Guid, Error> PrescriptionDoctorNotFound = id => new Error(
            $"Prescription.PrescriptionDoctorNotFound",
            $"The Doctor with the identifier {id} was not found.");
        public static readonly Func<Guid, Error> PrescriptionNotFound = id => new Error(
            $"Prescription.PrescriptionNotFound",
            $"The Prescription with the identifier {id} was not found.");
    }

}
