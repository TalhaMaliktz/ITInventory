using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ITInventory.Models.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ITInventory.Models
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
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceSpecifications> DeviceSpecifications { get; set; }

        public DbSet<IP> IP { get; set; }
        public DbSet<Specifications> Specifications { get; set; }
        public DbSet<DeviceType> Types { get; set; }

        public DbSet<DeviceHistory> DeviceHistory { get; set; }
        public DbSet<Location> Locations { get; set; }

        public System.Data.Entity.DbSet<ITInventory.Models.Entities.DeviceToDevice> DeviceToDevices { get; set; }
    }
}