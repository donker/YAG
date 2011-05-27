$(document).ready(function () {
 var settings = {
  overlayBgColor: '[settings:overlayBgColor]',
  overlayOpacity: 0.[settings:overlayOpacity],
  fixedNavigation: [settings:fixedNavigation],
  imageLoading: '[settings:templatepath]/[settings:imageLoading]',
  imageBtnPrev: '[settings:templatepath]/[settings:imageBtnPrev]',
  imageBtnNext: '[settings:templatepath]/[settings:imageBtnNext]',
  imageBtnClose: '[settings:templatepath]/[settings:imageBtnClose]',
  imageBlank: '[settings:templatepath]/[settings:imageBlank]',
  imageBtnBottomPrev: '[settings:templatepath]/[settings:imageBtnBottomPrev]',
  imageBtnBottomNext: '[settings:templatepath]/[settings:imageBtnBottomNext]',
  imageBtnPlay: '[settings:templatepath]/[settings:imageBtnPlay]',
  imageBtnStop: '[settings:templatepath]/[settings:imageBtnStop]',
  containerBorderSize: [settings:containerBorderSize],
  containerResizeSpeed: [settings:containerResizeSpeed],
  txtImage: '[resx:txtImage.Text]',
  txtOf: '[resx:txtOf.Text]',
  txtPrev: '[resx:txtPrev.Text]',
  txtNext: '[resx:txtNext.Text]'
 };
 $('#gallery[settings:moduleid] a').lightBox(settings);
});
