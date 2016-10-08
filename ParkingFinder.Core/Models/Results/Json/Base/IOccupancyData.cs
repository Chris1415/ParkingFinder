using System;
using Hach.Library.Models.Base;
using Newtonsoft.Json;

namespace ParkingFinder.Core.Models.Results.Json.Base
{
    /// <summary>
    /// Interface ot Occupancy Data
    /// </summary>
    public interface IOccupancyData : IModel
    {
        /// <summary>
        /// Determines if the data are valid
        /// </summary>
        [JsonProperty("validData")]
        bool ValidData { get; set; }

        /// <summary>
        /// Time stamp of the data
        /// </summary>
        [JsonProperty("timestamp")]
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Time Segment of the data
        /// </summary>
        [JsonProperty("timeSegment")]
        DateTime TimeSegment { get; set; }

        /// <summary>
        /// Category of Occupancy
        /// </summary>
        [JsonProperty("category")]
        int Category { get; set; }

        /// <summary>
        /// Text for Occupancy
        /// </summary>
        [JsonProperty("text")]
        string CategoryText { get; set; }
    }
}
