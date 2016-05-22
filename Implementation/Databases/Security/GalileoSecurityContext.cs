using Core.DatabaseModels.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Databases.Security
{
    public class GalileoSecurityContext : DbContext
    {
        public GalileoSecurityContext() : base("GalileoSecurity")
        {

        }
        public virtual DbSet<Core.DatabaseModels.Security.User> Users { get; set; }
        public virtual DbSet<Core.DatabaseModels.Security.UserToken> UserTokens { get; set; }
    }
}
