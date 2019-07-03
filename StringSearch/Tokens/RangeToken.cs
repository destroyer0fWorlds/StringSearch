using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Tokens
{
    /// <summary>
    /// Range token
    /// </summary>
    class RangeToken : Token
    {
        /// <summary>
        /// Start value
        /// </summary>
        public string StartValue { get; set; }

        /// <summary>
        /// End value
        /// </summary>
        public string EndValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeToken"/>
        /// </summary>
        public RangeToken()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeToken"/> class with the supplied start and end values
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        public RangeToken(string startValue, string endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }
    }
}
