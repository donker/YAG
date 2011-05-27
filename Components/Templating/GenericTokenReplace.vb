﻿'
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

Imports DotNetNuke.Services.Tokens

Namespace Templating
 Public Class GenericTokenReplace
  Inherits TokenReplace

  Public Sub New(ByVal AccessLevel As DotNetNuke.Services.Tokens.Scope)
   MyBase.New(Scope.DefaultSettings)
  End Sub

  Public Shadows Function ReplaceTokens(ByVal strSourceText As String) As String
   strSourceText = strSourceText.Replace("\[", "{{").Replace("\]", "}}")
   Return MyBase.ReplaceTokens(strSourceText).Replace("{{", "[").Replace("}}", "]")
  End Function

  Public Shadows Function ReplaceTokens(ByVal strSourceText As String, ByVal ParamArray additionalParameters As String()) As String
   strSourceText = strSourceText.Replace("\[", "{{").Replace("\]", "}}")
   Me.PropertySource("custom") = New CustomParameters(additionalParameters)
   Return MyBase.ReplaceTokens(strSourceText).Replace("{{", "[").Replace("}}", "]")
  End Function

  Public Sub AddCustomParameters(ByVal ParamArray additionalParameters As String())
   Me.PropertySource("custom") = New CustomParameters(additionalParameters)
  End Sub

  Public Sub AddResources(ByVal TemplateFileMapPath As String)
   Me.PropertySource("resx") = New Resources(TemplateFileMapPath)
  End Sub

  Public Sub AddPropertySource(key As String, resource As IPropertyAccess)
   Me.PropertySource(key) = resource
  End Sub

 End Class
End Namespace
