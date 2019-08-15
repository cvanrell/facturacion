﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.Billing.EntitiesCore.Entities;

namespace WIS.Billing.BusinessLogicCore.DataModel.Repositories
{
    public class ClientRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly ClientMapper _mapper;

        public ClientRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new ClientMapper();
        }

        public string GetDescription(string desc)
        {
            return this._context.Clients.Where(d => d.Description == desc).Select(d => d.Description).FirstOrDefault();
        }

        #region CLIENT

        public void AddClient(Client c)
        {
            Client client = CheckIfClientExists(c);
            if (client != null)
            {
                if (client.FL_DELETED == "S")
                {
                    client.FL_DELETED = "N";
                    client.DT_UPDROW = DateTime.Now;
                    //en log poner que la accion fue un insert pero en json de data, poner client.FL_DELETED = "N";
                    this._context.SaveChanges();

                    LogClient(client, "UPDATE");

                }
                else
                {
                    throw new Exception("Ya existe un cliente con el RUT especificado");
                }
            }
            else
            {
                client = new Client()
                {
                    Address = c.Address,
                    Description = c.Description,
                    RUT = c.RUT,
                    FL_DELETED = "N",
                    FL_FOREIGN = c.FL_FOREIGN,
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                };




                this._context.Clients.Add(client);
                this._context.SaveChanges();

                //Hacer ingreso de log aca
                LogClient(client, "INSERT");
            }
        }

        public void UpdateClient(Client c)
        {
            Client client = CheckIfClientExists(c);
            if (client == null)
            {
                throw new Exception("No se encuentra el cliente especificado");
            }
            else
            {
                client.Address = c.Address;
                client.Description = c.Description;
                client.DT_UPDROW = DateTime.Now;

                _context.SaveChanges();
                LogClient(client, "UPDATE");
            }
        }

        public void DeleteClient(Client c)
        {
            Client client = CheckIfClientExists(c);
            if (client == null)
            {
                throw new Exception("No se encuentra el cliente que desea eliminar");
            }
            else
            {
                client.FL_DELETED = "S";
                client.DT_UPDROW = DateTime.Now;

                _context.SaveChanges();

                LogClient(client, "DELETE");
            }
        }

        public Client CheckIfClientExists(Client client)
        {
            Client c = this._context.Clients.FirstOrDefault(x => x.RUT == client.RUT);
            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region DET_CLIENTS

        #region TARIFAS DE HORAS
        public void AddHourRate(HourRate hRate, string rutCliente)
        {
            Client client = _context.Clients.FirstOrDefault(x => x.RUT == rutCliente);

            if (client != null)
            {
                //Verificar que no existe una tarifa de hora con los mismos datos
                HourRate hr = CheckIfHourRateExists(hRate, client);

                //La tarifa existe
                if (hr != null)
                {
                    //Si registro estaba eliminado, lo vuelvo a activar
                    if (hr.FL_DELETED == "S")
                    {
                        hr.FL_DELETED = "N";
                        hr.DT_UPDROW = DateTime.Now;

                        //insertar log de detalle 
                    }
                    else
                    {
                        throw new Exception("Ya existe una tarifa con los datos ingresados");
                    }
                }
                else
                {
                    HourRate newHR = new HourRate()
                    {
                        Description = hRate.Description,
                        Client = client,
                        Currency = hRate.Currency,
                        AdjustmentPeriodicity = hRate.AdjustmentPeriodicity,
                        Amount = hRate.Amount,
                        SpecialDiscount = hRate.SpecialDiscount,
                        FL_DELETED = "N",
                        DT_ADDROW = DateTime.Now,
                        DT_UPDROW = DateTime.Now
                    };
                    this._context.HourRates.Add(newHR);
                    this._context.SaveChanges();

                    LogHourRate(newHR, client, "INSERT");
                }
            }
            else
            {
                throw new Exception("No se encontro el cliente especificado");
            }
        }

        public void UpdateHourRate(HourRate hRate, string rutCliente)
        {
            HourRate hr = _context.HourRates.FirstOrDefault(x => x.Id == hRate.Id);
            if (hr == null)
            {
                throw new Exception("No se encuentra la tarifa especificada");
            }
            else
            {
                hr.Description = hRate.Description;
                hr.Currency = hRate.Currency;
                hr.AdjustmentPeriodicity = hRate.AdjustmentPeriodicity;
                hr.Amount = hRate.Amount;
                hr.SpecialDiscount = hRate.SpecialDiscount;
                //hr.DT_UPDROW = Datetime.Now;

                //insertar en log 
                //context.SaveChanges();
            }
        }

        public void DeleteHourRate(HourRate hRate)
        {
            HourRate hr = _context.HourRates.FirstOrDefault(x => x.Id == hRate.Id);
            if (hr == null)
            {
                throw new Exception("No se encuentra la tarifa que desea eliminar");
            }
            else
            {


                hr.FL_DELETED = "S";
                //hr.DT_UPDROW = Datetime.Now;

                //Insertar en log 
                //context.SaveChanges();
            }
        }

        public HourRate CheckIfHourRateExists(HourRate hRate, Client client)
        {
            Client c = CheckIfClientExists(client);
            HourRate hr = _context.HourRates.FirstOrDefault(x => x.Client.Id == client.Id &&
                                x.Currency == hRate.Currency && x.AdjustmentPeriodicity == hRate.AdjustmentPeriodicity &&
                                x.Amount == hRate.Amount && x.SpecialDiscount == hRate.SpecialDiscount);
            return hr;
        }
        #endregion


        #region TARIFAS DE SOPORTE
        public void AddSupportRate(SupportRate sRate, string rutCliente)
        {
            Client client = _context.Clients.FirstOrDefault(x => x.RUT == rutCliente);
            if (client != null)
            {
                //Verificar que no existe una tarifa de hora con los mismos datos
                SupportRate sr = _context.SupportRates.FirstOrDefault(x => x.Client.Id == client.Id &&
                                x.Currency == sRate.Currency && x.AdjustmentPeriodicity == sRate.AdjustmentPeriodicity &&
                                x.Amount == sRate.Amount && x.SpecialDiscount == sRate.SpecialDiscount &&
                                x.IVA == sRate.IVA && x.Periodicity == sRate.Periodicity);

                //No existe tarifa, puede agregarla
                if (sr != null)
                {
                    //Si registro estaba eliminado, lo vuelvo a activar
                    if (sr.FL_DELETED == "S")
                    {
                        sr.FL_DELETED = "N";
                    }
                    else
                    {
                        throw new Exception("Ya existe una tarifa de soporte con los datos ingresados");
                    }
                }
                else
                {
                    SupportRate newSR = new SupportRate()
                    {
                        Description = sRate.Description,
                        Client = client,
                        Currency = sRate.Currency,
                        Periodicity = sRate.Periodicity,
                        AdjustmentPeriodicity = sRate.AdjustmentPeriodicity,
                        Amount = sRate.Amount,
                        SpecialDiscount = sRate.SpecialDiscount,
                        FL_DELETED = "N"
                    };
                    this._context.SupportRates.Add(newSR);
                    //context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("No se encontro el cliente especificado");
            }
        }

        public void UpdateSupportRate(SupportRate sRate, string rutCliente)
        {
            SupportRate sr = _context.SupportRates.FirstOrDefault(x => x.Id == sRate.Id);
            if (sr == null)
            {
                throw new Exception("No se encuentra la tarifa especificada");
            }
            else
            {
                sr.Description = sRate.Description;
                sr.Currency = sRate.Currency;
                sr.Periodicity = sr.Periodicity;
                sr.AdjustmentPeriodicity = sRate.AdjustmentPeriodicity;
                sr.Amount = sRate.Amount;
                sr.IVA = sRate.IVA;
                sr.SpecialDiscount = sRate.SpecialDiscount;
                //context.SaveChanges();
            }
        }

        public void DeleteSupportRate(SupportRate sRate)
        {
            SupportRate sr = _context.SupportRates.FirstOrDefault(x => x.Id == sRate.Id);
            if (sr == null)
            {
                throw new Exception("No se encuentra la tarifa que desea eliminar");
            }
            else
            {
                sr.FL_DELETED = "S";
                //context.SaveChanges();
            }
        }

        #endregion
        #endregion



        #region LOGS


        public void LogClient(Client c, string action)
        {
            Client client = CheckIfClientExists(c);
            string json = JsonConvert.SerializeObject(c);



            T_LOG_CLIENT l = new T_LOG_CLIENT()
            {
                USER = this._userId,
                ACTION = action,
                DT_ADDROW = DateTime.Now,
                DATA = json,
                PAGE = "CLI010",
                ID_CLIENT = client.Id.ToString()
            };

            this._context.T_LOG_CLIENT.Add(l);
        }

        public void LogHourRate(HourRate hr,Client c, string action)
        {
            HourRate hRate = CheckIfHourRateExists(hr, c);

            //CREAR UNA CLASE CON LOS CAMPOS QUE NECESITO DE HourRate Y CAMBIARLA POR LA CLASE ANONIMA QUE PASO POR PARAMETRO
            string json = JsonConvert.SerializeObject(new {
                Id = hRate.Id.ToString(),
                Description = hRate.Description,
                Currency = hRate.Currency,
                AdjustmentPeriodicity = hRate.AdjustmentPeriodicity,
                Amount = hRate.Amount,
                SpecialDiscount = hRate.SpecialDiscount,
                FL_DELETED = hRate.FL_DELETED,
                DT_ADDROW = hRate.DT_ADDROW,
                DT_UPDROW = hRate.DT_UPDROW,
                Client = new
                {
                    Id = hRate.Client.Id.ToString()
                }
            });



            T_LOG_HOUR_RATE l = new T_LOG_HOUR_RATE()
            {
                USER = this._userId,
                ACTION = action,
                DT_ADDROW = DateTime.Now,
                DATA = json,
                PAGE = "CLI020",
                ID_HOUR_RATE = hr.Id.ToString()
            };

            this._context.T_LOG_HOUR_RATE.Add(l);
        }

        #endregion
    }
}
