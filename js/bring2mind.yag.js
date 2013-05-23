function YagService($, settings, servicesFramework) {
 var baseServicepath = servicesFramework.getServiceRoot('Bring2mind/YAG');

 this.listCurrentFiles = function (success) {
  $.ajax({
   type: "GET",
   url: commentsServicepath,
   beforeSend: servicesFramework.setModuleHeaders,
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
   beforeSend: servicesFramework.setModuleHeaders,
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
   beforeSend: servicesFramework.setModuleHeaders,
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
   beforeSend: servicesFramework.setModuleHeaders,
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
   beforeSend: servicesFramework.setModuleHeaders,
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
