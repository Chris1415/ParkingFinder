namespace Hach.Library.Services.Web
{
    /// <summary>
    /// The Request Parameter Service to create a URL with a base url and an option data model
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// Christian Hahn, Sep-2016 (Modified)
    /// </author>
    public interface IRequestParameterService
    {
        /// <summary>
        /// Creates a request URL based on the given options
        /// </summary>
        /// <typeparam name="T">options type</typeparam>
        /// <param name="baseUrl">base URL</param>
        /// <param name="options">given options</param>
        /// <returns>Response in JSON format</returns>
        string BuildRequestUrl<T>(string baseUrl, T options);
    }
}
