<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompanyStatement
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompanyStatement))
        Me.StripPanel = New System.Windows.Forms.Panel()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOpAmt = New System.Windows.Forms.TextBox()
        Me.grdSale = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdPurchase = New System.Windows.Forms.DataGridView()
        Me.txtRecAmt = New System.Windows.Forms.TextBox()
        Me.txtExpAmt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSaleAmt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFromDate = New System.Windows.Forms.DateTimePicker()
        Me.txtClosingAmt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPurchaseAmt = New System.Windows.Forms.TextBox()
        Me.txtPayAmt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtMiscExpAmt = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSalaryAmt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtParty = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.txtOpDues = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTotal1 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotal2 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTotal3 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtClosingDue = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.StripPanel.SuspendLayout()
        CType(Me.grdSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StripPanel
        '
        Me.StripPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.StripPanel.Controls.Add(Me.lblCaption)
        Me.StripPanel.Location = New System.Drawing.Point(0, 0)
        Me.StripPanel.Name = "StripPanel"
        Me.StripPanel.Size = New System.Drawing.Size(630, 43)
        Me.StripPanel.TabIndex = 183
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.Font = New System.Drawing.Font("Trebuchet MS", 14.75!, System.Drawing.FontStyle.Bold)
        Me.lblCaption.ForeColor = System.Drawing.Color.White
        Me.lblCaption.Location = New System.Drawing.Point(12, 7)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(202, 26)
        Me.lblCaption.TabIndex = 0
        Me.lblCaption.Text = "Company Statement"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(366, 48)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 33)
        Me.btnSave.TabIndex = 190
        Me.btnSave.Text = "&Search"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(323, 606)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 33)
        Me.btnCancel.TabIndex = 191
        Me.btnCancel.Text = "E&xit"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 15)
        Me.Label1.TabIndex = 192
        Me.Label1.Text = "Opening Cash Balance"
        '
        'txtOpAmt
        '
        Me.txtOpAmt.BackColor = System.Drawing.Color.Red
        Me.txtOpAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtOpAmt.ForeColor = System.Drawing.Color.White
        Me.txtOpAmt.Location = New System.Drawing.Point(409, 129)
        Me.txtOpAmt.Name = "txtOpAmt"
        Me.txtOpAmt.ReadOnly = True
        Me.txtOpAmt.Size = New System.Drawing.Size(131, 30)
        Me.txtOpAmt.TabIndex = 193
        Me.txtOpAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grdSale
        '
        Me.grdSale.AllowUserToAddRows = False
        Me.grdSale.AllowUserToDeleteRows = False
        Me.grdSale.AllowUserToResizeRows = False
        Me.grdSale.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdSale.BackgroundColor = System.Drawing.Color.SlateGray
        Me.grdSale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSale.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSale.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdSale.Location = New System.Drawing.Point(72, 219)
        Me.grdSale.Name = "grdSale"
        Me.grdSale.ReadOnly = True
        Me.grdSale.RowHeadersVisible = False
        Me.grdSale.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.grdSale.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSale.Size = New System.Drawing.Size(332, 102)
        Me.grdSale.TabIndex = 194
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 227)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 15)
        Me.Label2.TabIndex = 195
        Me.Label2.Text = "Sale"
        '
        'grdPurchase
        '
        Me.grdPurchase.AllowUserToAddRows = False
        Me.grdPurchase.AllowUserToDeleteRows = False
        Me.grdPurchase.AllowUserToResizeRows = False
        Me.grdPurchase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.grdPurchase.BackgroundColor = System.Drawing.Color.SlateGray
        Me.grdPurchase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPurchase.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPurchase.DefaultCellStyle = DataGridViewCellStyle4
        Me.grdPurchase.Location = New System.Drawing.Point(72, 327)
        Me.grdPurchase.Name = "grdPurchase"
        Me.grdPurchase.ReadOnly = True
        Me.grdPurchase.RowHeadersVisible = False
        Me.grdPurchase.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.grdPurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPurchase.Size = New System.Drawing.Size(332, 102)
        Me.grdPurchase.TabIndex = 196
        '
        'txtRecAmt
        '
        Me.txtRecAmt.BackColor = System.Drawing.Color.Red
        Me.txtRecAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtRecAmt.ForeColor = System.Drawing.Color.White
        Me.txtRecAmt.Location = New System.Drawing.Point(409, 316)
        Me.txtRecAmt.Name = "txtRecAmt"
        Me.txtRecAmt.ReadOnly = True
        Me.txtRecAmt.Size = New System.Drawing.Size(131, 30)
        Me.txtRecAmt.TabIndex = 197
        Me.txtRecAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExpAmt
        '
        Me.txtExpAmt.BackColor = System.Drawing.Color.Red
        Me.txtExpAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtExpAmt.ForeColor = System.Drawing.Color.White
        Me.txtExpAmt.Location = New System.Drawing.Point(409, 496)
        Me.txtExpAmt.Name = "txtExpAmt"
        Me.txtExpAmt.ReadOnly = True
        Me.txtExpAmt.Size = New System.Drawing.Size(131, 30)
        Me.txtExpAmt.TabIndex = 199
        Me.txtExpAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(3, 500)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 15)
        Me.Label4.TabIndex = 198
        Me.Label4.Text = "Expense"
        '
        'txtSaleAmt
        '
        Me.txtSaleAmt.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtSaleAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtSaleAmt.ForeColor = System.Drawing.Color.White
        Me.txtSaleAmt.Location = New System.Drawing.Point(409, 255)
        Me.txtSaleAmt.Name = "txtSaleAmt"
        Me.txtSaleAmt.ReadOnly = True
        Me.txtSaleAmt.Size = New System.Drawing.Size(131, 30)
        Me.txtSaleAmt.TabIndex = 200
        Me.txtSaleAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(191, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 18)
        Me.Label3.TabIndex = 204
        Me.Label3.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.CustomFormat = "dd/MM/yyyy"
        Me.txtToDate.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtToDate.Location = New System.Drawing.Point(249, 53)
        Me.txtToDate.MinDate = New Date(2014, 1, 1, 0, 0, 0, 0)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(107, 23)
        Me.txtToDate.TabIndex = 202
        Me.txtToDate.Value = New Date(2014, 4, 26, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(3, 55)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 18)
        Me.Label5.TabIndex = 203
        Me.Label5.Text = "From Date"
        '
        'txtFromDate
        '
        Me.txtFromDate.CustomFormat = "dd/MM/yyyy"
        Me.txtFromDate.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFromDate.Location = New System.Drawing.Point(78, 53)
        Me.txtFromDate.MinDate = New Date(2014, 1, 1, 0, 0, 0, 0)
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(107, 23)
        Me.txtFromDate.TabIndex = 201
        Me.txtFromDate.Value = New Date(2014, 4, 26, 0, 0, 0, 0)
        '
        'txtClosingAmt
        '
        Me.txtClosingAmt.BackColor = System.Drawing.Color.Red
        Me.txtClosingAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtClosingAmt.ForeColor = System.Drawing.Color.White
        Me.txtClosingAmt.Location = New System.Drawing.Point(409, 609)
        Me.txtClosingAmt.Name = "txtClosingAmt"
        Me.txtClosingAmt.ReadOnly = True
        Me.txtClosingAmt.Size = New System.Drawing.Size(131, 30)
        Me.txtClosingAmt.TabIndex = 206
        Me.txtClosingAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(3, 615)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 15)
        Me.Label6.TabIndex = 205
        Me.Label6.Text = "Closing Balance"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(405, 440)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 22)
        Me.Label8.TabIndex = 210
        Me.Label8.Text = "Payment"
        '
        'txtPurchaseAmt
        '
        Me.txtPurchaseAmt.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtPurchaseAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtPurchaseAmt.ForeColor = System.Drawing.Color.White
        Me.txtPurchaseAmt.Location = New System.Drawing.Point(284, 434)
        Me.txtPurchaseAmt.Name = "txtPurchaseAmt"
        Me.txtPurchaseAmt.ReadOnly = True
        Me.txtPurchaseAmt.Size = New System.Drawing.Size(114, 30)
        Me.txtPurchaseAmt.TabIndex = 209
        '
        'txtPayAmt
        '
        Me.txtPayAmt.BackColor = System.Drawing.Color.Red
        Me.txtPayAmt.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtPayAmt.ForeColor = System.Drawing.Color.White
        Me.txtPayAmt.Location = New System.Drawing.Point(409, 464)
        Me.txtPayAmt.Name = "txtPayAmt"
        Me.txtPayAmt.ReadOnly = True
        Me.txtPayAmt.Size = New System.Drawing.Size(131, 30)
        Me.txtPayAmt.TabIndex = 208
        Me.txtPayAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(3, 364)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 15)
        Me.Label9.TabIndex = 211
        Me.Label9.Text = "Purchase"
        '
        'txtMiscExpAmt
        '
        Me.txtMiscExpAmt.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtMiscExpAmt.Font = New System.Drawing.Font("Trebuchet MS", 10.75!)
        Me.txtMiscExpAmt.ForeColor = System.Drawing.Color.White
        Me.txtMiscExpAmt.Location = New System.Drawing.Point(150, 499)
        Me.txtMiscExpAmt.Name = "txtMiscExpAmt"
        Me.txtMiscExpAmt.ReadOnly = True
        Me.txtMiscExpAmt.Size = New System.Drawing.Size(85, 24)
        Me.txtMiscExpAmt.TabIndex = 212
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(76, 502)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 18)
        Me.Label10.TabIndex = 213
        Me.Label10.Text = "Misc. Exp"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(248, 502)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(46, 18)
        Me.Label11.TabIndex = 215
        Me.Label11.Text = "Salary"
        '
        'txtSalaryAmt
        '
        Me.txtSalaryAmt.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtSalaryAmt.Font = New System.Drawing.Font("Trebuchet MS", 10.75!)
        Me.txtSalaryAmt.ForeColor = System.Drawing.Color.White
        Me.txtSalaryAmt.Location = New System.Drawing.Point(296, 499)
        Me.txtSalaryAmt.Name = "txtSalaryAmt"
        Me.txtSalaryAmt.ReadOnly = True
        Me.txtSalaryAmt.Size = New System.Drawing.Size(102, 24)
        Me.txtSalaryAmt.TabIndex = 214
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(210, 438)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 22)
        Me.Label13.TabIndex = 217
        Me.Label13.Text = "Bill Amt"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(188, 469)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 18)
        Me.Label14.TabIndex = 221
        Me.Label14.Text = "Party Payment"
        '
        'txtParty
        '
        Me.txtParty.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtParty.Font = New System.Drawing.Font("Trebuchet MS", 10.75!)
        Me.txtParty.ForeColor = System.Drawing.Color.White
        Me.txtParty.Location = New System.Drawing.Point(296, 466)
        Me.txtParty.Name = "txtParty"
        Me.txtParty.ReadOnly = True
        Me.txtParty.Size = New System.Drawing.Size(102, 24)
        Me.txtParty.TabIndex = 220
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(-1, 468)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 15)
        Me.Label15.TabIndex = 219
        Me.Label15.Text = "Bank"
        '
        'txtBank
        '
        Me.txtBank.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtBank.Font = New System.Drawing.Font("Trebuchet MS", 10.75!)
        Me.txtBank.ForeColor = System.Drawing.Color.White
        Me.txtBank.Location = New System.Drawing.Point(82, 466)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.ReadOnly = True
        Me.txtBank.Size = New System.Drawing.Size(103, 24)
        Me.txtBank.TabIndex = 218
        '
        'txtOpDues
        '
        Me.txtOpDues.BackColor = System.Drawing.Color.Red
        Me.txtOpDues.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtOpDues.ForeColor = System.Drawing.Color.White
        Me.txtOpDues.Location = New System.Drawing.Point(409, 162)
        Me.txtOpDues.Name = "txtOpDues"
        Me.txtOpDues.ReadOnly = True
        Me.txtOpDues.Size = New System.Drawing.Size(131, 30)
        Me.txtOpDues.TabIndex = 223
        Me.txtOpDues.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(3, 166)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 15)
        Me.Label16.TabIndex = 222
        Me.Label16.Text = "Party Dues"
        '
        'txtTotal1
        '
        Me.txtTotal1.BackColor = System.Drawing.Color.DimGray
        Me.txtTotal1.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtTotal1.ForeColor = System.Drawing.Color.White
        Me.txtTotal1.Location = New System.Drawing.Point(409, 195)
        Me.txtTotal1.Name = "txtTotal1"
        Me.txtTotal1.ReadOnly = True
        Me.txtTotal1.Size = New System.Drawing.Size(131, 30)
        Me.txtTotal1.TabIndex = 225
        Me.txtTotal1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(3, 199)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(39, 15)
        Me.Label17.TabIndex = 224
        Me.Label17.Text = "Total"
        '
        'txtTotal2
        '
        Me.txtTotal2.BackColor = System.Drawing.Color.DimGray
        Me.txtTotal2.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtTotal2.ForeColor = System.Drawing.Color.White
        Me.txtTotal2.Location = New System.Drawing.Point(409, 348)
        Me.txtTotal2.Name = "txtTotal2"
        Me.txtTotal2.ReadOnly = True
        Me.txtTotal2.Size = New System.Drawing.Size(131, 30)
        Me.txtTotal2.TabIndex = 227
        Me.txtTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(405, 230)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 22)
        Me.Label12.TabIndex = 226
        Me.Label12.Text = "Total Bill"
        '
        'txtTotal3
        '
        Me.txtTotal3.BackColor = System.Drawing.Color.DimGray
        Me.txtTotal3.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtTotal3.ForeColor = System.Drawing.Color.White
        Me.txtTotal3.Location = New System.Drawing.Point(409, 532)
        Me.txtTotal3.Name = "txtTotal3"
        Me.txtTotal3.ReadOnly = True
        Me.txtTotal3.Size = New System.Drawing.Size(131, 30)
        Me.txtTotal3.TabIndex = 229
        Me.txtTotal3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(3, 536)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 15)
        Me.Label7.TabIndex = 228
        Me.Label7.Text = "Total"
        '
        'txtClosingDue
        '
        Me.txtClosingDue.BackColor = System.Drawing.Color.Red
        Me.txtClosingDue.Font = New System.Drawing.Font("Trebuchet MS", 14.75!)
        Me.txtClosingDue.ForeColor = System.Drawing.Color.White
        Me.txtClosingDue.Location = New System.Drawing.Point(409, 566)
        Me.txtClosingDue.Name = "txtClosingDue"
        Me.txtClosingDue.ReadOnly = True
        Me.txtClosingDue.Size = New System.Drawing.Size(131, 30)
        Me.txtClosingDue.TabIndex = 231
        Me.txtClosingDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(3, 570)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(128, 15)
        Me.Label18.TabIndex = 230
        Me.Label18.Text = "Closing Party Dues"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Trebuchet MS", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(406, 295)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(94, 18)
        Me.Label19.TabIndex = 232
        Me.Label19.Text = "Total Receipt"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(453, 49)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(81, 33)
        Me.btnPrint.TabIndex = 233
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        
        'frmCompanyStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SlateGray
        Me.ClientSize = New System.Drawing.Size(1119, 701)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtClosingDue)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtTotal3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTotal2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtTotal1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtOpDues)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtParty)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtBank)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtSalaryAmt)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtMiscExpAmt)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtPurchaseAmt)
        Me.Controls.Add(Me.txtPayAmt)
        Me.Controls.Add(Me.txtClosingAmt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtToDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtFromDate)
        Me.Controls.Add(Me.txtSaleAmt)
        Me.Controls.Add(Me.txtExpAmt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRecAmt)
        Me.Controls.Add(Me.grdPurchase)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdSale)
        Me.Controls.Add(Me.txtOpAmt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StripPanel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmCompanyStatement"
        Me.Text = "frmCompanyStatement"
        Me.StripPanel.ResumeLayout(False)
        Me.StripPanel.PerformLayout()
        CType(Me.grdSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StripPanel As System.Windows.Forms.Panel
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOpAmt As System.Windows.Forms.TextBox
    Friend WithEvents grdSale As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdPurchase As System.Windows.Forms.DataGridView
    Friend WithEvents txtRecAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtExpAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSaleAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtClosingAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPurchaseAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtPayAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtMiscExpAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSalaryAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtParty As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents txtOpDues As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTotal1 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotal2 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtTotal3 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtClosingDue As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
End Class
