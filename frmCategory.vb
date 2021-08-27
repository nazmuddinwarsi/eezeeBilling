Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmCategory
    Dim cnn As New DataConn.Conn

    Private Sub frmCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        ''==============================
        GetParentGroup()
        Dim a As String
        Dim b As String
        Dim i As Integer

        a = "SHOW ME"
        b = ""
        For i = 0 To Len(a)
            b = b & Mid(a, i + 1, 1) & vbCrLf
        Next i

        btnShow.Text = b
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
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        treeView.Nodes.Clear()
        Call InsertNodes(Nothing, 0)
    End Sub
    Private Sub InsertNodes(ByVal n As TreeNode, ByVal module_id As Integer)
        Dim Conn As Data.Odbc.OdbcConnection = New Data.Odbc.OdbcConnection(cnn._connString)
        Dim cmd As New Data.Odbc.OdbcCommand("SELECT ID, ProductGroup FROM ProductGroup WHERE Under=" & module_id, Conn)
        cmd.Connection.Open()
        Dim rdr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader()
        While rdr.Read()
            Dim t As New TreeNode(rdr("ProductGroup").ToString())
            InsertNodes(t, Convert.ToInt16(rdr("ID").ToString()))
            If n Is Nothing Then
                treeView.Nodes.Add(t)
            Else
                n.Nodes.Add(t)
            End If
        End While
        rdr.Close()
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Trim(txtGroupName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Group Name...!")
            txtGroupName.Focus()
            Exit Sub
        End If
        If chkPrimary.Checked = False Then
            If cmbGroup.Text = "" Then
                cnn.ErrMsgBox("Please Select Group Name...!")
                cmbGroup.Focus()
                Exit Sub
            Else
                If cmbGroup.FindString(cmbGroup.Text) < 0 Then
                    cnn.ErrMsgBox("Please Select from List...!")
                    cmbGroup.Focus()
                    Exit Sub
                End If
            End If
        End If
        '---------------
        Try

            Dim strSQL As String
            Dim uID As Integer
            If chkPrimary.Checked = True Then
                uID = 0
            Else
                uID = cmbGroup.SelectedValue
            End If
            If lblID.Text = "" Then
                If cnn.CheckDuplicate("Select ID from ProductGroup Where ProductGroup=" & cnn.SQLquote(UCase(txtGroupName.Text))) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtGroupName.Focus()
                    Exit Sub
                End If
            End If
            'cnn.OpenConn()
            'cnn.Tran = cnn.cnn.BeginTransaction
            If lblID.Text <> "" Then
                strSQL = "UPDATE ProductGroup Set Under=" & uID & ", ProductGroup=" & cnn.SQLquote(UCase(txtGroupName.Text)) & ", Alias=" & cnn.SQLquote(UCase(txtAlias.Text))
                strSQL = strSQL & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' Where ID=" & lblID.Text
                'cnn.executeSQL(strSQL)
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Product Group", "Edit", lblID.Text, UCase(txtGroupName.Text))
            Else
                strSQL = "INSERT INTO ProductGroup (Under, ProductGroup, Alias, CreatedBy, CreatedDate) VALUES (" & uID & "," & cnn.SQLquote(UCase(txtGroupName.Text)) & "," & cnn.SQLquote(UCase(txtAlias.Text)) _
                & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Product Group", "Add", 0, UCase(txtGroupName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = ""
                txtGroupName.Text = ""
                txtAlias.Text = ""
                chkPrimary.Checked = False
                cmbGroup.Enabled = True
                cmbGroup.Text = ""
                txtGroupName.Focus()
            End If
            GetParentGroup()
            treeView.Nodes.Clear()
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub

    Private Sub cmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGroup.SelectedIndexChanged
        GetParentName(cmbGroup.SelectedValue)
    End Sub
    Private Sub GetParentName(ByVal sVal As Integer)
        Dim sSQL As String
        sSQL = "Select ProductGroup from ProductGroup Where ID IN (Select Under from ProductGroup Where ID=" & Val(sVal) & ")"
        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(sSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If dr.Read Then
            lblText.Visible = True
            lblParent.Visible = True
            lblParent.Text = dr.Item("ProductGroup")
        Else
            lblText.Visible = False
            lblParent.Visible = False
        End If
        dr.Close()
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
    End Sub

    Private Sub txtGroupName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGroupName.Leave
        txtGroupName.Text = UCase(txtGroupName.Text)
        txtAlias.Text = txtGroupName.Text
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub chkPrimary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPrimary.CheckedChanged
        If chkPrimary.Checked = True Then
            cmbGroup.Text = ""
            cmbGroup.Enabled = False
        Else
            cmbGroup.Enabled = True
        End If

    End Sub
End Class