using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WIS.Billing.BusinessLogicCore.Controllers;
using WIS.Billing.BusinessLogicCore.Enums;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;
using WIS.BusinessLogicCore.Controllers;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.App;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FormComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Page;
using WIS.CommonCore.Session;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogicCore
{
    public class ProjectController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private List<string> GridKeys { get; }

        public ProjectController(ISessionAccessor session, IDbConnection connection)
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
            //Inicializar selects
            this.InicializarSelects(ref form, userId); //TODO: No hace falta hacer un ref, los objetos se pasan por referencia

            var fieldDescription = form.GetField("description");

            fieldDescription.Value = "Exito";

            var fieldAddress = form.GetField("address");

            fieldAddress.Value = "Staccato";

            var fieldRut = form.GetField("rut");

            fieldRut.Value = "Staccato";
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

        private void InicializarSelects(ref Form form, int userId)
        {
            //Inicializar selects
            FormField selectCurrency = form.GetField("Currency");
            

            selectCurrency.Options = new List<SelectOption>();
            

            //Cargar selects
            selectCurrency.Options.Add(new SelectOption(TipoMoneda.Dólar.ToString(), TipoMoneda.Dólar.ToString()));
            selectCurrency.Options.Add(new SelectOption(TipoMoneda.Pesos.ToString(), TipoMoneda.Pesos.ToString()));

            //using (UnitOfWork uow = new UnitOfWork(this._pageName, userId))
            //{
            //    //VIA
            //    var queryVia = uow.BuildQuery(new GetViaQuery());

            //    var dataVia = queryVia.Select(e => e).ToList();

            //    foreach (var VIA in dataVia)
            //    {
            //        selectVia.Options.Add(new SelectOption(VIA.CD_VIA.ToString(), VIA.DS_VIA));
            //    }

            //    //TRANSPORTADORA
            //    var queryTransportadora = uow.BuildQuery(new GetTransportadoraQuery());
            //    var dataTransportadora = queryTransportadora.Select(t => t).ToList();

            //    foreach (var transportadora in dataTransportadora)
            //    {
            //        selectTransportadora.Options.Add(new SelectOption(transportadora.CD_TRANSPORTADORA.ToString(), transportadora.DS_TRANSPORTADORA));
            //    }

            //    //MONEDA
            //    var queryMoneda = uow.BuildQuery(new GetMonedaQuery());
            //    var dataMoneda = queryMoneda.Select(m => m).ToList();

            //    foreach (var moneda in dataMoneda)
            //    {
            //        selectMoneda.Options.Add(new SelectOption(moneda.CD_MONEDA.ToString(), moneda.DS_MONEDA));
            //    }

            //    //ALMACENAJE Y SEGURO
            //    var queryAlmacenajeSeguro = uow.BuildQuery(new GetAlmacenajeSeguroQuery());
            //    var dataAlmcenajeSeguro = queryAlmacenajeSeguro.Select(a => a).ToList();

            //    foreach (var almacenajeSeguro in dataAlmcenajeSeguro)
            //    {
            //        selectAlmacenajeSeguro.Options.Add(new SelectOption(almacenajeSeguro.TP_ALMACENAJE_Y_SEGURO.ToString(), almacenajeSeguro.DS_ALMACENAJE_Y_SEGURO));
            //    }

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
            using (WISDB context = new WISDB())
            {
                var query = context.Projects.Where(x => x.FL_DELETED == "N");

                var defaultSort = new SortCommand("Description", SortDirection.Ascending);

                grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);
            }

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            using (WISDB context = new WISDB())
            {
                foreach (GridRow row in grid.Rows)
                {
                    string[] dataList = new string[] { "" };
                    Project currentProject = GridHelper.RowToEntity<Project>(row, dataList.ToList());
                    //Chequeo si el cliente existe buscando por RUT                    



                    if (row.IsNew)
                    {
                        AddProject(context, currentProject);
                    }
                    else if (row.IsDeleted)
                    {
                        DeleteProject(context, currentProject);
                    }
                    else
                    {
                        UpdateProject(context, currentProject);
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


        public void AddProject(WISDB context, Project p)
        {
            Client client = context.Clients.FirstOrDefault(x => x.Id == p.Id);
            Project project = context.Projects.FirstOrDefault(x => x.Description == p.Description);
            if (project != null)
            {
                if (project.FL_DELETED == "S")
                {
                    project.FL_DELETED = "N";
                }
                else
                {
                    throw new Exception("Ya existe un proyecto con la descripción especificada");
                }
            }
            else
            {
                project = new Project()
                {                     
                    Description = p.Description,
                    Currency = p.Currency,
                    Amount = p.Amount,
                    IVA = p.IVA,
                    Total = p.Total,
                    InitialDate = p.InitialDate,
                    TotalAmount = p.Total,
                    FL_DELETED = "N",
                };
                //Pregunto si el cliente para ese proyecto es extranjero y seteo el IVA en 0
                if(client.FL_FOREIGN == "S")
                {
                    project.IVA = 0;
                }
                context.Projects.Add(project);
                context.SaveChanges();
            }
        }

        public void UpdateProject(WISDB context, Project p)
        {
            Project project = context.Projects.FirstOrDefault(x => x.Id == p.Id);
            if (project == null)
            {
                throw new Exception("No se encuentra el proyecto especificado");
            }
            else
            {
                project.Description = p.Description;
                project.Currency = p.Currency;
                project.Amount = p.Amount;
                project.IVA = p.IVA;
                project.Total = p.Total;
                project.InitialDate = p.InitialDate;
                project.TotalAmount = p.TotalAmount;
                context.SaveChanges();
            }
        }

        public void DeleteProject(WISDB context, Project p)
        {
            Project project = context.Projects.FirstOrDefault(x => x.Id == p.Id);
            if (project == null)
            {
                throw new Exception("No se encuentra el proyecto que desea eliminar");
            }
            else
            {
                project.FL_DELETED = "S";
                context.SaveChanges();
            }
        }
    }
}
