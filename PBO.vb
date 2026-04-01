Imports MySql.Data.MySqlClient

Public Class PBO
    ' ====== Variabel utama ======
    Private totalSecondsInitial As Integer = 0
    Private totalSecondsLeft As Integer = 0
    Private secondsPerQuestion As Integer = 30
    Private cmd As MySqlCommand
    Private dr As MySqlDataReader
    Private soalList As New List(Of Dictionary(Of String, String))
    Private currentQuestion As Integer = 0
    Private jawabanBenar As Integer = 0
    Private panduanSudahMuncul As Boolean = False
    Private waktuMulai As DateTime

    Private idMapel As Integer = 3 ' ID mapel PBO (ubah sesuai DB kamu)
    Private userLogin As String = ""

    ' === Saat Form Dibuka ===
    Private Sub PBO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        userLogin = ModuleGlobal.userLogin
        waktuMulai = DateTime.Now

        ' === TAMPILKAN PANDUAN SEKALI ===
        If Not panduanSudahMuncul Then
            MessageBox.Show(
                "📘 PANDUAN QUIZ PBO 📘" & vbCrLf & vbCrLf &
                "1️⃣ Bacalah setiap soal dengan teliti." & vbCrLf &
                "2️⃣ Setiap soal memiliki waktu 30 detik." & vbCrLf &
                "3️⃣ Skor akhir akan ditampilkan di akhir kuis." & vbCrLf &
                "4️⃣ Jangan menutup aplikasi sebelum kuis selesai." & vbCrLf & vbCrLf &
                "Klik OK untuk memulai kuis.",
                "Panduan Kuis", MessageBoxButtons.OK, MessageBoxIcon.Information)
            panduanSudahMuncul = True
        End If

        bukaKoneksi()
        muatSoal()

        If soalList.Count = 0 Then
            MessageBox.Show("Tidak ada soal di database.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            grpJawaban.Enabled = False
            btnNext.Enabled = False
            btnBack.Enabled = False
            Return
        End If

        totalSecondsInitial = secondsPerQuestion * soalList.Count
        totalSecondsLeft = totalSecondsInitial

        ProgressBar1.Maximum = totalSecondsInitial
        ProgressBar1.Value = totalSecondsLeft

        tampilkanSoal()
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub

    ' === Memuat soal dari tabel ===
    Private Sub muatSoal()
        Try
            soalList.Clear()
            Dim query As String = "SELECT * FROM tbl_soal_pbo ORDER BY id_soal"
            cmd = New MySqlCommand(query, conn)
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
        Catch ex As Exception
            MessageBox.Show("Gagal memuat soal PBO: " & ex.Message)
        End Try
    End Sub

    ' === Menampilkan soal ===
    Private Sub tampilkanSoal()
        If soalList.Count = 0 Then
            lblSoal.Text = "Tidak ada soal di database."
            grpJawaban.Enabled = False
            btnNext.Enabled = False
            Return
        End If

        If currentQuestion >= soalList.Count Then
            akhirKuis()
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
        btnBack.Enabled = (currentQuestion > 0 AndAlso totalSecondsLeft > 0)
        grpJawaban.Enabled = (totalSecondsLeft > 0)
        btnNext.Enabled = (totalSecondsLeft > 0)
    End Sub

    ' === Timer berjalan setiap detik ===
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If totalSecondsLeft <= 0 Then
            Timer1.Stop()
            akhirKuis()
            Return
        End If

        totalSecondsLeft -= 1
        lblTimer.Text = FormatTime(totalSecondsLeft)

        If totalSecondsLeft >= 0 AndAlso totalSecondsLeft <= totalSecondsInitial Then
            ProgressBar1.Value = totalSecondsLeft
        End If

        lblTimer.ForeColor = If(totalSecondsLeft <= Math.Min(30, CInt(totalSecondsInitial * 0.1)), Color.Red, Color.Black)
    End Sub

    ' === Tombol Next ===
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If soalList.Count = 0 Then Return
        If totalSecondsLeft <= 0 Then
            MessageBox.Show("Waktu sudah habis.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim s = soalList(currentQuestion)
        Dim jawabanUser As String = ""

        If rbA.Checked Then jawabanUser = "A"
        If rbB.Checked Then jawabanUser = "B"
        If rbC.Checked Then jawabanUser = "C"
        If rbD.Checked Then jawabanUser = "D"

        If jawabanUser = s("jawaban") Then
            jawabanBenar += 1
        End If

        currentQuestion += 1
        tampilkanSoal()
    End Sub

    ' === Tombol Back ===
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If soalList.Count = 0 Then Return
        If totalSecondsLeft > 0 AndAlso currentQuestion > 0 Then
            currentQuestion -= 1
            tampilkanSoal()
        ElseIf totalSecondsLeft <= 0 Then
            MessageBox.Show("Tidak dapat kembali, waktu sudah habis!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' === Akhir kuis ===
    Private Sub akhirKuis()
        Timer1.Stop()

        grpJawaban.Visible = False
        btnNext.Visible = False
        btnBack.Visible = False
        ProgressBar1.Visible = False
        lblTimer.Visible = False

        Dim waktuSelesai As DateTime = DateTime.Now
        Dim durasi As TimeSpan = waktuSelesai - waktuMulai
        Dim nilai As Integer = CInt((jawabanBenar / soalList.Count) * 100)

        lblNomor.Text = "🎉 KUIS PBO SELESAI 🎉"
        lblNomor.Font = New Font("Segoe UI", 17, FontStyle.Bold)
        lblNomor.ForeColor = Color.MidnightBlue

        Dim pesan As String
        If nilai >= 80 Then
            pesan = "🔥 Hebat! Kamu menguasai materi dengan sangat baik!"
        ElseIf nilai >= 60 Then
            pesan = "👍 Lumayan! Tinggal sedikit lagi sempurna."
        Else
            pesan = "😅 Tetap semangat! Coba lagi nanti ya."
        End If

        lblSoal.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        lblSoal.ForeColor = Color.DarkGreen
        lblSoal.Text =
            $"Nama: {userLogin}" & vbCrLf &
            $"Skor Akhir: {nilai}%" & vbCrLf &
            $"Benar: {jawabanBenar} dari {soalList.Count} soal" & vbCrLf &
            $"Waktu Pengerjaan: {Math.Round(durasi.TotalSeconds)} detik" & vbCrLf &
            vbCrLf & pesan

        simpanHasil(nilai)
    End Sub

    ' === Simpan hasil ke database ===
    Private Sub simpanHasil(nilai As Integer)
        Try
            bukaKoneksi()
            Dim query As String = "INSERT INTO hasil (nama_user, id_mapel, skor, tanggal) VALUES (@nama, @mapel, @skor, NOW())"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@nama", userLogin)
            cmd.Parameters.AddWithValue("@mapel", idMapel)
            cmd.Parameters.AddWithValue("@skor", nilai)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan hasil: " & ex.Message)
        End Try
    End Sub

    ' === Format waktu ===
    Private Function FormatTime(sec As Integer) As String
        If sec < 0 Then sec = 0
        Dim m As Integer = sec \ 60
        Dim s As Integer = sec Mod 60
        Return m.ToString("00") & ":" & s.ToString("00")
    End Function
End Class
