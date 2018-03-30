function myFunction(element) {
    var isZichtbaar = $(element).is(":visible");
    if (isZichtbaar) {
        $(element).slideUp();
    }
    else {
        $(element).slideDown(500);
    }
};