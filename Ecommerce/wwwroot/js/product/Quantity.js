$(document).ready(function () {
    // Handle the plus button click
    $('#button-plus').click(function () {
        var currentVal = parseInt($('#quantityInput').val());
        if (!isNaN(currentVal)) {
            $('#quantityInput').val(currentVal + 1);
        } else {
            $('#quantityInput').val(1);
        }
    });

    // Handle the minus button click
    $('#button-minus').click(function () {
        var currentVal = parseInt($('#quantityInput').val());
        if (!isNaN(currentVal) && currentVal > 1) {
            $('#quantityInput').val(currentVal - 1);
        } else {
            $('#quantityInput').val(1);
        }
    });
});
