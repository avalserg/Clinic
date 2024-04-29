using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users
{
    public class PatientTypeConfiguration:IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            
            
            
            builder.HasData(new Patient()
            {
                Id = 2,
                FirstName = "Patient1FirstName",
                LastName = "Patient1LastName",
                Patronymic = "Patient1Patronymic",
                DateBirthday = DateTime.Now,
                Address = "Patient1Address",
                Phone = "Patient1Phone",
                Avatar = "",
                ApplicationUserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b")

            });
        }
    }
}
