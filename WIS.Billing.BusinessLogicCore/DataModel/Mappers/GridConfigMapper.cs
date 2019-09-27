using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.Billing.EntitiesCore;
using WIS.BusinessLogicCore.GridUtil.Factories;
using WIS.CommonCore.Enums;
using WIS.CommonCore.FilterComponents;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.SortComponents;

namespace WIS.Billing.BusinessLogic.DataModel.Mappers
{
    public class GridConfigMapper : Mapper
    {
        public List<IGridColumn> MapToUserColumns(GridColumnFactory columnFactory, List<T_GRID_USER_CONFIG> userConfig)
        {
            List<IGridColumn> colList = new List<IGridColumn>();

            foreach (var col in userConfig)
            {
                ColumnType columnType = this.MapColumnType(col.VL_TYPE);

                IGridColumn column = columnFactory.Create(columnType);

                column.Name = col.DS_COLUMNA;
                column.Id = col.NM_DATAFIELD;
                column.Hidden = !this.MapStringToBoolean(col.FL_VISIBLE);
                column.Insertable = true;
                column.Fixed = this.MapFixedPosition(col.VL_POSICION_FIJADO);
                column.Width = col.VL_WIDTH ?? 100;
                column.Type = columnType;
                column.TextAlign = this.MapTextAlign(col.VL_ALINEACION);
                column.Order = col.NU_ORDEN_VISUAL ?? 0;

                colList.Add(column);
            }

            return colList;
        }
        public List<IGridColumn> MapToDefaultColumns(GridColumnFactory columnFactory, List<T_GRID_DEFAULT_CONFIG> defaultConfig)
        {
            List<IGridColumn> colList = new List<IGridColumn>();

            foreach (var col in defaultConfig)
            {
                ColumnType columnType = this.MapColumnType(col.VL_TYPE);

                IGridColumn column = columnFactory.Create(columnType);

                column.Name = col.DS_COLUMNA;
                column.Id = col.NM_DATAFIELD;
                column.Hidden = !this.MapStringToBoolean(col.FL_VISIBLE);
                column.Insertable = true;
                column.Fixed = this.MapFixedPosition(col.VL_POSICION_FIJADO);
                column.Width = col.VL_WIDTH ?? 100;
                column.Type = columnType;
                column.TextAlign = this.MapTextAlign(col.VL_ALINEACION);
                column.Order = col.NU_ORDEN_VISUAL ?? 0;

                colList.Add(column);
            }

            return colList;
        }

        public T_GRID_USER_CONFIG MapToUserEntity(string gridId, string application, IGridColumn column)
        {
            return new T_GRID_USER_CONFIG
            {
                NM_DATAFIELD = column.Id,
                CD_APLICACION = application,
                CD_BLOQUE = gridId,
                DS_COLUMNA = column.Name,
                VL_WIDTH = column.Width,
                NU_ORDEN_VISUAL = column.Order,
                FL_VISIBLE = this.MapBooleanToString(!column.Hidden),
                VL_POSICION_FIJADO = (short)column.Fixed,
                VL_TYPE = this.MapColumnType(column.Type),
                VL_ALINEACION = this.MapTextAlign(column.TextAlign)
            };
        }
        public T_GRID_DEFAULT_CONFIG MapToDefaultEntity(string gridId, string application, IGridColumn column)
        {
            return new T_GRID_DEFAULT_CONFIG
            {
                NM_DATAFIELD = column.Id,
                CD_APLICACION = application,
                CD_BLOQUE = gridId,
                DS_COLUMNA = column.Name,
                VL_WIDTH = column.Width,
                NU_ORDEN_VISUAL = column.Order,
                FL_VISIBLE = this.MapBooleanToString(!column.Hidden),
                VL_POSICION_FIJADO = (short)column.Fixed,
                VL_TYPE = this.MapColumnType(column.Type),
                VL_ALINEACION = this.MapTextAlign(column.TextAlign)
            };
        }

        public T_GRID_FILTER MapToFilterEntity(GridFilterData data, string application, int userId)
        {
            var newFilter = new T_GRID_FILTER
            {
                CD_APLICACION = application,
                USERID = userId,
                CD_BLOQUE = data.GridId,
                DS_FILTRO = data.Description,
                NM_FILTRO = data.Name,
                VL_FILTRO_AVANZADO = data.ExplicitFilter,
                DT_ADDROW = DateTime.Now,
                FL_GLOBAL = this.MapBooleanToString(data.IsGlobal),
                FL_INICIAL = this.MapBooleanToString(data.IsDefault)
            };

            foreach (var filter in data.Filters)
            {
                var filterDetail = new T_GRID_FILTER_DET
                {
                    CD_COLUMNA = filter.ColumnId,
                    VL_FILTRO = filter.Value
                };

                newFilter.T_GRID_FILTER_DET.Add(filterDetail);
            }

            int index = 1;

            foreach (var sort in data.Sorts)
            {
                var detail = newFilter.T_GRID_FILTER_DET.Where(d => d.CD_COLUMNA == sort.ColumnId).FirstOrDefault();

                if (detail == null)
                {
                    detail = new T_GRID_FILTER_DET
                    {
                        CD_FILTRO = newFilter.CD_FILTRO,
                        CD_COLUMNA = sort.ColumnId,
                    };

                    newFilter.T_GRID_FILTER_DET.Add(detail);
                }

                detail.VL_ORDEN = (short)sort.Direction;
                detail.NU_ORDEN_EJECUCION = index;

                index++;
            }

            return newFilter;
        }
        public GridFilterData MapToFilterObject(T_GRID_FILTER data)
        {
            var filterData = new GridFilterData
            {
                Id = data.CD_FILTRO,
                Name = data.NM_FILTRO,
                Description = data.DS_FILTRO,
                Date = data.DT_ADDROW,
                ExplicitFilter = data.VL_FILTRO_AVANZADO,
                GridId = data.CD_BLOQUE,
                IsDefault = this.MapStringToBoolean(data.FL_INICIAL),
                IsGlobal = this.MapStringToBoolean(data.FL_GLOBAL)
            };

            if(data.T_GRID_FILTER_DET != null)
            {
                foreach(var detalle in data.T_GRID_FILTER_DET.Where(d => d.VL_FILTRO != null))
                {
                    filterData.Filters.Add(new FilterCommand(detalle.CD_COLUMNA, detalle.VL_FILTRO));
                }

                foreach(var detalle in data.T_GRID_FILTER_DET.Where(d => d.VL_ORDEN != null).OrderBy(d => d.NU_ORDEN_EJECUCION))
                {
                    filterData.Sorts.Add(new SortCommand(detalle.CD_COLUMNA, (SortDirection)detalle.VL_ORDEN));
                }
            }

            return filterData;
        }

        public T_GRID_USER_CONFIG MapFromDefaultConfig(T_GRID_DEFAULT_CONFIG defaultConfig)
        {
            return new T_GRID_USER_CONFIG
            {
                CD_APLICACION = defaultConfig.CD_APLICACION,
                CD_BLOQUE = defaultConfig.CD_BLOQUE,
                DS_COLUMNA = defaultConfig.DS_COLUMNA,
                DS_DATA_FORMAT_STRING = defaultConfig.DS_DATA_FORMAT_STRING,
                VL_POSICION_FIJADO = defaultConfig.VL_POSICION_FIJADO,
                FL_VISIBLE = defaultConfig.FL_VISIBLE,
                NM_DATAFIELD = defaultConfig.NM_DATAFIELD,
                NU_ORDEN_VISUAL = defaultConfig.NU_ORDEN_VISUAL,
                RESOURCEID = defaultConfig.RESOURCEID,
                VL_ALINEACION = defaultConfig.VL_ALINEACION,
                VL_LINK = defaultConfig.VL_LINK,
                VL_TYPE = defaultConfig.VL_TYPE,
                VL_WIDTH = defaultConfig.VL_WIDTH
            };
        }

        private string MapColumnType(ColumnType type)
        {
            switch (type)
            {
                case ColumnType.Text:
                    return "ST";
                case ColumnType.DateTime:
                    return "DT";
                case ColumnType.Date:
                    return "DO";
                case ColumnType.Checkbox:
                    return "CK";
                case ColumnType.Button:
                    return "BA";
                case ColumnType.ItemList:
                    return "BL";
                case ColumnType.Progress:
                    return "PG";
                case ColumnType.Select:
                    return "SL";
                case ColumnType.SelectAsync:
                    return "SA";                    
            }

            return "ST";
        }
        private ColumnType MapColumnType(string columnType)
        {
            if (columnType == null)
                throw new Exception("Tipo de columna no definido");

            switch (columnType)
            {
                case "ST":
                    return ColumnType.Text;
                case "CK":
                    return ColumnType.Checkbox;
                case "DT":
                    return ColumnType.DateTime;
                case "DO":
                    return ColumnType.Date;
                case "BA":
                    return ColumnType.Button;
                case "BL":
                    return ColumnType.ItemList;
                case "PG":
                    return ColumnType.Progress;
                case "SL":
                    return ColumnType.Select;
                case "SA":
                    return ColumnType.SelectAsync;
            }

            return ColumnType.Text;
        }
        private string MapTextAlign(GridTextAlign textAlign)
        {
            string dbAlign = null;

            switch (textAlign)
            {
                case GridTextAlign.Right:
                    dbAlign = "D";
                    break;
                case GridTextAlign.Left:
                    dbAlign = "I";
                    break;
                case GridTextAlign.Center:
                    dbAlign = "C";
                    break;
            }

            return dbAlign;
        }
        private GridTextAlign MapTextAlign(string textAlign)
        {
            if (string.IsNullOrEmpty(textAlign))
                return GridTextAlign.Left;

            switch (textAlign)
            {
                case "D":
                    return GridTextAlign.Right;
                case "I":
                    return GridTextAlign.Left;
                case "C":
                    return GridTextAlign.Center;
            }

            return GridTextAlign.Left;
        }
        private FixPosition MapFixedPosition(short? fixedPosition)
        {
            return (FixPosition)(fixedPosition ?? 1);
        }        
    }
}
