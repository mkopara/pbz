using Core.DatabaseModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModels.Security
{
    public class TokenInfo
    {
        public TokenInfo()
        { }

        public TokenInfo(UserToken token)
        {
            Expires = token.ExpiresOn;
            UserId = token.UserId;
            Expired = token.ExpiresOn < DateTime.Now;
            Token = token.Token;
        }

        public int UserId { get; set; }
        public bool Expired { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }

    }
}
