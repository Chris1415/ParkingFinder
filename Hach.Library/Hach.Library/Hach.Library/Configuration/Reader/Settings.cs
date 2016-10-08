using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Hach.Library.Configuration.Reader
{
    /// <summary>
    /// Class to handle config Settings
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// </author>
    public static class Settings
    {
        /// <summary>
        /// Get the Flag for logging as bool
        /// </summary>
        public static bool Logging
        {
            get
            {
                const bool defaultValue = false;
                try
                {
                    string loggingValue = ConfigurationManager.AppSettings["Logging"] ?? string.Empty;
                    return Convert.ToBoolean(loggingValue);
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// The Step With for Paging
        /// </summary>
        public static int ElementsPerPage
        {
            get
            {
                const int defaultValue = 20;
                try
                {
                    string elements = ConfigurationManager.AppSettings["ElementsPerPage"] ?? string.Empty;
                    int elementsInInt = int.Parse(elements);
                    return elementsInInt <= 0 ? defaultValue : elementsInInt;
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// The Number of Paging Elements to be printed
        /// </summary>
        public static int NumberOfPaingElements
        {
            get
            {
                const int defaultValue = 5;
                try
                {
                    string distance = ConfigurationManager.AppSettings["NumberOfPaingElements"] ?? string.Empty;
                    int distanceInInt = int.Parse(distance);
                    return distanceInInt <= 0 ? defaultValue : distanceInInt;
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// Access the Google Maps Api Key
        /// </summary>
        public static string GoogleMapsApiKey
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["GoogleMapsApiKey"] ?? string.Empty;
                }
                catch (ConfigurationErrorsException)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Access the Base Price Url
        /// </summary>
        public static string BaseGoolgeGeolocationServiceUrl
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["BaseGoogleGeolocationService"] ?? string.Empty;
                }
                catch (ConfigurationErrorsException)
                {
                    return string.Empty;
                }
            }
        }

        public static string Repository
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["Repository"] ?? string.Empty;
                }
                catch (ConfigurationErrorsException)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Access Caching Time
        /// </summary>
        public static int CachingTime
        {
            get
            {
                try
                {
                    string cachingTime = ConfigurationManager.AppSettings["CachingTime"] ?? string.Empty;
                    return int.Parse(cachingTime);
                }
                catch (Exception)
                {
                    return 1440;
                }
            }
        }
    }
}
