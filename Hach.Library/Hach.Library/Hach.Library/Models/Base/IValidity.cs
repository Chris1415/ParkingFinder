namespace Hach.Library.Models.Base
{
    /// <summary>
    /// Interface for Validation
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public interface IValidity
    {
        /// <summary>
        /// Is Valid
        /// </summary>
        /// <returns>true if the model is valid</returns>
        bool IsValid();
    }
}
