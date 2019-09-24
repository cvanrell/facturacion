using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class BillLogObject
    {
        public string Id { get; set; }
        public long BillNumber { get; set; }
        public string Status { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }        
        public string FL_DELETED { get; set; }

        public SupportLogObject SupportLogObject { get; set; }
    }
}
