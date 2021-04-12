$(document).ready(function () {
    $(".remove-from-cart").click(function () {
        const modelCartId = this.getAttribute("del-data-id");
        $.ajax({
            url: window.location.origin + "/Cart/DeleteProductInCart",
            type: "Get",
            data: { 'id': modelCartId },
            success: function (result) {
                location.reload();
            },
        });
    });
});

//Function for displaying the total price of goods
function ChangeTotalPrice() {

    let totalPrice = 0;

    //Parse and add the price of all goods
    $(".prod-price").each(function (index, obj) {
        totalPrice += parseInt(($(this).text()));
    });

    //Displaying the total price
    $(".final-price").text("Итого: " + totalPrice + "$");
};

//Change call function and display of a specific product
function ChangePriceProduct(getId, getAmountProduct) {
    $.ajax({
        url: window.location.origin + "/Cart/CountPrice",
        type: "Post",
        data: { 'id': getId, 'amountProduct': getAmountProduct },
        success: function (result) {
            $("#" + getId).text(result);

            ChangeTotalPrice();
        },
    });
};

//When the page loads, it calls the function
$(document).ready(function () {
    ChangeTotalPrice();
});


$(document).ready(function () {
    //Function to decrease the value in input using buttons
    $(".btnMinus").click(function () {

        //Sets the minimum input threshold "1" for products
        $(".input-quantity-product").attr("min", 1);

        //Changes the value in input to -1
        this.nextElementSibling.stepDown();

        //Gets the id of the product to be passed to the function
        const getId = $(this).next().attr("data-id");
        //Gets the quantity of ordered one item of goods
        const getAmountProduct = $(this).next().val();


        ChangePriceProduct(getId, getAmountProduct);

        this.nextElementSibling.onchange();
    });

    $(".btnPlus").click(function () {
        $(".input-quantity-product").attr("max", 5);

        this.previousElementSibling.stepUp();

        const getId = $(this).prev().attr("data-id");
        const getAmountProduct = $(this).prev().val();

        ChangePriceProduct(getId, getAmountProduct);

        //Saves the change to input
        this.previousElementSibling.onchange();
    });
});

//A function for the fact that if the user removes "readonly" in the input,
//this function will not allow going beyond the range of values
$(document).ready(function () {
    $(".input-quantity-product").on("input", function () {

        const value = $(this).val();

        if ((value !== "") && (value.indexOf(".") === -1)) {

            $(this).val(Math.max(Math.min(value, 5), 1));
        }
    });
});

//Does not allow you to click farther if you have not clicked familiar with the user agreement
$(document).ready(function () {
    $("#agreementUser").click(function () {
        if ($(this).is(":checked")) {
            $("#btnFurtherOrder").removeAttr("disabled");
        } else {
            $("#btnFurtherOrder").attr("disabled", "disabled");
        }
    });
});


