jQuery(document).ready(function () {
    MatchHeightAll();

    // Cookie Module
    $(".cookieClose").click(function () {
        $(".cookie").slideUp(600);
        setCookie("CookieHint", "false", 365);
        return false;
    });
});

function MatchHeightAll() {
    jQuery(".Teaser").matchHeight();
}

function MatchHeightAllWithoutScrolling(elementToShow) {
    jQuery(".Teaser").matchHeight();
    ScrollTo(elementToShow, 0);
}

function ScrollTo(element, time) {
    if (!time) {
        time = 1000;
    }
    $("html, body")
          .stop()
          .animate({
              scrollTop: $(element).offset().top - 55
          }, time);
}