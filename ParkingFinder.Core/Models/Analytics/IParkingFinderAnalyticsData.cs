using System;
using ParkingFinder.Core.Models.Results.Json;

namespace ParkingFinder.Core.Models.Analytics
{
    /// <summary>
    /// Interface to the Model to hold all essential Analytics Properties
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IParkingFinderAnalyticsData
    {
        /// <summary>
        /// Reference to the Occupancy Model
        /// </summary>
        IOccupancyModel OccupancyModel { get; set; }

        /// <summary>
        /// Public Timestamp where the data was retrieved
        /// </summary>
        DateTime TimeStamp { get; set; }
    }
}
