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
        public static readonly Func<Guid, Error> MedicalCardNotFound = id => new Error(
            $"MedicalCard.MedicalCardNotFound",
            $"The MedicalCard with the identifier {id} was not found.");
    }

}
