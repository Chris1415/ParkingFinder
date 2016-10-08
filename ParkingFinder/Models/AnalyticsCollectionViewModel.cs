using ParkingFinder.Core.Models.Analytics;

namespace ParkingFinder.Models
{
    /// <summary>
    /// The analyics collection view model
    /// </summary>
    /// TODO Put everything, which is calculated in the view as dedicated property here and calculate in controller
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class AnalyticsCollectionViewModel
    {
        /// <summary>
        /// Property to indicate if the collection should be started
        /// </summary>
        public bool ShowStartCollection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IParkingFinderAnalyticsModel AnalyticsModel { get; set; }
    }
}