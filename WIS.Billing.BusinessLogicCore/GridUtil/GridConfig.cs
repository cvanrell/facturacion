using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.GridUtil.Factories;
using WIS.CommonCore.GridComponents;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.EntitiesCore;

namespace WIS.BusinessLogicCore.GridUtil
{
    public class GridConfig
    {
        private readonly WISDB Context;
        private readonly string Application;
        private readonly string GridId;
        private readonly int User;

        public GridConfig(WISDB context, string application, string gridId, int userId)
        {
            this.Context = context;
            this.Application = application;
            this.GridId = gridId;
            this.User = userId;
        }

        public void Update(List<GridColumn> columns)
        {
            var configLines = this.Context.T_GRID_DEFAULT_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == this.GridId).ToList();
            var userConfigLines = this.Context.T_GRID_USER_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == this.GridId && d.USERID == this.User).ToList();

            foreach(var column in columns)
            {
                var userConfig = userConfigLines.Where(d => d.NM_DATAFIELD == column.Id).FirstOrDefault();

                if (userConfig == null)
                {
                    var defaultConfig = configLines.Where(d => d.NM_DATAFIELD == column.Id).FirstOrDefault();

                    if (defaultConfig == null)
                        continue;

                    userConfig = this.MapFromDefaultConfig(defaultConfig);

                    userConfig.USERID = this.User;

                    this.Context.T_GRID_USER_CONFIG.Add(userConfig);
                }

                this.UpdateProperties(userConfig, column);
            }

            this.Context.SaveChanges();
        }
        public List<IGridColumn> GetColumns(GridColumnFactory columnFactory)
        {
            var userConfig = this.Context.T_GRID_USER_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == this.GridId && d.USERID == this.User).OrderBy(d => d.NU_ORDEN_VISUAL).ToList();

            if(userConfig.Count > 0)
                return this.MapToUserColumns(columnFactory, userConfig);

            var defaultConfig = this.Context.T_GRID_DEFAULT_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == this.GridId).OrderBy(d => d.NU_ORDEN_VISUAL).ToList();

            return this.MapToDefaultColumns(columnFactory, defaultConfig);
        }
        public void Reset()
        {
            var userConfigLines = this.Context.T_GRID_USER_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == this.GridId && d.USERID == this.User).ToList();

            foreach(var line in userConfigLines)
            {
                this.Context.T_GRID_USER_CONFIG.Remove(line);
            }
        }
        
        private List<IGridColumn> MapToUserColumns(GridColumnFactory columnFactory, List<T_GRID_USER_CONFIG> userConfig)
        {
            List<IGridColumn> colList = new List<IGridColumn>();

            foreach (var col in userConfig)
            {
                IGridColumn column = columnFactory.Create(col.GetColumnType());

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

            return colList;
        }
        private List<IGridColumn> MapToDefaultColumns(GridColumnFactory columnFactory, List<T_GRID_DEFAULT_CONFIG> defaultConfig)
        {
            List<IGridColumn> colList = new List<IGridColumn>();

            foreach (var col in defaultConfig)
            {
                IGridColumn column = columnFactory.Create(col.GetColumnType());

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

            return colList;
        }

        private T_GRID_USER_CONFIG MapFromDefaultConfig(T_GRID_DEFAULT_CONFIG defaultConfig)
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
        private void UpdateProperties(T_GRID_USER_CONFIG config, GridColumn column)
        {
            config.SetTextAlign(column.TextAlign);
            config.SetFixPosition(column.Fixed);
            config.SetVisible(!column.Hidden);
            config.NU_ORDEN_VISUAL = column.Order;
            config.VL_WIDTH = column.Width;
        }
    }
}
