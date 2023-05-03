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
End Class
