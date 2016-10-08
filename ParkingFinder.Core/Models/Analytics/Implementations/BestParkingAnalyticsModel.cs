using System.Collections.Generic;

namespace ParkingFinder.Core.Models.Analytics.Implementations
{
    /// <summary>
    /// The Best Parking Analytics Model, which stores all information about Best Parking
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class BestParkingAnalyticsModel : IBestParkingAnalyticsModel
    {
        /// <summary>
        /// The Result text as string to be printed out
        /// </summary>
        public string ResultText { get; set; }

        /// <summary>
        /// Data on which the result string is beeing built
        /// </summary>
        public IEnumerable<IParkingFinderAnalyticsData> AnalyticsData { get; set; }
    }
}
