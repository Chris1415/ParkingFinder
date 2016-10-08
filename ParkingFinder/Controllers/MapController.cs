using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using Hach.Library.Configuration.Reader;
using Hach.Library.Extensions;
using Hach.Library.Models.Web.Geolocation;
using NLog;
using ParkingFinder.Core.Models.Request;
using ParkingFinder.Core.Models.Request.Implementations;
using ParkingFinder.Core.Models.Results.Json.Base;
using ParkingFinder.Core.Services.Facade;
using ParkingFinder.Core.Services.Facade.Implementations;
using ParkingFinder.Models;

namespace ParkingFinder.Controllers
{
    /// <summary>
    /// Map Controller to handle Request for Accessing Map
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class MapController : Controller
    {
        #region Properties

        /// <summary>
        /// Reference to Parking Finder Service Facade
        /// </summary>
        private readonly IParkingFinderFacade ParkingFinderFacade = new ParkingFinderFacade();

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = Settings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        #region Request Handler

        /// <summary>
        /// Index Method
        /// </summary>
        /// <returns>Partial View to render</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Requests the Favorite Parkade Coordinates
        /// </summary>
        /// <returns></returns>
        public JsonResult RequestFavoriteCoordinates()
        {
            IParkadeModel parkade = null;
            HttpCookie favoriteParkadeId = HttpContext.Request.Cookies[Labels.CookieFavoriteKey];
            if (favoriteParkadeId != null)
            {
                string idAsString = favoriteParkadeId.Value;
                int id;
                if (int.TryParse(idAsString, out id))
                {
                    parkade = this.ParkingFinderFacade.RequestParkade(new OccupancyRequestParameter()
                    {
                        Id = id
                    });
                }
            }

            return parkade == null
                ? Json(string.Empty)
                : Json(new FavoriteParkadeModel()
                {
                    FavoriteLng = parkade.Longitude,
                    FavoriteLat = parkade.Latitude
                });
        }

        /// <summary>
        /// Request Handler to get All Available parkades within the distance around a given point
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longitude</param>
        /// <param name="rad">Radius</param>
        /// <returns>List of all Elements withing the distance around the center point</returns>
        public JsonResult UpdateParkades(string lat, string lng, string rad)
        {
            double latDouble;
            double lngDouble;
            double radDouble;

            // Check if parameter are valid
            if (lat.IsNullOrEmpty()
                || !double.TryParse(lat, NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture, out latDouble)
                || lng.IsNullOrEmpty()
                || !double.TryParse(lng, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lngDouble)
                || rad.IsNullOrEmpty()
                || !double.TryParse(rad, out radDouble))
            {
                Logger.Error("GeolocationMapper: input is invalid");
                return Json(string.Empty);
            }

            IGeolocationRequestParameter geolocationRequestParameter = new GeolocationRequestParameter()
            {
                Geolocation = new Geoobject(latDouble, lngDouble),
                Distance = radDouble
            };

            IEnumerable<IParkadeModel> parkades = ParkingFinderFacade.RequestParkadesWithinCenter(geolocationRequestParameter);
            return Json(parkades);
        }

        /// <summary>
        /// Request Handler to render the teaser for the favorite
        /// </summary>
        /// <returns>Partial view of info window data</returns>
        public ActionResult FavoriteTeaser()
        {
            IParkadeModel parkade = null;
            HttpCookie favoriteParkadeId = HttpContext.Request.Cookies[Labels.CookieFavoriteKey];
            if (favoriteParkadeId != null)
            {
                string idAsString = favoriteParkadeId.Value;
                int id;
                if (int.TryParse(idAsString, out id))
                {
                    parkade = this.ParkingFinderFacade.RequestParkade(new OccupancyRequestParameter()
                    {
                        Id = id
                    });
                }
            }
           
            return PartialView($"Partial/Favorite/{(parkade == null ? "Empty" : string.Empty)}FavoriteTeaser", parkade);
        }

        /// <summary>
        /// Request Handler to render the info window of a specific parkade 
        /// </summary>
        /// <param name="id">id of the parkade</param>
        /// <returns>Partial view of info window data</returns>
        [HttpPost]
        public ActionResult GetInfoWindow(string id)
        {
            int idInt;
            if (id.IsNullOrEmpty()
                || !int.TryParse(id, out idInt))
            {
                return Json(string.Empty);
            }

            IParkadeModel parkade = this.ParkingFinderFacade.RequestParkade(new OccupancyRequestParameter()
            {
                Id = idInt
            });

            HttpCookie favoriteCookie = HttpContext.Request.Cookies[Labels.CookieFavoriteKey];
            bool viewDataValue = false;
            if (favoriteCookie != null)
            {
                string favoriteId = favoriteCookie.Value;
                int favoriteIdAsInt;
                if (int.TryParse(favoriteId, out favoriteIdAsInt))
                {
                    viewDataValue = favoriteIdAsInt == idInt;
                }              
            }

            ViewData[Labels.CookieViewDataKey] = viewDataValue;
            return PartialView("Partial/InfoWindow/InfoWindow", parkade);
        }

        /// <summary>
        /// Geolocation Mapping function
        /// </summary>
        /// <param name="input">Input to be mapped</param>
        /// <returns>Json with the result geocoordinate</returns>
        [HttpPost]
        public JsonResult GeolocationMapper(string input)
        {
            // Check if parameter is valid
            if (input.IsNullOrEmpty())
            {
                Logger.Error("GeolocationMapper: input is null or empty");
                return Json(string.Empty);
            }

            // Execute the Request with the parameter via service facade
            Geoobject mappedGeoobject = ParkingFinderFacade.PredefinedServiceFacade.GeolocationMappingService.MapInputToGeolocation(input);
            // Return the result -> Geolocation as JSON if sucessfull, otherwise empty JSON string
            return mappedGeoobject.IsValid() ? Json(mappedGeoobject) : Json(string.Empty);
        }

        #endregion

        /// <summary>
        /// Static Labels
        /// </summary>
        public static class Labels
        {
            /// <summary>
            /// Key Value of Cookie Storage to Access Favorite
            /// </summary>
            public const string CookieFavoriteKey = "Favorite";

            /// <summary>
            /// Key Value of View Data to Access Favorite
            /// </summary>
            public const string CookieViewDataKey = "IsFavorite";
        }
    }
}