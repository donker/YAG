var swiperAR = 0;

$(document).ready(function () {
	var mySwiper = new Swiper('.swiper-container', {
		pagination: ".swiper-pagination",
		paginationClickable: true,
		nextButton: ".swiper-button-next",
		prevButton: ".swiper-button-prev",
		autoplay: [settings:autoplay],
		loop: true,
		effect: '[settings:effect]',
		setWrapperSize: true
	});
	setDivHeight();
});

$(window).resize(function () {
	setDivHeight();
});

function setDivHeight() {
	if (swiperAR == 0) {
		swiperAR = $('.swiper-img').height() / $('.swiper-img').width();
	}
	$('.swiper-wrapper').css({'height': $('.swiper-container').width() * swiperAR});
}