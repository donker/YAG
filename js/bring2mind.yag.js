function YagService($, settings, mid) {
 var moduleId = mid;
 var baseServicepath = $.dnnSF(moduleId).getServiceRoot('Bring2mind/YAG');

 this.listCurrentFiles = function (success) {
  $.ajax({
   type: "GET",
   url: commentsServicepath,
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: { }
  }).done(function (data) {
   if (success != undefined) {
    success(data);
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

 this.deleteFile = function (filename, success) {
  $.ajax({
   type: "POST",
   url: baseServicepath + "Delete",
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: { fileName: filename }
  }).done(function (data) {
   if (success != undefined) {
    success();
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

 this.commitFile = function (filename, success) {
  $.ajax({
   type: "POST",
   url: baseServicepath + "Commit",
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: { fileName: filename }
  }).done(function (data) {
   if (success != undefined) {
    success(data);
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

 this.editFile = function (control, value, success) {
  $.ajax({
   type: "POST",
   url: baseServicepath + "Edit",
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: { control: control, value: value }
  }).done(function (data) {
   if (success != undefined) {
    success();
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

 this.reorder = function (order, success) {
  $.ajax({
   type: "POST",
   url: baseServicepath + "Reorder",
   beforeSend: $.dnnSF(moduleId).setModuleHeaders,
   data: { order: order }
  }).done(function (data) {
   if (success != undefined) {
    success();
   }
  }).fail(function (xhr, status) {
   displayMessage(settings.errorBoxId, settings.serverErrorWithDescription + eval("(" + xhr.responseText + ")").ExceptionMessage, "dnnFormWarning");
  });
 };

}

function displayMessage(msgBoxId, message, cssclass) {
 var messageNode = $("<div/>")
                .addClass('dnnFormMessage ' + cssclass)
                .text(message);
 $(msgBoxId).prepend(messageNode);
 messageNode.fadeOut(3000, 'easeInExpo', function () {
  messageNode.remove();
 });
};
