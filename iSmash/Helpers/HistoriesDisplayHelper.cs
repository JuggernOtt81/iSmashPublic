using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSmash.Models;

namespace iSmash.Helpers
{
    public class HistoriesDisplayHelper
    {
        public static string DisplayData(TicketHistory ticketHistory)
        {
            var db = new ApplicationDbContext();

            var data = "";

            switch (ticketHistory.PropertyName)
            {
                case "DeveloperId":
                    data = "Yellow Rubber Duck, help me PLEASE!";
                        //db.Users.FirstOrDefault(u => u.Id == ticketHistory.NewValue).FullName;
                    break;
                default: 
                    data = ticketHistory.NewValue;
                    break;
            }

            return data;
        }
    }
}