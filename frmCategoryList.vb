Imports System
Imports System.Net
Imports System.Web
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.Odbc


Public Class frmCategoryList
    Dim cnn As New DataConn.Conn


    Private Sub frmCategoryList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        BindGrid()
    End Sub
    Private Sub frmCategoryList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmCategoryList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmCategoryList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
        GetParentGroup()
    End Sub

    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT TOP (100) PERCENT dbo.ProductGroup.ID, ISNULL(ProductGroup_1.ProductGroup, 'PRIMARY') AS ParentGroup, dbo.ProductGroup.ProductGroup, dbo.ProductGroup.Alias" _
        & " FROM dbo.ProductGroup LEFT OUTER JOIN dbo.ProductGroup AS ProductGroup_1 ON dbo.ProductGroup.Under = ProductGroup_1.ID"
        If txtGroupName.Text <> "" Then
            sSQL = " WHERE dbo.ProductGroup.ProductGroup LIKE " & cnn.SQLSearch(txtGroupName.Text)
        End If
        If txtAlias.Text <> "" Then
            If sSQL <> "" Then
                sSQL = sSQL & " AND dbo.ProductGroup.Alias LIKE " & cnn.SQLSearch(txtGroupName.Text)
            Else
                sSQL = " WHERE dbo.ProductGroup.ProductGroup LIKE " & cnn.SQLSearch(txtGroupName.Text)
            End If
        End If
        If cmbGroup.Text <> "" Then
            If sSQL <> "" Then
                sSQL = sSQL & " AND dbo.ProductGroup.Under = " & Val(cmbGroup.SelectedValue)
            Else
                sSQL = " WHERE dbo.ProductGroup.Under = " & Val(cmbGroup.SelectedValue)
            End If
        End If
        strSQL = strSQL & sSQL & " GROUP BY dbo.ProductGroup.ID, ProductGroup_1.ProductGroup, dbo.ProductGroup.ProductGroup, dbo.ProductGroup.Alias" _
        & " ORDER BY ParentGroup"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Parent Group"
        Grid.Columns(2).HeaderText = "Product Group"
        Grid.Columns(3).HeaderText = "Alias"
        
    End Sub
    Private Sub frmProdGroupList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(2, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmCategory)
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
            If cnn.GetUserAccess(2, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        '--------------------
        Dim strSQL As String
        strSQL = "Select ID, Under, ProductGroup, Alias from ProductGroup Where (ID = " & Val(lblID.Text) & ")"
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmCategory)
            frmCategory.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("Under")) Then
                If dr.Item("Under") = 0 Then
                    frmCategory.chkPrimary.Checked = True
                    frmCategory.cmbGroup.Enabled = False
                Else
                    frmCategory.chkPrimary.Checked = False
                    frmCategory.cmbGroup.SelectedValue = dr.Item("Under")
                End If
            End If
            If Not IsDBNull(dr.Item("ProductGroup")) Then
                frmCategory.txtGroupName.Text = dr.Item("ProductGroup")
            End If
            If Not IsDBNull(dr.Item("Alias")) Then
                frmCategory.txtAlias.Text = dr.Item("Alias")
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

    Private Sub Grid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Grid.KeyDown
        'If Grid.Rows.Count > 1 Then
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
            If cnn.GetUserAccess(2, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String
        '--------------------------------------------------------------
        strSQL = "Select ID from Products Where GroupID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this srecord  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "DELETE from ProductGroup Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        cnn.GenerateLog("Product Group", "Delete", Val(lblID.Text), lblName.Text)
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
        txtGroupName.Focus()
    End Sub
    Private Sub GetParentGroup()
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
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'On Error GoTo m_err2

        ' Declare an application object
        'Dim crxApplication As New CRAXDRT.Application
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        Dim strReportName As String
        Dim reportDesc As String
        'Dim tempValues As CrystalDecisions.Shared.ParameterValues
        'Dim tempDiscreteValue As CrystalDecisions.Shared.ParameterDiscreteValue

        '=== Connect to MS Access ===============

        strReportName = Application.StartupPath & "\Reports\CategoryList.rpt"
        reportDesc = "Category List"

        '==== create an object ======

        'CrxReport = crxApplication.OpenReport(strReportName)

        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        'CrxReport.RecordSelectionFormula = "{rptProductGroup.ProductGroup} = 'TEST GROUP 1'"

        ' ''http://msdn.microsoft.com/en-us/library/ms226530.aspx

        ''tempDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        ''tempValues = New CrystalDecisions.Shared.ParameterValues
        ''tempDiscreteValue.Value = reportDesc
        ''tempValues.Add(tempDiscreteValue)
        ''CrxReport.ParameterFields.Item("ReportTitle").DefaultValues = tempValues
        ' ''CrxReport.ParameterFields.GetItemByName("ReportTitle").AddDefaultValue(reportDesc)

        ''tempDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        ''tempValues = New CrystalDecisions.Shared.ParameterValues
        ''tempDiscreteValue.Value = CStr(Format(Now, "dd/MM/yyyy hh:mm:ss"))
        ''tempValues.Add(tempDiscreteValue)
        ''CrxReport.ParameterFields.Item("fromDate").DefaultValues = tempValues
        ' ''CrxReport.ParameterFields.GetItemByName("fromDate").AddDefaultValue(CStr(Format(Now, "dd/mm/yyyy hh:nn:ss")))


        ''tempDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        ''tempValues = New CrystalDecisions.Shared.ParameterValues
        ''tempDiscreteValue.Value = CStr(Format(Now, "dd/MM/yyyy hh:mm:ss"))
        ''tempValues.Add(tempDiscreteValue)
        ''CrxReport.ParameterFields.Item("toDate").DefaultValues = tempValues
        ' ''CrxReport.ParameterFields.GetItemByName("toDate").AddDefaultValue(CStr(Format(Now, "dd/mm/yyyy hh:nn:ss")))

        ''tempDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        ''tempValues = New CrystalDecisions.Shared.ParameterValues
        ''tempDiscreteValue.Value = CStr(Format(Now, "dd/MM/yyyy hh:mm:ss"))
        ''tempValues.Add(tempDiscreteValue)
        ''CrxReport.ParameterFields.Item("printDateTime").DefaultValues = tempValues
        ' ''CrxReport.ParameterFields.GetItemByName("printDateTime").AddDefaultValue(CStr(Format(Now, "dd/mm/yyyy hh:nn:ss")))

        '=== 11 Nov 2009 =====================
        'Call ResetRptDB(CrxReport)


        Call DisplayReport(CrxReport, reportDesc)

    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)

        'Dim objfrmRptViewer As New frmRptViewer
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String = ""
        If txtGroupName.Text <> "" Then
            strSelection = "{rptProductGroup.ProductGroup} LIKE " & cnn.SQLSearch(txtGroupName.Text)
        End If
        If txtAlias.Text <> "" Then
            If strSelection.ToString <> "" Then
                strSelection = strSelection & " AND {rptProductGroup.Alias} LIKE " & cnn.SQLSearch(txtAlias.Text)
            Else
                strSelection = "{rptProductGroup.Alias} LIKE " & cnn.SQLSearch(txtAlias.Text)
            End If
        End If
        If cmbGroup.Text <> "" Then
            If strSelection.ToString <> "" Then
                strSelection = strSelection & " AND {rptProductGroup.Under} = " & Val(cmbGroup.SelectedValue)
            Else
                strSelection = "{rptProductGroup.Under} = " & Val(cmbGroup.SelectedValue)
            End If
        End If

        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        objfrmRptViewer.CRViewer.ReportSource = CrxReport
        'objfrmRptViewer.CRViewer.SelectionFormula = "{rptProductGroup.ProductGroup} = 'TEST GROUP 1'"
        objfrmRptViewer.CRViewer.RefreshReport()
        objfrmRptViewer.CRViewer.ShowExportButton = True
        objfrmRptViewer.CRViewer.ShowPrintButton = True
        objfrmRptViewer.Text = reportDesc
        objfrmRptViewer.Show()
        ' Maximize the window state so we can view the whole report.
        objfrmRptViewer.WindowState = FormWindowState.Maximized
        ' Zoom the preview window to 100%
        'objfrmRptViewer.CRViewer.Zoom(100)

        'Screen.MousePointer = vbDefault
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