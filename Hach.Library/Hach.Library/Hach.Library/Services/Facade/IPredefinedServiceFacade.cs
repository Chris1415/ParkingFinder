using Hach.Library.Services.Caching;
using Hach.Library.Services.Mapping.Base;
using Hach.Library.Services.Mapping.Geolocation;
using Hach.Library.Services.Serialization.Base;
using Hach.Library.Services.Thread;
using Hach.Library.Services.Web;

namespace Hach.Library.Services.Facade
{
    /// <summary>
    /// Facade to handle all predefined Services
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IPredefinedServiceFacade
    {
        #region Properties

        /// <summary>
        /// Reference to the Url Request Parameter Service
        /// </summary>
        IRequestParameterService RequestParameterService { get; }

        /// <summary>
        /// Reference to the Request Service
        /// </summary>
        IRequestService RequestService { get; }

        /// <summary>
        /// Reference to a mapping Service
        /// </summary>
        IMappingService MappingService { get; }

        /// <summary>
        /// Reference to a geolocation mapping Service
        /// </summary>
        IGeolocationMappingService GeolocationMappingService { get; }

        /// <summary>
        /// Reference to a Caching Service
        /// </summary>
        ICachingService CachingService { get; }

        /// <summary>
        /// Reference to a Serialization Service
        /// </summary>
        ISerializationService SerializationService { get; }

        /// <summary>
        /// Reference to a Thread Service
        /// </summary>
        IThreadService ThreadService { get; }

        #endregion 
    }
}
