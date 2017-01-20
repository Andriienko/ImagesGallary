using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.DTOs;
using System.Security.Claims;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        IEnumerable<UserDTO> GetAllUsers(string userName);
        Task<UserDTO> AddFriend(string userName, string friendName);
        IEnumerable<UserDTO> GetAllFriends(string userName);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        ProfileDTO GetProfile(string userName);
        bool UploadUserImage(string userName, string path);
        bool IsFriends(string userOne, string userTwo);
        string RenderAvatar(string userName);
    }
}
