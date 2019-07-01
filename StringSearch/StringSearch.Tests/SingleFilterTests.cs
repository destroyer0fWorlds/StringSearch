using System.Linq;
using Xunit;

namespace StringSearch.Tests
{
    public class SingleFilterTests
    {
        [Fact]
        public void Single_Filter_Should_Parse_Property_Name_Successfully()
        {
            // Arrange
            var filter = "(Email[like]@gmail.com)";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal("Email", ((Criterion)parseResults.ElementAt(0)).Name);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Operator_Successfully()
        {
            // Arrange
            var filter = "(Email[like]@gmail.com)";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Like, ((Criterion)parseResults.ElementAt(0)).Operator);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Value_Successfully()
        {
            // Arrange
            var filter = "(Email[like]@gmail.com)";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal("@gmail.com", ((Criterion)parseResults.ElementAt(0)).Value);
        }
    }
}
