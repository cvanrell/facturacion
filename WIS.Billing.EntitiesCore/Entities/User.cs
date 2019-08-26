using System;
using System.Collections.Generic;
using System.Text;

namespace WIS.Billing.EntitiesCore.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TP_USER { get; set; }
        public bool FL_DELETED { get; set; }
        public DateTime DT_ADDROW { get; set; }
        public DateTime DT_UPDROW { get; set; }
    }
}
