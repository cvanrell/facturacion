using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil.Enums;

namespace WIS.BusinessLogicCore.FilterUtil.Tokens
{
    public class FilterTokenParenthesisOpen : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenParenthesisOpen()
        {
            this.TokenType = FilterTokenType.PARENTHESIS_OPEN;
        }
    }
}
