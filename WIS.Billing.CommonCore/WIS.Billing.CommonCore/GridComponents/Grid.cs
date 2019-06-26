using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.GridComponents
{
    public class Grid
    {
        public string Id { get; set; }
        public List<IGridColumn> Columns { get; set; }
        public List<GridRow> Rows { get; set; }
        public List<IGridItem> MenuItems { get; set; }

        public Grid()
        {
            this.Columns = new List<IGridColumn>();
            this.Rows = new List<GridRow>();
            this.MenuItems = new List<IGridItem>();
        }

        public Grid(string id)
        {
            this.Id = id;
            this.Columns = new List<IGridColumn>();
            this.Rows = new List<GridRow>();
            this.MenuItems = new List<IGridItem>();
        }

        public void AddOrUpdateColumn(IGridColumn column)
        {
            var existingColumn = this.Columns.Where(d => d.Id == column.Id).FirstOrDefault();

            if (existingColumn != null)
            {
                existingColumn.UpdateSpecificValues(column);
            }
            else
            {
                column.IsNew = true;

                this.Columns.Add(column);
            }               
        }
        public void SetColumnDefaultValues(Dictionary<string, string> values)
        {
            foreach(var column in this.Columns)
            {
                if (values.ContainsKey(column.Id))
                    column.DefaultValue = values[column.Id];
            }
        }
    }
}
