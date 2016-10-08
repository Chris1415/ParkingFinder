namespace Hach.Library.Models.Web.Geolocation.Parameters.Geolocation
{
    /// <summary>
    /// Query for Circles
    /// Holds a Point and a radius
    /// </summary>
    /// <author>
    /// Christian Hahn, Apr-2016
    /// </author>
    public interface ICircleQuery : IQuery
    {
        /// <summary>
        /// A Point which represents the center of a circle
        /// </summary>
        Geoobject CenterPoint { get; }
       
        /// <summary>
        /// The radius of the circle
        /// </summary>
        double Radius { get; }
    }
}
