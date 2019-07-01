using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil.Enums;

namespace WIS.BusinessLogicCore.FilterUtil.Tokens
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
