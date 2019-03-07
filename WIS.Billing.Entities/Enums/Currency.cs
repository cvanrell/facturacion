using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Billing.Entities.Enums
{
    public enum Currency
    {
        [Description("Dólares")]
        Dollar,
        [Description("Pesos")]
        Peso
    }

    public class CurrencyInfo
    {
        public Currency Currency { get; set; }
        public string CurrencyDescription
        {
            get
            {
                return Currency.GetDescription();
            }
        }
    }
}
