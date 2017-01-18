using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using BLL.Interfaces;
using BLL.Services;

namespace SocialImagesGallary
{
    public class IdentityConfig
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.CreatePerOwinContext<IImageService>(CreateImageService);
            app.CreatePerOwinContext<IFriendService>(CreateFriendService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("ApplicationDb");
        }
        private IImageService CreateImageService()
        {
            return serviceCreator.CreateImageService("ApplicationDb");
        }
        private IFriendService CreateFriendService()
        {
            return serviceCreator.CreateFriendService("ApplicationDb");
        }
    }
}