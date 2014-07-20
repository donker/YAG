<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Upload.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.Upload" %>
<div class="yag-row fileupload-buttonbar">
 <div class="yag-span7">
  <span class="yag-btn yag-btn-success fileinput-button" id="uploadSpan"><i class="yag-icon-plus yag-icon-white"></i>
   <span><%=LocalizeString("AddFiles")%></span>
   <input id="fileupload" type="file" name="files[]" data-url="<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/API/Upload")%>"
    multiple="1" />
  </span>
  <button type="reset" class="yag-btn yag-btn-warning yag-cancel">
   <i class="yag-icon-ban-circle yag-icon-white"></i><span><%=LocalizeString("CancelUpload")%></span>
  </button>
 </div>
</div>
<div class="fileupload-loading">
</div>
<br />
<div id="progress" class="yag-progress yag-progress-striped yag-progress-success" style="height:20px;">
 <div class="yag-bar" style="width: 0%;">
 </div>
</div>
<div class="files">
</div>
<div id="results">
</div>

<p class="yag_buttonBar">
 <asp:LinkButton runat="server" ID="cmdReturn" resourcekey="cmdReturn" CssClass="dnnPrimaryAction" />
</p>

<!--[if gte IE 8]><script src="<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/")%>js/jquery.xdr-transport.js"></script><![endif]-->

<script type="text/javascript">
var yagService

(function ($, Sys) {

 $.fn.dnnFileInput = function () { };
 $(':file').dnnFileInput();

 $(document).ready(function () {

  yagService = new YagService($, {
   serverErrorText: '<%= LocalizeJSString("ServerError") %>',
   serverErrorWithDescriptionText: '<%= LocalizeJSString("ServerErrorWithDescription") %>',
   errorBoxId: '#yagServiceErrorBox<%= ModuleId %>'
  },
  <%= ModuleId %>);

  $('#fileupload').yagUpload({
   moduleId: '<%=ModuleId%>',
   tabId: '<%=TabId%>',
   localization: {
    deleteConfirm: '<%=LocalizeString("Delete.Confirm")%>',
    uploaded: '<%=LocalizeString("Uploaded")%>',
    queued: '<%=LocalizeString("Queued")%>',
    cmdDelete: '<%=LocalizeString("Delete")%>'
   }
  });

 }); // doc ready

} (jQuery, window.Sys));
</script>
