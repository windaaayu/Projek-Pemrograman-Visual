Imports MySql.Data.MySqlClient

Module koneksi

    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public rd As MySqlDataReader
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public dr As MySqlDataReader

    Sub bukaKoneksi()
        Try
            conn = New MySqlConnection("server=localhost;user id=root;password=;database=db_quiz")
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal: " & ex.Message)
        End Try
    End Sub
End Module


