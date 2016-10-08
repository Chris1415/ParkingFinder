using System.Collections.Generic;
using Hach.Library.Models.Base;
using Newtonsoft.Json;
using ParkingFinder.Core.Models.Results.Json.Implementations;

namespace ParkingFinder.Core.Models.Results.Json
{
    /// <summary>
    /// Interface for the Occupancies Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IOccupanciesModel : IModel
    {
        /// <summary>
        /// List of All Occupancies
        /// </summary>
        [JsonProperty("allocations")]
        IEnumerable<OccupancyModel> Occupancies { get; set; }
    }
}
