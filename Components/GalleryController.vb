Imports System.Web.Mvc
Imports DotNetNuke.Web.Services
Imports DotNetNuke.Security.Permissions
Imports System.Globalization
Imports System.Runtime.Serialization.Json
Imports System.IO
Imports System.Web.Script.Serialization

Public Class GalleryController
 Inherits DnnController
 Implements IServiceRouteMapper

 Private _uploadList As Dictionary(Of String, String)
 Public ReadOnly Property UploadList() As Dictionary(Of String, String)
  Get
   If _uploadList Is Nothing Then
    _uploadList = New Dictionary(Of String, String)
    Dim listFile As String = Settings.ImageMapPath & UserInfo.UserID.ToString & ".resources"
    If IO.File.Exists(listFile) Then
     Using ins As New IO.StreamReader(listFile)
      Dim line As String = ins.ReadLine
      While Not String.IsNullOrEmpty(line)
       _uploadList(line.Substring(0, line.IndexOf(";"c))) = line.Substring(line.IndexOf(";"c) + 1)
       line = ins.ReadLine
      End While
     End Using
    End If
   End If
   Return _uploadList
  End Get
 End Property
 Private Sub SaveUploadList()
  If _uploadList Is Nothing Then Exit Sub
  Dim listFile As String = Settings.ImageMapPath & UserInfo.UserID.ToString & ".resources"
  Using ins As New IO.StreamWriter(listFile, False)
   For Each key As String In _uploadList.Keys
    ins.WriteLine(String.Format("{0};{1}", key, _uploadList(key)))
   Next
  End Using
 End Sub

 Public Sub RegisterRoutes(mapRouteManager As DotNetNuke.Web.Services.IMapRoute) Implements DotNetNuke.Web.Services.IServiceRouteMapper.RegisterRoutes
  mapRouteManager.MapRoute("Bring2mind/YAG", "", New With {.Controller = "Gallery", .Action = "ListCurrentFiles"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapRoute("Bring2mind/YAG", "Upload", New With {.Controller = "Gallery", .Action = "UploadFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapRoute("Bring2mind/YAG", "Delete", New With {.Controller = "Gallery", .Action = "DeleteFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapRoute("Bring2mind/YAG", "Commit", New With {.Controller = "Gallery", .Action = "CommitFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapRoute("Bring2mind/YAG", "Edit", New With {.Controller = "Gallery", .Action = "EditFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapRoute("Bring2mind/YAG", "Reorder", New With {.Controller = "Gallery", .Action = "Reorder"}, New String() {"Bring2mind.DNN.Modules.YAG"})
 End Sub

 <AcceptVerbs(HttpVerbs.Get)>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
 Public Function ListCurrentFiles() As ActionResult
  Dim res As New List(Of Image)
  If ActiveModule IsNot Nothing Then
   Dim album As New ImageCollection(Settings.ImageMapPath)
   If album IsNot Nothing Then
    res = album.Images
   End If
  End If
  Return Json(res, JsonRequestBehavior.AllowGet)
 End Function

 Private ReadOnly js As New JavaScriptSerializer()

 <ValidateInput(False)>
 <AcceptVerbs(HttpVerbs.Post)>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 Public Function UploadFile() As ActionResult
  Dim statuses As New List(Of FilesStatus)
  HandleUploadFile(System.Web.HttpContext.Current, statuses)
  System.Web.HttpContext.Current.Response.ContentType = "text/plain"
  WriteJsonIframeSafe(System.Web.HttpContext.Current, statuses)
  'Return Json(statuses, JsonRequestBehavior.DenyGet)
  Return Nothing
 End Function
 Private Sub WriteJsonIframeSafe(context As HttpContext, statuses As List(Of FilesStatus))
  context.Response.AddHeader("Vary", "Accept")
  Try
   If context.Request("HTTP_ACCEPT").Contains("application/json") Then
    context.Response.ContentType = "application/json"
   Else
    context.Response.ContentType = "text/plain"
   End If
  Catch
   context.Response.ContentType = "text/plain"
  End Try
  Dim jsonObj As String = js.Serialize(statuses.ToArray())
  context.Response.Write(jsonObj)
 End Sub

 <ValidateInput(False)>
 <AcceptVerbs(HttpVerbs.Post)>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 Public Function DeleteFile() As ActionResult
  Dim fileName As String = System.Web.HttpContext.Current.Request.Params("fileName")
  Dim res As Boolean = True
  If UploadList.ContainsKey(fileName) Then
   Dim fullName As String = Settings.ImageMapPath & UploadList(fileName) & Path.GetExtension(fileName)
   If IO.File.Exists(fullName) Then
    Try
     IO.File.Delete(fullName)
     IO.File.Delete(Settings.ImageMapPath & UploadList(fileName) & "_tn" & Path.GetExtension(fileName))
     IO.File.Delete(Settings.ImageMapPath & UploadList(fileName) & "_zoom" & Path.GetExtension(fileName))
    Catch ex As Exception
     res = False
    End Try
   End If
  Else
   Dim album As New ImageCollection(Settings.ImageMapPath)
   album.Delete(fileName)
   album.Save()
  End If
  Return Json(res, JsonRequestBehavior.DenyGet)
 End Function

 <ValidateInput(False)>
 <AcceptVerbs(HttpVerbs.Post)>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 Public Function CommitFile() As ActionResult
  Dim fileName As String = System.Web.HttpContext.Current.Request.Params("fileName")
  Dim res As String = ""
  If UploadList.ContainsKey(fileName) Then
   Dim newFile As String = UploadList(fileName)
   Dim ext As String = IO.Path.GetExtension(fileName)
   Dim fullName As String = Settings.ImageMapPath & newFile & ext
   If IO.File.Exists(fullName) Then
    Dim extOK As Boolean = False
    Select Case ext.ToLower
     Case ".jpg", ".png", ".gif"
      extOK = True
    End Select
    If Not extOK Then Throw New Exception("Must upload an image")
    Dim r As New Resizer(Settings)
    r.Process(fullName)
    res = Settings.ImagePath & newFile & "_tn" & ext
   End If
  End If
  Return Json(res, JsonRequestBehavior.DenyGet)
 End Function

 <ValidateInput(False)>
 <AcceptVerbs(HttpVerbs.Post)>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 Public Function EditFile() As ActionResult
  Dim control As String = System.Web.HttpContext.Current.Request.Params("control")
  Dim value As String = System.Web.HttpContext.Current.Request.Params("value")
  Dim res As Boolean = True
  Dim album As New ImageCollection(Settings.ImageMapPath)
  If control.StartsWith("title") Then
   album.UpdateTitle(control.Substring(6), value)
   album.Save()
  ElseIf control.StartsWith("remarks") Then
   album.UpdateRemarks(control.Substring(8), value)
   album.Save()
  Else
   res = False
  End If
  Return Json(res, JsonRequestBehavior.DenyGet)
 End Function

 <AcceptVerbs(HttpVerbs.Post)>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 Public Function Reorder() As ActionResult
  Dim res As Boolean = True
  Dim order As String() = System.Web.HttpContext.Current.Request.Params("order").Replace("T", "-").Split("&"c)
  Dim index As Integer = 0
  Dim newIndexes As New Dictionary(Of String, Integer)
  For Each ord As String In order
   newIndexes.Add(ord.Substring(ord.IndexOf("=") + 1), index)
   index += 1
  Next
  Dim album As New ImageCollection(Settings.ImageMapPath)
  For Each i As Image In album.Images
   If newIndexes.ContainsKey(i.File) Then
    i.Order = newIndexes(i.File)
   End If
  Next
  album.Sort()
  album.Save()
  Return Json(res, JsonRequestBehavior.DenyGet)
 End Function

 ' Upload file to the server
 Private Sub HandleUploadFile(context As HttpContext, ByRef statuses As List(Of FilesStatus))
  Dim headers As NameValueCollection = context.Request.Headers
  If String.IsNullOrEmpty(headers("X-File-Name")) Then
   UploadWholeFile(context, statuses)
  Else
   UploadPartialFile(headers("X-File-Name"), context, statuses)
  End If
 End Sub

 ' Upload partial file
 Private Sub UploadPartialFile(fileName As String, context As HttpContext, ByRef statuses As List(Of FilesStatus))
  fileName = Path.GetFileName(fileName)
  'If (New Random(Now.Second)).NextDouble > 0.7 Then
  ' Throw New HttpRequestValidationException("Random error")
  'End If
  'If context.Request.Files.Count <> 1 Then
  ' Throw New HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request")
  'End If
  Dim extension As String = Path.GetExtension(fileName)
  Dim newFile As String = ""
  If Not UploadList.ContainsKey(fileName) Then
   newFile = GetNewFilekey(extension)
   UploadList(fileName) = newFile
   SaveUploadList()
  Else
   newFile = UploadList(fileName)
  End If
  Dim fullName As String = String.Format("{0}{1}{2}", Settings.ImageMapPath, newFile, Path.GetExtension(fileName))
  Using inputStream As Stream = context.Request.Files(0).InputStream
   Using fs As New FileStream(fullName, FileMode.Append, FileAccess.Write)
    Dim buffer(1023) As Byte
    Dim l As Integer = inputStream.Read(buffer, 0, 1024)
    While l > 0
     fs.Write(buffer, 0, l)
     l = inputStream.Read(buffer, 0, 1024)
    End While
    fs.Flush()
    fs.Close()
   End Using
  End Using
  Dim f As New FileInfo(fullName)
  statuses.Add(New FilesStatus(Settings.ImagePath, newFile, extension, CInt(f.Length)))
 End Sub

 ' Upload entire file
 Private Sub UploadWholeFile(context As HttpContext, statuses As List(Of FilesStatus))
  For i As Integer = 0 To context.Request.Files.Count - 1
   Dim file As HttpPostedFile = context.Request.Files(i)
   Dim extension As String = Path.GetExtension(file.FileName)
   Dim newFile As String = GetNewFilekey(extension)
   Dim fileName As String = Path.GetFileName(file.FileName)
   UploadList(fileName) = newFile
   Dim fullName As String = Settings.ImageMapPath & newFile & extension
   file.SaveAs(fullName)
   statuses.Add(New FilesStatus(Settings.ImagePath, newFile, extension, file.ContentLength))
  Next
  SaveUploadList()
 End Sub

 Private ReadOnly Property Settings As GallerySettings
  Get
   Return GallerySettings.GetGallerySettings(ActiveModule.ModuleID)
  End Get
 End Property

 Private Function GetNewFilekey(extension As String) As String
  Dim res As String = String.Format("{0:yyyyMMdd}-{0:HHmmss}", Now)
  If IO.File.Exists(Settings.ImageMapPath & res & extension) Then
   Dim i As Integer = 0
   Do While IO.File.Exists(Settings.ImageMapPath & res & i.ToString & extension)
    i += 1
   Loop
   res &= i.ToString
  End If
  Return res
 End Function

End Class
