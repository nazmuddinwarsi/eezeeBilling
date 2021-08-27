Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmPurchaseList
    Dim cnn As New DataConn.Conn

    Private Sub frmPurchaseList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtFromDate.Text = Date.Today 'DateAdd(DateInterval.Month, -1, Date.Today)
        txtToDate.Text = Date.Today
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT dbo.PurchaseHeader.ID, dbo.PurchaseHeader.InvoiceNo, dbo.PurchaseHeader.InvoiceDate, dbo.tblAccount.AccountName, dbo.PurchaseHeader.TotalQty, dbo.PurchaseHeader.TotalAmount, dbo.PurchaseHeader.BillAmount, dbo.PurchaseHeader.Remarks" _
        & ", tblCREATE.fldName AS Create_Name, dbo.PurchaseHeader.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.PurchaseHeader.EditedDate FROM dbo.PurchaseDetails INNER JOIN dbo.PurchaseHeader ON dbo.PurchaseDetails.HeaderID = dbo.PurchaseHeader.ID" _
        & " INNER JOIN dbo.tblAccount ON dbo.PurchaseHeader.AccountID = dbo.tblAccount.ID INNER JOIN dbo.Products ON dbo.PurchaseDetails.ProductID = dbo.Products.ID" _
        & " INNER JOIN dbo.Users AS tblCREATE ON dbo.PurchaseHeader.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.PurchaseHeader.EditedBy = tblEDIT.ID" _
        & " WHERE (dbo.PurchaseHeader.MasterCompanyID =" & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.PurchaseHeader.MasterBranchID =" & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " AND (dbo.PurchaseHeader.InvoiceDate >='" & cnn.GetDate(txtFromDate.Text) & "') AND (dbo.PurchaseHeader.InvoiceDate <='" & cnn.GetDate(txtToDate.Text) & "')"
        If txtInvNo.Text <> "" Then
            strSQL = strSQL & " AND dbo.PurchaseHeader.InvoiceNo LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.Text <> "" Then
            strSQL = strSQL & " AND dbo.PurchaseHeader.AccountID = " & Val(cmbAccount.SelectedValue)
        End If
        If txtName.Text <> "" Then
            strSQL = strSQL & " AND dbo.Products.ProductName LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If cmbGroup.Text <> "" Then
            strSQL = strSQL & " AND dbo.Products.GroupID = " & Val(cmbGroup.SelectedValue)
        End If
        strSQL = strSQL & " GROUP BY dbo.PurchaseHeader.ID, dbo.PurchaseHeader.InvoiceNo, dbo.PurchaseHeader.InvoiceDate, dbo.tblAccount.AccountName, dbo.PurchaseHeader.TotalQty, dbo.PurchaseHeader.TotalAmount, dbo.PurchaseHeader.BillAmount, dbo.PurchaseHeader.Remarks" _
        & ", tblCREATE.fldName, dbo.PurchaseHeader.CreatedDate, tblEDIT.fldName, dbo.PurchaseHeader.EditedDate"

        '--------------------
        If cnn.cnn.State = 1 Then
            cnn.cnn.Close()
        End If
        cnn.cnn.Open()
        '--------------------
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Invoice No"
        Grid.Columns(2).HeaderText = "Invoice Date"
        Grid.Columns(3).HeaderText = "Supplier Name"
        Grid.Columns(4).HeaderText = "Total Qty"
        Grid.Columns(5).HeaderText = "Total Amount"
        Grid.Columns(6).HeaderText = "Bill Amount"
        Grid.Columns(7).HeaderText = "Remarks"
        Grid.Columns(8).HeaderText = "Created By"
        Grid.Columns(9).HeaderText = "Create Date"
        Grid.Columns(10).HeaderText = "Edited By"
        Grid.Columns(11).HeaderText = "Edit Date"
        If chkShow.Checked = True Then
            Grid.Columns(8).Visible = True : Grid.Columns(9).Visible = True : Grid.Columns(10).Visible = True : Grid.Columns(11).Visible = True
        Else
            Grid.Columns(8).Visible = False : Grid.Columns(9).Visible = False : Grid.Columns(10).Visible = False : Grid.Columns(11).Visible = False
        End If
        '=========================
        Grid2.DataSource = Nothing

    End Sub

    Private Sub frmPurchaseList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmPurchaseList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmPurchaseList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        ToolStrip1.Cursor = Cursors.Hand
        '==============================

        frameSearch.Visible = False
        FormRes()


        btnAdd.Image = MDI.ImageList1.Images(0)
        btnEdit.Image = MDI.ImageList1.Images(1)
        btnDelete.Image = MDI.ImageList1.Images(2)
        btnSearch.Image = MDI.ImageList1.Images(3)
        btnPrint.Image = MDI.ImageList1.Images(4)
        btnExit.Image = MDI.ImageList1.Images(5)
        '===========================================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        '--------------------
        If cnn.cnn.State = 1 Then
            cnn.cnn.Close()
        End If
        cnn.cnn.Open()
        '--------------------
        strSQL = "Select ID, AccountName from tblAccount Where tType='SUPPLIER' Order BY AccountName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbAccount.DisplayMember = "AccountName"
        cmbAccount.ValueMember = "ID"
        cmbAccount.DataSource = ds.Tables(0)
        cmbAccount.SelectedIndex = -1
        da = Nothing
        ds = Nothing
        '------------------
        strSQL = "Select ID, ProductGroup from ProductGroup Order BY ProductGroup ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbGroup.DisplayMember = "ProductGroup"
        cmbGroup.ValueMember = "ID"
        cmbGroup.DataSource = ds.Tables(0)
        cmbGroup.SelectedIndex = -1
        da = Nothing
        ds = Nothing

    End Sub

    Private Sub frmPurchaseList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width '- MDI.Panel.Width
        Grid.Height = (Me.Height / 2) - 37
        Grid2.Height = (Me.Height / 2) - 60
        Grid2.Top = Grid.Height + 90
        Grid2.Width = MDI.ClientSize.Width - MDI.Panel.Width
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        EditRecord()
    End Sub
    Private Sub EditRecord()
        If lblID.Text = "" Then
            cnn.ErrMsgBox("Please Select Record to Modify  !")
            Exit Sub
        End If
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(6, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        'cnn.ShowForm(frmPurchase)
        ''--------------------
        Dim strSQL As String
        strSQL = "Select ID, InvoiceNo, InvoiceDate, AccountID, TotalQty, TotalAmount, BillAmount, Remarks from PurchaseHeader Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmPurchase)
            frmPurchase.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("InvoiceNo")) Then
                frmPurchase.txtInvoiceNo.Text = dr.Item("InvoiceNo")
            End If
            If Not IsDBNull(dr.Item("InvoiceDate")) Then
                frmPurchase.txtDate.Text = dr.Item("InvoiceDate")
            End If
            If Not IsDBNull(dr.Item("AccountID")) Then
                frmPurchase.cmbSupplier.SelectedValue = dr.Item("AccountID")
            End If
            If Not IsDBNull(dr.Item("TotalQty")) Then
                frmPurchase.txtTotalQty.Text = dr.Item("TotalQty")
            End If
            If Not IsDBNull(dr.Item("TotalAmount")) Then
                frmPurchase.txtTotalAmount.Text = dr.Item("TotalAmount")
            End If
            If Not IsDBNull(dr.Item("BillAmount")) Then
                frmPurchase.txtBillAmt.Text = dr.Item("BillAmount")
            End If
            If Not IsDBNull(dr.Item("Remarks")) Then
                frmPurchase.txtRemarks.Text = dr.Item("Remarks")
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
        '----------------------------------------------------------------------
        '---------------------------------------
        'strSQL = "SELECT dbo.Products.ID, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.UnitMaster.UnitName, dbo.PurchaseDetails.Qty, dbo.PurchaseDetails.Rate, dbo.PurchaseDetails.Amount" _
        '& " FROM dbo.PurchaseDetails INNER JOIN dbo.Products ON dbo.PurchaseDetails.ProductID = dbo.Products.ID INNER JOIN dbo.UnitMaster ON dbo.Products.UnitID = dbo.UnitMaster.ID" _
        '& " WHERE (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ")"
        strSQL = "SELECT P.ID, P.ProductCode, P.ProductName, PD.Qty, PD.Nos, PD.Rate, PD.BasicPrice, PD.GSTAmt, PD.Amount, PD.CGSTPercent, PD.CGSTAmt, PD.SGSTPercent, PD.SGSTAmt, PD.IGSTPercent, PD.IGSTAmt" _
        & " FROM dbo.PurchaseDetails PD INNER JOIN dbo.Products P ON PD.ProductID = P.ID INNER JOIN dbo.ProductGroup PG ON P.GroupID = PG.ID" _
        & " WHERE (PD.HeaderID = " & Val(lblID.Text) & ")"

        frmPurchase.Grid.Rows.Clear()
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        'If dr.Read Then
        Do While dr.Read
            frmPurchase.Grid.Rows.Add(1)
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(0).Value = dr.Item("ID")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(1).Value = dr.Item("ProductName")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(2).Value = dr.Item("ProductCode")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(3).Value = dr.Item("Nos")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(4).Value = dr.Item("Rate")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(5).Value = dr.Item("BasicPrice")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(6).Value = dr.Item("GSTAmt")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(7).Value = dr.Item("Qty")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(8).Value = dr.Item("Amount")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(9).Value = dr.Item("CGSTPercent")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(10).Value = dr.Item("CGSTAmt")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(11).Value = dr.Item("SGSTPercent")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(12).Value = dr.Item("SGSTAmt")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(13).Value = dr.Item("IGSTPercent")
            frmPurchase.Grid.Rows(frmPurchase.Grid.Rows.Count - 1).Cells(14).Value = dr.Item("IGSTAmt")

            'Grid.Rows(Grid.Rows.Count - 1).Cells(0).Value = cmbProduct.SelectedValue
            'Grid.Rows(Grid.Rows.Count - 1).Cells(1).Value = cmbProduct.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(2).Value = txtCode.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(3).Value = txtNos.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(4).Value = txtRate.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(5).Value = Val(txtPrice.Text)
            'Grid.Rows(Grid.Rows.Count - 1).Cells(6).Value = txtGST.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(7).Value = txtQty.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(8).Value = (Val(txtPrice.Text) * Val(txtQty.Text)) + (Val(txtGST.Text) * Val(txtQty.Text)) ' Val(txtPrice.Text) * Val(txtQty.Text)
            'Grid.Rows(Grid.Rows.Count - 1).Cells(9).Value = lblCGST.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(10).Value = IIf(Val(lblCGST.Text) > 0, cnn.Num2(Val(txtGST.Text) / 2), 0)
            'Grid.Rows(Grid.Rows.Count - 1).Cells(11).Value = lblSGST.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(12).Value = IIf(Val(lblSGST.Text) > 0, cnn.Num2(Val(txtGST.Text) / 2), 0)
            'Grid.Rows(Grid.Rows.Count - 1).Cells(13).Value = lblIGST.Text
            'Grid.Rows(Grid.Rows.Count - 1).Cells(14).Value = IIf(Val(lblIGST.Text) > 0, cnn.Num2(Val(txtGST.Text)), 0)
        Loop
        'End If
        '---------------------------------------
        strSQL = "SELECT dbo.BillSundryDetails.SundryID, dbo.BillSundryMaster.SundryName, dbo.BillSundryDetails.Value, dbo.BillSundryDetails.Amount, dbo.BillSundryMaster.SundryType, dbo.BillSundryMaster.AmountAs, dbo.BillSundryMaster.PercentOf, dbo.BillSundryMaster.RoundOff, dbo.BillSundryMaster.RoundOffType , dbo.BillSundryNature.Type" _
        & " FROM dbo.BillSundryDetails INNER JOIN dbo.BillSundryMaster ON dbo.BillSundryDetails.SundryID = dbo.BillSundryMaster.ID INNER JOIN dbo.BillSundryNature ON dbo.BillSundryMaster.NatureID = dbo.BillSundryNature.ID" _
        & " WHERE (dbo.BillSundryDetails.HeaderID = " & Val(lblID.Text) & ") AND (dbo.BillSundryDetails.vType = 'PURCHASE')"

        frmPurchase.grdTax.Rows.Clear()
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        Do While dr.Read
            frmPurchase.grdTax.Rows.Add(1)
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(0).Value = dr.Item("SundryID")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(1).Value = dr.Item("SundryName")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(2).Value = dr.Item("Value")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(3).Value = dr.Item("Amount")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(4).Value = dr.Item("SundryType")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(5).Value = dr.Item("AmountAs")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(6).Value = dr.Item("PercentOf")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(7).Value = dr.Item("RoundOff")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(8).Value = dr.Item("RoundOffType")
            frmPurchase.grdTax.Rows(frmPurchase.grdTax.Rows.Count - 1).Cells(9).Value = dr.Item("Type")
        Loop
        frmPurchase.GridTotal()
        frmPurchase.CalculateSundryGrid()
    End Sub

    Private Sub Grid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(2).Value.ToString
                GetDetails()
            End If
        End If
    End Sub

    Private Sub Grid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellDoubleClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(2).Value.ToString
                EditRecord()
            End If
        End If
    End Sub

    Private Sub Grid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grid.KeyDown
        'If Grid.Rows.Count > 1 Then
        '    If e.KeyCode = Keys.Up Then
        '        Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
        '        If crRowIndex - 1 >= 0 Then
        '            lblID.Text = Grid.Rows(crRowIndex - 1).Cells(0).Value.ToString
        '            lblName.Text = Grid.Rows(crRowIndex - 1).Cells(2).Value.ToString
        '        End If
        '    End If
        '    If e.KeyCode = Keys.Down Then
        '        Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
        '        If crRowIndex + 1 < Grid.Rows.Count Then
        '            lblID.Text = Grid.Rows(crRowIndex + 1).Cells(0).Value.ToString
        '            lblName.Text = Grid.Rows(crRowIndex + 1).Cells(2).Value.ToString
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteRecord()
    End Sub
    Private Sub DeleteRecord()
        If lblID.Text = "" Then
            cnn.ErrMsgBox("Please Select Record to Delete  !")
            Exit Sub
        End If
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(6, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String
        '--------------------------------------------------------------
        strSQL = "SELECT dbo.PurchaseDetails.Qty AS PurchaseQty, dbo.ProductStock.Qty AS StockQty, dbo.PurchaseDetails.HeaderID, dbo.PurchaseDetails.ProductID" _
            & " FROM dbo.PurchaseDetails INNER JOIN dbo.ProductStock ON dbo.PurchaseDetails.ProductID = dbo.ProductStock.ProductID" _
            & " WHERE (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ")" _
            & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
            & " AND (dbo.ProductStock.SMID = 0)"

        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        While dr.Read
            If (Val(dr.Item("StockQty")) - Val(dr.Item("PurchaseQty"))) < 0 Then
                cnn.ErrMsgBox("You can't delete this record due to Stock Validation, Please check  !")
                dr.Close()
                cmd.Connection.Close()
                Exit Sub
            End If
        End While
        dr.Close()
        cmd.Connection.Close()
        '--------------------------------------- STOCK REVERSE
        strSQL = "UPDATE ProductStock SET dbo.ProductStock.Qty = (dbo.ProductStock.Qty - dbo.PurchaseDetails.Qty)" _
        & " FROM dbo.ProductStock INNER JOIN dbo.PurchaseDetails ON dbo.ProductStock.ProductID = dbo.PurchaseDetails.ProductID" _
        & " WHERE (dbo.ProductStock.ProductID = dbo.PurchaseDetails.ProductID) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")" _
        & " AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ")" _
        & " AND (dbo.ProductStock.SMID = 0)"
        '--------------------------------------- STOCK REVERSE
        strSQL = "DELETE from PurchaseDetails Where HeaderID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        strSQL = "DELETE from PurchaseHeader Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)

        cnn.GenerateLog("Purchase", "Delete", Val(lblID.Text), lblName.Text)
        cnn.InfoMsgBox("Record has been Deleted  !")
        BindGrid()
        lblID.Text = ""
        lblName.Text = ""
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        frameSearch.Visible = True
        txtFromDate.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(6, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchase)
    End Sub

    Private Sub Grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub
    Private Sub GetDetails()
        If lblID.Text <> "" Then
            Grid2.Columns.Clear()
            Dim strSQL, sSQL As String
            Dim da As New OdbcDataAdapter
            Dim ds As New DataSet
            sSQL = ""
            'strSQL = "SELECT dbo.PurchaseDetails.HeaderID, dbo.PurchaseHeader.InvoiceNo, dbo.PurchaseHeader.InvoiceDate, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.UnitMaster.UnitName, dbo.PurchaseDetails.Qty, dbo.PurchaseDetails.Rate, dbo.PurchaseDetails.Amount" _
            '& " FROM dbo.PurchaseDetails INNER JOIN dbo.Products ON dbo.PurchaseDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.UnitMaster ON dbo.Products.UnitID = dbo.UnitMaster.ID INNER JOIN dbo.PurchaseHeader ON dbo.PurchaseDetails.HeaderID = dbo.PurchaseHeader.ID" _
            '& " WHERE (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ")"
            strSQL = "SELECT dbo.PurchaseDetails.HeaderID, dbo.PurchaseHeader.InvoiceNo, dbo.PurchaseHeader.InvoiceDate, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.PurchaseDetails.Qty, dbo.PurchaseDetails.Rate, dbo.PurchaseDetails.Amount" _
                & " FROM dbo.PurchaseDetails INNER JOIN dbo.Products ON dbo.PurchaseDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.PurchaseHeader ON dbo.PurchaseDetails.HeaderID = dbo.PurchaseHeader.ID" _
                & " WHERE (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ")"
            da = New OdbcDataAdapter(strSQL, cnn.cnn)
            ds = New DataSet()
            da.Fill(ds)
            Grid2.DataSource = ds.Tables(0)
            Grid2.Columns(0).HeaderText = "ID"
            Grid2.Columns(0).Visible = False
            Grid2.Columns(1).HeaderText = "Invoice No"
            Grid2.Columns(2).HeaderText = "Invoice Date"
            Grid2.Columns(3).HeaderText = "Product Name"
            Grid2.Columns(4).HeaderText = "Product Code"
            Grid2.Columns(5).HeaderText = "Product Group"
            Grid2.Columns(6).HeaderText = "Qty"
            Grid2.Columns(7).HeaderText = "Rate"
            Grid2.Columns(8).HeaderText = "Amount"
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Purchase List"
        strReportName = Application.StartupPath & "\Reports\PurchaseList.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'PURCHASE LIST FROM " & txtFromDate.Text & " TO " & txtToDate.Text & "'"
        strSelection = "{rptPurchase.InvoiceDate} >= #" & cnn.GetDate(txtFromDate.Text) & "#" _
        & " AND {rptPurchase.InvoiceDate} <= #" & cnn.GetDate(txtToDate.Text) & "#" _
        & " AND {rptPurchase.MasterBranchID} = " & Val(MDI.lblMasterBranchID.Text)
        If txtInvNo.Text <> "" Then
            strSelection = strSelection & " AND {rptPurchase.InvoiceNo} LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptPurchase.AccountID} = " & Val(cmbAccount.SelectedValue)
        End If
        If txtName.Text <> "" Then
            strSelection = strSelection & " AND {rptPurchase.ProductName} LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If cmbGroup.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptPurchase.GroupID} = " & Val(cmbGroup.SelectedValue)
        End If
       
        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        objfrmRptViewer.CRViewer.ReportSource = CrxReport
        objfrmRptViewer.CRViewer.SelectionFormula = strSelection
        objfrmRptViewer.CRViewer.RefreshReport()
        objfrmRptViewer.CRViewer.ShowExportButton = True
        objfrmRptViewer.CRViewer.ShowPrintButton = True
        objfrmRptViewer.Text = reportDesc
        objfrmRptViewer.Show()
        objfrmRptViewer.WindowState = FormWindowState.Maximized
        Cursor.Current = Cursors.Default
        '===================================================
m_quit:
        objfrmRptViewer = Nothing
        Exit Sub
m_err:
        MsgBox("Flow error!", vbCritical)
        GoTo m_quit
m_err2:
        MsgBox(Err.Description, vbCritical)
        GoTo m_quit
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        BindGrid()
        frameSearch.Visible = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frameSearch.Visible = False
    End Sub
End Class