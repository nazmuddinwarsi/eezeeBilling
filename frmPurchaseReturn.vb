Imports System
Imports System.Data
Imports System.Drawing
Imports System.Data.Odbc

Public Class frmPurchaseReturn
    Dim cnn As New DataConn.Conn
    Public Sub GridTotal()
        Dim iTotQty, iTotAmt, iTotNos
        iTotQty = 0 : iTotAmt = 0 : iTotNos = 0
        For i = 0 To Grid.Rows.Count - 1
            iTotQty = Val(iTotQty) + Val(Grid.Rows(i).Cells(3).Value)
            iTotNos = Val(iTotNos) + Val(Grid.Rows(i).Cells(4).Value)
            iTotAmt = Val(iTotAmt) + Val(Grid.Rows(i).Cells(6).Value)
        Next
        txtTotalQty.Text = iTotQty : txtTotalNos.Text = iTotNos : txtTotalAmount.Text = cnn.Num2(iTotAmt)
    End Sub
    Public Sub CalculateSundryGrid()
        Dim iTotalBasicAmt, iTaxableAmt, iBillAmt, iPrevAmt, iGetTax
        iTotalBasicAmt = txtTotalAmount.Text
        iTaxableAmt = txtTotalAmount.Text
        iBillAmt = txtTotalAmount.Text
        iPrevAmt = txtTotalAmount.Text
        iGetTax = 0

        For i = 0 To grdTax.Rows.Count - 1
            If iGetTax = 0 Then
                If grdTax.Rows(i).Cells(9).Value = "VAT" Or grdTax.Rows(i).Cells(9).Value = "CST" Then
                    iGetTax = 1
                Else
                    iGetTax = 0
                End If
            End If
            If grdTax.Rows(i).Cells(5).Value <> "AMT" Then
                If grdTax.Rows(i).Cells(6).Value = "BASIC" Then
                    grdTax.Rows(i).Cells(3).Value = iTotalBasicAmt * grdTax.Rows(i).Cells(2).Value / 100
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                ElseIf grdTax.Rows(i).Cells(6).Value = "TAX" Then
                    grdTax.Rows(i).Cells(3).Value = iTaxableAmt * grdTax.Rows(i).Cells(2).Value / 100
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                ElseIf grdTax.Rows(i).Cells(6).Value = "BILL" Then
                    grdTax.Rows(i).Cells(3).Value = iBillAmt * grdTax.Rows(i).Cells(2).Value / 100
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                ElseIf grdTax.Rows(i).Cells(6).Value = "PREV" Then
                    grdTax.Rows(i).Cells(3).Value = iPrevAmt * grdTax.Rows(i).Cells(2).Value / 100
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                End If
            End If
            If grdTax.Rows(i).Cells(7).Value = "Y" Then '------- ROUNDOFF
                If grdTax.Rows(i).Cells(8).Value = "AUTO" Then '------- ROUNDOFF TYPE
                    grdTax.Rows(i).Cells(3).Value = Math.Round(grdTax.Rows(i).Cells(3).Value, 0)
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                ElseIf grdTax.Rows(i).Cells(8).Value = "UP" Then
                    grdTax.Rows(i).Cells(3).Value = cnn.RoundUp(grdTax.Rows(i).Cells(3).Value)
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                ElseIf grdTax.Rows(i).Cells(8).Value = "LOW" Then
                    grdTax.Rows(i).Cells(3).Value = cnn.RoundDown(grdTax.Rows(i).Cells(3).Value)
                    grdTax.Rows(i).Cells(3).Value = cnn.Num2(grdTax.Rows(i).Cells(3).Value)
                End If
            End If
            If iGetTax = 0 Then
                If grdTax.Rows(i).Cells(4).Value = "ADD" Then
                    iTaxableAmt = Val(iTaxableAmt) + Val(grdTax.Rows(i).Cells(3).Value)
                Else
                    iTaxableAmt = Val(iTaxableAmt) - Val(grdTax.Rows(i).Cells(3).Value)
                End If
                iBillAmt = cnn.Num2(iTaxableAmt)
            Else
                If grdTax.Rows(i).Cells(4).Value = "ADD" Then
                    iBillAmt = Val(iBillAmt) + Val(grdTax.Rows(i).Cells(3).Value)
                    iBillAmt = cnn.Num2(iBillAmt)
                Else
                    iBillAmt = Val(iBillAmt) - Val(grdTax.Rows(i).Cells(3).Value)
                    iBillAmt = cnn.Num2(iBillAmt)
                End If
            End If
            iPrevAmt = grdTax.Rows(i).Cells(3).Value
            iPrevAmt = cnn.Num2(iPrevAmt)
        Next
        txtBillAmount.Text = cnn.Num2(iBillAmt)
    End Sub
    Private Sub SaveDetails()
        Dim strSQL As String
        If lblID.Text <> "" Then
            cnn.executeSQL("DELETE From PurchaseReturnDetails Where HeaderID=" & lblID.Text)
            cnn.executeSQL("DELETE From BillSundryDetails Where HeaderID=" & lblID.Text & " AND vType='PURCHASERETURN'")
        End If

        For i = 0 To Grid.Rows.Count - 1
            '-------------------------------
            cnn.Check_Insert_Product(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value), 0)
            '-------------------------------
            strSQL = "INSERT INTO PurchaseReturnDetails (HeaderID, ProductID, Qty, Nos, Rate, Amount) VALUES("
            strSQL = strSQL & Val(lblHeaderID.Text) & "," & Val(Grid.Rows(i).Cells(0).Value) & "," & Val(Grid.Rows(i).Cells(3).Value) & "," _
            & Val(Grid.Rows(i).Cells(4).Value) & "," & Val(Grid.Rows(i).Cells(5).Value) & "," & Val(Grid.Rows(i).Cells(6).Value) & ")"
            cnn.executeSQL(strSQL)
        Next

        For i = 0 To grdTax.Rows.Count - 1
            strSQL = "INSERT INTO BillSundryDetails (HeaderID, vType, SundryID, Value, Amount) VALUES (" _
            & Val(lblHeaderID.Text) & ",'PURCHASERETURN'," & Val(grdTax.Rows(i).Cells(0).Value) & "," & Val(grdTax.Rows(i).Cells(2).Value) & "," & Val(grdTax.Rows(i).Cells(3).Value) & ")"
            cnn.executeSQL(strSQL)
        Next
    End Sub
    Private Sub GetDetails()
        txtCode.Text = "" : txtRate.Text = ""
        If cmbProduct.Text <> "" Then
            Dim strSQL As String
            strSQL = "SELECT dbo.Products.ProductName, dbo.Products.PurchasePrice, dbo.ProductStock.Qty FROM dbo.Products INNER JOIN dbo.ProductStock ON dbo.Products.ID = dbo.ProductStock.ProductID" _
              & " WHERE (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.ProductStock.ProductID = " & Val(cmbProduct.SelectedValue) & ")" _
              & " AND (dbo.ProductStock.SMID = 0) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")"

            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("ProductName")) Then
                    txtCode.Text = dr.Item("ProductName")
                End If
                If Not IsDBNull(dr.Item("Qty")) Then
                    lblStock.Text = dr.Item("Qty")
                    lblStock.Visible = True : lblText.Visible = True
                End If
                If Not IsDBNull(dr.Item("PurchasePrice")) Then
                    txtRate.Text = dr.Item("PurchasePrice")
                End If
            Else
                txtCode.Text = "" : txtRate.Text = ""
            End If
            dr.Close()
            cmd.Connection.Close()
        Else
            txtCode.Text = "" : txtRate.Text = ""
        End If
    End Sub

    Private Sub GetAmount()
        txtAmount.Text = Val(txtQty.Text) * Val(txtRate.Text)
    End Sub

    Private Sub frmPurchaseReturn_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            Me.Close()
        ElseIf e.KeyCode = 13 Then
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub frmPurchaseReturn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        StripPanel.Width = MDI.Width : lblHR1.Width = MDI.Width : lblHR2.Width = MDI.Width
        txtDate.Text = Date.Today
        ''==============================
        Dim strSQL As String
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        strSQL = "SELECT PurchaseReturnNo from Para Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
           & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
        Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
        If cmd.Connection.State = 1 Then cmd.Connection.Close()
        cmd.Connection.Open()
        Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
        If dr.Read Then
            If Not IsDBNull(dr.Item("PurchaseReturnNo")) Then
                txtInvoiceNo.Text = dr.Item("PurchaseReturnNo")
            Else
                cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
                Exit Sub
            End If
        Else
            cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
            Exit Sub
        End If
        dr.Close()
        cmd.Connection.Close()
        '---------------------------
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
        strSQL = "Select ID, ProductCode from Products Order BY ProductCode ASC"
        da = New OdbcDataAdapter(strSQL, cnn.cnn)
        ds = New DataSet()
        da.Fill(ds)
        cmbProduct.DisplayMember = "ProductCode"
        cmbProduct.ValueMember = "ID"
        cmbProduct.DataSource = ds.Tables(0)
        cmbProduct.SelectedIndex = -1
        ds = Nothing
        da = Nothing
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnAdd2_Click(sender As Object, e As EventArgs) Handles btnAdd2.Click
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

        strSQL = "SELECT dbo.BillSundryMaster.ID, dbo.BillSundryMaster.SundryName, dbo.BillSundryMaster.DefaultValue, dbo.BillSundryNature.Type," _
        & " dbo.BillSundryMaster.AmountAs, dbo.BillSundryMaster.PercentOf, dbo.BillSundryMaster.RoundOff, dbo.BillSundryMaster.RoundOffType," _
        & " dbo.BillSundryMaster.SundryType FROM dbo.BillSundryMaster INNER JOIN dbo.BillSundryNature ON dbo.BillSundryMaster.NatureID = dbo.BillSundryNature.ID" _
        & " Where (dbo.BillSundryMaster.ID = " & Val(cmbSundry.SelectedValue) & ")"
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
            grdTax.Rows(grdTax.Rows.Count - 1).Cells(9).Value = dr.Item("Type")
        End If
        cmbSundry.Text = ""
        txtValue.Text = ""
        CalculateSundryGrid()
        cmbSundry.Focus()
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If Grid.Rows.Count > 0 Then
            Grid.Rows.RemoveAt(Grid.CurrentCell.RowIndex)
        End If
        GridTotal()
        CalculateSundryGrid()
    End Sub

    Private Sub btnDel2_Click(sender As Object, e As EventArgs) Handles btnDel2.Click
        If grdTax.Rows.Count > 0 Then
            grdTax.Rows.RemoveAt(grdTax.CurrentCell.RowIndex)
        End If
        CalculateSundryGrid()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
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
        Dim cmd As Data.Odbc.OdbcCommand
        Dim dr As Data.Odbc.OdbcDataReader
        '-------------- CHECK STOCK
        If lblID.Text = "" Then
            For i = 0 To Grid.Rows.Count - 1
                '-------------------------------
                cnn.Check_Insert_Product(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value), 0)
                '-------------------------------

                strSQL = "SELECT dbo.ProductStock.Qty AS StockQty FROM dbo.ProductStock WHERE (dbo.ProductStock.ProductID = " & Val(Grid.Rows(i).Cells(0).Value) & ")" _
                & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
                & " AND (dbo.ProductStock.SMID = 0)"

                cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                If cmd.Connection.State = 1 Then cmd.Connection.Close()
                cmd.Connection.Open()
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If Val(Grid.Rows(i).Cells(3).Value) > Val(dr.Item("StockQty")) Then
                        cnn.ErrMsgBox("One or more Product Stock is not available, Please check  !")
                        dr.Close()
                        cmd.Connection.Close()
                        Exit Sub
                    End If
                    dr.Close()
                    cmd.Connection.Close()
                Else
                    cnn.ErrMsgBox("Something went Wrong, Please Contact Software Administrator  !")
                    Exit Sub
                End If
            Next
        Else

            For i = 0 To Grid.Rows.Count - 1
                '-------------------------------
                cnn.Check_Insert_Product(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(i).Cells(0).Value), 0)
                '-------------------------------

                strSQL = "SELECT dbo.PurchaseReturnDetails.Qty AS SaleQty, dbo.ProductStock.Qty AS StockQty, dbo.PurchaseReturnDetails.HeaderID, dbo.PurchaseReturnDetails.ProductID" _
                & " FROM dbo.PurchaseReturnDetails INNER JOIN dbo.ProductStock ON dbo.PurchaseReturnDetails.ProductID = dbo.ProductStock.ProductID" _
                & " WHERE (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ") AND (dbo.PurchaseReturnDetails.ProductID = " & Val(Grid.Rows(i).Cells(0).Value) & ")" _
                & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
                & " AND (dbo.ProductStock.SMID = 0)"

                cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                If cmd.Connection.State = 1 Then cmd.Connection.Close()
                cmd.Connection.Open()
                dr = cmd.ExecuteReader
                If dr.Read Then
                    If ((Val(dr.Item("StockQty")) - Val(Grid.Rows(i).Cells(3).Value)) + Val(dr.Item("SaleQty"))) < 0 Then
                        cnn.ErrMsgBox("One or more Product Stock is not available, Please check  !")
                        dr.Close()
                        cmd.Connection.Close()
                        Exit Sub
                    End If
                    dr.Close()
                    cmd.Connection.Close()
                Else
                    dr.Close()
                    cmd.Connection.Close()
                    strSQL = "SELECT dbo.ProductStock.Qty AS StockQty FROM dbo.ProductStock WHERE (dbo.ProductStock.ProductID = " & Val(Grid.Rows(i).Cells(0).Value) & ")" _
                & " AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ")" _
                & " AND (dbo.ProductStock.SMID = 0)"

                    cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
                    If cmd.Connection.State = 1 Then cmd.Connection.Close()
                    cmd.Connection.Open()
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If Val(Grid.Rows(i).Cells(3).Value) > Val(dr.Item("StockQty")) Then
                            cnn.ErrMsgBox("One or more Product Stock is not available, Please check  !")
                            dr.Close()
                            cmd.Connection.Close()
                            Exit Sub
                        End If
                        dr.Close()
                        cmd.Connection.Close()
                    Else
                        cnn.ErrMsgBox("Something went Wrong, Please Contact Software Administrator  !")
                        Exit Sub
                    End If
                End If
            Next
            '--------------------------------------- STOCK REVERSE
            strSQL = "UPDATE ProductStock SET dbo.ProductStock.Qty = (dbo.ProductStock.Qty + dbo.PurchaseReturnDetails.Qty)" _
            & " FROM dbo.ProductStock INNER JOIN dbo.PurchaseReturnDetails ON dbo.ProductStock.ProductID = dbo.PurchaseReturnDetails.ProductID" _
            & " WHERE (dbo.ProductStock.ProductID = dbo.PurchaseReturnDetails.ProductID) AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ")" _
            & " AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (dbo.PurchaseReturnDetails.HeaderID = " & Val(lblID.Text) & ")" _
            & " AND (dbo.ProductStock.SMID = 0)"

            cnn.executeSQL(strSQL)
            '-------------- CHECK STOCK
        End If


        If lblID.Text = "" Then
            strSQL = "SELECT PurchaseReturnNo from Para Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("PurchaseReturnNo")) Then
                    txtInvoiceNo.Text = dr.Item("PurchaseReturnNo")
                Else
                    cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
                    Exit Sub
                End If
            Else
                cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
                Exit Sub
            End If
            dr.Close()
            cmd.Connection.Close()
            '-------------------------------------------------
            strSQL = "INSERT INTO PurchaseReturnHeader (RefNo, RefDate, AccountID, TotalQty, TotalNos, TotalAmount, BillAmount, Remarks, MasterCompanyID, MasterBranchID, CreatedBy, CreatedDate) OutPut Inserted.ID as HeaderID VALUES (" _
            & cnn.SQLquote(txtInvoiceNo.Text) & ",'" & cnn.GetDate(txtDate.Text) & "'," & Val(cmbSupplier.SelectedValue) & "," & Val(txtTotalQty.Text) & "," & Val(txtTotalNos.Text) & "," & Val(txtTotalAmount.Text) _
            & "," & Val(txtBillAmount.Text) & "," & cnn.SQLquote(txtRemarks.Text) & "," & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(MDI.lblUserLoginID.Text) & ",'" & cnn.GetDate(Date.Today()) & "')"

            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                lblHeaderID.Text = dr.Item("HeaderID")
            End If
            dr.Close()
            cmd.Connection.Close()
            '==================================
            strSQL = "UPDATE Para SET PurchaseReturnNo = (PurchaseReturnNo + 1) Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
            cnn.executeSQL(strSQL)
            '==================================
            Call cnn.GenerateLog("Purchase Return", "Add", 0, Val(txtInvoiceNo.Text) & ", Dt. " & txtDate.Text)
        Else
            strSQL = "UPDATE PurchaseReturnHeader SET RefNo=" & cnn.SQLquote(txtInvoiceNo.Text) & ", RefDate='" & cnn.GetDate(txtDate.Text) & "'" _
            & ", AccountID=" & Val(cmbSupplier.SelectedValue) & ", TotalQty=" & Val(txtTotalQty.Text) & ", TotalNos=" & Val(txtTotalNos.Text) & ", TotalAmount=" & Val(txtTotalAmount.Text) _
            & ", BillAmount=" & Val(txtBillAmount.Text) & ", Remarks=" & cnn.SQLquote(txtRemarks.Text) _
            & ", EditedBy = " & Val(MDI.lblUserLoginID.Text) & ", EditedDate='" & cnn.GetDate(Date.Today()) & "' Where ID=" & Val(lblID.Text)
            cnn.executeSQL(strSQL)
            lblHeaderID.Text = lblID.Text
            Call cnn.GenerateLog("Purchase Return", "Edit", lblID.Text, Val(txtInvoiceNo.Text) & ", Dt. " & txtDate.Text)
        End If

        SaveDetails()

        '-------------------------Update Stock
        For i = 0 To Grid.Rows.Count - 1
            '=====================================
            'Dim strInsert As String
            'strInsert = "IF NOT EXISTS (SELECT dbo.BranchMaster.ID, dbo.BranchMaster.CompanyID FROM dbo.ProductStock INNER JOIN dbo.BranchMaster ON dbo.ProductStock.MasterCompanyID = dbo.BranchMaster.CompanyID AND dbo.ProductStock.MasterBranchID = dbo.BranchMaster.ID" _
            '& " WHERE (dbo.ProductStock.ProductID = " & Val(lblHeaderID.Text) & ") AND (dbo.ProductStock.MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) & ") AND (dbo.ProductStock.MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) & "))" _
            '& " BEGIN" _
            '& " INSERT INTO ProductStock (MasterCompanyID, MasterBranchID, ProductID, Qty) VALUES (" & Val(MDI.lblMasterCompanyID.Text) & "," & Val(MDI.lblMasterBranchID.Text) & "," & Val(Grid.Rows(i).Cells(0).Value) & ", 0)" _
            '& " End"
            'cnn.OpenConn()
            'Dim cmdSP As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strInsert, cnn.cnn)
            'If cmdSP.Connection.State = 1 Then cmdSP.Connection.Close()
            'cmdSP.Connection.Open()
            'cmdSP.ExecuteNonQuery()
            'cmdSP.Connection.Close()
            '=====================================
            strSQL = "UPDATE ProductStock SET Qty = (Qty - " & Val(Grid.Rows(i).Cells(3).Value) & ")" _
            & " Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text) _
            & " AND ProductID=" & Val(Grid.Rows(i).Cells(0).Value) _
            & " AND SMID = 0"
            cnn.executeSQL(strSQL)
        Next
        '-------------------------Update Stock

        'cnn.Tran.Commit()
        cnn.CloseConn()
        If lblID.Text <> "" Then
            cnn.InfoMsgBox("Record has been Updated Successfully  !")
            Me.Close()
        Else
            cnn.InfoMsgBox("Record has been Added Successfully  !")
            lblID.Text = "" : txtInvoiceNo.Text = "" : txtDate.Text = Date.Today : cmbSupplier.Text = "" : txtCity.Text = ""
            cmbProduct.Text = "" : txtCode.Text = "" : txtQty.Text = "" : txtRate.Text = "" : txtAmount.Text = "" : Grid.Rows.Clear()
            txtTotalQty.Text = "" : txtTotalNos.Text = "" : txtTotalAmount.Text = "" : txtRemarks.Text = "" : cmbSundry.Text = ""
            txtValue.Text = "" : grdTax.Rows.Clear() : txtBillAmount.Text = "" : txtInvoiceNo.Focus()
            strSQL = "SELECT PurchaseReturnNo from Para Where MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("PurchaseReturnNo")) Then
                    txtInvoiceNo.Text = dr.Item("PurchaseReturnNo")
                Else
                    cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
                    Exit Sub
                End If
            Else
                cnn.ErrMsgBox("There is a Problem in Entry, Please Contact Software Administrator  !")
                Exit Sub
            End If
            dr.Close()
            cmd.Connection.Close()
            '-------------------------------------------------
            txtInvoiceNo.Focus()
        End If
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        If cmbSupplier.Text <> "" Then
            Dim strSQL As String
            strSQL = "Select City from tblAccount Where tType='SUPPLIER' AND ID=" & Val(cmbSupplier.SelectedValue)
            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand(strSQL, cnn.cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim dr As Data.Odbc.OdbcDataReader = cmd.ExecuteReader
            If dr.Read Then
                txtCity.Text = dr.Item("City")
            End If
            dr.Close()
            cmd.Connection.Close()
        Else
            txtCity.Text = ""
        End If
    End Sub

    Private Sub Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellClick
         If Grid.Rows.Count > 0 Then
            If Grid.CurrentCell.RowIndex > -1 Then
                Dim crRowIndex As Integer = Grid.CurrentCell.RowIndex
                lblStock.Text = cnn.GetStock(Val(MDI.lblMasterCompanyID.Text), Val(MDI.lblMasterBranchID.Text), Val(Grid.Rows(crRowIndex).Cells(0).Value.ToString), 0)
                lblStock.Visible = True : lblText.Visible = True
            End If
        End If
    End Sub

    Private Sub Grid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid.CellContentClick

    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        GetAmount()
    End Sub

    Private Sub txtRate_TextChanged(sender As Object, e As EventArgs) Handles txtRate.TextChanged
        GetAmount()
    End Sub

    Private Sub cmbProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProduct.SelectedIndexChanged
        If cmbProduct.Text <> "" Then
            GetDetails()
        Else
            txtCode.Text = "" : txtRate.Text = "" : txtAmount.Text = ""
            lblStock.Visible = False : lblText.Visible = False
        End If
    End Sub

    Private Sub cmbSundry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSundry.SelectedIndexChanged
        txtValue.Text = ""
        If cmbSundry.Text <> "" Then
            GetSundryDetails()
        Else
            txtValue.Text = ""
        End If
    End Sub

    Private Sub cmbProduct_TextChanged(sender As Object, e As EventArgs) Handles cmbProduct.TextChanged
        If cmbProduct.Text <> "" Then
            GetDetails()
        Else
            txtCode.Text = "" : txtRate.Text = "" : txtAmount.Text = ""
            lblStock.Visible = False : lblText.Visible = False
        End If
    End Sub

    Private Sub cmbSundry_TextChanged(sender As Object, e As EventArgs) Handles cmbSundry.TextChanged
        txtValue.Text = ""
        If cmbSundry.Text <> "" Then
            GetSundryDetails()
        Else
            txtValue.Text = ""
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtInvoiceNo.Text = "" Then
            cnn.ErrMsgBox("Please Enter Ref No  !")
            txtInvoiceNo.Focus()
            Exit Sub
        End If
        If txtDate.Text = "" Then
            cnn.ErrMsgBox("Please Enter Ref Date  !")
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
            cnn.ErrMsgBox("Please Select Product Code  !")
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
        If Val(txtRate.Text) = 0 Then
            cnn.ErrMsgBox("Please Enter Product Rate  !")
            txtRate.Focus()
            Exit Sub
        End If
        If Val(txtAmount.Text) = 0 Then
            cnn.ErrMsgBox("Please Enter Product Amount  !")
            txtAmount.Focus()
            Exit Sub
        End If
        '---------
        For i = 0 To Grid.Rows.Count - 1
            If cmbProduct.SelectedValue = Grid.Rows(i).Cells(0).Value Then
                cnn.ErrMsgBox("Duplicate Entry, Please check  !")
                cmbProduct.Focus()
                Exit Sub
            End If
        Next
        '-----------------------------------------------
        Grid.Rows.Add(1)
        Grid.Rows(Grid.Rows.Count - 1).Cells(0).Value = cmbProduct.SelectedValue
        Grid.Rows(Grid.Rows.Count - 1).Cells(1).Value = cmbProduct.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(2).Value = txtCode.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(3).Value = txtQty.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(4).Value = txtNos.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(5).Value = txtRate.Text
        Grid.Rows(Grid.Rows.Count - 1).Cells(6).Value = txtAmount.Text
        GridTotal()
        CalculateSundryGrid()
        cmbProduct.Text = ""
        txtQty.Text = "" : txtNos.Text = "" : txtRate.Text = "" : txtAmount.Text = "" : lblStock.Visible = False : lblText.Visible = False
        cmbProduct.Focus()
    End Sub

    Private Sub btnCancel_Click_1(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class