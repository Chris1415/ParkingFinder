using System;
using Hach.Library.Configuration.Reader;
using Hach.Library.Models.Base;
using Hach.Library.Models.Base.Implementations;
using Hach.Library.Models.Static;

namespace Hach.Library.Services.Caching.Implementations
{
    /// <summary>
    /// Generic Caching Service
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class CachingService : ICachingService
    {
        #region Interface 

        /// <summary>
        /// Method to get a specific Model based on the given type
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <returns>cached model or null if not cached</returns>
        public IModel GetModel<T>() where T : class, IModel, new()
        {
            CacheModel<IModel> cachedModel = CachedRequestResults.GetListedCachedModel<CacheModel<IModel>, T>();
            if (cachedModel == null)
            {
                return default(T);
            }

           
            TimeSpan goneTime = DateTime.Now - cachedModel.TimeStamp;
              // Delete all elements, where the request is longer than X in the history
              if (MoreThanXMinutesElapsed(goneTime, cachedModel.MinutesToStore))
              {
                  cachedModel = null;
              }

            // Check if the request is still cached
            return cachedModel?.Model;
        }

        /// <summary>
        /// Method to set a specific model based on the given type
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <param name="model">mode lto be cached</param>
        public void SetModel<T>(IModel model) where T : IModel, new()
        {
            CachedRequestResults.SetListedCachedModel<T>(model);
        }

        #endregion

        #region Helper

        /// <summary>
        /// Helper to determine if a ts is greater than X minutes 
        /// </summary>
        /// <param name="ts">time span</param>
        /// <param name="thresholdInMinutes">threshold in minutes</param>
        /// <returns>true if the time span is greater than the threshold</returns>
        private static bool MoreThanXMinutesElapsed(TimeSpan ts, int thresholdInMinutes)
        {
            if (thresholdInMinutes == 0)
            {
                return false;
            }

            // Check if the minutes are greater than the threshold
            if (ts.Minutes >= thresholdInMinutes)
            {
                return true;
            }

            // Check the case if the minutes are less than the threshold, but the hours or days are greater 0
            return ts.Hours > 0 || ts.Days > 0;
        }

        #endregion
    }
}
