﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogic.FilterUtil.Enums;

namespace WIS.BusinessLogic.FilterUtil.Tokens
{
    public class FilterTokenBetween : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenBetween()
        {
            this.TokenType = FilterTokenType.BINARY_OP;
        }
    }
}
