using System.Collections.Generic;

namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for an validation error.
    /// </summary>
    public class IValidationError
    {
        /// <summary>
        /// Errors of the property.
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Name of the property
        /// </summary>
        public string Property { get; }
    }
}