using Config.Net;
using System;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Resolvers
{
    /// <summary>
    /// Implements a <see cref="IResolveConfigs{T, T2}"/> via <see cref="Config.Net.ConfigurationBuilder{T}"/>.
    /// </summary>
    /// <typeparam name="TConfig">The type of the configuration to resolve.</typeparam>
    /// <typeparam name="TOptions">The type of options to resolve the configuration with.</typeparam>
    public class ConfigNetResolver<TConfig, TOptions> : IResolveConfigs<TConfig, TOptions> where TConfig : class where TOptions : class
    {
        /// <summary>
        /// Define your function which sets the Configuration source for the <see cref="ConfigurationBuilder{T}"/>.
        /// </summary>
        public Func<ConfigurationBuilder<TConfig>, ConfigurationBuilder<TConfig>> Source { get; set; } = (builder) => builder.UseAppConfig();

        /// <summary>
        /// Creates a new instance with the default <see cref="Source"/>.
        /// </summary>
        public ConfigNetResolver()
        {
        }

        /// <summary>
        /// Creates a new instance with the given source.
        /// </summary>
        /// <param name="source">Func to call to build the configuration.</param>
        public ConfigNetResolver(Func<ConfigurationBuilder<TConfig>, ConfigurationBuilder<TConfig>> source)
        {
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Creates a new instance with a json source file.
        /// </summary>
        /// <param name="jsonFile">The file to use as json source.</param>
        public ConfigNetResolver(string jsonFile)
        {
            this.Source = (builder) => builder.UseJsonFile(jsonFile);
        }

        /// <inheritdoc/>
        public TConfig Resolve(TOptions options)
        {
            var builder = new ConfigurationBuilder<TConfig>();
            return this.Source.Invoke(builder).Build();
        }
    }
}