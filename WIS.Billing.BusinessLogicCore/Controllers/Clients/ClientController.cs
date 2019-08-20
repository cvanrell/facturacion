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
using WIS.CommonCore.App;
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
        private readonly string _pageName = "CLI010";
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


        //GRILLA
        public override Grid GridInitialize(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            grid.AddOrUpdateColumn(new GridColumnItemList("BTN_LIST", new List<IGridItem> {
                new GridItemHeader("Opciones"),
                new GridButton("btnEditar", "Tarifas de horas", "fas fa-wrench"),
                //new GridButton("btnAcceder", "Acceder", "fas fa-arrow-right"),
                new GridItemDivider(),
                //new GridItemHeader("Cosas 2"),
                //new GridButton("btnMejorar", "Conocer", "icon icon-cosa")
            }));

            return this.GridFetchRows(service, grid, gridQuery, userId);
        }
        public override Grid GridFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, int userId)
        {
            using (WISDB context = new WISDB())
            {
                var query = context.Clients.Where(x => x.FL_DELETED == "N");

                var defaultSort = new SortCommand("Description", SortDirection.Ascending);

                grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);
            }

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            {
                //using (WISDB context = new WISDB())
                //{
                foreach (GridRow row in grid.Rows)
                {
                    string[] dataList = new string[] { "" };
                    Client currentClient = GridHelper.RowToEntity<Client>(row, dataList.ToList());                    


                    if (row.IsNew)
                    {                        
                        context.ClientRepository.AddClient(currentClient);
                    }
                    else if (row.IsDeleted)
                    {
                        context.ClientRepository.DeleteClient(currentClient);
                        //DeleteClient(context, currentClient);
                    }
                    else
                    {
                        context.ClientRepository.UpdateClient(currentClient);
                        //UpdateClient(context, currentClient);
                    }


                    context.SaveChanges();
                }

                //}
            }

            return grid;
        }


        public override GridButtonActionQuery GridButtonAction(IGridService service, GridButtonActionQuery data, int userId)
        {
            if (data.ButtonId == "btnEditar")
            {
                

                data.Redirect = "/Clients/CLI020";

                this._session.SetValue("Id", data.Row.GetCell("Id").Value);
                this._session.SetValue("Description", data.Row.GetCell("Description").Value);
                this._session.SetValue("RUT", data.Row.GetCell("RUT").Value);

            }            



            return data;
        }

        //public Client CheckIfClientExists(WISDB context, Client client)
        //{
        //    Client c = context.Clients.FirstOrDefault(x => x.RUT == client.RUT);
        //    if (c != null)
        //    {
        //        return c;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        //public void AddClient(WISDB context, Client c)
        //{
        //    Client client = CheckIfClientExists(context, c);
        //    if (client != null)
        //    {
        //        if (client.FL_DELETED == "S")
        //        {
        //            client.FL_DELETED = "N";
        //        }
        //        else
        //        {
        //            throw new Exception("Ya existe un cliente con el RUT especificado");
        //        }
        //    }
        //    else
        //    {
        //        client = new Client()
        //        {
        //            Address = c.Address,
        //            Description = c.Description,
        //            RUT = c.RUT,
        //            FL_DELETED = "N",
        //        };
        //        context.Clients.Add(client);
        //        context.SaveChanges();
        //    }
        //}

        //public void UpdateClient(WISDB context, Client c)
        //{
        //    Client client = CheckIfClientExists(context, c);
        //    if (client == null)
        //    {
        //        throw new Exception("No se encuentra el cliente especificado");
        //    }
        //    else
        //    {
        //        client.Address = c.Address;
        //        client.Description = c.Description;
        //        context.SaveChanges();
        //    }
        //}

        //public void DeleteClient(WISDB context, Client c)
        //{
        //    Client client = CheckIfClientExists(context, c);
        //    if (client == null)
        //    {
        //        throw new Exception("No se encuentra el cliente que desea eliminar");
        //    }
        //    else
        //    {
        //        client.FL_DELETED = "S";
        //        context.SaveChanges();
        //    }
        //}
    }
}
