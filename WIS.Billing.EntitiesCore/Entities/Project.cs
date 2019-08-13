using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string InitialDate { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        [MaxLength(1)]        
        public string FL_DELETED { get; set; }

        public Client Client { get; set; }
        public List<Fee> Fees { get; set; }
    }
}
