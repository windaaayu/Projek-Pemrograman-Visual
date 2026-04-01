Imports MySql.Data.MySqlClient

Public Class Home
    Public Property roleUser As String ' menerima role dari form Master/login

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' ==== TAMPILKAN DATA LOGIN ====
        Try
            If Not String.IsNullOrEmpty(ModuleGlobal.userLogin) Then
                lblNama.Text = ModuleGlobal.userLogin
            Else
                lblNama.Text = "(Nama tidak ditemukan)"
            End If

            If Not String.IsNullOrEmpty(roleUser) Then
                lblRole.Text = roleUser
            Else
                lblRole.Text = "(Role tidak ditemukan)"
            End If
        Catch ex As Exception
            lblNama.Text = "(Error ambil data)"
            lblRole.Text = "-"
        End Try

        ' ==== ATUR ROLE ====
        If String.IsNullOrEmpty(roleUser) Then
            roleUser = "siswa" ' default kalau belum di-set
        End If

        ' === ATUR VISIBILITAS TOMBOL BERDASARKAN ROLE ===
        Select Case roleUser.ToLower()
            Case "admin"
                ' Admin bisa akses semua tombol
                btnBD.Visible = True
                btnPBO.Visible = True
                btnPemdas.Visible = True
                btnBasisData.Visible = True
                Button2.Visible = True
                Button3.Visible = True

            Case "guru"
                ' Guru hanya bisa kelola soal
                btnBD.Visible = True
                btnPBO.Visible = True
                btnPemdas.Visible = True
                btnBasisData.Visible = False
                Button2.Visible = False
                Button3.Visible = False

            Case "siswa"
                ' Siswa hanya bisa ikut quiz, tidak kelola soal
                btnBD.Visible = False
                btnPBO.Visible = False
                btnPemdas.Visible = False
                btnBasisData.Visible = True
                Button2.Visible = True
                Button3.Visible = True

            Case Else
                MessageBox.Show("Role tidak dikenali: " & roleUser)
        End Select
    End Sub

    ' =============================
    ' === QUIZ: BASIS DATA ========
    ' =============================
    Private Sub btnBasisData_Click(sender As Object, e As EventArgs) Handles btnBasisData.Click
        Dim panduan As String = "📘 Panduan Quiz Basis Data:" & vbCrLf &
                                "1. Jumlah soal: 10 soal pilihan ganda." & vbCrLf &
                                "2. Waktu pengerjaan: 5 menit." & vbCrLf &
                                "3. Jawaban benar bernilai 10 poin." & vbCrLf &
                                "4. Klik 'Yes' untuk memulai quiz."
        Dim result As DialogResult = MessageBox.Show(panduan, "Panduan Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If result = DialogResult.Yes Then
            Dim frm As New BasisData()
            If Master.Instance IsNot Nothing Then
                Master.Instance.LoadForm(frm)
            Else
                frm.ShowDialog()
            End If
        End If
    End Sub

    ' =============================
    ' === QUIZ: PBO ===============
    ' =============================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim panduan As String = "📘 Panduan Quiz PBO:" & vbCrLf &
                                "1. Jumlah soal: 10 soal pilihan ganda." & vbCrLf &
                                "2. Waktu pengerjaan: 5 menit." & vbCrLf &
                                "3. Jawaban benar bernilai 10 poin." & vbCrLf &
                                "4. Klik 'Yes' untuk memulai quiz."
        Dim result As DialogResult = MessageBox.Show(panduan, "Panduan Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If result = DialogResult.Yes Then
            Dim frm As New PBO()
            If Master.Instance IsNot Nothing Then
                Master.Instance.LoadForm(frm)
            Else
                frm.ShowDialog()
            End If
        End If
    End Sub

    ' =============================
    ' === QUIZ: PEMROGRAMAN DASAR =
    ' =============================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim panduan As String = "📘 Panduan Quiz Pemrograman Dasar:" & vbCrLf &
                                "1. Jumlah soal: 10 soal pilihan ganda." & vbCrLf &
                                "2. Waktu pengerjaan: 5 menit." & vbCrLf &
                                "3. Jawaban benar bernilai 10 poin." & vbCrLf &
                                "4. Klik 'Yes' untuk memulai quiz."
        Dim result As DialogResult = MessageBox.Show(panduan, "Panduan Quiz", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

        If result = DialogResult.Yes Then
            Dim frm As New PemrogramanDasar()
            If Master.Instance IsNot Nothing Then
                Master.Instance.LoadForm(frm)
            Else
                frm.ShowDialog()
            End If
        End If
    End Sub

    ' =============================
    ' === KELOLA SOAL (ADMIN/GURU)
    ' =============================
    Private Sub btnBD_Click(sender As Object, e As EventArgs) Handles btnBD.Click
        Master.mapelAktif = "Basis Data"
        Dim formKelola As New KelolaSoal()
        formKelola.ShowDialog()
    End Sub

    Private Sub btnPBO_Click(sender As Object, e As EventArgs) Handles btnPBO.Click
        Master.mapelAktif = "PBO"
        Dim formKelola As New KelolaSoal()
        formKelola.ShowDialog()
    End Sub

    Private Sub btnPemdas_Click(sender As Object, e As EventArgs) Handles btnPemdas.Click
        Master.mapelAktif = "Pemrograman Dasar"
        Dim formKelola As New KelolaSoal()
        formKelola.ShowDialog()
    End Sub
End Class
