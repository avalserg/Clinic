namespace ManageUsers.Api.Contracts.Patient
{
    public sealed record CreateReviewRequest(

    Guid PatientId,
    string Description
    );
}
