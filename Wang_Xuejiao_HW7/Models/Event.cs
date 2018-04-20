using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Wang_Xuejiao_HW7.Models
{
    public class Event
    {
        public Int32 EventID { get; set; }
        public string EventTitle { get; set; }

        [Display(Name ="Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime EventDate { get; set; }

        [Display(Name ="Event Location")]
        public string EventLocation { get; set; }

        [Display(Name ="Members Only?")]
        public Boolean MembersOnly { get; set; }


        public virtual Committee SponsoringCommittee { get; set; }
        public virtual List<AppUser> AppUsers { get; set; }
    }
}