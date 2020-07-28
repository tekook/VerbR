using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Tekook.CliConfigurator
{
    public abstract class Config
    {
        private static readonly NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();

        public void FillFromEnv()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                EnvSetterAttribute attr = prop.GetCustomAttribute<EnvSetterAttribute>();
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

        public bool IsValid()
        {
            return this.IsValid(out _);
        }

        public bool IsValid(out List<ValidationResult> results)
        {
            ValidationContext context = new ValidationContext(this);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results);
        }

        public void Validate()
        {
            if (!this.IsValid(out List<ValidationResult> results))
            {
                foreach (ValidationResult result in results)
                {
                    string propName = result.MemberNames.First();
                    Type t = this.GetType();
                    EnvSetterAttribute attr = t.GetProperty(propName).GetCustomAttribute<EnvSetterAttribute>();
                    if (attr != null)
                    {
                        log.Error("ENV: {env} -> {message}", attr.Name, result.ErrorMessage);
                    }
                }
                throw new ConfigException("Please check your enviroment variables, config is not valid!");
            }
        }
    }
}