Imports DotNetNuke.Web.Client.ClientResourceManagement

Public Class Upload
 Inherits ModuleBase

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  AddYagService()

  RegisterStyleSheet("bootstrap.yag.css")
  RegisterStyleSheet("bootstrap-responsive.yag.css")
  RegisterStyleSheet("fileupload.css")

  RegisterScript("jquery.ui.widget.js", 60)
  RegisterScript("jquery.iframe-transport.js", 60)
  RegisterScript("jquery.fileupload.js", 60)

  RegisterScript("upload.js", 70)

  If Not Me.IsPostBack Then
   If IO.File.Exists(Settings.ImageMapPath & UserInfo.UserID.ToString & ".resources") Then IO.File.Delete(Settings.ImageMapPath & UserInfo.UserID.ToString & ".resources")
  End If

 End Sub

 Private Sub cmdReturn_Click(sender As Object, e As System.EventArgs) Handles cmdReturn.Click
  For Each f As String In IO.Directory.GetFiles(Settings.ImageMapPath, "*.resources")
   Try
    IO.File.Delete(f)
   Catch ex As Exception
   End Try
  Next
  Me.Response.Redirect(EditUrl(), False)
 End Sub
End Class