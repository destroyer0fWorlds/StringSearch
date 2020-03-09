
namespace StringSearch.Filter.Tokens
{
    /// <summary>
    /// Property name token
    /// </summary>
    class NameToken : Token
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name => this.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameToken"/>
        /// </summary>
        public NameToken()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameToken"/> class with the supplied name
        /// </summary>
        /// <param name="name"></param>
        public NameToken(string name)
        {
            this.Value = name;
        }
    }
}
