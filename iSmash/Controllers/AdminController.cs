using iSmash.Models;
using iSmash.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace iSmash.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RolesHelper rolesHelper = new RolesHelper();
        // GET: Admin
        //[Authorize(Roles = "Admin")]
        public ActionResult ManageRoles()
        {
            var viewData = new List<CustomUserData>();
            var users = db.Users.ToList();
            foreach (var user in users)
            {
                viewData.Add(new CustomUserData
                {
                    RoleName = rolesHelper.ListUserRoles(user.Id).FirstOrDefault() ?? "UnAssigned",
                    DisplayName = user.DisplayName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            //setup some data that can be used inside the view
            //left panel - listbox in the view
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");
            //right panel - dropdown list
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");

            return View(viewData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string roleName)
        {
            if (userIds == null || roleName == null)
            {
                return RedirectToAction("ManageRoles");
            }

            if (userIds != null)
            {
                foreach (var userId in userIds)
                {
                    var userRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();

                    if (userRole != null)
                    {
                        var currentRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
                        rolesHelper.RemoveUserFromRole(userId, currentRole);
                    }
                    rolesHelper.AddUserToRole(userId, roleName);
                }
            }
            return RedirectToAction("ManageRoles");
        }
    }





}