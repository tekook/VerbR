﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR
{
    /// <summary>
    /// Base class for all verbs.
    /// </summary>
    /// <typeparam name="TOptions">Provide your Type of Options.</typeparam>
    public abstract class Verb<TOptions> where TOptions : class
    {
        /// <summary>
        /// Options of this <see cref="Verb{T, T2}"/>.
        /// </summary>
        public TOptions Options { get; set; }

        /// <summary>
        /// Create a new verb with the given options.
        /// </summary>
        /// <param name="options">Options used for this configuration.</param>
        protected Verb(TOptions options)
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
    /// <typeparam name="TOptions">Provide your Type of Options.</typeparam>
    /// <typeparam name="TConfig">Provide your Type of Config.</typeparam>
    public abstract class Verb<TOptions, TConfig> : Verb<TOptions> where TOptions : class where TConfig : class
    {
        /// <summary>
        /// Config of the <see cref="Verb{T, T2}"/>.
        /// </summary>
        public TConfig Config { get; set; }

        /// <summary>
        /// Resolver this Verb uses.
        /// </summary>
        protected IResolveConfig<TConfig> Resolver { get; set; }

        /// <summary>
        /// Create a new verb and load configuration from file or env.
        /// </summary>
        /// <param name="options">Options used for this configuration.</param>
        protected Verb(TOptions options) : base(options)
        {
            this.SetResolver();
            this.Config = this.Resolver?.Resolve();
        }

        /// <summary>
        /// Invokes the <see cref="Verb{TOptions, TConfig}"/> via <see cref="Verb{TOptions}.InvokeAsync"/>.Result.
        /// if <see cref="IConfigOptions.ValidationOnly"/> is set, returns 0 if the validation passed, throws <see cref="ValidationException"/> otherwise.
        /// </summary>
        /// <exception cref="ValidationException">Thrown if the validation fails.</exception>
        /// <seealso cref="Verb{T}.Invoke"/>
        /// <returns>Exit Code</returns>
        public new int Invoke()
        {
            if (this.Config is IConfig config)
            {
                if (!config.IsValid(out IEnumerable<IValidationError> errors))
                {
                    throw new ValidationException(errors);
                }
                if (this.Options is IConfigOptions configOptions && configOptions.ValidationOnly)
                {
                    // Validation is performed in constructor. If we reached this step we are valid.
                    return 0;
                }
            }
            return base.Invoke();
        }

        /// <summary>
        /// Creates and sets the <see cref="Resolver"/>.
        /// Called in <see cref="Verb{TOptions, TConfig}.Verb(TOptions)"/>
        /// </summary>
        protected abstract void SetResolver();
    }
}