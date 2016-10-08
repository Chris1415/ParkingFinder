using System.Collections.Generic;

namespace ParkingFinder.Core.Models.Results.Json.Base.Implementations
{
    public class MarkerData : IMarkerData
    {
        public int Category { get; }

        public string MarkerIcon => Labels.MarkerFolder + GetImageName();

        public string ActiveMarkerIcon => Labels.BigMarkerFolder + GetImageName();

        public string FavoriteMarkerIcon => Labels.BigMarkerFolder + "MapMarker_Ball_Right_Black.png";

        #region c'tor

        /// <summary>
        /// c'tor with Parameter
        /// </summary>
        /// <param name="category">Category of Occupancy</param>
        public MarkerData(int category)
        {
            this.Category = category;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Gets the Image Names mapped to the categories
        /// </summary>
        /// <returns>Image Name</returns>
        private string GetImageName()
        {
            string icon;
            return CategoryIconMapping.TryGetValue(this.Category, out icon)
                ? icon
                : string.Empty;
        }

        /// <summary>
        /// Category Icon Mapping Dictionary
        /// TODO Make it configurable
        /// Category 0 is not existing and means, no Occupancy Data present
        /// </summary>
        private static readonly Dictionary<int, string> CategoryIconMapping = new Dictionary<int, string>()
        {
            {0, "MapMarker_Ball_Right_Blue.png"},
            {1, "MapMarker_Ball_Right_Red.png"},
            {2, "MapMarker_Ball_Right_Orange.png"},
            {3, "MapMarker_Ball_Right_Yellow.png"},
            {4, "MapMarker_Ball_Right_Green.png"}
        };

        #endregion

        /// <summary>
        /// Static Labels
        /// </summary>
        public static class Labels
        {
            /// <summary>
            /// Marker Folder
            /// </summary>
            public const string MarkerFolder = "/Content/Images/marker/";

            /// <summary>
            /// Active Marker Folder
            /// </summary>
            public const string BigMarkerFolder = "/Content/Images/markerBig/";
        }
    }
}
