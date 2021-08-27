Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmReceiptList
    Dim cnn As New DataConn.Conn

    Private Sub frmReceiptList_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        txtFromDate.Text = Date.Today 'DateAdd(DateInterval.Month, -1, Date.Today)
        txtToDate.Text = Date.Today
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT ID, ReceiptNo, ReceiptDate, PayMode, Amount, AccountName, cPerson, City, Mobile, Create_Name, CreatedDate, Edit_Name, EditedDate from rptReceipt " _
        & " WHERE (MasterCompanyID =" & Val(MDI.lblMasterCompanyID.Text) & ") AND (MasterBranchID =" & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " AND (ReceiptDate >='" & cnn.GetDate(txtFromDate.Text) & "') AND (ReceiptDate <='" & cnn.GetDate(txtToDate.Text) & "')"
        If txtInvNo.Text <> "" Then
            strSQL = strSQL & " AND ReceiptNo LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.Text <> "" Then
            strSQL = strSQL & " AND AccountID = " & Val(cmbAccount.SelectedValue)
        End If

        strSQL = strSQL & " Order by ReceiptDate DESC"

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
        Grid.Columns(1).HeaderText = "Ref No"
        Grid.Columns(2).HeaderText = "Receipt Date"
        Grid.Columns(3).HeaderText = "Pay Mode"
        Grid.Columns(4).HeaderText = "Amount"
        Grid.Columns(5).HeaderText = "Buyer Name"
        Grid.Columns(6).HeaderText = "Contact Person"
        Grid.Columns(7).HeaderText = "City"
        Grid.Columns(8).HeaderText = "Mobile"
        Grid.Columns(9).HeaderText = "Created By"
        Grid.Columns(10).HeaderText = "Create Date"
        Grid.Columns(11).HeaderText = "Edited By"
        Grid.Columns(12).HeaderText = "Edit Date"
        If chkShow.Checked = True Then
            Grid.Columns(9).Visible = True : Grid.Columns(10).Visible = True : Grid.Columns(11).Visible = True : Grid.Columns(12).Visible = True
        Else
            Grid.Columns(9).Visible = False : Grid.Columns(10).Visible = False : Grid.Columns(11).Visible = False : Grid.Columns(12).Visible = False
        End If
        '=========================


    End Sub

    Private Sub frmReceiptList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmReceiptList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    End Sub

    Private Sub frmReceiptList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width '- MDI.Panel.Width
        Grid.Height = (Me.Height) - 37

        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        EditRecord()
    End Sub
    Private Sub EditRecord()
        If lblID.Text = "" Then
            cnn.ErrMsgBox("Please Select Record to Modify  !")
            Exit Sub
        End If
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(9, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        'cnn.ShowForm(frmPurchase)
        ''--------------------
        Dim strSQL As String
        strSQL = "Select * from Receipt Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmReceipt)
            frmReceipt.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("ReceiptNo")) Then
                frmReceipt.txtRefNo.Text = dr.Item("ReceiptNo")
            End If
            If Not IsDBNull(dr.Item("ReceiptDate")) Then
                frmReceipt.txtDate.Text = dr.Item("ReceiptDate")
            End If
            If Not IsDBNull(dr.Item("PayMode")) Then
                If dr.Item("PayMode") = "CASH" Then
                    frmReceipt.rbCash.Checked = True
                Else
                    frmReceipt.rbChq.Checked = True
                End If
            End If

            If Not IsDBNull(dr.Item("AccountID")) Then
                frmReceipt.cmbSupplier.SelectedValue = dr.Item("AccountID")
            End If
            If Not IsDBNull(dr.Item("BankID")) Then
                frmReceipt.cmbBank.SelectedValue = dr.Item("BankID")
            End If
            If Not IsDBNull(dr.Item("Amount")) Then
                frmReceipt.txtAmount.Text = dr.Item("Amount")
            End If
            If Not IsDBNull(dr.Item("Remarks")) Then
                frmReceipt.txtRemarks.Text = dr.Item("Remarks")
            End If

            dr.Close()
            cmd.Connection.Close()
        End If

    End Sub

    Private Sub Grid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub

    Private Sub Grid_DoubleClick(sender As Object, e As EventArgs) Handles Grid.DoubleClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(2).Value.ToString
                EditRecord()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteRecord()
    End Sub
    Private Sub DeleteRecord()
        If lblID.Text = "" Then
            cnn.ErrMsgBox("Please Select Record to Delete  !")
            Exit Sub
        End If
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(9, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String

        strSQL = "DELETE from Receipt Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)

        cnn.GenerateLog("Reciept", "Delete", Val(lblID.Text), lblName.Text)
        cnn.InfoMsgBox("Record has been Deleted  !")
        BindGrid()
        lblID.Text = ""
        lblName.Text = ""
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frameSearch.Visible = False
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        BindGrid()
        frameSearch.Visible = False
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        frameSearch.Visible = True
        txtFromDate.Focus()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(9, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmReceipt)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Receipt List"
        strReportName = Application.StartupPath & "\Reports\ReceiptList.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        strSelection = "{rptReceipt.ReceiptDate} >= #" & cnn.GetDate(txtFromDate.Text) & "#" _
        & " AND {rptReceipt.ReceiptDate} <= #" & cnn.GetDate(txtToDate.Text) & "#" _
        & " AND {rptReceipt.MasterBranchID} = " & Val(MDI.lblMasterBranchID.Text)
        If txtInvNo.Text <> "" Then
            strSelection = strSelection & " AND {rptReceipt.ReceiptNo} LIKE " & cnn.SQLSearch(txtInvNo.Text)
        End If
        If cmbAccount.SelectedValue > 0 Then
            strSelection = strSelection & " AND {rptReceipt.AccountID} = " & Val(cmbAccount.SelectedValue)
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

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class