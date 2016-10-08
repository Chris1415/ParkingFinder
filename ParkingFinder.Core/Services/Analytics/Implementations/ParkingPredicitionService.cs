using System;
using ParkingFinder.Core.Models.Analytics;

namespace ParkingFinder.Core.Services.Analytics.Implementations
{
    /// <summary>
    /// Parking Predicition Service
    /// Service to predict the the Free Parking Space 
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class ParkingPredicitionService : IParkingPredicitionService
    {
        /// <summary>
        /// Base Execute Method for all Analytics Services
        /// </summary>
        /// <param name="requestedDateTime">the reuested Date time to get analytics data</param>
        /// <param name="requestedId">the requested id to get data for</param>
        /// <param name="analyticsModel">Reference to the current Analytics data</param>
        /// <returns>output string with the analytics result</returns>
        public string Execute(DateTime requestedDateTime, string requestedId, IParkingFinderAnalyticsModel analyticsModel)
        {
            throw new NotImplementedException();
        }
    }
}
