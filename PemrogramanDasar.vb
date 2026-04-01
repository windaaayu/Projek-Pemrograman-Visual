Imports MySql.Data.MySqlClient

Public Class PemrogramanDasar
    Private cmd As MySqlCommand
    Private dr As MySqlDataReader
    Private soalList As New List(Of Dictionary(Of String, String))
    Private currentQuestion As Integer = 0
    Private jawabanBenar As Integer = 0
    Private startTime As DateTime

    Dim namaUser As String = userLogin  ' Ambil dari sesi login
    Dim idMapel As Integer = 2          ' ID Mapel PBO (ubah sesuai database)

    ' ✅ Ketika form PBO dibuka
    Private Sub PemrogramanDasar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            bukaKoneksi() ' dari Module Koneksi
            muatSoal()
            tampilkanSoal()
            startTime = DateTime.Now ' mulai waktu pengerjaan
        Catch ex As Exception
            MessageBox.Show("Gagal memuat kuis: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ✅ Mengambil semua soal dari tabel tbl_soal_pemdas
    Private Sub muatSoal()
        soalList.Clear()
        cmd = New MySqlCommand("SELECT * FROM tbl_soal_pemdas ORDER BY id_soal", conn)
        dr = cmd.ExecuteReader()

        While dr.Read()
            Dim s As New Dictionary(Of String, String)
            s("pertanyaan") = dr("pertanyaan").ToString()
            s("a") = dr("opsi_a").ToString()
            s("b") = dr("opsi_b").ToString()
            s("c") = dr("opsi_c").ToString()
            s("d") = dr("opsi_d").ToString()
            s("jawaban") = dr("jawaban").ToString().Trim().ToUpper()
            soalList.Add(s)
        End While
        dr.Close()
    End Sub

    ' ✅ Menampilkan soal ke form
    Private Sub tampilkanSoal()
        If soalList.Count = 0 Then
            lblSoal.Text = "Tidak ada soal PBO di database."
            grpJawaban.Enabled = False
            btnNext.Enabled = False
            Return
        End If

        If currentQuestion >= soalList.Count Then
            tampilkanHasil()
            Return
        End If

        Dim s = soalList(currentQuestion)
        lblNomor.Text = $"Soal {currentQuestion + 1}/{soalList.Count}"
        lblSoal.Text = s("pertanyaan")

        rbA.Text = "A. " & s("a")
        rbB.Text = "B. " & s("b")
        rbC.Text = "C. " & s("c")
        rbD.Text = "D. " & s("d")

        rbA.Checked = False
        rbB.Checked = False
        rbC.Checked = False
        rbD.Checked = False

        btnNext.Text = If(currentQuestion = soalList.Count - 1, "Selesai", "Next →")
    End Sub

    ' ✅ Tombol Next → ke soal berikutnya
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If soalList.Count = 0 Then Return

        Dim s = soalList(currentQuestion)
        Dim jawabanUser As String = ""

        If rbA.Checked Then jawabanUser = "A"
        If rbB.Checked Then jawabanUser = "B"
        If rbC.Checked Then jawabanUser = "C"
        If rbD.Checked Then jawabanUser = "D"

        If jawabanUser = "" Then
            MessageBox.Show("Pilih salah satu jawaban terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If jawabanUser = s("jawaban") Then
            jawabanBenar += 1
        End If

        currentQuestion += 1
        tampilkanSoal()
    End Sub

    ' ✅ Tombol Back untuk kembali ke soal sebelumnya
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If currentQuestion > 0 Then
            currentQuestion -= 1
            tampilkanSoal()
        End If
    End Sub

    ' ✅ Menampilkan hasil akhir
    Private Sub tampilkanHasil()
        grpJawaban.Visible = False
        btnNext.Enabled = False
        btnBack.Enabled = False

        Dim endTime As DateTime = DateTime.Now
        Dim durasi As TimeSpan = endTime - startTime
        Dim waktuPengerjaan As String = $"{durasi.Minutes:D2}:{durasi.Seconds:D2}"
        Dim skorAkhir As Integer = CInt((jawabanBenar / soalList.Count) * 100)

        lblNomor.Text = "🎯 KUIS PBO SELESAI!"
        lblSoal.Font = New Font(lblSoal.Font.FontFamily, 16, FontStyle.Bold)
        lblSoal.Text = $"Nama: {namaUser}" & vbCrLf &
                       $"Skor Akhir: {skorAkhir}" & vbCrLf &
                       $"Jawaban Benar: {jawabanBenar}/{soalList.Count}" & vbCrLf &
                       $"Waktu Pengerjaan: {waktuPengerjaan}"

        ' Pesan evaluasi
        Dim pesan As String = If(skorAkhir >= 75, "🎉 Bagus! Kamu menguasai materi ini!", "😢 Nilai kamu masih kurang, tetap semangat belajar!")
        MessageBox.Show(pesan, "Hasil Kuis", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Simpan hasil
        simpanHasil(skorAkhir)
    End Sub

    ' ✅ Simpan hasil ke tabel hasil
    Private Sub simpanHasil(skorAkhir As Integer)
        Try
            Dim query As String = "INSERT INTO hasil (nama_user, id_mapel, skor, tanggal) VALUES (@nama, @mapel, @skor, NOW())"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@nama", namaUser)
            cmd.Parameters.AddWithValue("@mapel", idMapel)
            cmd.Parameters.AddWithValue("@skor", skorAkhir)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan hasil: " & ex.Message)
        End Try
    End Sub
End Class
