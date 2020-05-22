using iSmash.Models;

namespace iSmash.Helpers
{
    public class UserHelper
    {

        //get project manager full name
        public static string ProjectManagerFullName(string id)
        {
            var db = new ApplicationDbContext();
            var pmName = db.Users.Find(id).FullName;
            return pmName;
        }
    }
}