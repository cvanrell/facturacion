using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.GridComponents
{
    public class GridItemDivider : GridItem, IGridItem
    {
        public GridItemDivider()
        {
            this.Id = string.Empty;
            this.Label = string.Empty;
            this.CssClass = string.Empty;
            this.ItemType = GridItemType.Divider;
        }        
    }
}
