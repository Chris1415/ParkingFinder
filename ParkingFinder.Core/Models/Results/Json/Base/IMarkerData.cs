namespace ParkingFinder.Core.Models.Results.Json.Base
{
    public interface IMarkerData
    {
        int Category { get; }

        string MarkerIcon { get; }

        string ActiveMarkerIcon { get; }

        string FavoriteMarkerIcon { get; }
    }
}
