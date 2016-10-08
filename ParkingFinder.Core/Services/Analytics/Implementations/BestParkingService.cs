using System;
using System.Collections.Generic;
using System.Linq;
using ParkingFinder.Core.Models.Analytics;
using CoreLabels = ParkingFinder.Core.Models.Labels;

namespace ParkingFinder.Core.Services.Analytics.Implementations
{
    /// <summary>
    /// Parking Predicition Service
    /// Service to predict the the Free Parking Space 
    /// </summary>
    /// <author>
    /// Christian Hahn, Sep-2016
    /// </author>
    public class BestParkingService : IBestParkingService
    {
        /// <summary>
        /// Base Execute Method for all Analytics Services
        /// </summary>
        /// <param name="requestedDateTime">the reuested Date time to get analytics data</param>
        /// <param name="requestedId">the requested id to get data for</param>
        /// <param name="analyticsModel">Reference to the current Analytics data</param>
        /// <returns>output string with the analytics result</returns>
        public string Execute(DateTime requestedDateTime, string requestedId, IParkingFinderAnalyticsModel analyticsModel)
        {
            IEnumerable<IParkingFinderAnalyticsData> analyticsDataFromDayAndId = PrepareAnalyticsData(requestedDateTime, requestedId, analyticsModel);
            Tuple<int, IList<IList<DateTime>>> calculatedTimeSpans = CalculateBestTimeSpans(analyticsDataFromDayAndId);
            return BuildResultString(calculatedTimeSpans.Item1, requestedDateTime, calculatedTimeSpans.Item2);
        }

        /// <summary>
        /// Helper to prepare the analytics data vor evaluation
        /// </summary>
        /// <param name="requestedDateTime">the reuested date time</param>
        /// <param name="requestedId">the requested parkade id </param>
        /// <param name="analyticsModel">all analytics data</param>
        /// <returns>filtered analytics data based on the input</returns>
        private static IEnumerable<IParkingFinderAnalyticsData> PrepareAnalyticsData(DateTime requestedDateTime, string requestedId, IParkingFinderAnalyticsModel analyticsModel)
        {
            KeyValuePair<int, IList<IParkingFinderAnalyticsData>> specificAnalyticsModel = analyticsModel
                .AnalyticsData
                .FirstOrDefault(element => element.Key.ToString().Equals(requestedId));

            return specificAnalyticsModel.Value.Where(
                    listElement =>
                        listElement.TimeStamp.ToString(CoreLabels.DateFormat)
                            .Equals(requestedDateTime.ToString(CoreLabels.DateFormat)));
        }

        /// <summary>
        /// Helper to calculate the time spans of best parking on the given date
        /// </summary>
        /// <param name="analyticsDataFromDayAndId">input analytics data of a specific parkade</param>
        /// <returns>all timespans of best parking with the category</returns>
        private static Tuple<int, IList<IList<DateTime>>> CalculateBestTimeSpans(IEnumerable<IParkingFinderAnalyticsData> analyticsDataFromDayAndId)
        {
            int maxCategory = int.MinValue;
            IList<IList<DateTime>> timeSpansOfMinCategory = new List<IList<DateTime>>()
                {
                    new List<DateTime>()
                };

            foreach (IParkingFinderAnalyticsData analyticsElement in analyticsDataFromDayAndId)
            {
                int givenCategory = analyticsElement.OccupancyModel.OccupancyData.Category;
                if (givenCategory > maxCategory)
                {
                    timeSpansOfMinCategory = new List<IList<DateTime>>()
                {
                    new List<DateTime>()
                };
                    maxCategory = givenCategory;
                    timeSpansOfMinCategory.Last().Add(analyticsElement.TimeStamp);
                }
                else if (givenCategory == maxCategory)
                {
                    timeSpansOfMinCategory.Last().Add(analyticsElement.TimeStamp);
                }
                else
                {
                    if (timeSpansOfMinCategory.Last().Any())
                    {
                        timeSpansOfMinCategory.Add(new List<DateTime>());
                    }

                }
            }

            return new Tuple<int, IList<IList<DateTime>>>(maxCategory, timeSpansOfMinCategory);
        }

        /// <summary>
        /// Helper to build the result as string based on the given data
        /// </summary>
        /// <param name="maxCategory">the max category</param>
        /// <param name="requestedDateTime">the requested date time </param>
        /// <param name="timeSpansOfMinCategory">the result list of diffrent time spans of max category</param>
        /// <returns>string, which can be displayed as result</returns>
        private static string BuildResultString(int maxCategory, DateTime requestedDateTime, IEnumerable<IList<DateTime>> timeSpansOfMinCategory)
        {
            string innerResult = timeSpansOfMinCategory
                .Where(@group => @group.Any())
                .Aggregate(string.Empty, (current, @group) =>
                current 
                + string.Format(
                    Labels.InnerResultFormat,
                    @group.First().ToString(CoreLabels.TimeFormat),
                    @group.Last().ToString(CoreLabels.TimeFormat)));

            return string.Format(
                Labels.OuterResultFormat, 
                maxCategory,
                requestedDateTime.ToString(CoreLabels.DateFormat), 
                innerResult);
        }

        /// <summary>
        /// Static Labels 
        /// </summary>
        public static class Labels
        {
            /// <summary>
            /// The Outer Result Format with Category, Date and Placeholder for Times
            /// </summary>
            public const string OuterResultFormat = "Best Category: {0} | {1} - {2}";

            /// <summary>
            /// Inner Result Format for diffrent Times
            /// </summary>
            public const string InnerResultFormat = "({0}) - ({1}) ,";
        }
    }
}
