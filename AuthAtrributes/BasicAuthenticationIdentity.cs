using Core.DomainModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AuthAtrributes
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {

        /// <summary>
        /// Get/Set for password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Get/Set for UserName
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get/Set for UserId
        /// </summary>
        public int UserId { get; set; }

        public TokenInfo TokenInfo { get; set; }

        /// <summary>
        /// Basic Authentication Identity Constructor
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public BasicAuthenticationIdentity(string email, string password)
                : base(email, "Basic")
        {
            Password = password;
            Email = email;
        }
    }
}
