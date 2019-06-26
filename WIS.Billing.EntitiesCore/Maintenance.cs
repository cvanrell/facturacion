using System;
using WIS.Billing.EntitiesCore.Enums;

namespace WIS.Billing.EntitiesCore
{
    public class Maintenance
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        public decimal Amount { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyDescription {
            get
            {
                return Currency.GetDescription();
            }
        }
        public Periodicity Periodicity { get; set; }
        public string PeriodicityDescription
        {
            get
            {
                return Periodicity.GetDescription();
            }
        }
    }
}
