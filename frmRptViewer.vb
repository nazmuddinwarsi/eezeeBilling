Public Class frmRptViewer

    Private Sub frmRptViewer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.P Then
            'here goes your code to print the report
            CRViewer.PrintReport()
        End If
    End Sub
End Class