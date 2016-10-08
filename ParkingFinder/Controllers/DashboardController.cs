using System;
using System.Web.Mvc;
using ParkingFinder.Core.Models.Results.Json;
using ParkingFinder.Core.Models.Results.Json.Implementations;
using ParkingFinder.Core.Services.Facade;
using ParkingFinder.Core.Services.Facade.Implementations;

namespace ParkingFinder.Controllers
{
    /// <summary>
    /// Dashboard Controller to handle Requests for the home page
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class DashboardController : Controller
    {   
        #region Properties

        /// <summary>
        /// Reference to the parking finder facade
        /// </summary>
        private readonly IParkingFinderFacade ParkingFinderFacade = new ParkingFinderFacade();

        #endregion

        #region Request Handler

        /// <summary>
        /// Index Action
        /// </summary>
        /// <returns>Action Result to be rendered</returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Requests the Data Overview View
        /// </summary>
        /// <returns>Partial View</returns>
        public ActionResult DataOverview()
        {
            IParkadesModel parkadesModel = this.ParkingFinderFacade.RequestParkades<ParkadesModel>();
            ITrainStationsModel trainstationsModel = this.ParkingFinderFacade.RequestTrainStations<TrainStationsModel>();
            IOccupanciesModel occupanciesModel = this.ParkingFinderFacade.RequestParkadeOccupancies<OccupanciesModel>();
            return PartialView("Partial/DataOverview", new Tuple<IParkadesModel, ITrainStationsModel, IOccupanciesModel>(parkadesModel, trainstationsModel, occupanciesModel));
        }

        #endregion
    }
}