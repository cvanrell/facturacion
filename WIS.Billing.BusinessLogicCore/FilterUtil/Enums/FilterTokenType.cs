using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.FilterUtil.Enums
{
    public enum FilterTokenType
    {
        PARENTHESIS_OPEN,
        PARENTHESIS_CLOSE,
        BINARY_AND,
        BINARY_OR,
        BINARY_EQUAL,
        BINARY_GREATER_THAN,
        BINARY_GREATER_THAN_OR_EQUAL,
        BINARY_LESS_THAN,
        BINARY_LESS_THAN_OR_EQUAL,
        UNARY_NOT,        
        UNARY_OP,
        BINARY_OP,
        LITERAL
    }
}
