using DistanceFareCalcuation.App.Models;

namespace DistanceFareCalcuation.App.Services
{
    public class FareCalculator
    {
        private List<Band> Bands { get; }
        private decimal DefaultFare {  get; }   

        public FareCalculator(List<Band> bands, decimal defaultFare)
        {
            Bands = bands;
            DefaultFare = defaultFare;
        }

        /// <summary>
        /// Calculate fare based on bands and distance
        /// </summary>
        /// <param name="dictance"></param>
        public decimal Calculate(decimal dictance)
        {
            Console.WriteLine($"Starting fare calculation for {dictance} miles rounded to {(int)Math.Ceiling(dictance)}...");

            var remainingMiles = dictance;
            decimal totalFare = 0m;

            dictance = Math.Ceiling(dictance);

            if (Bands == null || Bands.Count == 0)
            {
                throw new ArgumentException("Bands not found, rate cannot be calcuated");
            }

            if (dictance <= 0)
            {
                throw new ArgumentException("Miles driven cannot be negative");
            }

            if (DefaultFare <= 0)
            {
                throw new ArgumentException("defaultFare cannot be less or equal zero");
            }

            // order based on admin prefrences
            var orderedBands = Bands.OrderBy(b => b.Order).ToList();

            foreach (var band in orderedBands)
            {
                if (remainingMiles <= 0)
                    break;

                var milesInThisBand = Math.Min(remainingMiles, band.Limit);
                totalFare += milesInThisBand * band.Fare;
                remainingMiles -= milesInThisBand;

                Console.WriteLine($"Adding {milesInThisBand} miles at rate {band.Fare} GBP/mile, total so far: {totalFare} GBP");
            }

            if (remainingMiles > 0)
            {
                totalFare += remainingMiles * DefaultFare;

                Console.WriteLine($"Adding {remainingMiles} miles at rate {DefaultFare} GBP/mile, total so far: {totalFare} GBP");
            }

            Console.WriteLine($"Total fare for {dictance} miles: {totalFare} GBP");

            Console.WriteLine($"-------------------------------------------------------------");

            return totalFare;
        }
    }
}
