using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSmash.Models;
using Microsoft.AspNet.Identity;

namespace iSmash.Helpers
{
    public class MessageHelper
    {
        public static List<TicketNotification> GetUnreadNotifications()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId == null)
            {
                return new List<TicketNotification>();
            }
            else
            {
                var db = new ApplicationDbContext();
                return db.TicketNotifications.Where(t => t.RecipientId == userId && !t.IsRead).ToList();
            }

        }
    }
}