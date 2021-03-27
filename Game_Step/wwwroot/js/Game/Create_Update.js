CKEDITOR.replace('Description')


$(document).ready(function () {
    const inpFile = document.getElementById("inpFile1");
    const previewContainer = document.getElementById("image-preview");
    const previewImage = previewContainer.querySelector(".image-preview__image");
    const previewDefaultText = previewContainer.querySelector(".image-preview__default-text");

    inpFile.addEventListener("change", function () {
        const file = this.files[0];

        if (file) {
            const reader = new FileReader();

            previewDefaultText.style.display = "none";
            previewImage.style.display = "block";

            reader.addEventListener("load", function () {
                previewImage.setAttribute("src", this.result);
            });

            reader.readAsDataURL(file);
        }
    });
});

$(document).ready(function () {
    $("#upfile1").click(function () {
        $("#inpFile1").trigger("click");
    });
});

$(document).ready(function () {
    $("select").on("click", function () {

        $(this).parent(".select-box").toggleClass("open");

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
});

$(document).ready(function () {
    if ($("#discount-check").attr("checked") === 'checked') {
        $('#discount').show(1);
    }

    $("#discount-check").change(function () {
        if ($(this).prop('checked')) {
            $('#discount').show(350);
        } else {
            $('#discount').hide(350);
        }
    });
});