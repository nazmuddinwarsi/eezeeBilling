Imports System
Imports System.Net
Imports System.Web
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.Odbc
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.ReportSource

Public Class frmReport
    Dim cnn As New DataConn.Conn
    Private reportDoc As New ReportDocument

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================================
        'Dim strSQL As String = "Select * From rptProductGroup"
        'Dim DA As New OdbcDataAdapter(strSQL, cnn.cnn)
        'Dim DS As New DataSet
        'DA.Fill(DS, "rptProductGroup")
        'Dim cr As New ReportDocument
        'Dim strReportPath As String = Application.StartupPath & "\Reports\CategoryList.rpt"
        'cr.Load(strReportPath)
        'cr.SetDataSource(DS.Tables("rptProductGroup"))
        'CrystalReportViewer1.ShowRefreshButton = False
        'CrystalReportViewer1.ShowCloseButton = False
        'CrystalReportViewer1.ShowGroupTreeButton = False
        'CrystalReportViewer1.ReportSource = cr

    End Sub
End Class