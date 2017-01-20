using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            AppUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new AppUser { Email = userDto.Email, UserName = userDto.UserName };
                IdentityResult result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                UserProfile profile = new UserProfile
                {
                    Id = user.Id,
                    Adress = userDto.Address,
                    Name = userDto.Name
                };
                Database.ProfileManager.Create(profile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successfull", "");
            }
            return new OperationDetails(false, "User with same email already exist", "Email");
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claims = null;
            AppUser user = await Database.UserManager.FindAsync(userDto.UserName,userDto.Password);
            if (user != null)
                claims = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claims;
        }

        public bool UploadUserImage(string userName,string path)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var isOk = Database.ProfileManager.UploadImage(user.Id, path);
                return isOk;
            }
            return false;
        }
        public string RenderAvatar(string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var path = Database.ProfileManager.LoadAvatar(user.Id);
                return path;
            }
            return String.Empty;
        }
        public ProfileDTO GetProfile(string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user != null)
            {
                var profile = Database.ProfileManager.GetProfile(user.Id);
                var profileDto = new ProfileDTO
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    UserName = user.UserName,
                    Adress = profile.Adress,
                    Email = user.Email
                };
                return profileDto;
            }
            return  new ProfileDTO();
        }

        public async Task<UserDTO> AddFriend(string userName,string friendName)
        {
            AppUser user = await Database.UserManager.FindByNameAsync(userName);
            AppUser friend = await Database.UserManager.FindByNameAsync(friendName);
            if (user != null && friend!=null)
            {
                //var newFriend=new Friend
                //{
                //    FirstId = user.Id,
                //    SecondId = friend.Id
                //};
                if (!user.Friends.Contains(friend)&& user!=friend)
                {
                    user.Friends.Add(friend);
                    Database.Db.SaveChanges();
                    var friendDto = new UserDTO
                    {
                        Id = friend.Id,
                        UserName = friend.UserName,
                        Email = friend.Email
                    };
                    return friendDto;
                }
                
                //Database.Friends.Create(newFriend);

            }
            return new UserDTO();
        }
        public IEnumerable<UserDTO> GetAllFriends(string userName)
        {
            List<UserDTO> friendDtos = new List<UserDTO>();
            AppUser userr = Database.UserManager.FindByName(userName);
            if (userr != null)
            {
                var friends = userr.Friends.ToList();//Not good
                    foreach (var friend in friends)
                    {
                        //var user = Database.UserManager.Users.FirstOrDefault(u => u.Id == friend.SecondId);
                        //if (user != null)
                        //{
                        var userDto = new UserDTO
                        {
                            Id = friend.Id,
                            UserName = friend.UserName,
                            Email = friend.Email
                        };
                        friendDtos.Add(userDto);
                        //}
                    }
                //var allFriends = Database.Friends.GetAll().ToList();
                //IEnumerable<Friend> userFriends = allFriends.Where(f => f.FirstId == userr.Id);
               
            }
            return friendDtos;
        }

        public bool IsFriends(string userOne, string userTwo)
        {
            bool isFriends = false;
            var user=Database.UserManager.FindByName(userTwo);
            var user2 = Database.UserManager.FindByName(userOne);
            if (user != null && user2!=null)
            {
                isFriends=user.Friends.Contains(user2);
            }
            return isFriends;
        }

        public IEnumerable<UserDTO> GetAllUsers(string userName)
        {
            List<UserDTO> users = new List<UserDTO>();
            foreach (var user in Database.Db.Users.Where(u=>u.UserName!=userName))
            {
                var userDto = new UserDTO {
                    Id=user.Id,
                    UserName=user.UserName,
                    Email=user.Email
                };
                users.Add(userDto);
            }
            return users;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new AppRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }
    }
}
