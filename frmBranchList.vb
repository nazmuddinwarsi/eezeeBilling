Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmBranchList
    Dim cnn As New DataConn.Conn

    Private Sub frmBranchList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        BindGrid()
    End Sub

    Private Sub frmBranchList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmBranchList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmBranchList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        'strSQL = "SELECT CompanyName, Address, City, State, Country, Phone, EmailID, MasterTIN, MasterCST, MasterPAN, MasterServiceTax, MasterSalesTax" _
        '& " FROM CompanyMaster ORDER BY CompanyName, City"
        'strSQL = "SELECT TOP (100) PERCENT dbo.BranchMaster.ID, dbo.CompanyMaster.CompanyName, dbo.BranchMaster.BranchName, dbo.BranchMaster.Address, dbo.BranchMaster.City, dbo.BranchMaster.State, dbo.BranchMaster.Country, dbo.BranchMaster.Phone, dbo.BranchMaster.EmailID, dbo.BranchMaster.BranchTIN," _
        '& " dbo.BranchMaster.BranchCST, dbo.BranchMaster.BranchPAN, dbo.BranchMaster.BranchServiceTax, dbo.BranchMaster.BranchSalesTax" _
        '& " FROM dbo.BranchMaster INNER JOIN dbo.CompanyMaster ON dbo.BranchMaster.CompanyID = dbo.CompanyMaster.ID ORDER BY dbo.CompanyMaster.CompanyName, dbo.BranchMaster.BranchName"
        strSQL = "SELECT ID, BranchName, Address, City, State, Country, Phone, Fax, EmailID, BranchGSTinNo, BranchPAN FROM BranchMaster ORDER BY BranchName"

        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Company Name"
        Grid.Columns(2).HeaderText = "Address"
        Grid.Columns(3).HeaderText = "City"
        Grid.Columns(4).HeaderText = "State"
        Grid.Columns(5).HeaderText = "Country"
        Grid.Columns(6).HeaderText = "Phone"
        Grid.Columns(7).HeaderText = "Fax"
        Grid.Columns(8).HeaderText = "Email ID"
        Grid.Columns(9).HeaderText = "GST No"
        Grid.Columns(10).HeaderText = "PAN"
        
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub frmBranchList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
            cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
            Exit Sub
        End If
        '--------------------
        Dim strSQL As String
        strSQL = "SELECT TOP (100) PERCENT CompanyID, BranchName, Address, City, State, Country, Phone, Fax, EmailID, BranchGSTinNo, BranchPAN" _
        & " FROM dbo.BranchMaster Where ID=" & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmBranch)
            frmBranch.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("CompanyID")) Then
                frmBranch.cmbCompany.SelectedValue = dr.Item("CompanyID")
            End If
            If Not IsDBNull(dr.Item("BranchName")) Then
                frmBranch.txtName.Text = dr.Item("BranchName")
            End If
            If Not IsDBNull(dr.Item("Address")) Then
                frmBranch.txtAddress.Text = dr.Item("Address")
            End If
            If Not IsDBNull(dr.Item("City")) Then
                frmBranch.txtCity.Text = dr.Item("City")
            End If
            If Not IsDBNull(dr.Item("State")) Then
                frmBranch.txtState.Text = dr.Item("State")
            End If
            If Not IsDBNull(dr.Item("Country")) Then
                frmBranch.txtCountry.Text = dr.Item("Country")
            End If
            If Not IsDBNull(dr.Item("Fax")) Then
                frmBranch.txtFax.Text = dr.Item("Fax")
            End If
            If Not IsDBNull(dr.Item("Phone")) Then
                frmBranch.txtPhone.Text = dr.Item("Phone")
            End If
            If Not IsDBNull(dr.Item("EmailID")) Then
                frmBranch.txtEmail.Text = dr.Item("EmailID")
            End If
            If Not IsDBNull(dr.Item("BranchGSTinNo")) Then
                frmBranch.txtTIN.Text = dr.Item("BranchGSTinNo")
            End If
            If Not IsDBNull(dr.Item("BranchPAN")) Then
                frmBranch.txtPAN.Text = dr.Item("BranchPAN")
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        'If MDI.lblAccessType.Text <> "A" Then
        '    cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
        '    Exit Sub
        'End If
        'cnn.ShowForm(frmBranch)
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class