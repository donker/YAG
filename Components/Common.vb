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

Public Class Common

 Public Const glbTemplatesPath As String = "~/DesktopModules/Bring2mind/YAG/Templates/"
 Public Const glbSharedResourceFileName As String = "~/DesktopModules/Bring2mind/YAG/App_LocalResources/SharedResources.resx"

 Public Shared Function ReadFile(ByVal fileName As String) As String
  Return ReadFile(fileName, 10)
 End Function
 Public Shared Function ReadFile(ByVal fileName As String, retries As Integer) As String
  If Not IO.File.Exists(fileName) Then Return ""
  If retries = 0 Then Return ""
  Try
   Using sr As New IO.StreamReader(fileName)
    Return sr.ReadToEnd
   End Using
  Catch ioex As IO.IOException
   Threading.Thread.Sleep(200)
   Return ReadFile(fileName, retries - 1)
  Catch ex As Exception
   Return ""
  End Try
 End Function

 Public Shared Sub WriteTextToFile(filePath As String, textToWrite As String)
  WriteTextToFile(filePath, textToWrite, 10)
 End Sub
 Public Shared Sub WriteTextToFile(filePath As String, textToWrite As String, retries As Integer)
  If retries = 0 Then Exit Sub
  Try
   Using sw As New IO.StreamWriter(filePath)
    sw.Write(textToWrite)
    sw.Flush()
   End Using
  Catch ioex As IO.IOException
   Threading.Thread.Sleep(200)
   WriteTextToFile(filePath, textToWrite, retries - 1)
  Catch ex As Exception
  End Try
 End Sub

 Public Shared Function FormatBoolean(ByVal value As Boolean, ByVal format As String) As String
  If String.IsNullOrEmpty(format) Then
   Return value.ToString
  End If
  If format.Contains(";") Then
   If value Then
    Return Left(format, format.IndexOf(";"))
   Else
    Return Mid(format, format.IndexOf(";") + 2)
   End If
  End If
  Return value.ToString
 End Function

#Region " Reading Values "
 Public Shared Sub ReadValue(ByRef ValueTable As Hashtable, ByVal ValueName As String, ByRef Variable As Integer)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Integer)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As Hashtable, ByVal ValueName As String, ByRef Variable As Long)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Long)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As Hashtable, ByVal ValueName As String, ByRef Variable As String)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), String)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As Hashtable, ByVal ValueName As String, ByRef Variable As Boolean)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Boolean)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As Hashtable, ByVal ValueName As String, ByRef Variable As Date)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Date)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As Hashtable, ByVal ValueName As String, ByRef Variable As TimeSpan)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = TimeSpan.Parse(CType(ValueTable.Item(ValueName), String))
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As NameValueCollection, ByVal ValueName As String, ByRef Variable As Integer)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Integer)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As NameValueCollection, ByVal ValueName As String, ByRef Variable As Long)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Long)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As NameValueCollection, ByVal ValueName As String, ByRef Variable As String)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), String)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As NameValueCollection, ByVal ValueName As String, ByRef Variable As Boolean)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Boolean)
   Catch ex As Exception
    Select Case ValueTable.Item(ValueName).ToLower
     Case "on", "yes"
      Variable = True
     Case Else
      Variable = False
    End Select
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As NameValueCollection, ByVal ValueName As String, ByRef Variable As Date)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = CType(ValueTable.Item(ValueName), Date)
   Catch ex As Exception
   End Try
  End If
 End Sub

 Public Shared Sub ReadValue(ByRef ValueTable As NameValueCollection, ByVal ValueName As String, ByRef Variable As TimeSpan)
  If Not ValueTable.Item(ValueName) Is Nothing Then
   Try
    Variable = TimeSpan.Parse(CType(ValueTable.Item(ValueName), String))
   Catch ex As Exception
   End Try
  End If
 End Sub
#End Region

End Class
