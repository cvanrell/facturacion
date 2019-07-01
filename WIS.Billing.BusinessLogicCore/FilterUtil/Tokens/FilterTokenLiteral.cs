using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.FilterUtil.Enums;

namespace WIS.BusinessLogic.FilterUtil.Tokens
{
    public class FilterTokenLiteral : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }
        public string Value { get; set; }

        public FilterTokenLiteral(string value)
        {
            this.Value = value;
            this.TokenType = FilterTokenType.LITERAL;
        }
    }
}
