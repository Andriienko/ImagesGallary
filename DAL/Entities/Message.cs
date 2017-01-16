using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime SendTime { get; set; }

        public int ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
