; (function ($, window, document, undefined) {
 var pluginName = 'yagEdit',
  defaults = {
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

  $(this.element).find('.textbox').change(function() {
    yagService.editFile(this.id, this.value, null)
   });

  $('#exitControls a').click(function() {
   if (hasChanges) {
    yagService.reorder($(element).sortable('serialize'), null)
   };   
   return true;
  });
  
  $(this.element).find('li div.delbutton a.yag-close').click(function () {
   if (confirm(options.localization.deleteConfirm)) {
    var id = this.id;
    yagService.deleteFile(id.substring(6), function() {
     $('#'+id).parent().parent().remove();
    })   
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




