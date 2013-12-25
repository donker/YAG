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

Imports System.Xml.Serialization
Imports System.Linq

<Serializable()> _
Public Class ImageCollection

 <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("image")> _
 Public Property Images As New List(Of Image)

 Private _albumFile As String = ""
 Private _imagesMapPath As String = ""

 Public Sub New(imagesMapPath As String)
  MyBase.New()
  _albumFile = imagesMapPath & "album.xml"
  _imagesMapPath = imagesMapPath
  Dim x As New System.Xml.Serialization.XmlSerializer(GetType(ImageCollection))
  If IO.File.Exists(_albumFile) Then
   Using rdr As New IO.StreamReader(_albumFile)
    Dim a As ImageCollection = CType(x.Deserialize(rdr), ImageCollection)
    Me.Images = a.Images
   End Using
  End If
 End Sub
 Public Sub New()
 End Sub

 Public Sub Save()
  'WriteOrder()
  Dim x As New System.Xml.Serialization.XmlSerializer(GetType(ImageCollection))
  Using w As New IO.StreamWriter(_albumFile, False, System.Text.Encoding.UTF8)
   x.Serialize(w, Me)
  End Using
 End Sub

 Public Sub WriteOrder()
  Dim i As Integer = 0
  For Each img As Image In Images
   img.Order = i
   i += 1
  Next
 End Sub

 Public Sub Sort()
  Images.Sort(Function(x, y)
               Return x.Order.CompareTo(y.Order)
              End Function)
 End Sub

 Public Sub Delete(file As String)
  Try
   Dim i As Image = (From x In Images Select x Where x.File = file)(0)
   If IO.File.Exists(String.Format("{0}{1}{2}", _imagesMapPath, file, i.Extension)) Then IO.File.Delete(String.Format("{0}{1}{2}", _imagesMapPath, file, i.Extension))
   If IO.File.Exists(String.Format("{0}{1}_tn{2}", _imagesMapPath, file, i.Extension)) Then IO.File.Delete(String.Format("{0}{1}_tn{2}", _imagesMapPath, file, i.Extension))
   If IO.File.Exists(String.Format("{0}{1}_zoom{2}", _imagesMapPath, file, i.Extension)) Then IO.File.Delete(String.Format("{0}{1}_zoom{2}", _imagesMapPath, file, i.Extension))
   Images.Remove(i)
  Catch ex As Exception
  End Try
 End Sub

 Public Sub Recheck()
  Dim hasChanges As Boolean = False
  Dim registered As New List(Of String)
  Dim disappeared As New List(Of Image)
  For Each i As Image In Images
   If Not IO.File.Exists(_imagesMapPath & i.File & i.Extension) Then
    disappeared.Add(i)
   Else
    registered.Add(i.File)
   End If
  Next
  ' remove disappeared images from album
  For Each i As Image In disappeared
   hasChanges = True
   Images.Remove(i)
  Next
  ' pick up new images
  For Each f As String In IO.Directory.GetFiles(_imagesMapPath, "*.*")
   Dim m As Match = Regex.Match(f, "(?i)(\d{8}-\d{6,})\.(?-i)")
   If m.Success Then
    Dim fname As String = m.Groups(1).Value
    If Not registered.Contains(fname) Then
     Dim i As New Image With {.Extension = IO.Path.GetExtension(f), .File = fname, .Title = fname, .Order = Images.Count + 1}
     Images.Add(i)
     registered.Add(fname)
     hasChanges = True
    End If
   End If
  Next
  ' remove orphaned thumbnails/zooms
  For Each f As String In IO.Directory.GetFiles(_imagesMapPath, "*.*")
   Dim m As Match = Regex.Match(f, "(?i)([^_\\\.]+)(_tn|_zoom)\.(?-i)")
   If m.Success Then
    Dim fname As String = m.Groups(1).Value
    If Not registered.Contains(fname) Then
     Try
      IO.File.Delete(f)
     Catch ex As Exception
     End Try
    End If
   End If
  Next
  If hasChanges Then Save()
 End Sub

 Public Sub UpdateTitle(file As String, title As String)
  Try
   Dim i As Image = (From x In Images Select x Where x.File = file)(0)
   i.Title = title
  Catch ex As Exception
  End Try
 End Sub

 Public Sub UpdateRemarks(file As String, remarks As String)
  Try
   Dim i As Image = (From x In Images Select x Where x.File = file)(0)
   i.Remarks = remarks
  Catch ex As Exception
  End Try
 End Sub

End Class
