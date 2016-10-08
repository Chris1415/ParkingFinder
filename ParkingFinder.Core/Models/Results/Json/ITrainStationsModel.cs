using Hach.Library.Models.Base;
using Newtonsoft.Json;
using ParkingFinder.Core.Models.Results.Json.Base.Implementations;

namespace ParkingFinder.Core.Models.Results.Json
{
    /// <summary>
    /// Interfac efot the Train Statios Mode
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface ITrainStationsModel : IModel
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        string Type { get; set; }

        /// <summary>
        /// Version of Interface
        /// </summary>
        [JsonProperty("version")]
        int Version { get; set; }

        /// <summary>
        /// Total Number of Results
        /// </summary>
        [JsonProperty("totalCount")]
        int TotalCount { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty("count")]
        int Count { get; set; }

        /// <summary>
        /// Offeset
        /// </summary>
        [JsonProperty("offset")]
        int Offset { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [JsonProperty("limit")]
        int Limit { get; set; }

        /// <summary>
        /// Train Station Results
        /// </summary>
        [JsonProperty("results")]
        TrainStationModel[] TrainStations { get; set; }
    }
}
