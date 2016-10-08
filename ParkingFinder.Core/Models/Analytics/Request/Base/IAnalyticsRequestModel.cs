using System;
using Hach.Library.Models.Base;

namespace ParkingFinder.Core.Models.Analytics.Request.Base
{
    /// <summary>
    /// Base Request Model for all Analytics Requests
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IAnalyticsRequestModel : IValidity
    {
        /// <summary>
        /// Id of a Parkade
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Date to be analyzed
        /// </summary>
        string Date { get; set; }

        /// <summary>
        /// Data As DateTime to be analyzed
        /// </summary>
        DateTime DateAsDatetime { get; }
    }
}
