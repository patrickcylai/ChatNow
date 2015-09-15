Public Class Options

    Private Sub btnDeleteAcc_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteAcc.Click
        Try
            deleteUserfromFile(CurrentUser, LoginPath)
            MessageBox.Show("Account Successfully Deleted!", "Account Deletion", MessageBoxButton.OK, MessageBoxImage.Information)
        Catch ex As Exception
            MessageBox.Show("Account was not successfully Deleted!", "Account Deletion", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
End Class
