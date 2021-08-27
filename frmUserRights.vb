Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmUserRights
    Dim cnn As New DataConn.Conn

    Private Sub frmUserRights_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmUserRights_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        Grid.Width = MDI.ClientSize.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = MDI.Width

        '===========================================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, fldName from Users Where Active='Y' AND AccessType <> 'A' Order BY fldName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbUser.DisplayMember = "fldName"
        cmbUser.ValueMember = "ID"
        cmbUser.DataSource = ds.Tables(0)
        cmbUser.SelectedIndex = -1
        '======================================
        strSQL = "Select ID, ModuleName from ModuleName ORDER By ModuleName ASC"

        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        Grid.Rows.Clear()
        Do While dr.Read
            Grid.Rows.Add()
            Grid.Rows(Grid.Rows.Count - 1).Cells(0).Value = dr.Item("ID")
            Grid.Rows(Grid.Rows.Count - 1).Cells(1).Value = dr.Item("ModuleName")
            Grid.Rows(Grid.Rows.Count - 1).Cells(2).Value = 0
            Grid.Rows(Grid.Rows.Count - 1).Cells(3).Value = 0
            Grid.Rows(Grid.Rows.Count - 1).Cells(4).Value = 0
            Grid.Rows(Grid.Rows.Count - 1).Cells(5).Value = 0
        Loop
        Grid.Columns(0).Visible = False
        'da = New OdbcDataAdapter(strSQL, cnn.cnn)
        'ds = New DataSet()
        'da.Fill(ds)
        'Grid.DataSource = ds.Tables(0)
        'Grid.Columns(0).HeaderText = "ID"
        'Grid.Columns(0).Visible = False
        'Grid.Columns(1).HeaderText = "Module Name"
        'Grid.Columns(2).HeaderText = "Add"
        'Grid.Columns(3).HeaderText = "Edit"
        'Grid.Columns(4).HeaderText = "Delete"
        'Grid.Columns(5).HeaderText = "View"

    End Sub

    Private Sub frmUserRights_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub
    Private Sub FormRes()
        Grid.Width = Me.Width
        Grid.Height = Me.Height - 157
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cmbUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUser.SelectedIndexChanged
        If cmbUser.Text <> "" Then
            FillGrid()
        End If
    End Sub
    Private Sub FillGrid()
        Dim strSQL As String
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        For i = 0 To Grid.Rows.Count - 1
            strSQL = "Select * from UserRight Where UserID=" & Val(cmbUser.SelectedValue) _
            & " AND ModuleID = " & Grid.Rows(i).Cells(0).Value.ToString
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("fldAdd")) Then
                    If dr.Item("fldAdd") = "True" Then
                        Grid.Rows(i).Cells(2).Value = -1
                    Else
                        Grid.Rows(i).Cells(2).Value = 0
                    End If
                End If

                If Not IsDBNull(dr.Item("fldEdit")) Then
                    If dr.Item("fldEdit") = "True" Then
                        Grid.Rows(i).Cells(3).Value = -1
                    Else
                        Grid.Rows(i).Cells(3).Value = 0
                    End If
                End If

                If Not IsDBNull(dr.Item("fldDelete")) Then
                    If dr.Item("fldDelete") = "True" Then
                        Grid.Rows(i).Cells(4).Value = -1
                    Else
                        Grid.Rows(i).Cells(4).Value = 0
                    End If
                End If

                If Not IsDBNull(dr.Item("fldView")) Then
                    If dr.Item("fldView") = "True" Then
                        Grid.Rows(i).Cells(5).Value = -1
                    Else
                        Grid.Rows(i).Cells(5).Value = 0
                    End If
                End If
            Else
                Grid.Rows(i).Cells(2).Value = 0
                Grid.Rows(i).Cells(3).Value = 0
                Grid.Rows(i).Cells(4).Value = 0
                Grid.Rows(i).Cells(5).Value = 0
            End If
            dr.Close()
            cmd.Connection.Close()
        Next
    End Sub

    Private Sub cmbUser_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUser.TextChanged
        FillGrid()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cmbUser.Text = "" Then
            cnn.ErrMsgBox("Please Select User Name  !")
            cmbUser.Focus()
            Exit Sub
        End If
        If cmbUser.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select User Name from List  !")
            cmbUser.Focus()
            Exit Sub
        End If

        Dim strSQL As String
        Dim iAdd, iEdit, iDel, iView As String
        strSQL = "Delete from UserRight Where UserID = " & Val(cmbUser.SelectedValue)
        cnn.executeSQL(strSQL)
        For i = 0 To Grid.Rows.Count - 1
            If Grid.Rows(i).Cells(2).Value = -1 Then
                iAdd = -1
            Else
                iAdd = 0
            End If
            If Grid.Rows(i).Cells(3).Value = -1 Then
                iEdit = -1
            Else
                iEdit = 0
            End If
            If Grid.Rows(i).Cells(4).Value = -1 Then
                iDel = -1
            Else
                iDel = 0
            End If
            If Grid.Rows(i).Cells(5).Value = -1 Then
                iView = -1
            Else
                iView = 0
            End If
            '====================
            strSQL = "INSERT INTO UserRight (ModuleID, UserID, fldAdd, fldEdit, fldDelete, fldView) VALUES (" _
            & Val(Grid.Rows(i).Cells(0).Value) & "," & Val(cmbUser.SelectedValue) & "," & Val(iAdd) & "," & Val(iEdit) & "," & Val(iDel) & "," & Val(iView) & ")"
            cnn.executeSQL(strSQL)
        Next
        cnn.InfoMsgBox("Record has been Saved  !")
        cmbUser.Text = ""
        For i = 0 To Grid.Rows.Count - 1
            Grid.Rows(i).Cells(2).Value = 0
            Grid.Rows(i).Cells(3).Value = 0
            Grid.Rows(i).Cells(4).Value = 0
            Grid.Rows(i).Cells(5).Value = 0
        Next

    End Sub
End Class