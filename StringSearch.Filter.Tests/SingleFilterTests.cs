using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StringSearch.Filter.Tests
{
    public class SingleFilterTests
    {
        [Fact]
        public void Single_Filter_Should_Parse_Property_Name_Successfully()
        {
            // Arrange
            var filter = "(Email[like]@gmail.com)";

            // Act
            var parseResults = new FilterParser().Parse(filter);

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
            var parseResults = new FilterParser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Like, ((Criterion)parseResults.ElementAt(0)).Operator);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Global_Custom_Operator_Successfully()
        {
            // Arrange
            var filter = "(Email[%]@gmail.com)";
            var operatorOverrides = new Dictionary<OperatorType, string>() { { OperatorType.Like, "%" } };

            // Act
            var parseResults = new FilterParser(operatorOverrides).Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Like, ((Criterion)parseResults.ElementAt(0)).Operator);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Local_Custom_Operator_Successfully()
        {
            // Arrange
            var filter1 = "(Email[%]@gmail.com)";
            var likeOverride1 = new Dictionary<OperatorType, string>() { { OperatorType.Like, "%" } };

            var filter2 = "(Email[SimilarTo]@gmail.com)";
            var likeOverride2 = new Dictionary<OperatorType, string>() { { OperatorType.Like, "SimilarTo" } };

            // Act
            var parser = new FilterParser();
            var parseResults1 = parser.Parse(filter1, likeOverride1);
            var parseResults2 = parser.Parse(filter2, likeOverride2);

            // Assert
            Assert.NotNull(parseResults1);
            Assert.NotEmpty(parseResults1);
            Assert.Single(parseResults1);
            Assert.Equal(typeof(Criterion), parseResults1.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Like, ((Criterion)parseResults1.ElementAt(0)).Operator);

            Assert.NotNull(parseResults2);
            Assert.NotEmpty(parseResults2);
            Assert.Single(parseResults2);
            Assert.Equal(typeof(Criterion), parseResults2.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Like, ((Criterion)parseResults2.ElementAt(0)).Operator);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Global_And_Local_Custom_Operator_Successfully()
        {
            // Arrange
            var filter = "(Email[SimilarTo]@gmail.com)[or](Email[EndsWith]@gmail.com)";
            var localOverride = new Dictionary<OperatorType, string>() { { OperatorType.Like, "SimilarTo" } };
            var globalOverride = new Dictionary<OperatorType, string>() { { OperatorType.EndsWith, "EndsWith" } };

            // Act
            var parseResults = new FilterParser(globalOverride).Parse(filter, localOverride);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal(ConditionOperatorType.Like, ((Criterion)parseResults.ElementAt(0)).Operator);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(1).GetType());
            Assert.Equal(ConditionOperatorType.EndsWith, ((Criterion)parseResults.ElementAt(1)).Operator);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Custom_Logic_Operator_Successfully()
        {
            // Arrange
            var filter = "(FirstName[eq]John)[oreo](FirstName[eq]Jane)";
            var operatorOverrides = new Dictionary<OperatorType, string>() { { OperatorType.Or, "oreo" } };

            // Act
            var parseResults = new FilterParser(operatorOverrides).Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(1).GetType());
            Assert.Equal(LogicOperatorType.Or, ((Criterion)parseResults.ElementAt(1)).LogicOperator);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Value_Successfully()
        {
            // Arrange
            var filter = "(Email[like]@gmail.com)";

            // Act
            var parseResults = new FilterParser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            Assert.Equal("@gmail.com", ((Criterion)parseResults.ElementAt(0)).Value);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Square_Brackets_Successfully()
        {
            // Arrange
            // "[" and "]" are used to identify operators
            var filter = "(Em][][ail[like][@][gmail].][[com]]]])";

            // Act
            var parseResults = new FilterParser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            var criterion = ((Criterion)parseResults.ElementAt(0));
            Assert.Equal("Em][][ail", criterion.Name);
            Assert.Equal(ConditionOperatorType.Like, criterion.Operator);
            Assert.Equal("[@][gmail].][[com]]]]", criterion.Value);
        }

        [Fact]
        public void Single_Filter_Should_Parse_Parentheses_Successfully()
        {
            // Arrange
            // "(" and ")" are used to identify groups
            var filter = "(Em)()(ail[like](@)(gmail).)((com)))))";

            // Act
            var parseResults = new FilterParser().Parse(filter);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal(typeof(Criterion), parseResults.ElementAt(0).GetType());
            var criterion = ((Criterion)parseResults.ElementAt(0));
            Assert.Equal("Em)()(ail", criterion.Name);
            Assert.Equal(ConditionOperatorType.Like, criterion.Operator);
            Assert.Equal("(@)(gmail).)((com))))", criterion.Value);
        }
    }
}
