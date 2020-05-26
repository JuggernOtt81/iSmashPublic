using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iSmash.Models;
using iSmash.ViewModels;

namespace iSmash.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper roleHelper = new RolesHelper();
        [Authorize]
        public ActionResult Dashboard()
        {
            var allTickets = db.Tickets.ToList();
            var dashboardVm = new DashboardViewModel()
            {
                TicketCount = allTickets.Count,
                HighPriorityTicketCount = allTickets.Count(t => t.TicketPriority.Name == "TOP"),
                NewTicketCount = allTickets.Count(t => t.TicketStatus.Name == "New"),
                TotalComments = 20,
                AllTickets = db.Tickets.ToList()
            };
            dashboardVm.ProjectVM.ProjectCount = db.Projects.Count();
            dashboardVm.ProjectVM.AllProjects = db.Projects.ToList();
            dashboardVm.ProjectVM.AllPMs = roleHelper.UsersInRole("PM").ToList();

            return View(dashboardVm);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}