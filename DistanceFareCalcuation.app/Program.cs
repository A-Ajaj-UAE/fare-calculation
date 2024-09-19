﻿using DistanceFareCalcuation.App.Models;
using DistanceFareCalcuation.App.Validator;
using DistanceFareCalcuation.App.Services;

namespace DistanceFareCalcuation.App.DistanceFareCalcuation.app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bands = GetBands();

            Console.WriteLine($"Band Info:");
            foreach (var band in bands)
            {
                Console.WriteLine($"Limit: {band.Limit}, Fare: {band.Fare}, Order: {band.Order}");
            }
            Console.WriteLine("");

            var defaultFare = GetDefaultFare();
            Console.WriteLine($"Default Rate: {defaultFare}");
            Console.WriteLine("");

            Console.WriteLine("Validation Started...");
            var isValid = Validate(bands, defaultFare);
            Console.WriteLine("Validation completed...");
            Console.WriteLine("");

            if (isValid)
            {
                var calculator = new FareCalculator(bands, defaultFare);

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

        //Validate the required inputs
        private static bool Validate(List<Band> bands, decimal defaultFare)
        {
            try
            {
                if (defaultFare <= 0)
                {
                    Console.WriteLine("Error: Default Fare should be gretter than 0");
                    return false;
                }

                return bands.Validate(IsStrictMode: false);
            }
            catch 
            {
                return false;
            }
           
        }

        /// <summary>
        /// Get list of bands from repository 
        /// </summary>
        /// <returns></returns>
        private static List<Band> GetBands()
        {
            return new List<Band>()
            {
                new Band(1, 10m, 0),      // From 0 to 1 mile: 10.00 GBP
                new Band(5, 2m, 1),       // From next 5 miles: 2.00 GBP per mile
                new Band(10, 3m, 2),      // From next 10 miles: 3.00 GBP per mile
            }.OrderBy(b=> b.Order).ToList();
        }

        /// <summary>
        /// Getting the default fare for the calculation
        /// </summary>
        /// <returns></returns>
        private static decimal GetDefaultFare()
        {
            return 1m;
        }

    }
}
