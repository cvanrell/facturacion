using System;
using System.Collections.Generic;
using System.Text;
using WIS.Billing.EntitiesCore.Enums;

namespace WIS.Billing.EntitiesCore
{
    public class HourRate
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }        
        public decimal SpecialDiscount { get; set; }
               
        public string AdjustmentPeriodicity { get; set; }
        public string Currency { get; set; }

        public Client Client { get; set; }
        

        //public string CurrencyDescription {
        //    get
        //    {
        //        return Currency.GetDescription();
        //    }
        //}
                
        //public string AdjustmentPeriodicityDescription {
        //    get
        //    {
        //        return AdjustmentPeriodicity.GetDescription();
        //    }
        //}
        
    }
}
