<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDI))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PrintForm1 = New System.Drawing.Printing.PrintDocument()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.mnuMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExpType = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProducts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSupplier = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBuyer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBank = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSM = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaction = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPurchase = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPurReturn = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSale = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaleReturn = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuIssueSM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPayment = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReciept = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEmpAtt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEmpSal = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPurList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPurRetList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSaleList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaleRetList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuIssueSMList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPaymentList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRecieptList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEmpAttList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEmpSalList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuComStatement = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUserRights = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBillSundry = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuStock = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBuyerStatement = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSupplierStatement = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCompany = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBranch = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuQuit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.lblAccessType = New System.Windows.Forms.Label()
        Me.lblMasterCompanyID = New System.Windows.Forms.Label()
        Me.lblMasterBranchID = New System.Windows.Forms.Label()
        Me.lblUserLoginID = New System.Windows.Forms.Label()
        Me.lblMasterBranchName = New System.Windows.Forms.Label()
        Me.lblMasterCompanyName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCategory = New System.Windows.Forms.Button()
        Me.btnSale = New System.Windows.Forms.Button()
        Me.btnPurchase = New System.Windows.Forms.Button()
        Me.btnBuyer = New System.Windows.Forms.Button()
        Me.btnSupplier = New System.Windows.Forms.Button()
        Me.btnProducts = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnReceipt = New System.Windows.Forms.Button()
        Me.btnPayment = New System.Windows.Forms.Button()
        Me.btnExpHead = New System.Windows.Forms.Button()
        Me.btnGST = New System.Windows.Forms.Button()
        Me.btnEmpSalary = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StatusStrip.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "add.png")
        Me.ImageList1.Images.SetKeyName(1, "edit.png")
        Me.ImageList1.Images.SetKeyName(2, "delete.png")
        Me.ImageList1.Images.SetKeyName(3, "search.png")
        Me.ImageList1.Images.SetKeyName(4, "print.png")
        Me.ImageList1.Images.SetKeyName(5, "Logout.png")
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatusLabel.Text = "Status"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 727)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1095, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'mnuMaster
        '
        Me.mnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExpType, Me.mnuCategory, Me.mnuProducts, Me.mnuSupplier, Me.mnuBuyer, Me.mnuBank, Me.ToolStripSeparator3, Me.mnuSM})
        Me.mnuMaster.Name = "mnuMaster"
        Me.mnuMaster.Size = New System.Drawing.Size(55, 20)
        Me.mnuMaster.Text = "Master"
        '
        'mnuExpType
        '
        Me.mnuExpType.Name = "mnuExpType"
        Me.mnuExpType.Size = New System.Drawing.Size(152, 22)
        Me.mnuExpType.Text = "Expense Type"
        '
        'mnuCategory
        '
        Me.mnuCategory.Name = "mnuCategory"
        Me.mnuCategory.Size = New System.Drawing.Size(152, 22)
        Me.mnuCategory.Text = "Product Group"
        '
        'mnuProducts
        '
        Me.mnuProducts.Name = "mnuProducts"
        Me.mnuProducts.Size = New System.Drawing.Size(152, 22)
        Me.mnuProducts.Text = "Products"
        '
        'mnuSupplier
        '
        Me.mnuSupplier.Name = "mnuSupplier"
        Me.mnuSupplier.Size = New System.Drawing.Size(152, 22)
        Me.mnuSupplier.Text = "Suppliers"
        '
        'mnuBuyer
        '
        Me.mnuBuyer.Name = "mnuBuyer"
        Me.mnuBuyer.Size = New System.Drawing.Size(152, 22)
        Me.mnuBuyer.Text = "Customer"
        '
        'mnuBank
        '
        Me.mnuBank.Name = "mnuBank"
        Me.mnuBank.Size = New System.Drawing.Size(152, 22)
        Me.mnuBank.Text = "Bank"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(149, 6)
        '
        'mnuSM
        '
        Me.mnuSM.Name = "mnuSM"
        Me.mnuSM.Size = New System.Drawing.Size(152, 22)
        Me.mnuSM.Text = "Employee"
        Me.mnuSM.Visible = False
        '
        'mnuTransaction
        '
        Me.mnuTransaction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPurchase, Me.mnuPurReturn, Me.ToolStripSeparator1, Me.mnuSale, Me.mnuSaleReturn, Me.ToolStripSeparator6, Me.mnuIssueSM, Me.ToolStripSeparator2, Me.mnuPayment, Me.mnuReciept, Me.ToolStripSeparator8, Me.mnuEmpAtt, Me.mnuEmpSal})
        Me.mnuTransaction.Name = "mnuTransaction"
        Me.mnuTransaction.Size = New System.Drawing.Size(81, 20)
        Me.mnuTransaction.Text = "Transaction"
        '
        'mnuPurchase
        '
        Me.mnuPurchase.Name = "mnuPurchase"
        Me.mnuPurchase.Size = New System.Drawing.Size(190, 22)
        Me.mnuPurchase.Text = "Purchase"
        '
        'mnuPurReturn
        '
        Me.mnuPurReturn.Enabled = False
        Me.mnuPurReturn.Name = "mnuPurReturn"
        Me.mnuPurReturn.Size = New System.Drawing.Size(190, 22)
        Me.mnuPurReturn.Text = "Purchase Return"
        Me.mnuPurReturn.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(187, 6)
        '
        'mnuSale
        '
        Me.mnuSale.Name = "mnuSale"
        Me.mnuSale.Size = New System.Drawing.Size(190, 22)
        Me.mnuSale.Text = "Sale"
        '
        'mnuSaleReturn
        '
        Me.mnuSaleReturn.Enabled = False
        Me.mnuSaleReturn.Name = "mnuSaleReturn"
        Me.mnuSaleReturn.Size = New System.Drawing.Size(190, 22)
        Me.mnuSaleReturn.Text = "Sale Return"
        Me.mnuSaleReturn.Visible = False
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(187, 6)
        '
        'mnuIssueSM
        '
        Me.mnuIssueSM.Name = "mnuIssueSM"
        Me.mnuIssueSM.Size = New System.Drawing.Size(190, 22)
        Me.mnuIssueSM.Text = "Expense"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(187, 6)
        '
        'mnuPayment
        '
        Me.mnuPayment.Name = "mnuPayment"
        Me.mnuPayment.Size = New System.Drawing.Size(190, 22)
        Me.mnuPayment.Text = "Payment"
        '
        'mnuReciept
        '
        Me.mnuReciept.Name = "mnuReciept"
        Me.mnuReciept.Size = New System.Drawing.Size(190, 22)
        Me.mnuReciept.Text = "Receipt"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(187, 6)
        Me.ToolStripSeparator8.Visible = False
        '
        'mnuEmpAtt
        '
        Me.mnuEmpAtt.Name = "mnuEmpAtt"
        Me.mnuEmpAtt.Size = New System.Drawing.Size(190, 22)
        Me.mnuEmpAtt.Text = "Employee Attendance"
        Me.mnuEmpAtt.Visible = False
        '
        'mnuEmpSal
        '
        Me.mnuEmpSal.Name = "mnuEmpSal"
        Me.mnuEmpSal.Size = New System.Drawing.Size(190, 22)
        Me.mnuEmpSal.Text = "Employee Salary"
        Me.mnuEmpSal.Visible = False
        '
        'mnuReport
        '
        Me.mnuReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPurList, Me.mnuPurRetList, Me.ToolStripSeparator4, Me.mnuSaleList, Me.mnuSaleRetList, Me.ToolStripSeparator9, Me.mnuIssueSMList, Me.ToolStripSeparator7, Me.mnuPaymentList, Me.mnuRecieptList, Me.ToolStripSeparator5, Me.mnuEmpAttList, Me.mnuEmpSalList, Me.ToolStripSeparator10, Me.mnuComStatement})
        Me.mnuReport.Name = "mnuReport"
        Me.mnuReport.Size = New System.Drawing.Size(54, 20)
        Me.mnuReport.Text = "Listing"
        '
        'mnuPurList
        '
        Me.mnuPurList.Name = "mnuPurList"
        Me.mnuPurList.Size = New System.Drawing.Size(190, 22)
        Me.mnuPurList.Text = "Purchase"
        '
        'mnuPurRetList
        '
        Me.mnuPurRetList.Enabled = False
        Me.mnuPurRetList.Name = "mnuPurRetList"
        Me.mnuPurRetList.Size = New System.Drawing.Size(190, 22)
        Me.mnuPurRetList.Text = "Purchase Return"
        Me.mnuPurRetList.Visible = False
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(187, 6)
        '
        'mnuSaleList
        '
        Me.mnuSaleList.Name = "mnuSaleList"
        Me.mnuSaleList.Size = New System.Drawing.Size(190, 22)
        Me.mnuSaleList.Text = "Sale"
        '
        'mnuSaleRetList
        '
        Me.mnuSaleRetList.Enabled = False
        Me.mnuSaleRetList.Name = "mnuSaleRetList"
        Me.mnuSaleRetList.Size = New System.Drawing.Size(190, 22)
        Me.mnuSaleRetList.Text = "Sale Return"
        Me.mnuSaleRetList.Visible = False
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(187, 6)
        '
        'mnuIssueSMList
        '
        Me.mnuIssueSMList.Name = "mnuIssueSMList"
        Me.mnuIssueSMList.Size = New System.Drawing.Size(190, 22)
        Me.mnuIssueSMList.Text = "Expense"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(187, 6)
        '
        'mnuPaymentList
        '
        Me.mnuPaymentList.Name = "mnuPaymentList"
        Me.mnuPaymentList.Size = New System.Drawing.Size(190, 22)
        Me.mnuPaymentList.Text = "Payment"
        '
        'mnuRecieptList
        '
        Me.mnuRecieptList.Name = "mnuRecieptList"
        Me.mnuRecieptList.Size = New System.Drawing.Size(190, 22)
        Me.mnuRecieptList.Text = "Reciept"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(187, 6)
        Me.ToolStripSeparator5.Visible = False
        '
        'mnuEmpAttList
        '
        Me.mnuEmpAttList.Name = "mnuEmpAttList"
        Me.mnuEmpAttList.Size = New System.Drawing.Size(190, 22)
        Me.mnuEmpAttList.Text = "Employee Attendance"
        Me.mnuEmpAttList.Visible = False
        '
        'mnuEmpSalList
        '
        Me.mnuEmpSalList.Name = "mnuEmpSalList"
        Me.mnuEmpSalList.Size = New System.Drawing.Size(190, 22)
        Me.mnuEmpSalList.Text = "Employee Salary"
        Me.mnuEmpSalList.Visible = False
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(187, 6)
        Me.ToolStripSeparator10.Visible = False
        '
        'mnuComStatement
        '
        Me.mnuComStatement.Name = "mnuComStatement"
        Me.mnuComStatement.Size = New System.Drawing.Size(190, 22)
        Me.mnuComStatement.Text = "Company Statement"
        Me.mnuComStatement.Visible = False
        '
        'mnuAdmin
        '
        Me.mnuAdmin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUser, Me.mnuUserRights, Me.mnuBillSundry, Me.ToolStripMenuItem1, Me.mnuStock, Me.mnuBuyerStatement, Me.mnuSupplierStatement, Me.ToolStripSeparator11, Me.mnuCompany, Me.mnuBranch})
        Me.mnuAdmin.Name = "mnuAdmin"
        Me.mnuAdmin.Size = New System.Drawing.Size(98, 20)
        Me.mnuAdmin.Text = "Administration"
        '
        'mnuUser
        '
        Me.mnuUser.Name = "mnuUser"
        Me.mnuUser.Size = New System.Drawing.Size(174, 22)
        Me.mnuUser.Text = "Users"
        '
        'mnuUserRights
        '
        Me.mnuUserRights.Name = "mnuUserRights"
        Me.mnuUserRights.Size = New System.Drawing.Size(174, 22)
        Me.mnuUserRights.Text = "User Rights"
        '
        'mnuBillSundry
        '
        Me.mnuBillSundry.Name = "mnuBillSundry"
        Me.mnuBillSundry.Size = New System.Drawing.Size(174, 22)
        Me.mnuBillSundry.Text = "Bill Sundry"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(171, 6)
        '
        'mnuStock
        '
        Me.mnuStock.Name = "mnuStock"
        Me.mnuStock.Size = New System.Drawing.Size(174, 22)
        Me.mnuStock.Text = "Product Stock"
        '
        'mnuBuyerStatement
        '
        Me.mnuBuyerStatement.Name = "mnuBuyerStatement"
        Me.mnuBuyerStatement.Size = New System.Drawing.Size(174, 22)
        Me.mnuBuyerStatement.Text = "Buyer Statement"
        '
        'mnuSupplierStatement
        '
        Me.mnuSupplierStatement.Name = "mnuSupplierStatement"
        Me.mnuSupplierStatement.Size = New System.Drawing.Size(174, 22)
        Me.mnuSupplierStatement.Text = "Supplier Statement"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(171, 6)
        '
        'mnuCompany
        '
        Me.mnuCompany.Name = "mnuCompany"
        Me.mnuCompany.Size = New System.Drawing.Size(174, 22)
        Me.mnuCompany.Text = "Parameter"
        Me.mnuCompany.Visible = False
        '
        'mnuBranch
        '
        Me.mnuBranch.Name = "mnuBranch"
        Me.mnuBranch.Size = New System.Drawing.Size(174, 22)
        Me.mnuBranch.Text = "Company"
        '
        'mnuQuit
        '
        Me.mnuQuit.Name = "mnuQuit"
        Me.mnuQuit.Size = New System.Drawing.Size(42, 20)
        Me.mnuQuit.Text = "Quit"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMaster, Me.mnuTransaction, Me.mnuReport, Me.mnuAdmin, Me.mnuQuit})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(920, 24)
        Me.MenuStrip1.TabIndex = 18
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'lblAccessType
        '
        Me.lblAccessType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAccessType.Location = New System.Drawing.Point(10, 784)
        Me.lblAccessType.Name = "lblAccessType"
        Me.lblAccessType.Size = New System.Drawing.Size(29, 19)
        Me.lblAccessType.TabIndex = 17
        Me.lblAccessType.Visible = False
        '
        'lblMasterCompanyID
        '
        Me.lblMasterCompanyID.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMasterCompanyID.Location = New System.Drawing.Point(45, 784)
        Me.lblMasterCompanyID.Name = "lblMasterCompanyID"
        Me.lblMasterCompanyID.Size = New System.Drawing.Size(29, 19)
        Me.lblMasterCompanyID.TabIndex = 18
        Me.lblMasterCompanyID.Visible = False
        '
        'lblMasterBranchID
        '
        Me.lblMasterBranchID.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMasterBranchID.Location = New System.Drawing.Point(80, 784)
        Me.lblMasterBranchID.Name = "lblMasterBranchID"
        Me.lblMasterBranchID.Size = New System.Drawing.Size(29, 19)
        Me.lblMasterBranchID.TabIndex = 19
        Me.lblMasterBranchID.Visible = False
        '
        'lblUserLoginID
        '
        Me.lblUserLoginID.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUserLoginID.Location = New System.Drawing.Point(11, 814)
        Me.lblUserLoginID.Name = "lblUserLoginID"
        Me.lblUserLoginID.Size = New System.Drawing.Size(29, 19)
        Me.lblUserLoginID.TabIndex = 20
        Me.lblUserLoginID.Visible = False
        '
        'lblMasterBranchName
        '
        Me.lblMasterBranchName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMasterBranchName.Location = New System.Drawing.Point(46, 814)
        Me.lblMasterBranchName.Name = "lblMasterBranchName"
        Me.lblMasterBranchName.Size = New System.Drawing.Size(29, 19)
        Me.lblMasterBranchName.TabIndex = 21
        Me.lblMasterBranchName.Visible = False
        '
        'lblMasterCompanyName
        '
        Me.lblMasterCompanyName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMasterCompanyName.Location = New System.Drawing.Point(81, 814)
        Me.lblMasterCompanyName.Name = "lblMasterCompanyName"
        Me.lblMasterCompanyName.Size = New System.Drawing.Size(29, 19)
        Me.lblMasterCompanyName.TabIndex = 22
        Me.lblMasterCompanyName.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.SlateGray
        Me.Label2.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(34, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 23)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "MASTER"
        '
        'btnCategory
        '
        Me.btnCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCategory.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCategory.ForeColor = System.Drawing.Color.White
        Me.btnCategory.Location = New System.Drawing.Point(0, 30)
        Me.btnCategory.Name = "btnCategory"
        Me.btnCategory.Size = New System.Drawing.Size(175, 37)
        Me.btnCategory.TabIndex = 25
        Me.btnCategory.Text = "Product Group"
        Me.btnCategory.UseVisualStyleBackColor = False
        '
        'btnSale
        '
        Me.btnSale.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnSale.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSale.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSale.ForeColor = System.Drawing.Color.White
        Me.btnSale.Location = New System.Drawing.Point(0, 300)
        Me.btnSale.Name = "btnSale"
        Me.btnSale.Size = New System.Drawing.Size(175, 37)
        Me.btnSale.TabIndex = 26
        Me.btnSale.Text = "Sale"
        Me.btnSale.UseVisualStyleBackColor = False
        '
        'btnPurchase
        '
        Me.btnPurchase.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnPurchase.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPurchase.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPurchase.ForeColor = System.Drawing.Color.White
        Me.btnPurchase.Location = New System.Drawing.Point(0, 260)
        Me.btnPurchase.Name = "btnPurchase"
        Me.btnPurchase.Size = New System.Drawing.Size(175, 37)
        Me.btnPurchase.TabIndex = 27
        Me.btnPurchase.Text = "Purchase"
        Me.btnPurchase.UseVisualStyleBackColor = False
        '
        'btnBuyer
        '
        Me.btnBuyer.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnBuyer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBuyer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuyer.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuyer.ForeColor = System.Drawing.Color.White
        Me.btnBuyer.Location = New System.Drawing.Point(0, 150)
        Me.btnBuyer.Name = "btnBuyer"
        Me.btnBuyer.Size = New System.Drawing.Size(175, 37)
        Me.btnBuyer.TabIndex = 28
        Me.btnBuyer.Text = "Customers"
        Me.btnBuyer.UseVisualStyleBackColor = False
        '
        'btnSupplier
        '
        Me.btnSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnSupplier.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSupplier.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSupplier.ForeColor = System.Drawing.Color.White
        Me.btnSupplier.Location = New System.Drawing.Point(0, 110)
        Me.btnSupplier.Name = "btnSupplier"
        Me.btnSupplier.Size = New System.Drawing.Size(175, 37)
        Me.btnSupplier.TabIndex = 29
        Me.btnSupplier.Text = "Suppliers"
        Me.btnSupplier.UseVisualStyleBackColor = False
        '
        'btnProducts
        '
        Me.btnProducts.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnProducts.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProducts.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProducts.ForeColor = System.Drawing.Color.White
        Me.btnProducts.Location = New System.Drawing.Point(0, 70)
        Me.btnProducts.Name = "btnProducts"
        Me.btnProducts.Size = New System.Drawing.Size(175, 37)
        Me.btnProducts.TabIndex = 30
        Me.btnProducts.Text = "Products"
        Me.btnProducts.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.SlateGray
        Me.Label3.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(4, 232)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(167, 23)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "TRANSACTION"
        '
        'btnReceipt
        '
        Me.btnReceipt.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnReceipt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReceipt.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReceipt.ForeColor = System.Drawing.Color.White
        Me.btnReceipt.Location = New System.Drawing.Point(0, 380)
        Me.btnReceipt.Name = "btnReceipt"
        Me.btnReceipt.Size = New System.Drawing.Size(175, 37)
        Me.btnReceipt.TabIndex = 34
        Me.btnReceipt.Text = "Receipt"
        Me.btnReceipt.UseVisualStyleBackColor = False
        '
        'btnPayment
        '
        Me.btnPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnPayment.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPayment.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayment.ForeColor = System.Drawing.Color.White
        Me.btnPayment.Location = New System.Drawing.Point(0, 340)
        Me.btnPayment.Name = "btnPayment"
        Me.btnPayment.Size = New System.Drawing.Size(175, 37)
        Me.btnPayment.TabIndex = 35
        Me.btnPayment.Text = "Payment"
        Me.btnPayment.UseVisualStyleBackColor = False
        '
        'btnExpHead
        '
        Me.btnExpHead.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnExpHead.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExpHead.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExpHead.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpHead.ForeColor = System.Drawing.Color.White
        Me.btnExpHead.Location = New System.Drawing.Point(0, 190)
        Me.btnExpHead.Name = "btnExpHead"
        Me.btnExpHead.Size = New System.Drawing.Size(175, 37)
        Me.btnExpHead.TabIndex = 36
        Me.btnExpHead.Text = "Expense Type"
        Me.btnExpHead.UseVisualStyleBackColor = False
        '
        'btnGST
        '
        Me.btnGST.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnGST.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGST.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGST.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGST.ForeColor = System.Drawing.Color.White
        Me.btnGST.Location = New System.Drawing.Point(0, 420)
        Me.btnGST.Name = "btnGST"
        Me.btnGST.Size = New System.Drawing.Size(175, 37)
        Me.btnGST.TabIndex = 37
        Me.btnGST.Text = "GST Report"
        Me.btnGST.UseVisualStyleBackColor = False
        '
        'btnEmpSalary
        '
        Me.btnEmpSalary.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnEmpSalary.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEmpSalary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEmpSalary.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEmpSalary.ForeColor = System.Drawing.Color.White
        Me.btnEmpSalary.Location = New System.Drawing.Point(0, 460)
        Me.btnEmpSalary.Name = "btnEmpSalary"
        Me.btnEmpSalary.Size = New System.Drawing.Size(175, 37)
        Me.btnEmpSalary.TabIndex = 38
        Me.btnEmpSalary.Text = "--"
        Me.btnEmpSalary.UseVisualStyleBackColor = False
        Me.btnEmpSalary.Visible = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Orange
        Me.Label1.Location = New System.Drawing.Point(10, 606)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 47)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "9026419991 9580458575"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Orange
        Me.Label4.Location = New System.Drawing.Point(22, 528)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 22)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "For Any Support"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel
        '
        Me.Panel.BackColor = System.Drawing.Color.SlateGray
        Me.Panel.Controls.Add(Me.PictureBox1)
        Me.Panel.Controls.Add(Me.Label4)
        Me.Panel.Controls.Add(Me.Label1)
        Me.Panel.Controls.Add(Me.btnEmpSalary)
        Me.Panel.Controls.Add(Me.btnGST)
        Me.Panel.Controls.Add(Me.btnExpHead)
        Me.Panel.Controls.Add(Me.btnPayment)
        Me.Panel.Controls.Add(Me.btnReceipt)
        Me.Panel.Controls.Add(Me.Label3)
        Me.Panel.Controls.Add(Me.btnProducts)
        Me.Panel.Controls.Add(Me.btnSupplier)
        Me.Panel.Controls.Add(Me.btnBuyer)
        Me.Panel.Controls.Add(Me.btnPurchase)
        Me.Panel.Controls.Add(Me.btnSale)
        Me.Panel.Controls.Add(Me.btnCategory)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.lblMasterCompanyName)
        Me.Panel.Controls.Add(Me.lblMasterBranchName)
        Me.Panel.Controls.Add(Me.lblUserLoginID)
        Me.Panel.Controls.Add(Me.lblMasterBranchID)
        Me.Panel.Controls.Add(Me.lblMasterCompanyID)
        Me.Panel.Controls.Add(Me.lblAccessType)
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel.Location = New System.Drawing.Point(920, 0)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(175, 727)
        Me.Panel.TabIndex = 16
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.eezeeBilling.My.Resources.Resources.logo
        Me.PictureBox1.Location = New System.Drawing.Point(14, 553)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(149, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 42
        Me.PictureBox1.TabStop = False
        '
        'MDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.eezeeBilling.My.Resources.Resources.bg
        Me.ClientSize = New System.Drawing.Size(1095, 749)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.StatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "MDI"
        Me.Text = "MDI"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PrintForm1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents mnuMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExpType As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProducts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSupplier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBuyer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBank As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPurchase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPurReturn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuIssueSM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSale As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaleReturn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPayment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReciept As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEmpAtt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEmpSal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPurList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPurRetList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuIssueSMList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSaleList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaleRetList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPaymentList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRecieptList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEmpAttList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEmpSalList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuComStatement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAdmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUserRights As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBillSundry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCompany As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBranch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuQuit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents lblAccessType As System.Windows.Forms.Label
    Friend WithEvents lblMasterCompanyID As System.Windows.Forms.Label
    Friend WithEvents lblMasterBranchID As System.Windows.Forms.Label
    Friend WithEvents lblUserLoginID As System.Windows.Forms.Label
    Friend WithEvents lblMasterBranchName As System.Windows.Forms.Label
    Friend WithEvents lblMasterCompanyName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnCategory As System.Windows.Forms.Button
    Friend WithEvents btnSale As System.Windows.Forms.Button
    Friend WithEvents btnPurchase As System.Windows.Forms.Button
    Friend WithEvents btnBuyer As System.Windows.Forms.Button
    Friend WithEvents btnSupplier As System.Windows.Forms.Button
    Friend WithEvents btnProducts As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnReceipt As System.Windows.Forms.Button
    Friend WithEvents btnPayment As System.Windows.Forms.Button
    Friend WithEvents btnExpHead As System.Windows.Forms.Button
    Friend WithEvents btnGST As System.Windows.Forms.Button
    Friend WithEvents btnEmpSalary As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents mnuStock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBuyerStatement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSupplierStatement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator

End Class
