jQuery(document).ready(function () {
    $("#AnalyticsCollection").click(function () {
        CallAnalyticsCollection(true);
    });

    $("#StopAnalyticsCollection").click(function () {
        CallAnalyticsCollection(false);
    });

    $("#SubmitBestParkingForm").click(function () {
       CalculateBestParkingTimes();
    });
});

function CalculateBestParkingTimes() {
    var parkadeId = $("#bestParkingParkade").val();
    var date = $("#bestParkingDate").val();

    var url = "/Analytics/SearchForBestParking?Id=" + parkadeId + "&Date=" + date;
    $.post(url,
            null,
            function (data) {
                // $("#InfowWindowSource").html(data);
                $("#BestParkingResult").html(data);
            })
        .done(function () {
            //MatchHeightAllWithoutScrolling("#BestParkingResult");
        });
}

function CallAnalyticsCollection(start) {
    var jsonData = { start: start };

    // Creating the JSON data
    $.ajax({
        type: "POST",
        async: true,
        global: false,
        cache: false,
        data: jsonData,
        url: "/Analytics/CollectAnalytics",
        dataType: "json",
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        },
        success: function (data) {
            if (data === true) {
                $("#AnalyticsCollection").hide();
                $("#StopAnalyticsCollection").show();
            } else {
                $("#AnalyticsCollection").show();
                $("#StopAnalyticsCollection").hide();
            }
        }
    });
}