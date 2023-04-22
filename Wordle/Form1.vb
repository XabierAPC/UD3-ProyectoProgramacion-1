﻿Imports System.IO
Imports WordleClases
Public Class Form1
    Dim numeroFilas As Integer = 6
    Dim numeroColumnas As Integer = 5
    Dim tamanoLabel As Integer = 50
    Dim tamanoMargen As Integer = 5

    Dim indiceLabelActual As Integer
    Dim indiceMaximoCeldas As Integer
    Dim indiceMinimoCeldas As Integer
    Dim palabraFormando As String

    Private WithEvents _keyboardListener As New KeyboardListener()

    Dim wordle As Diccionario
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim directorioSolucion As String = Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)) ''busca donde esta el .sln
        Dim rutaCompletaDirectorioPadreSln As String = Directory.GetParent(directorioSolucion).FullName ''te busca la ruta absoluta de cada ordenador hasta la carpeta que contiene el .sln
        Dim rutaPalabrasLeer As String = Path.Combine(rutaCompletaDirectorioPadreSln, "PalabrasLeer") ''de la ruta abs ya obtenida se mueve a la carpeta PalabrasLeer
        Dim accesoFicherPalabras As String = Path.Combine(rutaPalabrasLeer, "Palabras.txt") ''de dicha ruta + carpeta accede al fichero Palabras.txt


        wordle = New Diccionario(accesoFicherPalabras)

        indiceLabelActual = Me.Controls.Count
        indiceMaximoCeldas = numeroColumnas + indiceLabelActual
        indiceMinimoCeldas = indiceLabelActual

        For i As Integer = 0 To numeroFilas - 1
            For j As Integer = 0 To numeroColumnas - 1
                Dim nuevoLabel As New Label With {
                    .Width = tamanoLabel,
                    .Height = tamanoLabel,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Left = j * (tamanoLabel + tamanoMargen) + tamanoMargen,
                    .Top = i * (tamanoLabel + tamanoMargen) + tamanoMargen
                }
                Me.Controls.Add(nuevoLabel)
            Next
        Next

        For i As Integer = 0 To Me.Controls.Count - 1
            If TypeOf Me.Controls(i) Is Label Then
                Debug.WriteLine(i)
            End If
        Next

    End Sub

    Private Sub _keyboardListener_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _keyboardListener.KeyDown
        Dim palabrasPermitidas As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ"
        If indiceLabelActual < numeroFilas * numeroColumnas Then
            Dim currentLabel As Label = CType(Me.Controls(indiceLabelActual), Label)
            Debug.WriteLine($"Current Label: {indiceLabelActual}")

            If palabrasPermitidas.Contains(e.KeyCode.ToString().ToUpper()) Then ''si no se ha pulsado eneter, se busca si la tecla esta en abecedario, si es asi haz ....
                If indiceLabelActual <> indiceMaximoCeldas Then
                    indiceLabelActual += 1
                    currentLabel.Text = e.KeyCode.ToString()
                    palabraFormando += currentLabel.Text
                End If
            End If

            If e.KeyCode = Keys.Back AndAlso indiceLabelActual > indiceMinimoCeldas Then
                palabraFormando = palabraFormando.Substring(0, palabraFormando.Length - 1)
                currentLabel = CType(Me.Controls(indiceLabelActual - 1), Label)
                currentLabel.Text = ""
                indiceLabelActual -= 1
                Debug.WriteLine(palabraFormando)
            End If

            If e.KeyCode = Keys.Return Then
                If indiceLabelActual <> indiceMaximoCeldas Then
                    MsgBox("Lenght != col")
                    Exit Sub
                End If

                palabraFormando = palabraFormando.Substring(0, numeroColumnas)

                If Not wordle.palbraEsValida(palabraFormando) Then
                    MsgBox("La palabra no existe")
                    Exit Sub
                End If

                For i = 0 To palabraFormando.Length - 1
                    Dim leterLabel As Label = CType(Me.Controls(i + indiceMinimoCeldas), Label)
                    Dim intCorrespondienteAChar() As Integer = wordle.GreenYellowGray(palabraFormando, 1)

                    If intCorrespondienteAChar(i) = 0 Then
                        leterLabel.BackColor = Color.Green
                    ElseIf intCorrespondienteAChar(i) = 1 Then
                        leterLabel.BackColor = Color.Yellow
                    Else
                        leterLabel.BackColor = Color.Gray
                    End If
                Next

                indiceMaximoCeldas += numeroColumnas
                indiceMinimoCeldas += numeroColumnas
                palabraFormando = ""
            End If

            Debug.WriteLine($"End Label: {indiceLabelActual}")
            Debug.WriteLine(palabraFormando)
        End If
    End Sub

End Class