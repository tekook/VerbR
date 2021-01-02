using Config.Net;
using System;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Resolvers
{
    /// <summary>
    /// Implements a <see cref="IResolveConfig{T}"/> via <see cref="Config.Net.ConfigurationBuilder{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the configuration to resolve.</typeparam>
    public class ConfigNetResolver<T> : IResolveConfig<T> where T : class
    {
        /// <summary>
        /// Define your function which sets the Configuration source for the <see cref="ConfigurationBuilder{T}"/>.
        /// </summary>
        public Func<ConfigurationBuilder<T>, ConfigurationBuilder<T>> Source { get; set; } = (builder) => builder.UseAppConfig();

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
        public ConfigNetResolver(Func<ConfigurationBuilder<T>, ConfigurationBuilder<T>> source)
        {
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <inheritdoc/>
        public T Resolve()
        {
            var builder = new ConfigurationBuilder<T>();
            return this.Source.Invoke(builder).Build();
        }
    }
}