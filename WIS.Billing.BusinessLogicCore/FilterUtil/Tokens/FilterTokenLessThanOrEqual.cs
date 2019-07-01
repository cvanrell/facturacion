using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil.Enums;

namespace WIS.BusinessLogicCore.FilterUtil.Tokens
{
    public class FilterTokenLessThanOrEqual : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenLessThanOrEqual()
        {
            this.TokenType = FilterTokenType.BINARY_LESS_THAN_OR_EQUAL;
        }
    }
}
