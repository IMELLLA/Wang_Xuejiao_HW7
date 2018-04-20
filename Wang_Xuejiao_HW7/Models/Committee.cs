using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Wang_Xuejiao_HW7.Models
{
    public class Committee
    {
        public Int32 CommitteeID { get; set; }

        [Required(ErrorMessage ="name is required")]
        [Display(Name ="Committee Name")]
        public string CommitteeName { get; set; }

        public virtual List<Event> Events { get; set; }

    }
}