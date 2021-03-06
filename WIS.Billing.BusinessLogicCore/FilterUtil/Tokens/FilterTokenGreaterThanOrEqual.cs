﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.BusinessLogicCore.FilterUtil.Enums;

namespace WIS.BusinessLogicCore.FilterUtil.Tokens
{
    public class FilterTokenGreaterThanOrEqual : IFilterToken
    {
        public FilterTokenType TokenType { get; set; }

        public FilterTokenGreaterThanOrEqual()
        {
            this.TokenType = FilterTokenType.BINARY_GREATER_THAN_OR_EQUAL;
        }
    }
}
