using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Value token
    /// </summary>
    class ValueToken : Token
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueToken"/>
        /// </summary>
        public ValueToken()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueToken"/> class with the supplied value
        /// </summary>
        /// <param name="value"></param>
        public ValueToken(string value)
        {
            this.Value = value;
        }
    }
}
