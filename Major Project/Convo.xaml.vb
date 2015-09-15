Imports System.Net.Sockets
Imports System.IO
Imports System.Threading
Imports System.Windows.Threading
Imports System.Text
Imports System.Xml

Class Convo
    'Declarations of network variables
    Dim Listener As New TcpListener(65535)
    Dim timer As New DispatcherTimer
    Dim Client As New TcpClient
    Dim Message As String = ""
    Private Const port As UInteger = 65535

    'Start TcpListener
    Sub Listening()
        Listener.Start()
    End Sub

    Private Sub btnSend_Click(sender As Object, e As RoutedEventArgs) Handles btnSend.Click

        Try
            If txtMessage.Text <> "" And txtIPaddr.Text <> "" Then
                Client = New TcpClient(txtIPaddr.Text.Trim, port)
                'Stores the message to a variable and adding the current username
                Dim SendMsg As String = CurrentUser + ": " + txtMessage.Text.Trim

                'Send Message to IP
                Dim Writer As New StreamWriter(Client.GetStream())
                Writer.Write(SendMsg)
                Writer.Flush()

                'Adds Sent message to the conversation textbox
                txtConvo.AppendText(SendMsg + Environment.NewLine)
                'Resets message textbox to nothing
                txtMessage.Text = ""
                'Scrolls textbox to the end
                txtConvo.ScrollToEnd()
            Else
                MessageBox.Show("Please enter an IP address to connect to first beofre messaging.", "No IP", MessageBoxButton.OK, MessageBoxImage.Error)
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("There was a problem sending the message.", "Message Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub Convo_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        'Create separate thread for listening for messages
        Dim ListThread As New Thread(New ThreadStart(AddressOf Listening))
        ListThread.Start()

        txtMessage.Text = ""

        'Set settings for timer as a Dispatcher Timer
        timer.Interval = TimeSpan.FromMilliseconds(1)
        AddHandler timer.Tick, AddressOf timer_task

        timer.Start()

        'Enable the timer
        timer.IsEnabled = True
        btnDisconnectfromIP.IsEnabled = False
    End Sub

    Sub timer_task() 'Task executed whenever the Dispatcher timer ticks
        If Listener.Pending = True Then
            Message = ""
            Client = Listener.AcceptTcpClient()

            'Concatenates each character into the original message
            Dim Reader As New StreamReader(Client.GetStream())
            While Reader.Peek > -1
                Message = Message + Convert.ToChar(Reader.Read()).ToString
            End While

            If Message = "" Then
                Exit Sub
            End If

            txtConvo.AppendText(Message + Environment.NewLine)
            txtConvo.ScrollToEnd()

        End If
    End Sub

    Private Sub btnConnecttoIP_Click(sender As Object, e As RoutedEventArgs) Handles btnConnecttoIP.Click 'Connects to the IP address entered by the user
        If txtIPaddr.Text = "" Then
            MessageBox.Show("Please enter an IP Address to connect to.", "No IP Address", MessageBoxButton.OK, MessageBoxImage.None)
            Exit Sub
        End If

        lblContactName.Text = ""
        txtConvo.SelectAll()
        txtConvo.Selection.Text = ""

        'Attempts to connect to the specified IP address
        Dim connectioncheck As New TcpClient()
        Try
            connectedIP = txtIPaddr.Text.Trim
            connectioncheck = New TcpClient(connectedIP, port)
            txtIPaddr.IsEnabled = False
            btnConnecttoIP.IsEnabled = False
            btnDisconnectfromIP.IsEnabled = True

            lblContactName.Text = connectedIP

            Dim Writer As New StreamWriter(connectioncheck.GetStream())
            Writer.Write(CurrentUser + " has Connected!" + " from " + GetIPAddress() + Environment.NewLine)
            Writer.Flush()
            connectioncheck.Close()
        Catch ex As Exception
            MessageBox.Show("Specified IP address is Offline or doesn't exist", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Information)
            connectioncheck.Close()
        End Try

    End Sub

    Sub ShutOff()
        Listener.Stop()
        timer.IsEnabled = False
    End Sub


    Private Sub Convo_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        ShutOff()
    End Sub

    Private Sub btnDisconnectfromIP_Click(sender As Object, e As RoutedEventArgs) Handles btnDisconnectfromIP.Click 'Close connections to the client computer
        Try
            'Sends a message telling the other user has been disconnected
            Client = New TcpClient(connectedIP, port)
            Dim Writer As New StreamWriter(Client.GetStream())
            Writer.Write(CurrentUser + " has Disconnected!" + Environment.NewLine)
            Writer.Flush()

            txtMessage.Text = ""
            txtConvo.ScrollToEnd()
        Catch ex As Exception
            MessageBox.Show("There was a problem sending the message.", "Message Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        'Closes all connections and resets all controls
        btnConnecttoIP.IsEnabled = True
        txtIPaddr.IsEnabled = True
        btnDisconnectfromIP.IsEnabled = False

        Client.Close()
        'ShutOff()
    End Sub

    'Private Sub btnSaveContact_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveContact.Click
    '    'Opens Save Contact Window

    '    If txtIPaddr.Text = "" Then
    '        MessageBox.Show("No Contact to Save.", "No Contact", MessageBoxButton.OK, MessageBoxImage.Information)
    '        Exit Sub
    '    End If

    '    connectedIP = txtIPaddr.Text.Trim
    '    Dim savecontactform As New Save_Contact
    '    savecontactform.Show()

    'End Sub

End Class
