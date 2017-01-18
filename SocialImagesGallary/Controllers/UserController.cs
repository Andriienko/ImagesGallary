using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BLL.Services;
using BLL.Interfaces;
using DAL.Entities;
using BLL.DTOs;

namespace SocialImagesGallary.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IEnumerable<UserDTO> GetAllUsers()
        {
            var allUsers= userService.GetAllUsers();
            return allUsers;
        }

    }
}
