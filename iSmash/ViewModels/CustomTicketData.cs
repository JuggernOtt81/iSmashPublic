using iSmash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSmash.ViewModels
{
    public class CustomTicketData
    {
        public string Priority { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Project { get; set; }
        public string Developer { get; set; }
        public List<string> ProjectNames { get; set; }
        public List<Ticket> Tickets { get; set; }
        public CustomTicketData()
        {
            ProjectNames = new List<string>();
            Tickets = new List<Ticket>();
        }
    }
}