Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmAddEmp
    Dim cnn As New DataConn.Conn
    Dim strSQL As String

    Private Sub frmAddEmp_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmAddEmp_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmAddEmp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
    End Sub

    Private Sub frmAddEmp_Resize(sender As Object, e As EventArgs) Handles Me.Resize

    End Sub

    Private Sub txtAge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAge.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtAge_TextChanged(sender As Object, e As EventArgs) Handles txtAge.TextChanged

    End Sub

    Private Sub txtSalary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSalary.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtSalary_TextChanged(sender As Object, e As EventArgs) Handles txtSalary.TextChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Account Name...!")
            txtName.Focus()
            Exit Sub
        End If
        If Val(txtSalary.Text) <= 0 Then
            cnn.ErrMsgBox("Please Enter Salary...!")
            txtSalary.Focus()
            Exit Sub
        End If
        '---------------
        Try

            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from tblEmployee Where EmpName=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            If lblID.Text <> "" Then
                strSQL = "UPDATE tblEmployee Set EmpName=" & cnn.SQLquote(UCase(txtName.Text)) & ", FatherName=" & cnn.SQLquote(UCase(txtFatherName.Text))
                strSQL = strSQL & ", fldAddress=" & cnn.SQLquote(txtAddress.Text) & ", fldCity=" & cnn.SQLquote(txtCity.Text) & ", MobileNo=" & cnn.SQLquote(txtMobileNo.Text)
                strSQL = strSQL & ", Salary=" & Val(txtSalary.Text)
                If rbActive.Checked = True Then
                    strSQL = strSQL & ", Active = 1, LeavingDate = NULL, Reason = ''"
                Else
                    strSQL = strSQL & ",  Active = 0, LeavingDate = '" & cnn.GetDate(txtLeavingDate.Text) & "', Reason = " & cnn.SQLquote(txtReason.Text)
                End If
                strSQL = strSQL & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' Where ID=" & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Employee", "Edit", lblID.Text, UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO tblEmployee (EmpName, FatherName, Age, fldAddress, fldCity, MobileNo, Salary, Active, LeavingDate, Reason, CompanyID, BranchID, CreatedBy, CreatedDate) VALUES ("
                strSQL = strSQL & cnn.SQLquote(UCase(txtName.Text)) & "," & cnn.SQLquote(UCase(txtFatherName.Text)) & "," & Val(txtAge.Text) & "," & cnn.SQLquote(txtAddress.Text)
                strSQL = strSQL & "," & cnn.SQLquote(txtCity.Text) & "," & cnn.SQLquote(txtMobileNo.Text) & "," & Val(txtSalary.Text)
                If rbActive.Checked = True Then
                    strSQL = strSQL & ", 1, NULL, '',"
                Else
                    strSQL = strSQL & ", 0, '" & cnn.GetDate(txtLeavingDate.Text) & "'," & cnn.SQLquote(txtReason.Text) & ","
                End If
                strSQL = strSQL & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Employee", "Add", 0, UCase(txtName.Text))
            End If
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = ""
                txtName.Text = "" : txtFatherName.Text = "" : txtAge.Text = "" : txtAddress.Text = "" : txtCity.Text = "" : txtMobileNo.Text = "" : txtSalary.Text = ""
                txtName.Focus()
            End If
        Catch ex As Exception
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub
End Class