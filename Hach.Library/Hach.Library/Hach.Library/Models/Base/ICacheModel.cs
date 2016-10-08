using System;

namespace Hach.Library.Models.Base
{
    /// <summary>
    /// Generic Cache Model
    /// </summary>
    /// <typeparam name="T">Generic Type of the Cached model</typeparam>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface ICacheModel<T>
    {
        /// <summary>
        /// Time Stamp to get information about the expiration
        /// </summary>
        DateTime TimeStamp { get; set; }

        /// <summary>
        /// The Generic Model
        /// </summary>
        T Model { get; set; }

        /// <summary>
        /// Set Model Method
        /// </summary>
        /// <param name="model">reference to the new generic model</param>
        void SetModel(T model);
    }
}
