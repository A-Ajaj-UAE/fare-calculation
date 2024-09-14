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
                new Band(0, 1, 10m),      // From 0 to 1 mile: 10.00 GBP
                new Band(1, 6, 2m),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(6, 16, 3m),      // From 6 to 16 miles: 3.00 GBP per mile
                new Band(0, int.MaxValue, 1m, true) // From 16 miles onward: 1.00 GBP per mile
            };

            decimal distance = 6m;
            decimal expectedFare = 1m * 10 + 5m * 2;

            FareCalculator fareCalculator = new FareCalculator(bands);

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
                new Band(0, 1, 10m),      // From 0 to 1 mile: 10.00 GBP
                new Band(1, 6, 2m),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(6, 16, 3m),      // From 6 to 16 miles: 3.00 GBP per mile
                new Band(0, int.MaxValue, 1m, true) // From 16 miles onward: 1.00 GBP per mile
            };

            decimal distance = 12m;
            decimal expectedFare = 1m * 10 + 5m * 2 + 6m * 3m;

            FareCalculator fareCalculator = new FareCalculator(bands);

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
                new Band(0, 1, 10m),      // From 0 to 1 mile: 10.00 GBP
                new Band(1, 6, 2m),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(6, 16, 3m),      // From 6 to 16 miles: 3.00 GBP per mile
                new Band(0, int.MaxValue, 1m, true) // From 16 miles onward: 1.00 GBP per mile
            };

            decimal distance = 100m;
            decimal expectedFare = 1m * 10 + 5m * 2 + 10m * 3 + 84m * 1;

            FareCalculator fareCalculator = new FareCalculator(bands);

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

            FareCalculator fareCalculator = new FareCalculator(bands);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fareCalculator.Calculate(distance));
        }

        [Fact]
        public void Calculate_ShouldThrowArgumentException_WhenDistanceIsNegative()
        {
            // Arrange
            List<Band> bands = new List<Band>();
            decimal distance = -5m;

            FareCalculator fareCalculator = new FareCalculator(bands);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fareCalculator.Calculate(distance));
        }
    }
}
