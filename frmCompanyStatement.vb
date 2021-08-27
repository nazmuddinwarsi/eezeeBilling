Imports System
Imports System.Data
Imports System.Data.Odbc
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing

Public Class frmCompanyStatement
    Dim cnn As New DataConn.Conn
    Private pimage As Image
    Private Sub frmCompanyStatement_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub
    Private Sub frmCompanyStatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI

        txtFromDate.Text = Date.Today
        txtToDate.Text = Date.Today
        StripPanel.Width = MDI.Width
        '--------------------

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        GetData()
    End Sub
    Private Sub GetData()
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        '========================= SALE GRID


        strSQL = "SELECT dbo.ProductGroup.ProductGroup, ISNULL(SUM(dbo.SaleDetails.Qty),0) AS Qty, ISNULL(SUM(dbo.SaleDetails.Nos),0) AS Nos, ISNULL(SUM(dbo.SaleDetails.Amount),0) AS Amount" _
        & " FROM dbo.SaleDetails INNER JOIN dbo.Products ON dbo.SaleDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.SaleHeader ON dbo.SaleDetails.HeaderID = dbo.SaleHeader.ID" _
        & " WHERE (dbo.SaleHeader.InvoiceDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "')" _
        & " GROUP BY dbo.ProductGroup.ProductGroup"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        grdSale.DataSource = ds.Tables(0)
        grdSale.Columns(0).HeaderText = "Group"
        grdSale.Columns(1).HeaderText = "Weight"
        grdSale.Columns(2).HeaderText = "Nos"
        grdSale.Columns(3).HeaderText = "Amount"
        '========================= PURCHASE GRID
        strSQL = "SELECT dbo.ProductGroup.ProductGroup, ISNULL(SUM(dbo.PurchaseDetails.Qty),0) AS Qty, ISNULL(SUM(dbo.PurchaseDetails.Nos),0) AS Nos, ISNULL(SUM(dbo.PurchaseDetails.Amount),0) AS Amount" _
        & " FROM dbo.PurchaseDetails INNER JOIN dbo.Products ON dbo.PurchaseDetails.ProductID = dbo.Products.ID INNER JOIN dbo.ProductGroup ON dbo.Products.GroupID = dbo.ProductGroup.ID INNER JOIN dbo.PurchaseHeader ON dbo.PurchaseDetails.HeaderID = dbo.PurchaseHeader.ID" _
        & " WHERE (dbo.PurchaseHeader.InvoiceDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "')" _
        & " GROUP BY dbo.ProductGroup.ProductGroup"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        grdPurchase.DataSource = ds.Tables(0)
        grdPurchase.Columns(0).HeaderText = "Group"
        grdPurchase.Columns(1).HeaderText = "Weight"
        grdPurchase.Columns(2).HeaderText = "Nos"
        grdPurchase.Columns(3).HeaderText = "Amount"



        txtOpDues.Text = cnn.GetBuyerDues(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), cnn.GetDate(txtFromDate.Text))

        txtClosingDue.Text = cnn.GetBuyerDues(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), DateAdd(DateInterval.Day, 1, cnn.GetDate(txtToDate.Text)))


        '========================= RECEIPT, PAYMENT, EXP, SALARY, SALE, PURCHASE
        strSQL = "SELECT  dbo.GET_OP_AMT('" & cnn.GetDate(txtFromDate.Text) & "'," & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & ") AS OpAmt, SUM(RecAmt) AS RecAmt, SUM(PP_PaymentAmt) AS PP_PaymentAmt, SUM(BP_PaymentAmt) AS BP_PaymentAmt, SUM(ExpAmt) AS ExpAmt, SUM(SalaryAmt) AS SalaryAmt, SUM(SaleAmt) AS SaleAmt, SUM(PurchaseAmt) AS PurchaseAmt FROM (" _
        & " SELECT ISNULL(SUM(Amount), 0) AS RecAmt, 0 AS PP_PaymentAmt, 0 AS BP_PaymentAmt, 0 AS ExpAmt, 0 AS SalaryAmt, 0 AS SaleAmt, 0 AS PurchaseAmt FROM dbo.Receipt" _
        & " WHERE (ReceiptDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UNION ALL" _
        & " SELECT 0 AS RecAmt, ISNULL(SUM(Amount), 0) AS PP_PaymentAmt, 0 AS BP_PaymentAmt, 0 AS ExpAmt, 0 AS SalaryAmt, 0 AS SaleAmt, 0 AS PurchaseAmt FROM dbo.Payment" _
        & " WHERE (PayType = 'PP') AND (PaymentDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UNION ALL" _
        & " SELECT 0 AS RecAmt, 0 AS PP_PaymentAmt, ISNULL(SUM(Amount), 0) AS BP_PaymentAmt, 0 AS ExpAmt, 0 AS SalaryAmt, 0 AS SaleAmt, 0 AS PurchaseAmt FROM dbo.Payment" _
        & " WHERE (PayType = 'BP') AND (PaymentDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UNION ALL" _
        & " SELECT 0 AS RecAmt, 0 AS PP_PaymentAmt, 0 AS BP_PaymentAmt, ISNULL(SUM(Amount), 0) AS ExpAmt, 0 AS SalaryAmt, 0 AS SaleAmt, 0 AS PurchaseAmt FROM dbo.tblExpense" _
        & " WHERE (fldDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UNION ALL" _
        & " SELECT 0 AS RecAmt, 0 AS PP_PaymentAmt, 0 AS BP_PaymentAmt, 0 AS ExpAmt, ISNULL(SUM(TotalPaid), 0) AS SalaryAmt, 0 AS SaleAmt, 0 AS PurchaseAmt FROM dbo.tblEmpSalaryPayment" _
        & " WHERE (PaymentDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (BranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UNION ALL" _
        & " SELECT 0 AS RecAmt, 0 AS PP_PaymentAmt, 0 AS BP_PaymentAmt, 0 AS ExpAmt, 0 AS SalaryAmt, ISNULL(SUM(BillAmount), 0) AS SaleAmt, 0 AS PurchaseAmt FROM dbo.SaleHeader" _
        & " WHERE (InvoiceDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
        & " UNION ALL" _
        & " SELECT 0 AS RecAmt, 0 AS PP_PaymentAmt, 0 AS BP_PaymentAmt, 0 AS ExpAmt, 0 AS SalaryAmt, 0 AS SaleAmt, ISNULL(SUM(BillAmount), 0) AS PurchaseAmt FROM dbo.PurchaseHeader" _
        & " WHERE (InvoiceDate BETWEEN '" & cnn.GetDate(txtFromDate.Text) & "' AND '" & cnn.GetDate(txtToDate.Text) & "') AND (MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")) AS t"

        cmd = New OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        dr = cmd.ExecuteReader()
        If dr.Read Then
            txtOpAmt.Text = dr.Item("OpAmt")
            txtRecAmt.Text = dr.Item("RecAmt")
            txtParty.Text = dr.Item("PP_PaymentAmt")
            txtBank.Text = dr.Item("BP_PaymentAmt")
            txtPayAmt.Text = Val(txtBank.Text) + Val(txtParty.Text)
            txtMiscExpAmt.Text = dr.Item("ExpAmt")
            txtSalaryAmt.Text = dr.Item("SalaryAmt")
            txtSaleAmt.Text = dr.Item("SaleAmt")
            txtPurchaseAmt.Text = dr.Item("PurchaseAmt")
        End If
        dr.Close() : cmd.Connection.Close()
        '===================================== CALCULATION
        txtExpAmt.Text = Val(txtMiscExpAmt.Text) + Val(txtSalaryAmt.Text)
        txtTotal1.Text = Val(txtOpAmt.Text) + Val(txtOpDues.Text)
        txtTotal2.Text = Val(txtTotal1.Text) + Val(txtSaleAmt.Text)
        txtTotal3.Text = Val(txtTotal2.Text) - (Val(txtPayAmt.Text) + Val(txtMiscExpAmt.Text) + Val(txtSalaryAmt.Text))
        txtClosingAmt.Text = Val(txtTotal3.Text) - Val(txtClosingDue.Text)

        txtOpAmt.Text = cnn.Num2(txtOpAmt.Text) : txtOpDues.Text = cnn.Num2(txtOpDues.Text) : txtTotal1.Text = cnn.Num2(txtTotal1.Text)
        txtSaleAmt.Text = cnn.Num2(txtSaleAmt.Text) : txtTotal2.Text = cnn.Num2(txtTotal2.Text) : txtPayAmt.Text = cnn.Num2(txtPayAmt.Text)
        txtExpAmt.Text = cnn.Num2(txtExpAmt.Text) : txtTotal3.Text = cnn.Num2(txtTotal3.Text) : txtClosingDue.Text = cnn.Num2(txtClosingDue.Text)
        txtClosingAmt.Text = cnn.Num2(txtClosingAmt.Text) : txtRecAmt.Text = cnn.Num2(txtRecAmt.Text)
        'txtExpAmt.Text = Val(txtMiscExpAmt.Text) + Val(txtSalaryAmt.Text) txtClosingAmt
        'txtClosingAmt.Text = (Val(txtOpAmt.Text) + Val(txtRecAmt.Text)) - (Val(txtPayAmt.Text) + Val(txtExpAmt.Text))
        'txtOpAmt.Text = cnn.Num2(txtOpAmt.Text) : txtRecAmt.Text = cnn.Num2(txtRecAmt.Text) : txtPayAmt.Text = cnn.Num2(txtPayAmt.Text)
        'txtMiscExpAmt.Text = cnn.Num2(txtMiscExpAmt.Text) : txtSalaryAmt.Text = cnn.Num2(txtSalaryAmt.Text) : txtSaleAmt.Text = cnn.Num2(txtSaleAmt.Text)
        'txtPurchaseAmt.Text = cnn.Num2(txtPurchaseAmt.Text) : txtExpAmt.Text = cnn.Num2(txtExpAmt.Text) : txtClosingAmt.Text = cnn.Num2(txtClosingAmt.Text)

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'PrintForm1.Form = Me.ActiveMdiChild

        'PrintForm1.PrintAction = Printing.PrintAction.PrintToPreview
        'PrintForm1.Print(Me, PowerPacks.Printing.PrintForm.PrintOption.Scrollable)
        ''PrintForm1.Print()
    End Sub
End Class