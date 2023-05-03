Public Class Palabra
    Public Property Texto As String
    Public Property NumeroLetras As Integer

    Public Sub New(textoPasado As String, numeroLetras As Integer)
        Me.Texto = textoPasado
        Me.NumeroLetras = numeroLetras
    End Sub

End Class
