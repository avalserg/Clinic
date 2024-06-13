namespace Reviews.Api.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PatientId"></param>
    /// <param name="Description"></param>
    public sealed record CreateReviewRequest(
        Guid PatientId,
        string Description
    );
}
