﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WIS.Billing.EntitiesCore
{
    public class T_LOG_CLIENT
    {
        [Key]
        public int NU_LOG { get; set; }
        public string ID_CLIENT { get; set; }
        public int ID_USER { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public string ACTION { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        public string DATA { get; set; }
        public string PAGE { get; set; }
    }
}
