Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Usuarios
    Private _users As List(Of Usuario)
    Private ReadOnly RutaFichero As String = Path.Combine(Path.Combine(Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath))).FullName, "PalabrasLeer"), "Usuarios.txt")

    Public Sub New()
        Me._users = New List(Of Usuario)
        Dim lines() As String = System.IO.File.ReadAllLines(RutaFichero)
        For Each line As String In lines
            Dim values() As String = line.Split(":")
            Me._users.Add(New Usuario(values(0), values(1)))
        Next
    End Sub

    Public Sub AnadirUsuario(ByVal username As String, ByVal password As String)
        Me._users.Add(New Usuario(username, password))
    End Sub

    Public Sub GuardarUsuarios(filename As String)
        Using writer As New System.IO.StreamWriter(filename)
            For Each user As Usuario In Me._users
                writer.WriteLine(user.Username & ":" & user.Password)
            Next
        End Using
    End Sub

    Public Function EsUsuarioValido(usuario As Usuario)
        For Each user In Me._users
            If user.Equals(usuario) Then
                Return False
            End If
        Next
        Return True
    End Function


End Class
