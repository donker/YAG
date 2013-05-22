'
' Bring2mind - http://www.bring2mind.net
' Copyright (c) 2011-2013
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

Imports DotNetNuke.Services.Exceptions

Imports Bring2mind.DNN.Modules.YAG.Templating

Public Class Settings
 Inherits DotNetNuke.Entities.Modules.ModuleSettingsBase

#Region " Properties "
 Private _settings As GallerySettings
 Public Shadows Property Settings() As GallerySettings
  Get
   If _settings Is Nothing Then
    _settings = GallerySettings.GetGallerySettings(ModuleId)
   End If
   Return _settings
  End Get
  Set(ByVal value As GallerySettings)
   _settings = value
  End Set
 End Property
#End Region

#Region " Events Handlers "
 Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

  If Not Me.IsPostBack Then
   ' Load up dropdown
   ddTemplate.Items.Clear()
   ddTemplate.Items.Add(New ListItem("Default [System]", "[G]_default"))
   For Each d As IO.DirectoryInfo In (New IO.DirectoryInfo(HttpContext.Current.Server.MapPath(DotNetNuke.Common.ResolveUrl(Common.glbTemplatesPath)))).GetDirectories
    If d.Name <> "_default" Then
     ddTemplate.Items.Add(New ListItem(d.Name & " [System]", "[G]" & d.Name))
    End If
   Next
   For Each d As IO.DirectoryInfo In (New IO.DirectoryInfo(Settings.PortalTemplatesMapPath)).GetDirectories
    ddTemplate.Items.Add(New ListItem(d.Name & " [Local]", "[P]" & d.Name))
   Next
  End If

 End Sub

 Private Sub cmdRegenerate_Click(sender As Object, e As System.EventArgs) Handles cmdRegenerate.Click

  With Settings
   .Width = Integer.Parse(txtWidth.Text)
   .Height = Integer.Parse(txtHeight.Text)
   .ZoomWidth = Integer.Parse(txtZoomWidth.Text)
   .ZoomHeight = Integer.Parse(txtZoomHeight.Text)
   .FitType = ddFitType.SelectedValue
   .ZoomFitType = ddZoomFitType.SelectedValue
  End With

  Dim killList As New List(Of String)
  For Each f As String In IO.Directory.GetFiles(Settings.ImageMapPath, "*_tn.*")
   killList.Add(f)
  Next
  For Each f As String In IO.Directory.GetFiles(Settings.ImageMapPath, "*_zoom.*")
   killList.Add(f)
  Next
  For Each f As String In killList
   Try
    IO.File.Delete(f)
   Catch ex As Exception
   End Try
  Next
  Dim r As New Resizer(Settings)
  For Each f As String In IO.Directory.GetFiles(Settings.ImageMapPath, "*.*")
   Try
    r.Process(f)
   Catch ex As Exception
   End Try
  Next

 End Sub

 Private Sub ddTemplate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddTemplate.SelectedIndexChanged
  CheckTemplateDescription(ddTemplate.SelectedValue)
 End Sub
#End Region

#Region " Base Method Implementations "
 ''' <summary>
 ''' Load the module's settings into the controls.
 ''' </summary>
 ''' <remarks>
 ''' </remarks>
 ''' <history>
 ''' 	[pdonker]	03/01/2008  Created
 ''' </history>
 Public Overrides Sub LoadSettings()
  Try
   If Not Page.IsPostBack Then
    With Settings
     txtWidth.Text = .Width.ToString
     txtHeight.Text = .Height.ToString
     txtZoomWidth.Text = .ZoomWidth.ToString
     txtZoomHeight.Text = .ZoomHeight.ToString
     Try
      ddFitType.Items.FindByValue(.FitType).Selected = True
     Catch ex As Exception
     End Try
     Try
      ddZoomFitType.Items.FindByValue(.ZoomFitType).Selected = True
     Catch ex As Exception
     End Try
     Try
      ddTemplate.Items.FindByValue(Settings.Template).Selected = True
     Catch ex As Exception
     End Try
     CheckTemplateDescription(Settings.Template)
    End With
   End If
  Catch exc As Exception
   ProcessModuleLoadException(Me, exc)
  End Try
 End Sub

 ''' <summary>
 ''' Write settings back to the Module settings class.
 ''' </summary>
 ''' <remarks>
 ''' </remarks>
 ''' <history>
 ''' 	[pdonker]	03/01/2008  Created
 ''' </history>
 Public Overrides Sub UpdateSettings()
  Try

   With Settings
    .Width = Integer.Parse(txtWidth.Text)
    .Height = Integer.Parse(txtHeight.Text)
    .ZoomWidth = Integer.Parse(txtZoomWidth.Text)
    .ZoomHeight = Integer.Parse(txtZoomHeight.Text)
    .FitType = ddFitType.SelectedValue
    .ZoomFitType = ddZoomFitType.SelectedValue
    .Template = ddTemplate.SelectedValue
    .Save()
   End With

  Catch exc As Exception
   ProcessModuleLoadException(Me, exc)
  End Try
 End Sub
#End Region

#Region " Private Methods "
 Private Sub CheckTemplateDescription(Template As String)
  Dim tmgr As New Bring2mind.DNN.Modules.YAG.Templating.TemplateManager(DotNetNuke.Entities.Portals.PortalSettings.Current, Settings, Template)
  plhTemplateDescription.Controls.Clear()
  plhTemplateDescription.Controls.Add(New LiteralControl(tmgr.Description))
 End Sub
#End Region

End Class