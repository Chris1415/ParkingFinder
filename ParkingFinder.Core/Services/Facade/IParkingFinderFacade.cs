using System.Collections.Generic;
using Hach.Library.Services.Facade;
using ParkingFinder.Core.Models.Request;
using ParkingFinder.Core.Models.Results.Json;
using ParkingFinder.Core.Models.Results.Json.Base;

namespace ParkingFinder.Core.Services.Facade
{
    /// <summary>
    /// Facade to handle all functionality for Parkade finding
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IParkingFinderFacade
    {
        #region Properties

        /// <summary>
        /// Reference to Predefined Services
        /// </summary>
        IPredefinedServiceFacade PredefinedServiceFacade { get; }

        #endregion
        #region Generic Interfaces

        /// <summary>
        /// Interface call to get all parkades
        /// </summary>
        /// <typeparam name="T">Result Type</typeparam>
        /// <returns>a Model with all parkades</returns>
        T RequestParkades<T>() where T : class, IParkadesModel, new();

        /// <summary>
        /// Interface call to get a single parkade
        ///  </summary>
        /// <returns>a Model with all parkades</returns>
        IParkadeModel RequestParkade(IOccupancyRequestParameter parameter);

        /// <summary>
        /// Interface call to get occupancies of all pakades
        /// </summary>
        /// <typeparam name="T">result model type</typeparam>
        /// <returns>All parkade occupancies</returns>
        T RequestParkadeOccupancies<T>() where T : class, IOccupanciesModel, new();

        /// <summary>
        /// Interface call to get all train stations
        /// </summary>
        /// <typeparam name="T">result model type</typeparam>
        /// <returns>all train stations</returns>
        T RequestTrainStations<T>() where T : class, ITrainStationsModel, new();

        #endregion

        #region Concrete Interfaces

        /// <summary>
        /// Interface call to get all parkades
        /// </summary>
        /// <returns>a Model with all parkades</returns>
        IEnumerable<IParkadeModel> RequestParkadesWithinCenter(IGeolocationRequestParameter geolocationRequestParamter);

        /// <summary>
        /// Interface call to get the occupancy of a single pakade
        /// </summary>
        /// <typeparam name="T">result model type</typeparam>
        /// <returns>One single Parkade with occupany</returns>
        T RequestParkadeOccupancy<T>(IOccupancyRequestParameter options) where T : class, IOccupancyModel, new();

        #endregion
    }
}
