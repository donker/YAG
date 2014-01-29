var yagService
jQuery(function ($) {
 yagService = new YagService($, {
   serverErrorText: '[resx:ServerError]',
   serverErrorWithDescriptionText: '[resx:ServerErrorWithDescription]',
   errorBoxId: '#yagServiceErrorBox[module:moduleId]'
  },
  [module:moduleID]);
});
