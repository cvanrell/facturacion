using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogic.Validation
{
    public class FieldGroupValidator : IFieldValidator, IFieldGroupValidator
    {
        public List<IValidationRuleGroup> RuleGroups { get; private set; }

        public FieldGroupValidator()
        {
            this.RuleGroups = new List<IValidationRuleGroup>();
        }

        public IFieldGroupValidator AddRuleGroup(IValidationRuleGroup group)
        {
            this.RuleGroups.Add(group);

            return this;
        }

        public IFieldValidationResult Validate()
        {
            IFieldValidationResult result = new FieldValidationResult();

            foreach (var group in this.RuleGroups)
            {
                IValidationErrorGroup validationGroup = group.Validate();

                if (!validationGroup.IsValid)
                {
                    result.AddGroup(validationGroup);

                    if (group.BreakValidationChain)
                        break;
                }
            }

            return result;
        }
    }
}
