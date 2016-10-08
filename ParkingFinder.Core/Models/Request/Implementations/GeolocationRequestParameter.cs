using Hach.Library.Models.Web.Geolocation;

namespace ParkingFinder.Core.Models.Request.Implementations
{
    /// <summary>
    /// The Geolocation Request Paramter Class
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class GeolocationRequestParameter : IGeolocationRequestParameter
    {
        /// <summary>
        /// Geolocation
        /// </summary>
        public Geoobject Geolocation { get; set; }

        /// <summary>
        /// Distance
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Determines if the current model is valid
        /// </summary>
        /// <returns>true if the model is valid</returns>
        public bool IsValid()
        {
          return Geolocation.IsValid() && Distance >= 0;
        }
    }
}
