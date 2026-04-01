Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class SignUp

    Private Sub SignUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tambahkan item role
        cmbRole.Items.Clear()
        cmbRole.Items.AddRange(New String() {"Siswa", "Guru", "Admin"})
        cmbRole.SelectedIndex = 0 ' default Siswa

        ' Placeholder setup
        TextBox1.Text = "Username"
        TextBox1.ForeColor = Color.Gray
        TextBox2.Text = "Password"
        TextBox2.ForeColor = Color.Gray
        TextBox3.Text = "Email"
        TextBox3.ForeColor = Color.Gray
        TextBox2.UseSystemPasswordChar = False

        RoundButton(Button1, 30)
        RoundPanel(Panel1, 20)
        Me.BeginInvoke(New Action(Sub() Me.ActiveControl = Nothing))
    End Sub

    ' ==== ROUND PANEL ====
    Private Sub RoundPanel(pnl As Panel, radius As Integer)
        Dim path As New GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, radius, radius), 180, 90)
        path.AddArc(New Rectangle(pnl.Width - radius, 0, radius, radius), 270, 90)
        path.AddArc(New Rectangle(pnl.Width - radius, pnl.Height - radius, radius, radius), 0, 90)
        path.AddArc(New Rectangle(0, pnl.Height - radius, radius, radius), 90, 90)
        path.CloseFigure()
        pnl.Region = New Region(path)
    End Sub

    ' ==== ROUND BUTTON ====
    Private Sub RoundButton(btn As Button, radius As Integer)
        Dim path As New GraphicsPath()
        Dim rect As Rectangle = New Rectangle(0, 0, btn.Width, btn.Height)
        Dim curveSize As Integer = radius * 2
        path.AddArc(rect.Left, rect.Top, curveSize, rect.Height, 90, 180)
        path.AddArc(rect.Right - curveSize, rect.Top, curveSize, rect.Height, 270, 180)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub

    ' ==== TEXTBOX PLACEHOLDER ====
    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        If TextBox1.Text = "Username" Then
            TextBox1.Text = ""
            TextBox1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            TextBox1.Text = "Username"
            TextBox1.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        If TextBox2.Text = "Password" Then
            TextBox2.Text = ""
            TextBox2.ForeColor = Color.Black
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            TextBox2.UseSystemPasswordChar = False
            TextBox2.Text = "Password"
            TextBox2.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub TextBox3_Enter(sender As Object, e As EventArgs) Handles TextBox3.Enter
        If TextBox3.Text = "Email" Then
            TextBox3.Text = ""
            TextBox3.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TextBox3_Leave(sender As Object, e As EventArgs) Handles TextBox3.Leave
        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            TextBox3.Text = "Email"
            TextBox3.ForeColor = Color.Gray
        End If
    End Sub

    ' ==== BUTTON REGISTER ====
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        bukaKoneksi()

        Try
            Dim email As String = TextBox3.Text.Trim()
            Dim username As String = TextBox1.Text.Trim()
            Dim password As String = TextBox2.Text.Trim()
            Dim role As String = cmbRole.SelectedItem.ToString()

            If email = "" Or username = "" Or password = "" Then
                MessageBox.Show("Semua kolom wajib diisi!")
                Return
            End If

            Dim sql As String = "INSERT INTO users (email, username, password, role) VALUES (@email, @username, @password, @role)"
            cmd = New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@email", email)
            cmd.Parameters.AddWithValue("@username", username)
            cmd.Parameters.AddWithValue("@password", password)
            cmd.Parameters.AddWithValue("@role", role)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Registrasi Berhasil! Silakan login.")
            Me.Hide()
            SignIn.Show()

        Catch ex As Exception
            MessageBox.Show("Gagal Registrasi: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub



End Class
