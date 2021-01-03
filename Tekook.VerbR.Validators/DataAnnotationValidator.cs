using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Validators
{
    /// <summary>
    /// Validator based von <see cref="System.ComponentModel.DataAnnotations.Validator"/>.
    /// </summary>
    /// <typeparam name="TConfig">The configuration to validate.</typeparam>
    public class DataAnnotationValidator : IValidateConfigs
    {
        /// <inheritdoc/>
        public bool IsValid(object config)
        {
            return this.IsValid(config, out _);
        }

        /// <inheritdoc/>
        public bool IsValid(object config, out IEnumerable<IValidationError> errors)
        {
            ValidationContext context = new ValidationContext(config);
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(config, context, results))
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