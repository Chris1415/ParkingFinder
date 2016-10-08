using System;
using ParkingFinder.Core.Models.Results.Json.Base;
using ParkingFinder.Core.Models.Results.Json.Base.Implementations;

namespace ParkingFinder.Core.Models.Results.Json.Implementations
{
    /// <summary>
    /// The Occupancy Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>#
    [Serializable]
    public class OccupancyModel : IOccupancyModel
    {
        #region Properties

        /// <summary>
        /// Site Reference
        /// </summary>
        public SiteModel Site { get; set; }

        /// <summary>
        /// Occupancy Data Reference
        /// </summary>
        public OccupancyData OccupancyData { get; set; }

        #endregion

        #region Additional Properties

        /// <summary>
        /// Model for Marker Data
        /// Holds the images of the diffrent Icons
        /// </summary>
        public IMarkerData MarkerData
        {
            get
            {
                if (this.OccupancyData == null)
                {
                    this.OccupancyData = new OccupancyData();
                }
                return new MarkerData(this.OccupancyData.Category);
            }
        }     

        #endregion
    }
}
