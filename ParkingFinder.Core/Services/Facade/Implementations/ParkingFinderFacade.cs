using System.Collections.Generic;
using System.Linq;
using ParkingFinder.Core.Configuration.Reader;
using Hach.Library.Models.Base;
using Hach.Library.Models.Web.Geolocation;
using Hach.Library.Services.Facade;
using Hach.Library.Services.Facade.Implementations;
using NLog;
using ParkingFinder.Core.Models.Request;
using ParkingFinder.Core.Models.Results.Json;
using ParkingFinder.Core.Models.Results.Json.Base;
using ParkingFinder.Core.Models.Results.Json.Base.Implementations;
using ParkingFinder.Core.Models.Results.Json.Implementations;
using LibrarySettings = Hach.Library.Configuration.Reader.Settings;

namespace ParkingFinder.Core.Services.Facade.Implementations
{
    /// <summary>
    /// Facade to handle all functionality for Parkade finding
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class ParkingFinderFacade : IParkingFinderFacade
    {
        #region Properties

        /// <summary>
        /// Reference to Predefined Services
        /// </summary>
        public IPredefinedServiceFacade PredefinedServiceFacade { get; }

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = LibrarySettings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        #region c'tor

        /// <summary>
        /// Default c'tor with predefined service references
        /// </summary>
        public ParkingFinderFacade()
        {
            PredefinedServiceFacade = new PredefinedServiceFacade();
        }

        /// <summary>
        /// c'tor with service references
        /// </summary>
        /// <param name="predefinedServiceFacade">reference to the predefined service face</param>
        public ParkingFinderFacade(IPredefinedServiceFacade predefinedServiceFacade)
        {
            PredefinedServiceFacade = predefinedServiceFacade;
        }

        #endregion

        #region Interfaces

        /// <summary>
        /// Interface call to get all parkades
        /// </summary>
        /// <typeparam name="T">result model type</typeparam>
        /// <returns>Model with all parkades</returns>
        public T RequestParkades<T>() where T : class, IParkadesModel, new()
        {
            return (T)RequestGeneric<T>(Settings.ParkadeBaseUrl);
        }

        /// <summary>
        /// Interface call to get a single parkade
        ///  </summary>
        /// <returns>a Model with all parkades</returns>
        public IParkadeModel RequestParkade(IOccupancyRequestParameter parameter)
        {
            if (!parameter.IsValid())
            {
                Logger.Error("RequestParkade: Invalid Model");
                return new ParkadeModel();
            }

            IParkadeModel specificParkade = ((IParkadesModel)RequestGeneric<ParkadesModel>(Settings.ParkadeBaseUrl))
                .Parkades
                .FirstOrDefault(element => element.Id.Equals(parameter.Id.ToString()));

            return specificParkade;
        }

        /// <summary>
        /// Interface call to get occupancies of all pakades
        /// </summary>
        /// <typeparam name="T">result model type</typeparam>
        /// <returns>All parkade occupancies</returns>
        public T RequestParkadeOccupancies<T>() where T : class, IOccupanciesModel, new()
        {
            return (T)RequestGeneric<T>(Settings.ParkadeOccupancyBaseUrl);
        }

        /// <summary>
        /// Interface call to get all train stations
        /// </summary>
        /// <typeparam name="T">result model type</typeparam>
        /// <returns>all train stations</returns>
        public T RequestTrainStations<T>() where T : class, ITrainStationsModel, new()
        {
            return (T)RequestGeneric<T>(Settings.TrainStationBaseUrl);
        }

        #endregion

        #region Concrete Interfaces

        /// <summary>
        /// Interface call to get all parkades
        /// </summary>
        /// <returns>a Model with all parkades</returns>
        public IEnumerable<IParkadeModel> RequestParkadesWithinCenter(IGeolocationRequestParameter requestParameter)
        {
            if (!requestParameter.IsValid())
            {
                Logger.Error("RequestParkadesWithinCenter: Invalid Model");
                return new List<ParkadeModel>();
            }

            ParkadesModel allParkades = this.RequestParkades<ParkadesModel>();
            OccupanciesModel allOccupancies = this.RequestParkadeOccupancies<OccupanciesModel>();

            IList<ParkadeModel> parkades = new List<ParkadeModel>();
            // TODO Implement Geolocation Service
            foreach (ParkadeModel parkade in allParkades.Parkades)
            {
                if ((requestParameter.Geolocation.Distance(new Geoobject(parkade.Latitude, parkade.Longitude)) >
                      requestParameter.Distance))
                {
                    continue;
                }

                // TODO Int Parse Prettier
                int parkadeId;
                if (!int.TryParse(parkade.Id, out parkadeId))
                {
                    parkadeId = -1;
                }

                OccupancyModel occupancyOfParkade = allOccupancies
                    .Occupancies
                    .FirstOrDefault(element => element.Site.Id == parkadeId)
                                                    ?? new OccupancyModel();

                parkade.OccupancyModel = occupancyOfParkade;
                parkades.Add(parkade);
            }

            return parkades.AsEnumerable();
            // END
        }

        /// <summary>
        /// Interface call to get the occupancy of a single pakade
        /// </summary>
        /// <returns>One single Parkade with occupany</returns>
        public T RequestParkadeOccupancy<T>(IOccupancyRequestParameter options) where T : class, IOccupancyModel, new()
        {
            if (!options.IsValid())
            {
                Logger.Error("RequestParkadeOccupancy: Model is invalid");
                return new T();
            }

            string composedUrl = this.PredefinedServiceFacade.RequestParameterService.BuildRequestUrl(Settings.ParkadeOccupancyBaseUrl, options);
            string result = this.PredefinedServiceFacade.RequestService.ExecuteRequest(composedUrl);
            T singleOccupancyModel = this.PredefinedServiceFacade.MappingService.MapStringToClass<T>(result);
            return singleOccupancyModel;
        }

        /// <summary>
        /// Requests an interface via the given base url and maps the result in a generic model
        /// </summary>
        /// <typeparam name="T">the generic model</typeparam>
        /// <param name="baseUrl">the base url to call</param>
        /// <returns></returns>
        private IModel RequestGeneric<T>(string baseUrl) where T : class, IModel, new()
        {
            IModel model = this.PredefinedServiceFacade.CachingService.GetModel<T>();
            if (model != default(T))
            {
                return model;
            }

            string result = this.PredefinedServiceFacade.RequestService.ExecuteRequest(baseUrl);
            model = this.PredefinedServiceFacade.MappingService.MapStringToClass<T>(result);
            this.PredefinedServiceFacade.CachingService.SetModel<T>(model);
            return model;
        }

        #endregion
    }
}
