using DistanceFareCalcuation.App.Models;
using DistanceFareCalcuation.App.Validator;
using System.ComponentModel.DataAnnotations;

namespace DistanceFareCalcuation.Tests
{
    public class BandValidationTests
    {
        [Fact]
        public void Validate_WhenNoBands_ThrowsValidationException()
        {
            // Arrange
            List<Band> bands = new List<Band>();

            // Act & Assert
            Assert.Throws<ValidationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenInvalidBandLimit_ThrowsValidationExceptio()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band (-10, 10, 0), // Invalid band with Limit less than 0
                new Band (10, 10, 1)
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenInvalidBandFare_ThrowsValidationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                new Band (1, -10, 0), // Invalid band with fare less than 0
                new Band (10, 10, 1)
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenFareIsNotDecreasingAndAllowMixFareFalse_ThrowsValidationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                 new Band (1, 10, 0), // Invalid band with fare less than 0
                 new Band (5, 4, 1),
                 new Band (5, 5, 2)
            };

            // Act & Assert
            Assert.Throws<ValidationException>(() => bands.Validate());
        }

        [Fact]
        public void Validate_WhenFareIsNotDecreasingAndIsStrictModeTrue_ThrowsValidationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                 new Band (1, 10, 0), 
                 new Band (5, 4, 1),
                 new Band (5, 5, 2) // Invalid band with fare not decreasing
            };

           
            bool result = bands.Validate(IsStrictMode: false);

            // Act & Assert
            Assert.Throws<ValidationException>(() => bands.Validate(IsStrictMode: true));
        }

        [Fact]
        public void Validate_WhenFareIsNotDecreasingAndIsStrictModeFalse_ReturnTrue()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                 new Band (1, 10, 0), 
                 new Band (5, 4, 1),
                 new Band (5, 5, 2) // Invalid band with fare not decreasing
            };

            // Act
            bool result = bands.Validate(IsStrictMode: false);

            // Act & Assert
            Assert.Throws<ValidationException>(() => bands.Validate(IsStrictMode: true));
        }

        [Fact]
        public void Validate_WhenlimitIsNotIncreasingAndIsStrictModeTrue_ThrowsValidationException()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                 new Band (1, 10, 0),
                 new Band (5, 4, 1),
                 new Band (4, 5, 2) // Invalid band with limit increasing
            };

            // Act
            bool result = bands.Validate(IsStrictMode: true);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WhenlimitIsNotIncreasingAndIsStrictModeFalse_ReturnTrue()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                 new Band (1, 10, 0), 
                 new Band (5, 4, 1),
                 new Band (4, 5, 2) // Invalid band with limit increasing
            };

            // Act
            bool result = bands.Validate(IsStrictMode: false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_WhenValidBands_ReturnsTrue()
        {
            // Arrange
            List<Band> bands = new List<Band>
            {
                 new Band (1, 10, 0), // Invalid band with fare less than 0
                 new Band (5, 3, 1),
                 new Band (10, 2, 2) 
            };

            // Act
            bool result = bands.Validate(IsStrictMode: false);

            // Assert
            Assert.True(result);
        }
    }
}