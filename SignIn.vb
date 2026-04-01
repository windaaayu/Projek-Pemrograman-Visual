Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class SignIn

    Private Sub SignIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Placeholder awal
        TextBox1.Text = "Username"
        TextBox1.ForeColor = Color.Gray

        TextBox2.Text = "Password"
        TextBox2.ForeColor = Color.Gray
        TextBox2.UseSystemPasswordChar = False

        ' Hilangkan fokus awal
        Me.BeginInvoke(New Action(Sub() Me.ActiveControl = Nothing))

        ' Bentuk tampilan tombol & panel
        RoundButton(Button1, 30)
        RoundPanel(Panel1, 20)
    End Sub

    ' ==== DESAIN PANEL ====
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

    ' ==== DESAIN TOMBOL ====
    Private Sub RoundButton(btn As Button, radius As Integer)
        Dim path As New GraphicsPath()
        Dim rect As Rectangle = New Rectangle(0, 0, btn.Width, btn.Height)
        Dim curveSize As Integer = radius * 2
        path.AddArc(rect.Left, rect.Top, curveSize, rect.Height, 90, 180)
        path.AddArc(rect.Right - curveSize, rect.Top, curveSize, rect.Height, 270, 180)
        path.CloseFigure()
        btn.Region = New Region(path)
    End Sub

    ' ==== TEXTBOX USERNAME ====
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

    ' ==== TEXTBOX PASSWORD ====
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

    ' ==== LOGIN ====
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            bukaKoneksi() ' dari Module Koneksi

            ' 🔹 Ambil juga kolom email
            Dim query As String = "SELECT username, email, role FROM users WHERE username=@username AND password=@password"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim())
            cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim())

            rd = cmd.ExecuteReader()

            If rd.HasRows Then
                rd.Read()

                ' 🔹 Simpan hasil login ke variabel global
                userLogin = rd("username").ToString()
                emailLogin = rd("email").ToString()
                roleUser = rd("role").ToString()

                MessageBox.Show("Login berhasil sebagai " & roleUser, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

                rd.Close()
                conn.Close()

                ' 🔹 Buka form utama (Master/Home)
                Me.Hide()
                Dim f As New Master()
                f.roleUser = roleUser
                f.Show()
            Else
                MessageBox.Show("Username atau password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Pastikan reader & koneksi tertutup
            If rd IsNot Nothing AndAlso Not rd.IsClosed Then rd.Close()
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim f As New SignUp()
        f.Show()
        Me.Hide()
    End Sub
End Class
