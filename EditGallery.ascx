<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditGallery.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.EditGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<ul id="images">
<asp:Repeater ID="rpImages" runat="server">
 <ItemTemplate>
 <li class="ui-state-default yag-well" id="image_<%#Eval("file").Replace("-","T")%>">
  <div class="delbutton">
   <a class="yag-close" id="delete<%#Eval("file")%>" href="#">&times;</a>
  </div>
  <div class="imagetn">
   <img src="<%=Settings.ImagePath%><%#Eval("file")%>_tn<%#Eval("extension")%>" alt="<%#Eval("title")%>" />
  </div>
  <input type="text" class="textbox" placeholder="Title" value="<%#Eval("title")%>" id="title-<%#Eval("file")%>" />
  <textarea class="textbox" rows="3" id="remarks-<%#Eval("file")%>"><%#Eval("remarks")%></textarea>
 </li>
 </ItemTemplate>
</asp:Repeater>
</ul>

<p style="clear:both;width:100%;text-align:center;padding-top:40px;" id="exitControls">
 <asp:LinkButton runat="server" ID="cmdReturn" resourcekey="cmdReturn" CssClass="dnnPrimaryAction" />
 <asp:LinkButton runat="server" ID="cmdUpload" resourcekey="cmdUpload" CssClass="dnnSecondaryAction" />
</p>

<script type="text/javascript">
 $(function () {
  $('#images').yagEdit({
   serviceUrl: '<%=ResolveUrl("~/DesktopModules/Bring2mind/YAG/API/")%>',
   moduleId: '<%=ModuleId%>',
   tabId: '<%=TabId%>',
   localization: { deleteConfirm: '<%=Resx("Delete.Confirm")%>' }
  });
 });
</script>
