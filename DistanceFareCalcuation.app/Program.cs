using DistanceFareCalcuation.App.Models;
using DistanceFareCalcuation.App.Validator;
using DistanceFareCalcuation.App.Services;

namespace DistanceFareCalcuation.App.DistanceFareCalcuation.app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bands = GetBands();

            var isValid = ValidateBands(bands);

            if (isValid)
            {
                var calculator = new FareCalculator(bands);

                // Test task scenarios
                calculator.Calculate(6m);
                calculator.Calculate(12m);
                calculator.Calculate(100m);
            }
            else
            {
                Console.WriteLine("Bands are not valid.");
            }

            Console.WriteLine("Execution completed, press any key to exit");
            Console.ReadKey();
        }

        private static bool ValidateBands(List<Band> bands)
        {
            return bands.Validate();
        }

        private static List<Band> GetBands()
        {
            //Define bands with start and end points
            return new List<Band>()
            {
                new Band(0, 1, 10m),      // From 0 to 1 mile: 10.00 GBP
                new Band(1, 6, 2m),       // From 1 to 6 miles: 2.00 GBP per mile
                new Band(6, 16, 3m),      // From 6 to 16 miles: 3.00 GBP per mile
                new Band(0, int.MaxValue, 1m, true) // From 16 miles onward: 1.00 GBP per mile
            }
            .OrderByDescending(b => b.IsBaseBand)
            .ThenBy(b => b.MileFrom).ToList();
        }

    }
}
