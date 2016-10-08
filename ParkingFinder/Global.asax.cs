using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hach.Library.Services.Facade;
using Hach.Library.Services.Facade.Implementations;
using ParkingFinder.Core.Configuration.Reader;
using ParkingFinder.Core.Models.Analytics;
using ParkingFinder.Core.Models.Analytics.Implementations;
using ParkingFinder.Core.Services.Facade;
using ParkingFinder.Core.Services.Facade.Implementations;

namespace ParkingFinder
{
    /// <summary>
    /// Global Asax Code behind
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class MvcApplication : HttpApplication
    {
        #region Properties

        /// <summary>
        /// Reference to Parking Finder Service Facade
        /// </summary>
        private readonly IPredefinedServiceFacade PredefinedServiceFacade = new PredefinedServiceFacade();

        #endregion

        /// <summary>
        /// Application Start
        /// Is Called one time on Startup
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Deserialize and set the result in cache
            IParkingFinderAnalyticsModel deserializedAnalyticsModel = this.PredefinedServiceFacade
                .SerializationService
                .Deserialize<ParkingFinderAnalyticsModel>(Settings.AnaylticsSerializationPath);
            this.PredefinedServiceFacade
                .CachingService
                .SetModel<ParkingFinderAnalyticsModel>(deserializedAnalyticsModel);
        }
    }
}
