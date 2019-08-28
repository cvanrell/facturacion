using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.QueryTypes
{
    public class V_HOUR_RATES
    {
        public string Client { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal SpecialDiscount { get; set; }
        public string AdjustmentPeriodicity { get; set; }
        public string Currency { get; set; }
        public DateTime DT_ADDROW { get; set; }
    }
}
