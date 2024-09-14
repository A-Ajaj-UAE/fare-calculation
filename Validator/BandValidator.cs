using fare_calcuation_app.Models;

namespace fare_calcuation_app.Validator
{
    public static class BandValidator
    {
        /// <summary>
        /// Validate set of bands to be applied to a fare calculation, when allow gap is used the base fare will be calculated
        /// </summary>
        /// <param name="bands">list of bands to validate</param>
        /// <param name="allowGaps"> when used a based fare will be calculated</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool Validate(this List<Band> bands, bool allowGaps = false)
        {
            try
            {
                if (bands.Count == 0)
                {
                    throw new InvalidOperationException("No bands to validate.");
                }

                if (bands.Any(b=> b.MileFrom == b.MileTo))
                {
                    throw new InvalidOperationException($"invalid band, Mile From should be less than Mile To.");
                }


                //order the bands by mile from
                bands = bands.OrderByDescending(b => b.IsBaseBand).ThenBy(b => b.MileFrom).ToList();

                // validate only one base rate band
                if (bands.Count == 0
                    || bands[0].MileFrom != 0
                    || bands.Count(b => b.IsBaseBand) != 1)
                {
                    throw new InvalidOperationException("single rate band must start at 0 and have IsBaseRate set to true.");
                }

                //remove the base rate band
                bands = bands.Where(b => !b.IsBaseBand).ToList();


                // Ensure no overlaps and no gaps
                for (int i = 0; i < bands.Count - 1; i++)
                {
                    var band = bands[i];
                    var nextBand = bands[i + 1];

                    if (band.MileTo > nextBand.MileFrom)
                    {
                        throw new InvalidOperationException($"Band {band.MileFrom} to {band.MileTo} overlaps with band {nextBand.MileFrom} to {nextBand.MileTo}");
                    }

                    if (!allowGaps && band.MileTo < nextBand.MileFrom)
                    {
                        throw new InvalidOperationException($"Band {band.MileFrom} to {band.MileTo} has a gap with band {nextBand.MileFrom} to {nextBand.MileTo}");
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
