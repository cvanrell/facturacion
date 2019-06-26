using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.GridComponents
{
    public class GridRow
    {
        public string Id { get; set; }
        public string CssClass { get; set; }
        public List<GridCell> Cells { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public GridRow()
        {
            this.Cells = new List<GridCell>();
            this.IsNew = false;
            this.IsDeleted = false;
        }

        public void AddCell(GridCell cell)
        {
            this.Cells.Add(cell);
        }
        public GridCell GetCell(string column)
        {
            return this.Cells.Where(d => d.Column.Id == column).FirstOrDefault();
        }
        public void SetCellColumn(List<IGridColumn> columns)
        {
            foreach (var cell in this.Cells)
            {
                cell.Column = columns.Where(d => d.Id == cell.Column.Id).FirstOrDefault();
            }
        }
        public bool IsValid()
        {
            return this.Cells.Any(c => c.IsValid());
        }
    }
}
