using System;
using System.Collections.Generic;
using System.Linq;
using Hach.Library.Services.Facade;
using Hach.Library.Services.Facade.Implementations;
using NLog;
using ParkingFinder.Core.Configuration.Reader;
using ParkingFinder.Core.Models;
using ParkingFinder.Core.Models.Analytics;
using ParkingFinder.Core.Models.Analytics.Implementations;
using ParkingFinder.Core.Models.Analytics.Request.Base;
using ParkingFinder.Core.Models.Results.Json;
using ParkingFinder.Core.Models.Results.Json.Implementations;
using ParkingFinder.Core.Services.Analytics;
using ParkingFinder.Core.Services.Analytics.Implementations;
using LibrarySettings = Hach.Library.Configuration.Reader.Settings;

namespace ParkingFinder.Core.Services.Facade.Implementations
{
    /// <summary>
    /// Service Facade to handle all Analytics functionality
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class AnalyticsServiceFacade : IAnalyticsServiceFacade
    {
        #region Properties

        /// <summary>
        /// Reference to Predefined Services
        /// </summary>
        public IPredefinedServiceFacade PredefinedServiceFacade { get; }

        /// <summary>
        /// Reference to a Parking Finder Facacde
        /// </summary>
        public IParkingFinderFacade ParkingFnderFacade { get; }

        /// <summary>
        /// Reference to a Best Parking Service
        /// </summary>
        public IBestParkingService BestParkingService { get; }

        /// <summary>
        /// Reference to a Parking Finder Service
        /// </summary>
        public IParkingPredicitionService ParkingPredicitionService { get; }

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = LibrarySettings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        /// <summary>
        /// c'tor with predefined Service References
        /// </summary>
        public AnalyticsServiceFacade()
        {
            PredefinedServiceFacade = new PredefinedServiceFacade();
            ParkingFnderFacade = new ParkingFinderFacade();
            ParkingPredicitionService = new ParkingPredicitionService();
            BestParkingService = new BestParkingService();
        }

        /// <summary>
        /// c''tor with service references
        /// </summary>
        /// <param name="predefinedServiceFacade">reference to predifined service facade</param>
        /// <param name="parkingFinderFacade">reference parking finder facade</param>
        /// <param name="parkingPredicitionService">reference to prediction service</param>
        /// <param name="bestParkingService">reference to a best parking service</param>
        public AnalyticsServiceFacade(
            IPredefinedServiceFacade predefinedServiceFacade,
            IParkingFinderFacade parkingFinderFacade,
            IParkingPredicitionService parkingPredicitionService,
            IBestParkingService bestParkingService)
        {
            this.PredefinedServiceFacade = predefinedServiceFacade;
            this.ParkingFnderFacade = parkingFinderFacade;
            this.ParkingPredicitionService = parkingPredicitionService;
            this.BestParkingService = bestParkingService;

        }

        #region Interface

        /// <summary>
        /// Analyses the Best Parking times for a given date and a parkade
        /// </summary>
        /// <param name="requestModel">given data in as request model</param>
        /// <returns>The Result of the best parking analytics in a model</returns>
        public IBestParkingAnalyticsModel AnalyseBestParking(IAnalyticsRequestModel requestModel)
        {
            if (!requestModel.IsValid())
            {
                Logger.Error("AnalyseBestParking: Request Model is not valid");
                return new BestParkingAnalyticsModel();
            }

            IParkingFinderAnalyticsModel analyticsModel = this.GetAnalyticsModel;

            string resultString = this.BestParkingService.Execute(
                requestModel.DateAsDatetime, 
                requestModel.Id,
                analyticsModel);

            IEnumerable<IParkingFinderAnalyticsData> analyticsModelForId = analyticsModel
                .AnalyticsData
                .FirstOrDefault(element => element.Key.ToString().Equals(requestModel.Id))
                .Value
                .Where(element => element
                    .TimeStamp
                    .ToString(Labels.DateFormat)
                    .Equals(requestModel
                        .DateAsDatetime
                        .ToString(Labels.DateFormat)))
                .OrderBy(element => element.TimeStamp);

            return new BestParkingAnalyticsModel()
            {
                ResultText = resultString,
                AnalyticsData = analyticsModelForId
            };
        }

        /// <summary>
        /// Starts a Thread to Collect Parking Occupancies
        /// </summary>
        /// <returns>Guid of the running thread</returns>
        public Guid StartCollectParkingOccupancies()
        {
            // Create a new Guid
            Guid threadGuid = Guid.NewGuid();
            // Create a Datacollection Thread
            System.Threading.Thread dataCollectionThread = new System.Threading.Thread(() => DataCollectionLoop(threadGuid));
            // Insert the thread with the guid into the static repository for tracking
            this.PredefinedServiceFacade.ThreadService.AddNewDataCollectionThread(dataCollectionThread, threadGuid);
            // Start the Collection
            dataCollectionThread.Start();
            // Give the GUID back to the UI for further interaction and tracking
            return threadGuid;
        }

        /// <summary>
        /// Interface function to stop the current data collection 
        /// </summary>
        /// <param name="guid">guid of the data collection thhread</param>
        public void StopCollectParkingOccupancies(Guid guid)
        {
            // Save Way to shutdown a thread with the given guid
            this.PredefinedServiceFacade.ThreadService.ForceShutDown(guid);
        }

        /// <summary>
        /// Helper to get the current Analytics Model
        /// </summary>
        public IParkingFinderAnalyticsModel GetAnalyticsModel => (IParkingFinderAnalyticsModel)this.PredefinedServiceFacade
            .CachingService
            .GetModel<ParkingFinderAnalyticsModel>()
            ?? new ParkingFinderAnalyticsModel();

        #endregion

        #region Helper

        /// <summary>
        /// The Loop which gets the Price every x minutes
        /// x can be adjusted via web.config entry "DelayWhileDataCollection"
        /// TODO Raus hier
        /// </summary>
        /// <param name="threadGuid">the guid of the current thread</param>
        private void DataCollectionLoop(Guid threadGuid)
        {
            // Check if the shut down should be forced
            while (!this.PredefinedServiceFacade.ThreadService.ShutDownForced(threadGuid))
            {
                // Get the station data from json dynamically based on the ids of the input
                IOccupanciesModel occupancies = this.ParkingFnderFacade.RequestParkadeOccupancies<OccupanciesModel>();
                IParkingFinderAnalyticsModel analyticsModel = (IParkingFinderAnalyticsModel)this.PredefinedServiceFacade.CachingService.GetModel<ParkingFinderAnalyticsModel>();

                foreach (OccupancyModel occupancy in occupancies.Occupancies)
                {
                    IParkingFinderAnalyticsData specificAnalyticsData = new ParkingFinderAnalyticsData()
                    {
                        OccupancyModel = occupancy,
                        TimeStamp = DateTime.Now
                    };

                    IList<IParkingFinderAnalyticsData> analyticsData;
                    if (analyticsModel.AnalyticsData.TryGetValue(occupancy.Site.Id, out analyticsData))
                    {
                        analyticsData.Add(specificAnalyticsData);
                    }
                    else
                    {
                        analyticsData = new List<IParkingFinderAnalyticsData>()
                        {
                            specificAnalyticsData
                        };

                        analyticsModel.AnalyticsData.Add(occupancy.Site.Id, analyticsData);
                    }
                }

                // Serialize and save the Result
                this.PredefinedServiceFacade.CachingService.SetModel<ParkingFinderAnalyticsModel>(analyticsModel);
                this.PredefinedServiceFacade.SerializationService.Serialize(analyticsModel, Settings.AnaylticsSerializationPath);

                // Sleep in Web.config
                // Sleep again
                System.Threading.Thread.Sleep(600000);
            }
            // Remove this thread from the static list
            Logger.Info("DataCollectionLoop: shutdown triggered");
            this.PredefinedServiceFacade.ThreadService.RemoveDataCollectionThread(threadGuid);
        }

        #endregion
    }
}
