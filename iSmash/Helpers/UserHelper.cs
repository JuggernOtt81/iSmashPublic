using System.Web;
using iSmash.Models;
using Microsoft.AspNet.Identity;

namespace iSmash.Helpers
{
    public class UserHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string UserId { get; set; }

        public UserHelper()
        {
            UserId = HttpContext.Current.User.Identity.GetUserId();
        }

        public string GetUserDisplayName()
        {
            return db.Users.Find(UserId).DisplayName;
        }

        public string GetUserAvatar()
        {
            return db.Users.Find(UserId).AvatarPath;
        }
        
        public static string ProjectManagerFullName(string id)
        {
            var db = new ApplicationDbContext();
            var pmName = db.Users.Find(id).FullName;
            return pmName;
        }

    }
}