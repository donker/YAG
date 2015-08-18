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

Imports DotNetNuke.Services.Tokens

Imports System.Xml.Serialization

<Serializable()> _
Public Class Image
 Implements IPropertyAccess

 <XmlElement("order")> _
 Public Property Order As Integer = 0

 <XmlElement("file")> _
 Public Property File As String

 <XmlElement("extension")> _
 Public Property Extension As String

 <XmlElement("title")> _
 Public Property Title As String

 <XmlElement("remarks")> _
 Public Property Remarks As String

 Public Sub New()
 End Sub
 Public Sub New(filepath As String, title As String, remarks As String)
  Me.File = IO.Path.GetFileNameWithoutExtension(filepath)
  Me.Extension = IO.Path.GetExtension(filepath)
  Me.Title = title
  Me.Remarks = remarks
 End Sub

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
   Case "order"
    Return (Me.Order.ToString(OutputFormat, formatProvider))
   Case "file"
    Return PropertyAccess.FormatString(Me.File, strFormat)
   Case "extension"
    Return PropertyAccess.FormatString(Me.Extension, strFormat)
   Case "title"
    Return PropertyAccess.FormatString(Me.Title, strFormat)
   Case "remarks"
    Return PropertyAccess.FormatString(Me.Remarks, strFormat)
   Case Else
    Return ""
  End Select
 End Function
#End Region

End Class
