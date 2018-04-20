using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

using Wang_Xuejiao_HW7.DAL;
using Wang_Xuejiao_HW7.Models;

namespace Wang_Xuejiao_HW7.Migrations
{
    //add identity data
    public class SeedIdentity
    {
        public void AddAdmin(AppDbContext db)
        {
            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));



            //check to see if the admin has been added
            //AppUser admin = db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

            //if admin hasn't been created, then add them
            //if (admin == null)
            //{
            //    admin = new AppUser();
            //    admin.UserName = "admin@example.com";
            //    admin.FirstName = "Admin";
            //    admin.PhoneNumber = "(512)555-5555";
            //    admin.Email= "admin@example.com";
            //    admin.LastName = "Bevo";
            //    admin.OkToText = true;
            //    admin.Majors = Major.Accounting;

            //    var result = UserManager.Create(admin, "Abc123!");
            //    db.SaveChanges();
            //    admin = db.Users.First(u => u.UserName == "admin@example.com");
            //}

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (RoleManager.RoleExists("Admin") == false)
            {
                RoleManager.Create(new AppRole("Admin"));
            }

            if (RoleManager.RoleExists("Member") == false)
            {
                RoleManager.Create(new AppRole("Member"));
            }

            if (RoleManager.RoleExists("Employee") == false)
            {
                RoleManager.Create(new AppRole("Employee"));
            }

            //make sure user is in role
            //if (UserManager.IsInRole(admin.Id, "Admin") == false)
            //{
            //    UserManager.AddToRole(admin.Id, "Admin");
            //}

            //save changes
            db.SaveChanges();
        }

    }
}