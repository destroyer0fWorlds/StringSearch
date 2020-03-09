using System.Linq;
using TypeSearch;
using TypeSearch.Criteria;
using Xunit;

namespace StringSearch.Converters.TypeSearchLib.Tests
{
    public class ConversionTests
    {
        [Fact]
        public void Single_Filter_Should_Convert_Successfully()
        {
            // Arrange
            var filter = "(Email[like]@gmail.com)";

            // Act
            var stringResults = new Parser().Parse(filter);
            var typeResults = new Parser().ParseAs(filter, new TypeSearchConverter<object>());

            // Assert
            Assert.NotNull(stringResults);
            Assert.NotNull(typeResults);

            Assert.NotEmpty(stringResults);
            Assert.NotEmpty(typeResults.Criteria);

            Assert.Equal(typeof(Criterion), stringResults.ElementAt(0).GetType());
            var stringCriterion = (Criterion)stringResults.ElementAt(0);
            Assert.Equal("Email", stringCriterion.Name);
            Assert.Equal(ConditionOperatorType.Like, stringCriterion.Operator);
            Assert.Equal("@gmail.com", stringCriterion.Value);

            Assert.Equal(typeof(CriteriaContainer<object>), typeResults.Criteria.ElementAt(0).GetType());
            var typeCriterion = typeResults.Criteria.ElementAt(0);
            Assert.NotNull(typeCriterion.SingleCriterion);
            var singleCriterion = typeCriterion.SingleCriterion;
            Assert.Equal("Email", singleCriterion.Name);
            Assert.Equal(SingleOperator.Like, singleCriterion.Operator);
            Assert.Equal("@gmail.com", singleCriterion.Value);
        }

        [Fact]
        public void Range_Filter_Should_Convert_Successfully()
        {
            // Arrange
            var filter = "(Age[between]20-30)";

            // Act
            var stringResults = new Parser().Parse(filter);
            var typeResults = new Parser().ParseAs(filter, new TypeSearchConverter<object>());

            // Assert
            Assert.NotNull(stringResults);
            Assert.NotNull(typeResults);

            Assert.NotEmpty(stringResults);
            Assert.NotEmpty(typeResults.Criteria);

            Assert.Equal(typeof(RangeCriterion), stringResults.ElementAt(0).GetType());
            var stringCriterion = (RangeCriterion)stringResults.ElementAt(0);
            Assert.Equal("Age", stringCriterion.Name);
            Assert.Equal(ConditionOperatorType.Between, stringCriterion.Operator);
            Assert.Equal("20", stringCriterion.StartValue);
            Assert.Equal("30", stringCriterion.EndValue);

            Assert.Equal(typeof(CriteriaContainer<object>), typeResults.Criteria.ElementAt(0).GetType());
            var typeCriterion = typeResults.Criteria.ElementAt(0);
            Assert.NotNull(typeCriterion.RangeCriterion);
            var rangeCriterion = typeCriterion.RangeCriterion;
            Assert.Equal("Age", rangeCriterion.Name);
            Assert.Equal(RangeOperator.Between, rangeCriterion.Operator);
            Assert.Equal("20", rangeCriterion.StartValue);
            Assert.Equal("30", rangeCriterion.EndValue);
        }

        [Fact]
        public void Nested_Filter_Should_Convert_First_Tier_Successfully()
        {
            // Arrange
            var filter = "(LastName[eq]Doe)[and]((FirstName[eq]John)[or](FirstName[eq]Jane))";

            // Act
            var stringResults = new Parser().Parse(filter);
            var typeResults = new Parser().ParseAs(filter, new TypeSearchConverter<object>());

            // Assert
            Assert.NotNull(stringResults);
            Assert.NotNull(typeResults);

            Assert.NotEmpty(stringResults);
            Assert.NotEmpty(typeResults.Criteria);

            Assert.Equal(typeof(Criterion), stringResults.ElementAt(0).GetType());
            var stringCriterion = (Criterion)stringResults.ElementAt(0);
            Assert.Equal("LastName", stringCriterion.Name);
            Assert.Equal(ConditionOperatorType.Equals, stringCriterion.Operator);
            Assert.Equal("Doe", stringCriterion.Value);

            Assert.Equal(typeof(CriteriaContainer<object>), typeResults.Criteria.ElementAt(0).GetType());
            var typeCriterion = typeResults.Criteria.ElementAt(0);
            Assert.NotNull(typeCriterion.SingleCriterion);
            var singleCriterion = typeCriterion.SingleCriterion;
            Assert.Equal("LastName", singleCriterion.Name);
            Assert.Equal(SingleOperator.Equals, singleCriterion.Operator);
            Assert.Equal("Doe", singleCriterion.Value);
        }

        [Fact]
        public void Nested_Filter_Should_Convert_Second_Tier_Successfully()
        {
            // Arrange
            var filter = "(LastName[eq]Doe)[and]((FirstName[eq]John)[or](FirstName[eq]Jane))";

            // Act
            var stringResults = new Parser().Parse(filter);
            var typeResults = new Parser().ParseAs(filter, new TypeSearchConverter<object>());

            // Assert
            Assert.NotNull(stringResults);
            Assert.NotNull(typeResults);

            Assert.NotEmpty(stringResults);
            Assert.NotEmpty(typeResults.Criteria);

            // StringSearch
            Assert.Equal(typeof(NestedCriterion), stringResults.ElementAt(1).GetType());
            var stringNestedCriterion = (NestedCriterion)stringResults.ElementAt(1);

            Assert.Equal(typeof(Criterion), stringNestedCriterion.Criteria.ElementAt(0).GetType());
            var stringFirstCriterion = (Criterion)stringNestedCriterion.Criteria.ElementAt(0);
            Assert.Equal(LogicOperatorType.And, stringFirstCriterion.LogicOperator);
            Assert.Equal("FirstName", stringFirstCriterion.Name);
            Assert.Equal(ConditionOperatorType.Equals, stringFirstCriterion.Operator);
            Assert.Equal("John", stringFirstCriterion.Value);

            Assert.Equal(typeof(Criterion), stringNestedCriterion.Criteria.ElementAt(1).GetType());
            var stringSecondCriterion = (Criterion)stringNestedCriterion.Criteria.ElementAt(1);
            Assert.Equal(LogicOperatorType.Or, stringSecondCriterion.LogicOperator);
            Assert.Equal("FirstName", stringSecondCriterion.Name);
            Assert.Equal(ConditionOperatorType.Equals, stringSecondCriterion.Operator);
            Assert.Equal("Jane", stringSecondCriterion.Value);
            
            // TypeSearch
            Assert.Equal(typeof(CriteriaContainer<object>), typeResults.Criteria.ElementAt(1).GetType());
            var typeCriterion = typeResults.Criteria.ElementAt(1);
            Assert.NotNull(typeCriterion.CriteriaCollection);
            var nestedCriteria = typeCriterion.CriteriaCollection;

            Assert.Equal(typeof(CriteriaContainer<object>), nestedCriteria.Criteria.ElementAt(0).GetType());
            var typeNestedCriterion1 = nestedCriteria.Criteria.ElementAt(0);
            Assert.NotNull(typeNestedCriterion1.SingleCriterion);
            var typeFirstCriterion = typeNestedCriterion1.SingleCriterion;
            Assert.Equal(LogicalOperator.And, typeNestedCriterion1.Operator);
            Assert.Equal("FirstName", typeFirstCriterion.Name);
            Assert.Equal(SingleOperator.Equals, typeFirstCriterion.Operator);
            Assert.Equal("John", typeFirstCriterion.Value);

            Assert.Equal(typeof(CriteriaContainer<object>), nestedCriteria.Criteria.ElementAt(1).GetType());
            var typeNestedCriterion2 = nestedCriteria.Criteria.ElementAt(1);
            Assert.NotNull(typeNestedCriterion2.SingleCriterion);
            var typeSecondCriterion = typeNestedCriterion2.SingleCriterion;
            Assert.Equal(LogicalOperator.Or, typeNestedCriterion2.Operator);
            Assert.Equal("FirstName", typeSecondCriterion.Name);
            Assert.Equal(SingleOperator.Equals, typeSecondCriterion.Operator);
            Assert.Equal("Jane", typeSecondCriterion.Value);
        }
    }
}
