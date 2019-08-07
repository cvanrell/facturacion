using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIS.Billing.EntitiesCore
{
    public class Client
    {
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string RUT { get; set; }
        public string Address { get; set; }
        [MaxLength(1)]
        public string FL_DELETED { get; set; }
        [MaxLength(1)]
        public string FL_FOREIGN { get; set; }


        public List<HourRate> HourRates { get; set; }        
        public List<Project> Projects { get; set; }
    }
}
