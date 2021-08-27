Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmEmpSalary
    Dim cnn As New DataConn.Conn
    Dim strSQL As String
    Private Sub frmEmpSalary_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmEmpSalary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width
        '=============== MONTH ================
        cmbMonth.DataSource = Nothing
        cmbMonth.Items.Clear()
        cmbMonth.DataSource = cnn.BindMonth
        cmbMonth.DisplayMember = "fldMonth"
        cmbMonth.ValueMember = "MonthID"
        cmbMonth.SelectedIndex = -1
        '--------------------------- EMPLOYEE
        cmbEmployee.DisplayMember = "EmpName"
        cmbEmployee.ValueMember = "ID"
        cmbEmployee.DataSource = cnn.BindEmployee()
        cmbEmployee.SelectedIndex = -1
    End Sub
    Private Sub GetDetails()
        txtPrevPaid.Text = "" : txtAdvPay.Text = "" : txtTotalPaid.Text = "" : txtSalary.Text = "" : txtSalPayment.Text = "" : txtAdvAmount.Text = ""
        If cmbMonth.Text <> "" And cmbEmployee.Text <> "" Then
            strSQL = "SELECT dbo.tblEmployee.ID, dbo.tblEmployee.FatherName FROM dbo.tblEmployee WHERE (dbo.tblEmployee.ID = " & Val(cmbEmployee.SelectedValue) & ")"
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("FatherName")) Then
                    txtFatherName.Text = dr.Item("FatherName")
                End If
            End If
            dr.Close() : cmd.Connection.Close()
            '===================================
            txtMonthDays.Text = cnn.GetMonthDays(Val(cmbMonth.SelectedValue))
            txtWorkingDays.Text = Val(txtMonthDays.Text) - Val(cnn.GetMonthHoliday(Val(cmbMonth.SelectedValue)))
            txtPresentDays.Text = cnn.GetPresentDays(Val(cmbMonth.SelectedValue), Val(cmbEmployee.SelectedValue))
            txtAbsentDays.Text = Val(txtWorkingDays.Text) - Val(txtPresentDays.Text)
            '===================================
            Dim iPresentDays As Double
            Dim iPerDay As Double
            strSQL = "IF EXISTS (SELECT ID from tblEmpSalary Where SalMonth=" & Val(cmbMonth.SelectedValue) _
            & " AND BranchID=" & Val(MDI.lblMasterBranchID.Text) & " AND EmpID=" & Val(cmbEmployee.SelectedValue) & ")" _
            & " SELECT 1 as iPaid, dbo.tblEmpSalary.SalMonth, dbo.tblMonths.fldMonth, dbo.tblEmpSalary.EmpBasicSalary, dbo.tblEmpSalary.PaidSalary, dbo.tblEmpSalary.EmpGrossSalary - dbo.tblEmpSalary.PaidSalary AS PaySalary, dbo.tblEmpSalary.EmpGrossSalary - dbo.tblEmpSalary.PaidSalary AS TotalPaid" _
            & " FROM dbo.tblEmpSalary INNER JOIN dbo.tblMonths ON dbo.tblEmpSalary.SalMonth = dbo.tblMonths.MonthID INNER JOIN dbo.tblEmployee ON dbo.tblEmpSalary.EmpID = dbo.tblEmployee.ID" _
            & " WHERE (dbo.tblEmpSalary.EmpID = " & Val(cmbEmployee.SelectedValue) & ") AND (dbo.tblEmpSalary.SalMonth = " & Val(cmbMonth.SelectedValue) & ")" _
            & " AND (dbo.tblEmpSalary.EmpBasicSalary - dbo.tblEmpSalary.PaidSalary > 0)" _
            & " ELSE " _
            & " SELECT 0 as iPaid, " & Val(cmbMonth.SelectedValue) & " AS SalMonth, (SELECT fldMonth FROM dbo.tblMonths WHERE (MonthID = " & Val(cmbMonth.SelectedValue) & ")) AS fldMonth, Salary AS EmpBasicSalary, 0 AS PaidSalary, Salary AS PaySalary, Salary AS TotalPaid" _
            & " FROM dbo.tblEmployee WHERE (ID = " & Val(cmbEmployee.SelectedValue) & ")"

            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr.Item("iPaid") = 1 Then
                    txtSalary.Text = cnn.Num2(dr.Item("EmpBasicSalary"))
                    'txtGrossSalary.Text = cnn.Num2(dr.Item("EmpSal"))
                    If Val(txtPresentDays.Text) > 0 Then
                        iPresentDays = Val(txtMonthDays.Text) - Val(txtAbsentDays.Text)
                        iPerDay = Val(txtSalary.Text) / Val(txtMonthDays.Text)
                    Else
                        iPresentDays = 0
                        iPerDay = Val(txtSalary.Text) / Val(txtMonthDays.Text)
                    End If

                    txtGrossSalary.Text = cnn.Num2(Val(iPresentDays) * Val(iPerDay))
                    txtPrevPaid.Text = cnn.Num2(dr.Item("PaidSalary"))
                    txtSalPayment.Text = cnn.Num2(dr.Item("PaySalary"))
                    'txtAdvAmount.Text = cnn.Num2(dr.Item("TotAdvance"))
                    txtTotalPaid.Text = cnn.Num2(dr.Item("TotalPaid"))
                    If Val(txtAdvAmount.Text) = 0 Then
                        txtAdvPay.ReadOnly = True
                    Else
                        txtAdvPay.ReadOnly = False
                    End If
                Else
                    txtSalary.Text = cnn.Num2(dr.Item("EmpBasicSalary"))
                    If Val(txtPresentDays.Text) > 0 Then
                        iPresentDays = Val(txtMonthDays.Text) - Val(txtAbsentDays.Text)
                        iPerDay = Val(txtSalary.Text) / Val(txtMonthDays.Text)
                    Else
                        iPresentDays = 0
                        iPerDay = Val(txtSalary.Text) / Val(txtMonthDays.Text)
                    End If

                    txtGrossSalary.Text = cnn.Num2(Val(iPresentDays) * Val(iPerDay))
                    txtPrevPaid.Text = cnn.Num2(dr.Item("PaidSalary"))
                    txtSalPayment.Text = cnn.Num2(Val(iPresentDays) * Val(iPerDay)) ' cnn.Num2(dr.Item("PaySalary"))
                    'txtAdvAmount.Text = cnn.Num2(dr.Item("TotAdvance"))
                    txtTotalPaid.Text = cnn.Num2(Val(iPresentDays) * Val(iPerDay)) ' cnn.Num2(dr.Item("TotalPaid"))
                    If Val(txtAdvAmount.Text) = 0 Then
                        txtAdvPay.ReadOnly = True
                    Else
                        txtAdvPay.ReadOnly = False
                    End If
                End If
            Else
                cnn.ErrMsgBox("This Month Salary is already Paid  !")
            End If
            dr.Close() : cmd.Connection.Close()
        End If
    End Sub
    Private Sub CalcSalary()
        'If Val(txtAdvPay.Text) > Val(txtAdvAmount.Text) Then
        '    cnn.ErrMsgBox("Invalid Input  !")
        '    txtAdvPay.Text = 0
        'End If
        'If Val(txtSalPayment.Text) > (Val(txtGrossSalary.Text) - Val(txtPrevPaid.Text)) Then
        '    cnn.ErrMsgBox("Invalid Input  !")
        '    txtAdvPay.Text = 0
        '    txtSalPayment.Text = (Val(txtGrossSalary.Text) - Val(txtPrevPaid.Text))
        'End If
        txtTotalPaid.Text = Val(txtSalPayment.Text) - Val(txtAdvPay.Text)
    End Sub


    Private Sub frmEmpSalary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        FormResize()
    End Sub
    Private Sub FormResize()
        StripPanel.Width = MDI.ClientSize.Width
    End Sub
    Private Sub cmbEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmployee.SelectedIndexChanged
        If cmbEmployee.Text <> "" Then
            GetDetails()
        End If
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonth.SelectedIndexChanged
        GetDetails()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Val(cmbMonth.SelectedValue) <= 0 Then
            cnn.ErrMsgBox("Please Select Month  !")
            cmbMonth.Focus()
            Exit Sub
        End If

        If Val(cmbEmployee.SelectedValue) <= 0 Then
            cnn.ErrMsgBox("Please Select Employee  !")
            cmbEmployee.Focus()
            Exit Sub
        End If
        'If Val(txtTotalPaid.Text) <= 0 Then
        '    cnn.ErrMsgBox("Please Enter Valid Amount")
        '    txtTotalPaid.Focus()
        '    Exit Sub
        'End If
        Dim iii As Double = cnn.Num2(Val(txtGrossSalary.Text) - Val(txtPrevPaid.Text))
        If Val(txtSalPayment.Text) > (iii) Then
            If MessageBox.Show("Do you want to Continue?", "Continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            'cnn.ErrMsgBox("Please Enter Valid Amount")
            'txtSalPayment.Focus()
            'Exit Sub
        End If
        'Dim iPrevAdvance, iPrevDue, iDueMonth
        ''If chkAdvance.Checked = True Then
        ''    iPrevAdvance = txtPrevAdvance.Text : iDueMonth = ""
        ''Else
        ''    iPrevAdvance = 0 : iDueMonth = ""
        ''End If
        'If chkDue.Checked = True Then
        '    iPrevDue = txtDue.Text
        '    If rbCurrent.Checked = True Then
        '        iDueMonth = "C"
        '    Else
        '        iDueMonth = "P"
        '    End If
        'Else
        '    iPrevDue = 0 : iDueMonth = ""
        'End If
        ''---------------------
        strSQL = "IF EXISTS (Select ID from tblEmpSalary WHere EmpID=" & Val(cmbEmployee.SelectedValue) _
        & " AND SalMonth=" & Val(cmbMonth.SelectedValue) & " AND BranchID=" & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UPDATE tblEmpSalary SET PaidSalary = (PaidSalary + " & Val(txtSalPayment.Text) & ") , EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "'" _
        & " WHERE SalMonth=" & Val(cmbMonth.SelectedValue) & " AND EmpID=" & Val(cmbEmployee.SelectedValue) & " AND BranchID=" & Val(MDI.lblMasterBranchID.Text) _
        & " ELSE" _
        & " INSERT INTO tblEmpSalary (PaymentDate, EmpID, EmpBasicSalary, EmpGrossSalary, SalMonth, PaidSalary, BranchID, CreatedBy, CreatedDate) VALUES ('" & cnn.GetDate(txtDate.Text) & "'" _
        & "," & Val(cmbEmployee.SelectedValue) & "," & Val(txtSalary.Text) & "," & Val(txtGrossSalary.Text) & "," & Val(cmbMonth.SelectedValue) & "," & Val(txtSalPayment.Text) _
        & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
        cnn.executeSQL(strSQL)

        strSQL = "INSERT INTO tblEmpSalaryPayment (EmpID, PaymentDate, SalMonth, Salary, SalaryPayment, AdvancePayment, TotalPaid, BranchID, CreatedBy, CreatedDate) VALUES (" _
        & Val(cmbEmployee.SelectedValue) & ",'" & cnn.GetDate(txtDate.Text) & "'," & Val(cmbMonth.SelectedValue) _
        & "," & Val(txtGrossSalary.Text) & "," & Val(txtSalPayment.Text) & "," & Val(txtAdvPay.Text) & "," & Val(txtTotalPaid.Text) _
        & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
        cnn.executeSQL(strSQL)
        cnn.InfoMsgBox("Record has been saved  !")

        cmbEmployee.Text = "" : cmbMonth.SelectedIndex = -1 : txtFatherName.Text = ""
        txtSalary.Text = "" : txtSalPayment.Text = "" : txtAdvAmount.Text = "" : txtAdvPay.Text = "" : txtPrevPaid.Text = "" : txtTotalPaid.Text = ""
        txtGrossSalary.Text = "" : txtMonthDays.Text = "" : txtPresentDays.Text = "" : txtAbsentDays.Text = "" : txtWorkingDays.Text = ""

    End Sub

    Private Sub txtSalPayment_TextChanged(sender As Object, e As EventArgs) Handles txtSalPayment.TextChanged
        CalcSalary()
    End Sub
End Class