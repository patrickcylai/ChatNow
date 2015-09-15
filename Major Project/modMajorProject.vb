Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.IO
Imports System.Text

Module MajorProject
    Public CurrentUser As String
    Public ReceivedMsg As String
    Public LoginPath As String = AppDomain.CurrentDomain.BaseDirectory + "\LoginBase.txt"
    Public ContactsPath As String = AppDomain.CurrentDomain.BaseDirectory + "\Contacts.txt"
    Public connectedIP As String = ""

    '=============
    'Binary Search
    '=============
    Function BinarySearch(ByVal SearchValue As String, ByVal list As ArrayList)
        Dim Lower As Integer = 0
        Dim Upper As Integer = list.Count - 1
        Dim FoundIt As Boolean = False
        Dim Position As Integer

        Do
            Dim Middle As Integer = (Upper + Lower) / 2
            If SearchValue = list(Middle) Then
                FoundIt = True
                Position = Middle
            Else
                If SearchValue < list(Middle) Then
                    Upper = Middle - 1
                Else
                    Lower = Middle + 1
                End If
            End If
        Loop Until FoundIt Or Lower > Upper

        If FoundIt = True Then
            Return True
        Else
            Return False
        End If
    End Function

    '==============
    'Insertion Sort
    '==============
    Sub InsertionSort(ByRef list As ArrayList)
        Dim firstindex As Integer = 0
        Dim lastindex As Integer = list.Count - 1
        Dim i As Integer

        For count = firstindex To lastindex
            Dim temp As String = list(count)
            For i = count To 1 Step -1
                If list(i - 1) > temp Then
                    list(i) = list(i - 1)
                Else
                    Exit For
                End If
            Next
            list(i) = temp
        Next
    End Sub

    '==========================================
    'Deletes a specific value from any text file
    '==========================================
    Sub deleteUserfromFile(ByVal value As String, ByVal path As String)
        Dim FileAddress As String = path
        Dim TheFileLines As New List(Of String)
        TheFileLines.AddRange(System.IO.File.ReadAllLines(FileAddress))
        TheFileLines.Sort()
        Dim line As Integer = TheFileLines.BinarySearch(value) + 1
        ' if line is beyond end of list the exit sub
        If line >= TheFileLines.Count Then
            Exit Sub
        End If
        TheFileLines.RemoveAt(line)
        System.IO.File.WriteAllLines(FileAddress, TheFileLines.ToArray)
    End Sub

    '==================
    'Network Operations
    '==================
    Function CheckConnection()
        Dim connected As Boolean
        connected = My.Computer.Network.IsAvailable
        Return connected
    End Function

    Function GetIPAddress()

        Dim strHostName As String
        Dim strIPAddress As String

        strHostName = Dns.GetHostName()

        strIPAddress = Dns.GetHostByName(strHostName).AddressList(0).ToString()

        Return strIPAddress
    End Function

End Module