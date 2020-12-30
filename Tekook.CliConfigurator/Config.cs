using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Base for all configuration classes.
    /// Extend this class and implement your needed properties.
    /// </summary>
    public abstract class Config
    {
        /// <summary>
        /// Fill this configuration from <see cref="EnvironmentAttribute">env</see>.
        /// </summary>
        [Obsolete(nameof(FillFromEnv) + " is obsolete and will be removed in the next version. Verb contructor automatically fills env if config is null")]
        public void FillFromEnv()
        {
            EnvironmentParser.Parse(this);
        }

        /// <summary>
        /// Checks if this configuration is valid via DataAnnotations.
        /// </summary>
        /// <returns>true if configuration is valid.</returns>
        public bool IsValid()
        {
            return this.IsValid(out _);
        }

        /// <summary>
        /// Checks if this configuration is valid via DataAnnotations.
        /// </summary>
        /// <param name="results">Result of the validation.</param>
        /// <returns>true if configuration is valid.</returns>
        public bool IsValid(out List<ValidationResult> results)
        {
            ValidationContext context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results);
        }

        /// <summary>
        /// Perform validation via <see cref="IsValid(out List{ValidationResult})"/> and throw an <see cref="ConfigException"/> if it is not valid.
        /// </summary>
        public void Validate()
        {
            if (!this.IsValid())
            {
                throw new ConfigException("Please check your enviroment variables, config is not valid!");
            }
        }
    }
}