using System.Collections.Generic;
using Hach.Library.Models.Base;
using Newtonsoft.Json;
using ParkingFinder.Core.Models.Results.Json.Base.Implementations;

namespace ParkingFinder.Core.Models.Results.Json
{
    /// <summary>
    /// Interface for the parkades Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IParkadesModel : IModel
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        string Type { get; set; }

        /// <summary>
        /// Total Number of Results
        /// </summary>
        [JsonProperty("totalCount")]
        int TotalCount { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty("count")]
        int Count { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        [JsonProperty("offset")]
        int Offset { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [JsonProperty("limit")]
        int Limit { get; set; }

        /// <summary>
        /// List of Single Parkade Results
        /// </summary>
        [JsonProperty("results")]
        IEnumerable<ParkadeModel> Parkades { get; set; }
    }
}
