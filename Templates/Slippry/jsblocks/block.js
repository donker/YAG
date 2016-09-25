$(document).ready(function() {
  $('#slippry[settings:moduleid] a[href=""]').click(function(e) {
    e.preventDefault();
  });
  $('#slippry[settings:moduleid]').slippry({
    captions: '[settings:captions]'=='none'?false:'[settings:captions]',
    responsive: [settings:responsive],
    pager: [settings:pager],
    controls: [settings:controls],
    speed: [settings:speed],
    pause: [settings:pause],
    transition: '[settings:transition]'=='none'?false:'[settings:transition]'
  });
});
