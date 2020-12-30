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
        /// Invoke <see cref="InvokeAsync"/>.Result
        /// </summary>
        /// <returns>Returns an Exitcode.</returns>
        public int Invoke()
        {
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
                EnvironmentParser.Parse(this.Config);
                this.Config.Validate();
            }
        }

        /// <summary>
        /// Invokes the <see cref="Verb{T, T2}"/> via <see cref="Verb{T}.InvokeAsync"/>.Result.
        /// if <see cref="ConfigurableOptions.ValidationOnly"/> is set, returns 0 if the validation passed.
        /// </summary>
        /// <seealso cref="Verb{T}.Invoke"/>
        /// <returns>Exit Code</returns>
        public new int Invoke()
        {
            if (this.Options.ValidationOnly)
            {
                // Validation is performed in constructor. If we reached this step we are valid.
                return 0;
            }
            return base.Invoke();
        }
    }
}