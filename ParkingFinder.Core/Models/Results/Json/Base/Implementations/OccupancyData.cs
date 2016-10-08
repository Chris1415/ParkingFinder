using System;

namespace ParkingFinder.Core.Models.Results.Json.Base.Implementations
{
    /// <summary>
    /// Occupancy Data Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    [Serializable]
    public class OccupancyData : IOccupancyData
    {
        /// <summary>
        /// Determines if the data are valid
        /// </summary>
        public bool ValidData { get; set; }

        /// <summary>
        /// Time stamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Time Segment of the data
        /// </summary>
        public DateTime TimeSegment { get; set; }

        /// <summary>
        /// Category of Occupancy
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// Text for Occupancy
        /// </summary>
        public string CategoryText { get; set; }

    }
}
