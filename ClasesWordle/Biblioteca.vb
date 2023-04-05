Public Class Biblioteca
    Private arrayPalabras As Palabra()

    Private palabraGenerada As Palabra
    'Property -> almacenar palabra seleccionada

    'Constructor -> aqui metemos las palbras al array (En un futuro sera desde un documento)

    'Metodo para hacer un return de una plabra aleatoria del array

    'Metodo para comparar un String pasado a la palabra seleccionada
    Public Function indicesValidosPalabra(palbraIntroducidaUsuario As String) As List(Of Byte)
        Dim listaResultados As New List(Of Byte)

        For Each charPalabraUsuario As Char In palbraIntroducidaUsuario
            Dim index As Integer = palbraIntroducidaUsuario.IndexOf(charPalabraUsuario)
            If index = -1 Then
                If charPalabraUsuario = palabraGenerada.Termino(0) Then
                    listaResultados.Add(1)
                Else
                    listaResultados.Add(0)
                End If
            Else
                listaResultados.Add(255)
            End If
        Next
    End Function



End Class
