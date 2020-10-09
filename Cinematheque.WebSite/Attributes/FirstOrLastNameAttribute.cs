using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Cinematheque.WebSite.Attributes
{
    public class FirstOrLastNameAttribute : ValidationAttribute, IClientValidatable
    {
        private string RegEx { get; set; }

        public FirstOrLastNameAttribute(string regex) : base("Illegal text for name or surname")
        {
            RegEx = regex;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!Regex.Match((string)value, RegEx, RegexOptions.IgnoreCase).Success)
                {
                    return new ValidationResult(
                    FormatErrorMessage(validationContext.DisplayName)
                    );
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());

            rule.ValidationParameters.Add("regex", RegEx);
            rule.ValidationType = "flname";
            yield return rule;
        }
    }
}