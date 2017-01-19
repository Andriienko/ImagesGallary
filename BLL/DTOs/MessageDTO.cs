using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string SendTime { get; set; }

        public int ImageId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public static IEnumerable<MessageDTO> CreateMany(IEnumerable<Message> messages)
        {
            List<MessageDTO> messageDtos = new List<MessageDTO>();
            foreach (var message in messages)
            {
                var msgDto = new MessageDTO
                {
                    Id = message.Id,
                    Content = message.Content,
                    SendTime = message.SendTime.ToUniversalTime().ToString(),
                    ImageId =  message.ImageId,
                    UserId = message.UserId,
                    UserName =message.User.UserName
                };
                messageDtos.Add(msgDto);
            }
            return messageDtos;
        }

        public static Message ConvertToMessage(MessageDTO msgDto)
        {
            var msg = new Message
            {
                Content = msgDto.Content,
                SendTime = DateTime.Parse(msgDto.SendTime),
                ImageId = msgDto.ImageId,
                UserId = msgDto.UserId,
            };
            return msg;
        }
    }
}
