Imports System.Net.NetworkInformation
Imports Microsoft.VisualBasic.ApplicationServices
Imports WordleClases
Public Class Form1
    Dim u As New Usuarios()
    Private Sub MensajeDeErrorLogin(labelActuar As Label, mensajeUsuario As String)
        If Not String.IsNullOrEmpty(mensajeUsuario) Then
            labelActuar.Text = mensajeUsuario
            labelActuar.Visible = True
        End If
    End Sub
    Private Sub LimpiarErrores(labelActuar As Label)
        labelActuar.Visible = False
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim exitOnNextPair As Boolean = False

        If String.IsNullOrEmpty(txtUserName.Text) Then
            MensajeDeErrorLogin(lblLoginUsername, "Por favor introduzca un nombre de usuario")
            exitOnNextPair = True
        End If

        If String.IsNullOrEmpty(txtPassword.Text) Then
            MensajeDeErrorLogin(lblLoginPassword, "Por favor introduzca una contrasena")
            exitOnNextPair = True
        End If

        If exitOnNextPair Then
            Exit Sub
        End If

        Dim user As New Usuario(txtUserName.Text, txtPassword.Text)

        If u.EsUsuarioValido(user) Then
            MensajeDeErrorLogin(lblLoginUsername, "El usuario NO existe")
            exitOnNextPair = True
        End If

        If exitOnNextPair Then
            Exit Sub
        End If
    End Sub

    Private Sub txtUserName_Enter(sender As Object, e As EventArgs) Handles txtUserName.Enter
        LimpiarErrores(lblLoginUsername)
    End Sub

    Private Sub txtPassword_Enter(sender As Object, e As EventArgs) Handles txtPassword.Enter
        LimpiarErrores(lblLoginPassword)
    End Sub
    Private Sub txtRegisterUsername_Enter(sender As Object, e As EventArgs) Handles txtRegisterUsername.Enter
        LimpiarErrores(lblRegisterUsername)
    End Sub

    Private Sub txtRegisterPassword_Enter(sender As Object, e As EventArgs) Handles txtRegisterPassword.Enter
        LimpiarErrores(lblRegisterPassword)
    End Sub

    Private Sub txtRegisterRePassword_Enter(sender As Object, e As EventArgs) Handles txtRegisterRePassword.Enter
        LimpiarErrores(lblRegisterRePassword)
    End Sub

    Private Sub Register_Click(sender As Object, e As EventArgs) Handles Register.Click
        Dim exitOnNextPair As Boolean = False

        If String.IsNullOrEmpty(txtRegisterUsername.Text) Then
            MensajeDeErrorLogin(lblRegisterUsername, "Por favor introduzca un nombre de usuario")
            exitOnNextPair = True
        End If

        If String.IsNullOrEmpty(txtRegisterPassword.Text) Then
            MensajeDeErrorLogin(lblRegisterPassword, "Por favor introduzca una contrasena")
            exitOnNextPair = True
        End If

        If String.IsNullOrEmpty(txtRegisterRePassword.Text) Then
            MensajeDeErrorLogin(lblRegisterRePassword, "Por favor introduzca una contrasena")
            exitOnNextPair = True
        End If

        If exitOnNextPair Then
            Exit Sub
        End If

        If txtRegisterUsername.Text.Length < 4 Then
            MensajeDeErrorLogin(lblRegisterUsername, "El nombre de usuario debe tener al menos 4 caracteres")
            exitOnNextPair = True
        End If

        If txtRegisterPassword.Text.Length < 6 Then
            MensajeDeErrorLogin(lblRegisterPassword, "La contrasena debe tener al menos 6 caracteres")
            exitOnNextPair = True
        End If

        If exitOnNextPair Then
            Exit Sub
        End If

        Dim user As New Usuario(txtRegisterUsername.Text, txtRegisterPassword.Text)

        If Not u.EsUsuarioValido(user) Then
            MensajeDeErrorLogin(lblRegisterUsername, "El usuario ya existe")
            exitOnNextPair = True
        End If

        If exitOnNextPair Then
            Exit Sub
        End If

        If txtRegisterPassword.Text <> txtRegisterRePassword.Text Then
            MensajeDeErrorLogin(lblRegisterRePassword, "La contrasena no coincide")
            exitOnNextPair = True
        End If

        If exitOnNextPair Then
            Exit Sub
        End If
    End Sub

    Private Sub linkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabel2.LinkClicked
        pnlRegister.Hide()
    End Sub

    Private Sub linkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabel1.LinkClicked
        pnlRegister.Show()
    End Sub
End Class
