using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDatabaseModels
{
    public class User
    {
        public User()
        {
            UserTokens = new HashSet<UserToken>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(254)]
        [DataType(DataType.EmailAddress)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }


        public virtual ICollection<UserToken> UserTokens { get; set; }

    }
}
