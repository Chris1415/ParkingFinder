using System;
using System.Net;
using Hach.Library.Configuration.Reader;
using NLog;

namespace Hach.Library.Services.Web.Implementations
{
    /// <summary>
    /// Service to handle all Requests
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// Christian Hahn, Sep-2016 (Modified)
    /// </author>
    public class RequestService : IRequestService
    {
        #region Properties

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = Settings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        #region Interface

        /// <summary>
        /// Execute Request
        /// </summary>
        /// <param name="url">concrete URL for the Request</param>
        /// <returns>JSON String as Result</returns>
        public string ExecuteRequest(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            }
            catch (Exception e)
            {
                Logger.Error("ExecuteRequest: " + e.Message);
                return string.Empty;
            }
           
        }

        #endregion
    }
}
