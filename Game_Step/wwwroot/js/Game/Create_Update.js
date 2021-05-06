CKEDITOR.replace("decsription");


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(input).next().attr("src", e.target.result);

            $(input).next().css("display", "block");

            $(input).next().next().css("display", "none");
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$(".clickInpImage").click(function () {
    $(this).next().trigger("click");

    $(this).next().change(function () {
        readURL(this);
    });
});


$(document).mouseup(function (e) {
    var container = $(".select-box");

    if (container.has(e.target).length === 0) {
        container.removeClass("open");
    }
});


$("select").on("change", function () {

    var selection = $(this).find("option:selected").text(),
        labelFor = $(this).attr("id"),
        label = $("[for='" + labelFor + "']");

    label.find(".label-desc").html(selection);

});

$(document).ready(function () {
    if ($("#discount-check").attr("checked") === "checked") {
        $("#discount").show(1);
    }

    $("#discount-check").change(function () {
        if ($(this).prop("checked")) {
            $("#discount").show(350);
        } else {
            $("#discount").hide(350);
        }
    });
});