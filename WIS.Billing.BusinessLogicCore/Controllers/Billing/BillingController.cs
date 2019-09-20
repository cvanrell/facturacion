using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore.Entities;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.Enums;
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


        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                //new GridItemHeader("Cosas 1"),
                new GridButton("btnFacturas", "Facturas de mantenimiento", "fas fa-wrench"),
                //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                //new GridItemDivider(),
                //new GridItemHeader("Cosas 2"),
                //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            }));
            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {

            if (grid.Id == "BIL010_grid_1")
            {
                DateTime actualDate = DateTime.Now;

                using (WISDB context = new WISDB())
                {
                    //var query = context.Supports.Include("SupportRate");

                    var query = GetSupportsForBilling(DateTime.Now, context).AsQueryable();

                    //if (actualDate.Month.Equals(12))
                    //{
                    //    query = context.Supports.Include(x => x.SupportRate);
                    //}


                    //else if (actualDate.Month.Equals(6))
                    //{
                    //    query = context.Supports.Include(x => x.SupportRate).Where(x => x.SupportRate.Periodicity != "Anual");
                    //}


                    //else if (actualDate.Month.Equals(3) || actualDate.Month.Equals(9))
                    //{
                    //    query = context.Supports.Include(x => x.SupportRate).Where(x => x.SupportRate.Periodicity == "Mensual" || x.SupportRate.Periodicity == "Trimestral");
                    //}


                    //else
                    //{
                    //    query = context.Supports.Include(x => x.SupportRate).Where(x => x.SupportRate.Periodicity == "Mensual");
                    //}

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
                }
            }
            else if(grid.Id == "BIL020_grid_1")
            {

            }
            return grid;
        }

        public override GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            if (data.ButtonId == "btnFacturas")
            {


                data.Redirect = "/Billing/BIL020";

                //this._session.SetValue("Id", data.Row.GetCell("Id").Value);
                //this._session.SetValue("Description", data.Row.GetCell("Description").Value);
                //this._session.SetValue("Address", data.Row.GetCell("Address").Value);
                //this._session.SetValue("RUT", data.Row.GetCell("RUT").Value);

            }



            return data;
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
