Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmAddExpense
    Dim cnn As New DataConn.Conn
    Dim strSQL As String

    Private Sub frmAddExpense_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmAddExpense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        txtDate.Text = Date.Today
        ''==============================

        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, ExpType from tblExpType Order BY ExpType ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbExpType.DisplayMember = "ExpType"
        cmbExpType.ValueMember = "ID"
        cmbExpType.DataSource = ds.Tables(0)
        cmbExpType.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        GetPara()
    End Sub
    Private Sub GetPara()
        '------------------------
        strSQL = "SELECT ExpRefNo from Para Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If dr.Read Then
            If Not IsDBNull(dr.Item("ExpRefNo")) Then
                txtRefNo.Text = dr.Item("ExpRefNo")
            Else
                cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
                dr.Close()
                cmd.Connection.Close()
                Exit Sub
            End If
        Else
            cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
            dr.Close()
            cmd.Connection.Close()
            Exit Sub
        End If
        dr.Close()
        cmd.Connection.Close()
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cmbExpType.Text = "" Then
            cnn.ErrMsgBox("Please Select Expense Type  !")
            cmbExpType.Focus()
            Exit Sub
        End If
        If cmbExpType.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select Expense Type from List  !")
            cmbExpType.Focus()
            Exit Sub
        End If
        If txtName.Text = "" Then
            cnn.ErrMsgBox("Please Enter Expense Name  !")
            txtName.Focus()
            Exit Sub
        End If
        If Val(txtAmount.Text) <= 0 Then
            cnn.ErrMsgBox("Please Enter Valid Amount  !")
            txtAmount.Focus()
            Exit Sub
        End If

        Dim strSQL As String

        If lblID.Text <> "" Then
            strSQL = "UPDATE tblExpense SET fldDate='" & cnn.GetDate(txtDate.Text) & "', ExpTypeID=" & Val(cmbExpType.SelectedValue) _
            & ", ExpName=" & cnn.SQLquote(txtName.Text) & ", Amount=" & Val(txtAmount.Text) & ", Remarks=" & cnn.SQLquote(txtRemarks.Text) _
            & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' WHERE ID=" & Val(lblID.Text)
        Else
            strSQL = "INSERT INTO tblExpense (RefNo, fldDate, ExpTypeID, ExpName, Amount, Remarks, MasterCompanyID, MasterBranchID, CreatedBy, CreatedDate) VALUES (" _
            & Val(txtRefNo.Text) & ",'" & cnn.GetDate(txtDate.Text) & "'," & Val(cmbExpType.SelectedValue) & "," & cnn.SQLquote(txtName.Text) & "," & Val(txtAmount.Text) _
            & "," & cnn.SQLquote(txtRemarks.Text) & "," & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
        End If
        cnn.executeSQL(strSQL)

        If lblID.Text <> "" Then
            cnn.InfoMsgBox("Record has been Updated Successfully !")
            Me.Close()
        Else
            cnn.InfoMsgBox("Record has been Added Successfully  !")
            cnn.executeSQL("UPDATE Para SET ExpRefNo = ExpRefNo + 1 Where MasterCompanyID=" & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID=" & Val(MDI.lblMasterBranchID.Text))

            txtDate.Text = Date.Today : cmbExpType.SelectedIndex = -1 : txtName.Text = "" : txtAmount.Text = "" : txtRemarks.Text = ""
            GetPara()
        End If
    End Sub
End Class