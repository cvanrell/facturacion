using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIS.BusinessLogicCore.Validation
{
    public class ValidationRuleGroup : IValidationRuleGroup
    {
        public string FieldName { get; set; }
        public List<IValidationRule> Rules { get; set; }
        public bool BreakValidationChain { get; set; }

        public ValidationRuleGroup()
        {
            this.Rules = new List<IValidationRule>();
            this.BreakValidationChain = false;
        }

        public ValidationRuleGroup(string fieldName, List<IValidationRule> rules)
        {
            this.FieldName = fieldName;
            this.Rules = rules;
            this.BreakValidationChain = false;
        }

        public ValidationRuleGroup(string fieldName, List<IValidationRule> rules, bool breakChain)
        {
            this.FieldName = fieldName;
            this.Rules = rules;
            this.BreakValidationChain = breakChain;
        }

        public IValidationErrorGroup Validate()
        {
            IValidationErrorGroup validationGroup = new ValidationErrorGroup(this.FieldName);

            foreach (var rule in this.Rules)
            {
                var result = rule.Validate();

                if (result.Count > 0)
                {
                    //Rompe la cadena de validación si una de las validaciones falla
                    validationGroup.AddErrors(result);
                    break;
                }
            }

            return validationGroup;
        }
    }
}
