using System;
using System.Linq;
using System.Text;

namespace Hach.Library.Extensions
{
    public static class UriExtension
    {
        /// <summary>
        /// Helper to append a parameter to the given url
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="key">new key</param>
        /// <param name="value">new value</param>
        /// <returns>url with the new parameter</returns>
        public static string AppendParameter(this Uri url, string key, string value)
        {
            if (key.IsNullOrEmpty() || value.IsNullOrEmpty())
            {
                return url.AbsoluteUri;
            }

            StringBuilder urlBuilder = new StringBuilder();
            bool isParameterPresent = false;
            string baseUrl = url.AbsoluteUri;

            // Get the Url without Parameter
            baseUrl = RemoveFromCharacter(baseUrl, '?');

            // Get the Url without the Subpage identifier
            baseUrl = RemoveLastCharacter(baseUrl, '/');

            //In every loop adjust the Query Parameter
            urlBuilder.Append(baseUrl);

            // If no Query is present, start with a "?"
            if (url.Query.IsNullOrEmpty())
            {
                urlBuilder.Append('?');
            }

            // Split the Query into key value pairs
            foreach (string query in url.Query.Split('&'))
            {
                // Split each key value pair into the parts
                string[] param = query.Split('=');
                if (param.Length != 2)
                {
                    // Error Case no key and value
                    continue;
                }

                string extractedKey = param[0];
                string extractedValue = param[1];

                // Check if the Key is already in the query list
                if (RemoveFirstCharacter(extractedKey, '?').Equals(key))
                {
                    // If it is, take the new value
                    extractedValue = value;
                    isParameterPresent = true;
                }

                urlBuilder.Append($"{extractedKey}={extractedValue}&");
            }

            // If the key was not already in the query list, add it
            if (!isParameterPresent)
            {
                urlBuilder.Append($"{key}={value}&");
            }

            return RemoveLastCharacter(urlBuilder.ToString(), '&');
        }

        #region Helper 

        /// <summary>
        /// Helper which removes the last character in the string, if it is the given one
        /// </summary> 
        /// <param name="baseUrl">base url</param>
        /// <param name="lastCharacter">character to be removed if it is the last one</param>
        /// <returns>base url withour the given character at the end</returns>
        private static string RemoveLastCharacter(string baseUrl, char lastCharacter)
        {
            // Check if the last character is the given one, if so remove it
            if (baseUrl.LastIndexOf(lastCharacter) == baseUrl.Length - 1)
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }

            return baseUrl;
        }

        /// <summary>
        /// Helper which removes the first character in the string, if it is the given one
        /// </summary> 
        /// <param name="baseUrl">base url</param>
        /// <param name="firstcharacter">character to be removed if it is the first one</param>
        /// <returns>base url withour the given character at the end</returns>
        private static string RemoveFirstCharacter(string baseUrl, char firstcharacter)
        {
            // Check if the first character is the given one, if so remove it
            if (baseUrl.IndexOf(firstcharacter) == 0)
            {
                baseUrl = baseUrl.Substring(1);
            }

            return baseUrl;
        }

        /// <summary>
        /// Helper which removes the given string from the given character
        /// </summary> 
        /// <param name="baseUrl">base url</param>
        /// <param name="character">character to be removed if it is present</param>
        /// <returns>base url withour the given character at the end</returns>
        private static string RemoveFromCharacter(string baseUrl, char character)
        {
            // Get the position of the character
            int index = baseUrl.LastIndexOf(character);
            if (index != -1)
            {
                // Remove the rest of the string from the character to the end
                baseUrl = baseUrl.Substring(0, index);
            }

            return baseUrl;
        }

        #endregion
    }
}
