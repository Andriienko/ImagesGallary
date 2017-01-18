using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        public ApplicationContext Db { get; }

        public FriendRepository Friends { get; }
        public IRepository<Image> Images { get; }
        public IRepository<Message> Messages { get; }
        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public IProfileManager ProfileManager { get; }
        public IdentityUnitOfWork(string connectioString)
        {
            Db = new ApplicationContext(connectioString);
            UserManager = new ApplicationUserManager(new UserStore<AppUser>(Db));
            RoleManager = new ApplicationRoleManager(new RoleStore<AppRole>(Db));
            ProfileManager = new ProfileManager(Db);
            Messages = new MessageRepository(Db);
            Images = new ImageRepository(Db);
            Friends=new FriendRepository(Db);
        }
        public async Task SaveAsync()
        {
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    ProfileManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
