Imports MySql.Data.MySqlClient

Public Class Hasil

    Private Sub Hasil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            bukaKoneksi()
            TampilDataHasil()
        Catch ex As Exception
            MessageBox.Show("Koneksi ke database gagal: " & ex.Message)
        End Try
    End Sub

    ' === TAMPILKAN DATA KE DATAGRID ===
    Sub TampilDataHasil()
        Try
            Dim query As String

            ' === CEK ROLE LOGIN ===
            If roleUser.ToLower() = "siswa" Then
                ' Jika siswa → hanya tampilkan hasil miliknya sendiri
                query =
                    "SELECT hasil.id_hasil AS 'ID Hasil', " &
                    "COALESCE(users.username, hasil.nama_user) AS 'Nama User', " &
                    "hasil.id_mapel AS 'Kode Mapel', " &
                    "hasil.skor AS 'Skor', " &
                    "hasil.tanggal AS 'Tanggal' " &
                    "FROM hasil " &
                    "LEFT JOIN users ON hasil.nama_user = users.username " &
                    "WHERE hasil.nama_user = @namaUser " &
                    "ORDER BY hasil.id_hasil DESC"

            ElseIf roleUser.ToLower() = "guru" Or roleUser.ToLower() = "admin" Then
                ' Jika guru / admin → bisa lihat semua hasil
                query =
                    "SELECT hasil.id_hasil AS 'ID Hasil', " &
                    "COALESCE(users.username, hasil.nama_user) AS 'Nama User', " &
                    "hasil.id_mapel AS 'Kode Mapel', " &
                    "hasil.skor AS 'Skor', " &
                    "hasil.tanggal AS 'Tanggal' " &
                    "FROM hasil " &
                    "LEFT JOIN users ON hasil.nama_user = users.username " &
                    "ORDER BY hasil.id_hasil DESC"
            Else
                MessageBox.Show("Role tidak dikenal!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim cmd As New MySqlCommand(query, conn)
            If roleUser.ToLower() = "siswa" Then
                cmd.Parameters.AddWithValue("@namaUser", userLogin)
            End If

            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "hasil")

            DataGridView1.DataSource = ds.Tables("hasil")

            ' === FORMAT TABEL ===
            With DataGridView1
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .ReadOnly = True
                .AllowUserToAddRows = False
                .RowHeadersVisible = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data: " & ex.Message)
        End Try
    End Sub


    ' === REFRESH DATA ===
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            TampilDataHasil()
            MessageBox.Show("Data berhasil diperbarui!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Gagal merefresh data: " & ex.Message)
        End Try
    End Sub


    ' === HAPUS DATA TERPILIH ===
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("Pilih data yang ingin dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim idHasil As Integer = CInt(DataGridView1.SelectedRows(0).Cells("ID Hasil").Value)

            ' Konfirmasi sebelum hapus
            If MessageBox.Show("Yakin ingin menghapus data dengan ID " & idHasil & " ?", "Konfirmasi Hapus",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If

            bukaKoneksi()
            Dim cmd As New MySqlCommand("DELETE FROM hasil WHERE id_hasil=@id", conn)
            cmd.Parameters.AddWithValue("@id", idHasil)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            TampilDataHasil()

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menghapus data: " & ex.Message)
        End Try
    End Sub

End Class
