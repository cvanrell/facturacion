using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Billing.EntitiesCore
{
    public class Fee
    {
        public Guid Id { get; set; }        
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string MonthYear { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }

        [MaxLength(1)]
        public string FL_DELETED { get; set; }



        public Project Project { get; set; }
    }
}
