using System.Collections.Generic;
using ParkingFinder.Core.Models.Results.Json.Base.Implementations;

namespace ParkingFinder.Core.Models.Results.Json.Implementations
{
    /// <summary>
    /// The Json Parkades Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class ParkadesModel : IParkadesModel
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Total Number of Results
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// List of Single Parkade Results
        /// </summary>
        public IEnumerable<ParkadeModel> Parkades { get; set; }
    }
}
