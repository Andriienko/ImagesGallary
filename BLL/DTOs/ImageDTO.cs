using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
   public class ImageDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Path { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        public IEnumerable<Message> Messages { get; set; }

    }
}
