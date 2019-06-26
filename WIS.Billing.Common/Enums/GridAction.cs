using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.Common.Enums
{
    public enum GridAction
    {
        Unknown,
        Initialize,
        FetchRows,
        UpdateConfig,
        ValidateRow,
        Commit,
        ButtonAction,
        MenuItemAction
    }
}
