using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;

namespace WIS.Billing.BusinessLogicCore
{
    public class HourRateActions
    {
        public static List<HourRate> GetHourRates(WISDB context)
        {
            List<HourRate> result = new List<HourRate>();
            result = context.HourRates.ToList();
            return result;
        }

        public static void AddHourRate(WISDB context, HourRate hourRate)
        {
            using (context)
            {
                HourRate hRate = context.HourRates.FirstOrDefault(x => x.Description == hourRate.Description && x.Client == hourRate.Client);
                if (hRate == null)
                {
                    if (!string.IsNullOrEmpty(hourRate.Description))
                    {
                        context.HourRates.Add(hourRate);
                        context.SaveChanges();
                    }
                    else
                    {                       
                        throw new Exception("La descripcion de la tarifa no debe ser nula");
                    }
                }
                else
                {                   
                    throw new Exception("Ya existe una tarifa con la descripcion ingresada");
                }
            }

        }

        public static void DeleteHourRate(WISDB context, Guid hourRatetId)
        {
            HourRate hourRate = context.HourRates.Find(hourRatetId);
            if (hourRate != null)
            {
                context.HourRates.Remove(hourRate);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Hubo un problema al intentar borrar la tarifa");

            }

        }

        public static void UpdateHourRate(WISDB context, HourRate hourRate)
        {
            using (context)
            {
                HourRate hr = context.HourRates.Find(hourRate.Id);
                if (hr != null)
                {
                    if (!string.IsNullOrEmpty(hourRate.Description))
                    {                     
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("La descripción de la tarifa no puede estar vacía");
                    }
                    
                }
                else
                {
                    throw new Exception("Hubo un problema al intentar actualizar la tarifa");
                }
            }

        }

        public static HourRate GetHourRate(WISDB context, Guid hourRateId)
        {
            return context.HourRates.SingleOrDefault(h => h.Id == hourRateId);
        }
    }
}
