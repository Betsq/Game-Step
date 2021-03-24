$(document).ready(function () {
    var modelId = $(".buy-button").attr('data-id');
    $.ajax({
        url: window.location.origin + "/Cart/IsProsuctInCart",
        type: "POST",
        data: { 'id': modelId },
        success: function (result) {
            if (result === true) {
                //When the product in cart, it changes the link of the button to go to the cart
                $('a.buy-button').attr('href', '/Game');
                //When the product in cart, it changes the class
                $('a.buy-button').attr('class', 'buy-button in-cart');

                $('a.buy-button').text('В КОРЗИНЕ');
            }
        },
    });
});

//Call function add item to cart
$(document).ready(function () {
    $("a.buy-button").click(function () {
        //Get from the attribute Id of the product
        var modelId = this.getAttribute('data-id');
        $.ajax({
            url: window.location.origin + "/Cart/AddToCart",
            type: "Get",
            data: { 'id': modelId },
            success: function (result) {
                $("#cart").html(result);
                //When adding a product, it changes the link of the button to go to the cart
                $('a.buy-button').attr('href', '/Game');
                //When adding a product, it changes the class
                $('a.buy-button').attr('class', 'buy-button in-cart');

                $('a.buy-button').text('В КОРЗИНЕ');
            },
        });
    });
});

//function to call the slide panel 
$(document).ready(function () {

    $(".btn-show-req").click(function () {
        $("#panel-show-req").slideToggle("slow");
        if ($("#arrow-show-req").hasClass("arrow arrow-down")) {
            $("#arrow-show-req").attr("class", "arrow arrow-up");
        }
        else {
            $("#arrow-show-req").attr("class", "arrow arrow-down");
        }
    });
});