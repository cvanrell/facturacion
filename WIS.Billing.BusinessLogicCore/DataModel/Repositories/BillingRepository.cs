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
    public class BillingRepository
    {
        private readonly WISDB _context;
        private readonly string _application;
        private readonly int _userId;
        private readonly BillMapper _mapper;

        public BillingRepository(WISDB context, string application, int userId)
        {
            this._context = context;
            this._application = application;
            this._userId = userId;
            this._mapper = new BillMapper();
        }

        #region FACTURACION

        public void AddBill(Bill b)
        {
            Bill bill = CheckIfBillExists(b);
            if (bill != null)
            {
                if (bill.FL_DELETED == "S")
                {
                    bill.FL_DELETED = "N";
                    bill.DT_UPDROW = DateTime.Now;

                    this._context.SaveChanges();

                    LogBill(bill, "UPDATE");
                }
                else
                {
                    throw new Exception("Ya existe un ajuste con los valores especificados");
                }
            }
            else
            {
                bill = new Bill()
                {
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                };

                this._context.Bills.Add(bill);
                this._context.SaveChanges();
                LogBill(bill, "INSERT");
            }
        }

        public void UpdateBill(Bill b)
        {


            Bill bill = _context.Bills.FirstOrDefault(x => x.Id == b.Id);




            if (bill == null)
            {
                throw new Exception("No se encuentra la factura especificada");
            }
            else
            {
                bill.Status = b.Status;
                bill.DT_UPDROW = DateTime.Now;

                _context.SaveChanges();

                LogBill(bill, "UPDATE");
            }
        }

        public void DeleteBill(Bill b)
        {
            Bill bill = CheckIfBillExists(b);
            if (bill == null)
            {
                throw new Exception("No se encuentra la factura que desea eliminar");
            }
            else
            {
                bill.FL_DELETED = "S";
                bill.DT_UPDROW = DateTime.Now;
                _context.SaveChanges();

                LogBill(bill, "DELETE");
            }
        }






        #endregion

        public Bill CheckIfBillExists(Bill b)
        {
            Bill bill = this._context.Bills.FirstOrDefault(x => x.BillNumber == b.BillNumber);
            return bill;
        }

        #region LOGS

        public void LogBill(Bill b, string action)
        {
            Bill bill = CheckIfBillExists(b);
            BillLogObject bLog = new BillLogObject()
            {
                Id = b.Id.ToString(),
                BillNumber = b.BillNumber,
                Status = b.Status,
                FL_DELETED = b.FL_DELETED,
                DT_ADDROW = b.DT_ADDROW,
                DT_UPDROW = b.DT_UPDROW,
            };

            string json = JsonConvert.SerializeObject(bLog);

            T_LOG_BILL l = new T_LOG_BILL()
            {
                ID_BILL = b.Id.ToString(),
                ID_USER = _userId,
                DT_ADDROW = DateTime.Now,
                ACTION = action,
                DATA = json,
                PAGE = "BIL010",                
            };

            this._context.T_LOG_BILL.Add(l);
        }

        #endregion
    }
}
