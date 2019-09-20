using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.DataModel;
using WIS.Billing.BusinessLogicCore.DataModel.Queries;
using WIS.Billing.BusinessLogicCore.Enums;
using WIS.Billing.BusinessLogicCore.Validation.Rules;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
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

namespace WIS.Billing.BusinessLogicCore.Controllers.Maintenance
{
    public class MaintenanceController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "MAN010";
        private List<string> GridKeys { get; }

        public MaintenanceController(ISessionAccessor session, IDbConnection connection)
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
            //Inicializar selects
            this.InicializarSelects(ref form, userId); //TODO: No hace falta hacer un ref, los objetos se pasan por referencia
            
            return form;
        }

        //public override List<SelectOption> FormSelectSearch(Form form, FormSelectSearchQuery query, int userId)
        //{
        //    switch (query.FieldId)
        //    {
        //        case "Cliente":
        //            return this.SearchTarifa(form, query, userId);
        //        default:
        //            return new List<SelectOption>();
        //    }
        //}

        public override Form FormSubmit(Form form, FormSubmitQuery query, int userId)
        {
            try
            {
                Support support = new Support()
                {
                    Description = form.GetField("Description").Value,                    

                };

                using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
                {
                    Client c = context.ClientRepository.CheckIfClientExists(form.GetField("Client").Value);
                    SupportRate sr = context.ClientRepository.CheckIfSupportRateExists(form.GetField("SupportRate").Value);
                    support.Client = c;
                    support.SupportRate = sr;
                    context.SupportRepository.AddSupport(support);
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
                ["Client"] = this.ValidateClient,
                
            };
            return schema;
        }

        private void InicializarSelects(ref Form form, int userId)
        {
            FormField selectClient = form.GetField("Client");

            selectClient.Options = new List<SelectOption>();


            //Cargar selects
            //selectPeriodicity.Options.Add(new SelectOption(Periodicidad.Mensual.ToString(), Periodicidad.Mensual.ToString()));
            //selectPeriodicity.Options.Add(new SelectOption(Periodicidad.Trimestral.ToString(), Periodicidad.Trimestral.ToString()));
            //selectPeriodicity.Options.Add(new SelectOption(Periodicidad.Semestral.ToString(), Periodicidad.Semestral.ToString()));
            //selectPeriodicity.Options.Add(new SelectOption(Periodicidad.Anual.ToString(), Periodicidad.Anual.ToString()));

            using (WISDB context = new WISDB())
            {
                //PROYECTOS
                var clients = context.Clients.Where(x => x.FL_DELETED == "N").ToList();

                foreach (var c in clients)
                {
                    selectClient.Options.Add(new SelectOption(c.Id.ToString(), c.Description));
                }

            }
            //using (UnitOfWork uow = new UnitOfWork(this._pageName, userId))
            //{            
            //    //UNIDAD MEDIDA
            //    //GetUnidadMedidaQuery queryUnidadMedida = new GetUnidadMedidaQuery(context);
            //    //var dataUnidadMedida = queryUnidadMedida.GetQuery().Select(e => e).ToList();

            //    //foreach (var unidadMedida in dataUnidadMedida)
            //    //{
            //    //    selectUnidadMedida.Options.Add(new SelectOption(unidadMedida.CD_UNIDADE_MEDIDA.ToString(), unidadMedida.DS_UNIDADE_MEDIDA));
            //    //}
            //}
        }


        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            //grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
            //    //new GridItemHeader("Cosas 1"),
            //    new GridButton("btnCuotas", "Cuotas de proyecto", "fas fa-wrench"),
            //    //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
            //    //new GridItemDivider(),
            //    //new GridItemHeader("Cosas 2"),
            //    //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            //}));

            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var query = context.Supports.Where(x => x.FL_DELETED == "N");

                var defaultSort = new SortCommand("Description", SortDirection.Ascending);

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
            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            //using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            //{
            //    //using (WISDB context = new WISDB())
            //    //{
            //    foreach (GridRow row in grid.Rows)
            //    {
            //        string[] dataList = new string[] { "" };
            //        Project currentProject = GridHelper.RowToEntity<Project>(row, dataList.ToList());
            //        //Chequeo s |i el cliente existe buscando por RUT                    



            //        if (row.IsNew)
            //        {
            //            context.ProjectRepository.AddProject(currentProject);
            //            //AddProject(context, currentProject);
            //        }
            //        else if (row.IsDeleted)
            //        {
            //            context.ProjectRepository.DeleteProject(currentProject);
            //        }
            //        else
            //        {
            //            context.ProjectRepository.UpdateProject(currentProject);
            //        }


            //        context.SaveChanges();
            //    }

            //    //}
            //}
            return grid;
        }
        

        #region VALIDACIONES

        //private FormValidationGroup ValidateDescription(FormField field, Form form, List<ComponentParameter> parameters, int userId, WISDB context)
        //{
        //    return new FormValidationGroup
        //    {
        //        BreakValidationChain = true,
        //        Rules = new List<WIS.BusinessLogicCore.Validation.IValidationRule>
        //        {

        //        }//,
        //        //OnSuccess = this.ValidateTP_INGRESO_OnSuccess
        //    };
        //}

        private FormValidationGroup ValidateClient(FormField field, Form form, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            return new FormValidationGroup
            {
                Field = field,
                BreakValidationChain = true,
                Rules = new List<WIS.BusinessLogicCore.Validation.IValidationRule>
                {
                    new ExisteClienteValidationRule(field.Value, context)
                },
                OnSuccess = this.ValidateCliente_OnSuccess
            };
        }
        #endregion

        public void ValidateCliente_OnSuccess(FormField field, Form form, List<ComponentParameter> parameters, int userId, WISDB context)
        {
            var cliente = form.GetField("Client").Value;

            FormField selectTarifa = form.GetField("SupportRate");

            List<SupportRate> supportRates = context.SupportRates.Where(x => x.Client.Id.ToString() == cliente && x.FL_DELETED != "S").ToList();

            //selectTarifa.Value = string.Empty;

            selectTarifa.Options.Clear();

            supportRates.ForEach(sRates =>
            {
                selectTarifa.Options.Add(new SelectOption(sRates.Id.ToString(), sRates.Description));
            });



        }

        #region SELECTS
        //private List<SelectOption> SearchTarifa(Form form, FormSelectSearchQuery FormQuery, int userId)
        //{
        //    List<SelectOption> opciones = new List<SelectOption>();

        //    var idCliente = form.GetField("Cliente").Value;

        //    using (UnitOfWork uow = new UnitOfWork(this._pageName, userId))
        //    {
        //        //if (int.TryParse(form.GetField("cdEmpresa").Value, out int cdEmpresa))                    
        //        //{
        //        var query = uow.BuildQuery(new GetTarifasSoporteQuery(FormQuery.SearchValue, idCliente));

        //        var data = query.Select(e => e).ToList();

        //        foreach (var cliente in data)
        //        {
        //            opciones.Add(new SelectOption(cliente.Id.ToString(), cliente.Description.ToString()));
        //        }
        //        //}
        //    }

        //    return opciones;
        //}
        #endregion
    }
}
