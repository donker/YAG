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

Imports System.Xml.Serialization

<Serializable()> _
Public Class ImageCollection

 <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("image")> _
 Public Property Images As New List(Of Image)

 Private _albumFile As String = ""
 Public Sub New(imagesMapPath As String)
  MyBase.New()
  _albumFile = imagesMapPath & "album.xml"
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
  WriteOrder()
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

End Class
