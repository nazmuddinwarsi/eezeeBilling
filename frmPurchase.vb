Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmPurchase
    Dim cnn As New DataConn.Conn
    Dim iEvent As Integer = 0
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtInvoiceNo.Text = "" Then
            cnn.ErrMsgBox("Please Enter Invoice No  !")
            txtInvoiceNo.Focus()
            Exit Sub
        End If
        If txtDate.Text = "" Then
            cnn.ErrMsgBox("Please Enter Invoice Date  !")
            txtDate.Focus()
            Exit Sub
        End If
        If cmbSupplier.Text = "" Then
            cnn.ErrMsgBox("Please Select Supplier Name  !")
            cmbSupplier.Focus()
            Exit Sub
        End If
        If cmbSupplier.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select Supplier Name from List !")
            cmbSupplier.Focus()
            Exit Sub
        End If
        If cmbProduct.Text = "" Then
            cnn.ErrMsgBox("Please Select Product Name  !")
            cmbProduct.Focus()
            Exit Sub
        End If
        If cmbProduct.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select Product Name from List !")
            cmbProduct.Focus()
            Exit Sub
        End If
        If Val(txtQty.Text) = 0 Then
            cnn.ErrMsgBox("Please Enter Product Quantity  !")
            txtQty.Focus()
            Exit Sub
        End If

        
        'If Val(txtRate.Text) = 0 Then
        '    cnn.ErrMsgBox("Please Enter Product Rate  !")
        '    txtRate.Focus()
        '    Exit Sub
        'End If
        If Val(txtAmount.Text) = 0 Then
            If MessageBox.Show("Amount is Zero, Do You want to Continue?", "eezeeBilling", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If
        '---------
        For i = 1 To Grid.Rows.Count - 1
            If cmbProduct.SelectedValue = Grid.Rows(i).Cells(0).Value Then
                If MessageBox.Show("Duplicate Entry, Do You want to Continue?", "eezeeBilling", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
        Next
        '-----------------------------------------------
        Dim iGST, Dividend As Double
        iGST = Val(lblCGST.Text) + Val(lblSGST.Text) + Val(lblIGST.Text)
        'Dividend = 100 + Val(iGST)


        Grid.Rows.Add(1)
        Grid.Rows(Grid.Rows.Count - 1).Cells(0).Value = cmbProduct.SelectedValue
        Grid.Rows(Grid.Rows.Count - 1).Cells(1).Value = cmbProduct.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(2).Value = txtCode.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(3).Value = txtNos.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(4).Value = txtRate.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(5).Value = Val(txtPrice.Text)
        Grid.Rows(Grid.Rows.Count - 1).Cells(6).Value = txtGST.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(7).Value = txtQty.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(8).Value = (Val(txtPrice.Text) * Val(txtQty.Text)) + (Val(txtGST.Text) * Val(txtQty.Text)) ' Val(txtPrice.Text) * Val(txtQty.Text)
        Grid.Rows(Grid.Rows.Count - 1).Cells(9).Value = lblCGST.Text
        'Grid.Rows(Grid.Rows.Count - 1).Cells(10).Value = IIf(Val(lblCGST.Text) > 0, cnn.Num2(Val(txtGST.Text) / 2), 0)
        Grid.Rows(Grid.Rows.Count - 1).Cells(10).Value = IIf(Val(lblCGST.Text) > 0, ((Val(txtPrice.Text) * Val(txtQty.Text)) * iGST / 100) / 2, 0)
        Grid.Rows(Grid.Rows.Count - 1).Cells(11).Value = lblSGST.Text
        'Grid.Rows(Grid.Rows.Count - 1).Cells(12).Value = IIf(Val(lblSGST.Text) > 0, cnn.Num2(Val(txtGST.Text) / 2), 0)
        Grid.Rows(Grid.Rows.Count - 1).Cells(12).Value = IIf(Val(lblSGST.Text) > 0, ((Val(txtPrice.Text) * Val(txtQty.Text)) * iGST / 100) / 2, 0)
        Grid.Rows(Grid.Rows.Count - 1).Cells(13).Value = lblIGST.Text
        'Grid.Rows(Grid.Rows.Count - 1).Cells(14).Value = IIf(Val(lblIGST.Text) > 0, cnn.Num2(Val(txtGST.Text)), 0)
        Grid.Rows(Grid.Rows.Count - 1).Cells(14).Value = IIf(Val(lblIGST.Text) > 0, ((Val(txtPrice.Text) * Val(txtQty.Text)) * iGST / 100), 0)
        GridTotal()
        CalculateSundryGrid()
        cmbProduct.Text = ""

        txtQty.Text = "" : txtNos.Text = "" : txtRate.Text = "" : txtAmount.Text = "" : txtPrice.Text = "" : txtGST.Text = ""
        lblStock.Visible = False : lblText.Visible = False
        cmbProduct.Focus()
    End Sub
    Public Sub GridTotal()
        Dim iTotQty, iTotAmt, iTotNos, iCGST, iSGST, iIGST
        iTotQty = 0 : iTotAmt = 0 : iTotNos = 0 : iCGST = 0 : iSGST = 0 : iIGST = 0
        For i = 0 To Grid.Rows.Count - 1
            iTotAmt = Val(iTotAmt) + (Val(Grid.Rows(i).Cells(5).Value) * Val(Grid.Rows(i).Cells(7).Value))
            iTotQty = Val(iTotQty) + Val(Grid.Rows(i).Cells(7).Value)
            'iCGST = Val(iCGST) + (Val(Grid.Rows(i).Cells(10).Value) * Val(Grid.Rows(i).Cells(7).Value))
            'iSGST = Val(iSGST) + (Val(Grid.Rows(i).Cells(12).Value) * Val(Grid.Rows(i).Cells(7).Value))
            'iIGST = Val(iIGST) + (Val(Grid.Rows(i).Cells(14).Value) * Val(Grid.Rows(i).Cells(7).Value))
            iCGST = Val(iCGST) + (Val(Grid.Rows(i).Cells(10).Value))
            iSGST = Val(iSGST) + (Val(Grid.Rows(i).Cells(12).Value))
            iIGST = Val(iIGST) + (Val(Grid.Rows(i).Cells(14).Value))

        Next
        txtTotalQty.Text = iTotQty
        txtTotalAmt.Text = cnn.Num2(iTotAmt)
        txtCGST.Text = cnn.Num2(iCGST) 'cnn.Num2(Val(iTotAmt) * Val(lblCGST2.Text) / 100)
        txtSGST.Text = cnn.Num2(iSGST) ' cnn.Num2(Val(iTotAmt) * Val(lblSGST2.Text) / 100)
        txtIGST.Text = cnn.Num2(iIGST) 'cnn.Num2(Val(iTotAmt) * Val(lblIGST2.Text) / 100)
        txtGrandTotal.Text = Val(iTotAmt) + Val(txtCGST.Text) + Val(txtSGST.Text) + Val(txtIGST.Text)
        txtGrandTotal.Text = cnn.Num2(txtGrandTotal.Text)
        Dim iDec As Decimal = txtGrandTotal.Text
        txtBillAmt.Text = Math.Round(iDec, 0)
        txtBillAmt.Text = cnn.Num2(txtBillAmt.Text)
        txtRoundOff.Text = Val(txtBillAmt.Text) - Val(txtGrandTotal.Text)
        txtRoundOff.Text = cnn.Num2(txtRoundOff.Text)
    End Sub
    Public Sub CalculateSundryGrid()
        'Dim iGetTax
        'Dim iTotalBasicAmt, iTaxableAmt, iPrevAmt, iTotAmt, iCGSTAmt, iSGSTAmt, iIGSTAmt, iBillAmt As Double
        'iTotalBasicAmt = txtTotalAmount.Text
        'iTaxableAmt = txtTotalAmount.Text
        'iTotAmt = txtTotalAmount.Text
        'iPrevAmt = txtTotalAmount.Text
        'iGetTax = 0


        'txtTotalAmt.Text = iTotAmt 'cnn.Num2(Math.Round(iTotAmt))
        'txtCGST.Text = iTotAmt * Val(lblCGST.Text) / 100 : txtCGST.Text = cnn.Num2(txtCGST.Text)
        'txtSGST.Text = iTotAmt * Val(lblSGST.Text) / 100 : txtSGST.Text = cnn.Num2(txtSGST.Text)
        'txtIGST.Text = iTotAmt * Val(lblIGST.Text) / 100 : txtIGST.Text = cnn.Num2(txtIGST.Text)

        'For i = 0 To grdTax.Rows.Count - 1
        '    If iGetTax = 0 Then
        '        If grdTax.Rows(i).Cells(9).Value = "VAT" Or grdTax.Rows(i).Cells(9).Value = "CST" Then
        '            iGetTax = 1
        '        Else
        '            iGetTax = 0
        '        End If
        '    End If
        '    If grdTax.Rows(i).Cells(5).Value <> "AMT" Then
        '        If grdTax.Rows(i).Cells(6).Value = "BASIC" Then
        '            grdTax.Rows(i).Cells(3).Value = iTotalBasicAmt * grdTax.Rows(i).Cells(2).Value / 100
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
        '        ElseIf grdTax.Rows(i).Cells(6).Value = "TAX" Then
        '            grdTax.Rows(i).Cells(3).Value = iTaxableAmt * grdTax.Rows(i).Cells(2).Value / 100
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
        '        ElseIf grdTax.Rows(i).Cells(6).Value = "BILL" Then
        '            grdTax.Rows(i).Cells(3).Value = iTotAmt * grdTax.Rows(i).Cells(2).Value / 100
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
        '        ElseIf grdTax.Rows(i).Cells(6).Value = "PREV" Then
        '            grdTax.Rows(i).Cells(3).Value = iPrevAmt * grdTax.Rows(i).Cells(2).Value / 100
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
        '        End If
        '    End If
        '    If grdTax.Rows(i).Cells(7).Value = "Y" Then '------- ROUNDOFF
        '        If grdTax.Rows(i).Cells(8).Value = "AUTO" Then '------- ROUNDOFF TYPE
        '            grdTax.Rows(i).Cells(3).Value = Math.Round(Val(grdTax.Rows(i).Cells(3).Value), 0)
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(Val(grdTax.Rows(i).Cells(3).Value))
        '        ElseIf grdTax.Rows(i).Cells(8).Value = "UP" Then
        '            grdTax.Rows(i).Cells(3).Value = cnn.RoundUp(Val(grdTax.Rows(i).Cells(3).Value))
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(Val(grdTax.Rows(i).Cells(3).Value))
        '        ElseIf grdTax.Rows(i).Cells(8).Value = "LOW" Then
        '            grdTax.Rows(i).Cells(3).Value = cnn.RoundDown(Val(grdTax.Rows(i).Cells(3).Value))
        '            grdTax.Rows(i).Cells(3).Value = cnn.Num2(Val(grdTax.Rows(i).Cells(3).Value))
        '        End If
        '    End If
        '    If iGetTax = 0 Then
        '        If grdTax.Rows(i).Cells(4).Value = "ADD" Then
        '            iTaxableAmt = Val(iTaxableAmt) + Val(grdTax.Rows(i).Cells(3).Value)
        '        Else
        '            iTaxableAmt = Val(iTaxableAmt) - Val(grdTax.Rows(i).Cells(3).Value)
        '        End If
        '        iTotAmt = cnn.Num2(iTaxableAmt)
        '    Else
        '        If grdTax.Rows(i).Cells(4).Value = "ADD" Then
        '            iTotAmt = Val(iTotAmt) + Val(grdTax.Rows(i).Cells(3).Value)
        '            iTotAmt = cnn.Num2(iTotAmt)
        '        Else
        '            iTotAmt = Val(iTotAmt) - Val(grdTax.Rows(i).Cells(3).Value)
        '            iTotAmt = cnn.Num2(iTotAmt)
        '        End If
        '    End If
        '    iPrevAmt = grdTax.Rows(i).Cells(3).Value
        '    iPrevAmt = cnn.Num2(iPrevAmt)
        'Next

        'txtBillAmt.Text = Val(txtTotalAmt.Text) + Val(txtCGST.Text) + Val(txtSGST.Text) + Val(txtIGST.Text)
        'txtBillAmt.Text = cnn.Num2(txtBillAmt.Text)
    End Sub

    Private Sub btnAdd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd2.Click
        If cmbSundry.Text = "" Then
            cnn.ErrMsgBox("Please Select Type !")
            cmbSundry.Focus()
            Exit Sub
        End If
        If Val(txtValue.Text) = 0 Then
            cnn.ErrMsgBox("Please Enter Valid Value !")
            txtValue.Focus()
            Exit Sub
        End If
        '---------
        Dim strSQL As String
        strSQL = "SELECT ID, SundryName, DefaultValue, AmountAs, PercentOf, RoundOff, RoundOffType, SundryType FROM dbo.BillSundryMaster" _
        & " WHERE (ID = " & Val(cmbSundry.SelectedValue) & ")"
        'strSQL = "SELECT dbo.BillSundryMaster.ID, dbo.BillSundryMaster.SundryName, dbo.BillSundryMaster.DefaultValue, dbo.BillSundryNature.Type," _
        '& " dbo.BillSundryMaster.AmountAs, dbo.BillSundryMaster.PercentOf, dbo.BillSundryMaster.RoundOff, dbo.BillSundryMaster.RoundOffType," _
        '& " dbo.BillSundryMaster.SundryType FROM dbo.BillSundryMaster INNER JOIN dbo.BillSundryNature ON dbo.BillSundryMaster.NatureID = dbo.BillSundryNature.ID" _
        '& " Where (dbo.BillSundryMaster.ID = " & Val(cmbSundry.SelectedValue) & ")"
        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If dr.Read Then
            grdTax.Rows.Add(1)
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(0).Value = dr.Item("ID")
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(1).Value = dr.Item("SundryName")
            If dr.Item("AmountAs") = "AMT" Then
                grdTax.Rows(grdTax.Rows.Count - 1).Cells(3).Value = txtValue.Text
            Else
                grdTax.Rows(grdTax.Rows.Count - 1).Cells(2).Value = txtValue.Text
            End If
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(4).Value = dr.Item("SundryType")
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(5).Value = dr.Item("AmountAs")
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(6).Value = dr.Item("PercentOf")
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(7).Value = dr.Item("RoundOff")
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(8).Value = dr.Item("RoundOffType")
            'grdTax.Rows(grdTax.Rows.Count - 1).Cells(9).Value = dr.Item("Type")
        End If
        cmbSundry.Text = ""
        txtValue.Text = ""
        CalculateSundryGrid()
        cmbSundry.Focus()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        If Grid.Rows.Count > 0 Then
            Grid.Rows.RemoveAt(Grid.CurrentCell.RowIndex)
        End If
        GridTotal()
        CalculateSundryGrid()
    End Sub

    Private Sub btnDel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel2.Click
        If grdTax.Rows.Count > 0 Then
            grdTax.Rows.RemoveAt(grdTax.CurrentCell.RowIndex)
        End If
        CalculateSundryGrid()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If cmbSupplier.Text = "" Then
            cnn.ErrMsgBox("Please Select Supplier Name  !")
            cmbSupplier.Focus()
            Exit Sub
        End If
        If cmbSupplier.SelectedValue = 0 Then
            cnn.ErrMsgBox("Please Select Supplier Name from List  !")
            cmbSupplier.Focus()
            Exit Sub
        End If
        If Grid.Rows.Count < 1 Then
            cnn.ErrMsgBox("Please Enter Product Details  !")
            cmbProduct.Focus()
            Exit Sub
        End If
        Dim strSQL As String
        '-------------- CHECK STOCK
        If lblID.Text <> "" Then
            For i = 0 To Grid.Rows.Count - 1
                '-------------------------------
                cnn.Check_Insert_Product(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value), 0)
                '-------------------------------
                strSQL = "SELECT dbo.PurchaseDetails.Qty AS PurchaseQty, dbo.ProductStock.Qty AS StockQty, dbo.PurchaseDetails.HeaderID, dbo.PurchaseDetails.ProductID" _
                & " FROM dbo.PurchaseDetails INNER JOIN dbo.ProductStock ON dbo.PurchaseDetails.ProductID = dbo.ProductStock.ProductID" _
                & " WHERE (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ") AND (dbo.PurchaseDetails.ProductID = " & Val(Grid.Rows(i).Cells(0).Value) & ")" _
                & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
                & " AND (dbo.ProductStock.SMID = 0)"

                Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                If cmd.Connection.State = 1 Then cmd.Connection.Close()
                cmd.Connection.Open()
                Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
                If dr.Read Then
                    If ((Val(dr.Item("StockQty")) + Val(Grid.Rows(i).Cells(7).Value)) - Val(dr.Item("PurchaseQty"))) < 0 Then
                        cnn.ErrMsgBox("One or more Product Stock is not available, Please check  !")
                        dr.Close()
                        cmd.Connection.Close()
                        Exit Sub
                    End If
                    'Else
                    '    cnn.ErrMsgBox("Something went Wrong, Please Contact Software Administrator  !")
                    '    Exit Sub
                End If
                dr.Close()
                cmd.Connection.Close()
            Next
            '--------------------------------------- STOCK REVERSE
            strSQL = "UPDATE ProductStock SET dbo.ProductStock.Qty = (dbo.ProductStock.Qty - dbo.PurchaseDetails.Qty)" _
            & " FROM dbo.ProductStock INNER JOIN dbo.PurchaseDetails ON dbo.ProductStock.ProductID = dbo.PurchaseDetails.ProductID" _
            & " WHERE (dbo.ProductStock.ProductID = dbo.PurchaseDetails.ProductID) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")" _
            & " AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.PurchaseDetails.HeaderID = " & Val(lblID.Text) & ")" _
            & " AND (dbo.ProductStock.SMID = 0)"

            cnn.executeSQL(strSQL)
            '-------------- CHECK STOCK
        End If

        'Dim objTrans As SqlTransaction
        'objTrans = cnn.cnn.BeginTransaction
        If lblID.Text = "" Then
            strSQL = "INSERT INTO PurchaseHeader (InvoiceNo, InvoiceDate, AccountID, TotalQty, TotalNos, TotalAmount, TotalCGST, TotalSGST, TotalIGST, GrandTotal, RoundOff, BillAmount, Remarks, MasterCompanyID, MasterBranchID, CreatedBy, CreatedDate) OutPut Inserted.ID as HeaderID VALUES (" _
            & cnn.SQLquote(txtInvoiceNo.Text) & ",'" & cnn.GetDate(txtDate.Text) & "'," & Val(cmbSupplier.SelectedValue) & "," & Val(txtTotalQty.Text) & "," & Val(txtTotalNos.Text) & "," & Val(txtTotalAmt.Text) _
            & "," & Val(txtCGST.Text) & "," & Val(txtSGST.Text) & "," & Val(txtIGST.Text) & "," & Val(txtGrandTotal.Text) & "," & Val(txtRoundOff.Text) & "," & Val(txtBillAmt.Text) & "," & cnn.SQLquote(txtRemarks.Text) & "," & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"

            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                lblHeaderID.Text = dr.Item("HeaderID")
            End If
            dr.Close()
            cmd.Connection.Close()
            Call cnn.GenerateLog("Purchase", "Add", 0, txtInvoiceNo.Text & ", Dt. " & txtDate.Text)
        Else
            strSQL = "UPDATE PurchaseHeader SET InvoiceNo=" & cnn.SQLquote(txtInvoiceNo.Text) & ", InvoiceDate='" & cnn.GetDate(txtDate.Text) & "'" _
            & ", AccountID=" & Val(cmbSupplier.SelectedValue) & ", TotalQty=" & Val(txtTotalQty.Text) & ", TotalNos=" & Val(txtTotalNos.Text) & ", TotalAmount=" & Val(txtTotalAmt.Text) _
            & ", TotalCGST=" & Val(txtCGST.Text) & ", TotalSGST=" & Val(txtSGST.Text) & ", TotalIGST=" & Val(txtIGST.Text) & ", GrandTotal=" & Val(txtGrandTotal.Text) & ", RoundOff=" & Val(txtRoundOff.Text) & ", BillAmount=" & Val(txtBillAmt.Text) & ", Remarks=" & cnn.SQLquote(txtRemarks.Text) _
            & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' Where ID=" & Val(lblID.Text)
            cnn.executeSQL(strSQL)
            lblHeaderID.Text = lblID.Text
            Call cnn.GenerateLog("Purchase", "Edit", lblID.Text, txtInvoiceNo.Text & ", Dt. " & txtDate.Text)
        End If

        SaveDetails()
        '-------------------------Update Stock
        For i = 0 To Grid.Rows.Count - 1
            '=====================================
            strSQL = "UPDATE ProductStock SET Qty = (Qty + " & Val(Grid.Rows(i).Cells(7).Value) & ")" _
            & " Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) _
            & " AND ProductID=" & Val(Grid.Rows(i).Cells(0).Value) _
            & " AND SMID = 0"
            cnn.executeSQL(strSQL)
        Next
        '-------------------------Update Stock
        'objTrans.Commit()
        cnn.CloseConn()
        If lblID.Text <> "" Then
            cnn.InfoMsgBox("Record has been Updated Successfully  !")
            Me.Close()
        Else
            cnn.InfoMsgBox("Record has been Added Successfully  !")
            lblID.Text = "" : txtInvoiceNo.Text = "" : txtDate.Text = Date.Today : cmbSupplier.Text = "" : txtCity.Text = ""
            cmbProduct.Text = "" : txtCode.Text = "" : txtQty.Text = "" : txtRate.Text = "" : txtAmount.Text = "" : Grid.Rows.Clear()
            txtTotalQty.Text = "" : txtTotalNos.Text = "" : txtTotalAmount.Text = "" : txtRemarks.Text = "" : cmbSundry.Text = ""
            txtValue.Text = "" : grdTax.Rows.Clear() : txtBillAmt.Text = "" : txtTotalAmt.Text = "" : txtCGST.Text = "" : txtSGST.Text = ""
            txtIGST.Text = "" : txtGrandTotal.Text = "" : txtRoundOff.Text = "" : txtInvoiceNo.Focus()
        End If

    End Sub
    Private Sub SaveDetails()
        Dim strSQL As String
        If lblID.Text <> "" Then
            cnn.executeSQL("DELETE From PurchaseDetails Where HeaderID=" & lblID.Text)
            cnn.executeSQL("DELETE From BillSundryDetails Where HeaderID=" & lblID.Text & " AND vType='PURCHASE'")
        End If

        For i = 0 To Grid.Rows.Count - 1
            '-------------------------------
            cnn.Check_Insert_Product(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value), 0)
            '-------------------------------
            strSQL = "INSERT INTO PurchaseDetails (HeaderID, ProductID, Qty, Nos, Rate, BasicPrice, GSTAmt, Amount, CGSTPercent, CGSTAmt, SGSTPercent, SGSTAmt, IGSTPercent, IGSTAmt) VALUES("
            strSQL = strSQL & Val(lblHeaderID.Text) & "," & Val(Grid.Rows(i).Cells(0).Value) & "," & Val(Grid.Rows(i).Cells(7).Value) & "," _
            & Val(Grid.Rows(i).Cells(3).Value) & "," & Val(Grid.Rows(i).Cells(4).Value) & "," & Val(Grid.Rows(i).Cells(5).Value) & "," & Val(Grid.Rows(i).Cells(6).Value) & "," & Val(Grid.Rows(i).Cells(8).Value) & "," _
            & Val(Grid.Rows(i).Cells(9).Value) & "," & Val(Grid.Rows(i).Cells(10).Value) & "," & Val(Grid.Rows(i).Cells(11).Value) & "," & Val(Grid.Rows(i).Cells(12).Value) & "," & Val(Grid.Rows(i).Cells(13).Value) & "," _
            & Val(Grid.Rows(i).Cells(14).Value) & ")"
            cnn.executeSQL(strSQL)
        Next

        'For i = 0 To grdTax.Rows.Count - 1
        '    strSQL = "INSERT INTO BillSundryDetails (HeaderID, vType, SundryID, Value, Amount) VALUES (" _
        '    & Val(lblHeaderID.Text) & ",'PURCHASE'," & Val(grdTax.Rows(i).Cells(0).Value) & "," & Val(grdTax.Rows(i).Cells(2).Value) & "," & Val(grdTax.Rows(i).Cells(3).Value) & ")"
        '    cnn.executeSQL(strSQL)

        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(0).Value = cmbProduct.SelectedValue
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(1).Value = cmbProduct.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(2).Value = txtCode.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(3).Value = txtNos.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(4).Value = txtRate.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(5).Value = Val(txtPrice.Text)
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(6).Value = txtGST.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(7).Value = txtQty.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(8).Value = (Val(txtPrice.Text) * Val(txtQty.Text)) + (Val(txtGST.Text) * Val(txtQty.Text)) ' Val(txtPrice.Text) * Val(txtQty.Text)
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(9).Value = lblCGST.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(10).Value = IIf(Val(lblCGST.Text) > 0, cnn.Num2(Val(txtGST.Text) / 2), 0)
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(11).Value = lblSGST.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(12).Value = IIf(Val(lblSGST.Text) > 0, cnn.Num2(Val(txtGST.Text) / 2), 0)
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(13).Value = lblIGST.Text
        '    'Grid.Rows(Grid.Rows.Count - 1).Cells(14).Value = IIf(Val(lblIGST.Text) > 0, cnn.Num2(Val(txtGST.Text)), 0)
        'Next
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSupplier.SelectedIndexChanged
        If cmbSupplier.Text <> "" Then
            Dim strSQL As String
            strSQL = "Select City, CGST, SGST, IGST from tblAccount Where tType='SUPPLIER' AND ID=" & Val(cmbSupplier.SelectedValue)
            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                txtCity.Text = dr.Item("City")
                If Not IsDBNull(dr.Item("CGST")) Then
                    If dr.Item("CGST") = "True" Then
                        chkCGST.Checked = True
                    Else
                        chkCGST.Checked = False
                    End If
                End If
                If Not IsDBNull(dr.Item("SGST")) Then
                    If dr.Item("SGST") = "True" Then
                        chkSGST.Checked = True
                    Else
                        chkSGST.Checked = False
                    End If
                End If
                If Not IsDBNull(dr.Item("IGST")) Then
                    If dr.Item("IGST") = "True" Then
                        chkIGST.Checked = True
                    Else
                        chkIGST.Checked = False
                    End If
                End If
            End If
            dr.Close()
            cmd.Connection.Close()
        Else
            txtCity.Text = "" : lblSGST.Text = "" : lblCGST.Text = "" : lblIGST.Text = ""
        End If

    End Sub
    Private Sub GetDetails()
        txtCode.Text = "" : txtRate.Text = ""
        If cmbProduct.Text <> "" Then
            Dim strSQL As String
            strSQL = "SELECT dbo.Products.ProductCode, dbo.Products.PurchasePrice, dbo.Products.CGST, dbo.Products.SGST, dbo.Products.IGST, dbo.ProductStock.Qty FROM dbo.Products INNER JOIN dbo.ProductStock ON dbo.Products.ID = dbo.ProductStock.ProductID" _
            & " WHERE (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.ProductStock.ProductID = " & Val(cmbProduct.SelectedValue) & ")" _
            & " AND (dbo.ProductStock.SMID = 0) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")"

            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("ProductCode")) Then
                    txtCode.Text = dr.Item("ProductCode")
                End If
                If chkCGST.Checked = True Then
                    If Not IsDBNull(dr.Item("CGST")) Then
                        lblCGST.Text = dr.Item("CGST")
                    End If
                End If
                If chkSGST.Checked = True Then
                    If Not IsDBNull(dr.Item("SGST")) Then
                        lblSGST.Text = dr.Item("SGST")
                    End If
                End If
                If chkIGST.Checked = True Then
                    If Not IsDBNull(dr.Item("IGST")) Then
                        lblIGST.Text = dr.Item("IGST")
                    End If
                End If
                If Not IsDBNull(dr.Item("PurchasePrice")) Then
                    txtPrice.Text = dr.Item("PurchasePrice")
                End If
            Else
                txtCode.Text = "" : txtRate.Text = ""
            End If
            GetGSTAmtFromPrice()
            dr.Close()
            cmd.Connection.Close()
            lblStock.Text = cnn.GetStock(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(cmbProduct.SelectedValue), 0)
        Else
            txtCode.Text = "" : txtRate.Text = ""
        End If
    End Sub

    Private Sub GetGSTAmtFromPrice()
        Dim iGST, iCGST, iSGST, iIGST As Double
        iGST = Val(lblCGST.Text) + Val(lblSGST.Text) + Val(lblIGST.Text)
        'Dividend = 100 + Val(iGST)
        txtGST.Text = Val(txtPrice.Text) * iGST / 100
        txtGST.Text = cnn.Num2(txtGST.Text)
        txtRate.Text = Val(txtPrice.Text) + Val(txtGST.Text)
        txtRate.Text = cnn.Num2(txtRate.Text)
        'txtPrice.Text = cnn.Num2(txtPrice.Text)
        txtAmount.Text = Val(txtQty.Text) * Val(txtRate.Text)

    End Sub
    Private Sub Grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub

    Private Sub Grid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.Click
        If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblStock.Text = cnn.GetStock(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(crRowIndex).Cells(0).Value.ToString), 0)
                lblStock.Visible = True : lblText.Visible = True
            End If
        End If
    End Sub

    Private Sub frmPurchase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width : lblHR1.Width = MDI.Width : lblHR2.Width = MDI.Width
        txtDate.Text = Date.Today
        ''==============================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "Select ID, AccountName from tblAccount Where tType='SUPPLIER' Order BY AccountName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbSupplier.DisplayMember = "AccountName"
        cmbSupplier.ValueMember = "ID"
        cmbSupplier.DataSource = ds.Tables(0)
        cmbSupplier.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        '------------------------
        strSQL = "Select ID, SundryName from BillSundryMaster Order BY SundryName ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbSundry.DisplayMember = "SundryName"
        cmbSundry.ValueMember = "ID"
        cmbSundry.DataSource = ds.Tables(0)
        cmbSundry.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        '------------------------
        strSQL = "Select ID, ProductName from Products Order BY ProductCode ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbProduct.DisplayMember = "ProductName"
        cmbProduct.ValueMember = "ID"
        cmbProduct.DataSource = ds.Tables(0)
        cmbProduct.SelectedIndex = -1
        ds = Nothing
        da = Nothing
        iEvent = 1
    End Sub

    Private Sub cmbProduct_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProduct.SelectedIndexChanged
        If iEvent > 0 Then
            If cmbProduct.Text <> "" Then
                GetDetails()
            Else
                txtCode.Text = "" : txtRate.Text = "" : txtAmount.Text = ""
                lblStock.Visible = False : lblText.Visible = False
            End If
        End If
    End Sub

    Private Sub cmbSundry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSundry.SelectedIndexChanged
        If iEvent > 0 Then
            txtValue.Text = ""
            If cmbSundry.Text <> "" Then
                GetSundryDetails()
            Else
                txtValue.Text = ""
            End If
        End If
        
    End Sub
    Private Sub GetSundryDetails()
        Dim strSQL As String
        strSQL = "SELECT DefaultValue FROM dbo.BillSundryMaster Where (dbo.BillSundryMaster.ID = " & Val(cmbSundry.SelectedValue) & ")"
        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If dr.Read Then
            If Not IsDBNull(dr.Item("DefaultValue")) Then
                txtValue.Text = dr.Item("DefaultValue")
            Else
                cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
            End If
        Else
            txtValue.Text = ""
        End If
        dr.Close()
        cmd.Connection.Close()
    End Sub
    Private Sub cmbProduct_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProduct.TextChanged
        If iEvent > 0 Then
            If cmbProduct.Text <> "" Then
                GetDetails()
            Else
                txtCode.Text = "" : txtRate.Text = "" : txtAmount.Text = "" : txtNos.Text = ""
                lblStock.Visible = False : lblText.Visible = False
            End If
        End If
    End Sub

    Private Sub cmbSundry_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSundry.TextChanged
        If iEvent > 0 Then
            txtValue.Text = ""
            If cmbSundry.Text <> "" Then
                GetSundryDetails()
            Else
                txtValue.Text = ""
            End If
        End If
       
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged
        GetGSTAmtFromPrice()
    End Sub

    Private Sub txtQty_LostFocus(sender As Object, e As EventArgs) Handles txtQty.LostFocus
        txtQty.Text = cnn.Num2(txtQty.Text)
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        GetAmount()
    End Sub

    Private Sub txtRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate.KeyPress
        e.Handled = cnn.IsNumericTextbox(sender, e.KeyChar)
    End Sub

    Private Sub txtRate_LostFocus(sender As Object, e As EventArgs) Handles txtRate.LostFocus
        txtRate.Text = cnn.Num2(txtRate.Text)
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        GetAmount()
    End Sub
    Private Sub GetAmount()
        txtAmount.Text = Val(txtQty.Text) * Val(txtPrice.Text)
        txtAmount.Text = cnn.Num2(txtAmount.Text)
    End Sub
End Class