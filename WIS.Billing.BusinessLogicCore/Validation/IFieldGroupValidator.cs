using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public interface IFieldGroupValidator : IFieldValidator
    {
        List<IValidationRuleGroup> RuleGroups { get; }

        IFieldGroupValidator AddRuleGroup(IValidationRuleGroup group);
    }
}
