﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.LogsEntities
{
    public class SupportRateObjectLog
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int IVA { get; set; }
        public string Currency { get; set; }
        public string Periodicity { get; set; }
        public string AdjustmentPeriodicity { get; set; }
        public decimal SpecialDiscount { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }        
        public string FL_DELETED { get; set; }
        public ClientLogObject ClientLogObject { get; set; }
    }

}
