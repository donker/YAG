
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports DotNetNuke.Web.Api

Imports System.IO
Imports System.Web.Script.Serialization

Public Class GalleryController
 Inherits DnnApiController
 Implements IServiceRouteMapper

#Region " Properties "
 Private ReadOnly Property Settings As GallerySettings
  Get
   Return GallerySettings.GetGallerySettings(ActiveModule.ModuleID)
  End Get
 End Property
#End Region

#Region " IServiceRouteMapper "
 Public Sub RegisterRoutes(mapRouteManager As DotNetNuke.Web.Api.IMapRoute) Implements DotNetNuke.Web.Api.IServiceRouteMapper.RegisterRoutes
  mapRouteManager.MapHttpRoute("Bring2mind/YAG", "Default", "", New With {.Controller = "Gallery", .Action = "ListCurrentFiles"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapHttpRoute("Bring2mind/YAG", "Upload", "Upload", New With {.Controller = "Gallery", .Action = "UploadFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapHttpRoute("Bring2mind/YAG", "Delete", "Delete", New With {.Controller = "Gallery", .Action = "DeleteFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapHttpRoute("Bring2mind/YAG", "Commit", "Commit", New With {.Controller = "Gallery", .Action = "CommitFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapHttpRoute("Bring2mind/YAG", "Edit", "Edit", New With {.Controller = "Gallery", .Action = "EditFile"}, New String() {"Bring2mind.DNN.Modules.YAG"})
  mapRouteManager.MapHttpRoute("Bring2mind/YAG", "Reorder", "Reorder", New With {.Controller = "Gallery", .Action = "Reorder"}, New String() {"Bring2mind.DNN.Modules.YAG"})
 End Sub
#End Region

#Region " Other Service Methods "
 <HttpGet()>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
 Public Function ListCurrentFiles() As HttpResponseMessage
  Dim res As New List(Of Image)
  If ActiveModule IsNot Nothing Then
   Dim album As New ImageCollection(Settings.ImageMapPath)
   If album IsNot Nothing Then
    res = album.Images
   End If
  End If
  Return Request.CreateResponse(HttpStatusCode.OK, res)
 End Function

 Public Class fileDTO
  Public Property fileName As String
 End Class

 <HttpPost()>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 <ValidateAntiForgeryToken()>
 Public Function DeleteFile(postData As fileDTO) As HttpResponseMessage
  Dim fileName As String = postData.fileName
  Dim res As Boolean = True
  Dim localFile As String = GetUploadedFileName(Settings.ImageMapPath, fileName)
  If localFile <> "" Then
   Dim fullName As String = Settings.ImageMapPath & localFile & Path.GetExtension(fileName)
   If IO.File.Exists(fullName) Then
    Try
     IO.File.Delete(fullName)
     IO.File.Delete(Settings.ImageMapPath & localFile & ".resources")
     IO.File.Delete(Settings.ImageMapPath & localFile & "_tn" & Path.GetExtension(fileName))
     IO.File.Delete(Settings.ImageMapPath & localFile & "_zoom" & Path.GetExtension(fileName))
    Catch ex As Exception
     res = False
    End Try
   End If
  Else
   Dim album As New ImageCollection(Settings.ImageMapPath)
   album.Delete(fileName)
   album.Save()
  End If
  Return Request.CreateResponse(HttpStatusCode.OK, res)
 End Function

 <HttpPost()>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 <ValidateAntiForgeryToken()>
 Public Function CommitFile(postData As fileDTO) As HttpResponseMessage
  Dim fileName As String = postData.fileName
  Dim res As String = ""
  Dim localFile As String = GetUploadedFileName(Settings.ImageMapPath, fileName)
  If localFile <> "" Then
   Dim ext As String = IO.Path.GetExtension(fileName)
   Dim fullName As String = Settings.ImageMapPath & localFile & ext
   If IO.File.Exists(fullName) Then
    Dim extOK As Boolean = False
    Select Case ext.ToLower
     Case ".jpg", ".jpeg", ".png", ".gif"
      extOK = True
    End Select
    If Not extOK Then Throw New Exception("Must upload an image")
    Dim r As New Resizer(Settings)
    r.Process(fullName)
    res = Settings.ImagePath & localFile & "_tn" & ext
   End If
  End If
  Return Request.CreateResponse(HttpStatusCode.OK, res)
 End Function

 Public Class editFileDTO
  Public Property control As String
  Public Property value As String
 End Class

 <HttpPost()>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 <ValidateAntiForgeryToken()>
 Public Function EditFile(postData As editFileDTO) As HttpResponseMessage
  Dim control As String = postData.control
  Dim value As String = postData.value
  Dim res As Boolean = True
  Dim album As New ImageCollection(Settings.ImageMapPath)
  If control.StartsWith("title") Then
   album.UpdateTitle(control.Substring(6), value)
   album.Save()
  ElseIf control.StartsWith("url") Then
   album.UpdateUrl(control.Substring(4), value)
   album.Save()
  ElseIf control.StartsWith("remarks") Then
   album.UpdateRemarks(control.Substring(8), value)
   album.Save()
  Else
   res = False
  End If
  Return Request.CreateResponse(HttpStatusCode.OK, res)
 End Function

 Public Class orderDTO
  Public Property order As String
 End Class

 <HttpPost()>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 <ValidateAntiForgeryToken()>
 Public Function Reorder(postData As orderDTO) As HttpResponseMessage
  Dim res As Boolean = True
  Dim order As String() = postData.order.Replace("T", "-").Split("&"c)
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
  Return Request.CreateResponse(HttpStatusCode.OK, res)
 End Function
#End Region

#Region " Upload Handler "
 Private ReadOnly js As New JavaScriptSerializer()

 <HttpPost()>
 <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
 <ValidateAntiForgeryToken()>
 Public Function UploadFile() As HttpResponseMessage
  Dim res As New HttpResponseMessage(HttpStatusCode.OK)
  Dim statuses As New List(Of FilesStatus)
  HandleUploadFile(System.Web.HttpContext.Current, statuses)
  System.Web.HttpContext.Current.Response.ContentType = "text/plain"
  res.Content = New StringContent(WriteJsonIframeSafe(System.Web.HttpContext.Current, statuses))
  Return res
 End Function

 Private Function WriteJsonIframeSafe(context As HttpContext, statuses As List(Of FilesStatus)) As String
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
  Return js.Serialize(statuses.ToArray())
 End Function

 ' Upload file to the server
 Private Sub HandleUploadFile(context As HttpContext, ByRef statuses As List(Of FilesStatus))
  Dim headers As NameValueCollection = context.Request.Headers
  If String.IsNullOrEmpty(headers("Content-Range")) Then
   UploadWholeFiles(context, statuses)
  Else
   Dim cdh As String = headers("Content-Disposition")
   Dim m As Match = Regex.Match(cdh, "filename=""([^""]+)""")
   If m.Success Then
    UploadPartialFile(m.Groups(1).Value, context, statuses)
   End If
  End If
 End Sub

 ' Upload partial file
 Private Sub UploadPartialFile(fileName As String, context As HttpContext, ByRef statuses As List(Of FilesStatus))
  fileName = HttpUtility.UrlDecode(Path.GetFileName(fileName))
  Dim extension As String = Path.GetExtension(fileName)
  Dim fileToWriteTo As String = ""
  Dim localFile As String = GetUploadedFileName(Settings.ImageMapPath, fileName)
  If localFile = "" Then
   fileToWriteTo = GetNewFilekey(extension)
   Common.WriteTextToFile(String.Format("{0}{1}.resources", Settings.ImageMapPath, fileToWriteTo), fileName)
  Else
   fileToWriteTo = localFile
  End If
  Dim fullName As String = String.Format("{0}{1}{2}", Settings.ImageMapPath, fileToWriteTo, Path.GetExtension(fileName))
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
  statuses.Add(New FilesStatus(Settings.ImagePath, fileToWriteTo, extension, CInt(f.Length)))
 End Sub

 Private Sub UploadWholeFiles(context As HttpContext, ByRef statuses As List(Of FilesStatus))
  For i As Integer = 0 To context.Request.Files.Count - 1
   UploadWholeFile(context.Request.Files(i), statuses, 10)
  Next
 End Sub

 ' Upload entire file
 Private Sub UploadWholeFile(file As HttpPostedFile, ByRef statuses As List(Of FilesStatus), retries As Integer)
  If retries = 0 Then Exit Sub
  Dim extension As String = Path.GetExtension(file.FileName)
  Dim newFile As String = GetNewFilekey(extension)
  Dim fileName As String = Path.GetFileName(file.FileName)
  Dim fullName As String = Settings.ImageMapPath & newFile & extension
  Try
   file.SaveAs(fullName)
   Common.WriteTextToFile(Settings.ImageMapPath & newFile & ".resources", HttpUtility.UrlDecode(file.FileName))
  Catch ioex As IOException
   Threading.Thread.Sleep(500)
   UploadWholeFile(file, statuses, retries - 1)
  Catch ex As Exception
   '
  End Try
  statuses.Add(New FilesStatus(Settings.ImagePath, newFile, extension, file.ContentLength))
 End Sub

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
#End Region

#Region " Private Methods "
 Private Function GetUploadedFileName(folder As String, originalFilename As String) As String
  For Each f As String In IO.Directory.GetFiles(folder, "*.resources")
   If Common.ReadFile(f) = originalFilename Then
    Return IO.Path.GetFileNameWithoutExtension(f)
   End If
  Next
  Return ""
 End Function
#End Region

End Class
