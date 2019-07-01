using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.FilterUtil.Enums;

namespace WIS.BusinessLogic.FilterUtil.Tokens
{
    public class FilterTokenOr : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenOr()
        {
            this.TokenType = FilterTokenType.BINARY_OR;
        }
    }
}
