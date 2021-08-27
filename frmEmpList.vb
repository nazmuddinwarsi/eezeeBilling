Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmEmpList
    Dim cnn As New DataConn.Conn

    Private Sub frmEmpList_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        BindGrid()
    End Sub

    Private Sub frmEmpList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmEmpList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        '===========================================

    End Sub

    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT tblEmployee.ID, tblEmployee.EmpName, tblEmployee.FatherName, tblEmployee.MobileNo, tblEmployee.fldCity, tblEmployee.fldAddress, tblEmployee.Active, tblCREATE.fldName AS Create_Name, dbo.tblEmployee.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.tblEmployee.EditedDate" _
        & " From tblEmployee INNER JOIN dbo.Users AS tblCREATE ON dbo.tblEmployee.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.tblEmployee.EditedBy = tblEDIT.ID ORDER BY Active DESC, EmpName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Employee Name"
        Grid.Columns(2).HeaderText = "Father Name"
        Grid.Columns(3).HeaderText = "Mobile No"
        Grid.Columns(4).HeaderText = "City"
        Grid.Columns(5).HeaderText = "Address"
        Grid.Columns(6).HeaderText = "Status"
        Grid.Columns(6).Visible = False
        Grid.Columns(7).HeaderText = "Created By"
        Grid.Columns(8).HeaderText = "Create Date"
        Grid.Columns(9).HeaderText = "Edited By"
        Grid.Columns(10).HeaderText = "Edit Date"
        If chkShow.Checked = True Then
            Grid.Columns(7).Visible = True : Grid.Columns(8).Visible = True : Grid.Columns(9).Visible = True : Grid.Columns(10).Visible = True
        Else
            Grid.Columns(7).Visible = False : Grid.Columns(8).Visible = False : Grid.Columns(9).Visible = False : Grid.Columns(10).Visible = False
        End If
    End Sub

    Private Sub frmEmpList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(5, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmAddEmp)
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
            If cnn.GetUserAccess(5, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        '--------------------
        Dim strSQL As String
        strSQL = "Select * from tblEmployee Where (ID = " & Val(lblID.Text) & ")"
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmAddEmp)
            frmAddEmp.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("EmpName")) Then
                frmAddEmp.txtName.Text = dr.Item("EmpName")
            End If
            If Not IsDBNull(dr.Item("FatherName")) Then
                frmAddEmp.txtFatherName.Text = dr.Item("FatherName")
            End If
            If Not IsDBNull(dr.Item("Age")) Then
                frmAddEmp.txtAge.Text = dr.Item("Age")
            End If
            If Not IsDBNull(dr.Item("fldAddress")) Then
                frmAddEmp.txtAddress.Text = dr.Item("fldAddress")
            End If
            If Not IsDBNull(dr.Item("fldCity")) Then
                frmAddEmp.txtCity.Text = dr.Item("fldCity")
            End If
            If Not IsDBNull(dr.Item("MobileNo")) Then
                frmAddEmp.txtMobileNo.Text = dr.Item("MobileNo")
            End If
            If Not IsDBNull(dr.Item("Salary")) Then
                frmAddEmp.txtSalary.Text = dr.Item("Salary")
            End If
            If Not IsDBNull(dr.Item("Active")) Then
                If dr.Item("Active") = "True" Then
                    frmAddEmp.rbActive.Checked = True
                Else
                    frmAddEmp.rbInactive.Checked = True
                End If
            End If
            If Not IsDBNull(dr.Item("LeavingDate")) Then
                frmAddEmp.txtLeavingDate.Text = dr.Item("LeavingDate")
            End If
            If Not IsDBNull(dr.Item("Reason")) Then
                frmAddEmp.txtReason.Text = dr.Item("Reason")
            End If
            
            dr.Close()
            cmd.Connection.Close()
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
            If cnn.GetUserAccess(5, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String
        '--------------------------------------------------------------
        strSQL = "Select ID from tblEmpAttendance Where EmpID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this srecord  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "Select ID from tblEmpSalaryPayment Where EmpID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this srecord  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "DELETE from tblEmployee Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        cnn.GenerateLog("Employee", "Delete", Val(lblID.Text), lblName.Text)
        cnn.InfoMsgBox("Record has been Deleted  !")
        BindGrid() : lblID.Text = "" : lblName.Text = ""
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub Grid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub

    Private Sub Grid_DoubleClick(sender As Object, e As EventArgs) Handles Grid.DoubleClick
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblID.Text = Grid.Rows(crRowIndex).Cells(0).Value.ToString
                lblName.Text = Grid.Rows(crRowIndex).Cells(1).Value.ToString
                EditRecord()
            End If
        End If
    End Sub

    Private Sub chkShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkShow.CheckedChanged
        BindGrid()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String
        strReportName = Application.StartupPath & "\Reports\rptEmployeeList.rpt"
        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        'Dim strSelection As String
        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'Employee List'"
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
End Class