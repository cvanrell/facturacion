using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore
{
    public class Development
    {
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public string TicketId { get; set; }
        public decimal TotalHours { get; set; }
        public HourRate Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
