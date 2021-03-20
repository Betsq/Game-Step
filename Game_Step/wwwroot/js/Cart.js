//Call function add item to cart
$(document).ready(function () {
    $(".btn-Add-Cart").click(function () {
        //Get from the attribute Id of the product
        var modelId = this.getAttribute('model-Id');
        $.ajax({
            url: window.location.origin + "/Cart/AddToCart",
            type: "Get",
            data: { 'id': modelId },
        });
    });
});
/*When adding an item to the cart, this function is called
to update the display of the quantity of items.*/
//$(document).ready(function () {
//    $(".btn-Add-Cart").click(function () {
//        $.ajax({
//            url: window.location.origin + "/Cart/CountOfGoods",
//            type: "POST",
//            success: function (result) {
//                $("#quantity-goods").html(result);
//            },
//        });
//    });
//});


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
