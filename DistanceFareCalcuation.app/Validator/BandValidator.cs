using DistanceFareCalcuation.App.Models;
using System.ComponentModel.DataAnnotations;

namespace DistanceFareCalcuation.App.Validator
{
    public static class BandValidator
    {
        /// <summary>
        /// Validate set of bands to be applied to a fare calculation, when allow gap is used the base fare will be calculated
        /// </summary>
        /// <param name="bands">list of bands to validate</param>
        /// <param name="allowMixedFare"> its show warning instead of error when rate is not decreaseing in order</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool Validate(this List<Band> bands, bool IsStrictMode = false)
        {
            try
            {
                if (bands == null || bands.Count == 0)
                {
                    throw new ValidationException("No bands to validate.");
                }

                if (bands.Any(b=> b.Limit <= 0))
                {
                    throw new ValidationException($"Error: Bands limit must be gretter than 0");
                }

                if (bands.Any(b => b.Fare <= 0))
                {
                    throw new ValidationException($"Error: Bands Fare must be gretter than 0");
                }

                // Ensure no price order more expensive than remaining
                for (int i = 0; i < bands.Count - 1; i++)
                {
                    var band = bands[i];
                    var nextBand = bands[i + 1];

                    if (band.Fare < nextBand.Fare)
                    {
                        if (IsStrictMode)
                        {
                            throw new ValidationException($"Error: Band with rate {band.Fare} and limit {band.Limit} is less than band with rate {nextBand.Fare} and limit  {nextBand.Fare}");
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Band with rate {band.Fare} and limit {band.Limit} is less than band with rate {nextBand.Fare} and limit  {nextBand.Fare}");
                        }
                    }

                    if (band.Limit > nextBand.Limit)
                    {
                        if (IsStrictMode)
                        {
                            throw new ValidationException($"Error: Band limit {band.Limit} with rate {band.Fare} is gretter than band with limit {nextBand.Limit} with rate {nextBand.Fare}");
                        }
                        else
                        {
                            Console.WriteLine($"Warning: Band limit {band.Limit} with rate {band.Fare} is gretter than band with limit {nextBand.Limit} with rate {nextBand.Fare}");
                        }
                    }

                    if (band.Fare == nextBand.Fare && band.Limit == nextBand.Limit)
                    {
                        Console.WriteLine($"Warning: Band limit {band.Limit} with rate {band.Fare} is equal to band {nextBand.Limit} with rate {nextBand.Fare}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to validate rate bands: {ex.Message}");
                throw;
            }
        }
    }
}
