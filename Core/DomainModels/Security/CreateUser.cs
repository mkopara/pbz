using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModels.Security
{
    public class CreateUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        //public string PasswordAgain { get; set; }

        public string PhoneNumber { get; set; }
    }
}
