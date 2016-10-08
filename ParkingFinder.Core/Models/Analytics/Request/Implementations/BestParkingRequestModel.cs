using Hach.Library.Extensions;
using System;

namespace ParkingFinder.Core.Models.Analytics.Request.Implementations
{
    /// <summary>
    /// Best Parking Request Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class BestParkingRequestModel : IBestParkingRequestModel
    {
        /// <summary>
        /// Determines if the current model is valid
        /// </summary>
        /// <returns>true, if the model is valid</returns>
        public bool IsValid()
        {
            DateTime parsedDateTime;
            return !Id.IsNullOrEmpty() || !Date.IsNullOrEmpty() || DateTime.TryParse(Date, out parsedDateTime);
        }

        /// <summary>
        /// Id of a Parkade
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Date to be analyzed
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Data As DateTime to be analyzed
        /// </summary>
        public DateTime DateAsDatetime
        {
            get
            {
                DateTime parsedDateTime;
                return DateTime.TryParse(Date, out parsedDateTime) ? parsedDateTime : DateTime.MinValue;
            }
        }
    }
}
