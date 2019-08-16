using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class ClientLogObject
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string RUT { get; set; }
        public string Address { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        public string FL_DELETED { get; set; }
        public string FL_FOREIGN { get; set; }        
    }
}
