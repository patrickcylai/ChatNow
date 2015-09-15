Imports System.Net
Imports System.Windows.Threading
Imports System.IO

Class MainWindow
    Public contactIP As New ArrayList
    Public contactName As New ArrayList

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Checks whether the computer is online
        Dim connected As Boolean = CheckConnection()
        If connected = False Then
            lblStatus.Foreground = New SolidColorBrush(Colors.Red)
            lblStatus.Text = "Offline"
        Else
            lblStatus.Foreground = New SolidColorBrush(Colors.Green)
            lblStatus.Text = "Online"
        End If

        'LoadContacts()
        tbitemConvo.IsSelected = True

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As RoutedEventArgs) Handles btnLogout.Click
        Dim windowLogin As New Login
        windowLogin.Show()
        Me.Close()
    End Sub

    'Private Sub LoadContacts()
    '    'lstboxContacts.Items.Clear()
    '    'Loads the contacts from the Contacts.txt into the listbox "Contacts"
    '    Dim readContacts As New StreamReader(ContactsPath)

    '    Dim row(0) As String

    '    Do While readContacts.Peek <> -1
    '        row = readContacts.ReadLine().Split(CChar(","))
    '        contactIP.Add(row(0))
    '        contactName.Add(row(1))
    '    Loop

    '    readContacts.Close()

    '    For count = 0 To contactIP.Count - 1
    '        lstboxContacts.Items.Add(contactName(count) + " (" + contactIP(count) + ")")
    '    Next

    'End Sub

    Private Sub MainWindow_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        Dim Closing As Convo = New Convo
        Closing.ShutOff()
    End Sub

    Private Sub btnOptions_Click(sender As Object, e As RoutedEventArgs) Handles btnOptions.Click
        Dim formOptions As New Options
        formOptions.Show()
    End Sub

    'Private Sub lstboxContacts_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles lstboxContacts.MouseDoubleClick
    '    Dim formConvo As New Convo
    '    formConvo.txtIPaddr.Text = contactIP(lstboxContacts.SelectedIndex)
    '    formConvo.lblContactName.Text = contactName(lstboxContacts.SelectedIndex)

    '    Dim tabitem As New TabItem
    '    tabitem.Header = contactName(lstboxContacts.SelectedIndex)
    '    tbctrlMain.Items.Add(tabitem)
    '    tabitem.Content = formConvo
    'End Sub
End Class
