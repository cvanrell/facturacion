using WIS.CommonCore.GridComponents;
using WIS.CommonCore.Constants;
using WIS.Billing.DataAccessCore.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.GridUtil.Factories;
using WIS.BusinessLogicCore.SortUtil;
using WIS.CommonCore.SortComponents;
using WIS.BusinessLogicCore.FilterUtil;
using WIS.BusinessLogicCore.DataModel.Queries;

namespace WIS.BusinessLogicCore.GridUtil.Services
{
    public class GridService : IGridService
    {
        private readonly IFilter _filter;
        private readonly ISorter _sorter;

        public GridService(IFilter filterService, ISorter sortingService)
        {
            this._filter = filterService;
            this._sorter = sortingService;
        }

        public List<IGridColumn> GetColumns(WISDB context, string application, string gridId, int userId)
        {
            var config = new GridConfig(context, application, gridId, userId);

            var columnFactory = new GridColumnFactory();

            return config.GetColumns(columnFactory);
        }
        public List<IGridColumn> GetColumnsFromEntity<T>()
        {
            List<IGridColumn> columns = new List<IGridColumn>();

            var type = typeof(T);

            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                columns.Add(new GridColumn
                {
                    Id = prop.Name,
                    Name = prop.Name,
                    IsNew = true
                });
            }

            return columns;
        }
        public List<GridRow> GetRows<T>(IQueryable<T> query, List<IGridColumn> columns, GridFetchRequest queryParameters, SortCommand defaultSorting, List<string> keys)
        {
            List<SortCommand> sorting = new List<SortCommand> { defaultSorting };

            return this.GetRows<T>(query, columns, queryParameters, sorting, keys);
        }
        public List<GridRow> GetRows<T>(IQueryable<T> query, List<IGridColumn> columns, GridFetchRequest queryParameters, List<SortCommand> defaultSorting, List<string> keys)
        {
            if(columns == null || columns.Where(d => !d.IsNew).Count() == 0)
                columns.AddRange(this.GetColumnsFromEntity<T>());
                        
            query = _filter.ApplyFilter(query, queryParameters.Filters);
            query = _filter.ApplyFilter(query, queryParameters.ExplicitFilter);

            var sorts = queryParameters.Sorts;

            if (!sorts.Any())
                sorts = defaultSorting;

            query = _sorter.ApplySorting(query, sorts);

            /*
             * TODO: descomentar y aplicar
            //- Aplica filtro empresa
            query = FilterExtensions.FilterUsuarioEmpresas<T>(ctxwisdb, grid.page, int.Parse(Encrypter.Desencriptar(grid.id_user)), query, empNotInclude);

            //- Aplica filtro Grupo consulta
            query = FilterExtensions.FilterGrupoConsultaUser<T>(ctxseg, int.Parse(Encrypter.Desencriptar(grid.id_user)), query);*/
            
            //Agregar paginado
            List<T> data = query.Skip(queryParameters.RowsToSkip).Take(queryParameters.RowsToFetch).ToList();

            //Reiniciar filas

            return this.BuildRows(data, columns, keys);
        }
        public List<GridRow> GetRows<T>(IQueryObject<T> queryObject, List<IGridColumn> columns, GridFetchRequest queryParameters, SortCommand defaultSorting, List<string> keys)
        {
            return this.GetRows(queryObject.GetQuery(), columns, queryParameters, defaultSorting, keys);
        }
        public List<GridRow> GetRows<T>(IQueryObject<T> queryObject, List<IGridColumn> columns, GridFetchRequest queryParameters, List<SortCommand> defaultSorting, List<string> keys)
        {
            return this.GetRows(queryObject.GetQuery(), columns, queryParameters, defaultSorting, keys);
        }

        public void SaveAddedColumns(WISDB context, Grid grid, string application)
        {
            foreach (var column in grid.Columns.Where(d => d.IsNew))
            {
                var newColumn = new T_GRID_DEFAULT_CONFIG
                {
                    NM_DATAFIELD = column.Id,
                    CD_APLICACION = application,
                    CD_BLOQUE = grid.Id,
                    DS_COLUMNA = column.Name,
                    VL_WIDTH = (int)column.Width
                };

                newColumn.SetVisible(!column.Hidden);
                newColumn.SetFixPosition(column.Fixed);
                newColumn.SetColumnType(column.Type);
                newColumn.SetTextAlign(column.TextAlign);

                context.T_GRID_DEFAULT_CONFIG.Add(newColumn);
            }
        }

        private List<GridRow> BuildRows<T>(List<T> data, List<IGridColumn> columns, List<string> keys)
        {
            var rows = new List<GridRow>();

            foreach (var item in data)
            {
                var newRow = new GridRow();

                var rowIdList = new List<string>();

                var properties = item.GetType().GetProperties();

                var itemType = item.GetType();

                foreach (var col in columns)
                {
                    var prop = itemType.GetProperty(col.Id, BindingFlags.Public | BindingFlags.Instance);

                    var value = string.Empty;

                    if (prop != null)
                    {
                        if(prop.PropertyType == typeof(DateTime))
                            value = ((DateTime)prop.GetValue(item)).ToString("o");
                        else if (Nullable.GetUnderlyingType(prop.PropertyType) == typeof(DateTime))
                            value = ((DateTime?)prop.GetValue(item))?.ToString("o");
                        else
                            value = Convert.ToString(prop.GetValue(item));

                        if (keys.Any(k => k == prop.Name))
                            rowIdList.Add(value);
                    }

                    newRow.Cells.Add(new GridCell
                    {
                        Column = col,
                        Value = value,
                        Old = value
                    });
                }

                newRow.Id = string.Join(CSystem.IdSeparator, rowIdList);

                rows.Add(newRow);
            }

            return rows;
        }        
        private void MapDefaultColumn(List<T_GRID_DEFAULT_CONFIG> dbColumns)
        {
            List<IGridColumn> colList = new List<IGridColumn>();

            foreach (var col in dbColumns)
            {
                var factory = new GridColumnFactory();

                IGridColumn column = factory.Create(col.GetColumnType());

                column.Name = col.DS_COLUMNA;
                column.Id = col.NM_DATAFIELD;
                column.Hidden = !col.IsVisible();
                column.Insertable = true;
                column.Fixed = col.GetFixPosition();
                column.Width = col.GetWidth();
                column.Type = col.GetColumnType();
                column.TextAlign = col.GetTextAlign();
                column.Order = col.GetOrder();

                colList.Add(column);
            }
        }
        private void MapUserColumn()
        {

        }
    }
}
