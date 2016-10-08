namespace Hach.Library.Services.Web
{
    /// <summary>
    /// Service to handle all Requests
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// Christian Hahn, Sep-2016 (Modified)
    /// </author>
    public interface IRequestService
    {
        /// <summary>
        /// Requests the petrol Service
        /// </summary>
        /// <param name="url">concrete URL for the Request</param>
        /// <returns>JSON String with a List of all Petrol Stations, which fit to the request options</returns>
        string ExecuteRequest(string url);
    }
}
