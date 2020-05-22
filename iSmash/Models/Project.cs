using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSmash.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Range(0, 10)]
        public int Priority { get; set; }
        public string PriorityLabel { get; set; }
        [Range(0, 10)]
        public int Status { get; set; }
        public string StatusLabel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectManagerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsArchived { get; set; }


        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public Project()
        {
            Users = new HashSet<ApplicationUser>();
            Tickets = new HashSet<Ticket>();
        }
    }
}