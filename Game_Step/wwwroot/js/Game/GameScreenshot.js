$(document).ready(function () {
    $(".del-button").click(function () {
        let getId = $(this).attr('data-id');
        $.ajax({
            url: window.location.origin + "/GameScreenshot/Delete",
            type: "Get",
            data: { 'id': getId },
            success: function () {
                location.reload();
            },
        });

    });
});


$(".button-add").click(function () {
    $(this).next().trigger("click");
});  