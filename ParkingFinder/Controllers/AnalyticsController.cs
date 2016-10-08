using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hach.Library.Configuration.Reader;
using Hach.Library.Extensions;
using NLog;
using ParkingFinder.Core.Models.Analytics;
using ParkingFinder.Core.Models.Analytics.Request.Implementations;
using ParkingFinder.Core.Services.Facade;
using ParkingFinder.Core.Services.Facade.Implementations;
using ParkingFinder.Models;

namespace ParkingFinder.Controllers
{
    /// <summary>
    /// The Analytics Controller to handle all Functions and Request regarding Analytics
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class AnalyticsController : Controller
    {
        #region Properties

        /// <summary>
        /// Reference to Parking Finder Service Facade
        /// </summary>
        private readonly IAnalyticsServiceFacade AnalyticsServiceFacade = new AnalyticsServiceFacade();

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = Settings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        #region Request Handler

        /// <summary>
        /// Requests the Data Collection View
        /// </summary>
        /// <returns>Partial View</returns>
        public ActionResult DataCollectionView()
        {
            return PartialView("Partial/DataCollectionView", this.GetAnalyticsCollectionViewModel);

        }

        /// <summary>
        /// Requests the Data Collection View
        /// </summary>
        /// <returns>Partial View</returns>
        public ActionResult BestParkingFormView()
        {
            IParkingFinderAnalyticsModel analyticsModel = this.AnalyticsServiceFacade.GetAnalyticsModel;
            IEnumerable<SelectListItem> availableDate = BuildAvailableDateDropDown(analyticsModel);
            IEnumerable<SelectListItem> availableParkade = BuildAvailableParkadeDropDown(analyticsModel);

            return PartialView("Partial/BestParkingForm", BuildAnalyticsBestParkingFormViewModel(availableDate, availableParkade));
        }

        /// <summary>
        /// Requests the Data Collection View
        /// </summary>
        /// <returns>Partial View</returns>
        [HttpPost]
        public ActionResult SearchForBestParking(BestParkingRequestModel requestModel)
        {      
            return PartialView("Partial/BestParkingFormResult", this.AnalyticsServiceFacade.AnalyseBestParking(requestModel));
        }

        /// <summary>
        /// Requests the Data Collection View
        /// </summary>
        /// <returns>Partial View</returns>
        public ActionResult ParkingPredictionFormView()
        {
            return PartialView("Partial/ParkingPredicitionForm");

        }

        /// <summary>
        /// Request Handler to Start / Stop Analytics Collection
        /// </summary>
        /// <param name="start">Flag to determine if the collection should be started or stoped</param>
        /// <returns>True if the Start was successfull other wise string.empty</returns>
        [HttpPost]
        public JsonResult CollectAnalytics(string start)
        {
            if (!start.IsNullOrEmpty() && start.Equals(bool.TrueString.ToLower()))
            {
                Guid newGuid = this.AnalyticsServiceFacade.StartCollectParkingOccupancies();
                Session[Labels.ThreadGuidIdentifier] = newGuid;
                return Json(true);
            }

            object threadGuid = Session[Labels.ThreadGuidIdentifier];
            if (threadGuid == null)
            {
                Logger.Error("CollectAnalytics: Thread Guid is null");
                return Json(false);
            }

            Guid mappedGuid = (Guid)threadGuid;
            this.AnalyticsServiceFacade.StopCollectParkingOccupancies(mappedGuid);
            Session[Labels.ThreadGuidIdentifier] = null;
            return Json(false);
        }

        #endregion

        #region Helper 

        /// <summary>
        /// Builds the available dates model with the a default entry
        /// </summary>
        /// <param name="analyticsModes">analytics Model</param>
        /// <returns>Filled Available Dates Model</returns>
        private static IEnumerable<SelectListItem> BuildAvailableDateDropDown(IParkingFinderAnalyticsModel analyticsModes)
        {
            IList<SelectListItem> availableDates = BuildAvailableDatesEntries(analyticsModes);
            BuildDefaultEntry(ref availableDates, "Choose a Date", string.Empty);
            return availableDates.GroupBy(x => x.Value).Select(g => g.First());
        }

        /// <summary>
        /// Builds the available parkades model with the a default entry
        /// </summary>
        /// TODO Refactor
        /// <param name="analyticsModes">analytics Model</param>
        /// <returns>Filled Available Dates Model</returns>
        private static IEnumerable<SelectListItem> BuildAvailableParkadeDropDown(IParkingFinderAnalyticsModel analyticsModes)
        {
            IList<SelectListItem> availableDates = BuildAvailableParkadesEntries(analyticsModes);
            BuildDefaultEntry(ref availableDates, "Choose a Parkade", string.Empty);
            return availableDates.GroupBy(x => x.Value).Select(g => g.First());
        }

        /// <summary>
        /// Builds the DropDown List Entries for the available Parkades
        /// </summary>
        /// TODO Refactor
        /// <param name="analyticsModel">current analytics Model</param>
        /// <returns>Filled Select List for Dropdown</returns>
        private static IList<SelectListItem> BuildAvailableParkadesEntries(IParkingFinderAnalyticsModel analyticsModel)
        {
            return (from analyticsElement in analyticsModel.AnalyticsData
                    from analyticsElementEntry in analyticsElement.Value
                    select new Tuple<int, string>(analyticsElementEntry.OccupancyModel.Site.Id, analyticsElementEntry.OccupancyModel.Site.DisplayName)                
               into dateOfData
               orderby dateOfData.Item2
                    select new SelectListItem()
                    {
                        Value = dateOfData.Item1.ToString(),
                        Text = dateOfData.Item2.ToUtf8(),
                        Disabled = false,
                    }).ToList();
        }

        /// <summary>
        /// Builds the complete List for available Dates
        /// </summary>
        /// <param name="analyticsModel">analytics Model</param>
        /// <returns>Filled Available Dates Model</returns>
        private static IList<SelectListItem> BuildAvailableDatesEntries(IParkingFinderAnalyticsModel analyticsModel)
        {
            return (from analyticsElement in analyticsModel.AnalyticsData
                    from analyticsElementEntry in analyticsElement.Value
                    select analyticsElementEntry.TimeStamp.ToString(Labels.DateFormat)
                into dateOfData
                    select new SelectListItem()
                    {
                        Value = dateOfData,
                        Text = dateOfData,
                        Disabled = false,
                    }).ToList();
        }

        /// <summary>
        /// Builds the default Entry for the available Dates
        /// </summary>
        /// <param name="availableDates">available dates List</param>
        private static void BuildDefaultEntry(ref IList<SelectListItem> availableDates, string text, string value)
        {
            availableDates.Add(new SelectListItem()
            {
                Text = text,
                Value = value,
                Selected = true
            });
        }

        /// <summary>
        /// Builds the View Model for the Best Parking Form
        /// </summary>
        /// <param name="availableDates">all available dates</param>
        /// <param name="availableParkades">all available parkades</param>
        /// <returns>The Best Parking Form View Model</returns>
        private static AnalyticsBestParkingFormViewModel BuildAnalyticsBestParkingFormViewModel(
            IEnumerable<SelectListItem> availableDates,
            IEnumerable<SelectListItem> availableParkades)
        {
            return new AnalyticsBestParkingFormViewModel()
            {
                AvailableDates = availableDates,
                AvailableParkades = availableParkades
            };
        }

        /// <summary>
        /// Helper to create the Analytics collection view model
        /// </summary>
        public AnalyticsCollectionViewModel GetAnalyticsCollectionViewModel
        {
            get
            {
                object sessionEntry = Session[Labels.ThreadGuidIdentifier];
                return new AnalyticsCollectionViewModel()
                {
                    AnalyticsModel = this.AnalyticsServiceFacade.GetAnalyticsModel,
                    ShowStartCollection = sessionEntry == null,
                };
            }
        }

        #endregion

        /// <summary>
        /// Static Labels
        /// </summary>
        public static class Labels
        {
            /// <summary>
            /// Thread Guid Identifier for the Session Storage
            /// </summary>
            public const string ThreadGuidIdentifier = "ThreadGuid";

            /// <summary>
            /// Date Format to get the german Date 
            /// </summary>
            public const string DateFormat = "dd.MM.yyyy";
        }
    }
}