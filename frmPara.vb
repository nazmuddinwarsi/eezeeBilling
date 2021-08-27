Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc
Public Class frmPara
    Dim cnn As New DataConn.Conn

    Private Sub frmPara_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmPara_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmPara_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strSQL As String
        strSQL = "SELECT SaleInvoiceNo, PurchaseReturnNo, SaleReturnNo from Para" _
        & " Where MasterCompanyID=" & Val(MDI.lblMasterCompanyID.Text) & " AND MasterBranchID=" & Val(MDI.lblMasterBranchID.Text)
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            If Not IsDBNull(dr.Item("SaleInvoiceNo")) Then
                txtSale.Text = dr.Item("SaleInvoiceNo")
            End If
            If Not IsDBNull(dr.Item("PurchaseReturnNo")) Then
                txtPurReturn.Text = dr.Item("PurchaseReturnNo")
            End If
            If Not IsDBNull(dr.Item("SaleReturnNo")) Then
                txtSaleReturn.Text = dr.Item("SaleReturnNo")
            End If
        End If
        dr.Close() : cmd.Connection.Close()
        '-------------------------------
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        strSQL = "Select MonthID, fldMonth, 0 from tblMonths ORDER BY SNo"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        Grid.DataSource = ds.Tables(0)
        Grid.Columns(0).HeaderText = "ID"
        Grid.Columns(0).Visible = False
        Grid.Columns(1).HeaderText = "Month"
        Grid.Columns(2).HeaderText = "No Of Holiday"

        '---------------------
        For i = 0 To Grid.Rows.Count - 1
            Grid.Rows(i).Cells(2).Value = cnn.GetMonthHoliday(Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value))
        Next
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub FormRes()
        StripPanel.Width = Me.Width
    End Sub
    Private Sub frmPara_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        FormRes()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtSale.Text = "" Then
            cnn.ErrMsgBox("Please Enter Sale Invoice No...!")
            txtSale.Focus()
            Exit Sub
        End If
        If txtPurReturn.Text = "" Then
            cnn.ErrMsgBox("Please Enter Purchase Return No...!")
            txtPurReturn.Focus()
            Exit Sub
        End If
        If txtSaleReturn.Text = "" Then
            cnn.ErrMsgBox("Please Enter Sale Return No...!")
            txtSaleReturn.Focus()
            Exit Sub
        End If
        '-----------------------
        Dim strSQL As String
        strSQL = "UPDATE Para Set SaleInvoiceNo=" & Val(txtSale.Text) & ", PurchaseReturnNo=" & Val(txtPurReturn.Text)
        strSQL = strSQL & ", SaleReturnNo=" & Val(txtSaleReturn.Text)
        strSQL = strSQL & " Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
        cnn.executeSQL(strSQL)
        cnn.GenerateLog("Para", "Edit", 0, "Branch : " & MDI.lblMasterBranchName.Text)

        '-----------------------------
        For i = 0 To Grid.Rows.Count - 1
            If cnn.CheckInEmpSal(Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value)) = False Then
                cnn.UpdateMonthHoliday(Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value), Val(Grid.Rows(i).Cells(2).Value))
            End If
        Next

        cnn.InfoMsgBox("Record has been saved  !")
        Me.Close()

    End Sub
End Class