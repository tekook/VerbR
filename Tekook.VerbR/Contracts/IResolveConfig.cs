namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for a Config Resolver.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResolveConfig<T> where T : class
    {
        /// <summary>
        /// Resolves the configuration.
        /// </summary>
        /// <returns>The resolved configuration.</returns>
        public T Resolve();
    }
}