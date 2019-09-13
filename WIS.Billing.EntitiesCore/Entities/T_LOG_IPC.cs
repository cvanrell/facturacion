using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.Entities
{
    public class T_LOG_IPC : T_LOG
    {        
        public string ID_ADJUSTMENT { get; set; }
        public DateTime DATEIPC { get; set; }
    }
}
