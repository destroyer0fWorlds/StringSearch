using System.Linq;
using Xunit;

namespace StringSearch.Tests
{
    public class RangeFilterTests
    {
        [Fact]
        public void Range_Filter_Should_Parse_Property_Name_Successfully()
        {
            // Arrange
            var filter = "(UserId[between]100-200)";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(0).GetType());
            Assert.Equal("UserId", ((RangeCriterion)parseResults.ElementAt(0)).Name);
        }

        [Fact]
        public void Range_Filter_Should_Parse_Operator_Successfully()
        {
            // Arrange
            var filter = "(UserId[between]100-200)";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Between, ((RangeCriterion)parseResults.ElementAt(0)).Operator);
        }

        [Fact]
        public void Range_Filter_Should_Parse_Value_Successfully()
        {
            // Arrange
            var filter = "(UserId[between]100-200)";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(0).GetType());
            Assert.Equal("100", ((RangeCriterion)parseResults.ElementAt(0)).StartValue);
            Assert.Equal("200", ((RangeCriterion)parseResults.ElementAt(0)).EndValue);
        }
    }
}
