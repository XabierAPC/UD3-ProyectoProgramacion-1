Imports System.IO
Imports System.Linq.Expressions
Imports WordleClases
Public Class Form1
    Dim numeroFilas As Integer = 6
    Dim numeroColumnas As Integer = 5
    Dim tamanoLabel As Integer = 50
    Dim tamanoMargen As Integer = 5
    Dim indiceLabelActual As Integer = 0
    Dim indiceMaxCeldasRellenadasPorFila As Integer

    Dim palabraFormando As String

    Dim wordle As Diccionario
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Dim directorioSolucion As String = Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)) ''busca donde esta el .sln
        Dim rutaCompletaDirectorioPadreSln As String = Directory.GetParent(directorioSolucion).FullName ''te busca la ruta absoluta de cada ordenador hasta la carpeta que contiene el .sln
        Dim rutaPalabrasLeer As String = Path.Combine(rutaCompletaDirectorioPadreSln, "PalabrasLeer") ''de la ruta abs ya obtenida se mueve a la carpeta PalabrasLeer
        Dim accesoFicherPalabras As String = Path.Combine(rutaPalabrasLeer, "Palabras.txt") ''de dicha ruta + carpeta accede al fichero Palabras.txt


        wordle = New Diccionario(accesoFicherPalabras)

        indiceLabelActual = Me.Controls.Count
        indiceMaxCeldasRellenadasPorFila = numeroColumnas + indiceLabelActual

        For i As Integer = 0 To numeroFilas - 1
            For j As Integer = 0 To numeroColumnas - 1
                Dim nuevoLabel As New Label()
                nuevoLabel.Width = tamanoLabel
                nuevoLabel.Height = tamanoLabel
                nuevoLabel.BorderStyle = BorderStyle.FixedSingle
                nuevoLabel.TextAlign = ContentAlignment.MiddleCenter

                nuevoLabel.Left = j * (tamanoLabel + tamanoMargen) + tamanoMargen
                nuevoLabel.Top = i * (tamanoLabel + tamanoMargen) + tamanoMargen

                Me.Controls.Add(nuevoLabel)
            Next
        Next



        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is Label Then
                Debug.WriteLine(i)
            End If
        Next



    End Sub



    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim palabrasPermitidas As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ"
        If indiceLabelActual < numeroFilas * numeroColumnas Then
            Dim currentLabel As Label = CType(Me.Controls(indiceLabelActual), Label)
            Debug.WriteLine($"Current Label: {indiceLabelActual}")

            If e.KeyChar = ChrW(Keys.Back) AndAlso indiceLabelActual >= indiceMaxCeldasRellenadasPorFila - numeroColumnas Then
                If palabraFormando.Length > 0 Then
                    palabraFormando = palabraFormando.Substring(0, palabraFormando.Length - 1)
                End If

                currentLabel = CType(Me.Controls(indiceLabelActual - 1), Label)
                currentLabel.Text = ""
                If indiceLabelActual <> indiceMaxCeldasRellenadasPorFila - numeroColumnas Then
                    indiceLabelActual -= 1

                End If



                Debug.WriteLine(palabraFormando)

            End If

            If e.KeyChar = ChrW(Keys.Enter) Then 'Si enter es pulsado mira si se ha completado la palabra o no
                If indiceLabelActual <> indiceMaxCeldasRellenadasPorFila Then
                    MsgBox("Lenght != col")
                Else
                    palabraFormando = palabraFormando.Substring(0, 5)

                    If wordle.palbraEsValida(palabraFormando) Then
                        indiceMaxCeldasRellenadasPorFila += 5
                        palabraFormando = ""
                    Else
                        MsgBox("La palabra no existe")
                    End If
                End If
                Return
            End If

            If palabrasPermitidas.Contains(e.KeyChar.ToString.ToUpper) Then ''si no se ha pulsado eneter, se busca si la tecla esta en abecedario, si es asi haz ....
                If indiceLabelActual <> indiceMaxCeldasRellenadasPorFila Then
                    indiceLabelActual += 1
                    currentLabel.Text = e.KeyChar.ToString()
                    palabraFormando += currentLabel.Text
                End If
            End If

            ''TODO al pressionar return eliminar palabras
            Debug.WriteLine($"End Label: {indiceLabelActual}")
            Debug.WriteLine(palabraFormando)
        End If
    End Sub
End Class