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
    public class FeeRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly FeeMapper _mapper;

        public FeeRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new FeeMapper();
        }

        public string GetDescription(string desc)
        {
            return this._context.Fees.Where(d => d.Description == desc).Select(d => d.Description).FirstOrDefault();
        }

        #region FEE

        public void AddFee(Fee f, string idProject)
        {
            Project project = _context.Projects.FirstOrDefault(x => x.Id.ToString() == idProject);

            if (project != null)
            {
                //Verificar que no existe una cuota con los mismos datos
                Fee fee = CheckIfFeeExists(f);
                if (fee != null)
                {
                    if (fee.FL_DELETED == "S")
                    {
                        fee.FL_DELETED = "N";
                        fee.DT_UPDROW = DateTime.Now;                        
                        this._context.SaveChanges();

                        LogFee(fee, "UPDATE");

                    }
                    else
                    {
                        throw new Exception("Ya existe una cuota con los datos especificado");
                    }
                }
                else
                {
                    fee = new Fee()
                    {
                        Description = f.Description,
                        Month = f.Month,
                        MonthYear = f.MonthYear,
                        Amount = f.Amount,
                        Discount = f.Discount,
                        Project = project,
                        FL_DELETED = "N",
                        DT_ADDROW = DateTime.Now,
                        DT_UPDROW = DateTime.Now,
                    };




                    this._context.Fees.Add(fee);
                    this._context.SaveChanges();

                    //Hacer ingreso de log aca
                    LogFee(fee, "INSERT");
                }
            }
            else
            {
                throw new Exception("No se encontro el cliente especificado");
            }
        }

        public void UpdateFee(Fee f)
        {
            Fee fee = CheckIfFeeExists(f);
            if (fee == null)
            {
                throw new Exception("No se encuentra el cliente especificado");
            }
            else
            {
                fee.Description = f.Description;
                fee.Month = f.Month;
                fee.MonthYear = f.MonthYear;
                fee.Amount = f.Amount;
                fee.Discount = f.Discount;
                fee.DT_UPDROW = DateTime.Now;

                _context.SaveChanges();
                LogFee(fee, "UPDATE");
            }
        }

        public void DeleteFee(Fee f)
        {
            Fee fee = CheckIfFeeExists(f);
            if (fee == null)
            {
                throw new Exception("No se encuentra el cliente que desea eliminar");
            }
            else
            {
                fee.FL_DELETED = "S";
                fee.DT_UPDROW = DateTime.Now;

                _context.SaveChanges();

                LogFee(fee, "DELETE");
            }
        }

        public Fee CheckIfFeeExists(Fee fee)
        {
            return this._context.Fees.Include(x => x.Project).FirstOrDefault(x => x.Description == fee.Description && x.MonthYear == fee.MonthYear /*&& x.Month == fee.Month && x.Project.Id.ToString() == fee.Project.Id.ToString()*/);
            //if (f != null)
            //{
            //    return f;
            //}
            //return null;


        }



        #endregion


        #region LOGS

        public void LogFee(Fee f, string action)
        {
            Fee fee = CheckIfFeeExists(f);
            FeeLogObject fLog = new FeeLogObject()
            {
                Id = fee.Id.ToString(),
                Description = fee.Description,                
                Amount = fee.Amount,
                Discount = fee.Discount,
                FL_DELETED = fee.FL_DELETED,
                DT_ADDROW = fee.DT_ADDROW,
                DT_UPDROW = fee.DT_UPDROW,
                ProjectLogObject = new ProjectLogObject
                {
                    Id = fee.Project.Id.ToString(),
                    Description = fee.Project.Description,
                    Currency = fee.Project.Currency,
                    Amount = fee.Project.Amount,
                    IVA = fee.Project.IVA,
                    Total = fee.Project.Total,
                    InitialDate = fee.Project.InitialDate,
                    TotalAmount = fee.Project.TotalAmount,
                    FL_DELETED = fee.Project.FL_DELETED,                    
                    DT_ADDROW = fee.Project.DT_ADDROW,
                    DT_UPDROW = fee.Project.DT_UPDROW,
                }
            };
            string json = JsonConvert.SerializeObject(fLog);



            T_LOG_FEE l = new T_LOG_FEE()
            {
                USER = this._userId,
                ACTION = action,
                DT_ADDROW = DateTime.Now,
                DATA = json,
                PAGE = "FEE010",
                ID_FEE = fee.Id.ToString()
            };

            this._context.T_LOG_FEE.Add(l);
        }


        #endregion
    }
}
