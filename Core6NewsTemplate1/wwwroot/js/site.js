// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const myCarouselElement = document.querySelector('#myCarousel')

const carousel = new bootstrap.Carousel(myCarouselElement);


const humburg = document.getElementById("humburg");
const sumMenu = document.querySelector(".subMenu");
let toggler = false;
humburg.addEventListener("click", function () {
    toggler = !toggler;
    if (toggler) {
        sumMenu.style.top = "0";
        sumMenu.style.opacity = "100";
        sumMenu.style.display = "block";

    } else {
        sumMenu.style.top = "-100%";
        sumMenu.style.opacity = "0";
        sumMenu.style.display = "none";



    }
    console.log(toggler);
})
$(document).ready(function () {

    // hide navbar on scroll
    $(window).scroll(function () {
        let scroll = $(window).scrollTop();

        if (scroll > 100) {
            $(".NavHeader").fadeOut();

        } else {
            $(".NavHeader").fadeIn();
        }
    })
    // post images slider

    const Dir = document.documentElement.getAttribute("dir");


    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        dots: true,
        rtl: Dir == "rtl" ? true : false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            },
            1300: {
                items: 4
            }
        }
    })
});
