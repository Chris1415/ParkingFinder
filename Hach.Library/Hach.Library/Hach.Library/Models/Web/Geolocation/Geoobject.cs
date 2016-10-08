using System;
using System.Globalization;
using Hach.Library.Extensions;
using Hach.Library.Models.Base;

namespace Hach.Library.Models.Web.Geolocation
{
    /// <summary>
    /// Geoobject Class
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// </author>
    public class Geoobject : IEquatable<Geoobject>, IValidity
    {
        #region c'tor


        /// <summary>
        /// c'tor default
        /// Invalid Geodata
        /// </summary>
        public Geoobject()
        {
            Lat = int.MinValue;
            Lng = int.MinValue;
        }

        /// <summary>
        /// c'tor with double input
        /// </summary>
        /// <param name="lat">latitude in string</param>
        /// <param name="lng">longtitude in string</param>
        public Geoobject(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        /// <summary>
        /// c'tor with string input
        /// </summary>
        /// <param name="lat">latitude in string</param>
        /// <param name="lng">longtitude in string</param>
        public Geoobject(string lat, string lng)
        {
            double latDouble;
            double lngDouble;

            if (lat.IsNullOrEmpty()
                || lng.IsNullOrEmpty()
                || !double.TryParse(lat, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out latDouble)
                || !double.TryParse(lng, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lngDouble))
            {
                Lat = int.MinValue;
                Lng = int.MinValue;
                return;
            }

            Lat = latDouble;
            Lng = lngDouble;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Helper to map Degree to Radians
        /// </summary>
        /// <param name="deg">degree</param>
        /// <returns>Radians</returns>
        public double DegreeToRad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// Helper to Map Radians to Degree
        /// </summary>
        /// <param name="rad">Radians</param>
        /// <returns>Degree</returns>
        public double RadToDegree(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        /// <summary>
        /// Calculates the Distance between to geoobjects
        /// </summary>
        /// <param name="toCompare">the geoobject to compare</param>
        /// <param name="unit">the Unit 'K' Kilometer or 'M' Miles </param>
        /// <returns></returns>
        public double Distance(Geoobject toCompare, char unit = 'K')
        {
            double theta = this.Lng - toCompare.Lng;
            double dist = Math.Sin(DegreeToRad(this.Lat)) * Math.Sin(DegreeToRad(toCompare.Lat))
                + Math.Cos(DegreeToRad(this.Lat)) * Math.Cos(DegreeToRad(toCompare.Lat)) * Math.Cos(DegreeToRad(theta));
            dist = Math.Acos(dist);
            dist = RadToDegree(dist);
            dist = dist * 60 * 1.1515;
            switch (unit)
            {

                case 'N':
                    dist = dist * 0.8684;
                    break;
                default:
                    dist = dist * 1.609344;
                    break;
            }

            return (dist);
        }

        #region Helper 

        #endregion

        /// <summary>
        /// Latitude
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Longtitude
        /// </summary>
        public double Lng { get; set; }

        #endregion Properties

        #region Helper

        /// <summary>
        /// Validates the given Model
        /// </summary>
        public bool IsValid()
        {
            return Lat >= -90
                && Lat <= 90
                && Lng >= -180
                && Lng <= 180;
        }

        #endregion

        /// <summary>
        /// Equality between geolocations
        /// </summary>
        /// <param name="other">other geolocation</param>
        /// <returns>true if the geolocation has the same lat and lng, otherwise false</returns>
        public bool Equals(Geoobject other)
        {
            return this.Lat.Equals(other.Lat)
                   && this.Lng.Equals(other.Lng);
        }
    }
}
