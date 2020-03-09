using System.Collections.Generic;
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
        public void Range_Filter_Should_Parse_Global_Custom_Operator_Successfully()
        {
            // Arrange
            var filter = "(UserId[betwixt]100-200)";
            var operatorOverrides = new Dictionary<OperatorType, string>() { { OperatorType.Between, "betwixt" } };

            // Act
            var parseResults = new Parser(operatorOverrides).Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Between, ((RangeCriterion)parseResults.ElementAt(0)).Operator);
        }

        [Fact]
        public void Range_Filter_Should_Parse_Local_Custom_Operator_Successfully()
        {
            // Arrange
            var filter1 = "(UserId[betwixt]100-200)";
            var likeOverride1 = new Dictionary<OperatorType, string>() { { OperatorType.Between, "betwixt" } };

            var filter2 = "(UserId[in]100-200)";
            var likeOverride2 = new Dictionary<OperatorType, string>() { { OperatorType.Between, "in" } };

            // Act
            var parser = new Parser();
            var parseResults1 = parser.Parse(filter1, likeOverride1);
            var parseResults2 = parser.Parse(filter2, likeOverride2);

            // Assert
            Assert.NotNull(parseResults1);
            Assert.NotEmpty(parseResults1);
            Assert.Single(parseResults1);
            Assert.Equal(typeof(RangeCriterion), parseResults1.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Between, ((RangeCriterion)parseResults1.ElementAt(0)).Operator);

            Assert.NotNull(parseResults2);
            Assert.NotEmpty(parseResults2);
            Assert.Single(parseResults2);
            Assert.Equal(typeof(RangeCriterion), parseResults2.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Between, ((RangeCriterion)parseResults2.ElementAt(0)).Operator);
        }

        [Fact]
        public void Range_Filter_Should_Parse_Global_And_Local_Custom_Operator_Successfully()
        {
            // Arrange
            var filter = "(UserId[betwixt]100-200)[and](UserId[not_in]300-400)";
            var globalOverride = new Dictionary<OperatorType, string>() { { OperatorType.Between, "betwixt" } };
            var localOverride = new Dictionary<OperatorType, string>() { { OperatorType.NotBetween, "not_in" } };

            // Act
            var parseResults = new Parser(globalOverride).Parse(filter, localOverride);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Between, ((RangeCriterion)parseResults.ElementAt(0)).Operator);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(1).GetType());
            Assert.Equal(ConditionOperatorType.NotBetween, ((RangeCriterion)parseResults.ElementAt(1)).Operator);
        }

        [Fact]
        public void Range_Filter_Should_Parse_Custom_Logic_Operator_Successfully()
        {
            // Arrange
            var filter = "(UserId[between]100-200)[else](UserId[between]300-400)";
            var operatorOverrides = new Dictionary<OperatorType, string>() { { OperatorType.Or, "else" } };

            // Act
            var parseResults = new Parser(operatorOverrides).Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(1).GetType());
            Assert.Equal(LogicOperatorType.Or, ((RangeCriterion)parseResults.ElementAt(1)).LogicOperator);
        }

        [Fact]
        public void Range_Filter_Should_Parse_Values_Successfully()
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

        //[Fact]
        //public void Range_Filter_Should_Parse_Square_Brackets_Successfully()
        //{
        //    // Arrange
        //    // "[" and "]" are used to identify operators
        //    var filter = "(U]s[e]r[Id[between]1[0]0-[[200]]])";

        //    // Act
        //    var parseResults = new Parser().Parse(filter);

        //    // Assert
        //    Assert.NotNull(parseResults);
        //    Assert.NotEmpty(parseResults);
        //    Assert.Single(parseResults);
        //    Assert.Equal(typeof(RangeCriterion), parseResults.ElementAt(0).GetType());
        //    var criterion = ((RangeCriterion)parseResults.ElementAt(0));
        //    Assert.Equal("U]s[e]r[Id", criterion.Name);
        //    Assert.Equal(ConditionOperatorType.Between, criterion.Operator);
        //    Assert.Equal("1[0]0", criterion.StartValue);
        //    Assert.Equal("[[200]]]", criterion.EndValue);
        //}
    }
}
