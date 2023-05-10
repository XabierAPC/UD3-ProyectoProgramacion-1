Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices
Public Enum TipoError
    UsuarioVacio
    ContrasenaVacia
    RepetirContrasenaVacia
    AmbosVacios
    UsuarioLongitudIncorrecta
    ContrasenaLongitudIncorrecta
    AmbasLongitudesIncorrectas
    UsuarioYaExiste
    ContrasenaNoCoincide

    UsuarioNoExiste
    ContrasenaIncorrecta
End Enum
Public Class Usuarios
    Private _users As List(Of Usuario)
    Private ReadOnly RutaFichero As String = Path.Combine(Path.Combine(Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath))).FullName, "PalabrasLeer"), "Usuarios.txt")

    Public Sub New()
        Me._users = New List(Of Usuario)
        Dim lines() As String = File.ReadAllLines(RutaFichero)
        For Each line As String In lines
            Dim values() As String = line.Split(":")
            Me._users.Add(New Usuario(values(0), values(1), values(2), values(3), values(4), values(5)))
        Next
    End Sub

    Public Function AnadirUsuario(username As String, password As String, repeatPassword As String) As TipoError
        Dim us As New Usuario(username, password)
        If username = "" AndAlso password = "" Then
            Return TipoError.AmbosVacios
        ElseIf username = "" Then
            Return TipoError.UsuarioVacio
        ElseIf password = "" Then
            Return TipoError.ContrasenaVacia
        End If

        If username.Length < 4 AndAlso password.Length < 4 Then
            Return TipoError.AmbasLongitudesIncorrectas
        ElseIf username.Length < 4 Then
            Return TipoError.UsuarioLongitudIncorrecta
        ElseIf password.Length < 6 Then
            Return TipoError.ContrasenaLongitudIncorrecta
        End If

        If Not EsUsuarioValido(us) Then
            Return TipoError.UsuarioYaExiste
        End If

        If password <> repeatPassword Then
            Return TipoError.ContrasenaNoCoincide
        End If

        Me._users.Add(New Usuario(username, password))

        Return Nothing
    End Function

    Public Function ValidarUsuario(usernanme As String, password As String) As TipoError
        If String.IsNullOrEmpty(usernanme) AndAlso String.IsNullOrEmpty(password) Then
            Return TipoError.AmbosVacios
        ElseIf String.IsNullOrEmpty(usernanme) Then
            Return TipoError.UsuarioVacio
        ElseIf String.IsNullOrEmpty(password) Then
            Return TipoError.ContrasenaVacia
        End If

        Dim user As Usuario
        user = BuscarUsuario(usernanme)


        If user Is Nothing Then
            Return TipoError.UsuarioNoExiste
        End If

        If user.Password <> password Then
            Return TipoError.ContrasenaIncorrecta
        End If

        Dim userReal As New Usuario(usernanme, password)
        Globales.Instanciadicionario = New Diccionario(userReal)
        Globales.User = userReal
        Return Nothing
    End Function

    Public Sub GuardarUsuarios()
        Using writer As New System.IO.StreamWriter(RutaFichero)
            For Each user As Usuario In Me._users
                writer.WriteLine(user.Username & ":" & user.Password & ":" & user.RachaActual & ":" & user.MejorRacha & ":" & user.PartidasGanadas & ":" & user.PartidasJugadas)
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

    Public Function BuscarUsuario(usuario As String) As Usuario
        For Each User In Me._users
            If User.Username.ToUpper = usuario.ToUpper Then
                Return User
            End If
        Next
        Return Nothing
    End Function

    Public Function GetRanking() As List(Of Usuario)
        Dim ranking As New List(Of Usuario)
        For Each user In Me._users

        Next
        Return ranking
    End Function

    Public Sub AgregarPuntuacion(userName As String, b As Boolean)
        Dim user As Usuario = BuscarUsuario(userName)
        user.PartidaFinalizada(b)
    End Sub
End Class
