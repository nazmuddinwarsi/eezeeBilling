Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmProductList
    Dim cnn As New DataConn.Conn

    Private Sub frmProductList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        BindGrid()
    End Sub

    Private Sub frmProductList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmProductList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmProductList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        Grid.Width = MDI.ClientSize.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = MDI.Width
        ToolStrip1.Cursor = Cursors.Hand
        btnAdd.Image = MDI.ImageList1.Images(0)
        btnEdit.Image = MDI.ImageList1.Images(1)
        btnDelete.Image = MDI.ImageList1.Images(2)
        btnSearch.Image = MDI.ImageList1.Images(3)
        btnPrint.Image = MDI.ImageList1.Images(4)
        btnExit.Image = MDI.ImageList1.Images(5)
        '============================================
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
        da = Nothing
        ds = Nothing
       
    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
       
        'strSQL = "SELECT dbo.Products.ID, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.UnitMaster.UnitName, dbo.Products.SalePrice, dbo.Products.PurchasePrice, dbo.Products.MinLevel, dbo.Products.MaxLevel, dbo.Products.Qty" _
        '& " FROM dbo.Products INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.UnitMaster ON dbo.Products.UnitID = dbo.UnitMaster.ID"
        strSQL = "SELECT dbo.Products.ID, dbo.Products.ProductName, dbo.Products.ProductCode, dbo.ProductGroup.ProductGroup, dbo.Products.SalePrice, dbo.Products.PurchasePrice" _
        & " FROM dbo.Products INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID"
        If txtName.Text <> "" Then
            sSQL = " WHERE dbo.Products.ProductName LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If txtCode.Text <> "" Then
            If sSQL <> "" Then
                sSQL = sSQL & " AND dbo.Products.ProductCode LIKE " & cnn.SQLSearch(txtCode.Text)
            Else
                sSQL = " WHERE dbo.Products.ProductCode LIKE " & cnn.SQLSearch(txtCode.Text)
            End If
        End If
        If cmbGroup.Text <> "" Then
            If sSQL <> "" Then
                sSQL = sSQL & " AND dbo.Products.GroupID = " & Val(cmbGroup.SelectedValue)
            Else
                sSQL = " WHERE dbo.Products.GroupID = " & Val(cmbGroup.SelectedValue)
            End If
        End If
       
        strSQL = strSQL & sSQL & " ORDER BY dbo.Products.ProductName, dbo.Products.ProductCode"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Product Name"
        Grid.Columns(2).HeaderText = "Product Code"
        Grid.Columns(3).HeaderText = "Product Group"
        Grid.Columns(4).HeaderText = "Sale Price"
        Grid.Columns(5).HeaderText = "Purchase Price"

    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub frmProductList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(3, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmProduct)
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
            If cnn.GetUserAccess(3, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        '--------------------
        Dim strSQL As String
        strSQL = "Select ID, GroupID, ProductName, ProductCode, SalePrice, PurchasePrice, CGST, SGST, IGST from Products Where (ID = " & Val(lblID.Text) & ")"
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmProduct)
            frmProduct.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("GroupID")) Then
                frmProduct.cmbGroup.SelectedValue = dr.Item("GroupID")
            End If
            If Not IsDBNull(dr.Item("ProductName")) Then
                frmProduct.txtName.Text = dr.Item("ProductName")
            End If
            If Not IsDBNull(dr.Item("ProductCode")) Then
                frmProduct.txtCode.Text = dr.Item("ProductCode")
            End If
            If Not IsDBNull(dr.Item("SalePrice")) Then
                frmProduct.txtSalePrice.Text = dr.Item("SalePrice")
            End If
            If Not IsDBNull(dr.Item("PurchasePrice")) Then
                frmProduct.txtPurchasePrice.Text = dr.Item("PurchasePrice")
            End If
            If Not IsDBNull(dr.Item("CGST")) Then
                frmProduct.txtCGST.Text = dr.Item("CGST")
            End If
            If Not IsDBNull(dr.Item("SGST")) Then
                frmProduct.txtSGST.Text = dr.Item("SGST")
            End If
            If Not IsDBNull(dr.Item("IGST")) Then
                frmProduct.txtIGST.Text = dr.Item("IGST")
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
    End Sub

    Private Sub Grid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(2).Value.ToString
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
    Private Sub Grid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grid.KeyDown
        'If Grid.Rows.Count > 0 Then
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
            If cnn.GetUserAccess(3, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String
        '--------------------------------------------------------------
        strSQL = "Select ID from PurchaseDetails Where ProductID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this srecord  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "Select ID from SaleReturnDetails Where ProductID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this srecord  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "DELETE from Products Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        cnn.GenerateLog("Products", "Delete", Val(lblID.Text), lblName.Text)
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
        txtName.Focus()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String
        strReportName = Application.StartupPath & "\Reports\rptProductList.rpt"
        reportDesc = "Products List"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub
    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String = ""
        '===== Generate report preview =====================
        strSelection = "{tblProducts.ID} > 0 "
        If txtName.Text <> "" Then
            strSelection = strSelection & " AND {tblProducts.ProductName} LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If txtCode.Text <> "" Then
            strSelection = strSelection & " AND {tblProducts.ProductCode} LIKE " & cnn.SQLSearch(txtCode.Text)
        End If
        If cmbGroup.Text <> "" Then
            strSelection = strSelection & " AND {tblProducts.GroupID} = " & Val(cmbGroup.SelectedValue)
        End If

        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'Product List'"
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
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class