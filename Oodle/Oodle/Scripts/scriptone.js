//For our moving boxes on the about page
$('.box').click(function () {

    $(this).animate({
        left: '-50%'
    }, 500, function () {
        $(this).css('left', '150%');
        $(this).appendTo('#container');
    });

    $(this).next().animate({
        left: '50%'
    }, 500);
});




//Toggle FAQ section about page
$(document).ready(function () {
    $(".content-box").hide();
    $(".contorol").click(function () {
        $(this).next(".content-box").slideToggle().siblings(".content-box").slideUp();
        if ($("i").hasClass("fa-plus")) {
            $(this).find("i").toggleClass("fa-minus");

        }
    });
});