namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for options which hold a configuration.
    /// </summary>
    public interface IConfigOptions
    {
        /// <summary>
        /// Path to the config to use.
        /// </summary>
        string Config { get; }

        /// <summary>
        /// Determinate if the config should only be validated.
        /// </summary>
        bool ValidationOnly { get; }
    }
}