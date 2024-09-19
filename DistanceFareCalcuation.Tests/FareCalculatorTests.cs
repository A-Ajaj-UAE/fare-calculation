using DistanceFareCalcuation.App.Models;
using DistanceFareCalcuation.App.Services;

namespace DistanceFareCalcuation.Tests
{
    public class FareCalculatorTests
    {
        [Fact]
        public void CalculateDiscanct6Mile_ShouldReturnCorrectFare()
        {
            // Arrange
            var bands = new List<Band>()
            {
                new Band(1, 10, 1),      // From 0 to 1 mile: 10.00 GBP
                new Band(5, 2, 2),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(10, 3, 3),      // From 6 to 16 miles: 3.00 GBP per mile
            };

            decimal distance = 6m;
            decimal expectedFare = 1m * 10 + 5m * 2;
            decimal defaultFare = 1m;

            FareCalculator fareCalculator = new FareCalculator(bands, defaultFare);

            // Act
            decimal actualFare = fareCalculator.Calculate(distance);

            // Assert
            Assert.Equal(expectedFare, actualFare);
        }

        [Fact]
        public void CalculateDiscanct12Mile_ShouldReturnCorrectFare()
        {
            // Arrange
            var bands = new List<Band>()
            {
                new Band(1, 10, 1),      // From 0 to 1 mile: 10.00 GBP
                new Band(5, 2, 2),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(10, 3, 3),      // From 6 to 16 miles: 3.00 GBP per mile
            };

            decimal distance = 12m;
            decimal expectedFare = 1m * 10 + 5m * 2 + 6m * 3m;
            decimal defaultFare = 1m;

            FareCalculator fareCalculator = new FareCalculator(bands, defaultFare);

            // Act
            decimal actualFare = fareCalculator.Calculate(distance);

            // Assert
            Assert.Equal(expectedFare, actualFare);
        }

        [Fact]
        public void CalculateDiscanct100Mile_ShouldReturnCorrectFare()
        {
            // Arrange
            var bands = new List<Band>()
            {
                new Band(1, 10, 1),      // From 0 to 1 mile: 10.00 GBP
                new Band(5, 2, 2),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(10, 3, 3),      // From 6 to 16 miles: 3.00 GBP per mile
            };

            decimal distance = 100m;
            decimal expectedFare = 1m * 10 + 5m * 2 + 10m * 3 + 84m * 1;
            decimal defaultFare = 1m;

            FareCalculator fareCalculator = new FareCalculator(bands,defaultFare);

            // Act
            decimal actualFare = fareCalculator.Calculate(distance);

            // Assert
            Assert.Equal(expectedFare, actualFare);
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentException_WhenBandsIsNull()
        {
            // Arrange
            List<Band> bands = null;
            decimal distance = 10m;
            decimal defaultFare = 1m;


            FareCalculator fareCalculator = new FareCalculator(bands, defaultFare);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fareCalculator.Calculate(distance));
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentException_WhenDistanceIsNegative()
        {
            // Arrange
            List<Band> bands = new List<Band>();
            decimal distance = -5m;
            decimal defaultFare = 1m;

            FareCalculator fareCalculator = new FareCalculator(bands, defaultFare);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fareCalculator.Calculate(distance));
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentException_WhenDefaultFareIsNegativeOrZero()
        {
            // Arrange
            List<Band> bands = new List<Band>();
            decimal distance = 5m;
            decimal defaultFare = 0m;

            FareCalculator fareCalculator = new FareCalculator(bands, defaultFare);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fareCalculator.Calculate(distance));
        }
    }
}
