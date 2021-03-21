$(document).ready(function () {
    var modelId = $(".btn-add-cart").attr('data-id');
    $.ajax({
        url: window.location.origin + "/Cart/IsProsuctInCart",
        type: "POST",
        data: { 'id': modelId },
        success: function (result) {
            if (result === true) {
                 //When the product in cart, it changes the link of the button to go to the cart
                $('a.btn-add-cart').attr('href', '/Game');
                //When the product in cart, it changes the class
                $('a.btn-add-cart').attr('class', 'btn-add-cart in-cart col-md-12');

                $('#span-text-change').text('В КОРЗИНЕ');
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
                //When adding a product, it changes the class
                $('a.btn-add-cart').attr('class', 'btn-add-cart in-cart col-md-12');

                $('#span-text-change').text('В КОРЗИНЕ');
            },
        });
    });
});
