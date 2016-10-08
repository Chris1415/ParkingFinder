using System.Collections.Generic;
using Hach.Library.Models.Base;

namespace ParkingFinder.Core.Models.Analytics
{
    /// <summary>
    /// Interface for the Model to store all gathered data for analytics
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface    IParkingFinderAnalyticsModel : IModel
    {
        /// <summary>
        /// The Specific Analytics Data
        /// </summary>
        IDictionary<int, IList<IParkingFinderAnalyticsData>> AnalyticsData { get; set; }
    }
}
