using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold> ({1})</p><p>Message:</p><p>{2}</p>";
                    var from = $"iSmash <{WebConfigurationManager.AppSettings["emailfrom"]}>";
                    var email = new MailMessage(from,
                        WebConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = "iSmash Contact Email",
                        Body = string.Format(body, model.FromName, model.FromEmail,
                            model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

    }
}