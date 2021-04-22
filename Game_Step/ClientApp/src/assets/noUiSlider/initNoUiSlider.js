window.onload = function() {
    var sliderPrice = document.getElementById('sliderPrice');

    noUiSlider.create(sliderPrice, {
        start: [0, 4999],
        connect: true,
        step: 1,
        range: {
            'min': 0,
            'max': 4999
        },
        format: {
            to: function (value) {
                return parseInt(value);
            },
            from: function (value) {
                return parseInt(value);
            }
        }
    });

    sliderPrice.noUiSlider.on("update", (values, handel) => {

        document.getElementById("minPrice").value = values[0];
        document.getElementById("maxPrice").value = values[1];
    });
}

