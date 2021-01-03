using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Validators
{
    /// <summary>
    /// Validator based on <see cref="FluentValidator{T}"/>.
    /// </summary>
    /// <typeparam name="T">The object to validate.</typeparam>
    public class FluentValidator<T> : AbstractValidator<T>, IValidateConfigs where T : class
    {
        /// <summary>
        /// Creates a new FluentValidator.
        /// </summary>
        public FluentValidator()
        {
            this.RuleSetter();
        }

        /// <summary>
        /// Creates a new FluentValidator.
        /// </summary>
        /// <param name="rules">Rules to add to this validator.</param>
        public FluentValidator(Action<FluentValidator<T>> rules) : base()
        {
            rules.Invoke(this);
        }

        /// <inheritdoc/>
        public bool IsValid(object config)
        {
            return this.IsValid(config, out _);
        }

        /// <inheritdoc/>
        public bool IsValid(object config, out IEnumerable<IValidationError> errors)
        {
            ValidationResult result = this.Validate((T)config);
            var errs = new List<ValidationError>();
            foreach (var failure in result.Errors)
            {
                errs.Add(new ValidationError(failure.PropertyName, new string[] { failure.ErrorMessage }));
            }
            errors = errs.ToArray();
            return result.IsValid;
        }

        /// <summary>
        /// Use this method to set your rules for the validator.
        /// </summary>
        protected virtual void RuleSetter()
        {
            this.RuleFor(c => c).NotNull();
        }
    }
}