Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Imports System.Text
Imports System.Runtime.InteropServices

Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Win32.AllocConsole()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim path As String = Application.StartupPath & "\sample.csv"

        Dim dt As DataTable = ReadCSV.ReadCSV(path)

        Me.DataGridView1.DataSource = dt
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                Console.Write(dt.Rows(i).Item(k).ToString & vbTab)
            Next
            Console.WriteLine()
        Next


        CreateCSVfile(path, "4", "me")

    End Sub


    Private Sub CreateCSVfile(ByVal _strCustomerCSVPath As String, ByVal _ID As String, ByVal _Name As String)
        Try
            Dim stLine As String = ""
            Dim objWriter As IO.StreamWriter = IO.File.AppendText(_strCustomerCSVPath)
            If IO.File.Exists(_strCustomerCSVPath) Then
                objWriter.Write(_ID & ",")
                objWriter.Write(_Name & ",")


                'If value contains comma in the value then you have to perform this opertions


                objWriter.Write(Environment.NewLine)
            End If
            objWriter.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class

Public Class Win32
    <DllImport("kernel32.dll")> Public Shared Function AllocConsole() As Boolean

    End Function
    <DllImport("kernel32.dll")> Public Shared Function FreeConsole() As Boolean

    End Function

End Class


