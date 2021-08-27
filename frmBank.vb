Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmBank
    Dim cnn As New DataConn.Conn

    Private Sub frmBank_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmBank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        ''==============================
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Display Name...!")
            txtName.Focus()
            Exit Sub
        End If
        '---------------
        Try

            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from tblBank Where DisplayName=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            Dim bBool As Boolean = False
            If lblID.Text <> "" Then
                strSQL = "UPDATE tblBank Set DisplayName=" & cnn.SQLquote(UCase(txtName.Text)) & ", AcName=" & cnn.SQLquote(UCase(txtAcName.Text))
                strSQL = strSQL & ", AcNo=" & cnn.SQLquote(txtAcNo.Text) & ", BankName=" & cnn.SQLquote(txtBank.Text) & ", BranchName=" & cnn.SQLquote(txtBranch.Text)
                strSQL = strSQL & " Where ID = " & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Bank", "Edit", lblID.Text, lblType.Text & "-" & UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO tblBank (DisplayName, AcName, AcNo, BankName, BranchName, OpeningAmount, CurBalance) VALUES ("
                strSQL = strSQL & cnn.SQLquote(UCase(txtName.Text)) & "," & cnn.SQLquote(UCase(txtAcName.Text)) & "," & cnn.SQLquote(txtAcNo.Text)
                strSQL = strSQL & "," & cnn.SQLquote(txtBank.Text) & "," & cnn.SQLquote(txtBranch.Text) & "," & Val(txtOpening.Text) & "," & Val(txtOpening.Text) & ")"
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Bank", "Add", 0, lblType.Text & "-" & UCase(txtName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = "" : txtName.Text = "" : txtAcName.Text = "" : txtAcNo.Text = "" : txtBank.Text = "" : txtBranch.Text = ""
                txtOpening.Text = ""
                txtName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub
End Class