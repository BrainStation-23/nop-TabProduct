$(document).ready(function () {
    $('#parentHorizontalTab').easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion
        width: 'auto', //auto or any width like 600px
        fit: true, // 100% fit in a container
        tabidentify: 'hor_1', // The tab groups identifier
        activate: function (event) { // Callback function if tab is switched
            HomePagePaging.animation();
        }
    });

    HomePagePaging.animation();
})