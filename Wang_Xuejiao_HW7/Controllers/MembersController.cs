using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wang_Xuejiao_HW7.DAL;
using Wang_Xuejiao_HW7.Models;
using Microsoft.AspNet.Identity;


namespace Wang_Xuejiao_HW7.Controllers
{
    public class MembersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Members
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Members/Details/5
        [Authorize]
        public ActionResult Details(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser member = db.Users.Find(Id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        [Authorize]
        public ActionResult Create()
        {

            return RedirectToAction("Login", "Accounts");
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Majors,text")] AppUser member)
        {
            if (ModelState.IsValid)
            {

                db.Users.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: AppUsers/Edit/5
        [Authorize]
        public ActionResult Edit(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser member = db.Users.Find(Id);
            if (member == null)
            {
                return HttpNotFound();
            }
            if (member.Id != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Accounts");
            }
            ViewBag.allEvents = GetAllEvents(member);
            return View(member);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,Majors,text")] AppUser member, int[] SelectedEvents)

        {
            if (ModelState.IsValid)
            {
                AppUser memberToChange = db.Users.Find(@member.Id);
                memberToChange.Events.Clear();


                if (SelectedEvents != null)
                {
                    foreach (int EventID in SelectedEvents)
                    {
                        Event eventToAdd = db.Events.Find(EventID);
                        memberToChange.Events.Add(eventToAdd);
                    }
                }

                memberToChange.FirstName = @member.FirstName;
                memberToChange.LastName = @member.LastName;
                memberToChange.Email = @member.Email;
                memberToChange.Majors = @member.Majors;
                memberToChange.PhoneNumber = @member.PhoneNumber;
                memberToChange.OkToText = @member.OkToText;

                db.Entry(memberToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.allEvents = GetAllEvents(@member);
            return View(member);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser member = db.Users.Find(Id);
            if (member == null)
            {
                return HttpNotFound();
            }

            if (member.Id != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Accounts");
            }
            return View(member);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Id)
        {
            AppUser member = db.Users.Find(Id);
            db.Users.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public MultiSelectList GetAllEvents(AppUser Member)
        {
            var query = from e in db.Events
                        orderby e.EventDate
                        select e;
            List<Event> allEvents = query.ToList();

            List<Int32> SelectedEvents = new List<Int32>();
            foreach (Event e in Member.Events)
            {
                SelectedEvents.Add(e.EventID);
            }
            MultiSelectList allEventList = new MultiSelectList(allEvents, "EventID", "EventTitle", SelectedEvents);
            return allEventList;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
