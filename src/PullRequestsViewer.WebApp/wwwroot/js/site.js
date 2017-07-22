// Write your JavaScript code.

$(document).ready(function ($) {
    $(".clickable-row").click(function () {
        window.open($(this).data("href"), 'name');
    });
});