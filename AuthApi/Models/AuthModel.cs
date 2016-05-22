using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthApi.Models
{
    public class AuthModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}