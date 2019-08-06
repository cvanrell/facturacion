using System;
using System.Collections.Generic;
using System.Text;
using WIS.Billing.EntitiesCore.Enums;

namespace WIS.Billing.EntitiesCore
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Description { get; set; }                
        public string Currency { get; set; }        
        public decimal Amount { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InitialDate { get; set; }

        public Client Client { get; set; }
        public List<Fee> Fees { get; set; }
    }
}
