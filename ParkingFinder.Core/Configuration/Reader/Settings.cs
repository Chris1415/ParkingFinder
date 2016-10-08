using System;
using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace ParkingFinder.Core.Configuration.Reader
{
    /// <summary>
    /// Class to handle Application specific Configuration settings
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public static class Settings
    {
        /// <summary>
        /// Access Train Station Base Url
        /// </summary>
        public static string TrainStationBaseUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["TrainStationBaseUrl"] ?? string.Empty;
                }
                catch (ConfigurationErrorsException)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Access Parkade Occupancy Base Url
        /// </summary>
        public static string ParkadeOccupancyBaseUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ParkadeOccupancyBaseUrl"] ?? string.Empty;
                }
                catch (ConfigurationErrorsException)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Access Parkade Base Url
        /// </summary>
        public static string ParkadeBaseUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["ParkadeBaseUrl"] ?? string.Empty;
                }
                catch (ConfigurationErrorsException)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Determines the Path for the Serialization
        /// </summary>
        public static string AnaylticsSerializationPath
        {
            get
            {
                const string defaultValue = "Serialization/AnalyticsDataModel";
                try
                {
                    string pathFromWebRoot = ConfigurationManager.AppSettings["AnaylticsSerializationPathFromWebRoot"] ?? string.Empty;
                    return Path.Combine(HostingEnvironment.ApplicationPhysicalPath, pathFromWebRoot);
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
        }
    }
}
