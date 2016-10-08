using System;

namespace Hach.Library.Attributes.RequestParameter
{
    /// <summary>
    /// Attribute to show that a specific property should be used as Part of the Url -> /PARAMTER/
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class UrlPathAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Position Property
        /// </summary>
        public int Position { get; set; }

        #endregion

        #region c'tor

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="position">The Position in the URL</param>
        public UrlPathAttribute(int position)
        {
            this.Position = position;
        }

        #endregion
    }
}
