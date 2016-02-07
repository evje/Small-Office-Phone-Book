using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SOPB.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SOPB.Identity", throwIfV1Schema: false)
        {
        }
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new SOPBIdentityInit());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<SOPB.WebUI.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class SOPBIdentityInit : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(ApplicationDbContext context)
        {
            var userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleMgr = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            string roleName = "admin";
            string password = "1_MYpassword";
            string email = "admin@mail.com";

            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new ApplicationRole(roleName));
            }

            var user = userMgr.FindByEmail(email);
            if (user == null)
            {
                userMgr.Create(new ApplicationUser { UserName = email, Email = email }, password);
                user = userMgr.FindByEmail(email);
            }
            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            string roleName1 = "user";
            string password1 = "1_MYpassword";
            string email1 = "user@mail.com";

            if (!roleMgr.RoleExists(roleName1))
            {
                roleMgr.Create(new ApplicationRole(roleName1));
            }

            var user1 = userMgr.FindByEmail(email1);
            if (user1 == null)
            {
                userMgr.Create(new ApplicationUser { UserName = email1, Email = email1 }, password1);
                user1 = userMgr.FindByEmail(email1);
            }
            if (!userMgr.IsInRole(user1.Id, roleName1))
            {
                userMgr.AddToRole(user1.Id, roleName1);
            }
        }
    }
}