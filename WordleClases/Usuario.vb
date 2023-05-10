Public Class Usuario
    Implements IEquatable(Of Usuario)

    Private _username As String
    Private _password As String

    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(value As String)
            _username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Property PartidasJugadas() As Integer
    Public Property PartidasGanadas() As Integer
    Public Property RachaActual() As Integer
    Public Property MejorRacha() As Integer
    Public Sub New(username As String, password As String, partidasJugadas As Integer, partidasGanadas As Integer, rachaActual As Integer, mejorRacha As Integer)
        Me.Username = username
        Me.Password = password
        Me.PartidasGanadas = partidasGanadas
        Me.PartidasJugadas = partidasJugadas
        Me.RachaActual = rachaActual
        Me.MejorRacha = mejorRacha
    End Sub

    Public Sub New(username As String, password As String)
        Me.Username = username
        Me.Password = password
    End Sub

    Public Function IsValidUser(username As String, password As String) As Boolean
        Return (Me.Username = username AndAlso Me.Password = password)
    End Function

    Public Overloads Function Equals(other As Usuario) As Boolean Implements IEquatable(Of Usuario).Equals
        Return other IsNot Nothing AndAlso
               _username.ToUpper() = other._username.ToUpper()
    End Function

    Public Sub PartidaFinalizada(esGanador As Boolean)
        PartidasJugadas += 1

        If esGanador Then
            PartidasGanadas += 1
            RachaActual += 1
        Else
            RachaActual = 0
        End If


        If MejorRacha < RachaActual Then
            MejorRacha = RachaActual
        End If

        MsgBox("Partida finalizada")
    End Sub
End Class
