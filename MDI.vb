Imports System.Windows.Forms

Public Class MDI
    Dim cnn As New DataConn.Conn
    Private Sub mnuCategory_Click(sender As Object, e As EventArgs) Handles mnuCategory.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(2, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmCategoryList)
    End Sub

    Private Sub mnuProducts_Click(sender As Object, e As EventArgs) Handles mnuProducts.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(3, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmProductList)
    End Sub

    Private Sub mnuSupplier_Click(sender As Object, e As EventArgs) Handles mnuSupplier.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(4, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        frmAccountList.lblType.Text = "SUPPLIER"
        cnn.ShowForm(frmAccountList)
    End Sub

    Private Sub mnuBuyer_Click(sender As Object, e As EventArgs) Handles mnuBuyer.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(4, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        frmAccountList.lblType.Text = "BUYER"
        cnn.ShowForm(frmAccountList)
    End Sub

    Private Sub MDI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    Private Sub MDI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub mnuQuit_Click(sender As Object, e As EventArgs) Handles mnuQuit.Click
        Application.Exit()
    End Sub

    Private Sub mnuPurchase_Click(sender As Object, e As EventArgs) Handles mnuPurchase.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(6, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchase)
    End Sub

    Private Sub mnuPurReturn_Click(sender As Object, e As EventArgs) Handles mnuPurReturn.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(7, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchaseReturn)
    End Sub

    Private Sub mnuIssueSM_Click(sender As Object, e As EventArgs) Handles mnuIssueSM.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(10, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmAddExpense)
    End Sub

    Private Sub mnuSM_Click(sender As Object, e As EventArgs) Handles mnuSM.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(5, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpList)
    End Sub
    Private Sub mnuPurList_Click(sender As Object, e As EventArgs) Handles mnuPurList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(6, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchaseList)
    End Sub

    Private Sub mnuPurRetList_Click(sender As Object, e As EventArgs) Handles mnuPurRetList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(7, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchaseReturnList)
    End Sub

    Private Sub mnuIssueSMList_Click(sender As Object, e As EventArgs) Handles mnuIssueSMList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(10, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmExpenseList)
    End Sub

    Private Sub mnuBillSundry_Click(sender As Object, e As EventArgs) Handles mnuBillSundry.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        cnn.ShowForm(frmBillSundryList)
    End Sub

    Private Sub mnuCompany_Click(sender As Object, e As EventArgs) Handles mnuCompany.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        'cnn.ShowForm(frmCompanyList)
        cnn.ShowForm(frmPara)
    End Sub

    Private Sub mnuBranch_Click(sender As Object, e As EventArgs) Handles mnuBranch.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        cnn.ShowForm(frmBranchList)
    End Sub

    Private Sub mnuUserRights_Click(sender As Object, e As EventArgs) Handles mnuUserRights.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        cnn.ShowForm(frmUserRights)
    End Sub

    Private Sub mnuUser_Click(sender As Object, e As EventArgs) Handles mnuUser.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        cnn.ShowForm(frmUserList)
    End Sub

    Private Sub btnCategory_Click(sender As Object, e As EventArgs) Handles btnCategory.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(2, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmCategory)
    End Sub

    Private Sub btnProducts_Click(sender As Object, e As EventArgs) Handles btnProducts.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(3, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmProduct)
    End Sub

    Private Sub btnSupplier_Click(sender As Object, e As EventArgs) Handles btnSupplier.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(4, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        frmAccountList.lblType.Text = "SUPPLIER"
        cnn.ShowForm(frmAccountList)
    End Sub

    Private Sub btnBuyer_Click(sender As Object, e As EventArgs) Handles btnBuyer.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(4, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        frmAccount.lblType.Text = "BUYER"
        cnn.ShowForm(frmAccount)
    End Sub

    Private Sub btnSalesMan_Click(sender As Object, e As EventArgs) Handles btnExpHead.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(1, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmAddExpType)
    End Sub

    Private Sub btnPurchase_Click(sender As Object, e As EventArgs) Handles btnPurchase.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(6, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchase)
    End Sub

    Private Sub btnPurReturn_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(8, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPurchaseReturn)
    End Sub

    Private Sub btnSale_Click(sender As Object, e As EventArgs) Handles btnSale.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(7, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSale)
    End Sub

    Private Sub btnSaleReturn_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(9, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSaleReturn)
    End Sub

    Private Sub mnuPayment_Click(sender As Object, e As EventArgs) Handles mnuPayment.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(8, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPayment)
    End Sub

    Private Sub mnuPaymentList_Click(sender As Object, e As EventArgs) Handles mnuPaymentList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(8, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmPaymentList)
    End Sub

    Private Sub mnuReciept_Click(sender As Object, e As EventArgs) Handles mnuReciept.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(9, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmReceipt)
    End Sub

    Private Sub mnuRecieptList_Click(sender As Object, e As EventArgs) Handles mnuRecieptList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(9, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmReceiptList)
    End Sub



    Private Sub mnuSale_Click(sender As Object, e As EventArgs) Handles mnuSale.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(7, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSale)
    End Sub

    Private Sub mnuSaleReturn_Click(sender As Object, e As EventArgs) Handles mnuSaleReturn.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(10, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSaleReturn)
    End Sub

    Private Sub mnuSaleList_Click(sender As Object, e As EventArgs) Handles mnuSaleList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(7, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSaleList)
    End Sub

    Private Sub mnuSaleRetList_Click(sender As Object, e As EventArgs) Handles mnuSaleRetList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(10, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmSaleReturnList)
    End Sub

    Private Sub mnuExpType_Click(sender As Object, e As EventArgs) Handles mnuExpType.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(1, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmExpTypeList)
    End Sub

    Private Sub mnuEmpSal_Click(sender As Object, e As EventArgs) Handles mnuEmpSal.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(14, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpSalary)
    End Sub

    Private Sub mnuEmpAtt_Click(sender As Object, e As EventArgs) Handles mnuEmpAtt.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(13, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpAttendance)
    End Sub

    Private Sub mnuEmpAttList_Click(sender As Object, e As EventArgs) Handles mnuEmpAttList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(13, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpAttendanceList)
    End Sub

    Private Sub mnuEmpSalList_Click(sender As Object, e As EventArgs) Handles mnuEmpSalList.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(14, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpSalaryList)
    End Sub

    Private Sub mnuComStatement_Click(sender As Object, e As EventArgs) Handles mnuComStatement.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        'cnn.ShowForm(frmCompanyStatement)
    End Sub

    Private Sub btnEmpAttendance_Click(sender As Object, e As EventArgs) Handles btnGST.Click
        'If lblAccessType.Text <> "A" Then
        '    If cnn.GetUserAccess(13, "fldView") = False Then
        '        cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
        '        Exit Sub
        '    End If
        'End If
        cnn.ShowForm(frmGST)
    End Sub

    Private Sub btnEmpSalary_Click(sender As Object, e As EventArgs) Handles btnEmpSalary.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(14, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpSalary)
    End Sub

   

    Private Sub mnuBank_Click(sender As Object, e As EventArgs) Handles mnuBank.Click
        If lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(5, "fldView") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmBankList)
    End Sub

    Private Sub mnuStock_Click(sender As Object, e As EventArgs) Handles mnuStock.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        frmStock.Show()
    End Sub

    Private Sub mnuBuyerStatement_Click(sender As Object, e As EventArgs) Handles mnuBuyerStatement.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        cnn.ShowForm(frmBuyerStatement)
    End Sub

    Private Sub mnuSupplierStatement_Click(sender As Object, e As EventArgs) Handles mnuSupplierStatement.Click
        If lblAccessType.Text <> "A" Then
            Exit Sub
        End If
        cnn.ShowForm(frmSupplierStatement)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        cnn.UPDATE_MY_STOCK()
    End Sub
End Class
