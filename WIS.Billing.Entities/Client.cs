using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string RUT { get; set; }
        public string Address { get; set; }
        public List<HourRate> HourRates { get; set; }
    }
}
