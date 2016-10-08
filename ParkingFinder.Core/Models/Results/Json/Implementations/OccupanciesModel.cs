using System.Collections.Generic;

namespace ParkingFinder.Core.Models.Results.Json.Implementations
{
    /// <summary>
    /// Occupancies Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class OccupanciesModel : IOccupanciesModel
    {
        /// <summary>
        /// List of All Occupancies
        /// </summary>
        public IEnumerable<OccupancyModel> Occupancies { get; set; }
    }
}
