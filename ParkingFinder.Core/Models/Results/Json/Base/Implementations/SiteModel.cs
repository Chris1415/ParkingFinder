using System;

namespace ParkingFinder.Core.Models.Results.Json.Base.Implementations
{
    /// <summary>
    /// The Site Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    [Serializable]
    public class SiteModel : ISiteModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Site Id
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// Surface Number
        /// </summary>
        public int FlaechenNummer { get; set; }

        /// <summary>
        /// Station Number
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// Site Name
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        public string DisplayName { get; set; }
    }
}
