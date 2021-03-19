CKEDITOR.replace('Description');

function togglePopup() {
    document.getElementById("popup-1").classList.toggle("active");
}


$(document).ready(function () {
    $.ajax({
        url: window.location.origin + "/Cart/CallCart",
        type: "Get",
        success: function (result) {
            $("#cart").html(result);
        },
    });
});