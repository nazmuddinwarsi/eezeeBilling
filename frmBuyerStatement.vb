Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmBuyerStatement
    Dim cnn As New DataConn.Conn

    Private Sub frmBuyerStatement_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmBuyerStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Text = DateAdd(DateInterval.Month, -1, Date.Today)
        txtToDate.Text = Date.Today
        StripPanel.Width = MDI.Width
        '--------------------
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If cmbAccount.Text = "" Then
                cnn.ErrMsgBox("Please Select Account Name  !")
                Exit Sub
            End If
            cnn.GetBuyerStatement(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(cmbAccount.SelectedValue), cnn.GetDate(txtFromDate.Text), cnn.GetDate(txtToDate.Text))

            Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim strReportName As String
            Dim reportDesc As String
            strReportName = Application.StartupPath & "\Reports\Statement.rpt"
            reportDesc = "Buyer Statement"

            CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
            CrxReport.DataDefinition.FormulaFields("DateRange").Text = "'" & cmbAccount.Text & " Statement From " & txtFromDate.Text & " TO " & txtToDate.Text & "'"
            CrxReport.DataDefinition.FormulaFields("PurSaleAmtText").Text = "'Sale Amount'"
            CrxReport.DataDefinition.FormulaFields("PayRecAmtText").Text = "'Receive Amount'"
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

    Private Sub btnDues_Click(sender As Object, e As EventArgs) Handles btnDues.Click
        Try
            
            cnn.GetBuyerDuesReport(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), cnn.GetDate(Date.Today()))

            Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim strReportName As String
            Dim reportDesc As String
            strReportName = Application.StartupPath & "\Reports\DuesList.rpt"
            reportDesc = "Buyer Dues List"

            CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
            CrxReport.DataDefinition.FormulaFields("DateRange").Text = "'Buyer Dues List As On " & Format(Date.Today(), "dd-MMM-yyyy") & "'"
            Call DisplayReport(CrxReport, reportDesc)
        Catch ex As Exception

        End Try
    End Sub
End Class