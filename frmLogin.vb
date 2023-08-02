Imports System.Data.SqlServerCe
Imports System.Data

Public Class frmLogin
    Dim dr As SqlCeDataReader
    Dim cmd As New SqlCeCommand
    Dim conn As SqlCeConnection
    Dim sqlce As New SqlCeCommandBuilder

    Public Sub ConnDB()

        conn = New SqlCeConnection("Data Source=D:\EXPERT_SYSTEM_FOR_HUMAN_NUTRITION_ANALYSIS\hospital.sdf")
        Try
            conn.Open()
            '  MessageBox.Show("connection is now open")
        Catch ex As Exception
            MessageBox.Show("connection Error")

        End Try
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPass.PasswordChar = "*"
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        ProgressBar1.Enabled = False
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(+1)
        If ProgressBar1.Value = 10 Then
            iblStatus.Text = "Loading.."
        ElseIf ProgressBar1.Value = 25 Then
            iblStatus.Text = "Loading...."
        ElseIf ProgressBar1.Value = 40 Then
            iblStatus.Text = "Gathering required data.."
        ElseIf ProgressBar1.Value = 65 Then
            iblStatus.Text = "Gathering required data....."
        ElseIf ProgressBar1.Value = 75 Then
            iblStatus.Text = "Verifying Data..."
        ElseIf ProgressBar1.Value = 82 Then
            iblStatus.Text = "Verifying Data......"
        ElseIf ProgressBar1.Value = 90 Then
            picloading.Visible = False
            Timer1.Stop()
            login()
        End If
    End Sub
    Private Sub login()
        Try
            If txtUser.Text = "" And txtPass.Text = "" Then
                MsgBox("Please enter username and password")
                txtUser.Focus()
            ElseIf txtUser.Text = "" Then
                MsgBox("Please enter username")
                txtUser.Focus()
            ElseIf txtPass.Text = "" Then
                MsgBox("Please enter password")
                txtPass.Focus()
            Else
                ConnDB()
                cmd = conn.CreateCommand()
                cmd.CommandText = "SELECT Username,Password FROM users where username = @d1 and password=@d2"
                cmd.Parameters.AddWithValue("@d1", txtUser.Text)
                cmd.Parameters.AddWithValue("@d2", (txtPass.Text))


                dr = cmd.ExecuteReader
                If dr.Read = True Then
                    Me.Hide()
                    frmMain.Show()
                   
                    MsgBox("Welcome " & " " & txtUser.Text, MsgBoxStyle.Information, "Login Report")

                Else
                    MsgBox("Incorrect Username or Password", MsgBoxStyle.Exclamation, "Login")
                End If
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox("" & ex.Message)
        End Try
    End Sub

    Private Sub chkpassword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkpassword.CheckedChanged
        If chkpassword.Checked Then
            txtPass.PasswordChar = ControlChars.NullChar
        Else
            txtPass.PasswordChar = "*"
        End If
    End Sub
    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub
    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        Timer1.Enabled = True
        ProgressBar1.Enabled = True
        ProgressBar1.Value = 0
        picloading.Visible = True
        iblStatus.Visible = True
    End Sub
End Class