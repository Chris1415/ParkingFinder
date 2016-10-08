using Hach.Library.Models.Base;
using Newtonsoft.Json;

namespace ParkingFinder.Core.Models.Results.Json.Base
{
    /// <summary>
    /// Inteface of the Trainstation Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface ITrainStationModel : IModel
    {
        /// <summary>
        /// Type of Train station
        /// </summary>
        [JsonProperty("type")]
        string Type { get; set; }

        /// <summary>
        /// Number of Trainstation
        /// </summary>
        [JsonProperty("bahnhofsNummer")]
        int BahnhofsNummer { get; set; }

        /// <summary>
        /// Name of the City
        /// </summary>
        [JsonProperty("cityTitle")]
        string CityTitle { get; set; }

        /// <summary>
        /// Eva Number
        /// </summary>
        [JsonProperty("evaNummer")]
        int EvaNummer { get; set; }

        /// <summary>
        /// Flag to determine if the trainstation is DB Bahnpark
        /// </summary>
        [JsonProperty("isDbBahnpark")]
        bool IsDbBahnpark { get; set; }

        /// <summary>
        /// Flag to determine if the train station is published
        /// </summary>
        [JsonProperty("isPublished")]
        bool IsPublished { get; set; }

        /// <summary>
        /// Main Pic Id
        /// </summary>
        [JsonProperty("mainPicId")]
        string MainPicId { get; set; }

        /// <summary>
        /// Main Pic Id in English
        /// </summary>
        [JsonProperty("mainPicId_en")]
        string MainPicIdEn { get; set; }

        /// <summary>
        /// The Station 
        /// </summary>
        [JsonProperty("station")]
        string Station { get; set; }
    }
}
