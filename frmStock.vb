Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmStock
    Dim cnn As New DataConn.Conn
    Private Sub frmStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Grid.Width = Me.Width
        BindGrid()
        '=====================================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        'strSQL = "SELECT DISTINCT CGST + SGST + IGST AS GST FROM dbo.Products ORDER BY GST"
        strSQL = "SELECT DISTINCT TOP (100) PERCENT CAST((CGST + SGST + IGST) / 2 AS Decimal(18, 2)) AS GST FROM dbo.Products ORDER BY GST"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbGST.DisplayMember = "GST"
        cmbGST.ValueMember = "GST"
        cmbGST.DataSource = ds.Tables(0)
        cmbGST.SelectedIndex = -1
        ds = Nothing
        da = Nothing
    End Sub

    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT ProductName, ProductCode, BQty From rptStock"
        If cmbGST.Text <> "" Then
            strSQL = strSQL & " WHERE GST = " & Val(cmbGST.Text)
        End If
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "Product Name"
        Grid.Columns(1).HeaderText = "HSN Code"
        Grid.Columns(2).HeaderText = "Qty"
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = "Stock List"
        strReportName = Application.StartupPath & "\Reports\Stock.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        strSelection = "{tblProducts.BQty} >0 "
        If cmbGST.Text <> "" Then
            strSelection = strSelection & " AND {tblProducts.GST} =" & Val(cmbGST.SelectedValue)
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

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        BindGrid()
    End Sub
End Class