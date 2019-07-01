using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public interface IValidationRuleGroup
    {
        bool BreakValidationChain { get; set; }

        IValidationErrorGroup Validate();
    }
}
