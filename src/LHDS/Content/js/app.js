$(document).ready(function () {
    $('.owl-carousel').owlCarousel({
        loop:true,
        margin:10,
        autoplay:true,
        nav:true,
        dots:true,
        responsive:{

            0:{

                items:1
            },
            600:{

                items:1
            },
            1000:{

                items:1
            }
        }
    })

    $(window).on('scroll', function () {
        if ($(window).scrollTop() > 200) {
            $('div#floatingHeader').addClass("floatingHeaderFixed");
        } else {
            $('div#floatingHeader').removeClass("floatingHeaderFixed");
        }
    });
});
