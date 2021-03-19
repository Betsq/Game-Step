$(document).ready(function () {
    $("#btnAddToCart").click(function () {
        var modelId = this.getAttribute('model-Id');
        $.ajax({
            url: window.location.origin + "/Home/AddToCart",
            type: "Get",
            data: { 'id': modelId },
        success: function (result) {
            $("#cart").html(result);
        },
            });
        });
    });