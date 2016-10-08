namespace Hach.Library.Models.Web.Geolocation.Parameters.Geolocation
{
    /// <summary>
    /// Interface for Geolocation Query
    /// </summary>
    /// <author>
    /// Christian Hahn, Apr-2016
    /// </author>
    public interface IQuery
    {
        /// <summary>
        /// Method to check if the Input is Valid
        /// </summary>
        /// <returns>True if the Query is Valid</returns>
        bool IsValid();
    }
}
