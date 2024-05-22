using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageUsers.Domain.Exceptions.Base;

namespace ManageUsers.Domain.Exceptions
{
    public sealed class DoctorNotFoundDomainException:DomainException
    {
        public DoctorNotFoundDomainException(Guid doctorId) 
            : base($"The doctor with the identifier {doctorId} was not found")
        {
        }
    }
}
