using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Groups
{
    /// <summary>
    /// Represents a set or characters (start and end)
    /// </summary>
    class CharacterSet
    {
        /// <summary>
        /// Start char
        /// </summary>
        public char StartCharacter { get; set; }

        /// <summary>
        /// End char
        /// </summary>
        public char EndCharacter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSet"/> class
        /// </summary>
        public CharacterSet()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSet"/> class for the given start and end characters
        /// </summary>
        /// <param name="startCharacter"></param>
        /// <param name="endCharacter"></param>
        public CharacterSet(char startCharacter, char endCharacter)
        {
            StartCharacter = startCharacter;
            EndCharacter = endCharacter;
        }

        public override string ToString()
        {
            return $"'{this.StartCharacter}' - '{this.EndCharacter}'";
        }
    }
}
