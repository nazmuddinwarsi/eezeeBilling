
Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmLogin
    Dim cnn As New DataConn.Conn

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Application.Exit()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, BranchName from BranchMaster ORDER BY BranchName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbBranch.DisplayMember = "BranchName"
        cmbBranch.ValueMember = "ID"
        cmbBranch.DataSource = ds.Tables(0)
        cmbBranch.SelectedIndex = 0
        'txtUserName.Text = cnn.Encrypt("7") & "-" & cnn.Encrypt("2018")
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        
        If Trim(cmbBranch.Text) = "" Then
            MessageBox.Show("Please Select Company Name...!", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbBranch.Focus()
            Exit Sub
        End If
        If Val(cmbBranch.SelectedValue) = 0 Then
            MessageBox.Show("Please Select Company Name from List ...!", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbBranch.Focus()
            Exit Sub
        End If
        If txtUserName.Text = "" Then
            MessageBox.Show("Enter User Name...!", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            Exit Sub
        End If
        If txtPassword.Text = "" Then
            MessageBox.Show("Enter Password...!", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtPassword.Focus()
            Exit Sub
        End If
        Dim strSQL As String
        '------------------------------------------
        'Dim iCheckDate As Integer
        'iCheckDate = cnn.CheckExpDate(Date.Today(), Val(cmbCompany.SelectedValue), Val(cmbBranch.SelectedValue))
        'If iCheckDate < 0 Then
        '    cnn.ErrMsgBox("Your Software has been expired." & vbCrLf & "Please contact your Software Provider  !")
        '    Exit Sub
        'ElseIf iCheckDate = 1 Then
        '    cnn.ErrMsgBox("There is Problem in Software configuration." & vbCrLf & " Please contact your Software Provider  !")
        '    Exit Sub
        'ElseIf iCheckDate = 2 Then
        '    cnn.InfoMsgBox("Software is going to be expire within 10 Days." & vbCrLf & " Please contact your Software Provider  !")
        '    'Exit Sub
        'End If
        '------------------------------------------
        strSQL = "select ID, fldPassword, AccessType, Active from Users where fldUserName=" & cnn.SQLquote(txtUserName.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        cnn.OpenConn()
        Dim rdr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If rdr.Read Then
            If rdr.Item("Active") = "N" Then
                rdr.Close()
                MessageBox.Show("User has been Suspended, Please Contact Administrator  !", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUserName.Focus()
                Exit Sub
            End If
            If txtPassword.Text = rdr.Item("fldPassword") Then
                MDI.lblMasterCompanyID.Text = Val(cmbCompany.SelectedValue)
                MDI.lblMasterBranchID.Text = Val(cmbBranch.SelectedValue)
                MDI.lblMasterCompanyName.Text = cmbCompany.Text
                MDI.lblMasterBranchName.Text = cmbBranch.Text
                MDI.lblAccessType.Text = rdr.Item("AccessType")
                MDI.lblUserLoginID.Text = rdr.Item("ID")
                rdr.Close()
                MDI.Text = "WELCOME TO " & cmbBranch.Text
                MDI.Show()
                Me.Hide()
            Else
                rdr.Close()
                MessageBox.Show("Invalid Password, try again!", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Focus()
                Exit Sub
            End If
        Else
            rdr.Close()
            MessageBox.Show("User does not exist contact system administrator...!", cnn.msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
        GetBranch()
    End Sub
    Private Sub GetBranch()
        cmbBranch.DataSource = Nothing
        If Val(cmbCompany.SelectedValue) > 0 Then
            Dim strSQL As String
            Dim da As New OdbcDataAdapter
            Dim ds As New DataSet

            strSQL = "Select ID, BranchName from BranchMaster Where CompanyID=" & Val(cmbCompany.SelectedValue) & " ORDER BY BranchName ASC"
            da = New OdbcDataAdapter(strSQL, cnn.cnn)
            ds = New DataSet()
            da.Fill(ds)
            cmbBranch.DisplayMember = "BranchName"
            cmbBranch.ValueMember = "ID"
            cmbBranch.DataSource = ds.Tables(0)
            cmbBranch.SelectedIndex = -1
        End If
    End Sub

    Private Sub cmbCompany_TextChanged(sender As Object, e As EventArgs) Handles cmbCompany.TextChanged
        GetBranch()
    End Sub
End Class