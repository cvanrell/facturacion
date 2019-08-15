using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class ProjectLogObject
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public decimal TotalAmount { get; set; }
        public string InitialDate { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        public string FL_DELETED { get; set; }
        public ClientLogObject ClientLogObject { get; set; }
    }
}
