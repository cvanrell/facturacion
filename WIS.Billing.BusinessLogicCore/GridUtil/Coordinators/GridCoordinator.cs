using WIS.BusinessLogicCore.Controllers;
using WIS.CommonCore.Session;
using WIS.CommonCore.Exceptions;
using WIS.Billing.DataAccessCore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WIS.CommonCore.ServiceWrappers;
using WIS.BusinessLogicCore.GridUtil.Services;
using WIS.CommonCore.GridComponents;

namespace WIS.BusinessLogicCore.GridUtil.Coordinators
{
    public class GridCoordinator : IGridCoordinator
    {
        private readonly IGridController _controller;
        private readonly IGridService _service;
        private readonly IDbConnection _connection;

        public GridCoordinator(IGridController controller, IGridService service, IDbConnection connection)
        {
            this._controller = controller;
            this._service = service;
            this._connection = connection;
        }

        public IGridWrapper Initialize(IGridWrapper wrapper)
        {
            var query = wrapper.GetData<GridFetchRequest>();

            IGridWrapper response = new GridWrapper(wrapper);

            var grid = new Grid(query.GridId);

            using (var context = new WISDB())
            {
                grid.Columns = this._service.GetColumns(context, wrapper.Application, wrapper.GridId, wrapper.User);
            }

            try
            {
                grid = this._controller.GridInitialize(this._service, grid, query, wrapper.User);

                using (WISDB context = new WISDB())
                {
                    this._service.SaveAddedColumns(context, grid, wrapper.Application);

                    context.SaveChanges();
                }

                var queryResponse = new GridInitializeResponse
                {
                    Grid = grid,
                    Parameters = query.Parameters
                };

                response.SetData(queryResponse);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IGridWrapper FetchRows(IGridWrapper wrapper)
        {
            var query = wrapper.GetData<GridFetchRequest>();

            IGridWrapper response = new GridWrapper(wrapper);

            var grid = new Grid(query.GridId);

            using (var context = new WISDB())
            {
                grid.Columns = this._service.GetColumns(context, wrapper.Application, wrapper.GridId, wrapper.User);
            }

            try
            {
                grid = this._controller.GridFetchRows(this._service, grid, query, wrapper.User);

                var queryResponse = new GridFetchResponse
                {
                    Rows = grid.Rows,
                    Parameters = query.Parameters
                };

                response.SetData(queryResponse);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IGridWrapper ValidateRow(IGridWrapper wrapper)
        {
            var data = wrapper.GetData<GridValidationRequest>();

            IGridWrapper response = new GridWrapper(wrapper);

            var grid = new Grid(data.GridId);

            using (var context = new WISDB())
            {
                grid.Columns = this._service.GetColumns(context, wrapper.Application, wrapper.GridId, wrapper.User);
            }

            GridRow row = data.Row;

            row.SetCellColumn(grid.Columns);

            try
            {
                grid = this._controller.GridValidateRow(this._service, row, grid, data.Parameters, wrapper.User);

                var validationResponse = new GridValidationResponse
                {
                    Row = row,
                    Parameters = data.Parameters
                };

                response.SetData(validationResponse);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IGridWrapper Commit(IGridWrapper wrapper)
        {
            var data = wrapper.GetData<GridCommitRequest>();

            IGridWrapper response = new GridWrapper(wrapper);

            var grid = new Grid(wrapper.GridId);

            using (var context = new WISDB())
            {
                grid.Columns = this._service.GetColumns(context, wrapper.Application, wrapper.GridId, wrapper.User);
            }

            grid.Rows = data.Rows;

            try
            {
                foreach (var row in grid.Rows)
                {
                    var validatedRow = row;

                    validatedRow.SetCellColumn(grid.Columns);

                    foreach (var cell in validatedRow.Cells)
                    {
                        cell.Modified = true;
                    }

                    grid = this._controller.GridValidateRow(this._service, validatedRow, grid, data.Query.Parameters, wrapper.User);
                }

                if (grid.Rows.Any(r => !r.IsValid()))
                    throw new WISException("Se encontraron errores al validar");

                grid = this._controller.GridCommit(this._service, grid, data.Query, wrapper.User);

                grid = this._controller.GridFetchRows(this._service, grid, data.Query, wrapper.User);

                var queryResponse = new GridFetchResponse
                {
                    Rows = grid.Rows,
                    Parameters = data.Query.Parameters
                };

                response.SetData(queryResponse);
            }
            catch (WISException ex)
            {
                response.SetData(grid.Rows);
                response.SetError(ex.Message);
            }

            return response;
        }
        public IGridWrapper ButtonAction(IGridWrapper wrapper)
        {
            var data = wrapper.GetData<GridButtonActionQuery>();

            IGridWrapper response = new GridWrapper(wrapper);

            var columns = new List<IGridColumn>();

            using (var context = new WISDB())
            {
                columns = this._service.GetColumns(context, wrapper.Application, wrapper.GridId, wrapper.User);
            }

            data.Row.SetCellColumn(columns);

            try
            {
                data = this._controller.GridButtonAction(this._service, data, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public IGridWrapper MenuItemAction(IGridWrapper wrapper)
        {
            var data = wrapper.GetData<GridMenuItemAction>();

            IGridWrapper response = new GridWrapper(wrapper);

            try
            {
                data = this._controller.GridMenuItemAction(this._service, data, wrapper.User);

                response.SetData(data);
            }
            catch (WISException ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
    }
}
