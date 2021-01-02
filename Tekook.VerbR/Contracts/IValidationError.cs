using System.Collections.Generic;

namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for an validation error.
    /// </summary>
    public interface IValidationError
    {
        /// <summary>
        /// Errors of the property.
        /// </summary>
        IEnumerable<string> Errors { get; }

        /// <summary>
        /// Name of the property
        /// </summary>
        string Property { get; }
    }
}