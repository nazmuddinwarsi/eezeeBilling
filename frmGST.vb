Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmGST
    Dim cnn As New DataConn.Conn
    Dim iType As String = ""
    Private Sub frmGST_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Text = DateAdd(DateInterval.Day, -7, Date.Today)
        txtToDate.Text = Date.Today
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        strSQL = "SELECT DISTINCT CONVERT(int, (CGST + SGST + IGST)/2) AS GST FROM dbo.Products ORDER BY GST"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbGST.DisplayMember = "GST"
        cmbGST.ValueMember = "GST"
        cmbGST.DataSource = ds.Tables(0)
        cmbGST.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        FormRes()
    End Sub
    Private Sub BindGrid()
        Try
            Dim strSQL, sSQL As String
            Dim da As New OdbcDataAdapter
            Dim ds As New DataSet
            If iType = "PURCHASE" Then
                strSQL = "Select ID, AccountName, InvoiceNo, InvoiceDate, TotalAmount, TotalCGST, TotalSGST, TotalIGST, BillAmount from rptPurchase" _
                & " WHERE (InvoiceDate >='" & cnn.GetDate(txtFromDate.Text) & "') AND (InvoiceDate <='" & cnn.GetDate(txtToDate.Text) & "')"
                strSQL = strSQL & " AND ((CGSTPercent + SGSTPercent + IGSTPercent) = " & Val(cmbGST.Text) & ")"
                strSQL = strSQL & " GROUP BY ID, AccountName, InvoiceNo, InvoiceDate, TotalAmount, TotalCGST, TotalSGST, TotalIGST, BillAmount Order by InvoiceDate, ID ASC"
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
                Grid.Columns(1).HeaderText = "Supplier Name"
                Grid.Columns(2).HeaderText = "Invoice No"
                Grid.Columns(3).HeaderText = "Invoice Date"
                Grid.Columns(4).HeaderText = "Total Amount"
                Grid.Columns(5).HeaderText = "CGST"
                Grid.Columns(6).HeaderText = "SGST"
                Grid.Columns(7).HeaderText = "IGST"
                Grid.Columns(8).HeaderText = "Bil Amount"
                '=========================
            Else
                strSQL = "Select ID, InvoiceNo, InvoiceDate, TotalAmount, TotalCGST, TotalSGST, TotalIGST, BillAmount from rptInvoice" _
                & " WHERE (InvoiceDate >='" & cnn.GetDate(txtFromDate.Text) & "') AND (InvoiceDate <='" & cnn.GetDate(txtToDate.Text) & "')"
                strSQL = strSQL & " AND ((CGSTPercent + SGSTPercent + IGSTPercent) = " & Val(cmbGST.Text) & ")"
                strSQL = strSQL & " GROUP BY ID, InvoiceNo, InvoiceDate, TotalAmount, TotalCGST, TotalSGST, TotalIGST, BillAmount Order by InvoiceDate, ID ASC"
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
                Grid.Columns(3).HeaderText = "Total Amount"
                Grid.Columns(4).HeaderText = "CGST"
                Grid.Columns(5).HeaderText = "SGST"
                Grid.Columns(6).HeaderText = "IGST"
                Grid.Columns(7).HeaderText = "Bil Amount"
                '=========================
            End If
        Catch ex As Exception
            cnn.ErrMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPurchase_Click(sender As Object, e As EventArgs) Handles btnPurchase.Click
        iType = "PURCHASE"
        BindGrid()
    End Sub

    Private Sub btnSale_Click(sender As Object, e As EventArgs) Handles btnSale.Click
        iType = "SALE"
        BindGrid()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String = ""
        If iType = "PURCHASE" Then
            reportDesc = "Purchase Report"
            strReportName = Application.StartupPath & "\Reports\GSTPurchase.rpt"
        Else
            reportDesc = "Sale Report"
            strReportName = Application.StartupPath & "\Reports\GSTSale.rpt"
        End If
       

        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        If iType = "PURCHASE" Then
            strSelection = "{rptPurchase.InvoiceDate} >= #" & cnn.GetDate(txtFromDate.Text) & "#" _
            & " AND {rptPurchase.InvoiceDate} <= #" & cnn.GetDate(txtToDate.Text) & "#" _
            & " AND {rptPurchase.GSTPercent} = " & Val(cmbGST.Text)
        Else
            strSelection = "{rptSale.InvoiceDate} >= #" & cnn.GetDate(txtFromDate.Text) & "#" _
            & " AND {rptSale.InvoiceDate} <= #" & cnn.GetDate(txtToDate.Text) & "#" _
            & " AND {rptSale.GSTPercent} = " & Val(cmbGST.Text)
            If rbAll.Checked = False Then
                If rbGST.Checked = True Then
                    strSelection = strSelection & " AND {rptSale.TINNo} <> ''"
                End If
                If rbWOGST.Checked = True Then
                    strSelection = strSelection & " AND {rptSale.TINNo} = ''"
                End If

            End If
        End If

        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'GST Report From " & txtFromDate.Text & " To " & txtToDate.Text & "'"
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

    Private Sub frmGST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width '- MDI.Panel.Width
        Grid.Height = Me.Height - 87
        StripPanel.Width = Me.Width
    End Sub
End Class