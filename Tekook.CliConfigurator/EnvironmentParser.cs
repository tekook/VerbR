using System;
using System.Reflection;

namespace Tekook.CliConfigurator
{
    internal class EnvironmentParser
    {
        public object Instance { get; set; }

        public Type Type { get; set; }

        public EnvironmentParser(object instance)
        {
            this.Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this.Type = instance.GetType();
        }

        public void Parse()
        {
            PropertyInfo[] props = this.Type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attributes = prop.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is EnvironmentAttribute envAttr)
                    {
                        HandleAttribute(prop, envAttr);
                    }
                    else if (attribute is EnvironmentObjectAttribute envObjAttr)
                    {
                        HandleAttribute(prop, envObjAttr);
                    }
                }
            }
        }

        private void HandleAttribute(PropertyInfo prop, EnvironmentObjectAttribute attr)
        {
            // to do -> handle EnvironMent
        }

        private void HandleAttribute(PropertyInfo prop, EnvironmentAttribute attr)
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