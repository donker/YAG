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

Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Resizer

 Private Property Settings As GallerySettings

 Public Sub New(settings As GallerySettings)
  Me.Settings = settings
 End Sub

 Public Sub Process(originalFile As String)

  ' Load Image
  Dim thisImage As New Bitmap(originalFile)
  Dim imgFormat As ImageFormat = thisImage.RawFormat
  Dim originalWidth As Integer = thisImage.Width
  Dim originalHeight As Integer = thisImage.Height
  Dim imgRatio As Single = Convert.ToSingle(originalHeight / originalWidth)

  Dim ext As String = IO.Path.GetExtension(originalFile)
  Dim saveFilename As String = Left(originalFile, originalFile.Length - ext.Length)

  ' Resize Image
  ResizeImage(thisImage, imgFormat, originalWidth, originalHeight, imgRatio, Settings.Width, Settings.Height, Settings.FitType, saveFilename & "_tn" & ext)
  ResizeImage(thisImage, imgFormat, originalWidth, originalHeight, imgRatio, Settings.ZoomWidth, Settings.ZoomHeight, Settings.FitType, saveFilename & "_zoom" & ext)

 End Sub

 Private Sub ResizeImage(thisImage As Bitmap, imgFormat As ImageFormat, originalWidth As Integer, originalHeight As Integer, imgRatio As Single, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer, FitType As String, SaveAs As String)

  Dim newHeight As Integer = MaxHeight
  Dim newWidth As Integer = MaxWidth
  Dim scaleW As Double = MaxWidth / originalWidth
  Dim scaleH As Double = MaxHeight / originalHeight
  Dim scaleX As Double = scaleW
  Dim scaleY As Double = scaleH
  Dim newX As Integer = 0
  Dim newY As Integer = 0

  Select Case FitType
   Case "Shrink"
    scaleX = Math.Min(scaleW, scaleH)
    scaleY = scaleX
    newHeight = Convert.ToInt32(originalHeight * scaleX)
    newWidth = Convert.ToInt32(originalWidth * scaleY)
   Case "Crop"
    scaleX = Math.Max(scaleW, scaleH)
    scaleY = scaleX
    If scaleW > scaleH Then
     newY = -1 * Convert.ToInt32(((scaleX * originalHeight) - MaxHeight) / 2)
    Else
     newX = -1 * Convert.ToInt32(((scaleX * originalWidth) - MaxWidth) / 2)
    End If
   Case Else
    ' Stretch
  End Select

  Using backBuffer As Bitmap = New Bitmap(newWidth, newHeight, Drawing.Imaging.PixelFormat.Format24bppRgb)
   Using backBufferGraphics As Graphics = Graphics.FromImage(backBuffer)
    backBufferGraphics.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
    backBufferGraphics.SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
    backBufferGraphics.DrawImage(thisImage, newX, newY, Convert.ToInt32(originalWidth * scaleX), Convert.ToInt32(originalHeight * scaleY))
    backBuffer.Save(SaveAs, imgFormat)
   End Using
  End Using

 End Sub
End Class
