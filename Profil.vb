' ==========================
' Profil.vb (TextBox3 dikunci total)
' ==========================
Public Class Profil

    Private fotoDefault As Image
    Private fotoDariExplorer As Boolean = False

    Private Sub Profil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tampilkan data dari ModuleGlobal
        TextBox1.Text = userLogin
        TextBox2.Text = emailLogin
        TextBox3.Text = roleUser

        ' Simpan foto default
        fotoDefault = PictureBox1.Image

        ' Ambil path foto dari My.Settings (jika ada)
        If Not String.IsNullOrEmpty(My.Settings.FotoUserPath) AndAlso IO.File.Exists(My.Settings.FotoUserPath) Then
            PictureBox1.Image = Image.FromFile(My.Settings.FotoUserPath)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            fotoDariExplorer = True
        End If

        ' Awal form: semua textbox dikunci
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.Enabled = False ' tidak bisa diklik atau diedit
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim result As DialogResult = MessageBox.Show(
            "Pilih tindakan untuk foto profil:" & vbCrLf & vbCrLf &
            "Ya = Ganti Foto" & vbCrLf &
            "Tidak = Hapus Foto" & vbCrLf &
            "Batal = Tutup",
            "Pengaturan Foto Profil",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            ofd.Title = "Pilih Foto Baru"

            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    ' Buat folder FotoUser jika belum ada
                    Dim folderPath As String = Application.StartupPath & "\FotoUser\"
                    If Not IO.Directory.Exists(folderPath) Then
                        IO.Directory.CreateDirectory(folderPath)
                    End If

                    ' Nama file berdasarkan username
                    Dim targetPath As String = IO.Path.Combine(folderPath, $"{userLogin}.jpg")

                    ' Salin ke folder lokal
                    IO.File.Copy(ofd.FileName, targetPath, True)

                    ' Simpan path ke My.Settings
                    My.Settings.FotoUserPath = targetPath
                    My.Settings.Save()

                    ' Tampilkan di PictureBox
                    PictureBox1.Image = Image.FromFile(targetPath)
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                    fotoDariExplorer = True

                    MessageBox.Show("Foto profil berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Gagal memuat foto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

        ElseIf result = DialogResult.No Then
            If fotoDariExplorer Then
                PictureBox1.Image = fotoDefault
                fotoDariExplorer = False
                My.Settings.FotoUserPath = ""
                My.Settings.Save()
                MessageBox.Show("Foto profil dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Tidak ada foto tambahan untuk dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        ' Hanya username & email yang bisa diedit
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        TextBox3.Enabled = False ' tetap dikunci, tidak bisa diklik
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        ' Simpan perubahan
        userLogin = TextBox1.Text
        emailLogin = TextBox2.Text
        ' Role tidak bisa diubah, jadi tidak disimpan ulang

        MessageBox.Show("Perubahan profil disimpan!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Kunci kembali textbox
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.Enabled = False
    End Sub

End Class
