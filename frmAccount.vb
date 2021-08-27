Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmAccount
    Dim cnn As New DataConn.Conn

    Private Sub frmAccount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        If lblType.Text = "SUPPLIER" Then
            lblCaption.Text = "Supplier Master"
        Else
            lblCaption.Text = "Customer Master"
        End If
        ''==============================
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Account Name...!")
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtCPerson.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Person Name...!")
            txtCPerson.Focus()
            Exit Sub
        End If
        '---------------
        Try
            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from tblAccount Where tType='" & lblType.Text & "' AND AccountName=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            Dim bBool As Boolean = False
            bBool = CheckEntry()
            If lblID.Text <> "" Then
                strSQL = "UPDATE tblAccount Set AccountName=" & cnn.SQLquote(UCase(txtName.Text)) & ", CPerson=" & cnn.SQLquote(UCase(txtCPerson.Text))
                strSQL = strSQL & ", Address1=" & cnn.SQLquote(txtAddress1.Text) & ", Address2=" & cnn.SQLquote(txtAddress2.Text) & ", City=" & cnn.SQLquote(txtCity.Text)
                strSQL = strSQL & ", State=" & cnn.SQLquote(txtState.Text) & ", PIN=" & cnn.SQLquote(txtPIN.Text) & ", EmailID=" & cnn.SQLquote(txtEmail.Text)
                strSQL = strSQL & ", Mobile=" & cnn.SQLquote(txtMobile.Text) & ", Phone=" & cnn.SQLquote(txtPhone.Text) & ", TINNo=" & cnn.SQLquote(txtTIN.Text)
                strSQL = strSQL & ", CGST=" & Val(chkCGST.Checked) & ", SGST=" & Val(chkSGST.Checked) & ", IGST=" & Val(chkIGST.Checked)
                strSQL = strSQL & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "'"
                If bBool = False Then
                    strSQL = strSQL & ", Opening = " & Val(txtOpening.Text)
                End If
                strSQL = strSQL & " Where ID = " & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Accounts", "Edit", lblID.Text, lblType.Text & "-" & UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO tblAccount (tType, AccountName, CPerson, Address1, Address2, City, State, PIN, EmailID, Mobile, Phone, TINNo, CreatedBy, CreatedDate, Opening, CGST, SGST, IGST) VALUES ("
                strSQL = strSQL & "'" & lblType.Text & "'," & cnn.SQLquote(UCase(txtName.Text)) & "," & cnn.SQLquote(UCase(txtCPerson.Text)) & "," & cnn.SQLquote(txtAddress1.Text)
                strSQL = strSQL & "," & cnn.SQLquote(txtAddress2.Text) & "," & cnn.SQLquote(txtCity.Text) & "," & cnn.SQLquote(txtState.Text) & "," & cnn.SQLquote(txtPIN.Text)
                strSQL = strSQL & "," & cnn.SQLquote(txtEmail.Text) & "," & cnn.SQLquote(txtMobile.Text) & "," & cnn.SQLquote(txtPhone.Text) & "," & cnn.SQLquote(txtTIN.Text) _
                & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "'," & Val(txtOpening.Text)
                strSQL = strSQL & "," & Val(chkCGST.Checked) & "," & Val(chkSGST.Checked) & "," & Val(chkIGST.Checked) & ")"
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Accounts", "Add", 0, lblType.Text & "-" & UCase(txtName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = "" : txtName.Text = "" : txtCPerson.Text = "" : txtAddress1.Text = "" : txtAddress2.Text = "" : txtCity.Text = ""
                txtState.Text = "" : txtPIN.Text = "" : txtEmail.Text = "" : txtMobile.Text = "" : txtPhone.Text = "" : txtTIN.Text = "" : txtOpening.Text = ""
                chkCGST.Checked = False : chkSGST.Checked = False : chkIGST.Checked = False
                txtName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub
    Private Function CheckEntry() As Boolean
        Dim strSQL As String = ""
        strSQL = "Select ID from Payment Where AccountID=" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            Return True
        End If
        strSQL = "Select ID from Receipt Where AccountID=" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            Return True
        End If
        strSQL = "Select ID from SaleHeader Where AccountID=" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            Return True
        End If
        strSQL = "Select ID from PurchaseHeader Where AccountID=" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            Return True
        End If
        strSQL = "Select ID from SaleReturnHeader Where AccountID=" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            Return True
        End If
        strSQL = "Select ID from PurchaseReturnHeader Where AccountID=" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            Return True
        End If
        Return False
    End Function
End Class