<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Upload.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.Upload" %>
<div class="yag-row fileupload-buttonbar">
 <div class="yag-span7">
  <span class="yag-btn yag-btn-success fileinput-button"><i class="yag-icon-plus yag-icon-white"></i>
   <span><%=Resx("AddFiles")%></span>
  </span>
   <input id="fileupload" type="file" name="files[]" data-url="<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/API/Upload")%>"
    multiple="1" class="nothing" />
  <button type="reset" class="yag-btn yag-btn-warning yag-cancel">
   <i class="yag-icon-ban-circle yag-icon-white"></i><span><%=Resx("CancelUpload")%></span>
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

<p style="width:100%;text-align:center;padding-top:40px;">
 <asp:LinkButton runat="server" ID="cmdReturn" resourcekey="cmdReturn" CssClass="dnnPrimaryAction" />
</p>

<!--[if gte IE 8]><script src="<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/")%>js/jquery.xdr-transport.js"></script><![endif]-->

<script type="text/javascript">
 $(function () {
  $('#fileupload').yagUpload({
   moduleId: '<%=ModuleId%>',
   tabId: '<%=TabId%>',
   localization: {
    deleteConfirm: '<%=Resx("Delete.Confirm")%>',
    uploaded: '<%=Resx("Uploaded")%>',
    queued: '<%=Resx("Queued")%>',
    cmdDelete: '<%=Resx("Delete")%>'
   }
  });
 });
</script>
