﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.BusinessLogicCore.Enums;
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
                    throw new Exception("Ya existe una factura con los valores especificados");
                }
            }
            else
            {
                bill = new Bill()
                {
                    BillNumber = b.BillNumber,
                    BillDate = b.BillDate,
                    Status = Estado.PENDIENTE_APROBACION,
                    Support = b.Support,
                    DT_ADDROW = DateTime.Now,
                    DT_UPDROW = DateTime.Now,
                    FL_DELETED = "N"
                };

                this._context.Bills.Add(bill);
                //this._context.SaveChanges();
                LogBill(bill, "INSERT");
            }
        }

        public void UpdateBill(Bill b)
        {
            Bill bill = CheckIfBillExists(b);

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

        public void UpdateBillStatus(string idBill, string status)
        {
            Bill bill = CheckIfBillExists(idBill);
            if (bill == null)
            {
                throw new Exception("No se encuentra la factura especificada");
            }
            else
            {
                bill.Status = status;
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
            Bill bill = this._context.Bills.Include(x => x.Support).ThenInclude(y => y.SupportRate).Include(x => x.Support).ThenInclude(y => y.Client).FirstOrDefault(x => x.BillNumber == b.BillNumber);
            return bill;
        }

        public Bill CheckIfBillExists(string idBill)
        {
            Bill bill = this._context.Bills.Include(x => x.Support).ThenInclude(y => y.SupportRate).Include(x => x.Support).ThenInclude(y => y.Client).FirstOrDefault(x => x.Id.ToString() == idBill);
            return bill;
        }

        #region LOGS

        public void LogBill(Bill b, string action)
        {
            //Bill bill = CheckIfBillExists(b);
            BillLogObject bLog = new BillLogObject()
            {
                Id = b.Id.ToString(),
                BillNumber = b.BillNumber,
                Status = b.Status,
                BillDate = b.BillDate,
                FL_DELETED = b.FL_DELETED,
                DT_ADDROW = b.DT_ADDROW,
                DT_UPDROW = b.DT_UPDROW,
                SupportLogObject = new SupportLogObject()
                {
                    Id = b.Support.Id.ToString(),
                    Description = b.Support.Description,
                    Total = b.Support.Total,
                    DT_ADDROW = b.Support.DT_ADDROW,
                    DT_UPDROW = b.Support.DT_UPDROW,
                    FL_DELETED = b.Support.FL_DELETED,


                    //Guardo tambien los datos de cliente y tarifa de soporte para registrar los datos en x fecha de estas mismas entidades al momento de agregar la factura
                    ClientLogObject = new ClientLogObject()
                    {
                        Id = b.Support.Client.Id.ToString(),
                        Description = b.Support.Client.Description,
                        Address = b.Support.Client.Address,
                        RUT = b.Support.Client.RUT,
                        FL_IVA = b.Support.Client.FL_IVA,
                        DT_ADDROW = b.Support.Client.DT_ADDROW,
                        DT_UPDROW = b.Support.Client.DT_UPDROW,
                        FL_DELETED = b.Support.Client.FL_DELETED,
                    },

                    SupportRateLogObject = new SupportRateLogObject()
                    {
                        Id = b.Support.SupportRate.Id.ToString(),
                        Description = b.Support.SupportRate.Description,
                        Currency = b.Support.SupportRate.Currency,
                        IVA = b.Support.SupportRate.IVA,
                        Amount = b.Support.SupportRate.Amount,
                        SpecialDiscount = b.Support.SupportRate.SpecialDiscount,
                        Periodicity = b.Support.SupportRate.Periodicity,
                        AdjustmentPeriodicity = b.Support.SupportRate.AdjustmentPeriodicity,
                        DT_ADDROW = b.Support.SupportRate.DT_ADDROW,
                        DT_UPDROW = b.Support.SupportRate.DT_UPDROW,
                        FL_DELETED = b.Support.SupportRate.FL_DELETED,
                    }
                }
            };

            string json = JsonConvert.SerializeObject(bLog);

            T_LOG_BILL l = new T_LOG_BILL()
            {
                ID_BILL = b.Id.ToString(),
                ID_USER = _userId,
                DT_ADDROW = DateTime.Now,
                ACTION = action,
                DATA = json,
                PAGE = "BIL020",
            };

            this._context.T_LOG_BILL.Add(l);
        }

        #endregion
    }
}
