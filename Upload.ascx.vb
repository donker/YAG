Imports DotNetNuke.Web.Client.ClientResourceManagement

Public Class Upload
 Inherits ModuleBase

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  DotNetNuke.Framework.jQuery.RequestRegistration()

  ClientResourceManager.RegisterStyleSheet(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/css/bootstrap.yag.css?_=" & Settings.Version))
  ClientResourceManager.RegisterStyleSheet(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/css/bootstrap-responsive.yag.css?_=" & Settings.Version))
  ClientResourceManager.RegisterStyleSheet(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/css/fileupload.css?_=" & Settings.Version))

  ClientResourceManager.RegisterScript(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/js/jquery.ui.widget.js?_=" & Settings.Version), 0)
  ClientResourceManager.RegisterScript(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/js/jquery.iframe-transport.js?_=" & Settings.Version), 2)
  ClientResourceManager.RegisterScript(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/js/jquery.fileupload.js?_=" & Settings.Version), 3)
  ClientResourceManager.RegisterScript(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/js/jquery.fileupload-fp.js?_=" & Settings.Version), 4)

  ClientResourceManager.RegisterScript(Me.Page, ResolveUrl("~/DesktopModules/Bring2mind/YAG/js/upload.js?_=" & Settings.Version), 40) ' last one loads our own stuff

  If Not Me.IsPostBack Then
   If IO.File.Exists(Settings.ImageMapPath & UserInfo.UserID.ToString & ".resources") Then IO.File.Delete(Settings.ImageMapPath & UserInfo.UserID.ToString & ".resources")
  End If

 End Sub

 Private Sub cmdReturn_Click(sender As Object, e As System.EventArgs) Handles cmdReturn.Click
  Me.Response.Redirect(EditUrl(), False)
 End Sub
End Class