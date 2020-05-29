using System.Web.Configuration;
using iSmash.Controllers;

namespace iSmash.Migrations
{
    using iSmash.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<iSmash.Models.ApplicationDbContext>
    {
        ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUser user = new ApplicationUser();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(iSmash.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            List<TicketType> ticketType = new List<TicketType>();
            List<TicketPriority> ticketPriorities = new List<TicketPriority>();

            #region role creation
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion

            #region user creation and role assignment
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var demoPassword = WebConfigurationManager.AppSettings["DemoPassword"];

            if (!context.Users.Any(u => u.Email == "juggernott81@gmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "juggernott81@gmail.com",
                    Email = "juggernott81@gmail.com",
                    FirstName = "Lawson",
                    LastName = "Ott",
                    DisplayName = "JuggernOtt81",
                    AvatarPath = "/Avatars/AvatarInfiniteCode.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.Email == "demoadmin@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demoadmin@mailinator.com",
                    Email = "demoadmin@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "DemoAdmin",
                    AvatarPath = "/Avatars/AvatarBlackInverted.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, demoPassword);
                userManager.AddToRoles(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.Email == "demoprojectmanager@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demoprojectmanager@mailinator.com",
                    Email = "demoprojectmanager@mailinator.com",
                    FirstName = "Demo",
                    LastName = "ProjectManager",
                    DisplayName = "DemoProjectManager",
                    AvatarPath = "/Avatars/AvatarRed.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, demoPassword);
                userManager.AddToRoles(user.Id, "ProjectManager");
            }
            if (!context.Users.Any(u => u.Email == "demodeveloper@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demodeveloper@mailinator.com",
                    Email = "demodeveloper@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "DemoDeveloper",
                    AvatarPath = "/Avatars/AvatarBlue.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, demoPassword);
                userManager.AddToRoles(user.Id, "Developer");
            }
            if (!context.Users.Any(u => u.Email == "demosubmitter@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "demosubmitter@mailinator.com",
                    Email = "demosubmitter@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "DemoSubmitter",
                    AvatarPath = "/Avatars/AvatarGreen.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, demoPassword);
                userManager.AddToRoles(user.Id, "Submitter");
            }

            context.SaveChanges();
            #endregion

            #region seed some projects 
            for (int i = 0; i < 25; i++)
            {
                string name = "Demo Project " + i;
                if (!context.Projects.Any(p => p.Name == name))
                {
                    context.Projects.AddOrUpdate(p => p.Name, new Project
                    {
                        Name = name,
                        Priority = 0,
                        Status = 0,
                        Description = "this is the description of " + name,
                        ProjectManagerId = db.Users.FirstOrDefault(u => u.Email == "DemoProjectManager@mailinator.com").Id,
                        Created = DateTime.Now
                    });
                }
            }

            context.SaveChanges();
            #endregion

            #region tickets(type, priority, status)
            if (!context.TicketType.Any(r => r.Name == "NewFeatureRequest"))
            {

                context.TicketType.AddOrUpdate(new TicketType { Name = "NewFeatureRequest" });
            }
            if (!context.TicketType.Any(r => r.Name == "BugReport"))
            {

                context.TicketType.AddOrUpdate(new TicketType { Name = "BugReport" });
            }
            if (!context.TicketType.Any(r => r.Name == "CustomerFeedback"))
            {

                context.TicketType.AddOrUpdate(new TicketType { Name = "CustomerFeedback" });
            }
            if (!context.TicketType.Any(r => r.Name == "ProductReturn"))
            {

                context.TicketType.AddOrUpdate(new TicketType { Name = "ProductReturn" });
            }
            if (!context.TicketType.Any(r => r.Name == "AbuseReport"))
            {

                context.TicketType.AddOrUpdate(new TicketType { Name = "AbuseReport" });
            }
            if (!context.TicketType.Any(r => r.Name == "Other"))
            {

                context.TicketType.AddOrUpdate(new TicketType { Name = "Other" });
            }

            if (!context.TicketPriorities.Any(r => r.Name == "TOP"))
            {

                context.TicketPriorities.AddOrUpdate(new TicketPriority { Name = "TOP" });
            }
            if (!context.TicketPriorities.Any(r => r.Name == "Secondary"))
            {

                context.TicketPriorities.AddOrUpdate(new TicketPriority { Name = "Secondary" });
            }
            if (!context.TicketPriorities.Any(r => r.Name == "BackBurner"))
            {

                context.TicketPriorities.AddOrUpdate(new TicketPriority { Name = "BackBurner" });
            }

            if (!context.TicketStatus.Any(r => r.Name == "NEW"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "NEW" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "ASSIGNED"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "ASSIGNED" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "Fixed"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "Fixed" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "InReview"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "InReview" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "FixAccepted"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "FixAccepted" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "FixRejected"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "FixRejected" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "Resolved"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "Resolved" });
            }
            if (!context.TicketStatus.Any(r => r.Name == "Archived"))
            {
                context.TicketStatus.AddOrUpdate(new TicketStatus { Name = "Archived" });
            }
            context.SaveChanges();
            #endregion



            #region seed some tickets
            string pmi = db.Users.FirstOrDefault(u => u.Email == "DemoProjectManager@mailinator.com").Id;
            var status = db.TicketStatus.FirstOrDefault(t => t.Name == "NEW");
            var priority = db.TicketPriorities.FirstOrDefault(t => t.Name == "TOP");
            var type = db.TicketType.FirstOrDefault(t => t.Name == "Other");
            var dev = db.Users.FirstOrDefault(u => u.Email == "DemoDeveloper@mailinator.com");
            var sub = db.Users.FirstOrDefault(u => u.Email == "DemoSubmitter@mailinator.com");
            var projId = db.Projects.FirstOrDefault(p => p.Id > 0);

            for (int i = 0; i < 25; i++)
            {
                string title = "Demo Ticket " + i;

                context.Tickets.AddOrUpdate(t => t.Title, new Ticket
                {
                    Title = title,
                    Description = title + " has a unique description here.",
                    Created = DateTime.Now,
                    TicketPriorityId = priority.Id,
                    TicketStatusId = status.Id,
                    TicketTypeId = type.Id,
                    DeveloperId = dev.Id,
                    SubmitterId = sub.Id,
                    ProjectId = projId.Id
                });
            }
            #endregion
        }
    }
}
