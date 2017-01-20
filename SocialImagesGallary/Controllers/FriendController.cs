using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;

namespace SocialImagesGallary.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private  IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        [HttpPost]
        public ActionResult GetFriends(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                userName = User.Identity.Name;
            }
            var allFriends = UserService.GetAllFriends(userName);
            return Json(allFriends);
        }
        [HttpPost]
        public async Task<ActionResult> AddFriend(string friendName)
        {
            var userName = User.Identity.Name;
            var friend = await UserService.AddFriend(userName, friendName);
            return Json(friend);
        }
    }
}