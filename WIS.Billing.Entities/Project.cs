using System;
using System.Collections.Generic;
using System.Text;
using WIS.Billing.Entities.Enums;

namespace WIS.Billing.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Client Client { get; set; }
        public int Installments { get; set; }
        public Currency Currency { get; set; }
        public string CurrencyDescription {
            get
            {
                return Currency.GetDescription();
            }
        }
        public decimal Amount { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        public List<Fee> Fees { get; set; }
    }
}
