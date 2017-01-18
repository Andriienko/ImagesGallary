﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;


namespace BLL.Interfaces
{
   public  interface IFriendService:IDisposable
   {
       void AddFriend(FriendDTO item);
       void RemoveFriend(FriendDTO item);
       IEnumerable<FriendDTO> GetAllFriend(string userId);
   }
}
