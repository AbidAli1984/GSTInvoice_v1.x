/**
 *  Document   : login.js
 *  Author     : redstar
 *  Description: login form script
 *
 **/

// Toggle Function
$(document).on('click', '.toggle, .signin', function () {
    'use strict';
    var toggle = $('.toggle');
    // Switches the Icon and form
    if (toggle.children('i').attr('class') === 'fa fa-user-plus') {
        toggle.children('i').removeClass('fa-user-plus');
        toggle.children('i').addClass('fa-times');
        $('.formLogin').slideUp("slow");
        $('.formRegister').slideDown("slow");
    }
    else {
        toggle.children('i').removeClass('fa-times');
        toggle.children('i').addClass('fa-user-plus');
        $('.formLogin').slideDown("slow");
        if ($('.formRegister').is(':visible'))
            $('.formRegister').slideUp("slow");
        else
            $('.formReset').slideUp("slow");
    }

});

$(document).on('click', '.forgetPassword a', function () {
    'use strict';
    // Switches the Icon and form
    $('.toggle').children('i').removeClass('fa-user-plus');
    $('.toggle').children('i').addClass('fa-times');
    $('.formLogin').slideUp("slow");
    $('.formReset').slideDown("slow");
});

$(document).on('click', '.register', function () {
    'use strict';
    var toggle = $('.toggle');
    // Switches form
    if (toggle.children('i').attr('class') === 'fa fa-user-plus') {
        toggle.children('i').removeClass('fa-user-plus');
        toggle.children('i').addClass('fa-times');
    }
    $('.formRegister').slideDown("slow");
    if ($('.formLogin').is(':visible'))
        $('.formLogin').slideUp("slow");
    else
        $('.formReset').slideUp("slow");
});