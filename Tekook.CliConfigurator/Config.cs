using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

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
        public void FillFromEnv()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                EnvironmentAttribute attr = prop.GetCustomAttribute<EnvironmentAttribute>();
                if (attr != null)
                {
                    string env = Environment.GetEnvironmentVariable(attr.Name);
                    if (env != null)
                    {
                        if (prop.PropertyType != typeof(string))
                        {
                            try
                            {
                                prop.SetValue(this, Activator.CreateInstance(prop.PropertyType, env));
                            }
                            catch (TargetInvocationException e)
                            {
                                throw new ConfigException(prop.Name, attr.Name, e.InnerException);
                            }
                            catch (Exception e)
                            {
                                throw new ConfigException(prop.Name, attr.Name, "Type Problem. See InnerException for Details.", e);
                            }
                        }
                        else
                        {
                            prop.SetValue(this, env);
                        }
                    }
                }
            }
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