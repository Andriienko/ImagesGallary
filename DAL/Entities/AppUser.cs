using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Image> Images { get; set; }

       public virtual ICollection<AppUser> Friends { get; set; }

        public AppUser()
        {
            Images = new List<Image>();
            Friends = new List<AppUser>();
        }
    }
}
