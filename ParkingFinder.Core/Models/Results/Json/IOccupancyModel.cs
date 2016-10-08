using Hach.Library.Models.Base;
using Newtonsoft.Json;
using ParkingFinder.Core.Models.Results.Json.Base;
using ParkingFinder.Core.Models.Results.Json.Base.Implementations;

namespace ParkingFinder.Core.Models.Results.Json
{
    /// <summary>
    /// Interface of the Occupancy Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IOccupancyModel : IModel
    {
        #region Properties

        /// <summary>
        /// Site Reference
        /// </summary>
        [JsonProperty("site")]
        SiteModel Site { get; set; }

        /// <summary>
        /// Occupancy Data Reference
        /// </summary>
        [JsonProperty("allocation")]
        OccupancyData OccupancyData { get; set; }

        #endregion

        #region Additional Properties

        /// <summary>
        /// Model for Marker Data
        /// Holds the images of the diffrent Icons
        /// </summary>
        IMarkerData MarkerData { get; }

        #endregion
    }
}
