using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DatabaseModels.Security
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }


        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ExpiresOn { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set;}
    }
}
