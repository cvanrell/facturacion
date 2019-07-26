using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore
{
    public class SupportRate
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int IVA { get; set; }
        public string Currency { get; set; }
        public string Periodicity { get; set; }
        public string AdjustmentPeriodicity { get; set; }
        public decimal SpecialDiscount { get; set; }        
                
        public Client Client { get; set; }
    }
}
