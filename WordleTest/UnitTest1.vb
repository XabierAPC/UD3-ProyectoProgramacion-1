Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WordleClases
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices
Imports WordleClases.Diccionario

<TestClass()> Public Class UnitTest1


    <TestMethod()> Public Sub TestMethod1()
        Dim user As New Usuario("pepe", "pepe")
        Dim v As New Usuarios()

        Dim result As User = Nothing
        Assert.AreNotEqual(v.BuscarUsuario(user.Username), result)
    End Sub

    <TestMethod()> Public Sub TestMethod2()
        Dim u As New Usuario("test", "test")
        Dim word As New Diccionario(u)
        word.GetRandomWord()
        Dim pal As String = word._palabraGenerada
        Debug.WriteLine(pal)
        Dim real() As TipoAcierto = word.GreenYellowGray(pal)
        Dim expected() As TipoAcierto = {TipoAcierto.Acertado, TipoAcierto.Acertado, TipoAcierto.Acertado, TipoAcierto.Acertado, TipoAcierto.Acertado, TipoAcierto.Acertado}
        Dim realEsValido = True
        For Each item In real
            If item <> TipoAcierto.Acertado Then
                realEsValido = False
            End If
        Next

        Dim expectedEsValido = True

        Assert.IsTrue(realEsValido = expectedEsValido)



    End Sub

End Class