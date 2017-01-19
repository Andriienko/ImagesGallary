using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using DAL.Entities;
using DAL.Identity;
using DAL.Repositories;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace DAL.EF
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext(string connectionString) : base(connectionString) { }
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new IdentityDbInit());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("FirstId");
                    m.MapRightKey("SecondId");
                    m.ToTable("Friends");
                });
            base.OnModelCreating(modelBuilder);

        }
    }
    public class IdentityDbInit:DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public  void PerformInitialSetup(ApplicationContext context)
        {
            ApplicationUserManager userMgr = new ApplicationUserManager(new UserStore<AppUser>(context));
            ApplicationRoleManager roleMgr = new ApplicationRoleManager(new RoleStore<AppRole>(context));
            ProfileManager profileMgr = new ProfileManager(context);
                List<string> roles = new List<string> { "Admin", "User" };
                string userName = "Admin";
                string password = "Pass1@";
                string email = "roman.andriienko@globallogic.com";
                foreach (string roleName in roles)
                {
                    var role =roleMgr.FindByName(roleName);
                    if (role == null)
                    {
                        role = new AppRole(roleName);
                        roleMgr.Create(role);
                    }
                }
            AppUser user = userMgr.FindByEmail(email);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email },password);
                user = userMgr.FindByEmail(email);
            }
            UserProfile profile = new UserProfile
            {
                Id = user.Id,
                Adress = "KBP-5r",
                Name = "Roman Andriienko"
            };
            profileMgr.Create(profile);
            bool inRole = userMgr.IsInRole(user.Id, "Admin");
            if (!inRole)
            {
               userMgr.AddToRole(user.Id, "Admin");
            }
        }
    }
}
