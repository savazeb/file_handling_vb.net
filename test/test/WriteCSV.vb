Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text

Public Class WriteCSV
    Public Shared Sub WriteCSV(dt As DataTable, path As String)
        Dim rows As Integer = dt.Rows.Count
        Dim cols As Integer = dt.Columns.Count

        Dim hasHeader As Boolean
        Dim replace As String = ""
        Dim separator As String = ","
        Dim quote As String = ""
        Dim text As String

        Dim writer As StreamWriter = New StreamWriter(path, False)

        'ここのif文では、DataTableに必要なカラムを追加するために最初に1行だけ読み込んでいます。
        If hasHeader = True Then
            'カラム名を保存する場合
            For i As Integer = 0 To cols - 1
                'カラム名を取得
                If quote <> "" Then
                    'データ内のクォーテーション文字のエスケープ処理
                    text = dt.Columns(i).ColumnName.Replace(quote, replace)
                Else
                    text = dt.Columns(i).ColumnName
                End If
                'レコード内の最後の列かどうか
                If i <> cols - 1 Then
                    writer.Write(quote + text + quote + separator)
                Else
                    '最後のレコードなら区切り文字は要らない
                    writer.WriteLine(quote + text + quote)
                End If
            Next i
        End If
        'データの保存処理
        For i As Integer = 0 To rows - 1
            For j As Integer = 0 To cols - 1
                If (quote <> "") Then
                    'データ内のクォーテーション文字のエスケープ処理
                    text = dt.Rows(i)(j).ToString().Replace(quote, replace)
                Else
                    text = dt.Rows(i)(j).ToString()
                End If
                'レコード内の最後の列かどうか
                If j <> cols - 1 Then
                    writer.Write(quote + text + quote + separator)
                Else
                    '最後のレコードなら区切り文字は要らない
                    writer.WriteLine(quote + text + quote)
                End If
            Next j
        Next i
        'ストリームを閉じる
        writer.Close()
    End Sub
End Class
