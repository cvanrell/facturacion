using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccess;
using WIS.Billing.Entities;
using System.Data.Entity;

namespace WIS.Billing.BusinessLogic
{
    public static class ClientActions
    {
        public static List<Client> GetClients(DataContext context)
        {
            List<Client> result = new List<Client>();
            result.AddRange(context.Clients.Include(c => c.HourRates).ToList());
            return result;
        }

        public static void AddClient(DataContext context, Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
        }

        public static void DeleteClient(DataContext context, Guid clientId)
        {
            Client client = context.Clients.Find(clientId);
            context.Clients.Remove(client);
            context.SaveChanges();
        }

        public static void UpdateClient(DataContext context, Client client)
        {
            context.Entry(client).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public static Client GetClient(DataContext context, Guid clientId)
        {
            return context.Clients.Include(c => c.HourRates).SingleOrDefault(c => c.Id == clientId);
        }
    }
}
