using ParkingFinder.Core.Models.Results.Json.Base.Implementations;

namespace ParkingFinder.Core.Models.Results.Json.Implementations
{
    /// <summary>
    /// Train Stations Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class TrainStationsModel : ITrainStationsModel
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Version of Interface
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Total Number of Results
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Offeset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Train Station Results
        /// </summary>
        public TrainStationModel[] TrainStations { get; set; }
    }
}
