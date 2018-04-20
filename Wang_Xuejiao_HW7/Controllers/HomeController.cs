using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wang_Xuejiao_HW7.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("MyView");
        }

        public ActionResult AppUsers()
        {
            return RedirectToAction("Index", "Members");
        }
        public ActionResult Events()
        {
            return RedirectToAction("Index", "Events");
        }
        public ActionResult Committees()
        {
            return RedirectToAction("Index", "Committees");
        }
    }
}