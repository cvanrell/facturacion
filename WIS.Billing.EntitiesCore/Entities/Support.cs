using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WIS.Billing.EntitiesCore.Enums;

namespace WIS.Billing.EntitiesCore.Entities
{
    public class Support
    {
        public Guid Id { get; set; }
        public string Description { get; set; }                
        public decimal Total { get; set; }
        //public decimal TotalAmount { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        [MaxLength(1)]
        public string FL_DELETED { get; set; }

        public Client Client { get; set; }
        public SupportRate SupportRate { get; set; }
    }
}
