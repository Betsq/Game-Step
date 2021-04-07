$(document).ready(function () {
    var modelId = $(".buy-button").attr('data-id');
    $.ajax({
        url: window.location.origin + "/Cart/IsProsuctInCart",
        type: "POST",
        data: { 'id': modelId },
        success: function (result) {
            if (result === true) {
                //When the product in cart, it changes the link of the button to go to the cart
                $('a.buy-button').attr('href', window.location.origin + '/Cart');
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
        if (!$(this).hasClass("in-cart")) {
            //Get from the attribute Id of the product
            var modelId = this.getAttribute('data-id');
            $.ajax({
                url: window.location.origin + "/Cart/AddToCart",
                type: "Get",
                data: { 'id': modelId },
                success: function (result) {
                    $("#cart").html(result);
                    //When adding a product, it changes the link of the button to go to the cart
                    $('a.buy-button').attr('href', window.location.origin + '/Cart');
                    //When adding a product, it changes the class
                    $('a.buy-button').attr('class', 'buy-button in-cart');

                    $('a.buy-button').text('В КОРЗИНЕ');
                },
            });
        }
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



function toggleScreenshotPopup() {
    document.getElementById("popup-screenshot").classList.toggle("screenshot-active");
}

var slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("slide-image");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}





