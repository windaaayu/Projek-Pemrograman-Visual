Imports MySql.Data.MySqlClient

Public Class KelolaSoal

    ' Diambil dari form Master
    Public mapelAktif As String = Master.mapelAktif

    Private Sub KelolaSoal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Jika kosong, ambil ulang dari Master
        If String.IsNullOrEmpty(mapelAktif) Then
            mapelAktif = Master.mapelAktif
        End If

        ' Jika tetap kosong → user belum pilih mapel
        If String.IsNullOrEmpty(mapelAktif) Then
            MsgBox("Mapel belum dipilih di form Master!", vbExclamation)
            Me.Close()
            Exit Sub
        End If

        Me.Text = "Kelola Soal - " & mapelAktif

        TampilData()
    End Sub


    ' ===================================================
    ' ================ TAMPILKAN DATA ===================
    ' ===================================================
    Sub TampilData()
        Try
            bukaKoneksi()

            Dim namaTabel As String = GetNamaTabel()
            If namaTabel = "" Then
                MsgBox("Mapel tidak dikenal!", vbExclamation)
                Exit Sub
            End If

            Dim query As String = $"SELECT id_soal, pertanyaan, jawaban FROM {namaTabel}"
            Dim cmd As New MySqlCommand(query, conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            DataGridView1.DataSource = dt

            ' Atur tampilan kolom
            If DataGridView1.Columns.Contains("id_soal") Then
                DataGridView1.Columns("id_soal").Visible = False
            End If

            If DataGridView1.Columns.Contains("pertanyaan") Then
                DataGridView1.Columns("pertanyaan").HeaderText = "Pertanyaan"
                DataGridView1.Columns("pertanyaan").Width = 500
            End If

            If DataGridView1.Columns.Contains("jawaban") Then
                DataGridView1.Columns("jawaban").HeaderText = "Jawaban"
                DataGridView1.Columns("jawaban").Width = 80
            End If

            ' Tambahkan tombol jika belum ada
            If Not DataGridView1.Columns.Contains("Edit") Then
                Dim btnEdit As New DataGridViewButtonColumn()
                btnEdit.Name = "Edit"
                btnEdit.HeaderText = "Edit"
                btnEdit.Text = "Edit"
                btnEdit.UseColumnTextForButtonValue = True
                DataGridView1.Columns.Add(btnEdit)
            End If

            If Not DataGridView1.Columns.Contains("Hapus") Then
                Dim btnHapus As New DataGridViewButtonColumn()
                btnHapus.Name = "Hapus"
                btnHapus.HeaderText = "Hapus"
                btnHapus.Text = "Hapus"
                btnHapus.UseColumnTextForButtonValue = True
                DataGridView1.Columns.Add(btnHapus)
            End If

        Catch ex As Exception
            MsgBox("Gagal menampilkan data: " & ex.Message, vbCritical)
        End Try
    End Sub



    ' ===================================================
    ' ================ AMBIL NAMA TABEL =================
    ' ===================================================
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



    ' ===================================================
    ' ===================== TAMBAH ======================
    ' ===================================================
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Dim formTambah As New TambahSoal
        formTambah.modeEdit = False
        formTambah.mapelAktif = mapelAktif ' <<< FIX PENTING
        formTambah.ShowDialog()

        TampilData()
    End Sub



    ' ===================================================
    ' ================== EDIT / HAPUS ===================
    ' ===================================================
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim namaTabel As String = GetNamaTabel()
        If namaTabel = "" Then Exit Sub

        Dim idSoal As String = DataGridView1.Rows(e.RowIndex).Cells("id_soal").Value.ToString()


        ' ==============================
        '            EDIT
        ' ==============================
        If e.ColumnIndex = DataGridView1.Columns("Edit").Index Then

            Dim formEdit As New TambahSoal
            formEdit.modeEdit = True
            formEdit.idSoalEdit = idSoal
            formEdit.mapelAktif = mapelAktif ' <<< FIX PENTING
            formEdit.ShowDialog()

            TampilData()


            ' ==============================
            '            HAPUS
            ' ==============================
        ElseIf e.ColumnIndex = DataGridView1.Columns("Hapus").Index Then

            Dim konfirmasi = MsgBox("Yakin ingin menghapus soal ini?", vbYesNo + vbQuestion)

            If konfirmasi = vbYes Then
                Try
                    bukaKoneksi()

                    Dim query As String = $"DELETE FROM {namaTabel} WHERE id_soal=@id"
                    Dim cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", idSoal)
                    cmd.ExecuteNonQuery()

                    MsgBox("Soal berhasil dihapus!", vbInformation)
                    TampilData()

                Catch ex As Exception
                    MsgBox("Gagal menghapus soal: " & ex.Message, vbCritical)
                End Try
            End If

        End If
    End Sub



    ' ===================================================
    ' ================== RAPIKAN GRID ===================
    ' ===================================================
    Private Sub RapiinGrid()
        With DataGridView1
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .RowHeadersVisible = False
        End With
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        RapiinGrid()
    End Sub

End Class
