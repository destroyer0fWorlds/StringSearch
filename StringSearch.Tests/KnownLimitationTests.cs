using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StringSearch.Tests
{
    public class KnownLimitationTests
    {
        [Fact]
        public void Filter_Cannot_Contain_Jagged_Parentheses()
        {
            // "(" and ")" are special characters.
            // Condition groups are expected to be surrounded by a single set of parentheses - extras cause problems.

            // Arrange
            var filter = "(Description[like]special characters: '@', '#', '[', '{', '(')";

            // Act
            Action act = () => { var parseResults = new Parser().Parse(filter); };

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Ranges_Cannot_Contain_More_Than_One_Hyphen()
        {
            // "-" is used to separate range values.
            // Having more or less than exactly one hyphen in a range causes problems.

            // Arrange
            var filter = "(DateOfBirth[between]01-01-2000-01-01-2100)";

            // Act
            Action act = () => { var parseResults = new Parser().Parse(filter); };

            // Assert
            Assert.Throws<FormatException>(act);
        }
    }
}
