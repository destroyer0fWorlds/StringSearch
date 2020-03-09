using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace StringSearch.OrderBy.Tests
{
    public class OrderByTests
    {
        [Fact]
        public void Single_Order_By_Parses_Successfully()
        {
            // Arrange
            var orderBy = "FirstName[asc]";

            // Act
            var parseResults = new OrderByParser().Parse(orderBy);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            Assert.Single(parseResults);
            Assert.Equal("FirstName", parseResults.ElementAt(0).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults.ElementAt(0).Direction);
        }

        [Fact]
        public void Multiple_Order_By_Parses_Successfully()
        {
            // Arrange
            var orderBy = "FirstName[asc],LastName[desc],DateOfBirth[desc],Age[asc]";

            // Act
            var parseResults = new OrderByParser().Parse(orderBy);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);
            
            Assert.Equal("FirstName", parseResults.ElementAt(0).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults.ElementAt(0).Direction);

            Assert.Equal("LastName", parseResults.ElementAt(1).Name);
            Assert.Equal(OrderByDirection.Descending, parseResults.ElementAt(1).Direction);

            Assert.Equal("DateOfBirth", parseResults.ElementAt(2).Name);
            Assert.Equal(OrderByDirection.Descending, parseResults.ElementAt(2).Direction);

            Assert.Equal("Age", parseResults.ElementAt(3).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults.ElementAt(3).Direction);
        }

        [Fact]
        public void Single_Order_By_Should_Parse_Global_Custom_Direction_Successfully()
        {
            // Arrange
            var orderBy = "FirstName[up],LastName[desc]";
            var directionOverride = new Dictionary<OrderByDirection, string>() { { OrderByDirection.Ascending, "up" } };

            // Act
            var parser = new OrderByParser(directionOverride);
            var parseResults = parser.Parse(orderBy);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);

            Assert.Equal("FirstName", parseResults.ElementAt(0).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults.ElementAt(0).Direction);

            Assert.Equal("LastName", parseResults.ElementAt(1).Name);
            Assert.Equal(OrderByDirection.Descending, parseResults.ElementAt(1).Direction);
        }

        [Fact]
        public void Single_Order_By_Should_Parse_Local_Custom_Direction_Successfully()
        {
            // Arrange
            var orderBy1 = "FirstName[asc],LastName[down]";
            var directionOverride1 = new Dictionary<OrderByDirection, string>() { { OrderByDirection.Descending, "down" } };

            var orderBy2 = "FirstName[asc],LastName[!asc]";
            var directionOverride2 = new Dictionary<OrderByDirection, string>() { { OrderByDirection.Descending, "!asc" } };

            // Act
            var parser = new OrderByParser();
            var parseResults1 = parser.Parse(orderBy1, directionOverride1);
            var parseResults2 = parser.Parse(orderBy2, directionOverride2);

            // Assert
            Assert.NotNull(parseResults1);
            Assert.NotEmpty(parseResults1);
            Assert.Equal("FirstName", parseResults1.ElementAt(0).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults1.ElementAt(0).Direction);
            Assert.Equal("LastName", parseResults1.ElementAt(1).Name);
            Assert.Equal(OrderByDirection.Descending, parseResults1.ElementAt(1).Direction);

            Assert.NotNull(parseResults2);
            Assert.NotEmpty(parseResults2);
            Assert.Equal("FirstName", parseResults2.ElementAt(0).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults2.ElementAt(0).Direction);
            Assert.Equal("LastName", parseResults2.ElementAt(1).Name);
            Assert.Equal(OrderByDirection.Descending, parseResults2.ElementAt(1).Direction);
        }

        [Fact]
        public void Single_Order_By_Should_Parse_Global_And_Local_Custom_Direction_Successfully()
        {
            // Arrange
            var orderBy = "FirstName[up],LastName[down]";
            var globalOverride = new Dictionary<OrderByDirection, string>() { { OrderByDirection.Ascending, "up" } };
            var localOverride = new Dictionary<OrderByDirection, string>() { { OrderByDirection.Descending, "down" } };

            // Act
            var parseResults = new OrderByParser(globalOverride).Parse(orderBy, localOverride);

            // Assert
            Assert.NotNull(parseResults);
            Assert.NotEmpty(parseResults);

            Assert.Equal("FirstName", parseResults.ElementAt(0).Name);
            Assert.Equal(OrderByDirection.Ascending, parseResults.ElementAt(0).Direction);

            Assert.Equal("LastName", parseResults.ElementAt(1).Name);
            Assert.Equal(OrderByDirection.Descending, parseResults.ElementAt(1).Direction);
        }
    }
}
