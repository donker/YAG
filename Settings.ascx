<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Settings.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<table cellspacing="0" cellpadding="2" border="0">
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plWidth" runat="server" controlname="txtWidth" suffix=":" />
  </td>
  <td>
   <asp:textbox id="txtWidth" runat="server" width="60" maxlength="10" cssclass="NormalTextBox" />&nbsp;
   <asp:comparevalidator ID="Comparevalidator1" runat="server" resourcekey="WholeNr.Error" operator="DataTypeCheck" type="Integer" controltovalidate="txtWidth" display="Dynamic" />&nbsp;
   <asp:requiredfieldvalidator ID="Requiredfieldvalidator1" runat="server" resourcekey="Required.Error" controltovalidate="txtWidth" display="Dynamic" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plHeight" runat="server" controlname="txtHeight" suffix=":" />
  </td>
  <td>
   <asp:textbox id="txtHeight" runat="server" width="60" maxlength="10" cssclass="NormalTextBox" />&nbsp;
   <asp:comparevalidator ID="Comparevalidator2" runat="server" resourcekey="WholeNr.Error" operator="DataTypeCheck" type="Integer" controltovalidate="txtHeight" display="Dynamic" />&nbsp;
   <asp:requiredfieldvalidator ID="Requiredfieldvalidator2" runat="server" resourcekey="Required.Error" controltovalidate="txtHeight" display="Dynamic" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plFitType" runat="server" controlname="ddFitType" suffix=":" />
  </td>
  <td>
   <asp:DropDownList runat="server" ID="ddFitType">
    <asp:ListItem Value="Crop" Resourcekey="optCrop" />
    <asp:ListItem Value="Shrink" Resourcekey="optShrink" />
    <asp:ListItem Value="Stretch" Resourcekey="optStretch" />
   </asp:DropDownList>
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plZoomWidth" runat="server" controlname="txtZoomWidth" suffix=":" />
  </td>
  <td>
   <asp:textbox id="txtZoomWidth" runat="server" width="60" maxlength="10" cssclass="NormalTextBox" />&nbsp;
   <asp:comparevalidator ID="Comparevalidator3" runat="server" resourcekey="WholeNr.Error" operator="DataTypeCheck" type="Integer" controltovalidate="txtZoomWidth" display="Dynamic" />&nbsp;
   <asp:requiredfieldvalidator ID="Requiredfieldvalidator3" runat="server" resourcekey="Required.Error" controltovalidate="txtZoomWidth" display="Dynamic" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plZoomHeight" runat="server" controlname="txtZoomHeight" suffix=":" />
  </td>
  <td>
   <asp:textbox id="txtZoomHeight" runat="server" width="60" maxlength="10" cssclass="NormalTextBox" />&nbsp;
   <asp:comparevalidator ID="Comparevalidator4" runat="server" resourcekey="WholeNr.Error" operator="DataTypeCheck" type="Integer" controltovalidate="txtZoomHeight" display="Dynamic" />&nbsp;
   <asp:requiredfieldvalidator ID="Requiredfieldvalidator4" runat="server" resourcekey="Required.Error" controltovalidate="txtZoomHeight" display="Dynamic" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plZoomFitType" runat="server" controlname="ddZoomFitType" suffix=":" />
  </td>
  <td>
   <asp:DropDownList runat="server" ID="ddZoomFitType">
    <asp:ListItem Value="Crop" Resourcekey="optCrop" />
    <asp:ListItem Value="Shrink" Resourcekey="optShrink" />
    <asp:ListItem Value="Stretch" Resourcekey="optStretch" />
   </asp:DropDownList>
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plRegenerate" runat="server" controlname="cmdRegenerate" suffix=":" />
  </td>
  <td>
   <asp:Button runat="server" ID="cmdRegenerate" resourcekey="cmdRegenerate" />
  </td>
 </tr>
 <tr>
  <td class="SubHead" width="165">
   <dnn:label id="plTemplate" runat="server" controlname="ddTemplate" suffix=":" />
  </td>
  <td>
   <asp:DropDownList runat="server" ID="ddTemplate" AutoPostBack="true" />
  </td>
 </tr>
 <tr>
  <td colspan="2"><asp:PlaceHolder runat="server" ID="plhTemplateDescription" />

  </td>
 </tr>
</table>

