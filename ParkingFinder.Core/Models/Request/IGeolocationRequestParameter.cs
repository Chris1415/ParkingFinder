using Hach.Library.Models.Base;
using Hach.Library.Models.Web.Geolocation;

namespace ParkingFinder.Core.Models.Request
{
    /// <summary>
    /// The Geolocation Request Paramter Class
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IGeolocationRequestParameter : IValidity
    {
        /// <summary>
        /// Geolocation
        /// </summary>
        Geoobject Geolocation { get; set; }

        /// <summary>
        /// Distance
        /// </summary>
        double Distance { get; set; }
    }
}
