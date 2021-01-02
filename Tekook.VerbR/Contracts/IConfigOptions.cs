namespace Tekook.VerbR.Contracts
{
    /// <summary>
    /// Contract for options which hold a configuration.
    /// </summary>
    public class IConfigOptions
    {
        /// <summary>
        /// Path to the config to use.
        /// </summary>
        public string Config { get; }

        /// <summary>
        /// Determinate if the config should only be validated.
        /// </summary>
        public bool ValidationOnly { get; }
    }
}