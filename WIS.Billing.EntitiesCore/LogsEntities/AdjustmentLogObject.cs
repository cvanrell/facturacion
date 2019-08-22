using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class AdjustmentLogObject
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal IPCValue { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
    }
}
