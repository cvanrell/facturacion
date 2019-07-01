using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation
{
    public class ValidationError : IValidationError
    {        
        public string Message { get; set; }

        public ValidationError(string message)
        {
            this.Message = message;
        }
    }
}
