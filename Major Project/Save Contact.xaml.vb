Imports System.IO

Public Class Save_Contact

    Private Sub Save_Contact_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtIP.Text = connectedIP
        txtContactName.Text = connectedIP
    End Sub

    Private Sub btnSaveContact_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveContact.Click
        'check whether contact already exists
        Dim check As Boolean = False
        check = CheckContactExist(connectedIP)

        If check = False Then
            Dim writeContactBase As New StreamWriter(ContactsPath, True)

            writeContactBase.WriteLine(txtIP.Text.Trim + "," + txtContactName.Text.Trim)
            writeContactBase.Close()

            MessageBox.Show("Contact has been saved.", "Contact Saved", MessageBoxButton.OK, MessageBoxImage.Information)
            Me.Close()
        Else
            MessageBox.Show("Contact already exists.", "Contact Exists", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub


    Function CheckContactExist(ByVal contact As String) 'Checks whether passed Contact already exists

        Dim readContacts As New StreamReader(ContactsPath)

        Dim IP As New ArrayList
        Dim Name As New ArrayList
        Dim row(0) As String

        'Reads in the contacts in the Contacts.txt file
        Do While readContacts.Peek <> -1
            row = readContacts.ReadLine().Split(CChar(","))
            IP.Add(row(0))
        Loop

        readContacts.Close()

        For count = 1 To IP.Count - 1
            If contact = IP(count) Then
                Return True
            End If
        Next

        Return False
    End Function
End Class
