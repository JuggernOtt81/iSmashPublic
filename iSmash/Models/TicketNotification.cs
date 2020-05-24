using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;

namespace iSmash.Models
{
    public class TicketNotification
    {
        public DateTime Created { get; set; }
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public bool IsRead { get; set; }
        public DateTime Sent { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
    }
}