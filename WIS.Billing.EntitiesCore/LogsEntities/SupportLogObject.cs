using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class SupportLogObject
    {
        public string Id { get; set; }
        public string Description { get; set; }        
        public decimal Total { get; set; }
        public decimal TotalAmount { get; set; }        
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        public string FL_DELETED { get; set; }
        public ClientLogObject ClientLogObject { get; set; }
    }
}
