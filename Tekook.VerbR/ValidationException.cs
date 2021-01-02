using System;
using System.Collections.Generic;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR
{
    /// <summary>
    /// Exception for validation.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// The errors which occurred while validating.
        /// </summary>
        public IEnumerable<IValidationError> ValidationError { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="validationError">The errors which occurred while validating.</param>
        public ValidationException(IEnumerable<IValidationError> validationError)
        {
            this.ValidationError = validationError ?? throw new ArgumentNullException(nameof(validationError));
        }
    }
}