namespace Contracts
{

    public record UserUpdatedEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }
}
