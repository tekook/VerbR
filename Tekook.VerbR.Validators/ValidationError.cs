using System;
using System.Collections.Generic;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Validators
{
    /// <summary>
    /// Default implementation for <see cref="IValidationError"/>.
    /// </summary>
    public class ValidationError : IValidationError
    {
        /// <inheritdoc/>
        public IEnumerable<string> Errors { get; }

        /// <inheritdoc/>
        public string Property { get; }

        /// <summary>
        /// Create a new instance of <see cref="ValidationError"/>.
        /// </summary>
        /// <param name="property">The property which caused the error.</param>
        /// <param name="errors">The errors which occurred.</param>
        public ValidationError(string property, IEnumerable<string> errors)
        {
            this.Property = property ?? throw new ArgumentNullException(nameof(property));
            this.Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }
    }
}