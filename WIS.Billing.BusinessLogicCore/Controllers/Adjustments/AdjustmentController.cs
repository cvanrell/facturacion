using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using WIS.CommonCore.Exceptions;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogicCore.Controllers.Adjustments
{
    public class AdjustmentController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "ADJ010";
        private List<string> GridKeys { get; }

        public AdjustmentController(ISessionAccessor session, IDbConnection connection)
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
            //decimal pruebaDecimal = 2.4m;

            //this._session.SetValue("PRUEBA", pruebaDecimal);

            return data;
        }

        //FORMULARIO
        public override Form FormInitialize(Form form, FormQuery query, int userId)
        {
            //Inicializar selects
            this.InicializarSelects(ref form, userId); //TODO: No hace falta hacer un ref, los objetos se pasan por referencia

            return form;
        }
        public override Form FormSubmit(Form form, FormSubmitQuery query, int userId)
        {
            //form.GetField("address").Value = "Submitted and commited";

            //query.Redirect = "/stock/STO110";
            try
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-UY");

                Adjustment adjustment = new Adjustment()
                {
                    Year = DateTime.Parse(form.GetField("DateIPC").Value).Year,
                    Month = DateTime.Parse(form.GetField("DateIPC").Value).ToString("MMMM", culture),
                    IPCValue = Decimal.Parse(form.GetField("IPCValue").Value),
                    DateIPC = DateTime.Parse(form.GetField("DateIPC").Value),
                };

                using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
                {
                    context.AdjustmentRepository.AddAdjustment(adjustment);
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
            try
            {
                ExecuteAdjustments(form, query, userId);
            }
            catch(WISException ex)
            {
                throw new WISException(ex.Message);
            }
            

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

        private void InicializarSelects(ref Form form, int userId)
        {
            //Inicializar selects
            FormField selectMonth = form.GetField("Month");

            selectMonth.Options = new List<SelectOption>();
            //Cargar selects

            //Meses            
            selectMonth.Options.Add(new SelectOption(Meses.Enero.ToString(), Meses.Enero.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Febrero.ToString(), Meses.Febrero.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Marzo.ToString(), Meses.Marzo.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Abril.ToString(), Meses.Abril.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Mayo.ToString(), Meses.Mayo.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Junio.ToString(), Meses.Junio.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Julio.ToString(), Meses.Julio.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Agosto.ToString(), Meses.Agosto.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Septiembre.ToString(), Meses.Septiembre.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Octubre.ToString(), Meses.Octubre.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Noviembre.ToString(), Meses.Noviembre.ToString()));
            selectMonth.Options.Add(new SelectOption(Meses.Diciembre.ToString(), Meses.Diciembre.ToString()));
        }


        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var query = context.Adjustments;

                var defaultSort = new SortCommand("DateIPC", SortDirection.Descending);

                grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);
            }

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            {
                foreach (GridRow row in grid.Rows)
                {
                    string[] dataList = new string[] { "" };
                    Adjustment currentAdjustment = GridHelper.RowToEntity<Adjustment>(row, dataList.ToList());
                    if (row.IsNew)
                    {
                        //context.ProjectRepository.AddProject(currentProject);
                        //AddProject(context, currentProject);
                    }
                    else if (row.IsDeleted)
                    {
                        //context.ProjectRepository.DeleteProject(currentProject);
                    }
                    else
                    {
                        context.AdjustmentRepository.UpdateAdjustment(currentAdjustment);
                    }
                    context.SaveChanges();
                }
            }
            return grid;
        }


        public override Form ExecuteAdjustments(Form form, FormButtonActionQuery query, int userId)
        {
            try
            {
                using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
                {
                    context.AdjustmentRepository.ExecuteAdjustments();
                    context.SaveChanges();                    
                }

                query.Redirect = "/Clients/CLI050";

            }
            catch (Exception ex)
            {
                //query.AddErrorNotification(ex.Message, new List<string> { ex.Message });
                //query.Redirect = "/Clients/CLI050";
                throw new Exception(ex.Message);
            }

            return form;
        }
    }
}
