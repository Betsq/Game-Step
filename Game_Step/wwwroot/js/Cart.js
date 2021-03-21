//Call function add item to cart
$(document).ready(function () {
    $("a.btn-add-cart").click(function () {
        //Get from the attribute Id of the product
        var modelId = this.getAttribute('data-id');
        $.ajax({
            url: window.location.origin + "/Cart/AddToCart",
            type: "Get",
            data: { 'id': modelId },
            success: function (result) {
                $("#cart").html(result);
            },
        });
    });
});

//Call function to display the number of items in the cart
$(document).ready(function () {
    $.ajax({
        url: window.location.origin + "/Cart/CountOfGoods",
        type: "POST",
        success: function (result) {
            $("#quantity-goods").html(result);
        },
    });
});


$(document).ready(function () {
    $(".btnDelProdCart").click(function () {
        var modelCartId = this.getAttribute('del-data-id');
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
