using fare_calcuation_app.Models;

namespace fare_calcuation_app.Services
{
    public class FareCalculator
    {
        private List<Band> Bands { get; }

        public FareCalculator(List<Band> bands)
        {
            Bands = bands;
        }

        /// <summary>
        /// Calculate fare based on bands and distance
        /// </summary>
        /// <param name="dictance"></param>
        public decimal Calculate(decimal dictance)
        {
            Console.WriteLine($"Starting fare calculation for {dictance} miles rounded to {(int)Math.Ceiling(dictance)}...");

            dictance = Math.Ceiling(dictance);

            if (Bands == null) 
            {
                throw new ArgumentException("Bands not found, rate cannot be calcuated");
            }

            if (dictance < 0)
            {
                throw new ArgumentException("Miles driven cannot be negative");
            }

            decimal totalFare = 0;
            decimal totalCoveredDistance = 0;


            var baseRate = Bands.Single(b=> b.IsBaseBand);

            foreach (var band in Bands)
            {
                
                if (dictance <= band.MileFrom)
                    continue;

                if(band.IsBaseBand)
                {
                    continue;
                }
                else
                {
                    decimal milesInThisBand = Math.Min(dictance, band.MileTo) - band.MileFrom;

                    totalFare += milesInThisBand * band.Rate;
                    totalCoveredDistance += milesInThisBand;

                    // Log the calculation per band
                    Console.WriteLine($"Adding {milesInThisBand} miles at rate {band.Rate} GBP/mile, total so far: {totalFare} GBP");
                }
            }

            totalFare += ((dictance - totalCoveredDistance) * baseRate.Rate);
            Console.WriteLine($"Adding remaining miles {(dictance - totalCoveredDistance)} at base rate {baseRate.Rate} GBP/mile, total so far: {totalFare} GBP");


            Console.WriteLine($"Total fare for {dictance} miles: {totalFare} GBP");


            Console.WriteLine($"-------------------------------------------------------------");


            return totalFare;
        }
    }
}
