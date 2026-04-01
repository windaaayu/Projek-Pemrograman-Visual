Imports MySql.Data.MySqlClient

Public Class ManagementUser

    Sub tampilData()
        Try
            bukaKoneksi()
            da = New MySqlDataAdapter("SELECT * FROM users", conn)
            ds = New DataSet()
            da.Fill(ds, "users")
            DataGridView1.DataSource = ds.Tables("users")
            DataGridView1.ReadOnly = True
        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data: " & ex.Message)
        End Try
    End Sub

    Private Sub ManagementUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilData()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Try
            bukaKoneksi()
            Dim sql As String = "INSERT INTO users (email, username, password) VALUES (@email, @username, @password)"
            cmd = New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@email", InputBox("Masukkan Email:"))
            cmd.Parameters.AddWithValue("@username", InputBox("Masukkan Username:"))
            cmd.Parameters.AddWithValue("@password", InputBox("Masukkan Password:"))
            cmd.ExecuteNonQuery()
            MessageBox.Show("Data berhasil ditambahkan!")
            tampilData()
        Catch ex As Exception
            MessageBox.Show("Gagal menambah data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Dim id As String = InputBox("Masukkan ID user yang akan diedit:")
            bukaKoneksi()
            Dim sql As String = "UPDATE users SET email=@email, username=@username, password=@password WHERE id=@id"
            cmd = New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.Parameters.AddWithValue("@email", InputBox("Email baru:"))
            cmd.Parameters.AddWithValue("@username", InputBox("Username baru:"))
            cmd.Parameters.AddWithValue("@password", InputBox("Password baru:"))
            cmd.ExecuteNonQuery()
            MessageBox.Show("Data berhasil diperbarui!")
            tampilData()
        Catch ex As Exception
            MessageBox.Show("Gagal mengedit data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Try
            Dim id As String = InputBox("Masukkan ID user yang akan dihapus:")
            bukaKoneksi()
            Dim sql As String = "DELETE FROM users WHERE id=@id"
            cmd = New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Data berhasil dihapus!")
            tampilData()
        Catch ex As Exception
            MessageBox.Show("Gagal menghapus data: " & ex.Message)
        End Try
    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        Try
            bukaKoneksi()
            da = New MySqlDataAdapter("SELECT * FROM users WHERE username LIKE '%" & txtCari.Text & "%' OR email LIKE '%" & txtCari.Text & "%'", conn)
            ds = New DataSet()
            da.Fill(ds, "users")
            DataGridView1.DataSource = ds.Tables("users")
        Catch ex As Exception
            MessageBox.Show("Gagal mencari data: " & ex.Message)
        End Try
    End Sub

End Class
