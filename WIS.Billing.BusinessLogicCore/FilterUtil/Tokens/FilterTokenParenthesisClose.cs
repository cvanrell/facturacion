using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil.Enums;

namespace WIS.BusinessLogicCore.FilterUtil.Tokens
{
    public class FilterTokenParenthesisClose : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenParenthesisClose()
        {
            this.TokenType = FilterTokenType.PARENTHESIS_CLOSE;
        }
    }
}
