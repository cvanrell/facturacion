using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.BusinessLogicCore
{
    public static class MaintenanceActions
    {
        public static List<Maintenance> GetMaintenances(DataContext context)
        {
            List<Maintenance> result = new List<Maintenance>();
            result.AddRange(context.Maintenances.Include("Client").ToList());
            return result;
        }

        public static void AddMaintenance(DataContext context, Maintenance maintenance)
        {
            maintenance.Id = Guid.NewGuid();
            var client = context.Clients.SingleOrDefault(c => c.Id == maintenance.Client.Id);
            if (client == null)
            {
                throw new Exception("El cliente no existe");
            }
            else
            {
                maintenance.Client = client;
            }
            context.Maintenances.Add(maintenance);
            context.SaveChanges();
        }

        public static void DeleteMaintenance(DataContext context, Guid maintenanceId)
        {
            Maintenance maintenance = context.Maintenances.Find(maintenanceId);
            context.Maintenances.Remove(maintenance);
            context.SaveChanges();
        }

        public static void UpdateMaintenance(DataContext context, Maintenance maintenance)
        {
            //context.Entry(maintenance).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public static Maintenance GetMaintenance(DataContext context, Guid maintenanceId)
        {
            return context.Maintenances.Include("Client").SingleOrDefault(m => m.Id == maintenanceId);
        }
    }
}
