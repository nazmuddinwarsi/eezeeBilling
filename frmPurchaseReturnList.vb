Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmPurchaseReturnList
    Dim cnn As New DataConn.Conn

    Private Sub frmPurchaseReturnList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtFromDate.Text = Date.Today 'DateAdd(DateInterval.Month, -1, Date.Today)
        txtToDate.Text = Date.Today
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT dbo.PurchaseReturnHeader.ID, dbo.PurchaseReturnHeader.RefNo, dbo.PurchaseReturnHeader.RefDate, dbo.tblAccount.AccountName, dbo.PurchaseReturnHeader.TotalQty, dbo.PurchaseReturnHeader.TotalAmount, dbo.PurchaseReturnHeader.BillAmount, dbo.PurchaseReturnHeader.Remarks" _
        & ", tblCREATE.fldName AS Create_Name, dbo.PurchaseReturnHeader.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.PurchaseReturnHeader.EditedDate FROM dbo.PurchaseReturnDetails INNER JOIN dbo.PurchaseReturnHeader ON dbo.PurchaseReturnDetails.HeaderID = dbo.PurchaseReturnHeader.ID INNER JOIN dbo.tblAccount ON dbo.PurchaseReturnHeader.AccountID = dbo.tblAccount.ID INNER JOIN dbo.Products ON dbo.PurchaseReturnDetails.ProductID = dbo.Products.ID" _
        & " INNER JOIN dbo.Users AS tblCREATE ON dbo.PurchaseReturnHeader.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.PurchaseReturnHeader.EditedBy = tblEDIT.ID" _
        & " WHERE (dbo.PurchaseReturnHeader.MasterCompanyID =" & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.PurchaseReturnHeader.MasterBranchID =" & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " AND (dbo.PurchaseReturnHeader.RefDate >='" & cnn.GetDate(txtFromDate.Text) & "') AND (dbo.PurchaseReturnHeader.RefDate <='" & cnn.GetDate(txtToDate.Text) & "')"
        If txtInvNo.Text <> "" Then
            strSQL = strSQL & " AND dbo.PurchaseReturnHeader.RefNo LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.Text <> "" Then
            strSQL = strSQL & " AND dbo.PurchaseReturnHeader.AccountID = " & Val(cmbAccount.SelectedValue)
        End If
        If txtName.Text <> "" Then
            strSQL = strSQL & " AND dbo.Products.ProductName LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If cmbGroup.Text <> "" Then
            strSQL = strSQL & " AND dbo.Products.GroupID = " & Val(cmbGroup.SelectedValue)
        End If
        strSQL = strSQL & " GROUP BY dbo.PurchaseReturnHeader.ID, dbo.PurchaseReturnHeader.RefNo, dbo.PurchaseReturnHeader.RefDate, dbo.tblAccount.AccountName, dbo.PurchaseReturnHeader.TotalQty, dbo.PurchaseReturnHeader.TotalAmount, dbo.PurchaseReturnHeader.BillAmount, dbo.PurchaseReturnHeader.Remarks, tblCREATE.fldName, dbo.PurchaseReturnHeader.CreatedDate, tblEDIT.fldName, dbo.PurchaseReturnHeader.EditedDate"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Ref No"
        Grid.Columns(2).HeaderText = "Ref Date"
        Grid.Columns(3).HeaderText = "Supplier Name"
        Grid.Columns(4).HeaderText = "Total Weight"
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
    End Sub

    Private Sub frmPurchaseReturnList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmPurchaseReturnList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmPurchaseReturnList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

    Private Sub frmPurchaseReturnList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = (Me.Height / 2) - 37
        Grid2.Height = (Me.Height / 2) - 60
        Grid2.Top = Grid.Height + 90
        Grid2.Width = MDI.ClientSize.Width
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
        'cnn.ShowForm(frmPurchaseReturn)
        ''--------------------
        Dim strSQL As String
        strSQL = "Select ID, RefNo, RefDate, AccountID, TotalQty, TotalAmount, BillAmount, Remarks from PurchaseReturnHeader Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmPurchaseReturn)
            frmPurchaseReturn.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("RefNo")) Then
                frmPurchaseReturn.txtInvoiceNo.Text = dr.Item("RefNo")
            End If
            If Not IsDBNull(dr.Item("RefDate")) Then
                frmPurchaseReturn.txtDate.Text = dr.Item("RefDate")
            End If
            If Not IsDBNull(dr.Item("AccountID")) Then
                frmPurchaseReturn.cmbSupplier.SelectedValue = dr.Item("AccountID")
            End If
            If Not IsDBNull(dr.Item("TotalQty")) Then
                frmPurchaseReturn.txtTotalQty.Text = dr.Item("TotalQty")
            End If
            If Not IsDBNull(dr.Item("TotalAmount")) Then
                frmPurchaseReturn.txtTotalAmount.Text = dr.Item("TotalAmount")
            End If
            If Not IsDBNull(dr.Item("BillAmount")) Then
                frmPurchaseReturn.txtBillAmount.Text = dr.Item("BillAmount")
            End If
            If Not IsDBNull(dr.Item("Remarks")) Then
                frmPurchaseReturn.txtRemarks.Text = dr.Item("Remarks")
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
        '---------------------------------------
        strSQL = "SELECT dbo.Products.ID, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.PurchaseReturnDetails.Qty, dbo.PurchaseReturnDetails.Nos, dbo.PurchaseReturnDetails.Rate, dbo.PurchaseReturnDetails.Amount" _
                & " FROM dbo.PurchaseReturnDetails INNER JOIN dbo.Products ON dbo.PurchaseReturnDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.PurchaseReturnHeader ON dbo.PurchaseReturnDetails.HeaderID = dbo.PurchaseReturnHeader.ID" _
                & " WHERE (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ")"
        frmPurchaseReturn.Grid.Rows.Clear()
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader

        Do While dr.Read
            frmPurchaseReturn.Grid.Rows.Add(1)
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(0).Value = dr.Item("ID")
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(1).Value = dr.Item("ProductCode")
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(2).Value = dr.Item("ProductName")
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(3).Value = dr.Item("Qty")
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(4).Value = dr.Item("Nos")
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(5).Value = dr.Item("Rate")
            frmPurchaseReturn.Grid.Rows(frmPurchaseReturn.Grid.Rows.Count - 1).Cells(6).Value = dr.Item("Amount")
        Loop

        '---------------------------------------
        strSQL = "SELECT dbo.BillSundryDetails.SundryID, dbo.BillSundryMaster.SundryName, dbo.BillSundryDetails.Value, dbo.BillSundryDetails.Amount, dbo.BillSundryMaster.SundryType, dbo.BillSundryMaster.AmountAs, dbo.BillSundryMaster.PercentOf, dbo.BillSundryMaster.RoundOff, dbo.BillSundryMaster.RoundOffType , dbo.BillSundryNature.Type" _
        & " FROM dbo.BillSundryDetails INNER JOIN dbo.BillSundryMaster ON dbo.BillSundryDetails.SundryID = dbo.BillSundryMaster.ID INNER JOIN dbo.BillSundryNature ON dbo.BillSundryMaster.NatureID = dbo.BillSundryNature.ID" _
        & " WHERE (dbo.BillSundryDetails.HeaderID = " & Val(lblID.Text) & ") AND (dbo.BillSundryDetails.vType = 'PURCHASERETURN')"

        frmPurchaseReturn.grdTax.Rows.Clear()
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader

        Do While dr.Read
            frmPurchaseReturn.grdTax.Rows.Add(1)
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(0).Value = dr.Item("SundryID")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(1).Value = dr.Item("SundryName")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(2).Value = dr.Item("Value")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(3).Value = dr.Item("Amount")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(4).Value = dr.Item("SundryType")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(5).Value = dr.Item("AmountAs")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(6).Value = dr.Item("PercentOf")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(7).Value = dr.Item("RoundOff")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(8).Value = dr.Item("RoundOffType")
            frmPurchaseReturn.grdTax.Rows(frmPurchaseReturn.grdTax.Rows.Count - 1).Cells(9).Value = dr.Item("Type")
        Loop

        frmPurchaseReturn.GridTotal()
        frmPurchaseReturn.CalculateSundryGrid()
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

    Private Sub Grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellContentClick

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
        strSQL = "SELECT dbo.PurchaseReturnDetails.Qty AS PurchaseReturnQty, dbo.ProductStock.Qty AS StockQty, dbo.PurchaseReturnDetails.HeaderID, dbo.PurchaseReturnDetails.ProductID" _
                & " FROM dbo.PurchaseReturnDetails INNER JOIN dbo.ProductStock ON dbo.PurchaseReturnDetails.ProductID = dbo.ProductStock.ProductID" _
                & " WHERE (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ")" _
                & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
                & " AND (dbo.ProductStock.SMID = 0)"

        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        While dr.Read
            If (Val(dr.Item("StockQty")) + Val(dr.Item("PurchaseReturnQty"))) < 0 Then
                cnn.ErrMsgBox("You can't delete this record due to Stock Validation, Please check  !")
                dr.Close()
                cmd.Connection.Close()
                Exit Sub
            End If
        End While
        dr.Close()
        cmd.Connection.Close()
        '--------------------------------------- STOCK REVERSE
        strSQL = "UPDATE ProductStock SET dbo.ProductStock.Qty = (dbo.ProductStock.Qty + dbo.PurchaseReturnDetails.Qty)" _
             & " FROM dbo.ProductStock INNER JOIN dbo.PurchaseReturnDetails ON dbo.ProductStock.ProductID = dbo.PurchaseReturnDetails.ProductID" _
             & " WHERE (dbo.ProductStock.ProductID = dbo.PurchaseReturnDetails.ProductID) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")" _
             & " AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ")" _
             & " AND (dbo.ProductStock.SMID = 0)"
        '--------------------------------------- STOCK REVERSE
        strSQL = "DELETE from PurchaseReturnDetails Where HeaderID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        strSQL = "DELETE from PurchaseReturnDetails Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)

        cnn.GenerateLog("Purchase Return", "Delete", Val(lblID.Text), lblName.Text)
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
        cnn.ShowForm(frmPurchaseReturn)
    End Sub
    Private Sub GetDetails()
        If lblID.Text <> "" Then
            Dim strSQL, sSQL As String
            Dim da As New OdbcDataAdapter
            Dim ds As New DataSet
            sSQL = ""
            'strSQL = "SELECT dbo.PurchaseReturnDetails.HeaderID, dbo.PurchaseReturnHeader.RefNo, dbo.PurchaseReturnHeader.RefDate, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.UnitMaster.UnitName, dbo.PurchaseReturnDetails.Qty, dbo.PurchaseReturnDetails.Rate, dbo.PurchaseReturnDetails.Amount" _
            '& " FROM dbo.PurchaseReturnDetails INNER JOIN dbo.Products ON dbo.PurchaseReturnDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.UnitMaster ON dbo.Products.UnitID = dbo.UnitMaster.ID INNER JOIN dbo.PurchaseReturnHeader ON dbo.PurchaseReturnDetails.HeaderID = dbo.PurchaseReturnHeader.ID" _
            '& " WHERE (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ")"
            strSQL = "SELECT dbo.PurchaseReturnDetails.HeaderID, dbo.PurchaseReturnHeader.RefNo, dbo.PurchaseReturnHeader.RefDate, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.PurchaseReturnDetails.Qty, dbo.PurchaseReturnDetails.Rate, dbo.PurchaseReturnDetails.Amount" _
                & " FROM dbo.PurchaseReturnDetails INNER JOIN dbo.Products ON dbo.PurchaseReturnDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.PurchaseReturnHeader ON dbo.PurchaseReturnDetails.HeaderID = dbo.PurchaseReturnHeader.ID" _
                & " WHERE (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ")"
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
            Grid2.Columns(6).HeaderText = "Weight"
            Grid2.Columns(7).HeaderText = "Rate"
            Grid2.Columns(8).HeaderText = "Amount"
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Purchase Return List"
        strReportName = Application.StartupPath & "\Reports\PurchaseReturnList.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        strSelection = "{rptPurchaseReturn.RefDate} >= #" & cnn.GetDate(txtFromDate.Text) & "#" _
        & " AND {rptPurchaseReturn.RefDate} <= #" & cnn.GetDate(txtToDate.Text) & "#" _
        & " AND {rptPurchaseReturn.MasterBranchID} = " & Val(MDI.lblMasterBranchID.Text)
        If txtInvNo.Text <> "" Then
            strSelection = strSelection & " AND {rptPurchaseReturn.RefNo} LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptPurchaseReturn.AccountID} = " & Val(cmbAccount.SelectedValue)
        End If
        If txtName.Text <> "" Then
            strSelection = strSelection & " AND {rptPurchaseReturn.ProductName} LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If cmbGroup.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptPurchaseReturn.GroupID} = " & Val(cmbGroup.SelectedValue)
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
End Class