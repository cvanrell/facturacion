using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;

namespace WIS.CommonCore.GridComponents
{
    public class GridItemHeader : GridItem, IGridItem
    {
        public GridItemHeader(string label)
        {
            this.Id = string.Empty;
            this.Label = label;
            this.CssClass = string.Empty;
            this.ItemType = GridItemType.Header;
        }
    }
}
