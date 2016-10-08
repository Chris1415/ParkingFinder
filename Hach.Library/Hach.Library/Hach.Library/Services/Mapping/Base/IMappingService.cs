namespace Hach.Library.Services.Mapping.Base
{
    /// <summary>
    /// Base interface for mapping strings to classes
    /// </summary>
    /// <author>
    /// Christian Hahn, Jul-2016
    /// </author>
    public interface IMappingService
    {
        /// <summary>
        /// Maps the given string to the given class
        /// </summary>
        /// <typeparam name="T">given class type</typeparam>
        /// <param name="input">input json</param>
        /// <returns>instance of the created class</returns>
        T MapStringToClass<T>(string input) where T : new();
    }
}
