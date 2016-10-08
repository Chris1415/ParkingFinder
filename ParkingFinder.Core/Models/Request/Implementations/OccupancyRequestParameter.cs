namespace ParkingFinder.Core.Models.Request.Implementations
{
    /// <summary>
    /// Occupancy Request Parameter Model
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class OccupancyRequestParameter : IOccupancyRequestParameter
    {
        /// <summary>
        /// Id of an parkade
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Determines if the Model is valid
        /// </summary>
        /// <returns>true if the model is valid</returns>
        public bool IsValid()
        {
          return Id > 0;
        } 
    }
}