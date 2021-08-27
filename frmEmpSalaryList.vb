Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmEmpSalaryList
    Dim cnn As New DataConn.Conn

    Private Sub frmEmpSalaryList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmEmpSalaryList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        StripPanel.Width = Me.Width
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        '=============== MONTH ================
        cmbMonth.DataSource = Nothing
        cmbMonth.Items.Clear()
        cmbMonth.DataSource = cnn.BindMonth
        cmbMonth.DisplayMember = "fldMonth"
        cmbMonth.ValueMember = "MonthID"
        cmbMonth.SelectedIndex = -1
    End Sub
    Public Function BindGrid()
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        If rbCompact.Checked = True Then
            btnDelete.Visible = False
            strSQL = "SELECT dbo.tblEmployee.ID, dbo.tblEmployee.EmpName, dbo.tblEmployee.FatherName, dbo.tblEmployee.MobileNo, dbo.tblEmployee.fldCity, " _
            & " ISNULL(dbo.tblEmpSalary.EmpBasicSalary, 0) AS EmpBasicSalary, ISNULL(dbo.tblEmpSalary.EmpGrossSalary, 0) AS EmpGrossSalary, ISNULL(dbo.tblEmpSalary.PaidSalary, 0) AS PaidSalary" _
            & ", tblCREATE.fldName AS Create_Name, dbo.tblEmpSalary.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.tblEmpSalary.EditedDate FROM dbo.tblEmployee LEFT OUTER JOIN dbo.tblEmpSalary ON dbo.tblEmployee.ID = dbo.tblEmpSalary.EmpID" _
            & " INNER JOIN dbo.Users AS tblCREATE ON dbo.tblEmpSalary.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.tblEmpSalary.EditedBy = tblEDIT.ID" _
            & " WHERE (dbo.tblEmpSalary.SalMonth = " & Val(cmbMonth.SelectedValue) & ") AND (dbo.tblEmpSalary.BranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.tblEmployee.Active = 1)"

            da = New OdbcDataAdapter(strSQL, cnn.cnn)
            ds = New DataSet()
            da.Fill(ds)
            Grid.DataSource = ds.Tables(0)
            Grid.Columns(0).HeaderText = "ID"
            Grid.Columns(0).Visible = False
            Grid.Columns(1).HeaderText = "Emp Name"
            Grid.Columns(2).HeaderText = "Father Name"
            Grid.Columns(3).HeaderText = "Mobile No"
            Grid.Columns(4).HeaderText = "City"
            Grid.Columns(5).HeaderText = "Basic Salary"
            Grid.Columns(6).HeaderText = "Gross Salary"
            Grid.Columns(7).HeaderText = "Paid Salary"
            Grid.Columns(8).HeaderText = "Created By"
            Grid.Columns(9).HeaderText = "Create Date"
            Grid.Columns(10).HeaderText = "Edited By"
            Grid.Columns(11).HeaderText = "Edit Date"
            If chkShow.Checked = True Then
                Grid.Columns(8).Visible = True : Grid.Columns(9).Visible = True : Grid.Columns(10).Visible = True : Grid.Columns(11).Visible = True
            Else
                Grid.Columns(8).Visible = False : Grid.Columns(9).Visible = False : Grid.Columns(10).Visible = False : Grid.Columns(11).Visible = False
            End If
        Else
            btnDelete.Visible = True
            strSQL = "SELECT dbo.tblEmpSalaryPayment.ID, dbo.tblEmpSalaryPayment.PaymentDate, dbo.tblEmployee.EmpName, dbo.tblEmployee.FatherName, dbo.tblEmployee.MobileNo, dbo.tblEmployee.fldCity, dbo.tblEmpSalaryPayment.Salary, dbo.tblEmpSalaryPayment.SalaryPayment, dbo.tblEmpSalaryPayment.TotalPaid" _
            & ", tblCREATE.fldName AS Create_Name, dbo.tblEmpSalaryPayment.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.tblEmpSalaryPayment.EditedDate FROM dbo.tblEmployee INNER JOIN dbo.tblEmpSalaryPayment ON dbo.tblEmployee.ID = dbo.tblEmpSalaryPayment.EmpID" _
            & " INNER JOIN dbo.Users AS tblCREATE ON dbo.tblEmpSalaryPayment.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.tblEmpSalaryPayment.EditedBy = tblEDIT.ID" _
            & " WHERE (dbo.tblEmpSalaryPayment.SalMonth = " & Val(cmbMonth.SelectedValue) & ") AND (dbo.tblEmpSalaryPayment.BranchID = " & Val(MDI.lblMasterBranchID.Text) & ")"

            da = New OdbcDataAdapter(strSQL, cnn.cnn)
            ds = New DataSet()
            da.Fill(ds)
            Grid.DataSource = ds.Tables(0)
            Grid.Columns(0).HeaderText = "ID"
            Grid.Columns(0).Visible = False
            Grid.Columns(1).HeaderText = "Date"
            Grid.Columns(2).HeaderText = "Emp Name"
            Grid.Columns(3).HeaderText = "Father Name"
            Grid.Columns(4).HeaderText = "Mobile No"
            Grid.Columns(5).HeaderText = "City"
            Grid.Columns(6).HeaderText = "Basic Salary"
            Grid.Columns(7).HeaderText = "Salary Pay"
            Grid.Columns(8).HeaderText = "Net Pay"
            Grid.Columns(9).HeaderText = "Created By"
            Grid.Columns(10).HeaderText = "Create Date"
            Grid.Columns(11).HeaderText = "Edited By"
            Grid.Columns(12).HeaderText = "Edit Date"
            If chkShow.Checked = True Then
                Grid.Columns(9).Visible = True : Grid.Columns(10).Visible = True : Grid.Columns(11).Visible = True : Grid.Columns(12).Visible = True
            Else
                Grid.Columns(9).Visible = False : Grid.Columns(10).Visible = False : Grid.Columns(11).Visible = False : Grid.Columns(12).Visible = False
            End If
        End If
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid()
    End Sub

    Private Sub btnOpenPrint_Click(sender As Object, e As EventArgs) Handles btnOpenPrint.Click
        DisplayReport()
    End Sub

    Private Sub DisplayReport()
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim objfrmRptViewer As New frmRptViewer

        Dim strSelection As String = ""
        If rbCompact.Checked = True Then
            strReportName = Application.StartupPath & "\Reports\rptEmpSalaryCompact.rpt"
            strSelection = "{rptEmpSalaryCompact.SalMonth} = " & Val(cmbMonth.SelectedValue) & " AND {rptEmpSalaryCompact.BranchID} = " & Val(MDI.lblMasterBranchID.Text)
        Else
            strReportName = Application.StartupPath & "\Reports\rptEmpSalaryDetails.rpt"
            strSelection = "{rptEmpSalaryDetails.SalMonth} = " & Val(cmbMonth.SelectedValue) & " AND {rptEmpSalaryDetails.BranchID} = " & Val(MDI.lblMasterBranchID.Text)
        End If


        '===== Generate report preview =====================

        Cursor.Current = Cursors.WaitCursor
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        CrxReport.Refresh()
        objfrmRptViewer.CRViewer.ReportSource = CrxReport
        objfrmRptViewer.CRViewer.SelectionFormula = strSelection
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'Employee Salary List'"
        objfrmRptViewer.CRViewer.RefreshReport()
        objfrmRptViewer.CRViewer.ShowExportButton = True
        objfrmRptViewer.CRViewer.ShowPrintButton = True
        objfrmRptViewer.Text = "Employee Salary List"
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

    Private Sub frmEmpSalaryList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = MDI.ClientSize.Width - MDI.Panel.Width
        Grid.Height = Me.Height - 100
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(14, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpSalary)
    End Sub

    Private Sub Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
            End If
        End If
    End Sub

    Private Sub Grid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If Val(lblID.Text) = 0 Then
            cnn.ErrMsgBox("Please Select Record to delete !!")
            Exit Sub
        End If
        '------------------------------
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim strSQL As String
        Dim iSalMonth, iSalYear, iSal, iEmpID As Integer
        strSQL = "Select * From tblEmpSalaryPayment Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = ConnectionState.Open Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            iEmpID = dr.Item("EmpID") : iSalMonth = dr.Item("SalMonth") : iSal = dr.Item("SalaryPayment")
            'iEmpID = dr.Item("BranchID")
        End If
        dr.Close() : cmd.Connection.Close()
        strSQL = "UPDATE tblEmpSalary SET PaidSalary = (PaidSalary - " & Val(iSal) & ") Where EmpID=" & Val(iEmpID) & " AND SalMonth=" & Val(iSalMonth)
        cnn.executeSQL(strSQL)
        strSQL = "DELETE From tblEmpSalaryPayment Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)


        cnn.InfoMsgBox("Record has been Deleted  !")
        BindGrid()
    End Sub
End Class