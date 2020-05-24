using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using iSmash.Helpers;
using iSmash.Models;
using iSmash.ViewModels;
using Microsoft.AspNet.Identity;

namespace iSmash.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projHelper = new ProjectsHelper();
        private RolesHelper rolesHelper = new RolesHelper();
        private TicketHelper ticketHelper = new TicketHelper();
        private TicketAttachment ticketAttachment = new TicketAttachment();
        private HistoryHelper historyHelper = new HistoryHelper();
        private NotificationHelper notificationHelper = new NotificationHelper();

        public ActionResult Dashboard(int id)
        {
            return View(db.Tickets.Find(id));
        }

        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult ManageTicketAssignments()
        {
            //load a multi select of users
            ViewBag.UserIds = new SelectList(db.Users, "Id", "FullName");

            //load a multi select of projects
            ViewBag.TicketIds = new SelectList(db.Tickets, "Id", "Title");

            //load the viewmodel
            return View(db.Tickets.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageTicketAssignments(List<string> userIds, List<int> ticketIds)
        {
            



            //if and only if at least one person is chosen, do the following:
            if (userIds == null || ticketIds == null)
            {
                return RedirectToAction("ManageTicketAssignments");
            }
            //add each SELECTED USER to each SELECTED PROJECT
            foreach (var userId in userIds)
            {
                foreach (var ticketId in ticketIds)
                {
                    //projHelper.AddUserToProject(userId, projectId);
                    ticketHelper.AddDevToTicket(userId, ticketId);
                }
            }
            return RedirectToAction("ManageTicketAssignments");
        }


 

        // GET: Tickets


        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Developer).Include(t => t.Project).Include(t => t.Submitter).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            //return View(tickets.ToList());

            var allTickets = ticketHelper.ListMyTickets().ToList();
            //var ticketIndexVMs = new List<TicketIndexViewModel>();
            //var userId = new User.Identity.GetUserId();
            //var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
            //foreach (var ticket in ticketHelper.GetMyTickets())
            //{
            //    ticketHelper.GetMyTickets();
            //}

            return View(allTickets);
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.SubmitterId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name");



            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,DeveloperId,Title,Description,IsArchived")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.TicketPriorityId = 1;
                ticket.Created = DateTime.Now;
                ticket.Updated = null;
                ticket.SubmitterId = User.Identity.GetUserId();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.SubmitterId = new SelectList(db.Users, "Id", "FirstName", ticket.SubmitterId);
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperId = new SelectList(rolesHelper.UsersInRole("Developer"), "Id", "FullName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketStatusId,TicketPriorityId,DeveloperId,Title,Description,IsArchived,Created,SubmitterId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //we want to use AsNoTracking() to get a Memento Ticket object
                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);


                ticket.Updated = DateTime.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                var newTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);

                historyHelper.ManageHistoryRecordCreation(oldTicket, ticket);
                notificationHelper.ManageNotifications(oldTicket, newTicket);

                return RedirectToAction("Index", "TicketHistories");

            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FirstName", ticket.DeveloperId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.SubmitterId = new SelectList(db.Users, "Id", "FirstName", ticket.SubmitterId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketType, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
    }
}
