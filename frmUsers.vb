Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmUsers
    Dim cnn As New DataConn.Conn

    Private Sub frmUsers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmUsers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Person Name...!")
            txtName.Focus()
            Exit Sub
        End If
        If Trim(txtUserName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter User Name...!")
            txtUserName.Focus()
            Exit Sub
        End If
        If Trim(txtPassword.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Password...!")
            txtPassword.Focus()
            Exit Sub
        End If
        If Trim(cmbAccess.Text) = "" Then
            cnn.ErrMsgBox("Please Select Access Type...!")
            cmbAccess.Focus()
            Exit Sub
        End If
        If cmbAccess.Text = "ADMIN" Or cmbAccess.Text = "USER" Then
        Else
            cnn.ErrMsgBox("Please Select Access Type from List...!")
            cmbAccess.Focus()
            Exit Sub
        End If
        '---------------
        Try

            Dim strSQL As String
            If lblID.Text = "" Then
                strSQL = "Select ID from Users Where fldUserName=" & cnn.SQLquote(txtName.Text)
                If cnn.CheckDuplicate(strSQL) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            Dim iAct, iAccess As String
            If cmbAccess.Text = "ADMIN" Then
                iAccess = "A"
            Else
                iAccess = "U"
            End If
            If chkActive.Checked = True Then
                iAct = "Y"
            Else
                iAct = "N"
            End If
            If lblID.Text <> "" Then
                strSQL = "UPDATE Users Set fldName=" & cnn.SQLquote(UCase(txtName.Text)) & ", fldUserName=" & cnn.SQLquote(txtUserName.Text)
                strSQL = strSQL & ", fldPassword=" & cnn.SQLquote(txtPassword.Text) & ", AccessType='" & iAccess & "', Active='" & iAct & "'" _
                & " Where ID=" & lblID.Text
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Users", "Edit", lblID.Text, UCase(txtName.Text))
            Else
                strSQL = "INSERT INTO Users (fldName, fldUserName, fldPassword, AccessType, Active) OutPut Inserted.ID as HeaderID VALUES ("
                strSQL = strSQL & cnn.SQLquote(UCase(txtName.Text)) & "," & cnn.SQLquote(txtUserName.Text) & "," & cnn.SQLquote(txtPassword.Text) & ",'" & iAccess & "','" & iAct & "')"

                Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                If cmd.Connection.State = 1 Then cmd.Connection.Close()
                cmd.Connection.Open()
                Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
                If dr.Read Then
                    lblHeaderID.Text = dr.Item("HeaderID")
                End If
                dr.Close()
                cmd.Connection.Close()

                If iAccess <> "A" Then
                    strSQL = "INSERT INTO UserRight (ModuleID, UserID, fldAdd, fldEdit, fldDelete, fldView)" _
                    & " SELECT ID , " & Val(lblHeaderID.Text) & ",0,0,0,0 from ModuleName"
                    cnn.executeSQL(strSQL)
                End If

                cnn.GenerateLog("Users", "Add", 0, UCase(txtName.Text))
            End If
            'cnn.Tran.Commit()
            cnn.CloseConn()
            If lblID.Text <> "" Then
                cnn.InfoMsgBox("Record has been Updated Successfully  !")
                Me.Close()
            Else
                cnn.InfoMsgBox("Record has been Added Successfully  !")
                lblID.Text = ""
                txtName.Text = ""
                txtUserName.Text = ""
                txtPassword.Text = ""
                cmbAccess.Text = ""
                chkActive.Checked = False
                txtName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub
End Class