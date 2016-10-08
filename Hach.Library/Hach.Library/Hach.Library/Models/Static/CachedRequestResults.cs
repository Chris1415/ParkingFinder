using System;
using System.Collections.Generic;
using System.Linq;
using Hach.Library.Configuration.Reader;
using Hach.Library.Models.Base;
using Hach.Library.Models.Base.Implementations;
using NLog;

namespace Hach.Library.Models.Static
{
    /// <summary>
    /// Application Specific custom Cache 
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public static class CachedRequestResults
    {
        #region Properties

        /// <summary>
        /// List of Cached Models
        /// </summary>
        public static IList<CacheModel<IModel>> CachedModels = new List<CacheModel<IModel>>();

        /// <summary>
        /// NLog
        /// </summary>
        private static readonly Logger Logger = Settings.Logging ? LogManager.GetCurrentClassLogger() : LogManager.CreateNullLogger();

        #endregion

        #region Interface

        /// <summary>
        /// Get a cached model of the given type TU
        /// </summary>
        /// <typeparam name="T">The Type to be returned</typeparam>
        /// <typeparam name="TU">the inner type of the model to be returned</typeparam>
        /// <returns>Reference to the cached model or null</returns>
        public static T GetListedCachedModel<T, TU>() where T : CacheModel<IModel> where TU : IModel
        {
            return CachedModels
                .Where(element => element != null && element.Model.GetType() == typeof(TU))
                .Cast<T>()
                .FirstOrDefault();
        }

        /// <summary>
        /// Caches a given model of the given type T
        /// </summary>
        /// <typeparam name="T">the model to be cached</typeparam>
        /// <param name="model">specific model</param>
        public static void SetListedCachedModel<T>(IModel model) where T : IModel
        {
            try
            {
                CacheModel<IModel> savedResult = CachedModels.FirstOrDefault(cachedModel => cachedModel.Model is T);
                if (savedResult == null)
                {
                    CacheModel<IModel> newModel = new CacheModel<IModel>();
                    newModel.SetModel(model);
                    CachedModels.Add(newModel);
                    return;
                }

                savedResult.Model = model;
            }
            catch (Exception e)
            {
                Logger.Error("SetListedCachedModel: " + e.Message);
            }

        }

        /// <summary>
        /// Caches a given model of the given type T
        /// </summary>
        /// <typeparam name="T">the model to be cached</typeparam>
        /// <param name="model">specific model</param>
        public static bool RemoveListedCachedModel<T>(IModel model) where T : IModel
        {
            CacheModel<IModel> savedResult = CachedModels.FirstOrDefault(cachedModel => cachedModel.Model is T);
            return savedResult != null && CachedModels.Remove(savedResult);
        }

        #endregion
    }
}
