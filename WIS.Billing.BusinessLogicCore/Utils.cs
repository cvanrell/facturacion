using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.BusinessLogicCore
{
    class Utils
    {
        public static Client CheckIfClientExists(WISDB context, Client client)
        {
            Client c = context.Clients.FirstOrDefault(x => x.RUT == client.RUT);
            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }

        }

        
    }
}
