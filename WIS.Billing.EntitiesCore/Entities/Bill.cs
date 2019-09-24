using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WIS.Billing.EntitiesCore.Entities
{
    public class Bill
    {
        public Guid Id { get; set; }
        public long BillNumber { get; set; }
        public string Status { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
        [MaxLength(1)]
        public string FL_DELETED { get; set; }


        public Support Support { get; set; }
    }
}
