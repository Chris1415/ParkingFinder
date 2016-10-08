using System;
using Hach.Library.Services.Facade;
using ParkingFinder.Core.Models.Analytics;
using ParkingFinder.Core.Models.Analytics.Request.Base;
using ParkingFinder.Core.Services.Analytics;

namespace ParkingFinder.Core.Services.Facade
{
    /// <summary>
    /// Service Facade to handle all Analytics functionality
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IAnalyticsServiceFacade
    {
        #region Properties

        /// <summary>
        /// Reference to Predefined Services
        /// </summary>
        IPredefinedServiceFacade PredefinedServiceFacade { get; }

        /// <summary>
        /// Reference to a Parking Finder Facacde
        /// </summary>
        IParkingFinderFacade ParkingFnderFacade { get; }

        /// <summary>
        /// Reference to a Best Parking Service
        /// </summary>
        IBestParkingService BestParkingService { get; }

        /// <summary>
        /// Reference to a Parking Finder Service
        /// </summary>
        IParkingPredicitionService ParkingPredicitionService { get; }

        #endregion

        #region Interface

        /// <summary>
        /// Analyses the Best Parking times for a given date and a parkade
        /// </summary>
        /// <param name="requestModel">given data in as request model</param>
        /// <returns>The Result of the best parking analytics in a model</returns>
        IBestParkingAnalyticsModel AnalyseBestParking(IAnalyticsRequestModel requestModel);

        /// <summary>
        /// Starts a Thread to Collect Parking Occupancies
        /// </summary>
        /// <returns>Guid of the running thread</returns>
        Guid StartCollectParkingOccupancies();

        /// <summary>
        /// Interface function to stop the current data collection 
        /// </summary>
        /// <param name="guid">guid of the data collection thhread</param>
        void StopCollectParkingOccupancies(Guid guid);

        /// <summary>
        /// Helper to retrieve the current Analytics Model
        /// </summary>
        IParkingFinderAnalyticsModel GetAnalyticsModel { get; }

        #endregion
    }
}
