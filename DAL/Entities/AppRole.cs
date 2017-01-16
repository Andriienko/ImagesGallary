using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class AppRole : IdentityRole
    {
        public AppRole(string roleName) : base(roleName) { }
        public AppRole() : base() { }
    }

}
