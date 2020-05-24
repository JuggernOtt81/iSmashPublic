using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSmash.Models;
using Microsoft.AspNet.Identity;

namespace iSmash.Helpers
{
    public class NotificationHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {
            ManageAssignmentNotifications(oldTicket, newTicket);

            GenerateTicketChangeNotification(oldTicket, newTicket);

        }

        private void ManageAssignmentNotifications(Ticket oldTicket, Ticket newTicket)
        {

        }


        private void GenerateTicketAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            bool assigned = oldTicket.DeveloperId == null && newTicket.DeveloperId != null;
            //bool unassigned = oldTicket.DeveloperId != null && newTicket.DeveloperId == null;
            //bool reassigned = (!assigned) && (!unassigned) && (oldTicket.DeveloperId != newTicket.DeveloperId);

            if (assigned)
            {
                var created = DateTime.Now;

                db.TicketNotifications.Add(new TicketNotification
                {
                    Created = created,
                    TicketId = newTicket.Id,
                    SenderId = HttpContext.Current.User.Identity.GetUserId(),
                    RecipientId = newTicket.DeveloperId,
                    Subject = "You have been assigned to a ticket.",
                    Body = $"You have been assigned to Ticket Id: {newTicket.Id} on {created.ToString("MM dd, yyyy")}. This ticket is attached to Project: {newTicket.Project.Name}."
                });
            }

            db.SaveChanges();
        }

        private void GenerateTicketChangeNotification(Ticket oldTicket, Ticket newTicket)
        {

        }





    }
}