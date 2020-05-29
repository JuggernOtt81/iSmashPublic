//using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using iSmash.Models;
using System.Web;
using System.Linq;
using iSmash.Helpers;
using iSmash.Controllers;

namespace iSmash.Helpers
{
    public class TicketHelper
    {
        private ProjectsHelper projectHelper = new ProjectsHelper();
        private RolesHelper roleHelper = new RolesHelper();
        private ApplicationDbContext db = new ApplicationDbContext();


        //public bool IsUserOnTicket(string userId, int ticketId)
        //{
        //    var ticket = db.Tickets.Find(ticketId);
        //    var flag = ticket.Users.Any(u => u.Id == userId);
        //    return (flag);
        //}

        public ICollection<Ticket> ListMyTickets()
        {
            var myTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Admin":
                case "DemoAdmin":
                    myTickets.AddRange(db.Tickets);
                    break;
                case "Project Manager":
                case "DemoPM":
                    //myTickets.AddRange(user.Projects.Where(p => p.IsArchived == false).SelectMany(p => p.Tickets));
                    myTickets.AddRange(db.Projects.Where(p => p.ProjectManagerId == userId).SelectMany(p => p.Tickets));
                    break;
                case "Developer":
                case "DemoDeveloper":
                    myTickets.AddRange(db.Tickets.Where(t => t.IsArchived == false).Where(t => t.DeveloperId == userId));
                    break;
                case "Submitter":
                case "DemoSubmitter":
                    myTickets.AddRange(db.Tickets.Where(t => t.IsArchived == false).Where(t => t.SubmitterId == userId));
                    break;
            }
            return myTickets;
        }
        //public ICollection<Ticket> ListUserTickets(string userId)
        //{
        //    ApplicationUser user = db.Users.Find(userId);
        //    var tickets = user.Ticket.ToList();
        //    return tickets;
        //}
        public void AddSubToTicket(string userId, int ticketId)
        {
            Ticket ticket = db.Tickets.Find(ticketId);
            var newUser = db.Users.Find(userId);
            ticket.SubmitterId = newUser.Id;

            db.SaveChanges();
        }
        public void AddDevToTicket(string userId, int ticketId)
        {
            Ticket ticket = db.Tickets.Find(ticketId);
            var newUser = db.Users.Find(userId);
            ticket.DeveloperId = newUser.Id;

            db.SaveChanges();
        }

        //public ICollection<ApplicationUser> UsersOnTicket(int ticketId)
        //{
        //    var dev = db.Tickets.Find(UsersOnTicket);
        //    var sub = db.Tickets.Find(UsersOnTicket );

        //    return ticketUsers;
        //}

        //public ICollection<ApplicationUser> UsersNotOnProject(int ticketId)
        //{
        //    return db.Users.Where(u => u.Ticket.All(p => p.Id != ticketId)).ToList();
        //}

        public List<Ticket> GetMyTickets()
        {
            var  allTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                    allTickets = db.Tickets.ToList();
                    break;

                case "ProjectManager":
                    allTickets = db.Users.Find(userId).Projects.SelectMany(p =>p.Tickets).ToList();
                    break;

                case "Developer":
                    allTickets = db.Tickets.Where(t => t.DeveloperId == userId).ToList();
                    break;

                case "Submitter":
                    allTickets = db.Tickets.Where(t => t.SubmitterId == userId).ToList();
                    break;

                //case "DemoUser":
                //    allTickets = db.Tickets.ToList();
                //    break;
            }

            return allTickets;
        }
    }
}