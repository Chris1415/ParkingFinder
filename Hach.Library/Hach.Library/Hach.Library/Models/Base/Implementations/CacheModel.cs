using System;

namespace Hach.Library.Models.Base.Implementations
{
    /// <summary>
    /// Generic Cache Model
    /// </summary>
    /// <typeparam name="T">Generic Type of the Cached model</typeparam>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class CacheModel<T> : ICacheModel<T>
    {
        /// <summary>
        /// Time Stamp to get information about the expiration
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Time in Minutes how long the cached model is valid
        /// A value of 0 menas, it never expires
        /// </summary>
        public int MinutesToStore { get; set; }

        /// <summary>
        /// The Generic Model
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        /// Set Model Method
        /// </summary>
        /// <param name="model">reference to the new generic model</param>
        public void SetModel(T model)
        {
            this.Model = model;
            TimeStamp = DateTime.Now;
            MinutesToStore = 0;
        }

        /// <summary>
        /// Set Model Method
        /// </summary>
        /// <param name="model">reference to the new generic model</param>
        /// <param name="minutesToStore">Time in minutes to store the model</param>
        public void SetModel(T model, int minutesToStore)
        {
            this.Model = model;
            TimeStamp = DateTime.Now;
            MinutesToStore = minutesToStore;
        }
    }
}
