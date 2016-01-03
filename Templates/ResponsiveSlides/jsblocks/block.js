$(document).ready(function() {
  $('#p[settings:moduleid]').responsiveSlides({
    auto: [settings:auto],
    speed: [settings:speed],
    timeout: [settings:timeout],
    pager: [settings:pager],
    nav: [settings:nav],
    random: [settings:random],
    pause: [settings:pause],
    pauseControls: [settings:pauseControls],
    prevText: '[settings:prevText]',
    nextText: '[settings:nextText]',
    maxwidth: '[settings:maxwidth]'
  });
});
