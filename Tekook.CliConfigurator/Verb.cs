using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Base class for all verbs.
    /// </summary>
    /// <typeparam name="T">Provide your Type of Options.</typeparam>
    public abstract class Verb<T> where T : Options
    {
        /// <summary>
        /// Logger for this class.
        /// </summary>
        private static readonly NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Options of this <see cref="Verb{T, T2}"/>.
        /// </summary>
        public T Options { get; set; }

        /// <summary>
        /// Create a new verb with the given options.
        /// </summary>
        /// <param name="options">Options used for this configuration.</param>
        protected Verb(T options)
        {
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Invoke the <see cref="Verb{T}"/>.
        /// </summary>
        /// <returns>Returns an Exitcode.</returns>
        public int Invoke()
        {
            log.Info("Application starting");
            return this.InvokeAsync().Result;
            
        }


        /// <summary>
        /// Invoke the <see cref="Verb{T}"/> asynchronosly.
        /// </summary>
        /// <returns>Exitcode.</returns>
        public abstract Task<int> InvokeAsync();

    }
    /// <summary>
    /// Base class for all verbs with configs.
    /// </summary>
    /// <typeparam name="T">Provide your Type of Options.</typeparam>
    /// <typeparam name="T2">Provide your Type of Config.</typeparam>
    public abstract class Verb<T, T2> : Verb<T> where T : ConfigurableOptions where T2 : Config
    {

        private static readonly NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Config of the <see cref="Verb{T, T2}"/>.
        /// </summary>
        public T2 Config { get; set; }

        /// <summary>
        /// Create a new verb and load configuration from file or env.
        /// </summary>
        /// <param name="options">Options used for this configuration.</param>
        protected Verb(T options) : base(options)
        {
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
                EnvironmentParser parser = new EnvironmentParser(this.Config);
                parser.Parse();
                this.Config.Validate();
            }
        }
        /// <summary>
        /// Invoke the <see cref="Verb{T, T2}"/>.
        /// </summary>
        /// <returns>Exit Code</returns>
        public new int Invoke()
        {
            if (this.Options.ValidationOnly)
            {
                // Validation is performed in constructor. If we reached this step we are valid.
                log.Info("Validation passed");
                return 0;
            }
            return base.Invoke();
        }
    }
}