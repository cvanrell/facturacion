﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation
{
    public interface IValidationError
    {
        string Message { get; set; }
    }
}
