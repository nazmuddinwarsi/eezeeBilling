Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmCompanyList
    Dim cnn As New DataConn.Conn

    Private Sub frmCompanyList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        BindGrid()
    End Sub

    Private Sub frmCompanyList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmCompanyList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmCompanyList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
        '============================================
    End Sub
    Private Sub BindGrid()
        Dim strSQL, sSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        sSQL = ""
        strSQL = "SELECT ID, CompanyName, Address, City, State, Country, Phone, EmailID, MasterTIN, MasterCST, MasterPAN, MasterServiceTax, MasterSalesTax" _
        & " FROM CompanyMaster ORDER BY CompanyName, City"
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
        Grid.Columns(7).HeaderText = "Email ID"
        Grid.Columns(8).HeaderText = "TIN No"
        Grid.Columns(9).HeaderText = "CST No"
        Grid.Columns(10).HeaderText = "PAN"
        Grid.Columns(11).HeaderText = "Serv. Tax No"
        Grid.Columns(12).HeaderText = "Sales Tax No"

    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub frmCompanyList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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
        strSQL = "SELECT CompanyName, Address, City, State, Country, Phone, Fax, EmailID, Website, MasterTIN, MasterCST, MasterPAN, MasterServiceTax, MasterSalesTax" _
        & " FROM CompanyMaster Where ID = " & Val(lblID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            cnn.ShowForm(frmCompany)
            frmCompany.lblID.Text = lblID.Text
            If Not IsDBNull(dr.Item("CompanyName")) Then
                frmCompany.txtName.Text = dr.Item("CompanyName")
            End If
            If Not IsDBNull(dr.Item("Address")) Then
                frmCompany.txtAddress.Text = dr.Item("Address")
            End If
            If Not IsDBNull(dr.Item("City")) Then
                frmCompany.txtCity.Text = dr.Item("City")
            End If
            If Not IsDBNull(dr.Item("State")) Then
                frmCompany.txtState.Text = dr.Item("State")
            End If
            If Not IsDBNull(dr.Item("Country")) Then
                frmCompany.txtCountry.Text = dr.Item("Country")
            End If
            If Not IsDBNull(dr.Item("Phone")) Then
                frmCompany.txtPhone.Text = dr.Item("Phone")
            End If
            If Not IsDBNull(dr.Item("Fax")) Then
                frmCompany.txtFax.Text = dr.Item("Fax")
            End If
            If Not IsDBNull(dr.Item("EmailID")) Then
                frmCompany.txtEmail.Text = dr.Item("EmailID")
            End If
            If Not IsDBNull(dr.Item("Website")) Then
                frmCompany.txtWebsite.Text = dr.Item("Website")
            End If
            'MasterTIN, MasterCST, MasterPAN, MasterServiceTax, MasterSalesTax
            If Not IsDBNull(dr.Item("MasterTIN")) Then
                frmCompany.txtTIN.Text = dr.Item("MasterTIN")
            End If
            If Not IsDBNull(dr.Item("MasterCST")) Then
                frmCompany.txtCST.Text = dr.Item("MasterCST")
            End If
            If Not IsDBNull(dr.Item("MasterPAN")) Then
                frmCompany.txtPAN.Text = dr.Item("MasterPAN")
            End If
            If Not IsDBNull(dr.Item("MasterServiceTax")) Then
                frmCompany.txtServiceTax.Text = dr.Item("MasterServiceTax")
            End If
            If Not IsDBNull(dr.Item("MasterSalesTax")) Then
                frmCompany.txtSalesTax.Text = dr.Item("MasterSalesTax")
            End If
            dr.Close()
            cmd.Connection.Close()
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
            Exit Sub
        End If
        cnn.ShowForm(frmCompany)
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