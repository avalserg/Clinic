using ManageUsers.Domain.Primitives;
using ManageUsers.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageUsers.Domain
{
    public class Administrator : Entity
    {
        private Administrator(
            Guid id,
            FullName fullName,
            Guid applicationUserId
        ) : base(id)
        {
            FullName = fullName;
            ApplicationUserId = applicationUserId;
        }

        private Administrator() { }
        public FullName FullName { get; private set; }
        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; private set; }
        public ApplicationUser? ApplicationUser { get; private set; }
        public static Administrator Create(
            Guid id,
            FullName fullName,
            Guid applicationUserId
        )
        {
            var administrator = new Administrator(
                id,
               fullName,
               applicationUserId
            );

            //some  logic to create entity
            return administrator;
        }

        public void Update(
            FullName fullName
        )
        {
            FullName = fullName;
        }
    }
}
