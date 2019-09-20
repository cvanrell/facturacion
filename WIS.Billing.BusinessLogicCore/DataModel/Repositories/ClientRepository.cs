using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.BusinessLogicCore.Enums;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.Billing.EntitiesCore.Entities;
using WIS.Billing.EntitiesCore.LogsEntities;

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
                    FL_IVA = c.FL_IVA,
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                };




                this._context.Clients.Add(client);
                this._context.SaveChanges();
                
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

        public Client CheckIfClientExists(string clientId)
        {
            Client c = this._context.Clients.FirstOrDefault(x => x.Id.ToString() == clientId);
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
                //Convierto los valores de moneda y periodicidad del row de la grilla para comparar despues en la base
                if (int.Parse(hRate.Currency) == 0)
                {
                    hRate.Currency = "Dólares";
                }
                else if (int.Parse(hRate.Currency) == 1)
                {
                    hRate.Currency = "Pesos";
                }

                switch (int.Parse(hRate.AdjustmentPeriodicity))
                {
                    case 0:
                        hRate.AdjustmentPeriodicity = "Mensual";
                        break;
                    case 1:
                        hRate.AdjustmentPeriodicity = "Trimestral";
                        break;
                    case 2:
                        hRate.AdjustmentPeriodicity = "Semestral";
                        break;
                    case 3:
                        hRate.AdjustmentPeriodicity = "Anual";
                        break;
                }

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

                        _context.SaveChanges();
                        LogHourRate(hr, client, "UPDATE");
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


                    #region IF TEMPORAL PARA PERIODICIDAD Y MONEDA 
                    //if (int.Parse(hRate.Currency) == 0)
                    //{
                    //    newHR.Currency = "Dólares";
                    //}
                    //else if(int.Parse(hRate.Currency) == 1)
                    //{
                    //    newHR.Currency = "Pesos";
                    //}

                    //switch(int.Parse(hRate.AdjustmentPeriodicity))
                    //{
                    //    case 0:
                    //        newHR.AdjustmentPeriodicity = "Mensual";
                    //        break;
                    //    case 1:
                    //        newHR.AdjustmentPeriodicity = "Trimestral";
                    //        break;
                    //    case 2:
                    //        newHR.AdjustmentPeriodicity = "Semestral";
                    //        break;
                    //    case 3:
                    //        newHR.AdjustmentPeriodicity = "Anual";
                    //        break;
                    //}

                    #endregion
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
            HourRate hr = _context.HourRates.Include(x => x.Client).FirstOrDefault(x => x.Id == hRate.Id);

            #region IF TEMPORAL PARA PERIODICIDAD Y MONEDA 
            if (hr.Currency != hRate.Currency)
            {
                if (int.Parse(hRate.Currency) == 0)
                {
                    hr.Currency = "Dólares";
                }
                else if (int.Parse(hRate.Currency) == 1)
                {
                    hr.Currency = "Pesos";
                }
            }

            if (hr.AdjustmentPeriodicity != hRate.AdjustmentPeriodicity)
            {
                switch (int.Parse(hRate.AdjustmentPeriodicity))
                {
                    case 0:
                        hr.AdjustmentPeriodicity = "Mensual";
                        break;
                    case 1:
                        hr.AdjustmentPeriodicity = "Trimestral";
                        break;
                    case 2:
                        hr.AdjustmentPeriodicity = "Semestral";
                        break;
                    case 3:
                        hr.AdjustmentPeriodicity = "Anual";
                        break;
                }
            }

            #endregion

            Client client;

            if (hr == null)
            {
                throw new Exception("No se encuentra la tarifa especificada");
            }
            else
            {
                hr.Description = hRate.Description;
                //hr.Currency = hRate.Currency;
                //hr.AdjustmentPeriodicity = hRate.AdjustmentPeriodicity;
                hr.Amount = hRate.Amount;
                hr.SpecialDiscount = hRate.SpecialDiscount;
                hr.DT_UPDROW = DateTime.Now;
                
                _context.SaveChanges();

                client = CheckIfClientExists(hr.Client);

                LogHourRate(hr, client, "UPDATE");
            }
        }

        public void DeleteHourRate(HourRate hRate)
        {
            HourRate hr = _context.HourRates.Include(x => x.Client).FirstOrDefault(x => x.Id == hRate.Id);
            Client client;
            if (hr == null)
            {
                throw new Exception("No se encuentra la tarifa que desea eliminar");
            }
            else
            {
                hr.FL_DELETED = "S";
                hr.DT_UPDROW = DateTime.Now;
                
                _context.SaveChanges();
                client = CheckIfClientExists(hr.Client);

                LogHourRate(hr, client, "DELETE");
            }
        }

        public HourRate CheckIfHourRateExists(HourRate hRate, Client client)
        {
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

                if (int.Parse(sRate.Currency) == 0)
                {
                    sRate.Currency = TipoMoneda.Dólares.ToString();
                }
                else if (int.Parse(sRate.Currency) == 1)
                {
                    sRate.Currency = TipoMoneda.Pesos.ToString();
                }

                switch (int.Parse(sRate.AdjustmentPeriodicity))
                {
                    case 0:
                        sRate.AdjustmentPeriodicity = "Mensual";
                        break;
                    case 1:
                        sRate.AdjustmentPeriodicity = "Trimestral";
                        break;
                    case 2:
                        sRate.AdjustmentPeriodicity = "Semestral";
                        break;
                    case 3:
                        sRate.AdjustmentPeriodicity = "Anual";
                        break;
                }

                switch (int.Parse(sRate.Periodicity))
                {
                    case 0:
                        sRate.Periodicity = "Mensual";
                        break;
                    case 1:
                        sRate.Periodicity = "Trimestral";
                        break;
                    case 2:
                        sRate.Periodicity = "Semestral";
                        break;
                    case 3:
                        sRate.Periodicity = "Anual";
                        break;
                }
                //Verificar que no existe una tarifa de hora con los mismos datos
                SupportRate sr = CheckIfSupportRateExists(sRate, client);

                //No existe tarifa, puede agregarla
                if (sr != null)
                {
                    //Si registro estaba eliminado, lo vuelvo a activar
                    if (sr.FL_DELETED == "S")
                    {
                        sr.FL_DELETED = "N";
                        sr.DT_UPDROW = DateTime.Now;

                        _context.SaveChanges();
                        LogSupportRate(sr, client, "UPDATE");
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
                        FL_DELETED = "N",
                        DT_ADDROW = DateTime.Now,
                        DT_UPDROW = DateTime.Now
                    };

                    if (newSR.Client.FL_IVA == "S")
                    {
                        newSR.IVA = 22;
                    }
                    else if (newSR.Client.FL_IVA == "N")
                    {
                        newSR.IVA = 0;
                    }


                    this._context.SupportRates.Add(newSR);                    
                    this._context.SaveChanges();

                    LogSupportRate(newSR, client, "INSERT");                                    
                }
            }
            else
            {
                throw new Exception("No se encontro el cliente especificado");
            }
        }

        public void UpdateSupportRate(SupportRate sRate, string rutCliente)
        {
            SupportRate sr = _context.SupportRates.Include(x => x.Client).FirstOrDefault(x => x.Id == sRate.Id);
            Client client;
            if (sr == null)
            {
                throw new Exception("No se encuentra la tarifa especificada");
            }
            else
            {
                sr.Description = sRate.Description;
                //sr.Currency = sRate.Currency;
                //sr.Periodicity = sRate.Periodicity;
                //sr.AdjustmentPeriodicity = sRate.AdjustmentPeriodicity;
                sr.Amount = sRate.Amount;
                sr.IVA = sRate.IVA;
                sr.SpecialDiscount = sRate.SpecialDiscount;
                sr.DT_UPDROW = DateTime.Now;



                #region IF TEMPORAL PARA PERIODICIDAD Y MONEDA 
                if (sr.Currency != sRate.Currency)
                {
                    if (int.Parse(sRate.Currency) == 0)
                    {
                        sr.Currency = "Dólares";
                    }
                    else if (int.Parse(sRate.Currency) == 1)
                    {
                        sr.Currency = "Pesos";
                    }
                }

                if (sr.AdjustmentPeriodicity != sRate.AdjustmentPeriodicity)
                {
                    switch (int.Parse(sRate.AdjustmentPeriodicity))
                    {
                        case 0:
                            sr.AdjustmentPeriodicity = "Mensual";
                            break;
                        case 1:
                            sr.AdjustmentPeriodicity = "Trimestral";
                            break;
                        case 2:
                            sr.AdjustmentPeriodicity = "Semestral";
                            break;
                        case 3:
                            sr.AdjustmentPeriodicity = "Anual";
                            break;
                    }
                }

                if (sr.Periodicity != sRate.Periodicity)
                {
                    switch (int.Parse(sRate.Periodicity))
                    {
                        case 0:
                            sr.Periodicity = "Mensual";
                            break;
                        case 1:
                            sr.Periodicity = "Trimestral";
                            break;
                        case 2:
                            sr.Periodicity = "Semestral";
                            break;
                        case 3:
                            sr.Periodicity = "Anual";
                            break;
                    }
                }
                #endregion
                _context.SaveChanges();

                client = CheckIfClientExists(sr.Client);

                LogSupportRate(sr, client, "UPDATE");                
            }
        }

        public void DeleteSupportRate(SupportRate sRate)
        {
            SupportRate sr = _context.SupportRates.Include(x => x.Client).FirstOrDefault(x => x.Id == sRate.Id);
            Client client;
            if (sr == null)
            {
                throw new Exception("No se encuentra la tarifa que desea eliminar");
            }
            else
            {
                sr.FL_DELETED = "S";
                sr.DT_UPDROW = DateTime.Now;
                
                _context.SaveChanges();
                client = CheckIfClientExists(sr.Client);

                LogSupportRate(sr, client, "DELETE");                
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
                ID_USER = this._userId,
                ACTION = action,
                DT_ADDROW = DateTime.Now,
                DATA = json,
                PAGE = "CLI010",
                ID_CLIENT = client.Id.ToString()
            };

            this._context.T_LOG_CLIENT.Add(l);
        }

        public void LogHourRate(HourRate hr, Client c, string action)
        {
            HourRate hRate = CheckIfHourRateExists(hr, c);

            HourRateLogObject hrLog = new HourRateLogObject()
            {
                Id = hRate.Id.ToString(),
                Description = hRate.Description,
                Currency = hRate.Currency,
                AdjustmentPeriodicity = hRate.AdjustmentPeriodicity,
                Amount = hRate.Amount,
                SpecialDiscount = hRate.SpecialDiscount,
                FL_DELETED = hRate.FL_DELETED,
                DT_ADDROW = hRate.DT_ADDROW,
                DT_UPDROW = hRate.DT_UPDROW,
                ClientLogObject = new ClientLogObject
                {
                    Id = hRate.Client.Id.ToString(),
                    Description = hRate.Client.Description,
                    RUT = hRate.Client.RUT,
                    Address = hRate.Client.Address,
                    FL_DELETED = hRate.Client.FL_DELETED,
                    FL_IVA = hRate.Client.FL_IVA,
                    DT_ADDROW = hRate.Client.DT_ADDROW,
                    DT_UPDROW = hRate.Client.DT_UPDROW,                    
                }
            };
            string json = JsonConvert.SerializeObject(hrLog);
            {
                T_LOG_HOUR_RATE l = new T_LOG_HOUR_RATE()
                {
                    ID_USER = this._userId,
                    ACTION = action,
                    DT_ADDROW = DateTime.Now,
                    DATA = json,
                    PAGE = "CLI020",
                    ID_HOUR_RATE = hr.Id.ToString()
                };

                this._context.T_LOG_HOUR_RATE.Add(l);
                //HourRateLogObject jsonDeserialized = JsonConvert.DeserializeObject<HourRateLogObject>(l.DATA);
            }
        }

        public static void LogHourRate(HourRate hr, string action, string page, int userId, WISDB context, T_LOG_RATE_ADJUSTMENTS logAdj = null)
        {
            //HourRate hRate = CheckIfHourRateExists(hr, c);

            //Creo un objeto LogObject debido a que si parseo un objeto HourRate directo; 
            //al tener un cliente, y este mismo tener una coleccion de HourRate y SupportRate tira error al hacer la conversion
            HourRateLogObject hrLog = new HourRateLogObject()
            {
                Id = hr.Id.ToString(),
                Description = hr.Description,
                Currency = hr.Currency,
                AdjustmentPeriodicity = hr.AdjustmentPeriodicity,
                Amount = hr.Amount,
                SpecialDiscount = hr.SpecialDiscount,
                FL_DELETED = hr.FL_DELETED,
                DT_ADDROW = hr.DT_ADDROW,
                DT_UPDROW = hr.DT_UPDROW,
                ClientLogObject = new ClientLogObject
                {
                    Id = hr.Client.Id.ToString(),
                    Description = hr.Client.Description,
                    RUT = hr.Client.RUT,
                    Address = hr.Client.Address,
                    FL_DELETED = hr.Client.FL_DELETED,
                    FL_IVA = hr.Client.FL_IVA,
                    DT_ADDROW = hr.Client.DT_ADDROW,
                    DT_UPDROW = hr.Client.DT_UPDROW,
                },
                logAdjustment = logAdj

            };
            string json = JsonConvert.SerializeObject(hrLog);
            {
                T_LOG_HOUR_RATE l = new T_LOG_HOUR_RATE()
                {
                    ID_USER = userId,
                    ACTION = action,
                    DT_ADDROW = DateTime.Now,
                    DATA = json,
                    PAGE = page,
                    ID_HOUR_RATE = hr.Id.ToString(),
                    LogAdjustment = logAdj
                };

                context.T_LOG_HOUR_RATE.Add(l);                
            }
        }

        public void LogSupportRate(SupportRate sr, Client c, string action)
        {
            SupportRate sRate = CheckIfSupportRateExists(sr, c);

            //Creo un objeto LogObject debido a que si parseo un objeto HourRate directo; 
            //al tener un cliente, y este mismo tener una coleccion de HourRate y SupportRate tira error al hacer la conversion
            SupportRateLogObject srLog = new SupportRateLogObject()
            {
                Id = sRate.Id.ToString(),
                Description = sRate.Description,
                Currency = sRate.Currency,
                IVA = sRate.IVA,
                AdjustmentPeriodicity = sRate.AdjustmentPeriodicity,
                Periodicity = sRate.Periodicity,
                Amount = sRate.Amount,
                SpecialDiscount = sRate.SpecialDiscount,
                FL_DELETED = sRate.FL_DELETED,
                DT_ADDROW = sRate.DT_ADDROW,
                DT_UPDROW = sRate.DT_UPDROW,
                ClientLogObject = new ClientLogObject
                {
                    Id = sRate.Client.Id.ToString(),
                    Description = sRate.Client.Description,
                    RUT = sRate.Client.RUT,
                    Address = sRate.Client.Address,
                    FL_DELETED = sRate.Client.FL_DELETED,
                    FL_IVA = sRate.Client.FL_IVA,
                    DT_ADDROW = sRate.Client.DT_ADDROW,
                    DT_UPDROW = sRate.Client.DT_UPDROW,                    
                }                
            };
            string json = JsonConvert.SerializeObject(srLog);
            {
                T_LOG_SUPPORT_RATE l = new T_LOG_SUPPORT_RATE()
                {
                    ID_USER = this._userId,
                    ACTION = action,
                    DT_ADDROW = DateTime.Now,
                    DATA = json,
                    PAGE = "CLI020",
                    ID_SUPPORT_RATE = sr.Id.ToString()
                };
                this._context.T_LOG_SUPPORT_RATE.Add(l);
            }
        }

        public static void LogSupportRate(SupportRate sr, string action, string page, int userId, WISDB context, T_LOG_RATE_ADJUSTMENTS logAdj = null)
        {
            //Creo un objeto LogObject debido a que si parseo un objeto HourRate directo; 
            //al tener un cliente, y este mismo tener una coleccion de HourRate y SupportRate tira error al hacer la conversion
            SupportRateLogObject srLog = new SupportRateLogObject()
            {
                Id = sr.Id.ToString(),
                Description = sr.Description,
                Currency = sr.Currency,
                IVA = sr.IVA,
                AdjustmentPeriodicity = sr.AdjustmentPeriodicity,
                Periodicity = sr.Periodicity,
                Amount = sr.Amount,
                SpecialDiscount = sr.SpecialDiscount,
                FL_DELETED = sr.FL_DELETED,
                DT_ADDROW = sr.DT_ADDROW,
                DT_UPDROW = sr.DT_UPDROW,
                ClientLogObject = new ClientLogObject
                {
                    Id = sr.Client.Id.ToString(),
                    Description = sr.Client.Description,
                    RUT = sr.Client.RUT,
                    Address = sr.Client.Address,
                    FL_DELETED = sr.Client.FL_DELETED,
                    FL_IVA = sr.Client.FL_IVA,
                    DT_ADDROW = sr.Client.DT_ADDROW,
                    DT_UPDROW = sr.Client.DT_UPDROW,
                },
                logAdjustment = logAdj
            };
            string json = JsonConvert.SerializeObject(srLog);
            {
                T_LOG_SUPPORT_RATE l = new T_LOG_SUPPORT_RATE()
                {
                    ID_USER = userId,
                    ACTION = action,
                    DT_ADDROW = DateTime.Now,
                    DATA = json,
                    PAGE = page,
                    ID_SUPPORT_RATE = sr.Id.ToString(),
                    LogAdjustment = logAdj
                };
                context.T_LOG_SUPPORT_RATE.Add(l);
            }
        }

        #endregion

        public SupportRate CheckIfSupportRateExists(SupportRate sRate, Client client)
        {            
            SupportRate sr = _context.SupportRates.FirstOrDefault(x => x.Client.Id == client.Id &&
                                x.Currency == sRate.Currency && x.AdjustmentPeriodicity == sRate.AdjustmentPeriodicity &&
                                x.Amount == sRate.Amount && x.SpecialDiscount == sRate.SpecialDiscount &&
                                x.IVA == sRate.IVA && x.Periodicity == sRate.Periodicity);
            return sr;
        }

        public SupportRate CheckIfSupportRateExists(string sRateId)
        {
            SupportRate sr = this._context.SupportRates.FirstOrDefault(x => x.Id.ToString() == sRateId);

            return sr;
        }
    }
}
