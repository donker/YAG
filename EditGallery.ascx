<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditGallery.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.EditGallery" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<asp:DataList ID="dlExisting" runat="server">
 <ItemTemplate>
  <div class="imageItem">
   <div class="imageHolder">
    <img src="<%=Settings.ImagePath%><%#Eval("file")%>_tn<%#Eval("extension")%>" alt="<%#Eval("title")%>" />
    <div class="title"><%#Eval("title")%></div>
    <div class="remarks"><%#Eval("remarks")%></div>
    <asp:ImageButton runat="server" ID="cmdDelete" ImageUrl="~/images/delete.gif" CommandName="Delete" resourcekey="cmdDelete" CommandArgument='<%#Eval("file")%>' />
    <asp:ImageButton runat="server" ID="cmdUp" ImageUrl="~/images/up.gif" CommandName="Up" resourcekey="cmdDelete" CommandArgument='<%#Eval("file")%>' />
    <asp:ImageButton runat="server" ID="cmdDown" ImageUrl="~/images/dn.gif" CommandName="Down" resourcekey="cmdDelete" CommandArgument='<%#Eval("file")%>' />
   </div>
  </div>
 </ItemTemplate>
</asp:DataList>

<asp:Panel runat="server" ID="pnlUpload" CssClass="uploadPanel">
<table cellspacing="0" cellpadding="2" border="0">
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plFile" runat="server" controlname="ctlUpload" suffix=":" />
  </td>
  <td>
   <asp:FileUpload runat="server" ID="ctlUpload" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plTitle" runat="server" controlname="txtTitle" suffix=":" />
  </td>
  <td>
   <asp:TextBox runat="server" ID="txtTitle" Width="300" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plRemarks" runat="server" controlname="txtRemarks" suffix=":" />
  </td>
  <td>
   <asp:TextBox runat="server" ID="txtRemarks" Width="300" Height="100" TextMode="MultiLine" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
  </td>
  <td>
   <asp:Button runat="server" ID="cmdUpload" resourcekey="cmdUpload" />
  </td>
 </tr>
</table>
</asp:Panel>

<p>
 <asp:LinkButton runat="server" ID="cmdReturn" resourcekey="cmdReturn" CssClass="CommandButton" />
</p>