using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

using Wang_Xuejiao_HW7.Models;

namespace Wang_Xuejiao_HW7.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false) { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Committee> Committees { get; set; }


        //This is a dbSet that you need to make roles work
        public DbSet<AppRole> AppRoles { get; set; }
    }
}