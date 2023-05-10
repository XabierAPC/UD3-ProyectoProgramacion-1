Imports System.Drawing.Text
Imports System.IO
Imports WordleClases
Imports WorldleUtilidades
Public Class Form1
    Dim numeroFilas As Integer = 6
    Dim numeroColumnas As Integer = 5
    Dim tamanoLabel As Integer = 62
    Dim tamanoMargen As Integer = 5

    Dim fix As Integer

    Dim inicioLabels As Integer
    Dim finLabels As Integer

    Dim indiceLabelActual As Integer
    Dim indiceMaximoCeldas As Integer
    Dim indiceMinimoCeldas As Integer
    Dim palabraFormando As String
    Dim grpContenedor As New GroupBox()

    Dim wasLoaded As Boolean = False

    Private WithEvents _keyboardListener As KeyboardListener

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        CargarJuego()

        wasLoaded = True
    End Sub

    Private Sub CargarJuego()
        'Generate random word
        Globales.Instanciadicionario.GetRandomWord(numeroColumnas)
        MsgBox(Instanciadicionario._palabraGenerada.Texto)

        'Create group box and add it to the form

        grpContenedor.Dock = DockStyle.Fill
        grpContenedor.Size = New Size(numeroColumnas * (tamanoLabel + tamanoMargen) + tamanoMargen, numeroFilas * (tamanoLabel + tamanoMargen) + tamanoMargen)
        Me.Controls.Add(grpContenedor)

        'Set position of group box and other controls
        Dim posY = Me.Height / 2
        Dim posX = Me.Width / 2
        grpTeclado.Location = New Point(posX - (grpTeclado.Size.Width / 2), Me.Height - grpTeclado.Size.Height)
        grpMenu.Location = New Point(posX - (grpMenu.Size.Width / 2), 0)
        posX = posX - (numeroColumnas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2
        posY = posY - (numeroFilas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2

        Dim font As New Font("Arial", 24, FontStyle.Bold)
        For i As Integer = 0 To numeroFilas - 1
            posX = posX + ((numeroColumnas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2) - ((numeroColumnas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2)
            For j As Integer = 0 To numeroColumnas - 1
                Dim nuevoLabel As New Label With {
                .Width = tamanoLabel,
                .Height = tamanoLabel,
                .BorderStyle = BorderStyle.FixedSingle,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Left = j * (tamanoLabel + tamanoMargen) + tamanoMargen,
                .Top = i * (tamanoLabel + tamanoMargen) + tamanoMargen,
                .Location = New Point(posX, posY),
                .Font = font
            }
                posX += nuevoLabel.Width + tamanoMargen
                grpContenedor.Controls.Add(nuevoLabel)
            Next
            posX = Me.Width / 2 - (numeroColumnas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2
            posY += tamanoLabel + tamanoMargen
        Next

        Dim lbl As New Label With {.Visible = False}
        grpContenedor.Controls.Add(lbl)

        'Access the Label controls inside the group box
        For i As Integer = 0 To grpContenedor.Controls.Count - 1
            If TypeOf grpContenedor.Controls(i) Is Label Then
                Debug.WriteLine(i)
            End If
        Next

        For Each control As Control In grpContenedor.Controls
            If TypeOf control Is Label Then
                ' Do something with the label
                Dim label As Label = CType(control, Label)
                label.Text = ""
            End If
        Next

        'Set values for other variables
        fix = Me.Controls.Count
        inicioLabels = fix
        indiceLabelActual = 0
        indiceMaximoCeldas = numeroColumnas + indiceLabelActual
        indiceMinimoCeldas = indiceLabelActual
        finLabels = Me.Controls.Count - 1
        _keyboardListener = New KeyboardListener()
    End Sub



    Private Sub _keyboardListener_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _keyboardListener.KeyDown
        Dim caracteresPermitidos As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ"
        If indiceLabelActual <= numeroFilas * numeroColumnas Then
            Dim currentLabel As Label = CType(grpContenedor.Controls(indiceLabelActual), Label)
            Debug.WriteLine($"Current Label: {indiceLabelActual}")

            If caracteresPermitidos.Contains(e.KeyCode.ToString().ToUpper()) Then ''si no se ha pulsado eneter, se busca si la tecla esta en abecedario, si es asi haz ....
                LetraPresionada(currentLabel, e.KeyCode.ToString())
            End If

            If e.KeyCode = Keys.Back Then
                ReturnPresionado(currentLabel)
            End If

            If e.KeyCode = Keys.Return Then
                EnterPresionado()
            End If

            e.Handled = True
            Debug.WriteLine($"End Label: {indiceLabelActual}")
            Debug.WriteLine(palabraFormando)
        End If
    End Sub
    Private Sub EnterPresionado()
        If indiceLabelActual <> indiceMaximoCeldas Then
            Exit Sub
        End If

        palabraFormando = palabraFormando.Substring(0, numeroColumnas)

        If Not Globales.Instanciadicionario.palbraEsValida(palabraFormando) Then
            Exit Sub
        End If

        For i = 0 To palabraFormando.Length - 1
            Dim leterLabel As Label = CType(grpContenedor.Controls(i + indiceMinimoCeldas), Label)
            Dim intCorrespondienteAChar() As Integer = Globales.Instanciadicionario.GreenYellowGray(palabraFormando)

            If intCorrespondienteAChar(i) = Diccionario.TipoAcierto.Acertado Then
                leterLabel.BackColor = ColorTranslator.FromHtml("#538d4e")
                'cont
            ElseIf intCorrespondienteAChar(i) = Diccionario.TipoAcierto.Regular Then
                leterLabel.BackColor = ColorTranslator.FromHtml("#b59f3b")
            Else
                leterLabel.BackColor = ColorTranslator.FromHtml("#3a3a3c")
            End If
        Next
        If Globales.Instanciadicionario.HaGanado(palabraFormando, indiceLabelActual) Then
            Dim frm2 As New Form2
            frm2.Show()
            Globales.listaUsuarios.GuardarUsuarios()
            Me.Dispose()
        End If
        indiceMaximoCeldas += numeroColumnas
        indiceMinimoCeldas += numeroColumnas
        palabraFormando = ""
    End Sub

    Private Sub ReturnPresionado(currentLabel As Label)
        If indiceLabelActual > indiceMinimoCeldas Then
            palabraFormando = palabraFormando.Substring(0, palabraFormando.Length - 1)
            currentLabel = CType(grpContenedor.Controls(indiceLabelActual - 1), Label)
            currentLabel.Text = ""
            indiceLabelActual -= 1
            Debug.WriteLine(palabraFormando)
        End If
    End Sub

    Private Sub LetraPresionada(currentLabel As Label, letra As String)
        If indiceLabelActual <> indiceMaximoCeldas Then
            indiceLabelActual += 1
            currentLabel.Text = letra
            palabraFormando += currentLabel.Text
        End If
    End Sub
    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btnQ.Click, btnW.Click, btnR.Click, btnT.Click, btnY.Click, btnU.Click, btnI.Click, btnE.Click, btnO.Click, btnP.Click, btnA.Click, btnS.Click, btnD.Click, btnF.Click, btnG.Click, btnH.Click, btnJ.Click, btnK.Click, btnL.Click, btnÑ.Click, btnZ.Click, btnX.Click, btnC.Click, btnV.Click, btnB.Click, btnN.Click, btnM.Click
        Dim button As Button = CType(sender, Button)
        Dim currentLabel As Label = CType(Me.Controls(indiceLabelActual), Label)
        LetraPresionada(currentLabel, button.Text)
    End Sub

    Private Sub btnENVIAR_Click(sender As Object, e As EventArgs) Handles btnENVIAR.Click
        EnterPresionado()

    End Sub

    Private Sub btnELIMINAR_Click(sender As Object, e As EventArgs) Handles btnELIMINAR.Click
        Dim currentLabel As Label = CType(Me.Controls(indiceLabelActual), Label)
        ReturnPresionado(currentLabel)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim posY = Me.Height / 2
        Dim posX = Me.Width / 2

        grpTeclado.Location = New Point(posX - (grpTeclado.Size.Width / 2), Me.Height - grpTeclado.Size.Height)
        grpMenu.Location = New Point(posX - (grpMenu.Size.Width / 2), 0)
        'If wasLoaded Then
        '    posX = Me.Width / 2 - (numeroColumnas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2
        '    posY = Me.Height / 2 - (numeroFilas * (tamanoLabel + tamanoMargen) + tamanoMargen) / 2

        '    For i As Integer = fix To numeroFilas * numeroColumnas + 1
        '        Dim labelToUpdate As Label = CType(Me.Controls(i), Label)
        '        Dim row As Integer = (i - inicioLabels) \ numeroColumnas
        '        Dim col As Integer = (i - inicioLabels) Mod numeroColumnas
        '        labelToUpdate.Left = posX + col * (tamanoLabel + tamanoMargen) + tamanoMargen
        '        labelToUpdate.Top = posY + row * (tamanoLabel + tamanoMargen) + tamanoMargen
        '    Next
        'End If


    End Sub


End Class