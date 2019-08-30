using Microsoft.EntityFrameworkCore;
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
    public class SupportRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly SupportMapper _mapper;

        public SupportRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new SupportMapper();
        }

        #region SOPORTES

        public void AddSupport(Support s)
        {            
            Support support = CheckIfSupportExists(s);
            if (support != null)
            {
                if (support.FL_DELETED == "S")
                {
                    support.FL_DELETED = "N";
                    support.DT_UPDROW = DateTime.Now;

                    this._context.SaveChanges();

                    LogSupport(support, "UPDATE");
                }
                else
                {
                    throw new Exception("Ya existe un mantenimiento con la descripción especificada");
                }
            }
            else
            {
                support = new Support()
                {
                    Description = s.Description,                   
                    Client = s.Client,
                    Total = s.Total,                    
                    FL_DELETED = "N",
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                };                

                this._context.Supports.Add(support);
                this._context.SaveChanges();
                LogSupport(support, "INSERT");
            }
        }

        //public void UpdateSupport(Support s)
        //{
        //    Support support = _context.Supports.Include(x => x.Client).FirstOrDefault(x => x.Id == s.Id);
        //    if (support == null)
        //    {
        //        throw new Exception("No se encuentra el mantenimiento especificado");
        //    }
        //    else
        //    {
        //        support.Description = s.Description;
        //        //project.Currency = p.Currency;
        //        support.Amount = p.Amount;
        //        support.IVA = p.IVA;
        //        support.Total = p.Total;
        //        support.InitialDate = p.InitialDate;
        //        support.TotalAmount = p.TotalAmount;
        //        support.DT_UPDROW = DateTime.Now;

        //        if (project.Currency != p.Currency)
        //        {
        //            if (int.Parse(p.Currency) == 0)
        //            {
        //                project.Currency = "Dólares";
        //            }
        //            else if (int.Parse(p.Currency) == 1)
        //            {
        //                project.Currency = "Pesos";
        //            }
        //        }

        //        _context.SaveChanges();

        //        LogProject(project, "UPDATE");
        //    }
        //}

        public void DeleteProject(Support s)
        {
            Support support = _context.Supports.Include(x => x.Client).FirstOrDefault(x => x.Id == s.Id);
            if (support == null)
            {
                throw new Exception("No se encuentra el mantenimiento que desea eliminar");
            }
            else
            {
                support.FL_DELETED = "S";
                support.DT_UPDROW = DateTime.Now;
                _context.SaveChanges();

                LogSupport(support, "DELETE");
            }
        }

        #endregion

        public Support CheckIfSupportExists(Support s)
        {
            Support sup = this._context.Supports.Include(x => x.Client).FirstOrDefault(x => x.Description == s.Description && x.Client == s.Client);
            return sup;
        }

        #region LOGS

        public void LogSupport(Support s, string action)
        {
            Support support = CheckIfSupportExists(s);
            //Client c;
            SupportLogObject sLog = new SupportLogObject()
            {
                Id = support.Id.ToString(),
                Description = support.Description,
                Total = support.Total,                                
                FL_DELETED = support.FL_DELETED,
                DT_ADDROW = support.DT_ADDROW,
                DT_UPDROW = support.DT_UPDROW,
                ClientLogObject = new ClientLogObject()
                {
                    Id = support.Client.Id.ToString(),
                    Description = support.Client.Description,
                    RUT = support.Client.RUT,
                    Address = support.Client.Address,
                    FL_DELETED = support.Client.FL_DELETED,
                    FL_IVA = support.Client.FL_IVA,
                    DT_ADDROW = support.Client.DT_ADDROW,
                    DT_UPDROW = support.Client.DT_UPDROW,
                }
            };

            string json = JsonConvert.SerializeObject(sLog);

            T_LOG_SUPPORT l = new T_LOG_SUPPORT()
            {
                ID_SUPPORT = s.Id.ToString(),
                ID_USER = _userId,
                DT_ADDROW = DateTime.Now,
                ACTION = action,
                DATA = json,
                PAGE = "MAN010"
            };

            this._context.T_LOG_SUPPORT.Add(l);
        }

        #endregion
    }
}
