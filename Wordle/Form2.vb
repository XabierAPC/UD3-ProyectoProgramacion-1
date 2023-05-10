Imports WordleClases

Public Class Form2


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox3.Text = Globales.User.RachaActual
        TextBox4.Text = Globales.User.MejorRacha
        TextBox5.Text = Globales.User.PartidasGanadas
        TextBox6.Text = Globales.User.PartidasJugadas
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frm1 As New Form1
        frm1.Show()
        Me.Close()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


    End Sub
End Class