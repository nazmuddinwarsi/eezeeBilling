Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmSupplierStatement
    Dim cnn As New DataConn.Conn

    Private Sub frmSupplierStatement_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmSupplierStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Text = DateAdd(DateInterval.Month, -1, Date.Today)
        txtToDate.Text = Date.Today
        StripPanel.Width = MDI.Width
        '--------------------
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
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
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            cnn.GetSupplierStatement(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(cmbAccount.SelectedValue), cnn.GetDate(txtFromDate.Text), cnn.GetDate(txtToDate.Text))

            Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim strReportName As String
            Dim reportDesc As String
            strReportName = Application.StartupPath & "\Reports\Statement.rpt"
            reportDesc = "Supplier Statement"

            CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
            CrxReport.DataDefinition.FormulaFields("DateRange").Text = "'Supplier Statement From " & txtFromDate.Text & " TO " & txtToDate.Text & "'"
            CrxReport.DataDefinition.FormulaFields("PurSaleAmtText").Text = "'Purchase Amount'"
            CrxReport.DataDefinition.FormulaFields("PayRecAmtText").Text = "'Payment Amount'"
            Call DisplayReport(CrxReport, reportDesc)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String = ""
        '===== Generate report preview =====================

        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        objfrmRptViewer.CRViewer.ReportSource = CrxReport
        'objfrmRptViewer.CRViewer.SelectionFormula = strSelection
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
