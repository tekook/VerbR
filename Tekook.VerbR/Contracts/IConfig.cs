using System.Collections.Generic;

namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for configuration which can be validated.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Determinates if the configuration is valid.
        /// </summary>
        /// <returns>true if the config is valid.</returns>
        public bool IsValid();

        /// <summary>
        /// Determinates if the configuration is valid.
        /// </summary>
        /// <param name="errors">The resulting errors.</param>
        /// <returns>true if the config is valid.</returns>
        public bool IsValid(out IEnumerable<IValidationError> errors);
    }
}