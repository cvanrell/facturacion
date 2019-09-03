using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.Billing.EntitiesCore.Entities;
using WIS.Billing.EntitiesCore.LogsEntities;

namespace WIS.Billing.BusinessLogicCore.DataModel.Repositories
{
    public class AdjustmentRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly AdjustmentMapper _mapper;

        public AdjustmentRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new AdjustmentMapper();
        }

        #region AJUSTES

        public void AddAdjustment(Adjustment a)
        {
            Adjustment adjustment = CheckIfAdjustmentExists(a);
            if (adjustment != null)
            {
                //if (adjustment.FL_DELETED == "S")
                //{
                //    adjustment.FL_DELETED = "N";
                //    adjustment.DT_UPDROW = DateTime.Now;

                //    this._context.SaveChanges();

                //    LogAdjustment(adjustment, "UPDATE");
                //}
                //else
                //{
                //    throw new Exception("Ya existe un ajuste con los valores especificados");
                //}
            }
            else
            {
                adjustment = new Adjustment()
                {
                    Year = a.Year,
                    Month = a.Month,
                    IPCValue = a.IPCValue,
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                };

                this._context.Adjustments.Add(adjustment);
                this._context.SaveChanges();
                LogAdjustment(adjustment, "INSERT");
            }
        }

        public void UpdateAdjustment(Adjustment a)
        {
            Adjustment adjustment = _context.Adjustments.FirstOrDefault(x => x.Id == a.Id);
            if (adjustment == null)
            {
                throw new Exception("No se encuentra el ajuste especificado");
            }
            else
            {
                adjustment.IPCValue = a.IPCValue;
                adjustment.DT_UPDROW = DateTime.Now;

                _context.SaveChanges();

                LogAdjustment(adjustment, "UPDATE");
            }
        }

        //public void DeleteAdjustment(Adjustment a)
        //{
        //    Adjustment adjustment = _context.Adjustments.FirstOrDefault(x => x.Id == a.Id);
        //    if (adjustment == null)
        //    {
        //        throw new Exception("No se encuentra el ajuste que desea eliminar");
        //    }
        //    else
        //    {
        //        adjustment.FL_DELETED = "S";
        //        adjustment.DT_UPDROW = DateTime.Now;
        //        _context.SaveChanges();

        //        LogAdjustment(adjustment, "DELETE");
        //    }
        //}


        public void ExecuteAdjustments()
        {
            DateTime actualDate = DateTime.Now;

            List<HourRate> hourRates = GetHourRates(actualDate);

            List<SupportRate> supportRates = GetSupportRates(actualDate);

            string periodicity;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var hr in hourRates)
                    {
                        //periodicity = hr.AdjustmentPeriodicity;

                        //switch (hr.AdjustmentPeriodicity)
                        //{
                        //case "Mensual":
                        ExecuteAjuste(hr);
                        //break;
                        //case "Trimestral":
                        //    ExecuteTrimestral();
                        //    break;
                        //case "Semestral":
                        //    ExecuteSemestral();
                        //    break;
                        //case "Anual":
                        //    ExecuteAnual();
                        //    break;
                        //}

                        //LOGUEAR CAMBIO
                        ClientRepository.LogHourRate(hr, "ADJUSTMENT", "ADJ010", this._userId, this._context);

                    }

                    foreach (var sr in supportRates)
                    {
                        ExecuteAjuste(sr);
                        ClientRepository.LogSupportRate(sr, "ADJUSTMENT", "ADJ010", this._userId, this._context);
                    }

                    //LOG de la ejecucion


                    transaction.Commit();
                }
                catch
                {
                    transaction.Dispose();
                }
            }

            
        }

        public List<HourRate> GetHourRates(DateTime actualDate)
        {
            List<HourRate> hourRates = new List<HourRate>();
            //Pregunto si el mes es Dicimebre, por lo que se van a ajustar todas las tarifas, ya que incluye Mensual, Trimestral, Semestral y Anual
            if (actualDate.Month.Equals(12))
            {
                hourRates = _context.HourRates.Include(x => x.Client).Where(x => x.Currency == "Pesos").ToList();
            }


            //Si es Junio, se ajustan las tarifas Mensual, Trimestral y Semestral, por lo tanto son todas menos las Anuales
            else if (actualDate.Month.Equals(6))
            {
                hourRates = _context.HourRates.Include(x => x.Client).Where(x => x.AdjustmentPeriodicity != "Anual" && x.Currency == "Pesos").ToList();
            }


            //Si es Marzo o Septiembre (Junio y Diciembre ya estan contemplados en los if anteriores
            else if (actualDate.Month.Equals(3) || actualDate.Month.Equals(9))
            {
                hourRates = _context.HourRates.Include(x => x.Client).Where(x => x.AdjustmentPeriodicity == "Mensual" && x.AdjustmentPeriodicity == "Trimestral" && x.Currency == "Pesos").ToList();
            }

            //Si es cualquier otro mes distinto a los casos anteriores solo va a ser Mensual
            else
            {
                hourRates = _context.HourRates.Include(x => x.Client).Where(x => x.AdjustmentPeriodicity != "Mensual" && x.Currency == "Pesos").ToList();
            }
            return hourRates;
        }

        public List<SupportRate> GetSupportRates(DateTime actualDate)
        {
            List<SupportRate> supportRates = new List<SupportRate>();
            //Pregunto si el mes es Dicimebre, por lo que se van a ajustar todas las tarifas, ya que incluye Mensual, Trimestral, Semestral y Anual
            if (actualDate.Month.Equals(12))
            {
                supportRates = _context.SupportRates.Include(x => x.Client).Where(x => x.Currency == "Pesos").ToList();
            }


            //Si es Junio, se ajustan las tarifas Mensual, Trimestral y Semestral, por lo tanto son todas menos las Anuales
            else if (actualDate.Month.Equals(6))
            {
                supportRates = _context.SupportRates.Include(x => x.Client).Where(x => x.AdjustmentPeriodicity != "Anual" && x.Currency == "Pesos").ToList();
            }


            //Si es Marzo o Septiembre (Junio y Diciembre ya estan contemplados en los if anteriores
            else if (actualDate.Month.Equals(3) || actualDate.Month.Equals(9))
            {
                supportRates = _context.SupportRates.Include(x => x.Client).Where(x => x.AdjustmentPeriodicity == "Mensual" && x.AdjustmentPeriodicity == "Trimestral" && x.Currency == "Pesos").ToList();
            }

            //Si es cualquier otro mes distinto a los casos anteriores solo va a ser Mensual
            else
            {
                supportRates = _context.SupportRates.Include(x => x.Client).Where(x => x.AdjustmentPeriodicity != "Mensual" && x.Currency == "Pesos").ToList();
            }
            return supportRates;
        }


        public void ExecuteAjuste(Rate rate)
        {
            decimal oldIPC = GetPeriodicityIPC(rate.AdjustmentPeriodicity);
            decimal lastIPC = GetLastIPC();

            decimal newAmount = rate.Amount * oldIPC / lastIPC;

            rate.Amount = newAmount;
        }

        public decimal GetPeriodicityIPC(string periodicity)
        {
            DateTime IPCDate = DateTime.Now;

            switch (periodicity)
            {
                case "Mensual":
                    IPCDate = DateTime.Now.AddMonths(-1);
                    break;
                case "Trimestral":
                    IPCDate = DateTime.Now.AddMonths(-3);
                    break;
                case "Semestral":
                    IPCDate = DateTime.Now.AddMonths(-6);
                    break;
                case "Anual":
                    IPCDate = DateTime.Now.AddYears(-1);
                    break;
            }
            return _context.Adjustments.FirstOrDefault(x => x.DateIPC == IPCDate).IPCValue;
        }

        public decimal GetLastIPC()
        {
            DateTime actualDate = DateTime.Now.AddMonths(-1);
            return _context.Adjustments.FirstOrDefault(x => x.DateIPC == actualDate).IPCValue;
        }

        #endregion

        public Adjustment CheckIfAdjustmentExists(Adjustment a)
        {
            Adjustment ad = this._context.Adjustments.FirstOrDefault(x => x.Year == a.Year && x.Month == a.Month);
            return ad;
        }

        #region LOGS

        public void LogAdjustment(Adjustment a, string action)
        {
            Adjustment adjustment = CheckIfAdjustmentExists(a);
            AdjustmentLogObject aLog = new AdjustmentLogObject()
            {
                Year = a.Year,
                Month = a.Month,
                IPCValue = a.IPCValue,
                DT_ADDROW = a.DT_ADDROW,
                DT_UPDROW = a.DT_UPDROW,
            };

            string json = JsonConvert.SerializeObject(aLog);

            T_LOG_ADJUSTMENT l = new T_LOG_ADJUSTMENT()
            {
                ID_ADJUSTMENT = a.Id.ToString(),
                ID_USER = _userId,
                DT_ADDROW = DateTime.Now,
                ACTION = action,
                DATA = json,
                PAGE = "ADJ010"
            };

            this._context.T_LOG_ADJUSTMENT.Add(l);
        }

        #endregion
    }
}
