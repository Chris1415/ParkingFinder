// Global Variables
var map;
var defaultLatLng = { lat: 51.309810, lng: 10.169846 };
var bounds;
var markers = [];
var currentPosition = { lat: 51.309810, lng: 10.169846 };
var currentPositionMarker;
var favoriteMarker;
var processor;
var activeMarker;
// global Parameter for the Marker Computation
var stepWith = 50;
var customZoomEventWished = true;
var directionsService;
var directionsDisplay;
var hideMarker = false;
var cookieFavoriteKey = "Favorite";

// Dom Ready initializations
$(document)
    .ready(function () {
        // Locate Me Button & Function
        $("#LocateMe").click(function () {
            ShowLoadingScreen();
            LocateMe();
        });

        // Enter Click Event for Search
        $("#SearchBox").keydown(function (e) {
            if (e.keyCode === 13) {
                SearchClicked();
            }
        });

        // GeoLocation Button & Function
        $("#Search").click(function () {
            SearchClicked();
        });
    });

// Function to initialize the Map
function initMap() {
    // Create a map object and specify the DOM element for display.
    map = new google.maps.Map($("#Map")[0],
    {
        center: defaultLatLng,
        scrollwheel: true,
        zoom: 7,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        mapTypeControl: false,
        scaleControl: true,
        streetViewControl: false
    });

    directionsService = new google.maps.DirectionsService;
    directionsDisplay = new google.maps.DirectionsRenderer;
    directionsDisplay.setMap(map);
    lastOpenedWindow = new google.maps.InfoWindow();

    currentPositionMarker = new google.maps.Marker();
    favoriteMarker = new google.maps.Marker();
    activeMarker = new google.maps.Marker();
    bounds = new google.maps.LatLngBounds();
    ShowLoadingScreen();
    MapIpToGeocoordinate();

    var initial = true;

    // Idle is fired when the map becomes idle after panning and zooming.
    // Now calculation of new marker can start with the new viewport of the map
    map.addListener('idle', function () {
        if (!customZoomEventWished || initial || openingWindow) {
            customZoomEventWished = true;
            initial = false;
            openingWindow = false;
            return;
        }

        var currentPosition = {
            lat: map.getCenter().lat(),
            lng: map.getCenter().lng()
        };
        var radius = calculateDistanceOfViewPort();
        spinner("start");
        ExecuteParkadeSearch(currentPosition, radius, false, false);
    });
}

function MapIpToGeocoordinate() {
    // Use Geo Ip in case no Favorite Store and no current location has been found till yet
    if (google.loader.ClientLocation) {
        currentPosition = {
            lat: google.loader.ClientLocation.latitude,
            lng: google.loader.ClientLocation.longitude
        };

        ExecuteParkadeSearch(currentPosition, 5, true, true);
    } else {
        HideLoadingScreen();
    }
}

// Helper to get the geolocation via Browser
function LocateMe() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            currentPosition = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            spinner("start");
            RemoveDisplayedRoute();
            hideMarker = false;
            RemoveAllMarkers();
            ExecuteParkadeSearch(currentPosition, 5, true, true);
        });
    } else {
        ShowErrorMessage("Locate Me failed");
        HideLoadingScreen();
    }
}

function ContiniousLocateMe() {
    // Request repeated updates.
    var watchId = navigator.geolocation.watchPosition(scrollMap, handleError);

    function CancelLocateMe() {
        // Cancel the updates when the user clicks a button.
        navigator.geolocation.clearWatch(watchId);
    }

    function scrollMap(position) {
        currentPosition = {
            lat: position.coords.latitude,
            lng: position.coords.longitude
        };
        AddPositionMarker(currentPosition);
    }

    function handleError(error) {
        CancelLocateMe();
    }
}

// Executes Search After click (Enter)
function SearchClicked() {

    var inputPlz = $("#pac-input").val();
    // Validation
    if (inputPlz.length === 0) {
        ShowErrorMessage("Please insert a value for searching");
        return;
    }

    ShowLoadingScreen();
    spinner("start");
    RemoveDisplayedRoute();
    hideMarker = false;
    MapInputToGeolocation(inputPlz);
}

// Maps the input of the Search Field to a geolocation
function MapInputToGeolocation(inputPlz) {
    $.ajax({
        type: "POST",
        url: "/Map/GeolocationMapper",
        data: { 'input': inputPlz },
        context: document.body
    })
        .done(function (data) {
            if (data.length !== 0) {
                HideErrorMessage();

                // Get the neccessary Lat and Lng
                var lat = data["Lat"];
                var lng = data["Lng"];

                currentPosition = {
                    lat: lat,
                    lng: lng
                }

                RemoveAllMarkers();
                ExecuteParkadeSearch(currentPosition, 5, true, true);
                return;
            }

            spinner();
            HideLoadingScreen();
            ShowErrorMessage("No Geolocation found");
        });
}

// Helper to add the current position marker
function AddPositionMarker(location, panTo) {

    // Prepare the map to remove Current Position Marker
    currentPositionMarker.setMap(null);

    // Build a map readable location type
    var loc = new google.maps.LatLng(location.lat, location.lng);

    // Create a Marker
    currentPositionMarker = new google.maps.Marker({
        position: loc,
        map: map,
        title: 'Marker',
        icon: "/Content/Images/CurrentLocation.png"
    });

    if (panTo) {
        // Extend the map bounds with the current position
        bounds.extend(loc);
        map.panToBounds(bounds);
        map.fitBounds(bounds);
    }

    ScrollTo("#HeadlineMap");
    HideLoadingScreen();
}

function ExecuteParkadeSearch(location, distance, fitBounds, showCurrentPosition) {
    if (!distance) {
        distance = 5;
    }

    if (!location) {
        return;
    }

    var jsonData = { Lat: location.lat, Lng: location.lng, Rad: distance };
    // Creating the JSON data
    $.ajax({
        type: "POST",
        async: true,
        global: false,
        cache: false,
        data: jsonData,
        url: "/Map/UpdateParkades",
        dataType: "json",
        error: function () {
            HideLoadingScreen();
            ShowErrorMessage("Error while Searching...");
        },
        success: function (data) {

            if (data.length <= Schwelle || distance >= DistSchwelle) {
                ExecuteParkadeSearch(location, distance + 5, fitBounds, showCurrentPosition);
                return;
            }

            // Initialize Bounds
            bounds = new google.maps.LatLngBounds();

            AsyncCalculationForMarker(data, fitBounds, showCurrentPosition);
        }
    });
}

// Helper to build the info window of a specific marker 
function BuildInfoWindow(marker) {
    // Build the Url for the Request for petrol stations
    var url = "/Map/GetInfoWindow?id=" + marker.id;
    $.post(url,
            null,
            function (data) {
                // $("#InfowWindowSource").html(data);
                $("#InfoPlaceholder").html(data);
            })
        .done(function () {
            // Set the Smartphone View
            $("#smartphoneViewWrapper").html($("#infoWindowSmartphone").html());

            $("#InfoPlaceholder").show();
            //.panTo(marker.getPosition());

            if (activeMarker.id !== favoriteMarker.id) {
                activeMarker.setIcon(activeMarker.savedIcon);
            }

            activeMarker = marker;

            if (marker.id !== favoriteMarker.id) {
                activeMarker.setIcon(marker.activeIcon);
            }
        })
    .fail(function () {
        ShowErrorMessage("Error while getting data");
    });
}

function CloseClicked() {
    RemoveDisplayedRoute();
    if (activeMarker.id !== favoriteMarker.id) {
        activeMarker.setIcon(activeMarker.savedIcon);
    }
    activeMarker = new google.maps.Marker();
    $("#InfoPlaceholder").hide();
}

// Spinning Loader
function spinner(start) {
    if (start) {
        $("#gmapSpinner").fadeIn();
    } else {
        $("#gmapSpinner").fadeOut();
    }
}

// Removes all Markers from the map
function RemoveAllMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

function ShowLoadingScreen() {
    // Gray Out the Screen to demonstrate Loading
    $(".grayout").fadeTo(500, 0.3);
}

function HideLoadingScreen() {
    // Remove the gray Loading screen
    $(".grayout").fadeTo(200, 1.0, function () {
        $("#grayout").hide();
    });
}

// Error message Helper
// Show Error message
function ShowErrorMessage(message) {
    ScrollTo("#HeadlineMap");
    // Error Case -> Change  Appearance in frontend
    $("#AlertInputBox").fadeIn(200);
    $("#ErrorLabel").text(message);
}

// Hide Error message
function HideErrorMessage() {
    // No Error case -> Change appearance
    $("#AlertInputBox").fadeOut();
}
// End Error message Helper

// Async Loader Helper
// Async function handlefor calculating markers which the browser is still responsive
function AsyncCalculationForMarker(data, fitBounds, showCurrentPosition) {
    var busy = false;
    var counter = 0;
    processor = setInterval(function () {
        if (!busy) {
            busy = true;

            var hasMoreResults = ComputeNewMarker(data, counter++, fitBounds, showCurrentPosition);

            if (!hasMoreResults) {
                spinner();
                clearInterval(processor);
                HideLoadingScreen();
            }
            busy = false;
        }
    },
        300);
}

function ComputeNewMarker(data, step, fitBounds, showCurrentPosition) {
    var length = data.length;
    var start = step * stepWith;
    var end = start + stepWith;

    if (end > length) {
        end = length;
    }

    // Looping through the JSON data
    for (var i = start; i < end; i++) {

        var element = data[i];
        var lat = parseFloat(element["Latitude"]);
        var lng = parseFloat(element["Longitude"]);
        if (isNaN(lat || isNaN(lng))) {
            continue;
        }

        if (IsMarkerInArray(markers, element["Id"])) {
            continue;
        }

        var loc = new google.maps.LatLng(lat, lng);
        bounds.extend(loc);

        var occupanyData = element["OccupancyModel"]["OccupancyData"];
        var zIndex;

        if (occupanyData == null) {
            zIndex = 0;
        } else {
            zIndex = occupanyData["Category"];
        }


        // Create a Marker
        var marker = new google.maps.Marker({
            position: loc,
            map: map,
            title: 'Marker',
            id: element["Id"],
            icon: element["OccupancyModel"]["MarkerData"]["MarkerIcon"],
            activeIcon: element["OccupancyModel"]["MarkerData"]["ActiveMarkerIcon"],
            savedIcon: element["OccupancyModel"]["MarkerData"]["MarkerIcon"],
            favoriteIcon: element["OccupancyModel"]["MarkerData"]["FavoriteMarkerIcon"],
            zIndex: zIndex
        });

        var favoriteId = getCookie(cookieFavoriteKey);
        if (favoriteId && marker.id === favoriteId) {
            favoriteMarker = marker;
            marker.setIcon(marker.favoriteIcon);
        }

        markers.push(marker);

        // Creating a closure to retain the correct data, notice how I pass the current data in the loop into the closure (marker, data)
        AddClickEvent(marker);
    }
    if (i === length) {

        if (showCurrentPosition) {
            AddPositionMarker(currentPosition, false);

            bounds.extend(currentPosition);
        }

        customZoomEventWished = false;

        //Fit the map to the newly inclusive bounds
        if (fitBounds) {
            // Workaround that the custom zoom_change Event does not execute a search egain
            map.fitBounds(bounds);
        }

        if (hideMarker) {
            HideMarker();
        }

        return false;
    } else {
        return true;
    }
}

// Helper to determine if a marker with a specific id is in array
function IsMarkerInArray(markerArray, markerId) {
    var toReturn = false;
    $.each(markerArray, function (index, value) {
        if (value["id"] === markerId) {
            toReturn = true;
            return true;
        }
    });
    return toReturn;
}

function AddClickEvent(innerMarker) {
    // Add a Click Callback for more Information on the map
    google.maps.event.addListener(innerMarker,
        'click',
        function () {
            BuildInfoWindow(innerMarker);
        });
};

// End Async Loader Helper

//Begin Geolocation Helper

// Calculates the distance between, map corner and map center
function calculateDistanceOfViewPort() {
    var bounds = map.getBounds();
    var center = bounds.getCenter();
    var ne = bounds.getNorthEast();
    return Math.round(google.maps.geometry.spherical.computeDistanceBetween(center, ne) / 1000);
}

// Calculates the distance between two positions
function calculateDistanceOfPoints(first, second) {
    return Math.round(google.maps.geometry.spherical.computeDistanceBetween(first, second) / 1000);
}
// End Geolocation Helper

// Route Helper
// Show a Route from current location and the target
function calculateAndDisplayRoute(destinationLat, destinationLng, travelMode) {
    if (!currentPositionMarker.position) {
        ShowErrorMessage("No Start Position Set! Use Locate Me or enter a Location in the Search Box");
        return;
    }

    HideErrorMessage();
    if (!travelMode) {
        travelMode = "DRIVING";
    }
    directionsService.route({
        origin: currentPositionMarker.position,
        destination: new google.maps.LatLng(destinationLat, destinationLng),
        travelMode: travelMode,
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
            $(".routeInformation").removeClass("hidden");
            $(".baseInformation").addClass("hidden");

            FillRouteScreen(response);
            hideMarker = true;
            HideMarker();
            ScrollTo("#Map");
        } else {
            ShowErrorMessage("Directions request failed due to " + status);
        }
    });
}

function FillRouteScreen(response) {
    var detailresponseInforamtion = response.routes[0].legs[0];
    var startLocation = detailresponseInforamtion.start_location;
    var endLocation = detailresponseInforamtion.end_location;
    var startAddress = detailresponseInforamtion.start_address;
    var endAddress = detailresponseInforamtion.end_address;
    var distance = detailresponseInforamtion.distance.text;
    var duration = detailresponseInforamtion.duration.text;
    var travelMode = response.request.travelMode;
    var linkUrl = "https://www.google.de/maps/dir/" +
        startLocation.lat() +
        "," +
        startLocation.lng() +
        "/" +
        endLocation.lat() +
        "," +
        endLocation.lng();

    $(".gmapMoreInformationLink").attr("href", linkUrl);
    $(".gmapStart").text(startAddress);
    $(".gmapEnd").text(endAddress);
    $(".gmapDistance").text(distance);
    $(".gmapDuration").text(duration);
    $(".gmapTransport").text(travelMode);
}

function ShowDetailedRoute() {
    var link = $(".gmapMoreInformationLink").attr("href");
    window.open(link, "_blank");
}

// Removes the displayed Route
function RemoveDisplayedRoute() {
    directionsDisplay.setMap(null);
    directionsDisplay = null;
    directionsDisplay = new google.maps.DirectionsRenderer;
    directionsDisplay.setMap(map);
    hideMarker = false;
    ShowMarker();

    $(".baseInformation").removeClass("hidden");
    $(".routeInformation").addClass("hidden");
    ScrollTo("#Map");
}

//End Route Helper

function HideMarker() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    currentPositionMarker.setMap(null);
}

function ShowMarker() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
    currentPositionMarker.setMap(map);
}

function SetMarkersFavoriteStatus(addAsFavorite, markerId) {
    if (addAsFavorite === "true") {
        setCookie(cookieFavoriteKey, markerId);
        $.each(markers, function (index, value) {
            if (value.id === markerId) {
                value.setIcon(value.favoriteIcon);
                favoriteMarker.setIcon(favoriteMarker.savedIcon);
                favoriteMarker = value;
            }
        });
        $(".AddFavorite").hide();
        $(".RemoveFavorite").show();
        UpdateFavoriteTeaser();
    } else {
        setCookie(cookieFavoriteKey, null);
        if (favoriteMarker.id !== activeMarker.id) {
            favoriteMarker.setIcon(favoriteMarker.savedIcon);
        } else {
            favoriteMarker.setIcon(favoriteMarker.activeIcon);
        }

        favoriteMarker = new google.maps.Marker();
        $(".AddFavorite").show();
        $(".RemoveFavorite").hide();
        UpdateFavoriteTeaser();
    }
}

function UpdateFavoriteTeaser() {
    $.get("/Map/FavoriteTeaser",
        null,
        function (data) {
            $("#FavoriteTeaserWrapper").html(data);
        });
}