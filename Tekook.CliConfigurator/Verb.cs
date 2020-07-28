using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tekook.CliConfigurator
{
    public abstract class Verb<T, T2> where T : Options where T2 : Config
    {
        private static readonly NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Config of the <see cref="Verb{T, T2}"/>.
        /// </summary>
        public T2 Config { get; set; }

        /// <summary>
        /// Options of this <see cref="Verb{T, T2}"/>.
        /// </summary>
        public T Options { get; set; }

        protected Verb(T options)
        {
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
            if (this.Options.Config != null)
            {
                try
                {
                    this.Config = JsonConvert.DeserializeObject<T2>(File.ReadAllText(this.Options.Config));
                }
                catch (IOException e)
                {
                    throw new ConfigException($"Could not read config file. ({e.Message})", e);
                }
                catch (JsonException e)
                {
                    throw new ConfigException($"Problems reading config file. ({e.Message})", e);
                }
            }
            else
            {
                this.Config = (T2)Activator.CreateInstance(typeof(T2));
                this.Config.FillFromEnv();
                this.Config.Validate();
            }
        }

        /// <summary>
        /// Invoke the <see cref="Verb{T, T2}"/>.
        /// </summary>
        /// <returns>Returns an Exitcode.</returns>
        public int Invoke()
        {
            if (this.Options.ValidationOnly)
            {
                // Validation is performed in constructor. If we reached this step we are valid.
                log.Info("Validation passed");
                return 0;
            }
            else
            {
                log.Info("Application starting");
                return this.InvokeAsync().Result;
            }
        }

        /// <summary>
        /// Invoke the <see cref="Verb{T, T2}"/> asynchronosly.
        /// </summary>
        /// <returns>Exitcode.</returns>
        public abstract Task<int> InvokeAsync();
    }
}