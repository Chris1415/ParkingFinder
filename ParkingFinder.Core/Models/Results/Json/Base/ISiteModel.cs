using Hach.Library.Models.Base;
using Newtonsoft.Json;

namespace ParkingFinder.Core.Models.Results.Json.Base
{
    public interface ISiteModel : IModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        int Id { get; set; }

        /// <summary>
        /// Site Id
        /// </summary>
        [JsonProperty("siteId")]
        int SiteId { get; set; }

        /// <summary>
        /// Surface Number
        /// </summary>
        [JsonProperty("flaechenNummer")]
        int FlaechenNummer { get; set; }

        /// <summary>
        /// Station Number
        /// </summary>
        [JsonProperty("stationName")]
        string StationName { get; set; }

        /// <summary>
        /// Site Name
        /// </summary>
        [JsonProperty("siteName")]
        string SiteName { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        [JsonProperty("displayName")]
        string DisplayName { get; set; }
    }
}
