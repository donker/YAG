

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

Public Class ModuleBase
 Inherits DotNetNuke.Entities.Modules.PortalModuleBase

 Private _settings As GallerySettings
 Public Shadows Property Settings As GallerySettings
  Get
   If _settings Is Nothing Then
    _settings = GallerySettings.GetGallerySettings(ModuleId)
   End If
   Return _settings
  End Get
  Set(value As GallerySettings)
   _settings = value
  End Set
 End Property

 Public Function LocalizeJSString(resourceKey As String) As String
  Return DotNetNuke.UI.Utilities.ClientAPI.GetSafeJSString(LocalizeString(resourceKey))
 End Function

 Public Sub RegisterStyleSheet(styleSheet As String)
  DotNetNuke.Web.Client.ClientResourceManagement.ClientResourceManager.RegisterStyleSheet(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/Yag/css/" & styleSheet & "?_=" & Settings.Version))
 End Sub

 Public Sub RegisterScript(scriptFile As String, priority As Integer)
  DotNetNuke.Web.Client.ClientResourceManagement.ClientResourceManager.RegisterScript(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/Yag/js/" & scriptFile & "?_=" & Settings.Version), priority)
 End Sub

 Public Sub AddYagService()

  If Context.Items("YagServiceAdded") Is Nothing Then
   DotNetNuke.Framework.jQuery.RequestDnnPluginsRegistration()
   DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxScriptSupport()
   DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxAntiForgerySupport()
   RegisterScript("bring2mind.yag.js", 70)
   Context.Items("YagServiceAdded") = True
  End If

 End Sub

#Region " Page Events "
 Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
  DotNetNuke.Framework.jQuery.RequestRegistration()
 End Sub
#End Region

End Class
