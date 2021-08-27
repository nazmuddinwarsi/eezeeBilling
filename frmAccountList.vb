Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmAccountList
    Dim cnn As New DataConn.Conn

    Private Sub frmAccountList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If lblType.Text = "SUPPLIER" Then
            lblCaption.Text = "Supplier List"
        Else
            lblCaption.Text = "Customer List"
        End If
        BindGrid()
    End Sub

    Private Sub frmAccountList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmAccountList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmAccountList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        Grid.Width = MDI.ClientSize.Width - MDI.Panel.Width
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

    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT dbo.tblAccount.ID, dbo.tblAccount.AccountName, dbo.tblAccount.CPerson, dbo.tblAccount.Address1, dbo.tblAccount.Address2, dbo.tblAccount.City, dbo.tblAccount.State, dbo.tblAccount.PIN, dbo.tblAccount.Mobile, dbo.tblAccount.Phone, dbo.tblAccount.EmailID" _
        & ", tblCREATE.fldName AS Create_Name, dbo.tblAccount.CreatedDate, tblEDIT.fldName AS Edit_Name, dbo.tblAccount.EditedDate" _
        & " FROM dbo.tblAccount INNER JOIN dbo.Users AS tblCREATE ON dbo.tblAccount.CreatedBy = tblCREATE.ID LEFT OUTER JOIN dbo.Users AS tblEDIT ON dbo.tblAccount.EditedBy = tblEDIT.ID" _
        & " WHERE dbo.tblAccount.tType='" & lblType.Text & "'"

        If txtName.Text <> "" Then
            strSQL = strSQL & " AND dbo.tblAccount.AccountName LIKE " & cnn.SQLSearch(txtName.Text)
        End If
        If txtPerson.Text <> "" Then
            strSQL = strSQL & " AND dbo.tblAccount.CPerson LIKE " & cnn.SQLSearch(txtPerson.Text)
        End If
        If txtCity.Text <> "" Then
            strSQL = strSQL & " AND dbo.tblAccount.City LIKE " & cnn.SQLSearch(txtCity.Text)
        End If
        If txtState.Text <> "" Then
            strSQL = strSQL & " AND dbo.tblAccount.State LIKE " & cnn.SQLSearch(txtState.Text)
        End If
        If txtMobile.Text <> "" Then
            strSQL = strSQL & " AND dbo.tblAccount.Mobile LIKE " & cnn.SQLSearch(txtMobile.Text)
        End If
        If txtPhone.Text <> "" Then
            strSQL = strSQL & " AND dbo.tblAccount.Phone LIKE " & cnn.SQLSearch(txtPhone.Text)
        End If

        strSQL = strSQL & sSQL & " ORDER BY dbo.tblAccount.AccountName, dbo.tblAccount.CPerson"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Account Name"
        Grid.Columns(2).HeaderText = "Contact Person"
        Grid.Columns(3).HeaderText = "Address 1"
        Grid.Columns(4).HeaderText = "Address 2"
        Grid.Columns(5).HeaderText = "City"
        Grid.Columns(6).HeaderText = "State"
        Grid.Columns(7).HeaderText = "PIN Code"
        Grid.Columns(8).HeaderText = "Mobile"
        Grid.Columns(9).HeaderText = "Phone"
        Grid.Columns(10).HeaderText = "Email ID"
        Grid.Columns(11).HeaderText = "Created By"
        Grid.Columns(12).HeaderText = "Create Date"
        Grid.Columns(13).HeaderText = "Edited By"
        Grid.Columns(14).HeaderText = "Edit Date"
        If chkShow.Checked = True Then
            Grid.Columns(11).Visible = True : Grid.Columns(12).Visible = True : Grid.Columns(13).Visible = True : Grid.Columns(14).Visible = True
        Else
            Grid.Columns(11).Visible = False : Grid.Columns(12).Visible = False : Grid.Columns(13).Visible = False : Grid.Columns(14).Visible = False
        End If
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub frmAccountList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
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
            If cnn.GetUserAccess(4, "fldEdit") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        '--------------------
        Dim strSQL As String
        'strSQL = "SELECT dbo.tblAccount.ID, dbo.tblAccount.AccountName, dbo.tblAccount.CPerson, dbo.tblAccount.Address1, dbo.tblAccount.Address2, dbo.tblAccount.City, dbo.tblAccount.State, dbo.tblAccount.PIN, dbo.tblAccount.Mobile, dbo.tblAccount.Phone, dbo.tblAccount.EmailID" _
        '& ", dbo.tblAccount.TINNo, dbo.tblAccount.Opening FROM dbo.tblAccount WHERE (dbo.tblAccount.tType='" & lblType.Text & "') AND (dbo.tblAccount.ID = " & Val(lblID.Text) & ")"
        strSQL = "SELECT ID, AccountName, CPerson, Address1, Address2, City, State, PIN, Mobile, Phone, EmailID" _
        & ", TINNo, Opening, CGST, SGST, IGST FROM dbo.tblAccount WHERE (dbo.tblAccount.tType='" & lblType.Text & "') AND (dbo.tblAccount.ID = " & Val(lblID.Text) & ")"
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            frmAccount.lblType.Text = lblType.Text
            cnn.ShowForm(frmAccount)
            frmAccount.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("AccountName")) Then
                frmAccount.txtName.Text = dr.Item("AccountName")
            End If
            If Not IsDBNull(dr.Item("CPerson")) Then
                frmAccount.txtCPerson.Text = dr.Item("CPerson")
            End If
            If Not IsDBNull(dr.Item("Address1")) Then
                frmAccount.txtAddress1.Text = dr.Item("Address1")
            End If
            If Not IsDBNull(dr.Item("Address2")) Then
                frmAccount.txtAddress2.Text = dr.Item("Address2")
            End If
            If Not IsDBNull(dr.Item("City")) Then
                frmAccount.txtCity.Text = dr.Item("City")
            End If
            If Not IsDBNull(dr.Item("State")) Then
                frmAccount.txtState.Text = dr.Item("State")
            End If
            If Not IsDBNull(dr.Item("PIN")) Then
                frmAccount.txtPIN.Text = dr.Item("PIN")
            End If
            If Not IsDBNull(dr.Item("Mobile")) Then
                frmAccount.txtMobile.Text = dr.Item("Mobile")
            End If
            If Not IsDBNull(dr.Item("Phone")) Then
                frmAccount.txtPhone.Text = dr.Item("Phone")
            End If
            If Not IsDBNull(dr.Item("EmailID")) Then
                frmAccount.txtEmail.Text = dr.Item("EmailID")
            End If
            If Not IsDBNull(dr.Item("TINNo")) Then
                frmAccount.txtTIN.Text = dr.Item("TINNo")
            End If
            If Not IsDBNull(dr.Item("Opening")) Then
                frmAccount.txtOpening.Text = dr.Item("Opening")
            End If
            If Not IsDBNull(dr.Item("CGST")) Then
                If dr.Item("CGST") = "true" Then
                    frmAccount.chkCGST.Checked = True
                Else
                    frmAccount.chkCGST.Checked = False
                End If
            End If
            If Not IsDBNull(dr.Item("SGST")) Then
                If dr.Item("SGST") = "true" Then
                    frmAccount.chkSGST.Checked = True
                Else
                    frmAccount.chkSGST.Checked = False
                End If
            End If
            If Not IsDBNull(dr.Item("IGST")) Then
                If dr.Item("IGST") = "true" Then
                    frmAccount.chkIGST.Checked = True
                Else
                    frmAccount.chkIGST.Checked = False
                End If
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(4, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        frmAccount.lblType.Text = lblType.Text
        cnn.ShowForm(frmAccount)

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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        frameSearch.Visible = True
        txtName.Focus()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        frameSearch.Visible = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        BindGrid()
        frameSearch.Visible = False
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim CrxReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim strReportName As String
        Dim reportDesc As String
        strReportName = Application.StartupPath & "\Reports\AccountList.rpt"
        If lblType.Text = "SUPPLIER" Then
            reportDesc = "Suppliers List"
        Else
            reportDesc = "Customers List"
        End If

        CrxReport.Load(strReportName, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
        Call DisplayReport(CrxReport, reportDesc)
    End Sub

    Private Sub DisplayReport(ByVal CrxReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal reportDesc As String)
        Dim objfrmRptViewer As New frmRptViewer
        Dim strSelection As String
        strSelection = "{tblAccount.tType} = '" & lblType.Text & "'"
        If txtName.Text <> "" Then
            strSelection = strSelection & " AND {tblAccount.AccountName} LIKE " & cnn.CrystalSearch(txtName.Text)
        End If
        If txtPerson.Text <> "" Then
            strSelection = strSelection & " AND {tblAccount.CPerson} LIKE " & cnn.CrystalSearch(txtPerson.Text)
        End If
        If txtCity.Text <> "" Then
            strSelection = strSelection & " AND {tblAccount.City} LIKE " & cnn.CrystalSearch(txtCity.Text)
        End If
        If txtState.Text <> "" Then
            strSelection = strSelection & " AND {tblAccount.State} LIKE " & cnn.CrystalSearch(txtState.Text)
        End If
        If txtMobile.Text <> "" Then
            strSelection = strSelection & " AND {tblAccount.Mobile} LIKE " & cnn.CrystalSearch(txtMobile.Text)
        End If
        If txtPhone.Text <> "" Then
            strSelection = strSelection & " AND {tblAccount.Phone} LIKE " & cnn.CrystalSearch(txtPhone.Text)
        End If
        '===== Generate report preview =====================
        Cursor.Current = Cursors.WaitCursor
        CrxReport.Refresh()
        CrxReport.DataDefinition.FormulaFields("lblCaption").Text = "'" & lblType.Text & " LIST'"
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteRecord()
    End Sub
    Private Sub DeleteRecord()
        If lblID.Text = "" Then
            cnn.ErrMsgBox("Please Select Record to Delete  !")
            Exit Sub
        End If
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(4, "fldDelete") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        If MessageBox.Show("Do you want to Delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim strSQL As String
        '--------------------------------------------------------------
        strSQL = "Select ID from SaleHeader Where AccountID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this record  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "Select ID from SaleReturnHeader Where AccountID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this record  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "Select ID from PurchaseHeader Where AccountID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this record  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "Select ID from PurchaseReturnHeader Where AccountID =" & Val(lblID.Text)
        If cnn.CheckDuplicate(strSQL) = True Then
            cnn.ErrMsgBox("This Record is in Use, You can't delete this record  !")
            Exit Sub
        End If
        '--------------------------------------------------------------
        strSQL = "DELETE from tblAccount Where ID=" & Val(lblID.Text)
        cnn.executeSQL(strSQL)
        cnn.GenerateLog("Buyer", "Delete", Val(lblID.Text), lblName.Text)
        cnn.InfoMsgBox("Record has been Deleted  !")
        BindGrid()
        lblID.Text = ""
        lblName.Text = ""
    End Sub
End Class