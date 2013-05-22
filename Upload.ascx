<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Upload.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.Upload" %>
<div class="row fileupload-buttonbar">
 <div class="span7">
  <span class="btn btn-success fileinput-button"><i class="icon-plus icon-white"></i>
   <span><%=Resx("AddFiles")%></span>
   <input id="fileupload" type="file" name="files[]" data-url="<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/API/Upload")%>?TabId=<%=TabId%>&ModuleId=<%=ModuleId%>"
    multiple="1" />
  </span>
  <button type="reset" class="btn btn-warning cancel">
   <i class="icon-ban-circle icon-white"></i><span><%=Resx("CancelUpload")%></span>
  </button>
 </div>
 <div class="span5">
  <!-- The global progress bar -->
  <div class="progress progress-success progress-striped active fade">
   <div class="bar" style="width: 0%;">
   </div>
  </div>
 </div>
</div>
<div class="fileupload-loading">
</div>
<br />
<div id="progress" class="progress progress-striped progress-success">
 <div class="bar" style="width: 0%;">
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
   serviceUrl: '<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/API/")%>',
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
