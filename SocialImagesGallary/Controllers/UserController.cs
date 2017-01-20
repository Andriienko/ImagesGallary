using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BLL.Services;
using BLL.Interfaces;
using DAL.Entities;
using BLL.DTOs;
using Microsoft.AspNet.Identity.Owin;

namespace SocialImagesGallary.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        public IEnumerable<UserDTO> GetAllUsers(string userName)
        {
            var allUsers= _userService.GetAllUsers(userName);
            return allUsers;
        }


    }
}
