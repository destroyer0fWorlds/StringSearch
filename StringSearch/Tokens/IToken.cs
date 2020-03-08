
namespace StringSearch.Tokens
{
    /// <summary>
    /// Represents a discrete value
    /// </summary>
    interface IToken
    {
        /// <summary>
        /// The original string value
        /// </summary>
        string Value { get; set; }
    }
}
