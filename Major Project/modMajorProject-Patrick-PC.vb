
Module MajorProject

    Sub BinarySearch(ByVal SearchValue As String, ByVal list As ArrayList)
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
            'action
        Else
            'action
        End If
    End Sub

    Sub BubbleSort(ByVal list As ArrayList)
        Dim firstindex As Integer = 0
        Dim lastindex As Integer = list.Count - 1

        While lastindex > firstindex
            Dim currentindex As Integer = 0
            While currentindex < lastindex
                If list(currentindex) > list(currentindex + 1) Then
                    Dim temp As String = list(currentindex)
                    list(currentindex) = list(currentindex + 1)
                    list(currentindex + 1) = temp
                End If
                currentindex += 1
            End While
            lastindex -= 1
        End While

    End Sub

    Sub SelectionSort(ByVal list As ArrayList)
        Dim firstindex As Integer = 0
        Dim lastindex As Integer = list.Count - 1
        Dim endunsortedindex As Integer = firstindex

        While endunsortedindex < lastindex
            Dim currentindex As Integer = endunsortedindex
            Dim smallestelement As String = list(currentindex)
            Dim smallestindex As Integer = currentindex
            While currentindex < lastindex
                currentindex += 1
                If list(currentindex) < smallestelement Then
                    smallestelement = list(currentindex)
                    smallestindex = currentindex
                End If
            End While
            Dim temp As String = list(smallestindex)
            list(smallestindex) = list(endunsortedindex)
            list(endunsortedindex) = temp
            endunsortedindex += 1
        End While

    End Sub

    Sub InsertionSort(ByVal list As ArrayList)
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

    Sub deleteLinefromFile(ByVal line As Integer, ByVal path As String)

        Dim FileAddress As String = path
        Dim TheFileLines As New List(Of String)
        TheFileLines.AddRange(System.IO.File.ReadAllLines(FileAddress))
        ' if line is beyond end of list the exit sub
        If line >= TheFileLines.Count Then Exit Sub
        TheFileLines.RemoveAt(line)
        System.IO.File.WriteAllLines(FileAddress, TheFileLines.ToArray)
    End Sub

End Module