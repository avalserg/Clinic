namespace Contracts
{
    public record UserDeletedEvent
    {
        public Guid Id { get; set; }
    }
}
