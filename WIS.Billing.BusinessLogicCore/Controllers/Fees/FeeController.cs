using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogicCore.Controllers.Fees
{
    public class FeeController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "FEE010";
        private List<string> GridKeys { get; }

        public FeeController(ISessionAccessor session, IDbConnection connection)
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
            decimal pruebaDecimal = 2.4m;

            this._session.SetValue("PRUEBA", pruebaDecimal);

            return data;
        }

        //FORMULARIO
        public override Form FormInitialize(Form form, FormQuery query, int userId)
        {
            string projectDescription = _session.GetValue<string>("Description");

            form.GetField("Description").Value = projectDescription;

            return form;
        }
        public override Form FormSubmit(Form form, FormSubmitQuery query, int userId)
        {

            query.ResetForm = true;

            return form;
        }
        public override Form FormButtonAction(Form form, FormButtonActionQuery query, int userId)
        {
            form.GetField("address").Value = "Button action performed";
            form.GetField("type").Value = "3";

            return form;
        }


        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            //grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
            //    new GridItemHeader("Cosas 1"),
            //    new GridButton("btnEditar", "Tarifas de horas", "fas fa-wrench"),
            //    new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
            //    new GridItemDivider(),
            //    new GridItemHeader("Cosas 2"),
            //    new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            //}));

            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            try
            {
                using (WISDB context = new WISDB())
                {
                    var idCliente = _session.GetValue<string>("Id");

                    if (!string.IsNullOrEmpty(idCliente))
                    {
                        var query = context.Fees.Where(x => x.FL_DELETED == "N");

                        var defaultSort = new SortCommand("MonthYear", SortDirection.Ascending);

                        grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Erro al cargar la grilla:" + grid.Id);
            }

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {            
            var idProject = _session.GetValue<string>("Id");

            using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            {
                foreach (GridRow row in grid.Rows)
                {
                    string[] dataList = new string[] { "" };


                    Fee currentFee = GridHelper.RowToEntity<Fee>(row, dataList.ToList());
                    if (row.IsNew)
                    {
                        context.FeeRepository.AddFee(currentFee, idProject);
                    }
                    else if (row.IsDeleted)
                    {
                        context.FeeRepository.DeleteFee(currentFee);
                    }
                    else
                    {
                        context.FeeRepository.UpdateFee(currentFee);
                    }
                    context.SaveChanges();
                }                
            }
            return grid;
        }


        public override GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            return data;
        }

    }
}
