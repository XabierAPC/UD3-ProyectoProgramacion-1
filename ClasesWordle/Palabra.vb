Public Class Palabra
    Implements IEquatable(Of Palabra)

    Public Property Termino As String
    Public Property Definicion As String()

    Public Overloads Function Equals(other As Palabra) As Boolean Implements IEquatable(Of Palabra).Equals
        Return other IsNot Nothing AndAlso
               Me.Termino.Equals(other.Termino, StringComparison.InvariantCultureIgnoreCase)
    End Function

    Public Sub New(Termino As String)
        Me.Termino = Termino
    End Sub

    Public Sub New(Termino As String, Definciones As String())
        Me.Termino = Termino
        Me.Definicion = Definciones
    End Sub
End Class
