using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Base token
    /// </summary>
    abstract class Token : IToken
    {
        /// <summary>
        /// The original string value
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
