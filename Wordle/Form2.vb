Imports WordleClases

Public Class Form2


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblestadisticas.Text = Globales.User.RachaActual
        lblmejorracha.Text = Globales.User.MejorRacha
        lblpartidasganadas.Text = Globales.User.PartidasGanadas
        lblpartidasjugadas.Text = Globales.User.PartidasJugadas
        lblpalabraadecuada.Text = Globales.Instanciadicionario._palabraGenerada
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnreintentar.Click
        Dim frm1 As New Form1
        frm1.Show()
        Me.Close()

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnfinalizar.Click
        Me.Close()
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles lblpalabradia.Click
        lblpalabradia.Text = Globales.User.PartidasGanadas
    End Sub

End Class