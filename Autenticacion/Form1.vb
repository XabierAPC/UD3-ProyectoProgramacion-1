Imports WordleClases
Imports Wordle
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
        Dim resultado As TipoError = u.ValidarUsuario(txtUserName.Text, txtPassword.Text)
        If resultado <> Nothing Then
            Select Case resultado
                Case TipoError.UsuarioVacio
                    MensajeDeErrorLogin(lblLoginUsername, "Por favor introduzca un nombre de usuario")
                Case TipoError.ContrasenaVacia
                    MensajeDeErrorLogin(lblLoginPassword, "Por favor introduzca una contrasena")
                Case TipoError.AmbosVacios
                    MensajeDeErrorLogin(lblLoginUsername, "Por favor introduzca un nombre de usuario")
                    MensajeDeErrorLogin(lblLoginPassword, "Por favor introduzca una contrasena")
                Case TipoError.UsuarioNoExiste
                    MensajeDeErrorLogin(lblLoginUsername, "El usuario no existe")
                Case TipoError.ContrasenaIncorrecta
                    MensajeDeErrorLogin(lblLoginPassword, "La contrasena es incorrecta")
            End Select
        Else
            MsgBox("Usuario Logeado")
            Dim word As New Wordle.Form1
            word.Show()
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
        Dim resultado As TipoError = u.AnadirUsuario(txtRegisterUsername.Text, txtRegisterPassword.Text, txtRegisterRePassword.Text)
        If resultado <> Nothing Then
            Select Case resultado
                Case TipoError.UsuarioVacio
                    MensajeDeErrorLogin(lblRegisterUsername, "Por favor introduzca un nombre de usuario")
                Case TipoError.ContrasenaVacia
                    MensajeDeErrorLogin(lblRegisterPassword, "Por favor introduzca una contrasena")
                Case TipoError.RepetirContrasenaVacia
                    MensajeDeErrorLogin(lblRegisterRePassword, "Por favor introduzca una contrasena")
                Case TipoError.AmbosVacios
                    MensajeDeErrorLogin(lblRegisterUsername, "Por favor introduzca un nombre de usuario")
                    MensajeDeErrorLogin(lblRegisterPassword, "Por favor introduzca una contrasena")
                    MensajeDeErrorLogin(lblRegisterRePassword, "Por favor introduzca una contrasena")
                Case TipoError.UsuarioLongitudIncorrecta
                    MensajeDeErrorLogin(lblRegisterUsername, "La longitud mayor a 4 caracteres")
                Case TipoError.ContrasenaLongitudIncorrecta
                    MensajeDeErrorLogin(lblRegisterPassword, "La longitud mayor a 6 caracteres")
                Case TipoError.AmbasLongitudesIncorrectas
                    MensajeDeErrorLogin(lblRegisterUsername, "La longitud mayor a 4 caracteres")
                    MensajeDeErrorLogin(lblRegisterPassword, "La longitud mayor a 6 caracteres")
                Case TipoError.UsuarioYaExiste
                    MensajeDeErrorLogin(lblRegisterUsername, "El usuario ya existe")
                Case TipoError.ContrasenaNoCoincide
                    MensajeDeErrorLogin(lblRegisterRePassword, "La contrasena no coincide")
            End Select
        Else
            MsgBox("Usuario Registrado")
            pnlRegister.Hide()
        End If
    End Sub

    Private Sub linkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabel2.LinkClicked
        pnlRegister.Hide()
    End Sub

    Private Sub linkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkLabel1.LinkClicked
        pnlRegister.Show()
    End Sub
End Class
