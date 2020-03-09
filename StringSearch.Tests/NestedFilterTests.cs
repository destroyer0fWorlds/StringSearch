using System.Linq;
using Xunit;

namespace StringSearch.Tests
{
    public class NestedFilterTests
    {
        [Fact]
        public void Nested_Filter_Should_Parse_Property_Hierarchy_Successfully()
        {
            // Arrange
            var filter = "(LastName[eq]Doe)[and]((FirstName[eq]John)[or](FirstName[eq]Jane))";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);

            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());

            Assert.Equal(typeof(NestedCriterion), parseResults.ElementAt(1).GetType());
            var nestedCriterion = (NestedCriterion)parseResults.ElementAt(1);
            Assert.Equal(typeof(Criterion), nestedCriterion.Criteria.ElementAt(0).GetType());
            Assert.Equal(typeof(Criterion), nestedCriterion.Criteria.ElementAt(1).GetType());
        }

        [Fact]
        public void Nested_Filter_Should_Parse_First_Tier_Successfully()
        {
            // Arrange
            var filter = "(LastName[eq]Doe)[and]((FirstName[eq]John)[or](FirstName[eq]Jane))";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);

            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            var criterion = (Criterion)parseResults.ElementAt(0);
            Assert.Equal("LastName", criterion.Name);
            Assert.Equal(ConditionOperatorType.Equals, criterion.Operator);
            Assert.Equal("Doe", criterion.Value);

            Assert.Equal(typeof(NestedCriterion), parseResults.ElementAt(1).GetType());
        }

        [Fact]
        public void Nested_Filter_Should_Parse_Second_Tier_Successfully()
        {
            // Arrange
            var filter = "(LastName[eq]Doe)[and]((FirstName[eq]John)[or](FirstName[eq]Jane))";

            // Act
            var parseResults = new Parser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);

            Assert.Equal(typeof(NestedCriterion), parseResults.ElementAt(1).GetType());
            var nestedCriterion = (NestedCriterion)parseResults.ElementAt(1);

            Assert.Equal(typeof(Criterion), nestedCriterion.Criteria.ElementAt(0).GetType());
            var firstCriterion = (Criterion)nestedCriterion.Criteria.ElementAt(0);
            Assert.Equal(LogicOperatorType.And, firstCriterion.LogicOperator);
            Assert.Equal("FirstName", firstCriterion.Name);
            Assert.Equal(ConditionOperatorType.Equals, firstCriterion.Operator);
            Assert.Equal("John", firstCriterion.Value);

            Assert.Equal(typeof(Criterion), nestedCriterion.Criteria.ElementAt(1).GetType());
            var secondCriterion = (Criterion)nestedCriterion.Criteria.ElementAt(1);
            Assert.Equal(LogicOperatorType.Or, secondCriterion.LogicOperator);
            Assert.Equal("FirstName", secondCriterion.Name);
            Assert.Equal(ConditionOperatorType.Equals, secondCriterion.Operator);
            Assert.Equal("Jane", secondCriterion.Value);
        }
    }
}
