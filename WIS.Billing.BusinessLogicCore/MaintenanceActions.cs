using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.BusinessLogicCore
{
    public static class MaintenanceActions
    {
        public static List<Maintenance> GetMaintenances(WISDB context)
        {
            List<Maintenance> result = new List<Maintenance>();
            result.AddRange(context.Maintenances.Include("Client").ToList());
            return result;
        }

        public static void AddMaintenance(WISDB context, Maintenance maintenance)
        {
            maintenance.Id = Guid.NewGuid();
            var client = context.Clients.SingleOrDefault(c => c.Id == maintenance.Client.Id);
            if (client == null)
            {
                throw new Exception("El cliente al que le intenta asociar el soporte no existe");
            }
            else
            {
                maintenance.Client = client;
            }
            context.Maintenances.Add(maintenance);
            context.SaveChanges();
        }

        public static void DeleteMaintenance(WISDB context, Guid maintenanceId)
        {
            Maintenance maintenance = context.Maintenances.Find(maintenanceId);
            context.Maintenances.Remove(maintenance);
            context.SaveChanges();
        }

        public static void UpdateMaintenance(WISDB context, Maintenance maintenance)
        {
            using(context)
            {
                Maintenance m = context.Maintenances.FirstOrDefault(x => x.Id == maintenance.Id);
                if(m == null)
                {
                    throw new Exception("No se encuentra el registro de soporte que se intenta modificar, Id: " + maintenance.Id);
                }
                else{
                    context.SaveChanges();                   
                }
            }
            context.SaveChanges();
        }

        public static Maintenance GetMaintenance(WISDB context, Guid maintenanceId)
        {
            return context.Maintenances.Include("Client").SingleOrDefault(m => m.Id == maintenanceId);
        }
    }
}
