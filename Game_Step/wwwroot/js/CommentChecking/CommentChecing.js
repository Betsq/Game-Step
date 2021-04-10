$(document).ready(function () {
    $(".del-comment").click(function () {
        const id = $(this).attr("data-id");
        const thisBtn = $(this);
        $.ajax({
            url: window.location.origin + "/Comment/RemoveMainComment",
            type: "POST",
            data: { "id": id },
            success: function (result) {
                if (result === true) {
                    thisBtn.parent().remove();
                }
                else {
                    alert("Ошибка, комментарий не удален!");
                }
            },
        });
    });
});