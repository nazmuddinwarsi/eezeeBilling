Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmSaleList
    Dim cnn As New DataConn.Conn

    Private Sub frmSaleList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtFromDate.Text = Date.Today 'DateAdd(DateInterval.Month, -1, Date.Today)
        txtToDate.Text = Date.Today
        BindGrid()
    End Sub

    Private Sub frmSaleList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmSaleList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If

    End Sub

    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT dbo.SaleHeader.ID, dbo.SaleHeader.InvoiceNo, dbo.SaleHeader.InvoiceDate, dbo.tblAccount.AccountName, dbo.SaleHeader.TotalNos, dbo.SaleHeader.TotalQty, dbo.SaleHeader.TotalAmount, dbo.SaleHeader.BillAmount, dbo.SaleHeader.Remarks" _
        & ", tblCREATE.fldName AS Create_Name, dbo.SaleHeader.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.SaleHeader.EditedDate, dbo.SaleHeader.AccountID FROM dbo.SaleDetails INNER JOIN dbo.SaleHeader ON dbo.SaleDetails.HeaderID = dbo.SaleHeader.ID INNER JOIN dbo.tblAccount ON dbo.SaleHeader.AccountID = dbo.tblAccount.ID INNER JOIN dbo.Products ON dbo.SaleDetails.ProductID = dbo.Products.ID" _
        & " INNER JOIN dbo.Users AS tblCREATE ON dbo.SaleHeader.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.SaleHeader.EditedBy = tblEDIT.ID" _
        & " WHERE (dbo.SaleHeader.MasterCompanyID =" & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.SaleHeader.MasterBranchID =" & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.SaleHeader.SalesManID = 0)" _
        & " AND (dbo.SaleHeader.InvoiceDate >='" & cnn.GetDate(txtFromDate.Text) & "') AND (dbo.SaleHeader.InvoiceDate <='" & cnn.GetDate(txtToDate.Text) & "')"
        If txtInvNo.Text <> "" Then
            strSQL = strSQL & " AND dbo.SaleHeader.InvoiceNo LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.Text <> "" Then
            strSQL = strSQL & " AND dbo.SaleHeader.AccountID = " & Val(cmbAccount.SelectedValue)
        End If
        If cmbProduct.Text <> "" Then
            strSQL = strSQL & " AND dbo.SaleDetails.ProductID = " & Val(cmbProduct.SelectedValue)
        End If
        If cmbGroup.Text <> "" Then
            strSQL = strSQL & " AND dbo.Products.GroupID = " & Val(cmbGroup.SelectedValue)
        End If
        strSQL = strSQL & " GROUP BY dbo.SaleHeader.ID, dbo.SaleHeader.InvoiceNo, dbo.SaleHeader.InvoiceDate, dbo.tblAccount.AccountName, dbo.SaleHeader.TotalNos, dbo.SaleHeader.TotalQty, dbo.SaleHeader.TotalAmount, dbo.SaleHeader.BillAmount, dbo.SaleHeader.Remarks" _
        & ", tblCREATE.fldName, dbo.SaleHeader.CreatedDate, tblEDIT.fldName, dbo.SaleHeader.EditedDate, dbo.SaleHeader.AccountID"

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
        Grid.Columns(4).HeaderText = "Total Nos"
        Grid.Columns(5).HeaderText = "Total Qty"
        Grid.Columns(6).HeaderText = "Total Amount"
        Grid.Columns(7).HeaderText = "Bill Amount"
        Grid.Columns(8).HeaderText = "Remarks"
        Grid.Columns(9).HeaderText = "Created By"
        Grid.Columns(10).HeaderText = "Create Date"
        Grid.Columns(11).HeaderText = "Edited By"
        Grid.Columns(12).HeaderText = "Edit Date"
        Grid.Columns(4).Visible = False
        If chkShow.Checked = True Then
            Grid.Columns(9).Visible = True : Grid.Columns(10).Visible = True : Grid.Columns(11).Visible = True : Grid.Columns(12).Visible = True
        Else
            Grid.Columns(9).Visible = False : Grid.Columns(10).Visible = False : Grid.Columns(11).Visible = False : Grid.Columns(12).Visible = False
        End If
        Grid.Columns(13).HeaderText = "AccountID"
        Grid.Columns(13).Visible = False
        '=========================
        Dim iQty, iBill As Integer
        iQty = 0 : iBill = 0
        For i = 0 To Grid.Rows.Count - 1
            iQty = Val(iQty) + Val(Grid.Rows(i).Cells(5).Value)
            iBill = Val(iBill) + Val(Grid.Rows(i).Cells(7).Value)
        Next
        lblQty.Text = iQty : lblBillAmount.Text = iBill
        '=========================
        Grid2.DataSource = Nothing
    End Sub

    Private Sub frmSaleList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        ToolStrip1.Cursor = Cursors.Hand
        btnAdd.Image = MDI.ImageList1.Images(0)
        btnEdit.Image = MDI.ImageList1.Images(1)
        btnDelete.Image = MDI.ImageList1.Images(2)
        btnSearch.Image = MDI.ImageList1.Images(3)
        btnPrint.Image = MDI.ImageList1.Images(4)
        btnInvoice.Image = MDI.ImageList1.Images(4)
        btnExit.Image = MDI.ImageList1.Images(5)
        '===========================================
        frameSearch.Visible = False

        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, AccountName from tblAccount Where tType='BUYER' Order BY AccountName ASC"
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
        '------------------
        strSQL = "Select ID, ProductName from Products Order BY ProductName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbProduct.DisplayMember = "ProductName"
        cmbProduct.ValueMember = "ID"
        cmbProduct.DataSource = ds.Tables(0)
        cmbProduct.SelectedIndex = -1
        da = Nothing
        ds = Nothing
    End Sub

    Private Sub frmSaleList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = (Me.Height / 2) - 37
        Grid2.Height = (Me.Height / 2) - 60
        Grid2.Top = Grid.Height + 90
        Grid2.Width = Me.Width
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
            If cnn.GetUserAccess(7, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        'cnn.ShowForm(frmSale)
        ''--------------------
        Dim strSQL As String
        strSQL = "Select ID, InvoiceNo, InvoiceDate, AccountID, TotalQty, TotalAmount, BillAmount, Remarks from SaleHeader Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmSale)
            frmSale.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("InvoiceNo")) Then
                frmSale.txtInvoiceNo.Text = dr.Item("InvoiceNo")
            End If
            If Not IsDBNull(dr.Item("InvoiceDate")) Then
                frmSale.txtDate.Text = dr.Item("InvoiceDate")
            End If
            If Not IsDBNull(dr.Item("AccountID")) Then
                frmSale.cmbBuyer.SelectedValue = dr.Item("AccountID")
            End If
            If Not IsDBNull(dr.Item("TotalQty")) Then
                frmSale.txtTotalQty.Text = dr.Item("TotalQty")
            End If
            If Not IsDBNull(dr.Item("TotalAmount")) Then
                frmSale.txtTotalAmount.Text = dr.Item("TotalAmount")
            End If
            If Not IsDBNull(dr.Item("BillAmount")) Then
                frmSale.txtBillAmt.Text = dr.Item("BillAmount")
            End If
            If Not IsDBNull(dr.Item("Remarks")) Then
                frmSale.txtRemarks.Text = dr.Item("Remarks")
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
        '---------------------------------------
        'strSQL = "SELECT dbo.Products.ID, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.UnitMaster.UnitName, dbo.SaleDetails.Qty, dbo.SaleDetails.Rate, dbo.SaleDetails.Amount" _
        '& " FROM dbo.SaleDetails INNER JOIN dbo.Products ON dbo.SaleDetails.ProductID = dbo.Products.ID INNER JOIN dbo.UnitMaster ON dbo.Products.UnitID = dbo.UnitMaster.ID" _
        '& " WHERE (dbo.SaleDetails.HeaderID = " & Val(lblID.Text) & ")"

        strSQL = "SELECT P.ID, P.ProductCode, P.ProductName, SD.Qty, SD.Nos, SD.Rate, SD.BasicPrice, SD.GSTAmt, SD.Amount, SD.CGSTPercent, SD.CGSTAmt, SD.SGSTPercent, SD.SGSTAmt, SD.IGSTPercent, SD.IGSTAmt" _
        & " FROM dbo.SaleDetails SD INNER JOIN dbo.Products P ON SD.ProductID = P.ID INNER JOIN dbo.ProductGroup PG ON P.GroupID = PG.ID" _
        & " WHERE (SD.HeaderID = " & Val(lblID.Text) & ")"


        frmSale.Grid.Rows.Clear()
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader

        Do While dr.Read
            frmSale.Grid.Rows.Add(1)
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(0).Value = dr.Item("ID")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(1).Value = dr.Item("ProductName")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(2).Value = dr.Item("ProductCode")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(3).Value = dr.Item("Nos")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(4).Value = dr.Item("Rate")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(5).Value = dr.Item("BasicPrice")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(6).Value = dr.Item("GSTAmt")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(7).Value = dr.Item("Qty")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(8).Value = dr.Item("Amount")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(9).Value = dr.Item("CGSTPercent")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(10).Value = dr.Item("CGSTAmt")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(11).Value = dr.Item("SGSTPercent")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(12).Value = dr.Item("SGSTAmt")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(13).Value = dr.Item("IGSTPercent")
            frmSale.Grid.Rows(frmSale.Grid.Rows.Count - 1).Cells(14).Value = dr.Item("IGSTAmt")
        Loop

        '---------------------------------------
        strSQL = "SELECT dbo.BillSundryDetails.SundryID, dbo.BillSundryMaster.SundryName, dbo.BillSundryDetails.Value, dbo.BillSundryDetails.Amount, dbo.BillSundryMaster.SundryType, dbo.BillSundryMaster.AmountAs, dbo.BillSundryMaster.PercentOf, dbo.BillSundryMaster.RoundOff, dbo.BillSundryMaster.RoundOffType" _
        & " FROM dbo.BillSundryDetails INNER JOIN dbo.BillSundryMaster ON dbo.BillSundryDetails.SundryID = dbo.BillSundryMaster.ID " _
        & " WHERE (dbo.BillSundryDetails.HeaderID = " & Val(lblID.Text) & ") AND (dbo.BillSundryDetails.vType = 'SALE')"
        
        frmSale.grdTax.Rows.Clear()
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader

        Do While dr.Read
            frmSale.cmbSundry.SelectedValue = dr.Item("SundryID")
            frmSale.txtValue.Text = dr.Item("Value")
            frmSale.txtDiscAmt.Text = dr.Item("Amount")
            'frmSale.grdTax.Rows.Add(1)
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(0).Value = dr.Item("SundryID")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(1).Value = dr.Item("SundryName")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(2).Value = dr.Item("Value")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(3).Value = dr.Item("Amount")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(4).Value = dr.Item("SundryType")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(5).Value = dr.Item("AmountAs")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(6).Value = dr.Item("PercentOf")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(7).Value = dr.Item("RoundOff")
            'frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(8).Value = dr.Item("RoundOffType")
            ''frmSale.grdTax.Rows(frmSale.grdTax.Rows.Count - 1).Cells(9).Value = dr.Item("Type")
        Loop

        frmSale.GridTotal()
        frmSale.CalculateSundryGrid()
    End Sub

    Private Sub Grid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(2).Value.ToString
                lblAccountID.Text = Grid.Rows(crRowIndex).Cells(13).Value.ToString
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
            If cnn.GetUserAccess(7, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String
        '--------------------------------------------------------------
        strSQL = "SELECT dbo.SaleDetails.Qty AS SaleQty, dbo.ProductStock.Qty AS StockQty, dbo.SaleDetails.HeaderID, dbo.SaleDetails.ProductID" _
            & " FROM dbo.SaleDetails INNER JOIN dbo.ProductStock ON dbo.SaleDetails.ProductID = dbo.ProductStock.ProductID" _
            & " WHERE (dbo.SaleDetails.HeaderID = " & Val(lblID.Text) & ")" _
            & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
            & " AND (dbo.ProductStock.SMID = 0)"

        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        While dr.Read
            If (Val(dr.Item("StockQty")) + Val(dr.Item("SaleQty"))) < 0 Then
                cnn.ErrMsgBox("You can't delete this record due to Stock Validation, Please check  !")
                dr.Close()
                cmd.Connection.Close()
                Exit Sub
            End If
        End While
        dr.Close()
        cmd.Connection.Close()
        '--------------------------------------- STOCK REVERSE
        strSQL = "UPDATE ProductStock SET dbo.ProductStock.Qty = (dbo.ProductStock.Qty + dbo.SaleDetails.Qty)" _
        & " FROM dbo.ProductStock INNER JOIN dbo.SaleDetails ON dbo.ProductStock.ProductID = dbo.SaleDetails.ProductID" _
        & " WHERE (dbo.ProductStock.ProductID = dbo.SaleDetails.ProductID) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")" _
        & " AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.SaleDetails.HeaderID = " & Val(lblID.Text) & ")" _
        & " AND (dbo.ProductStock.SMID = 0)"
        cnn.executeSQL(strSQL)
        '--------------------------------------- STOCK REVERSE
        strSQL = "DELETE from SaleDetails Where HeaderID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        strSQL = "DELETE from SaleHeader Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)

        cnn.GenerateLog("Sale", "Delete", Val(lblID.Text), lblName.Text)
        cnn.InfoMsgBox("Record has been Deleted  !")
        BindGrid()
        lblID.Text = ""
        lblName.Text = ""
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        frameSearch.Visible = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        BindGrid()
        frameSearch.Visible = False
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
            If cnn.GetUserAccess(7, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSale)
    End Sub

    Private Sub Grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub
    Private Sub GetDetails()
        If lblID.Text <> "" Then
            Dim strSQL, sSQL As String
            Dim da As New OdbcDataAdapter
            Dim ds As New DataSet
            sSQL = ""
            strSQL = "SELECT dbo.SaleDetails.HeaderID, dbo.SaleHeader.InvoiceNo, dbo.SaleHeader.InvoiceDate, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.SaleDetails.Nos, dbo.SaleDetails.Qty, dbo.SaleDetails.Rate, dbo.SaleDetails.Amount" _
                & " FROM dbo.SaleDetails INNER JOIN dbo.Products ON dbo.SaleDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.SaleHeader ON dbo.SaleDetails.HeaderID = dbo.SaleHeader.ID" _
                & " WHERE (dbo.SaleDetails.HeaderID = " & Val(lblID.Text) & ")"

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
            Grid2.Columns(6).HeaderText = "Nos"
            Grid2.Columns(7).HeaderText = "Qty"
            Grid2.Columns(8).HeaderText = "Rate"
            Grid2.Columns(9).HeaderText = "Amount"
            Grid2.Columns(6).Visible = False
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        framePrint.Visible = True
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        strSelection = "{rptSale.InvoiceDate} >= #" & cnn.GetDate(txtFromDate.Text) & "#" _
        & " AND {rptSale.InvoiceDate} <= #" & cnn.GetDate(txtToDate.Text) & "#" _
        & " AND {rptSale.SalesManID} = 0" _
        & " AND {rptSale.MasterBranchID} = " & Val(MDI.lblMasterBranchID.Text)
        If txtInvNo.Text <> "" Then
            strSelection = strSelection & " AND {rptSale.InvoiceNo} LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptSale.AccountID} = " & Val(cmbAccount.SelectedValue)
        End If
        If cmbProduct.Text <> "" Then
            strSelection = strSelection & " AND {rptSale.ProductID}  = " & Val(cmbProduct.SelectedValue)
        End If
        If cmbGroup.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptSale.GroupID} = " & Val(cmbGroup.SelectedValue)
        End If

        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'Sale List From " & txtFromDate.Text & " To " & txtToDate.Text & "'"
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

    Private Sub DisplayInvoice(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        strSelection = "{rptInvoice.ID} = " & Val(lblID.Text)
        Dim iDue As Double
        iDue = cnn.GetBuyerDueByID(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(lblAccountID.Text), cnn.GetDate(Date.Today()))
        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        CrxReport.DataDefinition.FormulaFields("BuyerDue").Text = "'" & iDue & "'"
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

    Private Sub btnInvoice_Click(sender As Object, e As EventArgs) Handles btnInvoice.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Invoice"
        strReportName = Application.StartupPath & "\Reports\Invoice.rpt"

       
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayInvoice(CrxReport, reportDesc)
    End Sub

    Private Sub btnDetailsPrint_Click(sender As Object, e As EventArgs) Handles btnDetailsPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Sale List"
        strReportName = Application.StartupPath & "\Reports\SaleList.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub btnCompactPrint_Click(sender As Object, e As EventArgs) Handles btnCompactPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Sale List"
        strReportName = Application.StartupPath & "\Reports\SaleListCompact.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub btnCancelPrint_Click(sender As Object, e As EventArgs) Handles btnCancelPrint.Click
        framePrint.Visible = False
    End Sub
End Class