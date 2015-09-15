Imports System.IO

Public Class Login

    '==============
    'TYPE STRUCTURE
    '==============
    Structure NewLogin
        Dim Username As String
        Dim Password As String
    End Structure

    'Declare variable as object: NewLogin
    Dim inLogin As NewLogin


    '=========================================================================================================>
    'Formatting the controls in the login form
    '=========================================================================================================>
    Private Sub txtUsername_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtUsername.GotFocus
        If txtUsername.Text = "Username" Then
            txtUsername.Text = ""
            txtUsername.FontStyle = FontStyles.Normal
        End If
    End Sub

    Private Sub txtUsername_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtUsername.LostFocus
        If txtUsername.Text = "" Then
            txtUsername.FontStyle = FontStyles.Italic
            txtUsername.Text = "Username"
        End If
    End Sub
    '==========================================================================================================>

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs) Handles btnLogin.Click
        Dim authen As Boolean = False

        If txtUsername.Text <> "" And txtUsername.Text <> "Username" Then
            'Trim textbox text to remove trailing spaces
            inLogin.Username = txtUsername.Text.Trim
            inLogin.Password = txtPassword.Password.Trim

            authen = AuthenticateLogin(inLogin.Username, inLogin.Password)
            If authen = True Then
                'Open the mainwindow
                Dim mainWindow As New MainWindow
                mainWindow.Show()

                'Set global variable to username for future references
                CurrentUser = inLogin.Username
                Me.Close()
            Else
                MessageBox.Show("Either your username or password was incorrect please try again.", "Login Incorrect", _
                                MessageBoxButton.OK, MessageBoxImage.Asterisk)
                Exit Sub
            End If
        Else
            MessageBox.Show("Please enter a username and login", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Sub lblCreateNewAccount_Click(sender As Object, e As RoutedEventArgs) Handles lblCreateNewAccount.Click

        inLogin.Username = ""
        inLogin.Password = ""

        If txtUsername.Text <> "" Or txtPassword.Password <> "" Or txtUsername.Text <> "Username" Then
            inLogin.Username = txtUsername.Text.Trim
            inLogin.Password = txtPassword.Password.Trim

            'check whether already exists
            Dim check As Boolean = False
            check = CheckAccountExist(inLogin.Username)

            If check = False Then

                Dim writeLoginBase As New StreamWriter(LoginPath, True)

                writeLoginBase.WriteLine(inLogin.Username + "," + inLogin.Password)
                writeLoginBase.Close()

                MessageBox.Show("New account has been created.", "Account Created", MessageBoxButton.OK, MessageBoxImage.Information)
            Else
                MessageBox.Show("Account already exists.", "Account Exists", MessageBoxButton.OK, MessageBoxImage.Error)
            End If
        End If

    End Sub

    Function AuthenticateLogin(ByVal username As String, ByVal password As String)
        'Checks whether passed username and password any accounts in the Accounts file

        Dim readLoginBase As New StreamReader(LoginPath)

        Dim User As New ArrayList
        Dim Pass As New ArrayList
        Dim row(1) As String

        Do While readLoginBase.Peek <> -1
            row = readLoginBase.ReadLine().Split(CChar(","))
            User.Add(row(0))
            Pass.Add(row(1))
        Loop

        readLoginBase.Close()

        For count = 0 To User.Count - 1
            If username = User(count) And password = Pass(count) Then
                Return True
            End If
        Next

        Return False
    End Function

    Function CheckAccountExist(ByVal username As String)
        'Checks whether passed Username already exists

        Dim readLoginBase As New StreamReader(LoginPath)

        Dim Users As New ArrayList
        Dim Pass As New ArrayList
        Dim row(0) As String
        Dim Exists As Boolean = False

        Do While readLoginBase.Peek <> -1
            row = readLoginBase.ReadLine().Split(CChar(","))
            Users.Add(row(0))
        Loop

        readLoginBase.Close()

        If Users.Count = 0 Then
            Return False
        End If
        'SORTS AND SEARCHES THE PASSED ARRAY AND GIVEN SEARCH VALUE
        InsertionSort(Users)
        Exists = BinarySearch(username, Users)

        Return Exists
    End Function

End Class
