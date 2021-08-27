Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmAddExpType
    Dim cnn As New DataConn.Conn

    Private Sub frmAddExpType_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmAddExpType_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmAddExpType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        ''==============================
    End Sub

    Private Sub frmAddExpType_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Account Name...!")
            txtName.Focus()
            Exit Sub
        End If
        '---------------
        Try

            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from tblExpType Where ExpType=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            If lblID.Text <> "" Then
                strSQL = "UPDATE tblExpType Set ExpType=" & cnn.SQLquote(UCase(txtName.Text)) & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) _
                & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' Where ID=" & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Expense Type", "Edit", lblID.Text, UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO tblExpType (ExpType, CreatedBy, CreatedDate) VALUES (" & cnn.SQLquote(UCase(txtName.Text)) & "," _
                & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Expense Type", "Add", 0, UCase(txtName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = ""
                txtName.Text = ""
                txtName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub
End Class