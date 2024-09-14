using DistanceFareCalcuation.App.Models;
using DistanceFareCalcuation.App.Validator;

namespace DistanceFareCalcuation.Tests
{
    public class BandValidationTests
    {
        [Fact]
        public void Validate_WhenNoBands_ThrowsInvalidOperationException()
        {
            // Arrange
            List<Band> bands = new List<Band>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenInvalidBand_ThrowsInvalidOperationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band { MileFrom = 0, MileTo = 10 },
                new Band { MileFrom = 10, MileTo = 10 } // Invalid band with MileFrom equal to MileTo
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenMultipleBaseRateBands_ThrowsInvalidOperationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band { MileFrom = 0, MileTo = 10, IsBaseBand = true },
                new Band { MileFrom = 10, MileTo = 20, IsBaseBand = true } // Multiple base rate bands
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenGapExistsAndAllowGapsIsFalse_ThrowsInvalidOperationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band { MileFrom = 0, MileTo = 10 },
                new Band { MileFrom = 20, MileTo = 30 } // Gap between bands
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenOverlapExists_ThrowsInvalidOperationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band { MileFrom = 0, MileTo = 10 },
                new Band { MileFrom = 5, MileTo = 15 } // Overlapping bands
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenValidBands_ReturnsTrue()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band { MileFrom = 0, MileTo = 10, IsBaseBand = true },
                new Band { MileFrom = 10, MileTo = 20 },
                new Band { MileFrom = 20, MileTo = 30 }
            };

            // Act
            bool result = bands.Validate();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WhenGapExistsAndAllowGapsIsTrue_ReturnsTrue()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {

                new Band { MileFrom = 0, MileTo = 10 },
                new Band { MileFrom = 20, MileTo = 30 }, // Gap between bands
                new Band { MileFrom = 0, MileTo = int.MaxValue , IsBaseBand = true }
            };

            // Act
            bool result = bands.Validate(allowGaps: true);

            // Assert
            Assert.True(result);
        }
    }
}