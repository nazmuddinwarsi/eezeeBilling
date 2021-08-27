Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmBranch
    Dim cnn As New DataConn.Conn

    Private Sub frmBranch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        '--------------------------------
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, CompanyName from CompanyMaster ORDER BY CompanyName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbCompany.DisplayMember = "CompanyName"
        cmbCompany.ValueMember = "ID"
        cmbCompany.DataSource = ds.Tables(0)
        cmbCompany.SelectedIndex = -1
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If Trim(cmbCompany.Text) = "" Then
        '    cnn.ErrMsgBox("Please Select Company...!")
        '    cmbCompany.Focus()
        '    Exit Sub
        'End If
        'If Trim(cmbCompany.SelectedValue) = 0 Then
        '    cnn.ErrMsgBox("Please Select Company from List...!")
        '    cmbCompany.Focus()
        '    Exit Sub
        'End If
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Company Name...!")
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtAddress.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Address...!")
            txtAddress.Focus()
            Exit Sub
        End If
        If Trim(txtCity.Text) = "" Then
            cnn.ErrMsgBox("Please Enter City...!")
            txtCity.Focus()
            Exit Sub
        End If

        '---------------
        Try

            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from BranchMaster Where CompanyID=" & Val(cmbCompany.SelectedValue) & " AND BranchName=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            'cnn.OpenConn()
            'cnn.Tran = cnn.cnn.BeginTransaction
            If lblID.Text <> "" Then
                strSQL = "UPDATE BranchMaster Set CompanyID=" & Val(cmbCompany.SelectedValue) & ", BranchName=" & cnn.SQLquote(UCase(txtName.Text))
                strSQL = strSQL & ", Address=" & cnn.SQLquote(UCase(txtAddress.Text)) & ", City=" & cnn.SQLquote(UCase(txtCity.Text))
                strSQL = strSQL & ", State=" & cnn.SQLquote(UCase(txtState.Text)) & ", Country=" & cnn.SQLquote(UCase(txtCountry.Text)) & ", Phone=" & cnn.SQLquote(UCase(txtPhone.Text))
                strSQL = strSQL & ", Fax=" & cnn.SQLquote(UCase(txtFax.Text)) & ", EmailID=" & cnn.SQLquote(txtEmail.Text) & ", BranchGSTinNo=" & cnn.SQLquote(UCase(txtTIN.Text))
                strSQL = strSQL & ", BranchPAN=" & cnn.SQLquote(UCase(txtPAN.Text)) & " Where ID=" & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Branch", "Edit", lblID.Text, UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO BranchMaster (CompanyID, BranchName, Address, City, State, Country, Phone, Fax, EmailID, BranchGSTinNo, BranchCST, BranchPAN, BranchServiceTax, BranchSalesTax) OutPut Inserted.ID as HeaderID VALUES ("
                strSQL = strSQL & Val(cmbCompany.SelectedValue) & "," & cnn.SQLquote(UCase(txtName.Text)) & "," & cnn.SQLquote(txtAddress.Text) & "," & cnn.SQLquote(txtCity.Text) & "," & cnn.SQLquote(txtState.Text) _
                & "," & cnn.SQLquote(txtCountry.Text) & "," & cnn.SQLquote(txtPhone.Text) & "," & cnn.SQLquote(txtFax.Text) & "," & cnn.SQLquote(txtEmail.Text) _
                & "," & cnn.SQLquote(txtTIN.Text) & "," & cnn.SQLquote(txtCST.Text) & "," & cnn.SQLquote(txtPAN.Text) & "," & cnn.SQLquote(txtServiceTax.Text) & "," & cnn.SQLquote(txtSalesTax.Text) & ")"

                Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                If cmd.Connection.State = 1 Then cmd.Connection.Close()
                cmd.Connection.Open()
                Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
                If dr.Read Then
                    lblHeaderID.Text = dr.Item("HeaderID")
                End If
                dr.Close()
                cmd.Connection.Close()

                strSQL = "INSERT INTO Para (SaleInvoiceNo, SMRefNo, PurchaseReturnNo, SaleReturnNo, PaymentRefNo, ReceiptRefNo, dm, dy, MasterCompanyID, MasterBranchID) " _
                & "Select 1, 1, 1, 1, 1, 1, dm, dy, " & Val(cmbCompany.SelectedValue) & "," & Val(lblHeaderID.Text) & " from Para"
                cnn.executeSQL(strSQL)

                cnn.GenerateLog("Branch", "Add", 0, UCase(txtName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = ""
                cmbCompany.Text = ""
                txtName.Text = ""
                txtAddress.Text = ""
                txtCity.Text = ""
                txtState.Text = ""
                txtCountry.Text = ""
                txtPhone.Text = ""
                txtFax.Text = ""
                txtEmail.Text = ""
                txtTIN.Text = ""
                txtCST.Text = ""
                txtPAN.Text = ""
                txtServiceTax.Text = ""
                txtSalesTax.Text = ""
                txtName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub
End Class