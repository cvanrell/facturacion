using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public class FieldValidatorOutputResolver
    {
        private readonly IValidationErrorGroup _group;

        public FieldValidatorOutputResolver(IValidationErrorGroup group)
        {
            this._group = group;
        }

        public string Resolve()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var error in _group.Errors)
            {
                sb.Append(error.Message);
            }

            return sb.ToString();
        }
    }
}
