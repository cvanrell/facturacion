using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Billing.EntitiesCore
{
    public class Fee
    {
        public Guid Id { get; set; }        
        public string Description { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string MonthYear { get; set; }

        

        public Project Project { get; set; }
    }
}
