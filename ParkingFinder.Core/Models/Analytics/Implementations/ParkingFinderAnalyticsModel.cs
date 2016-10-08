using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ParkingFinder.Core.Models.Analytics.Implementations
{
    /// <summary>
    /// Model to store all gathered data for analytics
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    [Serializable]
    public class ParkingFinderAnalyticsModel : IParkingFinderAnalyticsModel
    {
        #region Properties

        /// <summary>
        /// The Specific Analytics Data
        /// </summary>
        public IDictionary<int, IList<IParkingFinderAnalyticsData>> AnalyticsData { get; set; }

        #endregion 

        /// <summary>
        /// c'tor
        /// </summary>
        public ParkingFinderAnalyticsModel()
        {
            AnalyticsData = new ConcurrentDictionary<int, IList<IParkingFinderAnalyticsData>>();
        }
    }
}
