$(document).ready(function () {
    $("#btnAddToCart").click(function () {
        var modelId = this.getAttribute('model-Id');
        $.ajax({
            url: window.location.origin + "/Cart/AddToCart",
            type: "Get",
            data: { 'id': modelId },
        });
    });
});

$(document).ready(function () {
    $(".btnDelProdCart").click(function () {
        var modelCartId = this.getAttribute('model-id-in-cart');
        $.ajax({
            url: window.location.origin + "/Cart/DeleteProductInCart",
            type: "Get",
            data: { 'id': modelCartId },
            success: function (result) {
                $("#cart").html(result);
            },
        });
    })
    document.getElementById("popup-1").classList.toggle("active");
});
