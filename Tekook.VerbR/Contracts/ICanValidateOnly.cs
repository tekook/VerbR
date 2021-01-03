namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for options which hold a configuration.
    /// </summary>
    public interface ICanValidateOnly
    {
        /// <summary>
        /// Determinate if the config should only be validated.
        /// </summary>
        bool ValidationOnly { get; }
    }
}