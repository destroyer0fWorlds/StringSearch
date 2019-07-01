using System;
using System.Collections.Generic;
using System.Text;

namespace StringSearch.Groups
{
    class CharacterSet
    {
        public char StartCharacter { get; set; }

        public char EndCharacter { get; set; }

        public CharacterSet()
        {

        }

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
