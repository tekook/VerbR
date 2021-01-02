using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Scaffold
{
    /// <summary>
    /// Scaffold implementation of <see cref="IConfig"/> using <see cref="System.ComponentModel.DataAnnotations"/> for validation.
    /// </summary>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsValid()
        {
            return this.IsValid(out _);
        }

        /// <inheritdoc/>
        public bool IsValid(out IEnumerable<IValidationError> errors)
        {
            ValidationContext context = new ValidationContext(this);
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, context, results))
            {
                List<IValidationError> errs = new List<IValidationError>();
                foreach (ValidationResult result in results)
                {
                    errs.Add(new ValidationError(result.MemberNames.FirstOrDefault(), new[] { result.ErrorMessage }));
                }
                errors = errs.ToArray();
                return false;
            }
            errors = new IValidationError[] { };
            return true;
        }
    }
}