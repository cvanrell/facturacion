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
    public static class ClientActions
    {
        public static List<Client> GetClients(WISDB context)
        {
            List<Client> result = new List<Client>();
            result.AddRange(context.Clients.Include(c => c.HourRates).ToList());
            return result;
        }

        public static void AddClient(WISDB context, Client client)
        {
            using (context)
            {
                Client c = context.Clients.FirstOrDefault(x => x.Description == client.Description);
                if(c == null) 
                {
                    if(!string.IsNullOrEmpty(client.Description))
                    {
                        context.Clients.Add(client);
                        context.SaveChanges();
                    }
                    else
                    {
                        //Mensaje de error "La descripcion del cliente no debe ser nula"
                        throw new Exception("La descripcion del cliente no debe ser nula");
                    }
                }
                else
                {
                    //Mensaje de error "Ya existe el cliente ingresado"
                    throw new Exception("Ya existe el cliente ingresado");
                }
            }
            
        }

        public static void DeleteClient(WISDB context, Guid clientId)
        {
            Client client = context.Clients.Find(clientId);
            if(client != null)
            {
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Hubo un problema al intentar borrar el cliente");
                
            }
            
        }

        public static void UpdateClient(WISDB context, Client client)
        {
            using(context)
            {
                Client c = context.Clients.Find(client.Id);
                if(c != null)
                {                    
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Hubo un problema al intentar actualizar el cliente");
                }
            }
            
        }

        public static Client GetClient(WISDB context, Guid clientId)
        {
            return context.Clients.Include(c => c.HourRates).SingleOrDefault(c => c.Id == clientId);
        }
    }
}
