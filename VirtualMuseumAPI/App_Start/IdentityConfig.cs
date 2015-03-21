using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using VirtualMuseumAPI.Models;
using System.Data.Entity;
using System.Web;
using VirtualMuseumAPI.Helpers;
using System.IO;
using System.Web.Hosting;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
namespace VirtualMuseumAPI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager
    : UserManager<ApplicationUser, string>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, string> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<ApplicationUser, VirtualMuseumAPI.Models.ApplicationRole, string,
                    ApplicationUserLogin, ApplicationUserRole,
                    ApplicationUserClaim>(context.Get<ApplicationDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationRoleManager : RoleManager<VirtualMuseumAPI.Models.ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<VirtualMuseumAPI.Models.ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            return new ApplicationRoleManager(
                new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
        }
    }


    public class ApplicationDbInitializer
        : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityRolesForEF(context);
            InitializeIdentityForEF(context);
            InitializePrivacyRoles(context);
            base.Seed(context);
        }

        public static void InitializeIdentityRolesForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current
               .GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roleManager = HttpContext.Current
                .GetOwinContext().Get<ApplicationRoleManager>();

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName("Admin");
            if (role == null)
            {
                role = new VirtualMuseumAPI.Models.ApplicationRole("Admin");
                roleManager.Create(role);
            }

            //Create Role Manager if it does not exist
            role = roleManager.FindByName("Manager");
            if (role == null)
            {
                role = new VirtualMuseumAPI.Models.ApplicationRole("Manager");
                roleManager.Create(role);
            }

            //Create Role Curator if it does not exist
            role = roleManager.FindByName("Curator");
            if (role == null)
            {
                role = new VirtualMuseumAPI.Models.ApplicationRole("Curator");
                roleManager.Create(role);
            }

            //Create Role Visitor if it does not exist
            role = roleManager.FindByName("Curator");
            if (role == null)
            {
                role = new VirtualMuseumAPI.Models.ApplicationRole("Visitor");
                roleManager.Create(role);
            }

        }
 
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current
                .GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roleManager = HttpContext.Current
                .GetOwinContext().Get<ApplicationRoleManager>();

            const string userName = "VirtualMuseum";
            const string email = "museum@awesomepeople.tv";
            const string password = "@wesomePeople_20";
            const string roleName = "Admin";

            var user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = userName, Email = email };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
                VirtualMuseumFactory factory = new VirtualMuseumFactory();
                string script = File.ReadAllText(HostingEnvironment.MapPath("~/database_script.sql"));
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VirtualMuseumConnectionString"].ToString());
                Server server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);

               //db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, script);
                //db.SaveChanges();
                factory.createPrivateArtist(user.UserName, user.Id);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

        public static void InitializePrivacyRoles(ApplicationDbContext db)
        {
            VirtualMuseumFactory factory = new VirtualMuseumFactory();
            var userManager = HttpContext.Current
                .GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindByName("VirtualMuseum");
            factory.createPrivacyLevel("PUBLIC", "public", user.Id);
            factory.createPrivacyLevel("PRIVATE", "private", user.Id);
        }


    }
}
