<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="YAG.ascx.vb" Inherits="Bring2mind.DNN.Modules.YAG.YAG" %>
<%@ Register TagPrefix="sg" Assembly="Bring2mind.DNN.Modules.YAG" Namespace="Bring2mind.DNN.Modules.YAG.Templating" %>

<sg:ViewTemplate runat="server" id="vtContents" />

<div class="yag_buttonBar">
 <asp:LinkButton runat="server" ID="cmdEdit" resourcekey="EditContent.Action" CssClass="dnnPrimaryAction" Visible="false" />
</div>