namespace ParkingFinder.Core.Models.Results.Json.Base.Implementations
{
    /// <summary>
    /// The Trainstation Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class TrainStationModel : ITrainStationModel
    {
        /// <summary>
        /// Type of Train station
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Number of Trainstation
        /// </summary>
        public int BahnhofsNummer { get; set; }

        /// <summary>
        /// Name of the City
        /// </summary>
        public string CityTitle { get; set; }

        /// <summary>
        /// Eva Number
        /// </summary>
        public int EvaNummer { get; set; }

        /// <summary>
        /// Flag to determine if the trainstation is DB Bahnpark
        /// </summary>
        public bool IsDbBahnpark { get; set; }

        /// <summary>
        /// Flag to determine if the train station is published
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Main Pic Id
        /// </summary>
        public string MainPicId { get; set; }

        /// <summary>
        /// Main Pic Id in English
        /// </summary>
        public string MainPicIdEn { get; set; }

        /// <summary>
        /// The Station 
        /// </summary>
        public string Station { get; set; }
    }
}
