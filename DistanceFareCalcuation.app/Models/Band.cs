namespace DistanceFareCalcuation.App.Models
{
    public class Band
    {
        public Band(int limit, decimal rate, int order)
        {
            Limit = limit;
            Fare = rate;
            Order = order;
        }

        public int Limit { get; set; }  // The maximum number of miles for this rate band
        public decimal Fare { get; set; } // The fare per mile for this band
        public int Order { get; set; }
    }
}
