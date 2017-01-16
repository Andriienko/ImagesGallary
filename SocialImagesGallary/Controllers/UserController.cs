using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Services;
using BLL.Interfaces;

namespace SocialImagesGallary.Controllers
{
    public class UserController : ApiController
    {
        IUserService userService;
        public UserController(IUserService serv)
        {
            userService = serv;
        }

        protected override void Dispose(bool disposing)
        {
            userService.Dispose();
            base.Dispose(disposing);
        }
    }
}
