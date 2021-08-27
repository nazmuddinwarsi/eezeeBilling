Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmEmpAttendance
    Dim cnn As New DataConn.Conn

    Private Sub frmEmpAttendance_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmEmpAttendance_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmEmpAttendance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        StripPanel.Width = Me.Width
        txtDate.Text = Date.Today()

    End Sub
    Private Sub BindGrid()
        'Grid.Rows.Clear()
            Dim strSQL As String
            Dim da As New OdbcDataAdapter
            Dim ds As New DataSet
            '---------------- CHECK
            strSQL = "SELECT fldDate From tblEmpAttendance Where CompanyID=" & Val(MDI.lblMasterCompanyID.Text) & " AND BranchID=" & Val(MDI.lblMasterBranchID.Text) _
            & " AND fldDate='" & cnn.GetDate(txtDate.Text) & "'"
            If cnn.CheckDuplicate(strSQL) = True Then
                strSQL = "SELECT dbo.tblEmployee.ID, dbo.tblEmployee.EmpName, dbo.tblEmployee.FatherName, dbo.tblEmployee.fldAddress, dbo.tblEmployee.fldCity, dbo.tblEmployee.MobileNo, dbo.tblEmpAttendance.Status" _
                & " FROM dbo.tblEmployee INNER JOIN dbo.tblEmpAttendance ON dbo.tblEmployee.ID = dbo.tblEmpAttendance.EmpID" _
                & " WHERE (dbo.tblEmployee.CompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.tblEmployee.BranchID = " & Val(MDI.lblMasterBranchID.Text) _
                & ") AND (dbo.tblEmpAttendance.fldDate = '" & cnn.GetDate(txtDate.Text) & "') AND (dbo.tblEmployee.Active = 1) "
            Else
                strSQL = "SELECT dbo.tblEmployee.ID, dbo.tblEmployee.EmpName, dbo.tblEmployee.FatherName, dbo.tblEmployee.fldAddress, dbo.tblEmployee.fldCity, dbo.tblEmployee.MobileNo, 'FULL DAY' AS Status" _
                & " FROM dbo.tblEmployee WHERE (dbo.tblEmployee.CompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.tblEmployee.BranchID =" & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.tblEmployee.Active = 1)"
            End If
            Grid.Rows.Clear()
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            Do While dr.Read
                Grid.Rows.Add(1)
                Grid.Rows(Grid.Rows.Count - 1).Cells(0).Value = dr.Item("ID")
                Grid.Rows(Grid.Rows.Count - 1).Cells(1).Value = dr.Item("EmpName")
                Grid.Rows(Grid.Rows.Count - 1).Cells(2).Value = dr.Item("FatherName")
                Grid.Rows(Grid.Rows.Count - 1).Cells(3).Value = dr.Item("fldAddress")
                Grid.Rows(Grid.Rows.Count - 1).Cells(4).Value = dr.Item("fldCity")
                Grid.Rows(Grid.Rows.Count - 1).Cells(5).Value = dr.Item("MobileNo")
                Grid.Rows(Grid.Rows.Count - 1).Cells(6).Value = dr.Item("Status")
            Loop
            dr.Close()
            cmd.Connection.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        '------------------------------
        Dim strSQL As String = ""
        strSQL = "DELETE From tblEmpAttendance Where CompanyID=" & Val(MDI.lblMasterCompanyID.Text) & " AND BranchID=" & Val(MDI.lblMasterBranchID.Text) _
        & " AND fldDate='" & cnn.GetDate(txtDate.Text) & "'"
        cnn.executeSQL(strSQL)

        For i = 0 To Grid.Rows.Count - 1
            strSQL = "INSERT INTO tblEmpAttendance (fldDate, EmpID, Status, CompanyID, BranchID, CreatedBy, CreatedDate) VALUES (" _
            & "'" & cnn.GetDate(txtDate.Text) & "'," & Val(Grid.Rows(i).Cells(0).Value) & "," _
            & cnn.SQLquote(Grid.Rows(i).Cells(6).Value) & "," & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) _
            & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"
            cnn.executeSQL(strSQL)
        Next
        cnn.InfoMsgBox("Record has been Saved  !")
        Grid.Rows.Clear() : txtDate.Focus() ': txtDate.Text = Date.Today()
    End Sub

    Private Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
        BindGrid()
    End Sub
End Class