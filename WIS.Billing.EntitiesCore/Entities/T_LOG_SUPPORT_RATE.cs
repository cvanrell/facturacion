using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIS.Billing.EntitiesCore.Entities
{
    public class T_LOG_SUPPORT_RATE
    {
        [Key]
        public int NU_LOG { get; set; }
        public string ID_SUPPORT_RATE { get; set; }
        public int USER { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public string ACTION { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        public string DATA { get; set; }
        public string PAGE { get; set; }
    }
}
