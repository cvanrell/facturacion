using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel;
using WIS.Billing.BusinessLogicCore.Enums;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore.Entities;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.FormUtil.Validation;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.App;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogicCore.Controllers.Billing
{
    public class BillingController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "BIL010";
        private List<string> GridKeys { get; }

        public BillingController(ISessionAccessor session, IDbConnection connection)
        {
            this._session = session;
            this._connection = connection;

            this.GridKeys = new List<string>
            {
                "Id"
            };
        }

        public override PageQueryData PageLoad(PageQueryData data, int userId)
        {
            return data;
        }

        //FORMULARIO
        public override Form FormInitialize(Form form, FormQuery query, int userId)
        {

            string supportName = _session.GetValue<string>("SupportName");

            form.GetField("SupportName").Value = supportName;

            return form;
        }
        public override Form FormSubmit(Form form, FormSubmitQuery query, int userId)
        {
            try
            {
                Bill bill = new Bill()
                {
                    BillNumber = long.Parse(form.GetField("BillNumber").Value),
                    BillDate = DateTime.Parse(form.GetField("BillDate").Value),
                };



                using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
                {
                    Support b = context.SupportRepository.CheckIfSupportExists(_session.GetValue<string>("SupportId"));
                    bill.Support = b;
                    context.BillingRepository.AddBill(bill);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            query.ResetForm = true;

            return form;
        }
        public override Form FormButtonAction(Form form, FormButtonActionQuery query, int userId)
        {
            form.GetField("address").Value = "Button action performed";
            form.GetField("type").Value = "3";

            return form;
        }


        protected override FormValidationSchema GetValidationSchema(Form form, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            var schema = new FormValidationSchema
            {
                //["Description"] = this.ValidateDescription,
                //["Client"] = this.ValidateClient,
                //["Amount"] = this.ValidateAmount,
                //["Currency"] = this.ValidateCurrency
            };
            return schema;
        }

        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            switch (grid.Id)
            {
                case "BIL010_grid_1":
                    grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                        //new GridItemHeader("Cosas 1"),
                        new GridButton("btnFacturas", "Facturas de mantenimiento", "fas fa-wrench"),
                        //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                        //new GridItemDivider(),
                        //new GridItemHeader("Cosas 2"),
                        //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
                    }));
                    break;
                case "BIL020_grid_1":
                    //grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                    //    //new GridItemHeader("Cosas 1"),
                    //    new GridButton("btnAprobar", "Aprobar factura", "fas fa-wrench"),
                    //    new GridButton("btnRechazar", "Aprobar factura", "fas fa-wrench"),
                    //    //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                    //    //new GridItemDivider(),
                    //    //new GridItemHeader("Cosas 2"),
                    //    //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
                    //}));
                    break;
            }


            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            using (WISDB context = new WISDB())
            {
                switch (grid.Id)
                {
                    case "BIL010_grid_1":
                        return GridSupportsFetchRows(service, grid, gridQuery, userId, context);
                    case "BIL020_grid_1":
                        return GridBillsFetchRows(service, grid, gridQuery, userId, context);
                }

            }
            return grid;
        }

        public Grid GridSupportsFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId, WISDB context)
        {
            //var query = context.Supports.Include("SupportRate");

            var query = GetSupportsForBilling(DateTime.Now, context).AsQueryable();

            var defaultSort = new SortCommand("Total", SortDirection.Descending);

            grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);

            foreach (var r in grid.Rows)
            {
                Support s = context.Supports.Include(x => x.Client).FirstOrDefault(x => x.Id.ToString() == r.Id.ToString());
                foreach (var c in r.Cells)
                {
                    if (c.Column.Id == "Client")
                    {
                        c.Value = s.Client.Description;
                    }
                }
            }
            return grid;
        }



        private Grid GridBillsFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId, WISDB context)
        {
            string supportId = _session.GetValue<string>("SupportId");

            var query = context.Bills.Where(x => x.Support.Id.ToString() == supportId && x.FL_DELETED == "N");

            var defaultSort = new SortCommand("BillDate", SortDirection.Ascending);

            grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);

            foreach (var row in grid.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.Column.Id == "BillDate")
                    {
                        cell.Value = DateTime.Parse(cell.Value).Date.ToString("d");
                    }
                    if (cell.Column.Id == "Status")
                    {
                        switch (cell.Value)
                        {
                            case Estado.PENDIENTE_APROBACION:
                                grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                                    new GridButton("btnAprobar", "Aprobar factura", "fas fa-wrench"),
                                    new GridButton("btnRechazar", "Rechazar factura", "fas fa-wrench"),
                                    }));
                                break;

                            case Estado.PENDIENTE_FACTURACION:
                                grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                                    new GridButton("btnFacturar", "Facturar", "fas fa-wrench"),
                                    new GridButton("btnCancelar", "Cancelar factura", "fas fa-wrench"),
                                }));
                                break;

                            case Estado.FACTURADA:
                                grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                                    new GridButton("btnPagar", "Pagar factura", "fas fa-wrench"),
                                    new GridButton("btnCancelar", "Cancelar factura", "fas fa-wrench"),
                                }));
                                break;
                        }
                    }
                }
            }

            return grid;
        }

        public override GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            string idBill = data.Row.GetCell("Id").Value;
            switch (data.ButtonId)
            {
                case "btnFacturas":
                    this._session.SetValue("SupportName", data.Row.GetCell("Description").Value);
                    this._session.SetValue("SupportId", data.Row.GetCell("Id").Value);

                    data.Redirect = "/Billing/BIL020";
                    break;

                case "btnAprobar":
                    UpdateBillStatus(idBill, Estado.PENDIENTE_FACTURACION, userId);
                    break;

                case "btnRechazar":
                    UpdateBillStatus(idBill, Estado.RECHAZADA, userId);
                    break;

                case "btnFacturar":
                    UpdateBillStatus(idBill, Estado.FACTURADA, userId);
                    break;

                case "btnPagar":
                    UpdateBillStatus(idBill, Estado.PAGADA, userId);
                    break;

                case "btnCancelar":
                    UpdateBillStatus(idBill, Estado.CANCELADA, userId);
                    break;

                
            }
            
            return data;
        }


        public void UpdateBillStatus(string idBill, string status, int userId)
        {
            using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            {
                context.BillingRepository.UpdateBillStatus(idBill, Estado.PENDIENTE_FACTURACION);
                context.SaveChanges();
            }
        }

        public List<Support> GetSupportsForBilling(DateTime actualDate, WISDB context)
        {
            List<Support> supports;
            if (actualDate.Month.Equals(12))
            {
                supports = context.Supports.Include(x => x.SupportRate).ToList();
            }


            else if (actualDate.Month.Equals(6))
            {
                supports = context.Supports.Include(x => x.SupportRate).Where(x => x.SupportRate.Periodicity != "Anual").ToList();
            }


            else if (actualDate.Month.Equals(3) || actualDate.Month.Equals(9))
            {
                supports = context.Supports.Include(x => x.SupportRate).Where(x => x.SupportRate.Periodicity == "Mensual" || x.SupportRate.Periodicity == "Trimestral").ToList();
            }


            else
            {
                supports = context.Supports.Include(x => x.SupportRate).Where(x => x.SupportRate.Periodicity == "Mensual").ToList();
            }

            return supports;
        }



        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            {
                foreach (GridRow row in grid.Rows)
                {
                    string[] dataList = new string[] { "" };
                    Bill currentBill = GridHelper.RowToEntity<Bill>(row, dataList.ToList());
                    if (row.IsNew)
                    {
                        context.BillingRepository.AddBill(currentBill);
                    }
                    else if (row.IsDeleted)
                    {
                        context.BillingRepository.DeleteBill(currentBill);
                    }
                    else
                    {
                        context.BillingRepository.UpdateBill(currentBill);
                    }
                    context.SaveChanges();
                }
            }
            return grid;
        }

    }
}
