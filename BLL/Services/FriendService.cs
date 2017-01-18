using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FriendService : IFriendService
    {
        IUnitOfWork Database { get; set; }
        public FriendService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void AddFriend(FriendDTO newFriend)
        {
            var friend = new Friend
            {
                FirstId = newFriend.FirstId,
                SecondId = newFriend.SecondId
            };
            Database.Friends.Create(friend);
        }

        public IEnumerable<FriendDTO> GetAllFriend(string userId)
        {
            List<FriendDTO> friends=new List<FriendDTO>();
            foreach (var friend in Database.Friends.GetAll())
            {
                var friendDto = new FriendDTO
                {
                    Id = friend.Id,
                    FirstId = friend.FirstId,
                    SecondId = friend.SecondId
                };
                friends.Add(friendDto);
            }
            return friends;
        }

        public void RemoveFriend(FriendDTO item)
        {
            Database.Friends.Delete(item.Id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
