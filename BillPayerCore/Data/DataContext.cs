using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BillPayerCore.DataModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BillPayerCore.Data
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public User UserInfo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<User> UserInfos { get; set; }
        public DbSet<HouseHold> HouseHolds { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillSplit> BillSplits { get; set; }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}
