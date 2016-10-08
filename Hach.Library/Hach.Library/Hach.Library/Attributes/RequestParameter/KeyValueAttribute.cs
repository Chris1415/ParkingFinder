using System;

namespace Hach.Library.Attributes.RequestParameter
{
    /// <summary>
    /// Attribute to show that a specific property should be used as Key - Value Pair in Url -> key=value
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class KeyValueAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Key Property
        /// </summary>
        public string Key { get; set; }

        #endregion

        #region c'tor

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="key">Key</param>
        public KeyValueAttribute(string key)
        {
            this.Key = key;
        }

        #endregion
    }
}
