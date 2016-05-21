using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Database.Security
{
    public class GalileoSecurityContext : DbContext
    {
        public GalileoSecurityContext() : base("GalileoSecurity")
        {

        }
        public virtual DbSet<UserDatabaseModels.User> Users { get; set; }
    }
}
