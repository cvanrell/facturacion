﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WIS.Billing.Entities;
using WIS.Billing.DataAccess;

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
            using (context)
            {
                Client c = context.Clients.FirstOrDefault(x => x.Description == client.Description);
                if(c == null) 
                {
                    if(string.IsNullOrEmpty(client.Description))
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

        public static void DeleteClient(DataContext context, Guid clientId)
        {
            Client client = context.Clients.Find(clientId);
            if(client != null)
            {
                throw new Exception("Ya existe el cliente ingresado");
            }
            else
            {
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            
        }

        public static void UpdateClient(DataContext context, Client client)
        {
            using(context)
            {
                Client c = context.Clients.Find(client.Id);
                if(c != null)
                {
                    context.Entry(client).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontro el cliente seleccionado");
                }
            }
            
        }

        public static Client GetClient(DataContext context, Guid clientId)
        {
            return context.Clients.Include(c => c.HourRates).SingleOrDefault(c => c.Id == clientId);
        }
    }
}
