using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

namespace WIS.Billing.BusinessLogicCore.Controllers.Clients
{
    public class ClientController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private List<string> GridKeys { get; }

        public ClientController(ISessionAccessor session, IDbConnection connection)
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
            var fieldDescription = form.GetField("description");

            fieldDescription.Value = "Exito";

            var fieldAddress = form.GetField("address");

            fieldAddress.Value = "Staccato";

            var fieldRut= form.GetField("rut");

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


        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                new GridItemHeader("Cosas 1"),
                new GridButton("btnDormitar", "Dormitar", "fas fa-wrench"),
                new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                new GridItemDivider(),
                new GridItemHeader("Cosas 2"),
                new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            }));

            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var query = context.Clients;

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
                    Client currentClient = GridHelper.RowToEntity<Client>(row, dataList.ToList());
                    //Chequeo si el cliente existe buscando por RUT
                    //Client clientExists = CheckIfClientExists(currentClient);
                    Client clientExists = context.Clients.FirstOrDefault(x => x.RUT == currentClient.RUT);


                    if (clientExists != null)
                    {
                        //El cliente existe y la row es nueva, significa que quiere ingresar un cliente existente
                        if (row.IsNew)
                        {
                            throw new Exception("El cliente que intenta ingresar ya existe");
                        }
                        //El cliente existe y quiere modificar sus datos
                        else
                        {
                            clientExists.Address = currentClient.Address;
                            clientExists.Description = currentClient.Description;
                            //context.SaveChanges();
                        }
                    }
                    //Si no existe es porque quiere guardar. Compruebo igualmente estado de la row
                    else if(clientExists == null && row.IsNew)
                    {
                        context.Clients.Add(currentClient);
                    }
                    else if(row.IsDeleted)
                    {
                        context.Clients.Remove(currentClient);
                    }
                    context.SaveChanges();
                }

            }
            return grid;
        }

        public Client CheckIfClientExists(Client client)
        {            
            using (WISDB context = new WISDB())
            {
                Client c = context.Clients.FirstOrDefault(x => x.RUT == client.RUT);
                if (c != null)
                {
                    return c;
                }
                else
                {
                    return null;
                }
            }            
        }
    }
}
