using Hach.Library.Attributes.RequestParameter;

namespace ParkingFinder.Core.Models.Request.Implementations
{
    /// <summary>
    /// Occupancy Request Parameter Model
    /// </summary>
    /// TODO WIEDER ENTFERNEN
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class TestRequestModel : IOccupancyRequestParameter
    {
        /// <summary>
        /// Id of an parkade
        /// </summary>
        [UrlPath(0)]
        public string ParamCode1 { get; set; }

        [UrlPath(1)]
        public string ParamCode2 { get; set; }

        [KeyValue("Key1")]
        public string ParamKeyValue1 { get; set; }

        [KeyValue("Key2")]
        public string ParamKeyValue2 { get; set; }

        /// <summary>
        /// Determines if the Model is valid
        /// </summary>
        /// <returns>true if the model is valid</returns>
        public bool IsValid()
        {
          return Id > 0;
        }

        public int Id { get; set; }
    }
}