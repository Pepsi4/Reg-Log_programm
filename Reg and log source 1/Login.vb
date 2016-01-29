Imports MySql.Data.MySqlClient
Public Class Login
    Private Sub Label2_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    Public Sub login2()
        check = "false"

        connect()
        Try
            cmd.Connection = conn
            cmd.CommandText = "SELECT name FROM users"
            reader = cmd.ExecuteReader()

            While reader.Read()
                If reader.GetValue(0) = login Then
                    check = "true"
                End If
            End While
            reader.Close()
        Catch ex As Exception
            MsgBox("error")
        End Try
        If check = "true" Then
            Try
                cmd.Connection = conn
                cmd.CommandText = "SELECT pass FROM users WHERE name = " & "'" & login & "'"
                reader = cmd.ExecuteReader()
                While reader.Read()
                    If reader.GetValue(0) = pass Then
                        MsgBox("YEEEEEEE")   'Esli login + pass pravilnii
                    End If
                End While
            Catch ex As Exception
                MsgBox("error")
            End Try
        End If
    End Sub
    Public Sub connect()
        Try
            conn.Open()
        Catch
            MsgBox("Connection error.")
        End Try
    End Sub
    Dim login As String
    Dim pass As String
    Dim numberLogin As String
    Dim numberPass As String
    Dim error1 As String
    Dim chars As String = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890" 'Dopustimoe
    Dim conn As New MySqlConnection("Server=db4free.net;User id=u814333431;password=tester;database=u814333431")
    Dim check As String
    Dim cmd As New MySqlCommand
    Dim cmd2 As New MySqlCommand
    Dim reader As MySqlDataReader
    Dim reader2 As MySqlDataReader
    Public Sub LoginNoError()
        ErrorProvider1.SetError(TextBox1, "")
    End Sub

    Public Sub PassNoError()
        ErrorProvider2.SetError(TextBox3, "")
    End Sub

    Public Sub loginError()
        ErrorProvider1.SetError(TextBox1, "Error")
    End Sub

    Public Sub passError()
        ErrorProvider2.SetError(TextBox3, "Error")
    End Sub

    Public Sub LongPass()
        numberPass = "true"

        Dim LongPass As Integer = Len(TextBox3.Text) ' dlina parol9
        Do Until LongPass = 0
            Dim passtest As String = Mid(pass, LongPass, 1)
            If InStr(chars, passtest) Then
            Else
                numberPass = "false"
            End If
            LongPass = LongPass - 1
        Loop
        If pass = "" Then
            numberPass = "false"
        End If
    End Sub

    Public Sub LongLogin()
        numberLogin = "true"

        Dim LongLogin As Integer = Len(TextBox1.Text) ' Dlina logina
        Do Until LongLogin = 0
            Dim logintest As String = Mid(login, LongLogin, 1) 'Pokazivaet 1 znak
            If InStr(chars, logintest) Then ' ichim

            Else
                numberLogin = "false"
            End If
            LongLogin = LongLogin - 1
        Loop
        If login = "" Then
            numberLogin = "false"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        pass = TextBox3.Text ' pass
        login = TextBox1.Text ' login
        Dim error1 As String = ""
        LongLogin()
        LongPass()
        If login <> "" Or pass <> "" Then
            If numberLogin = "true" And numberPass = "true" Then  '2oi etap proverki
                LoginNoError()
                PassNoError()
                login2()
            End If

            If numberLogin = "true" And numberPass = "false" Then ' +-
                error1 = " Password"
                LoginNoError()
                passError()
            End If

            If numberLogin = "false" And numberPass = "false" Then
                error1 = " Login and Password"
                passError()
                loginError()
            End If

            If numberLogin = "false" And numberPass = "true" Then ' -+
                error1 = " Login"
                loginError()
                PassNoError()
            End If

        Else
            error1 = " Login and Password"
            passError()
            loginError()
        End If
        If error1 <> "" Then
            MsgBox("Something wrong with" & error1)
        End If
        conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Sign_up.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
    End Sub
End Class
