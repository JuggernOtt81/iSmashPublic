using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;

namespace iSmash.Models
{
    public class MyProjectsList : Controller
    {
        public int ProjId { get; set; }
        public string ProjPriority { get; set; }
        public string ProjTitle { get; set; }
        public DateTime? ProjCreated { get; set; }
        public DateTime? ProjUpdated { get; set; }
        public string ProjStatus { get; set; }
        public string ProjManager { get; set; }
        //public string ProjTeam { get; set; }

        public virtual ICollection<ApplicationUser> ProjTeam { get; set; }



        public MyProjectsList()
        {
            ProjTeam = new HashSet<ApplicationUser>();
        }
    }
}