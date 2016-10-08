using Hach.Library.Configuration.Reader;
using Hach.Library.Services.Caching;
using Hach.Library.Services.Caching.Implementations;
using Hach.Library.Services.Mapping.Base;
using Hach.Library.Services.Mapping.Geolocation;
using Hach.Library.Services.Mapping.Geolocation.Implementations;
using Hach.Library.Services.Mapping.Json.Implementations;
using Hach.Library.Services.Serialization.Base;
using Hach.Library.Services.Serialization.Json.Implementations;
using Hach.Library.Services.Thread;
using Hach.Library.Services.Thread.Implementations;
using Hach.Library.Services.Web;
using Hach.Library.Services.Web.Implementations;
using NLog;

namespace Hach.Library.Services.Facade.Implementations
{
    /// <summary>
    /// Facade to handle all predefined Services
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class PredefinedServiceFacade : IPredefinedServiceFacade
    {
        #region Properties

        /// <summary>
        /// Reference to the Url Request Parameter Service
        /// </summary>
        public IRequestParameterService RequestParameterService { get; }

        /// <summary>
        /// Reference to the Request Service
        /// </summary>
        public IRequestService RequestService { get; }

        /// <summary>
        /// Reference to a mapping Service
        /// </summary>
        public IMappingService MappingService { get; }

        /// <summary>
        /// Reference to a geolocation mapping Service
        /// </summary>
        public IGeolocationMappingService GeolocationMappingService { get; }

        /// <summary>
        /// Reference to a Caching Service
        /// </summary>
        public ICachingService CachingService { get; }

        /// <summary>
        /// Reference to a Serialization Service
        /// </summary>
        public ISerializationService SerializationService { get; }

        /// <summary>
        /// Reference to a Thread Service
        /// </summary>
        public IThreadService ThreadService { get; }

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = Settings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        #region c'tor

        /// <summary>
        /// Default c'tor with predefined Services
        /// </summary>
        public PredefinedServiceFacade()
        {
            RequestService = new RequestService();
            RequestParameterService = new UrlRequestParameterService();
            MappingService = new JsonMapperService();
            GeolocationMappingService = new GoogleGeolocationMappingService();
            CachingService = new CachingService();
            SerializationService = new JsonSerializationService();
            ThreadService = new ThreadService();
        }

        /// <summary>
        /// Parametrized c'tor with variable services
        /// </summary>
        /// <param name="requestService">Reference to the request service</param>
        /// <param name="requestParameterService">refernece to the request paramter service</param>
        /// <param name="mappingService">reference to a mapping service</param>
        /// <param name="geolocationMappingService">Reference to a geolocation mapping service</param>
        /// <param name="cachingService">Reference to a caching service</param>
        /// <param name="serializationService">Reference to a serialization service</param>
        /// <param name="threadService">Reference to a thread service</param>
        public PredefinedServiceFacade(
            IRequestService requestService,
            IRequestParameterService requestParameterService,
            IMappingService mappingService,
            IGeolocationMappingService geolocationMappingService,
            ICachingService cachingService,
            ISerializationService serializationService,
            IThreadService threadService)
        {
            RequestParameterService = requestParameterService;
            RequestService = requestService;
            MappingService = mappingService;
            GeolocationMappingService = geolocationMappingService;
            CachingService = cachingService;
            SerializationService = serializationService;
            ThreadService = threadService;
        }

        #endregion
    }
}
