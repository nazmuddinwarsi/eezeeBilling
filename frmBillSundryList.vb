Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmBillSundryList
    Dim cnn As New DataConn.Conn

    Private Sub frmBillSundryList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        'strSQL = "SELECT dbo.BillSundryMaster.ID, dbo.BillSundryMaster.SundryName, dbo.BillSundryMaster.DefaultValue, CASE WHEN dbo.BillSundryMaster.SundryType = 'ADD' THEN 'ADDITIVE' ELSE 'SUBSTRACTIVE' END AS SundryType, dbo.BillSundryNature.SundryNature, dbo.BillSundryMaster.RoundOff, dbo.BillSundryMaster.RoundOffType, dbo.BillSundryMaster.AmountAs," _
        '& " dbo.BillSundryMaster.PercentOf FROM dbo.BillSundryMaster INNER JOIN dbo.BillSundryNature ON dbo.BillSundryMaster.NatureID = dbo.BillSundryNature.ID"
        'If cmbNature.Text <> "" Then
        '    sSQL = " WHERE dbo.BillSundryMaster.NatureID = " & Val(cmbNature.SelectedValue)
        'End If
        'strSQL = strSQL & sSQL & " ORDER BY dbo.BillSundryMaster.SundryName"
        strSQL = "SELECT ID, SundryName, DefaultValue, CASE WHEN SundryType = 'ADD' THEN 'ADDITIVE' ELSE 'SUBSTRACTIVE' END AS SundryType, RoundOff, RoundOffType, AmountAs, PercentOf FROM dbo.BillSundryMaster" _
        & " ORDER BY SundryName"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Sundry Name"
        Grid.Columns(2).HeaderText = "Default Value"
        Grid.Columns(3).HeaderText = "Sundry Type"
        Grid.Columns(4).HeaderText = "Round Off"
        Grid.Columns(5).HeaderText = "Round Off Type"
        Grid.Columns(6).HeaderText = "Amount As"
        Grid.Columns(7).HeaderText = "Percent Of"
        Grid.Columns(3).Visible = False
        Grid.Columns(4).Visible = False
        Grid.Columns(5).Visible = False
        Grid.Columns(6).Visible = False
        Grid.Columns(7).Visible = False
    End Sub
    Private Sub frmBillSundryList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmBillSundryList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmBillSundryList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, SundryNature from BillSundryNature Order BY SundryNature ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbNature.DisplayMember = "SundryNature"
        cmbNature.ValueMember = "ID"
        cmbNature.DataSource = ds.Tables(0)
        cmbNature.SelectedIndex = -1
    End Sub

    Private Sub frmBillSundryList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
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
            cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
            Exit Sub
        End If
        ''--------------------
        Dim strSQL As String
        strSQL = "Select ID, SundryName, DefaultValue, SundryType, NatureID, RoundOff, RoundOffType, AmountAs, PercentOf from BillSundryMaster Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmBillSundry)
            frmBillSundry.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("SundryName")) Then
                frmBillSundry.txtName.Text = dr.Item("SundryName")
            End If
            If Not IsDBNull(dr.Item("DefaultValue")) Then
                frmBillSundry.txtValue.Text = dr.Item("DefaultValue")
            End If
            If Not IsDBNull(dr.Item("NatureID")) Then
                frmBillSundry.cmbNature.SelectedValue = dr.Item("NatureID")
            End If
            If Not IsDBNull(dr.Item("SundryType")) Then
                If dr.Item("SundryType") = "ADD" Then
                    frmBillSundry.optAdd.Checked = True
                Else
                    frmBillSundry.optSub.Checked = True
                End If
            End If
            If Not IsDBNull(dr.Item("RoundOff")) Then
                If dr.Item("RoundOff") = "Y" Then
                    frmBillSundry.chkRoundOff.Checked = True
                Else
                    frmBillSundry.chkRoundOff.Checked = False
                End If
            End If
            If Not IsDBNull(dr.Item("RoundOffType")) Then
                If dr.Item("RoundOffType") <> "" Then
                    If dr.Item("RoundOffType") = "AUTO" Then
                        frmBillSundry.optAuto.Checked = True
                    ElseIf dr.Item("RoundOffType") = "UP" Then
                        frmBillSundry.optUp.Checked = True
                    ElseIf dr.Item("RoundOffType") = "LOW" Then
                        frmBillSundry.optLow.Checked = True
                    End If
                End If
            End If
            If Not IsDBNull(dr.Item("AmountAs")) Then
                If dr.Item("AmountAs") = "AMT" Then
                    frmBillSundry.optAmt.Checked = True
                Else
                    frmBillSundry.optPer.Checked = True
                End If
            End If
            If Not IsDBNull(dr.Item("PercentOf")) Then
                If dr.Item("PercentOf") <> "" Then
                    If dr.Item("PercentOf") = "BASIC" Then
                        frmBillSundry.optBasic.Checked = True
                    ElseIf dr.Item("PercentOf") = "TAX" Then
                        frmBillSundry.optTaxable.Checked = True
                    ElseIf dr.Item("PercentOf") = "BILL" Then
                        frmBillSundry.optBill.Checked = True
                    ElseIf dr.Item("PercentOf") = "PREV" Then
                        frmBillSundry.optPrevious.Checked = True
                    End If
                End If
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
            Exit Sub
        End If
        cnn.ShowForm(frmBillSundry)
    End Sub
End Class