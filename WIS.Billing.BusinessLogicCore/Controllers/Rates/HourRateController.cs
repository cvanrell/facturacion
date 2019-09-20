using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.DataAccessCore.Database;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogicCore.Controllers.Rates
{
    public class HourRateController : BaseController
    {

        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "CLI030";
        private List<string> GridKeys { get; }

        public HourRateController(ISessionAccessor session, IDbConnection connection)
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

            string HourRateDescription = _session.GetValue<string>("Description");

            form.GetField("Description").Value = HourRateDescription;

            string clientDescription = _session.GetValue<string>("Client");

            form.GetField("Client").Value = clientDescription;

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
            //grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
            //    //new GridItemHeader("Cosas 1"),
            //    new GridButton("btnHistorico", "Historico Tarifa", "fas fa-wrench"),
            //    //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
            //    //new GridItemDivider(),
            //    //new GridItemHeader("Cosas 2"),
            //    //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            //}));

            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            try
            {
                using (WISDB context = new WISDB())
                {
                    var idTarifa = _session.GetValue<string>("Id");

                    if (!string.IsNullOrEmpty(idTarifa))
                    {
                        //CONSULTA A LA VISTA PARA CONSEGUIR DATOS DEL LOG
                        var query = context.H_HOUR_RATE.Where(x => x.ID_HOUR_RATE == idTarifa);

                        var defaultSort = new SortCommand("DT_ADDROW", SortDirection.Descending);

                        grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);

                        
                    }
                }

                foreach (var row in grid.Rows)
                {
                    DateTime date = new DateTime();
                    foreach (var cell in row.Cells)
                    {
                        if (cell.Column.Id == "DT_ADDROW")
                        {
                            date = DateTime.Parse(cell.Value).Date;
                        }

                        if (cell.Column.Id == "DT_ONLY")
                        {
                            cell.Value = date.ToString("d");
                            //cell.Value = date.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error al cargar la grilla:" + grid.Id);
            }

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {           
            

            return grid;
        }


        public override GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            //if (data.ButtonId == "btnHistorico")
            //{
            //    //JavaScriptSerializer JSONConverter = new JavaScriptSerializer();

            //    data.Parameters.Add(new ComponentParameter
            //    {
            //        Id = "HISTORICO",
            //        Value = "true"
            //    });
            //    _session.SetValue("Tarifas_HISTORICO", true);

            //    data.Redirect = "/Clients/CLI_030";

            //    this._session.SetValue("Id", data.Row.GetCell("Id").Value);

            //}
            return data;
        }
                
    }
}
