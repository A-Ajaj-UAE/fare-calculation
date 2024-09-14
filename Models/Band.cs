namespace fare_calcuation_app.Models
{
    public class Band
    {
        public Band()
        {
            
        }
        public Band(int MileFrom, int MileTo, decimal Rate, bool IsBaseRate = false)
        {
            this.MileFrom = MileFrom;

            this.MileTo = MileTo;

            this.Rate = Rate;

            this.IsBaseBand = IsBaseRate;
        }

        /// <summary>
        /// Mile From
        /// </summary>
        public int MileFrom { get; set; }
        /// <summary>
        /// Mile To
        /// </summary>
        public int MileTo { get; set; }
        /// <summary>
        /// Rate per mile
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// IsBaseBand determin a band having a default rate for mile
        /// </summary>
        public bool IsBaseBand { get; set; }
    }
}
