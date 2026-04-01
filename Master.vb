Public Class Master
    ' === VARIABEL GLOBAL UNTUK DIOPER KE FORM LAIN ===
    Public Shared Instance As Master                     ' referensi form Master (bisa dipanggil dari form lain)
    Public Shared mapelAktif As String = ""               ' menyimpan mata pelajaran aktif
    Public roleUser As String                            ' menyimpan role user yang login

    ' === METHOD UNTUK MEMUAT FORM DALAM PANEL ===
    Public Sub LoadForm(ByVal childForm As Form)
        Panel2.Controls.Clear()
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        Panel2.Controls.Add(childForm)
        childForm.Show()
    End Sub

    Private Sub ShowFormInPanel(frm As Form)
        Panel2.Controls.Clear()
        frm.TopLevel = False
        frm.FormBorderStyle = FormBorderStyle.None
        frm.Dock = DockStyle.Fill
        Panel2.Controls.Add(frm)
        frm.Show()
    End Sub

    ' === EVENT LOAD MASTER ===
    Private Sub Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Simpan referensi Instance untuk bisa diakses dari form lain
        Instance = Me

        ' Buka Home pertama kali
        Dim f As New Home()
        f.roleUser = roleUser
        ShowFormInPanel(f)

        ' 🔹 Sembunyikan tombol User Management jika bukan admin
        If roleUser.ToLower() = "siswa" Or roleUser.ToLower() = "guru" Then
            Button6.Visible = False   ' User Management hanya untuk admin
        Else
            Button6.Visible = True
        End If
    End Sub

    ' === NAVIGASI MENU ===
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New Home()
        f.roleUser = roleUser
        ShowFormInPanel(f)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ShowFormInPanel(New Profil())
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ShowFormInPanel(New About())
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ShowFormInPanel(New Hasil())
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ShowFormInPanel(New ManagementUser())
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Restart()
    End Sub
End Class
