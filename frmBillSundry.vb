Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmBillSundry
    Dim cnn As New DataConn.Conn

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If Trim(txtName.Text) = "" Then
            cnn.ErrMsgBox("Please Enter Sundry Name...!")
            txtName.Focus()
            Exit Sub
        End If
        'If Trim(cmbNature.Text) = "" Then
        '    cnn.ErrMsgBox("Please Select Sundry Nature...!")
        '    cmbNature.Focus()
        '    Exit Sub
        'End If
        'If Val(cmbNature.SelectedValue) = 0 Then
        '    cnn.ErrMsgBox("Please Select Sundry Nature from List ...!")
        '    cmbNature.Focus()
        '    Exit Sub
        'End If
        '---------------
        Try
            Dim strSQL, sType, sRoundOff, sRoundType, sAmtAs, sPercOf As String
            If optAdd.Checked = True Then
                sType = "ADD"
            Else
                sType = "SUB"
            End If
            If chkRoundOff.Checked = 0 Then
                sRoundOff = "N"
                sRoundType = ""
            Else
                sRoundOff = "Y"
                If optAuto.Checked = True Then
                    sRoundType = "AUTO"
                ElseIf optUp.Checked = True Then
                    sRoundType = "UP"
                ElseIf optLow.Checked = True Then
                    sRoundType = "LOW"
                End If
            End If
            If optAmt.Checked = True Then
                sAmtAs = "AMT" : sPercOf = ""
            Else
                sAmtAs = "PER"
                If optBasic.Checked = True Then
                    sPercOf = "BASIC"
                ElseIf optTaxable.Checked = True Then
                    sPercOf = "TAX"
                ElseIf optBill.Checked = True Then
                    sPercOf = "BILL"
                ElseIf optPrevious.Checked = True Then
                    sPercOf = "PREV"
                End If
            End If

            If lblID.Text = "" Then
                If cnn.CheckDuplicate("Select ID from BillSundryMaster Where SundryName=" & cnn.SQLquote(UCase(txtName.Text))) = True Then
                    cnn.ErrMsgBox("Duplicate Entry Please check...!")
                    txtName.Focus()
                    Exit Sub
                End If
            End If
            'cnn.OpenConn()
            'cnn.Tran = cnn.cnn.BeginTransaction
            If lblID.Text <> "" Then
                strSQL = "UPDATE  BillSundryMaster SET SundryName=" & cnn.SQLquote(UCase(txtName.Text)) & ", DefaultValue=" & Val(txtValue.Text) & ", SundryType='" & sType & "'" _
                & ", NatureID=" & Val(cmbNature.SelectedValue) & ", RoundOff='" & sRoundOff & "'" _
                & ", RoundOffType='" & sRoundType & "', AmountAs='" & sAmtAs & "', PercentOf='" & sPercOf & "'" _
                & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' WHERE ID=" & Val(lblID.Text)
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Bill Sundry", "Edit", lblID.Text, cnn.SQLquote(UCase(txtName.Text)))
            Else
                strSQL = "INSERT INTO BillSundryMaster (SundryName, DefaultValue, SundryType, NatureID, RoundOff, RoundOffType, AmountAs, PercentOf, CreatedBy, CreatedDate) VALUES ("
                strSQL = strSQL & cnn.SQLquote(UCase(txtName.Text)) & "," & Val(txtValue.Text) & ",'" & sType & "'," & Val(cmbNature.SelectedValue) _
                & ",'" & sRoundOff & "','" & sRoundType & "','" & sAmtAs & "','" & sPercOf & "'," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
                cnn.executeSQL(strSQL)
                cnn.GenerateLog("Bill Sundry", "Add", 0, cnn.SQLquote(UCase(txtName.Text)))
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
                txtValue.Text = ""
                cmbNature.Text = ""
                chkRoundOff.Checked = False
                optAuto.Checked = True
                optAdd.Checked = True
                optAmt.Checked = True
                optBasic.Checked = True
                txtName.Focus()
            End If
        Catch ex As Exception
            'cnn.Tran.Rollback()
            cnn.ErrMsgBox("There is a Problem due to " & ex.Message)
        End Try
    End Sub

    Private Sub chkRoundOff_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRoundOff.CheckedChanged
        If chkRoundOff.Checked = True Then
            frameRoundOff.Enabled = True
        Else
            frameRoundOff.Enabled = False
        End If
    End Sub

    Private Sub frmBillSundry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmBillSundry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        ''==============================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, SundryNature from BillSundryNature ORDER BY SundryNature ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbNature.DisplayMember = "SundryNature"
        cmbNature.ValueMember = "ID"
        cmbNature.DataSource = ds.Tables(0)
        cmbNature.SelectedIndex = -1
    End Sub

    Private Sub optAmt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAmt.CheckedChanged
        frameOf.Enabled = False
    End Sub

    Private Sub optPer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPer.CheckedChanged
        frameOf.Enabled = True
    End Sub
End Class