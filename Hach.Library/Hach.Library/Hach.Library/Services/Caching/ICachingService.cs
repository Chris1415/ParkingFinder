using Hach.Library.Models.Base;

namespace Hach.Library.Services.Caching
{
    /// <summary>
    /// Generic Caching Service
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface ICachingService
    {
        #region Interface

        /// <summary>
        /// Method to get a specific Model based on the given type
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <returns>cached model or null if not cached</returns>
        IModel GetModel<T>() where T : class, IModel, new();

        /// <summary>
        /// Method to set a specific model based on the given type
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <param name="model">mode lto be cached</param>
        void SetModel<T>(IModel model) where T : IModel, new();

        #endregion
    }
}
