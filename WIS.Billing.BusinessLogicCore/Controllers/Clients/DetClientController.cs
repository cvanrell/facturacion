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
    public class DetClientController : BaseController
    {
        private readonly ISessionAccessor _session;
        private readonly IDbConnection _connection;
        private readonly string _pageName = "CLI020";
        private List<string> GridKeys { get; }

        public DetClientController(ISessionAccessor session, IDbConnection connection)
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
            //var fieldDescription = form.GetField("description");

            //fieldDescription.Value = "Exito";

            //var fieldAddress = form.GetField("address");

            //fieldAddress.Value = "Staccato";

            //var fieldRut = form.GetField("rut");

            //fieldRut.Value = "Staccato";

            //string cliente = _session.GetValue<string>("Id");

            //form.GetField("Description").Value = cliente;

            string clientDescription = _session.GetValue<string>("Description");

            form.GetField("Description").Value = clientDescription;

            string rutCliente = _session.GetValue<string>("RUT");

            form.GetField("RUT").Value = rutCliente;

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
                        switch (grid.Id)
                        {
                            //Obtiene grid con registros de las tarifas de horas del cliente
                            case "CLI020_grid_T":
                                return GridHourRateFetchRows(service, grid, gridQuery, context, idCliente);
                            //Obtiene grid con registros de las tarifas de soporte del cliente
                            case "CLI020_grid_S":
                                return GridSupportRatesFetchRows(service, grid, gridQuery, context, idCliente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("Erro al cargar la grilla:" + grid.Id);
            }


            //using (WISDB context = new WISDB())
            //{
            //    var query = context.Clients.Where(x => x.FL_DELETED == "N");

            //    var defaultSort = new SortCommand("Description", SortDirection.Ascending);

            //    grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);
            //}

            return grid;
        }

        public override Grid GridCommit(IGridService service, Grid grid, GridFetchRequest query, int userId)
        {
            //Para realizar commit desde varias grid, pregunto por el id de la misma y derivo a funcionalidad para 
            //ingresar en la tabla que corresponda
            var rutCliente = _session.GetValue<string>("RUT");

            using (UnitOfWork context = new UnitOfWork(this._pageName, userId))
            {
                //using (WISDB context = new WISDB())
                //{
                foreach (GridRow row in grid.Rows)
                {
                    string[] dataList = new string[] { "" };


                    //Si la grilla es de tarifas:
                    if (grid.Id == "CLI020_grid_T")
                    {
                        HourRate currentHRate = GridHelper.RowToEntity<HourRate>(row, dataList.ToList());
                        if (row.IsNew)
                        {
                            context.ClientRepository.AddHourRate(currentHRate, rutCliente);
                            //AddHourRate(context, currentHRate, rutCliente);
                        }
                        else if (row.IsDeleted)
                        {
                            context.ClientRepository.DeleteHourRate(currentHRate);
                        }
                        else
                        {
                            context.ClientRepository.UpdateHourRate(currentHRate, rutCliente);
                        }
                    }//Si la grilla es de tarifas de soporte
                    else if (grid.Id == "CLI020_grid_S")
                    {
                        SupportRate currentSRate = GridHelper.RowToEntity<SupportRate>(row, dataList.ToList());
                        if (row.IsNew)
                        {
                            context.ClientRepository.AddSupportRate(currentSRate, rutCliente);
                        }
                        else if (row.IsDeleted)
                        {
                            context.ClientRepository.DeleteSupportRate(currentSRate);
                        }
                        else
                        {
                            context.ClientRepository.UpdateSupportRate(currentSRate, rutCliente);
                        }
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
                //JavaScriptSerializer JSONConverter = new JavaScriptSerializer();

                data.Parameters.Add(new ComponentParameter
                {
                    Id = "EDITAR",
                    Value = "true"
                });
                _session.SetValue("Clients_EDITAR", true);

                data.Redirect = "/Clients/DET_CLIENTS";

                this._session.SetValue("Id", data.Row.GetCell("Id").Value);

            }
            //else if (data.ButtonId == "btnSaldo")
            //{
            //    data.Redirect = "/documento/DOC020";

            //    this._session.SetValue("DOC020_NU_DOCUMENTO", data.Row.GetCell("NU_DOCUMENTO").Value);
            //    this._session.SetValue("DOC020_TP_DOCUMENTO", data.Row.GetCell("TP_DOCUMENTO").Value);
            //    this._session.SetValue("DOC020_CD_EMPRESA", data.Row.GetCell("CD_EMPRESA").Value);
            //}
            //else
            //{
            //    data.Redirect = "/documento/DOC081";

            //    this._session.SetValue("DOC080_NU_DOCUMENTO", data.Row.GetCell("NU_DOCUMENTO").Value);
            //    this._session.SetValue("DOC080_TP_DOCUMENTO", data.Row.GetCell("TP_DOCUMENTO").Value);
            //    this._session.SetValue("DOC080_CD_EMPRESA", data.Row.GetCell("CD_EMPRESA").Value);
            //}



            return data;
        }

        public Grid GridHourRateFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, WISDB context, string idCliente)
        {
            var query = context.HourRates.Where(x => x.Client.Id.ToString() == idCliente && x.FL_DELETED == "N");

            var defaultSort = new SortCommand("Amount", SortDirection.Ascending);

            grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);


            return grid;
        }


        public Grid GridSupportRatesFetchRows(IGridService service, Grid grid, GridFetchRequest gridQuery, WISDB context, string idCliente)
        {
            var query = context.SupportRates.Where(x => x.Client.Id.ToString() == idCliente && x.FL_DELETED == "N");

            var defaultSort = new SortCommand("Amount", SortDirection.Ascending);

            grid.Rows = service.GetRows(query, grid.Columns, gridQuery, defaultSort, this.GridKeys);

            return grid;
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
        //    //Client client = context.Clients.FirstOrDefault(x => x.RUT == c.RUT);
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

        #region TARIFAS
        public void AddHourRate(WISDB context, HourRate hRate, string rutCliente)
        {
            Client client = context.Clients.FirstOrDefault(x => x.RUT == rutCliente);
            //Client client = Utils.CheckIfClientExists(context, c);
            if (client != null)
            {
                //Verificar que no existe una tarifa de hora con los mismos datos
                HourRate hr = context.HourRates.FirstOrDefault(x => x.Client.Id == client.Id &&
                                x.Currency == hRate.Currency && x.AdjustmentPeriodicity == hRate.AdjustmentPeriodicity &&
                                x.Amount == hRate.Amount && x.SpecialDiscount == hRate.SpecialDiscount);

                //La tarifa existe
                if (hr != null)
                {
                    //Si registro estaba eliminado, lo vuelvo a activar
                    if (hr.FL_DELETED == "S")
                    {
                        hr.FL_DELETED = "N";
                    }
                    else
                    {
                        throw new Exception("Ya existe una tarifa con los datos ingresados");
                    }
                }
                else
                {
                    HourRate newHR = new HourRate()
                    {
                        Description = hRate.Description,
                        Client = client,
                        Currency = hRate.Currency,
                        AdjustmentPeriodicity = hRate.AdjustmentPeriodicity,
                        Amount = hRate.Amount,
                        SpecialDiscount = hRate.SpecialDiscount,
                        FL_DELETED = "N"
                    };
                    context.HourRates.Add(newHR);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("No se encontro el cliente especificado");
            }
        }

        public void UpdateHourRate(WISDB context, HourRate hRate, string rutCliente)
        {
            HourRate hr = context.HourRates.FirstOrDefault(x => x.Id == hRate.Id);
            if (hr == null)
            {
                throw new Exception("No se encuentra la tarifa especificada");
            }
            else
            {
                hr.Description = hRate.Description;
                hr.Currency = hRate.Currency;
                hr.AdjustmentPeriodicity = hRate.AdjustmentPeriodicity;
                hr.Amount = hRate.Amount;
                hr.SpecialDiscount = hRate.SpecialDiscount;
                context.SaveChanges();
            }
        }

        public void DeleteHourRate(WISDB context, HourRate hRate)
        {
            HourRate hr = context.HourRates.FirstOrDefault(x => x.Id == hRate.Id);
            if (hr == null)
            {
                throw new Exception("No se encuentra la tarifa que desea eliminar");
            }
            else
            {
                hr.FL_DELETED = "S";
                context.SaveChanges();
            }
        }
        #endregion


        #region TARIFAS DE SOPORTE
        public void AddSupportRate(WISDB context, SupportRate sRate, string rutCliente)
        {
            Client client = context.Clients.FirstOrDefault(x => x.RUT == rutCliente);
            if (client != null)
            {
                //Verificar que no existe una tarifa de hora con los mismos datos
                SupportRate sr = context.SupportRates.FirstOrDefault(x => x.Client.Id == client.Id &&
                                x.Currency == sRate.Currency && x.AdjustmentPeriodicity == sRate.AdjustmentPeriodicity &&
                                x.Amount == sRate.Amount && x.SpecialDiscount == sRate.SpecialDiscount &&
                                x.IVA == sRate.IVA && x.Periodicity == sRate.Periodicity);

                //No existe tarifa, puede agregarla
                if (sr != null)
                {
                    //Si registro estaba eliminado, lo vuelvo a activar
                    if (sr.FL_DELETED == "S")
                    {
                        sr.FL_DELETED = "N";
                    }
                    else
                    {
                        throw new Exception("Ya existe una tarifa de soporte con los datos ingresados");
                    }
                }
                else
                {
                    SupportRate newSR = new SupportRate()
                    {
                        Description = sRate.Description,
                        Client = client,
                        Currency = sRate.Currency,
                        Periodicity = sRate.Periodicity,
                        AdjustmentPeriodicity = sRate.AdjustmentPeriodicity,
                        Amount = sRate.Amount,
                        SpecialDiscount = sRate.SpecialDiscount,
                        FL_DELETED = "N"
                    };
                    context.SupportRates.Add(newSR);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new Exception("No se encontro el cliente especificado");
            }
        }

        public void UpdateSupportRate(WISDB context, SupportRate sRate, string rutCliente)
        {
            SupportRate sr = context.SupportRates.FirstOrDefault(x => x.Id == sRate.Id);
            if (sr == null)
            {
                throw new Exception("No se encuentra la tarifa especificada");
            }
            else
            {
                sr.Description = sRate.Description;
                sr.Currency = sRate.Currency;
                sr.Periodicity = sr.Periodicity;
                sr.AdjustmentPeriodicity = sRate.AdjustmentPeriodicity;
                sr.Amount = sRate.Amount;
                sr.IVA = sRate.IVA;
                sr.SpecialDiscount = sRate.SpecialDiscount;
                context.SaveChanges();
            }
        }

        public void DeleteSupportRate(WISDB context, SupportRate sRate)
        {
            SupportRate sr = context.SupportRates.FirstOrDefault(x => x.Id == sRate.Id);
            if (sr == null)
            {
                throw new Exception("No se encuentra la tarifa que desea eliminar");
            }
            else
            {
                sr.FL_DELETED = "S";
                context.SaveChanges();
            }
        }

        #endregion
    }
}
