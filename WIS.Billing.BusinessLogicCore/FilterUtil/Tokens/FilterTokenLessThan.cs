using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.FilterUtil.Enums;

namespace WIS.BusinessLogic.FilterUtil.Tokens
{
    public class FilterTokenLessThan : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenLessThan()
        {
            this.TokenType = FilterTokenType.BINARY_LESS_THAN;
        }
    }
}
