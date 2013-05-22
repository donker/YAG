'
' Bring2mind - http://www.bring2mind.net
' Copyright (c) 2012
' by Bring2mind
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Security
Imports Bring2mind.DNN.Modules.YAG.Templating

Public Class YAG
 Inherits ModuleBase
 Implements IActionable

#Region " Private Members "
 Private _urlParameters As New List(Of String)
 Private _pageSize As Integer = -1
 Private _nrRecords As Integer = 0
 Private _startRec As Integer = -1
 Private _endRec As Integer = -1
 Private _reqPage As Integer = 1
 Private _usePaging As Boolean = False
#End Region

#Region " Properties "
#End Region

#Region " Event Handlers "
 Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  DotNetNuke.Framework.jQuery.RequestRegistration()
  Common.ReadValue(Me.Request.Params, "Page", _reqPage)
  DataBind()

 End Sub

 Private Sub vtContents_GetData(ByVal DataSource As String, ByVal Parameters As Dictionary(Of String, String), ByRef Replacers As System.Collections.Generic.List(Of GenericTokenReplace), ByRef Arguments As System.Collections.Generic.List(Of String())) Handles vtContents.GetData

  Select Case DataSource.ToLower

   Case "images"

    Dim album As New ImageCollection(Settings.ImageMapPath)

    _pageSize = Integer.Parse(Parameters("pagesize"))
    If _pageSize > 0 Then
     _usePaging = True
     _startRec = ((_reqPage - 1) * _pageSize) + 1
     _endRec = _reqPage * _pageSize
     Dim i As Integer = 1
     For Each img As Image In album.Images
      If i >= _startRec And i <= _endRec Then
       Replacers.Add(New GalleryTokenReplace(ModuleConfiguration, Settings, img))
      End If
      i += 1
     Next
    Else
     For Each img As Image In album.Images
      Replacers.Add(New GalleryTokenReplace(ModuleConfiguration, Settings, img))
     Next
    End If

  End Select

 End Sub
#End Region

#Region " Overrides "
 Public Overrides Sub DataBind()

  Dim tmgr As New TemplateManager(PortalSettings, Settings, Settings.Template)
  With vtContents
   .TemplatePath = tmgr.TemplatePath
   .TemplateRelPath = tmgr.TemplateRelPath
   .TemplateMapPath = tmgr.TemplateMapPath
   .DefaultReplacer = New GalleryTokenReplace(ModuleConfiguration, Settings)
  End With
  vtContents.DataBind()

 End Sub
#End Region

#Region " Optional Interfaces "
 Public ReadOnly Property ModuleActions() As ModuleActionCollection Implements IActionable.ModuleActions
  Get
   Dim Acts As New ModuleActionCollection
   Acts.Add(GetNextActionID, Localization.GetString("Upload", LocalResourceFile), ModuleActionType.EditContent, "", "", EditUrl("Upload"), False, SecurityAccessLevel.Edit, True, False)
   Acts.Add(GetNextActionID, Localization.GetString(ModuleActionType.EditContent, LocalResourceFile), ModuleActionType.EditContent, "", "", EditUrl(), False, SecurityAccessLevel.Edit, True, False)
   Acts.Add(GetNextActionID, Localization.GetString("TemplateSettings", LocalResourceFile), ModuleActionType.EditContent, "", "", EditUrl("TemplateSettings"), False, SecurityAccessLevel.Edit, True, False)
   Return Acts
  End Get
 End Property
#End Region


End Class