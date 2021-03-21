$(document).ready(function () {
    var modelId = $(".btn-add-cart").attr('data-id');
    $.ajax({
        url: window.location.origin + "/Cart/IsProsuctInCart",
        type: "POST",
        data: { 'id': modelId },
        success: function (result) {
            if (result === true) {
                $('a.btn-add-cart').attr('href', '/Game');
            }
        },
    });
});

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
                //When adding a product, it changes the link of the button to go to the cart
                $('a.btn-add-cart').attr('href', '/Game');
            },
        });
    });
});
