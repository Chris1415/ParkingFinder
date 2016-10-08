using System.Collections.Generic;
using System.Web.Mvc;

namespace ParkingFinder.Models
{
    public class AnalyticsBestParkingFormViewModel
    {
        public IEnumerable<SelectListItem> AvailableDates { get; set; }

        public IEnumerable<SelectListItem> AvailableParkades { get; set; }
    }
}