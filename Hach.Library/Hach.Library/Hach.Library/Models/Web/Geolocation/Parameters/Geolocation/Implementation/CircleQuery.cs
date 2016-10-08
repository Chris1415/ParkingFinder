namespace Hach.Library.Models.Web.Geolocation.Parameters.Geolocation.Implementation
{
    /// <summary>
    /// Query for Circles
    /// Holds a Point and a radius
    /// </summary>
    /// <author>
    /// Christian Hahn, Apr-2016
    /// </author>
    public class CircleQuery : ICircleQuery
    {
        /// <summary>
        /// A Point which represents the center of a circle
        /// </summary>
        public Geoobject CenterPoint { get; }

        /// <summary>
        /// The radius of the circle
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// c'tor with Paramters
        /// </summary>
        /// <param name="centerPoint">Center Point of the Circle</param>
        /// <param name="radius">Radius of the Circle</param>
        public CircleQuery(Geoobject centerPoint, double radius)
        {
            this.CenterPoint = centerPoint;
            this.Radius = radius;
        }

        /// <summary>
        /// Method to check if the input is valid
        /// </summary>
        /// <returns>true if the input is valid, otherwise false</returns>
        public bool IsValid()
        {
            return this.CenterPoint != null
                && this.CenterPoint.IsValid()
                && this.Radius >= 0;
        }
    }
}
