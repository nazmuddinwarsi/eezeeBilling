Public Class Splash

    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1 '2000
        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        frmLogin.Show()
        Me.Hide()
        Timer1.Enabled = False
    End Sub
End Class