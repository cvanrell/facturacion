using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.CommonCore.GridComponents
{
    public class GridColumnItemList : GridColumn
    {
        public List<IGridItem> Items { get; set; }

        public GridColumnItemList()
        {
            this.Type = Enums.ColumnType.ItemList;
            this.Items = new List<IGridItem>();
        }

        public GridColumnItemList(string id, List<IGridItem> items)
        {
            this.Id = id;
            this.Type = Enums.ColumnType.ItemList;
            this.Items = items;
        }

        public override void UpdateSpecificValues(IGridColumn column)
        {
            var castedColumn = (GridColumnItemList)column;

            this.Items = castedColumn.Items;
        }
    }
}
