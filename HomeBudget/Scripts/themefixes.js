jQuery(document).ready(function () {
//    $("input.text-box").wrap(function () {
//        return '<div class="wrapper"><div class="bg">'+$(this).html()+"</div></div>";
//    });
    $('input[type="text"], input[type="password"]').wrap('<div class="wrapper"></div>');

});