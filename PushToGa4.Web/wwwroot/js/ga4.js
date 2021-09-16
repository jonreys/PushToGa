$(document).ready(function () {
    document.title = document.title.substring(0, document.title.lastIndexOf("/"));

    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    gtag('config', 'G-01JM05KNTW');

    $("#btnSend").click(function () {
        var userId = $("#userId").val();
        var userName = $("#username").val();
        var type = $("#optionTypeId option:selected").val();

        if (userId != null && userId.length > 0) {
            gtag("event", "login", {
                method: "User ID:" + userId + " - " + type
            });

            alert('Sent');
        }
        else
            alert('Oooops, User ID is not present.');
    });
});