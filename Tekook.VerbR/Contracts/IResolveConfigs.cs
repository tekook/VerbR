namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for a Config Resolver.
    /// </summary>
    /// <typeparam name="TConfig">The config to resolve.</typeparam>
    /// <typeparam name="TOptions">The options to use for resolving.</typeparam>
    public interface IResolveConfigs<TConfig, TOptions> where TConfig : class where TOptions : class
    {
        /// <summary>
        /// Resolves the configuration.
        /// </summary>
        /// <returns>The resolved configuration.</returns>
        TConfig Resolve(TOptions options);
    }
}