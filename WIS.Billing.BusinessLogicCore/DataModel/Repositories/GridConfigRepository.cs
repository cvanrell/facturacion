using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.Billing.DataAccessCore.Database;
using WIS.Billing.BusinessLogicCore.DataModel.Mappers;
using WIS.BusinessLogicCore.FilterUtil;
using WIS.BusinessLogicCore.GridUtil.Factories;
using WIS.CommonCore.Enums;
using WIS.CommonCore.GridComponents;
using WIS.CommonCore.SortComponents;
using WIS.Billing.BusinessLogic.DataModel.Mappers;
using WIS.Billing.EntitiesCore;
using Microsoft.EntityFrameworkCore;

namespace WIS.Billing.BusinessLogicCore.DataModel.Repositories
{
    public class GridConfigRepository
    {
        private readonly WISDB _context;
        private readonly string Application;
        private readonly int User;
        private readonly GridConfigMapper _mapper;

        public GridConfigRepository(WISDB context, string application, int user)
        {
            this._context = context;
            this.Application = application;
            this.User = user;
            this._mapper = new GridConfigMapper();
        }

        public void AddDefaultConfig(string gridId, IGridColumn column)
        {
            this._context.T_GRID_DEFAULT_CONFIG.Add(this._mapper.MapToDefaultEntity(gridId, this.Application, column));
        }
        public void AddUserConfig(string gridId, IGridColumn column)
        {
            this._context.T_GRID_USER_CONFIG.Add(this._mapper.MapToUserEntity(gridId, this.Application, column));
        }
        //public void SaveFilter(GridFilterData data)
        //{
        //    T_GRID_FILTER filter = this._mapper.MapToFilterEntity(data, this.Application, this.User);

        //    filter.CD_FILTRO = this._context.GetNextSequenceValue<long>("S_GRID_FILTER");

        //    this._context.T_GRID_FILTER.Add(filter);
        //}
        public void RemoveFilter(long filterId)
        {
            var filter = this._context.T_GRID_FILTER.Include("T_GRID_FILTER_DET").Where(d => (d.USERID == this.User || d.FL_GLOBAL == "S") && d.CD_FILTRO == filterId).FirstOrDefault();

            if (filter == null)
                return;

            if(filter.T_GRID_FILTER_DET.Count > 0)
                this._context.T_GRID_FILTER_DET.RemoveRange(filter.T_GRID_FILTER_DET);

            this._context.T_GRID_FILTER.Remove(filter);
        }
        public List<GridFilterData> GetFilterList(string gridId)
        {
            List<T_GRID_FILTER> filters = this._context.T_GRID_FILTER.Include("T_GRID_FILTER_DET").AsNoTracking()
                .Where(d => (d.USERID == this.User || d.FL_GLOBAL == "S") && d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId)
                .OrderByDescending(d => d.FL_GLOBAL).ThenBy(d => d.NM_FILTRO).ToList();

            List<GridFilterData> filterData = new List<GridFilterData>();

            foreach(var filter in filters)
            {
                filterData.Add(this._mapper.MapToFilterObject(filter));
            }

            return filterData;
        }
        public GridFilterData GetDefaultFilter(string gridId)
        {
            T_GRID_FILTER filter = this._context.T_GRID_FILTER.Include("T_GRID_FILTER_DET").AsNoTracking()
                .Where(d => (d.USERID == this.User || d.FL_GLOBAL == "S") && d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId && d.FL_INICIAL == "S")
                .FirstOrDefault();

            return filter != null ? this._mapper.MapToFilterObject(filter) : null;
        }
        public void Update(string gridId, List<GridColumn> columns)
        {
            var configLines = this._context.T_GRID_DEFAULT_CONFIG.AsNoTracking().Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId).ToList();
            var userConfigLines = this._context.T_GRID_USER_CONFIG.AsNoTracking().Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId && d.USERID == this.User).ToList();
            
            foreach (var column in columns)
            {
                var userConfig = userConfigLines.Where(d => d.NM_DATAFIELD == column.Id).FirstOrDefault();

                if (userConfig == null)
                {
                    var defaultConfig = configLines.Where(d => d.NM_DATAFIELD == column.Id).FirstOrDefault();

                    if (defaultConfig == null)
                        continue;

                    this.AddUserConfig(gridId, column);
                }
                else
                {
                    T_GRID_USER_CONFIG config = this._mapper.MapToUserEntity(gridId, this.Application, column);

                    this._context.T_GRID_USER_CONFIG.Attach(config);

                    //this._context.Entry(config).State = System.Data.Entity.EntityState.Modified;
                }
            }
        }
        public List<IGridColumn> GetColumns(string gridId, GridColumnFactory columnFactory)
        {
            var userConfig = this._context.T_GRID_USER_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId && d.USERID == this.User).OrderBy(d => d.NU_ORDEN_VISUAL).ToList();

            if (userConfig.Count > 0)
                return this._mapper.MapToUserColumns(columnFactory, userConfig);

            var defaultConfig = this._context.T_GRID_DEFAULT_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId).OrderBy(d => d.NU_ORDEN_VISUAL).ToList();

            return this._mapper.MapToDefaultColumns(columnFactory, defaultConfig);
        }
        public void Reset(string gridId)
        {
            var userConfigLines = this._context.T_GRID_USER_CONFIG.Where(d => d.CD_APLICACION == this.Application && d.CD_BLOQUE == gridId && d.USERID == this.User).ToList();

            foreach (var line in userConfigLines)
            {
                this._context.T_GRID_USER_CONFIG.Remove(line);
            }
        }
    }
}
