using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace Wang_Xuejiao_HW7.Models
{
    public enum Major {[Display(Name = "Accounting")] Accounting, [Display(Name = "Business Honors")] BusinessHonors, [Display(Name = "Finance")] Finance, [Display(Name = "International Business")] International_Business, [Display(Name = "Management")] Management, [Display(Name = "MIS")] MIS, [Display(Name = "Marketing")] Marketing, [Display(Name = "Supply Chain Management")] Supply_Chain_Management, [Display(Name = "STM")] STM }

    public class AppUser : IdentityUser
    {

        [Required(ErrorMessage = "First Name Required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "May we sent you text messages?")]
        [Display(Name = "Okay to text?")]
        public bool OkToText { get; set; }

        [Required] 
        [Display(Name = "Major")]
        public Major Majors { get; set; }

        public virtual List<Event> Events { get; set; }

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // NOTE: The authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;

        }
    }
}


