; (function ($, window, document, undefined) {
 var pluginName = 'yagEdit',
  defaults = {
   serviceUrl: '',
   moduleId: '-1',
   tabId: '-1',
   localization: {deleteConfirm : 'Do you really want to delete this picture?'}
  };

 function yagEdit(element, options) {
  this.element = element;
  this.options = $.extend({}, defaults, options);
  this._defaults = defaults;
  this._name = pluginName;
  this.init();
  
  var hasChanges = false;

  $(this.element).sortable({
   update: function(event, ui) {
            hasChanges = true;
           }

  });
  $(this.element).disableSelection();

  $(this.element).find('.textbox').change(function() {
    $.post(options.serviceUrl+'Edit?TabId='+options.tabId+'&ModuleId='+options.moduleId,
        { control: this.id, value: this.value });
    // alert($(this.element).sortable('serialize'));
   });

  $('#exitControls a').click(function() {
   if (hasChanges) {
    $.post(options.serviceUrl+'Reorder?TabId='+options.tabId+'&ModuleId='+options.moduleId,
        { order: $(element).sortable('serialize') });
   };   
   return true;
  });
  
  $(this.element).find('li div.delbutton a.close').click(function () {
   if (confirm(options.localization.deleteConfirm)) {
    $.post(options.serviceUrl+'Delete?TabId='+options.tabId+'&ModuleId='+options.moduleId,
        { fileName: this.id.substring(6) });
    $(this).parent().parent().remove();
   };
  });
 }

// aux functions

 yagEdit.prototype.init = function () {
 };

 $.fn[pluginName] = function (options) {
  return this.each(function () {
   if (!$.data(this, 'plugin_' + pluginName)) {
    $.data(this, 'plugin_' + pluginName,
                new yagEdit(this, options));
   }
  });
 }

})(jQuery, window, document);




