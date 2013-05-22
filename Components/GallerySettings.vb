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

Imports DotNetNuke.Services.Tokens

Public Class GallerySettings
 Implements IPropertyAccess

#Region " Constructor "
 Public Sub New(ByVal ModuleId As Integer)

  _ModuleId = ModuleId
  _Settings = (New DotNetNuke.Entities.Modules.ModuleController).GetModuleSettings(ModuleId)
  Common.ReadValue(_Settings, "Width", Width)
  Common.ReadValue(_Settings, "Height", Height)
  Common.ReadValue(_Settings, "ZoomWidth", ZoomWidth)
  Common.ReadValue(_Settings, "ZoomHeight", ZoomHeight)
  Common.ReadValue(_Settings, "FitType", FitType)
  Common.ReadValue(_Settings, "ZoomFitType", ZoomFitType)
  Common.ReadValue(_Settings, "Template", Template)

  _PortalModulePath = DotNetNuke.Entities.Portals.PortalSettings.Current.HomeDirectory
  _PortalModuleMapPath = DotNetNuke.Entities.Portals.PortalSettings.Current.HomeDirectoryMapPath
  If Not _PortalModulePath.EndsWith("/") Then
   _PortalModulePath &= "/"
  End If
  _PortalModulePath &= String.Format("YAG/", ModuleId)
  _imagePath = String.Format("{0}{1}/", _PortalModulePath, ModuleId)
  If Not _PortalModuleMapPath.EndsWith("\") Then
   _PortalModuleMapPath &= "\"
  End If
  _PortalModuleMapPath &= String.Format("YAG\", ModuleId)
  _imageMapPath = String.Format("{0}{1}\", _PortalModuleMapPath, ModuleId)
  If Not IO.Directory.Exists(_imageMapPath) Then
   IO.Directory.CreateDirectory(_imageMapPath)
  End If

  _portalTemplatesMapPath = String.Format("{0}Templates\", _PortalModuleMapPath)
  If Not IO.Directory.Exists(_portalTemplatesMapPath) Then
   IO.Directory.CreateDirectory(_portalTemplatesMapPath)
  End If
  _PortalTemplatesPath = String.Format("{0}Templates/", _PortalModulePath)

  ' Template Settings - first load defaults
  SetTemplate(Template)

 End Sub
#End Region

#Region " Public Methods "
 Public Sub Save()
  Dim objModules As New DotNetNuke.Entities.Modules.ModuleController
  objModules.UpdateModuleSetting(_ModuleId, "Width", Me.Width.ToString)
  objModules.UpdateModuleSetting(_ModuleId, "Height", Me.Height.ToString)
  objModules.UpdateModuleSetting(_ModuleId, "ZoomWidth", Me.ZoomWidth.ToString)
  objModules.UpdateModuleSetting(_ModuleId, "ZoomHeight", Me.ZoomHeight.ToString)
  objModules.UpdateModuleSetting(_ModuleId, "FitType", Me.FitType)
  objModules.UpdateModuleSetting(_ModuleId, "ZoomFitType", Me.ZoomFitType)
  objModules.UpdateModuleSetting(_ModuleId, "Template", Me.Template)
  DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(_ModuleId), Me)
 End Sub

 Public Sub SaveTemplateSettings()
  Dim objModules As New DotNetNuke.Entities.Modules.ModuleController
  For Each key As String In TemplateSettings.Keys
   objModules.UpdateModuleSetting(_ModuleId, "t_" & key, TemplateSettings(key))
  Next
 End Sub

 Public Sub SetTemplateSetting(key As String, value As String)
  If Not TemplateSettings.ContainsKey(key) Then
   TemplateSettings.Add(key, value)
  Else
   TemplateSettings(key) = value
  End If
 End Sub
#End Region

#Region " Properties "
 Private Property ModuleId As Integer = -1
 Private Property Settings As Hashtable
 Private Property PortalModulePath As String = ""
 Private Property PortalModuleMapPath As String = ""
 Private Property TemplateManager As Templating.TemplateManager
 Public Property Width As Integer = 80
 Public Property Height As Integer = 80
 Public Property ZoomWidth As Integer = 400
 Public Property ZoomHeight As Integer = 300
 Public Property FitType As String = "Crop"
 Public Property ZoomFitType As String = "Shrink"
 Public Property PortalTemplatesPath As String = ""
 Public Property TemplateSettings As New Dictionary(Of String, String)

 Private _imagePath As String = ""
 Public ReadOnly Property ImagePath() As String
  Get
   Return _imagePath
  End Get
 End Property

 Private _imageMapPath As String = ""
 Public ReadOnly Property ImageMapPath() As String
  Get
   Return _imageMapPath
  End Get
 End Property

 Private _template As String = "[G]_default"
 Public Property Template() As String
  Get
   Return _template
  End Get
  Set(ByVal value As String)
   _template = value
   SetTemplate(value)
  End Set
 End Property

 Private _portalTemplatesMapPath As String = ""
 Public ReadOnly Property PortalTemplatesMapPath As String
  Get
   Return _portalTemplatesMapPath
  End Get
 End Property

 Dim _version As String = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString
 Public ReadOnly Property Version As String
  Get
   Return _version
  End Get
 End Property
#End Region

#Region " Static Methods "
 Public Shared Function GetGallerySettings(ByVal ModuleId As Integer) As GallerySettings
  Dim modSettings As GallerySettings = Nothing
  Try
   modSettings = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(CacheKey(ModuleId)), GallerySettings)
  Catch
  End Try
  If modSettings Is Nothing Then
   modSettings = New GallerySettings(ModuleId)
   DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(ModuleId), modSettings)
  End If
  Return modSettings
 End Function

 Private Shared Function CacheKey(ByVal ModuleId As Integer) As String
  Return "GallerySettings" & ModuleId.ToString
 End Function
#End Region

#Region " Private Methods "
 Private Sub SetTemplate(template As String)
  _TemplateManager = New Bring2mind.DNN.Modules.YAG.Templating.TemplateManager(DotNetNuke.Entities.Portals.PortalSettings.Current, Me, template)
  TemplateSettings.Clear()
  For Each st As Templating.TemplateSetting In _TemplateManager.TemplateSettings.Settings
   TemplateSettings.Add(st.Key, st.DefaultValue)
  Next
  For Each key As String In _Settings.Keys
   If key.StartsWith("t_") Then
    SetTemplateSetting(Mid(key, 3), CStr(_Settings(key)))
   End If
  Next
 End Sub
#End Region

#Region " IPropertyAccess "
 Public ReadOnly Property Cacheability As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
  Get
   Return CacheLevel.fullyCacheable
  End Get
 End Property

 Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, AccessingUser As DotNetNuke.Entities.Users.UserInfo, AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
  Dim OutputFormat As String = String.Empty
  If strFormat = String.Empty Then
   OutputFormat = "D"
  Else
   OutputFormat = strFormat
  End If
  Select Case strPropertyName.ToLower
   Case "moduleid"
    Return (_ModuleId.ToString(OutputFormat, formatProvider))
   Case "width"
    Return (Me.Width.ToString(OutputFormat, formatProvider))
   Case "height"
    Return (Me.Height.ToString(OutputFormat, formatProvider))
   Case "zoomwidth"
    Return (Me.ZoomWidth.ToString(OutputFormat, formatProvider))
   Case "zoomheight"
    Return (Me.ZoomHeight.ToString(OutputFormat, formatProvider))
   Case "fittype"
    Return PropertyAccess.FormatString(Me.FitType, strFormat)
   Case "imagepath"
    Return PropertyAccess.FormatString(Me.ImagePath, strFormat)
   Case "imagemappath"
    Return PropertyAccess.FormatString(Me.ImageMapPath, strFormat)
   Case "portaltemplatespath"
    Return PropertyAccess.FormatString(Me.PortalTemplatesPath, strFormat)
   Case "portaltemplatesmappath"
    Return PropertyAccess.FormatString(Me.PortalTemplatesMapPath, strFormat)
   Case "templatepath"
    Return PropertyAccess.FormatString(_TemplateManager.TemplatePath, strFormat)
   Case "templatemappath"
    Return PropertyAccess.FormatString(_TemplateManager.TemplateMapPath, strFormat)
   Case Else
    If TemplateSettings.ContainsKey(strPropertyName) Then
     Return PropertyAccess.FormatString(CStr(TemplateSettings(strPropertyName)), strFormat)
    End If
    If strPropertyName.StartsWith("t_") Then strPropertyName = Mid(strPropertyName, 3)
    If TemplateSettings.ContainsKey(strPropertyName) Then
     Return PropertyAccess.FormatString(CStr(TemplateSettings(strPropertyName)), strFormat)
    End If
    Return ""
  End Select
 End Function
#End Region

End Class
