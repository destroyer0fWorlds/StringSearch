using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StringSearch.Filter.Tests
{
    public class KnownBehaviorTests
    {
        [Fact]
        public void Ranges_Cannot_Contain_More_Than_One_Hyphen()
        {
            // "-" is used to separate range values.
            // Having more or less than exactly one hyphen in a range causes problems.

            // Arrange
            var filter = "(DateOfBirth[between]01-01-2000-01-01-2100)";

            // Act
            Action act = () => { var parseResults = new FilterParser().Parse(filter); };

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Ranges_Must_Contain_At_Least_One_Hyphen()
        {
            // "-" is used to separate range values.
            // Having more or less than exactly one hyphen in a range causes problems.

            // Arrange
            var filter = "(Age[between]30)";

            // Act
            Action act = () => { var parseResults = new FilterParser().Parse(filter); };

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Parsing_Null_Should_Result_In_Empty_Parse()
        {
            // Arrange
            string filter = null;

            // Act
            var parseResults = new FilterParser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.Empty(parseResults);
        }

        [Fact]
        public void Parsing_Empty_String_Should_Result_In_Empty_Parse()
        {
            // Arrange
            string filter = string.Empty;

            // Act
            var parseResults = new FilterParser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.Empty(parseResults);
        }
    }
}
