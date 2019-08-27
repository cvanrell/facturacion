using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class FeeLogObject
    {
        public string Id { get; set; }
        public string Description { get; set; }        
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string MonthYear { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        public string FL_DELETED { get; set; }
        public ProjectLogObject ProjectLogObject { get; set; }
    }
}
