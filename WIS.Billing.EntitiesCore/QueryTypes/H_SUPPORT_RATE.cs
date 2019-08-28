using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.QueryTypes
{
    public class H_SUPPORT_RATE
    {
        public int NU_LOG { get; set; }
        public string ID_SUPPORT_RATE { get; set; }
        public string Client { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string SpecialDiscount { get; set; }
        public string AdjustmentPeriodicity { get; set; }
        public string Action { get; set; }
        public string FL_DELETED { get; set; }
        public int ID_USER { get; set; }
        //public string DT_UPDROW { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_ONLY { get; set; }
    }
}
