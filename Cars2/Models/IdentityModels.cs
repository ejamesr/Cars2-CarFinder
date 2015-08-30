using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Cars2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private enum Connections {
            DefaultConnection,      // local connection - default
            CFWebHCL3               // my database on Azure
        }
        //
        // Set 'connection' to one of the above enums
        //
        private static readonly string connection = Connections.

            DefaultConnection       // <--- change this value to one of the above enums

            .ToString();


        public ApplicationDbContext()
            : base(connection, throwIfV1Schema: false)
        {

        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static readonly bool IsDefault = 
            connection.Equals(Connections.DefaultConnection.ToString());
    }
}