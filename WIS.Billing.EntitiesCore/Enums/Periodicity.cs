using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Billing.EntitiesCore.Enums
{
    public enum Periodicity
    {
        [Description("Anual")]
        Annual,
        [Description("Semestral")]
        Biannual,
        [Description("Trimestral")]
        Quarterly,
        [Description("Mensual")]
        Monthly
    }

    public class PeriodicityInfo
    {
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
