using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDatabaseModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(254)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Salt { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

       
    }
}
