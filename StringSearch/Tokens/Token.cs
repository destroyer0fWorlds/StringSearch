
namespace StringSearch.Tokens
{
    /// <summary>
    /// Base token
    /// </summary>
    abstract class Token : IToken
    {
        /// <inheritdoc />
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
