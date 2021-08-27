Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmUnit
    Dim cnn As New DataConn.Conn

    Private Sub frmUnit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmUnit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtGroupName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Unit Name...!")
            txtGroupName.Focus()
            Exit Sub
        End If
        '---------------
        Try

            Dim strSQL As String
            If lblID.Text = "" Then
                If cnn.CheckDuplicate("Select ID from UnitMaster Where UnitName=" & cnn.SQLquote(UCase(txtGroupName.Text))) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtGroupName.Focus()
                    Exit Sub
                End If
            End If
            'cnn.OpenConn()
            'cnn.Tran = cnn.cnn.BeginTransaction
            If lblID.Text <> "" Then
                strSQL = "UPDATE UnitMaster Set UnitName=" & cnn.SQLquote(UCase(txtGroupName.Text)) & ", Descr=" & cnn.SQLquote(UCase(txtAlias.Text))
                strSQL = strSQL & " Where ID=" & lblID.Text
                cnn.executeTranSQL(strSQL)
                cnn.GenerateLog("Unit", "Edit", lblID.Text, UCase(txtGroupName.Text))
            Else
                strSQL = "INSERT INTO UnitMaster(UnitName, Descr) VALUES (" & cnn.SQLquote(UCase(txtGroupName.Text)) & "," & cnn.SQLquote(UCase(txtAlias.Text)) & ")"
                cnn.executeTranSQL(strSQL)
                cnn.GenerateLog("Unit", "Add", 0, UCase(txtGroupName.Text))
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
                txtGroupName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class