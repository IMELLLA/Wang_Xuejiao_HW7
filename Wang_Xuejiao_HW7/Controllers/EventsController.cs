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

namespace Wang_Xuejiao_HW7.Controllers
{
    public class EventsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Event
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Event/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Event/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AllCommittees = GetAllCommittees();
            return View();
        }

        // POST: Event/Create

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventTitle,EventDate,Location,MembersOnly")] Event @event, Int32 CommitteeID)
        {
            Committee SelectedCommittee = db.Committees.Find(CommitteeID);
            @event.SponsoringCommittee = SelectedCommittee;
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllCommittees = GetAllCommittees(@event);
            return View(@event);
        }

        // GET: Event/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(Id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllCommittees = GetAllCommittees(@event);
            ViewBag.AllUsers = GetAllUsers(@event);
            return View(@event);
        }

        // POST: Event/Edit/5
        [Authorize(Roles = "Admin")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventTitle,EventDate,Location,UsersOnly")] Event @event, Int32 CommitteeID, string[] SelectedUsers)

        {
            if (ModelState.IsValid)
            {
                //find associated event 
                Event eventToChange = db.Events.Find(@event.EventID);

                if (eventToChange.SponsoringCommittee.CommitteeID != CommitteeID)
                {
                    //find committee
                    Committee SelectedCommittee = db.Committees.Find(CommitteeID);

                    //update the committee
                    eventToChange.SponsoringCommittee = SelectedCommittee;
                }

                //change members
                //remove any existing members
                eventToChange.AppUsers.Clear();

                //if there are members to add, add them
                if (SelectedUsers != null)
                {
                    foreach (string Id in SelectedUsers)
                    {
                        AppUser UserToAdd = db.Users.Find(Id);
                        eventToChange.AppUsers.Add(UserToAdd);
                    }
                }

                //update the rest of the fields
                eventToChange.EventTitle = @event.EventTitle;
                eventToChange.EventDate = @event.EventDate;
                eventToChange.EventLocation = @event.EventLocation;
                eventToChange.MembersOnly = @event.MembersOnly;

                db.Entry(eventToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            ViewBag.AllCommittees = GetAllCommittees(@event);
            ViewBag.AllUsers = GetAllUsers(@event);
            return View(@event);
        }

        // GET: Event/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public SelectList GetAllCommittees()
        {
            var query = from c in db.Committees
                        orderby c.CommitteeName
                        select c;
            List<Committee> allCommittees = query.ToList();

            SelectList allCommitteeslist = new SelectList(allCommittees, "CommitteeID", "CommitteeName");

            return allCommitteeslist;
        }

        public MultiSelectList GetAllCommittees(Event @event)
        {
            var query = from c in db.Committees
                        orderby c.CommitteeName
                        select c;
            List<Committee> allCommittees = query.ToList();

            SelectList list = new SelectList(allCommittees, "CommitteeID", "CommitteeName", @event.SponsoringCommittee.CommitteeID);
            return list;
        }

        public MultiSelectList GetAllUsers(Event @event)
        {
            var query = from m in db.Users
                        orderby m.Email
                        select m;
            List<AppUser> allUsers = query.ToList();
            List<string> SelectedUsers = new List<string>();
            foreach (AppUser m in @event.AppUsers)
            {
                SelectedUsers.Add(m.Id);
            }
            MultiSelectList allUsersList = new MultiSelectList(allUsers, "Id", "Email", SelectedUsers);


            return allUsersList;
        }

    }
}

