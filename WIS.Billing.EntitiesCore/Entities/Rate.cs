using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIS.Billing.EntitiesCore.Entities
{
   public class Rate
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal SpecialDiscount { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        public string AdjustmentPeriodicity { get; set; }
        public string Currency { get; set; }
        [MaxLength(1)]
        public string FL_DELETED { get; set; }

        public Client Client { get; set; }
    }
}
