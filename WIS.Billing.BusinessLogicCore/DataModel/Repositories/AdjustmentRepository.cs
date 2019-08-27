using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.DataAccessCore.Database;
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
