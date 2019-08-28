using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.DataAccessCore.Database;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.App;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogicCore.Controllers.Rates
{
    public class RateController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "CLI050";
        private List<string> GridKeys { get; }

        public RateController(ISessionAccessor session, IDbConnection connection)
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

            string RateDescription = _session.GetValue<string>("Description");

            form.GetField("Description").Value = RateDescription;

            return form;
        }
        public override Form FormSubmit(Form form, FormSubmitQuery query, int userId)
        {
            form.GetField("address").Value = "Submitted and commited";

            //query.Redirect = "/stock/STO110";

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
            if (grid.Id == "CLI050_grid_T")
            {
                grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                //new GridItemHeader("Cosas 1"),
                new GridButton("btnHistoricoH", "Historico Tarifa", "fas fa-wrench"),
                //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                //new GridItemDivider(),
                //new GridItemHeader("Cosas 2"),
                //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            }));
            }
            else if (grid.Id == "CLI050_grid_S")
            {
                grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                //new GridItemHeader("Cosas 1"),
                new GridButton("btnHistoricoS", "Historico Tarifa", "fas fa-wrench"),
                //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                //new GridItemDivider(),
                //new GridItemHeader("Cosas 2"),
                //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            }));
            }

            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            try
            {
                using (WISDB context = new WISDB())
                {
                    switch (grid.Id)
                    {
                        //Obtiene grid con registros de las tarifas de horas del cliente
                        case "CLI050_grid_T":
                            return GridHourRateFetchRows(service, grid, gridQuery, context);
                        //Obtiene grid con registros de las tarifas de soporte del cliente
                        case "CLI050_grid_S":
                            return GridSupportRatesFetchRows(service, grid, gridQuery, context);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error al cargar la grilla:" + grid.Id);
            }

            return grid;
        }

        public Grid GridHourRateFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, WISDB context)
        {
            var query = context.V_HOUR_RATES;

            var defaultSort = new SortCommand("Client", SortDirection.Ascending);

            grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);


            return grid;
        }


        public Grid GridSupportRatesFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, WISDB context)
        {
            var query = context.V_SUPPORT_RATES;

            var defaultSort = new SortCommand("Client", SortDirection.Ascending);

            grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {


            return grid;
        }


        public override GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            if (data.ButtonId == "btnHistoricoH")
            {
                //JavaScriptSerializer JSONConverter = new JavaScriptSerializer();

                data.Parameters.Add(new ComponentParameter
                {
                    Id = "HISTORICO",
                    Value = "true"
                });
                _session.SetValue("Tarifas_HISTORICO", true);

                data.Redirect = "/Clients/CLI030";

                this._session.SetValue("Id", data.Row.GetCell("Id").Value);
                this._session.SetValue("Description", data.Row.GetCell("Description").Value);

            }
            else if (data.ButtonId == "btnHistoricoS")
            {
                data.Parameters.Add(new ComponentParameter
                {
                    Id = "HISTORICO",
                    Value = "true"
                });
                _session.SetValue("Tarifas_HISTORICO", true);

                data.Redirect = "/Clients/CLI040";

                this._session.SetValue("Id", data.Row.GetCell("Id").Value);
                this._session.SetValue("Description", data.Row.GetCell("Description").Value);
            }
            return data;
        }
    }
}
