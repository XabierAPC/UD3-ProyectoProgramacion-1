Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WordleClases
Imports System.Windows.Forms


<TestClass()> Public Class UnitTest1
    'Dim directorioSolucion As String = Path.GetDirectoryName(Path.GetDirectoryName(Windows.Forms.Application.StartupPath)) ''busca donde esta el .sln
    'Dim rutaCompletaDirectorioPadreSln As String = Directory.GetParent(directorioSolucion).FullName ''te busca la ruta absoluta de cada ordenador hasta la carpeta que contiene el .sln
    'Dim rutaPalabrasLeer As String = Path.Combine(rutaCompletaDirectorioPadreSln, "PalabrasLeer") ''de la ruta abs ya obtenida se mueve a la carpeta PalabrasLeer
    'Dim accesoFicherPalabras As String = Path.Combine(rutaPalabrasLeer, "Palabras.txt") ''de dicha ruta + carpeta accede al fichero Palabras.txt
    'Dim wordle As New Diccionario(accesoFicherPalabras)

    <TestMethod()> Public Sub TestMethod1()
        'wordle.GetRandomWord(5)
        Dim result = 1
        Assert.AreEqual(1, result)
    End Sub

End Class