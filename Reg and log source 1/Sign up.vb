Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.IO
Public Class Sign_up
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Login.Show()
        Me.Close()
    End Sub

    Private Sub Sign_up_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reg As HttpWebRequest = HttpWebRequest.Create("http://icanhazip.com")
        Dim res As HttpWebResponse = reg.GetResponse
        Dim stream As Stream = res.GetResponseStream
        Dim reader As New StreamReader(stream)
        ip = reader.ReadToEnd
    End Sub
    Public Sub check()
        If testLogin = "false" Or testlogin12 = "false" Then
            ErrorProvider1.SetError(TextBox1, "Error")
            error1 = "Login"
        Else
            ErrorProvider1.SetError(TextBox1, "")
        End If

        If testPass1 = "false" Or testpass12 = "false" Then
            ErrorProvider2.SetError(TextBox2, "Error")
            If error1 = "Login" Then
                error1 = error1 + " and Password"
            Else
                error1 = error1 + "Password"
            End If
        Else
            ErrorProvider2.SetError(TextBox2, "")
        End If

        If testPass2 = "false" Or testpass22 = "false" Then
            ErrorProvider3.SetError(TextBox3, "Error")
            If error1 = "Login" Then
                error1 = error1 + " and Password"
            End If
            If error1 = "" Then
                error1 = "Password"
            End If
        Else
            ErrorProvider3.SetError(TextBox3, "")

        End If


        If testLogin = "" And testlogin12 = "" And testPass1 = "" And testpass12 = "" And testPass2 = "" And testpass22 = "" Then
            If pass1 <> pass2 Then
                ErrorProvider3.SetError(TextBox3, "Error")
                ErrorProvider2.SetError(TextBox2, "Error")
                error1 = ""
                error1 = "Passwords does not match"
            End If
        End If

        If error1 <> "" Then
            MsgBox("Something wrong with: " & error1)
        End If

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
    End Sub
    Dim pass3, pass2, pass1, login1, ip, hostname, error1, testlogin12, testpass12, testpass22 As String
    Dim chars As String = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890" 'Dopustimoe
    Dim MyLoginTest As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        testlogin12 = ""
        testpass12 = ""
        testpass22 = ""
        pass3 = ""

        login1 = TextBox1.Text
        pass1 = TextBox2.Text
        pass2 = TextBox3.Text

        If login1 = "" Then
            testlogin12 = "false"
        End If
        If pass1 = "" Then
            testpass12 = "false"
        End If
        If pass2 = "" Then
            testpass22 = "false"
        End If

        LongLogin1()
        LongPass1()
        LongPass2()
        check()
        MyLoginTest = "true"

        If error1 = "" Then
            Try
                connect()
                cmd.Connection = conn
                cmd.CommandText = "SELECT name FROM users"
                reader = cmd.ExecuteReader()
                While reader.Read
                    If reader.GetValue(0) = login1 Then
                        MyLoginTest = "false"
                    End If
                End While
                reader.Close()
            Catch
                MsgBox("Connection error1.")
            End Try


            If MyLoginTest = "false" Then
                MsgBox("Error. Try another login.")
            Else
                Try

                    cmd.CommandText = "INSERT INTO users(name,pass,ip) VALUES " & "('" & login1 & "','" & pass1 & "','" & ip & "')"
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("You have successfully registered")
                    Login.Show()
                    Me.Close()

                Catch
                    MsgBox("Connection error2.")
                End Try


            End If
        End If

        conn.Close()
        error1 = ""
    End Sub
    Public Sub LongLogin1()
        testLogin = ""
        numberlogin = Len(TextBox1.Text)
        Do Until numberlogin = 0
            Dim longlogin1 = Mid(login1, numberlogin, 1)
            If InStr(chars, longlogin1) Then
            Else
                testLogin = "false"
            End If
            numberlogin = numberlogin - 1
        Loop

    End Sub
    Dim numberlogin As Integer '2-a9 proverka
    Dim numberPass1 As Integer '2-a9 proverka
    Dim numberPass2 As Integer '2-a9 proverka
    Dim testLogin As String '2-a9 proverka
    Dim testPass1 As String '2-a9 proverka
    Dim testPass2 As String '2-a9 proverka
    Public Sub LongPass1()
        testPass1 = ""
        numberPass1 = Len(TextBox2.Text)
        Do Until numberPass1 = 0
            Dim longpass1 = Mid(pass1, numberPass1, 1)
            If InStr(chars, longpass1) Then
            Else
                testPass1 = "false"
            End If
            numberPass1 = numberPass1 - 1
        Loop

    End Sub
    Public Sub LongPass2()
        testPass2 = ""
        numberPass2 = Len(TextBox3.Text)
        Do Until numberPass2 = 0
            Dim longpass2 = Mid(pass2, numberPass2, 1)
            If InStr(chars, longpass2) Then
            Else
                testPass2 = "false"
            End If
            numberPass2 = numberPass2 - 1
        Loop

    End Sub
    Dim conn As New MySqlConnection(("Server=db4free.net;User id=u814333431;password=tester;database=u814333431"))
    Dim cmd As New MySqlCommand
    Dim reader As MySqlDataReader

    Public Sub connect()
        Try
            conn.Open()
        Catch
            MsgBox("Connection error")
        End Try
    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
    End Sub
End Class