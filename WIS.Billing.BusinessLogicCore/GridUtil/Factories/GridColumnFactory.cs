using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.Enums;
using WIS.CommonCore.GridComponents;

namespace WIS.BusinessLogicCore.GridUtil.Factories
{
    public class GridColumnFactory
    {
        public IGridColumn Create(ColumnType type)
        {
            switch (type)
            {
                case ColumnType.Text:
                    return new GridColumnText();
                case ColumnType.Button:
                    return new GridColumnButton();
                case ColumnType.ItemList:
                    return new GridColumnItemList();
                case ColumnType.Progress:
                    return new GridColumnProgress();
            }

            return new GridColumn();
        }
    }
}
