Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmProduct
    Dim cnn As New DataConn.Conn

    Private Sub frmProduct_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmProduct_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        ''==============================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, ProductGroup from ProductGroup Order BY ProductGroup ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbGroup.DisplayMember = "ProductGroup"
        cmbGroup.ValueMember = "ID"
        cmbGroup.DataSource = ds.Tables(0)
        cmbGroup.SelectedIndex = -1
        ds = Nothing
        da = Nothing

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Trim(cmbGroup.Text) = "" Then
            cnn.ErrMsgBox("Please Select Group Name...!")
            cmbGroup.Focus()
            Exit Sub
        End If
        If cmbGroup.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select Group Name from List ...!")
            cmbGroup.Focus()
            Exit Sub
        End If
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Product Name...!")
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtCode.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Product Model...!")
            txtCode.Focus()
            Exit Sub
        End If

        '---------------
        Try
            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from Products Where GroupID=" & Val(cmbGroup.SelectedValue) & " AND ProductName=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            'cnn.OpenConn()
            'cnn.Tran = cnn.cnn.BeginTransaction
            If lblID.Text <> "" Then
                strSQL = "UPDATE Products Set GroupID=" & Val(cmbGroup.SelectedValue) & ", ProductName=" & cnn.SQLquote(UCase(txtName.Text)) & ", ProductCode=" & cnn.SQLquote(UCase(txtCode.Text)) & ", SalePrice=" & Val(txtSalePrice.Text)
                strSQL = strSQL & ", CGST=" & Val(txtCGST.Text) & ", SGST=" & Val(txtSGST.Text) & ", IGST=" & Val(txtIGST.Text)
                strSQL = strSQL & ", PurchasePrice=" & Val(txtPurchasePrice.Text) & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' Where ID=" & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Products", "Edit", lblID.Text, UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO Products (GroupID, ProductName, ProductCode, SalePrice, PurchasePrice, CreatedBy, CreatedDate, CGST, SGST, IGST) OutPut Inserted.ID as HeaderID VALUES (" & Val(cmbGroup.SelectedValue)
                strSQL = strSQL & "," & cnn.SQLquote(UCase(txtName.Text)) & "," & cnn.SQLquote(UCase(txtCode.Text))
                strSQL = strSQL & "," & Val(txtSalePrice.Text) & "," & Val(txtPurchasePrice.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "'"
                strSQL = strSQL & "," & Val(txtCGST.Text) & "," & Val(txtSGST.Text) & "," & Val(txtIGST.Text) & ")"
                Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                If cmd.Connection.State = 1 Then cmd.Connection.Close()
                cmd.Connection.Open()
                Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
                If dr.Read Then
                    lblHeaderID.Text = dr.Item("HeaderID")
                End If
                dr.Close()
                cmd.Connection.Close()
                '=====================================
                cnn.Check_Insert_Product(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(lblHeaderID.Text), 0)
                'Dim strInsert As String
                'strInsert = "IF NOT EXISTS (SELECT dbo.BranchMaster.ID, dbo.BranchMaster.CompanyID FROM dbo.ProductStock INNER JOIN dbo.BranchMaster ON dbo.ProductStock.MasterCompanyID = dbo.BranchMaster.CompanyID AND dbo.ProductStock.MasterBranchID = dbo.BranchMaster.ID" _
                '& " WHERE (dbo.ProductStock.ProductID = " & Val(lblHeaderID.Text) & ") AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & "))" _
                '& " BEGIN" _
                '& " INSERT INTO ProductStock (MasterCompanyID, MasterBranchID, ProductID, Qty, SMID) VALUES (" & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(lblHeaderID.Text) & ", 0,0)" _
                '& " End"
                'cnn.OpenConn()
                'Dim cmdSP As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strInsert, cnn.cnn)
                'If cmdSP.Connection.State = 1 Then cmdSP.Connection.Close()
                'cmdSP.Connection.Open()
                'cmdSP.ExecuteNonQuery()
                'cmdSP.Connection.Close()
                '=====================================
                cnn.GenerateLog("Products", "Add", 0, UCase(txtName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = ""
                cmbGroup.Text = "" : txtName.Text = "" : txtCode.Text = "" : txtSalePrice.Text = "" : txtPurchasePrice.Text = "" : txtCGST.Text = "" : txtSGST.Text = "" : txtIGST.Text = ""
                cmbGroup.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtSalePrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSalePrice.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtSalePrice_TextChanged(sender As Object, e As EventArgs) Handles txtSalePrice.TextChanged

    End Sub

    Private Sub txtPurchasePrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPurchasePrice.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtPurchasePrice_TextChanged(sender As Object, e As EventArgs) Handles txtPurchasePrice.TextChanged

    End Sub
End Class