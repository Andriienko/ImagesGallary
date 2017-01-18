using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Identity;
using DAL.EF;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repositories;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationContext Db { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        FriendRepository Friends { get; }
        IRepository<Image> Images { get; }
        IRepository<Message> Messages { get; }
        IProfileManager ProfileManager { get; }
        Task SaveAsync();
    }
}
