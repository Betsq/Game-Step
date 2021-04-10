$(document).ready(function () {
    $("#btn-adding-main-comment").click(function () {
        const serializeForm = $("#AddMainComment").serialize();

        $.ajax({
            url: window.location.origin + "/Comment/AddMainComment",
            type: "POST",
            data: serializeForm,
            success() {
                location.reload();
            },
        });
    });
});

$(document).ready(function () {
    $(".comments__btn-reply").click(function () {
        const serializeForm = $(this).parent().parent().parent().serialize();

        $.ajax({
            url: window.location.origin + "/Comment/AddSubComment",
            type: "POST",
            data: serializeForm,
            success: function () {
                location.reload();
            },
        });
    });
});

$(document).ready(function () {
    $(".btn-remove-comment").click(function () {
        if (confirm("Вы действительно хотите удалить?")) {
            const getId = $(this).attr("data-id");
            const thisButton = $(this);
            $.ajax({
                url: window.location.origin + "/Comment/RemoveMainComment",
                type: "POST",
                data: { "id": getId },
                success: function (result) {
                    if (result === true) {
                        $(thisButton).parentsUntil("ul.comments__list").hide(250, function () {
                            $(thisButton).parentsUntil("ul.comments__list").remove();
                        });
                    }
                    else {
                        alert("Произошла ошибка, комментарий не удален");
                    }
                },
            });

        }
    });
});

$(document).ready(function () {
    $(".btn-remove-sub-comment").click(function () {
        if (confirm("Вы действительно хотите удалить?")) {
            const getId = $(this).attr("data-id");
            const thisButton = $(this);
            $.ajax({
                url: window.location.origin + "/Comment/RemoveSubComment",
                type: "POST",
                data: { "id": getId },
                success: function (result) {
                    if (result === true) {
                        $(thisButton).parentsUntil("ul.sub-ul-comment").hide(250, function () {
                            $(thisButton).parentsUntil("ul.sub-ul-comment").remove();
                        });
                    }
                    else {
                        alert("Произошла ошибка, комментарий не удален");
                    }
                },
            });

        }
    });
});

//method for dispalying the ccomment container of the reply
$(document).ready(function () {
    $(".comments__reply").click(function () {

        let checker = $(this).parent().next().hasClass("reply-container-active");
        if (checker !== true) {
            $(".comments__reply-container").hide(200);
        }

        $(".comments__reply-container").removeClass("reply-container-active");

        $(this).parent().next().addClass("reply-container-active");
        $(this).parent().next().show(500);
    });
});

//method for hide the comment container of the reply
$(document).ready(function () {
    $(".comments__close-textarea").click(function () {
        $(this).parent().parent().parent().removeClass("reply-container-active");
        $(this).parent().parent().parent().hide(500);
    });
});
