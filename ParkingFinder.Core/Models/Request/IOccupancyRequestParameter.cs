using Hach.Library.Models.Base;

namespace ParkingFinder.Core.Models.Request
{
    /// <summary>
    /// Occupancy Request Parameter Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IOccupancyRequestParameter : IValidity
    {
        /// <summary>
        /// Id of an parkade
        /// </summary>
        int Id { get; set; }
    }
}
