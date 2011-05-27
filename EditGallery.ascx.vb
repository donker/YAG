'
' Bring2mind - http://www.bring2mind.net
' Copyright (c) 2011
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

Public Class EditGallery
 Inherits ModuleBase

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  If Not Me.IsPostBack Then
   Me.DataBind()
  End If

 End Sub

 Public Overrides Sub DataBind()

  Dim album As New ImageCollection(Settings.ImageMapPath)
  dlExisting.DataSource = album.Images
  dlExisting.DataBind()

 End Sub

 Private Sub cmdUpload_Click(sender As Object, e As System.EventArgs) Handles cmdUpload.Click

  Dim origName As String = ctlUpload.FileName
  Dim ext As String = IO.Path.GetExtension(origName)
  Dim extOK As Boolean = False
  Select Case ext.ToLower
   Case ".jpg", ".png", ".gif"
    extOK = True
  End Select
  If Not extOK Then Throw New Exception("Must upload an image")

  Dim newFile As String = String.Format("{0}{1:yyyyMMdd}-{1:HHmmss}{2}", Settings.ImageMapPath, Now, ext)
  ctlUpload.SaveAs(newFile)

  Dim r As New Resizer(Settings)
  r.Process(newFile)

  Dim title As String = txtTitle.Text.Trim
  If title = "" Then title = IO.Path.GetFileNameWithoutExtension(ctlUpload.FileName)

  Dim album As New ImageCollection(Settings.ImageMapPath)
  album.Images.Add(New Image(newFile, title, txtRemarks.Text.Trim))
  album.Save()

  txtTitle.Text = ""
  txtRemarks.Text = ""

  Me.DataBind()

 End Sub

 Private Sub dlExisting_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlExisting.ItemCommand

  Dim file As String = CStr(e.CommandArgument)
  Dim album As New ImageCollection(Settings.ImageMapPath)
  Dim img As Image = album.Images.Find(Function(x)
                                        If x.File = file Then Return True
                                       End Function)
  If img IsNot Nothing Then
   Select Case e.CommandName.ToLower
    Case "up"
     If img.Order > 0 Then
      Dim img2 As Image = album.Images.Find(Function(x)
                                             If x.Order = img.Order - 1 Then Return True
                                            End Function)
      img.Order -= 1
      If img2 IsNot Nothing Then
       img2.Order += 1
      End If
     End If
     album.Sort()
    Case "down"
     Dim img2 As Image = album.Images.Find(Function(x)
                                            If x.Order = img.Order + 1 Then Return True
                                           End Function)
     img.Order += 1
     If img2 IsNot Nothing Then
      img2.Order -= 1
     End If
     album.Sort()
    Case "delete"
     Try
      IO.File.Delete(Settings.ImageMapPath & img.File & img.Extension)
      IO.File.Delete(Settings.ImageMapPath & img.File & "_tn" & img.Extension)
      IO.File.Delete(Settings.ImageMapPath & img.File & "_zoom" & img.Extension)
     Catch ex As Exception
     End Try
     album.Images.Remove(img)
   End Select
  End If
  album.Save()
  Me.DataBind()

 End Sub

 Private Sub cmdReturn_Click(sender As Object, e As System.EventArgs) Handles cmdReturn.Click
  Response.Redirect(DotNetNuke.Common.NavigateURL, False)
 End Sub
End Class