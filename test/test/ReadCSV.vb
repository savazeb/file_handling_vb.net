Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text
Public Class ReadCSV
    Public Shared Function ReadCSV(ByVal path As String) As DataTable

        Dim dt As New DataTable

        Dim hasHeader As Boolean

        Using parser As New TextFieldParser(path)
            ' カンマ区切りの指定
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' フィールドが引用符で囲まれているか
            parser.HasFieldsEnclosedInQuotes = True
            ' フィールドの空白トリム設定
            parser.TrimWhiteSpace = False

            Dim data() As String
            'ここのif文では、DataTableに必要なカラムを追加するために最初に1行だけ読み込んでいます。
            'データがあるか確認します。
            If Not parser.EndOfData Then
                'CSVファイルから1行読み取ります。
                data = parser.ReadFields()
                'カラムの数を取得します。
                Dim cols As Integer = data.Length
                If hasHeader Then
                    For i As Integer = 0 To cols - 1 Step 1
                        dt.Columns.Add(New DataColumn(data(i)))
                    Next i
                Else
                    For i As Integer = 0 To cols - 1 Step 1
                        'カラム名にダミーを設定します。
                        dt.Columns.Add(New DataColumn())
                    Next i
                    'DataTableに追加するための新規行を取得します。
                    Dim row As DataRow = dt.NewRow()
                    For i As Integer = 0 To cols - 1 Step 1
                        'カラムの数だけデータをうつします。
                        row(i) = data(i)
                    Next i
                    'DataTableに追加します。
                    dt.Rows.Add(row)
                End If
            End If

            'output
            While Not parser.EndOfData
                data = parser.ReadFields()
                Dim row As DataRow = dt.NewRow()
                For i As Integer = 0 To dt.Columns.Count - 1 Step 1
                    row(i) = data(i)
                    Console.Write(row(i) + vbTab)
                Next i
                Console.WriteLine()
                dt.Rows.Add(row)
            End While
        End Using
        Return dt
    End Function
End Class
