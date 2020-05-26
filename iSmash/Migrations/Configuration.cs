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
                    AvatarPath = "/Avatars/defaultAvatar.png",
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
                    AvatarPath = "/Avatars/defaultAvatar.png",
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
                    AvatarPath = "/Avatars/defaultAvatar.png",
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
                    AvatarPath = "/Avatars/defaultAvatar.png",
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
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, demoPassword);
                userManager.AddToRoles(user.Id, "Submitter");
            }
            if (!context.Users.Any(u => u.Email == "arussell@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "arussell@coderfoundry.com",
                    Email = "arussell@coderfoundry.com",
                    FirstName = "Andrew",
                    LastName = "Russell",
                    DisplayName = "Drew",
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "Developer");
            }
            if (!context.Users.Any(u => u.Email == "JasonTwichell@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "JasonTwichell@coderfoundry.com",
                    Email = "JasonTwichell@coderfoundry.com",
                    FirstName = "Jason",
                    LastName = "Twichell",
                    DisplayName = "JSON",
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "Developer");
            }
            if (!context.Users.Any(u => u.Email == "Araynor@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "Araynor@coderfoundry.com",
                    Email = "Araynor@coderfoundry.com",
                    FirstName = "Antonio",
                    LastName = "Raynor",
                    DisplayName = "FixItAgainTony",
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "Submitter");
            }
            if (!context.Users.Any(u => u.Email == "bdavis@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "bdavis@coderfoundry.com",
                    Email = "bdavis@coderfoundry.com",
                    FirstName = "Bobby",
                    LastName = "Davis",
                    DisplayName = "BossMan",
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "ProjectManager");
            }
            if (!context.Users.Any(u => u.Email == "kdoyle@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "kdoyle@coderfoundry.com",
                    Email = "kdoyle@coderfoundry.com",
                    FirstName = "Kevin",
                    LastName = "Doyle",
                    DisplayName = "SmileyMan",
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "Submitter");
            }
            if (!context.Users.Any(u => u.Email == "nsanders@coderfoundry.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "nsanders@coderfoundry.com",
                    Email = "nsanders@coderfoundry.com",
                    FirstName = "Natosha",
                    LastName = "Sanders",
                    DisplayName = "GateKeeper",
                    AvatarPath = "/Avatars/defaultAvatar.png",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Abc&123");
                userManager.AddToRoles(user.Id, "Submitter");
            }
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
            #endregion
        }
    }
}
