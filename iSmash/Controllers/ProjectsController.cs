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
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationUser user = new ApplicationUser();
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projHelper = new ProjectsHelper();
        private RolesHelper rolesHelper = new RolesHelper();

        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult ManageProjectAssignments()
        {
            var emptyCustomUserData = new List<CustomUserData>();
            var users = db.Users.ToList();

            //load a multi select of users
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "FullName");

            //load a multi select of projects
            ViewBag.ProjectIds = new MultiSelectList(db.Projects, "Id", "Name");

            foreach (var user in users)
            {
                emptyCustomUserData.Add(new CustomUserData
                {
                    ProjectNames = projHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList(),
                    RoleName = rolesHelper.ListUserRoles(user.Id).FirstOrDefault(),
                    DisplayName = user.DisplayName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            //load the viewmodel
            return View(emptyCustomUserData);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectAssignments(List<string> userIds, List<int> projectIds)
        {
            //if and only if at least one person is chosen, do the following:
            if (userIds == null || projectIds == null)
            {
                return RedirectToAction("ManageProjectAssignments");
            }
            //add each SELECTED USER to each SELECTED PROJECT
            foreach (var userId in userIds)
            {
                foreach (var projectId in projectIds)
                {
                    projHelper.AddUserToProject(userId, projectId);
                }
            }
            return RedirectToAction("ManageProjectAssignments");
        }

        // GET: Projects
        public ActionResult Index()
        {

            //return View(db.Projects.ToList());
            return View(db.Projects.OrderBy(p => p.Id).ToList());
        }

        // GET: MyProjectsList
        public ActionResult MyProjectsList()
        {
            var myUserId = User.Identity.GetUserId();
            var myProjects = projHelper.ListUserProjects(myUserId);
            return View("myProjectsList");
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Description,ProjectManagerId,Created,Updated,IsArchived")] Project project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Projects.Add(project);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(project);
        //}

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,ProjectManagerId,Created,Updated,IsArchived")] Project
project, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //project.Name = Name;
                //project.Description = Description;
                project.ProjectManagerId = "unassigned";
                project.Created = DateTime.Now;
                project.PriorityLabel = "unassigned";
                project.StatusLabel = "unassigned";

                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "ProjectManager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Priority,PriorityLabel,Status,StatusLabel,Created,Name,Description,ProjectManagerId,IsArchived")] Project project)
        {
            if (ModelState.IsValid)
            {

                project.Updated = DateTime.Now;
                db.Entry(project).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
