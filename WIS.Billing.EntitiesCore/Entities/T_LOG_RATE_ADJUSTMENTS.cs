using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIS.Billing.EntitiesCore.Entities
{
    public class T_LOG_RATE_ADJUSTMENTS
    {
        [Key]
        public long NU_LOG { get; set; }        
        public int ID_USER { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public string ACTION { get; set; }
        public string PAGE { get; set; }

        public T_LOG_IPC LogIPC { get; set; }
    }
}
