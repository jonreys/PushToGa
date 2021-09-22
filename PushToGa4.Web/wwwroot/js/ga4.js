$(document).ready(function () {
    /// To Get the next string after '/'
    var location = window.location.href.substring(window.location.href.lastIndexOf("/") + 1);
    /// Test if the string guid 
    if (isGuid(location)) {
        /// If guid = true || Get only the Url without GUID
        document.title = window.location.href.substring(0, window.location.href.lastIndexOf("/"));
    }
    else
        /// If guid = false || Get whole Url
        document.title = window.location.href;

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

/// Test the string if Guid using Regex
function isGuid(stringToTest) {
    if (stringToTest[0] === "{") {
        stringToTest = stringToTest.substring(1, stringToTest.length - 1);
    }
    var regexGuid = /^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/gi;
    
    return regexGuid.test(stringToTest);
}

