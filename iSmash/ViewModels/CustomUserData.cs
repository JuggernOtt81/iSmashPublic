using iSmash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSmash.ViewModels
{
    public class CustomUserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
        public List<string> ProjectNames { get; set; }
        public List<Ticket> Tickets { get; set; }
        public CustomUserData()
        {
            ProjectNames = new List<string>();
            Tickets = new List<Ticket>();
        }
    }
}