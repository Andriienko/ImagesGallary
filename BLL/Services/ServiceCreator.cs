using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Repositories;

namespace BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
        public IImageService CreateImageService(string connection)
        {
            return new ImageService(new IdentityUnitOfWork(connection));
        }
        public IFriendService CreateFriendService(string connection)
        {
            return new FriendService(new IdentityUnitOfWork(connection));
        }
    }
}
