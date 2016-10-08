using System.Collections.Generic;

namespace Hach.Library.Models.Static
{
    /// <summary>
    /// Static In Memory Data Structure
    /// Belongs to the Pruning Repository
    /// </summary>
    /// <typeparam name="T">Custom Result Type</typeparam>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public static class InMemoryDataStructure<T> where T : IEqualityComparer<T>
    {
        /// <summary>
        /// List for GeoObjectItems
        /// </summary>
        private static SortedSet<T> firstGeoObjects = new SortedSet<T>();

        /// <summary>
        /// List for GeoObjectItems
        /// </summary>
        private static SortedSet<T> secondGeoObjects = new SortedSet<T>();

        /// <summary>
        /// Determines if the first or second list should be used
        /// </summary>
        public static bool UseFirstList { get; set; }

        /// <summary>
        /// Get the Elements of the specific List
        /// </summary>
        /// <param name="active">true if the active list should be retrieved</param>
        /// <returns>List of elements</returns>
        public static SortedSet<T> GetElements(bool active)
        {
            return (active && UseFirstList)
                || (!active && !UseFirstList)
                ? firstGeoObjects
                : secondGeoObjects;
        }
    }
}
