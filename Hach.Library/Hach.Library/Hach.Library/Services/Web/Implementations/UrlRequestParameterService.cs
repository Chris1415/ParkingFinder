using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hach.Library.Attributes.RequestParameter;
using Hach.Library.Extensions;

namespace Hach.Library.Services.Web.Implementations
{
    /// <summary>
    /// The Request Parameter Service to create a URL with a base url and an option data model
    /// Parameter are encoded as part of the path BASEURL/PARAM1/PARAM2/...
    /// Parameter are encoded as Key - Value pairs
    /// Depends on the used Attributes
    /// TODO ZU ENDE IMPLEMENTIEREN UND ALTE FUNKTION ENTFERNEN
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016 
    /// </author>
    public class UrlRequestParameterService : IRequestParameterService
    {
        #region Interface

        /// <summary>
        /// Creates a request URL based on the given options
        /// </summary>
        /// <typeparam name="T">options type</typeparam>
        /// <param name="baseUrl">base URL</param>
        /// <param name="options">given options</param>
        /// <returns>Response in JSON format</returns>
        public string BuildRequestUrl<T>(string baseUrl, T options)
        {
            // Build the base url
            string url = baseUrl;
            // Get all parameters of the Url with the given options
            BuildUrlOld(options, ref url);
            return url;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Extracts all Keys and Values for the Parameters of the request Url from the given Options via Attributes
        /// </summary>
        /// <typeparam name="T">Options Type</typeparam>
        /// <param name="options">given options</param>
        /// <param name="url">Url for the request</param>
        public void BuildUrlOld<T>(T options, ref string url)
        {
            IList<Tuple<int, string>> propertyValues = new List<Tuple<int, string>>();
            IList<Tuple<string, string>> keyValueProperties = new List<Tuple<string, string>>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                UrlPathAttribute urlPathAttribute = property.GetCustomAttribute<UrlPathAttribute>();
                if (urlPathAttribute != null)
                {
                    int position = urlPathAttribute.Position;
                    string value = (string)property.GetValue(options);
                    if (value.IsNullOrEmpty())
                    {
                        continue;
                    }

                    propertyValues.Add(new Tuple<int, string>(position, value));
                }

                KeyValueAttribute keyValueAttriute = property.GetCustomAttribute<KeyValueAttribute>();
                if (keyValueAttriute != null)
                {
                    string key = keyValueAttriute.Key;
                    string value = (string)property.GetValue(options);
                    if (key.IsNullOrEmpty() || value.IsNullOrEmpty())
                    {
                        continue;
                    }

                    keyValueProperties.Add(new Tuple<string, string>(key, value));
                }
            }

            propertyValues = propertyValues.OrderBy(element => element.Item1).ToList();

            foreach (string propertyValue in propertyValues.Select(element => element.Item2))
            {
                url += $"/{propertyValue}";
            }

            url += "?";

            foreach (Tuple<string, string> keyValuePair in keyValueProperties)
            {
                url += $"{keyValuePair.Item1}={keyValuePair.Item2}&";
            }
        }

        /// <summary>
        /// Extracts all Keys and Values for the Parameters of the request Url from the given Options via Attributes
        /// </summary>
        /// <typeparam name="T">Options Type</typeparam>
        /// <param name="options">given options</param>
        /// <param name="url">Url for the request</param>
        public void BuildUrl<T>(T options, ref string url)
        {
            url = typeof(T).GetProperties()
                .Select(property => property.GetValue(options))
                .Aggregate(url, (current, value) => current + $"/{value.ToString().ToLower()}");
        }

        #endregion
    }
}