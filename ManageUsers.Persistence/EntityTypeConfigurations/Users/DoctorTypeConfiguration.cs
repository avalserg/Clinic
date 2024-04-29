using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users
{
    public class DoctorTypeConfiguration:IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            
            
            builder.HasData(new Doctor()
            {
                Id = 3,
                FirstName = "Doctor1FirstName",
                LastName = "Doctor1LastName",
                Patronymic = "Doctor1Patronymic",
                DateBirthday = DateTime.Now,
                Address = "Doctor1Address",
                Phone = "Doctor1Phone",
                Experience = 1,
                CabinetNumber = 1,
                Category = "High",
                ApplicationUserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950d")
            });
        }
    }
}
