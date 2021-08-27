Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmReceipt
    Dim cnn As New DataConn.Conn

    Private Sub frmReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmReceipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        txtDate.Text = Date.Today
        ''==============================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, AccountName from tblAccount Where tType='BUYER' Order BY AccountName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbSupplier.DisplayMember = "AccountName"
        cmbSupplier.ValueMember = "ID"
        cmbSupplier.DataSource = ds.Tables(0)
        cmbSupplier.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        '------------------------
        strSQL = "Select ID, DisplayName from tblBank Order BY DisplayName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbBank.DisplayMember = "DisplayName"
        cmbBank.ValueMember = "ID"
        cmbBank.DataSource = ds.Tables(0)
        cmbBank.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        '------------------------
        strSQL = "SELECT ReceiptRefNo from Para Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If dr.Read Then
            If Not IsDBNull(dr.Item("ReceiptRefNo")) Then
                txtRefNo.Text = dr.Item("ReceiptRefNo")
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

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cmbSupplier.Text = "" Then
            cnn.ErrMsgBox("Please Select Buyer Name  !")
            cmbSupplier.Focus()
            Exit Sub
        End If
        If cmbSupplier.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select Buyer Name from List  !")
            cmbSupplier.Focus()
            Exit Sub
        End If
        If rbChq.Checked = True Then
            If Val(cmbBank.SelectedValue) <= 0 Then
                cnn.ErrMsgBox("Please Select Bank Name !")
                cmbBank.Focus()
                Exit Sub
            End If
        End If
        If Val(txtAmount.Text) <= 0 Then
            cnn.ErrMsgBox("Please Enter Valid Amount  !")
            txtAmount.Focus()
            Exit Sub
        End If

        Dim strSQL, iPayMode As String
        If rbCash.Checked = True Then
            iPayMode = "CASH"
        Else
            iPayMode = "CHEUQE"
        End If

        If lblID.Text <> "" Then
            strSQL = "UPDATE dbo.tblBank SET dbo.tblBank.CurBalance = (dbo.tblBank.CurBalance - dbo.Receipt.Amount) FROM dbo.Receipt INNER JOIN dbo.tblBank ON dbo.Receipt.BankID = dbo.tblBank.ID" _
            & " WHERE (dbo.Receipt.ID = " & Val(lblID.Text) & ")"
            cnn.executeSQL(strSQL)
            strSQL = "UPDATE Receipt SET ReceiptDate='" & cnn.GetDate(txtDate.Text) & "', AccountID=" & Val(cmbSupplier.SelectedValue) _
            & ", BankID=" & Val(cmbBank.SelectedValue) & ", PayMode='" & iPayMode & "', Amount=" & Val(txtAmount.Text) & ", Remarks=" & cnn.SQLquote(txtRemarks.Text) _
            & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' WHERE ID=" & Val(lblID.Text)
        Else
            strSQL = "INSERT INTO Receipt (ReceiptNo, ReceiptDate, AccountID, PayMode, BankID, Amount, Remarks, MasterCompanyID, MasterBranchID, CreatedBy, CreatedDate) VALUES (" _
            & Val(txtRefNo.Text) & ",'" & cnn.GetDate(txtDate.Text) & "'," & Val(cmbSupplier.SelectedValue) & ",'" & iPayMode & "'," & Val(cmbBank.SelectedValue) & "," & Val(txtAmount.Text) _
            & "," & cnn.SQLquote(txtRemarks.Text) & "," & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
        End If
        cnn.executeSQL(strSQL)
        strSQL = "UPDATE tblBank SET CurBalance = (CurBalance + " & Val(txtAmount.Text) & ") WHERE ID=" & Val(cmbBank.SelectedValue)
        cnn.executeSQL(strSQL)

        If lblID.Text <> "" Then
            cnn.InfoMsgBox("Record has been Updated Successfully !")
            Me.Close()
        Else
            cnn.InfoMsgBox("Record has been Added Successfully  !")
            cnn.executeSQL("UPDATE Para SET ReceiptRefNo = ReceiptRefNo + 1 Where MasterCompanyID=" & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID=" & Val(MDI.lblMasterBranchID.Text))

            txtDate.Text = Date.Today : cmbSupplier.SelectedIndex = -1 : txtAmount.Text = "" : rbCash.Checked = True : txtRemarks.Text = ""
            strSQL = "SELECT ReceiptRefNo from Para Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("ReceiptRefNo")) Then
                    txtRefNo.Text = dr.Item("ReceiptRefNo")
                Else
                    dr.Close() : cmd.Connection.Close() : cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !") : Exit Sub
                End If
            Else
                dr.Close() : cmd.Connection.Close() : cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !") : Exit Sub
            End If
            dr.Close() : cmd.Connection.Close()
        End If
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        '-------------------------------------------------
        lblBalance.Text = GetOpeningBalance()
        lblBalance.Text = cnn.Num2(lblBalance.Text)
        '-------------------------------------------------
    End Sub
    Private Function GetOpeningBalance() As Double
        If cmbSupplier.Text <> "" Then
            '-------------------------------------------------
            cnn.GetBuyerStatement(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(cmbSupplier.SelectedValue), "1950-01-01", cnn.GetDate(Date.Today()))
            '-------------------------------------------------
            Dim strSQL As String
            Dim iBal As Double = 0
            strSQL = "SELECT SUM(SalePurchaseAmt) - SUM(PaymentRecAmt) AS Balance FROM dbo.tblTempStatement"
            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                iBal = dr.Item(0)
            End If
            dr.Close()
            cmd.Connection.Close()
            Return iBal
        End If
    End Function

    Private Sub rbCash_CheckedChanged(sender As Object, e As EventArgs) Handles rbCash.CheckedChanged
        PayType()
    End Sub

    Private Sub rbChq_CheckedChanged(sender As Object, e As EventArgs) Handles rbChq.CheckedChanged
        PayType()
    End Sub
    Private Sub PayType()
        If rbCash.Checked = True Then
            cmbBank.Enabled = False : cmbBank.SelectedIndex = -1
        Else
            cmbBank.Enabled = True
        End If
    End Sub

    Private Sub cmbBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBank.SelectedIndexChanged
        '-------------------------------------------------
        lblBalance2.Text = GetCurBalance()
        lblBalance2.Text = cnn.Num2(lblBalance2.Text)
        '-------------------------------------------------
    End Sub
    Private Function GetCurBalance() As Double
        If cmbBank.Text <> "" Then
            Dim strSQL As String
            Dim iBal As Double = 0
            strSQL = "SELECT CurBalance FROM dbo.tblBank Where ID=" & Val(cmbBank.SelectedValue)
            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                iBal = dr.Item(0)
            End If
            dr.Close()
            cmd.Connection.Close()
            Return iBal
        Else
            Return 0
        End If
    End Function
End Class