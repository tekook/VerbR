using System;
using System.Reflection;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Parser for <see cref="EnvironmentAttribute"/> and <see cref="EnvironmentObjectAttribute"/>.
    /// </summary>
    internal static class EnvironmentParser
    {
        /// <summary>
        /// Parse an object with the given prefix.
        /// </summary>
        /// <param name="instance">Object to fill.</param>
        /// <param name="prefix">Prefix to add to all env variables.</param>
        public static void Parse(object instance, string prefix = "")
        {
            Type type = instance.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attributes = prop.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is EnvironmentAttribute envAttr)
                    {
                        HandleAttribute(instance, prop, envAttr, prefix);
                    }
                    else if (attribute is EnvironmentObjectAttribute envObjAttr)
                    {
                        HandleObjectAttribute(instance, prop, envObjAttr, prefix);
                    }
                }
            }
        }

        /// <summary>
        /// Parse an attribute of the instance.
        /// </summary>
        /// <param name="instance">The object being parsed.</param>
        /// <param name="prop">The property which should be parsed.</param>
        /// <param name="attr">The attribute of the property.</param>
        /// <param name="prefix">The prefix to use.</param>
        /// <exception cref="ConfigException">Thrown if TargetInvocation fails while setting the property.</exception>
        private static void HandleAttribute(object instance, PropertyInfo prop, EnvironmentAttribute attr, string prefix)
        {
            string env = Environment.GetEnvironmentVariable(prefix + attr.Name);
            if (env != null)
            {
                if (prop.PropertyType != typeof(string))
                {
                    try
                    {
                        prop.SetValue(instance, Activator.CreateInstance(prop.PropertyType, env));
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
                    prop.SetValue(instance, env);
                }
            }
        }

        /// <summary>
        /// Handle <see cref="EnvironmentObjectAttribute"/>.
        /// Calls <see cref="Parse(object, string)"/> with the object of the property.
        /// If no instance is provided a new one will be created.
        /// </summary>
        /// <param name="instance">The object being parsed.</param>
        /// <param name="prop">The property of the object which should be parsed.</param>
        /// <param name="attr">The attribute of the property.</param>
        /// <param name="prefix">The prefix to use.</param>
        private static void HandleObjectAttribute(object instance, PropertyInfo prop, EnvironmentObjectAttribute attr, string prefix)
        {
            object value = prop.GetValue(instance);
            if (value == null)
            {
                value = Activator.CreateInstance(prop.PropertyType);
                prop.SetValue(instance, value);
            }
            Parse(value, prefix + attr.Prefix);
        }
    }
}