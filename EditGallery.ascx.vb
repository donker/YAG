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

Imports DotNetNuke.Web.Client.ClientResourceManagement

Public Class EditGallery
 Inherits ModuleBase

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  DotNetNuke.Framework.jQuery.RequestUIRegistration()
  AddYagService()
  RegisterStyleSheet("bootstrap.yag.css")
  RegisterStyleSheet("editgallery.css")
  RegisterScript("editgallery.js", 10)

  If Not Me.IsPostBack Then
   Me.DataBind()
  End If

 End Sub

 Public Overrides Sub DataBind()

  Dim album As New ImageCollection(Settings.ImageMapPath)
  album.Recheck()
  rpImages.DataSource = album.Images
  rpImages.DataBind()

 End Sub

 Private Sub cmdReturn_Click(sender As Object, e As System.EventArgs) Handles cmdReturn.Click
  Response.Redirect(DotNetNuke.Common.NavigateURL, False)
 End Sub

 Private Sub cmdUpload_Click(sender As Object, e As System.EventArgs) Handles cmdUpload.Click
  Response.Redirect(EditUrl("Upload"), False)
 End Sub
End Class