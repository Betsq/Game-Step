$(document).ready(function () {
    $(".btnDelProdCart").click(function () {
        var modelCartId = this.getAttribute('del-data-id');
        $.ajax({
            url: window.location.origin + "/Cart/DeleteProductInCart",
            type: "Get",
            data: { 'id': modelCartId },
            success: function (result) {
                $("#cart").html(result);
                //When remove from the shopping cart we change to this value
                $('a.btn-add-cart').attr('href', 'javascript: void(0)');
            },
        });
    })
    document.getElementById("popup-1").classList.toggle("active");
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


