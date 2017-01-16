using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ContentImage { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        public virtual IEnumerable<Message> Messages { get; set; }

        public Image()
        {
            Messages = new List<Message>();
        }
    }
}
