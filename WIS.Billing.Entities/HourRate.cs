using System;
using System.Collections.Generic;
using System.Text;
using WIS.Billing.Entities.Enums;

namespace WIS.Billing.Entities
{
    public class HourRate
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyDescription {
            get
            {
                return Currency.GetDescription();
            }
        }
        public decimal Rate { get; set; }
        public Periodicity AdjustmentPeriodicity { get; set; }
        public string AdjustmentPeriodicityDescription {
            get
            {
                return AdjustmentPeriodicity.GetDescription();
            }
        }
        public int StartingMonth { get; set; }
    }
}
