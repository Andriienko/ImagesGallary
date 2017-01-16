using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public string UserImage { get; set; }

        public virtual AppUser User { get; set; }

    }
}
