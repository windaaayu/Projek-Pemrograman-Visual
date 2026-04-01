Imports MySql.Data.MySqlClient

Public Class TambahSoal
    Public modeEdit As Boolean = False
    Public idSoalEdit As String = ""
    Public mapelAktif As String   ' <===== FIX PENTING

    Private Sub TambahSoal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbJawabanBenar.Items.Clear()
        cmbJawabanBenar.Items.AddRange(New String() {"A", "B", "C", "D"})

        If modeEdit Then
            TampilkanDataEdit()
        End If
    End Sub

    ' === menentukan tabel sesuai mapel ===
    Private Function GetNamaTabel() As String
        Select Case mapelAktif
            Case "Basis Data"
                Return "tbl_soal_basisdata"
            Case "PBO"
                Return "tbl_soal_pbo"
            Case "Pemrograman Dasar"
                Return "tbl_soal_pemdas"
            Case Else
                Return ""
        End Select
    End Function

    ' === ambil data untuk edit ===
    Private Sub TampilkanDataEdit()
        Try
            Call bukaKoneksi()
            Dim namaTabel = GetNamaTabel()

            Dim query As String = $"SELECT * FROM {namaTabel} WHERE id_soal = @id"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", idSoalEdit)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                txtSoal.Text = dr("pertanyaan").ToString()
                txtA.Text = dr("opsi_a").ToString()
                txtB.Text = dr("opsi_b").ToString()
                txtC.Text = dr("opsi_c").ToString()
                txtD.Text = dr("opsi_d").ToString()
                cmbJawabanBenar.Text = dr("jawaban").ToString()
            End If

            dr.Close()
            conn.Close()

        Catch ex As Exception
            MsgBox("Gagal memuat data untuk edit: " & ex.Message)
        End Try
    End Sub

    ' === simpan atau update soal ===
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            If txtSoal.Text = "" Or txtA.Text = "" Or txtB.Text = "" Or txtC.Text = "" Or txtD.Text = "" Or cmbJawabanBenar.Text = "" Then
                MsgBox("Semua kolom harus diisi!", vbExclamation)
                Exit Sub
            End If

            Call bukaKoneksi()
            Dim namaTabel = GetNamaTabel()
            Dim query As String

            If modeEdit = False Then
                query = $"INSERT INTO {namaTabel} (pertanyaan, opsi_a, opsi_b, opsi_c, opsi_d, jawaban) 
                         VALUES (@tanya, @a, @b, @c, @d, @jawaban)"
            Else
                query = $"UPDATE {namaTabel} SET pertanyaan=@tanya, opsi_a=@a, opsi_b=@b, opsi_c=@c, opsi_d=@d, jawaban=@jawaban 
                         WHERE id_soal=@id"
            End If

            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@tanya", txtSoal.Text)
            cmd.Parameters.AddWithValue("@a", txtA.Text)
            cmd.Parameters.AddWithValue("@b", txtB.Text)
            cmd.Parameters.AddWithValue("@c", txtC.Text)
            cmd.Parameters.AddWithValue("@d", txtD.Text)
            cmd.Parameters.AddWithValue("@jawaban", cmbJawabanBenar.Text)

            If modeEdit Then
                cmd.Parameters.AddWithValue("@id", idSoalEdit)
            End If

            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan!", vbInformation)
            conn.Close()
            Me.Close()

        Catch ex As Exception
            MsgBox("Gagal menyimpan data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub

End Class
