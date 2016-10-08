using System;
using ParkingFinder.Core.Models.Results.Json;
using ParkingFinder.Core.Models.Results.Json.Implementations;
namespace ParkingFinder.Core.Models.Analytics.Implementations
{
    /// <summary>
    /// Model to hold all essential Analytics Properties
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    [Serializable]
    public class ParkingFinderAnalyticsData : IParkingFinderAnalyticsData
    {
        #region Properties

        /// <summary>
        /// Reference to the Occupancy Model
        /// </summary>
        public IOccupancyModel OccupancyModel { get; set; }


        /// <summary>
        /// Public Timestamp where the data was retrieved
        /// </summary>
        public DateTime TimeStamp { get; set; }

        #endregion

        /// <summary>
        /// c'tor
        /// </summary>
        public ParkingFinderAnalyticsData()
        {
            OccupancyModel = new OccupancyModel();
        }
    }
}
