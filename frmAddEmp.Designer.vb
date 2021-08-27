<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddEmp
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
        Me.rbInactive = New System.Windows.Forms.RadioButton()
        Me.rbActive = New System.Windows.Forms.RadioButton()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.lblLeavingDate = New System.Windows.Forms.Label()
        Me.txtLeavingDate = New System.Windows.Forms.DateTimePicker()
        Me.txtSalary = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMobileNo = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAge = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFatherName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.StripPanel = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.StripPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbInactive
        '
        Me.rbInactive.AutoSize = True
        Me.rbInactive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbInactive.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.rbInactive.ForeColor = System.Drawing.Color.White
        Me.rbInactive.Location = New System.Drawing.Point(123, 20)
        Me.rbInactive.Name = "rbInactive"
        Me.rbInactive.Size = New System.Drawing.Size(72, 21)
        Me.rbInactive.TabIndex = 8
        Me.rbInactive.Text = "Inactive"
        Me.rbInactive.UseVisualStyleBackColor = True
        '
        'rbActive
        '
        Me.rbActive.AutoSize = True
        Me.rbActive.Checked = True
        Me.rbActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbActive.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.rbActive.ForeColor = System.Drawing.Color.White
        Me.rbActive.Location = New System.Drawing.Point(22, 20)
        Me.rbActive.Name = "rbActive"
        Me.rbActive.Size = New System.Drawing.Size(62, 21)
        Me.rbActive.TabIndex = 7
        Me.rbActive.TabStop = True
        Me.rbActive.Text = "Active"
        Me.rbActive.UseVisualStyleBackColor = True
        '
        'lblReason
        '
        Me.lblReason.AutoSize = True
        Me.lblReason.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.lblReason.ForeColor = System.Drawing.Color.White
        Me.lblReason.Location = New System.Drawing.Point(133, 411)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(49, 18)
        Me.lblReason.TabIndex = 415
        Me.lblReason.Text = "Reason"
        Me.lblReason.Visible = False
        '
        'txtReason
        '
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtReason.Location = New System.Drawing.Point(136, 436)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(309, 23)
        Me.txtReason.TabIndex = 10
        Me.txtReason.Visible = False
        '
        'lblLeavingDate
        '
        Me.lblLeavingDate.AutoSize = True
        Me.lblLeavingDate.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.lblLeavingDate.ForeColor = System.Drawing.Color.White
        Me.lblLeavingDate.Location = New System.Drawing.Point(133, 359)
        Me.lblLeavingDate.Name = "lblLeavingDate"
        Me.lblLeavingDate.Size = New System.Drawing.Size(85, 18)
        Me.lblLeavingDate.TabIndex = 413
        Me.lblLeavingDate.Text = "Leaving Date"
        Me.lblLeavingDate.Visible = False
        '
        'txtLeavingDate
        '
        Me.txtLeavingDate.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtLeavingDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtLeavingDate.Location = New System.Drawing.Point(136, 383)
        Me.txtLeavingDate.Name = "txtLeavingDate"
        Me.txtLeavingDate.Size = New System.Drawing.Size(113, 23)
        Me.txtLeavingDate.TabIndex = 9
        Me.txtLeavingDate.Visible = False
        '
        'txtSalary
        '
        Me.txtSalary.BackColor = System.Drawing.Color.White
        Me.txtSalary.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtSalary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSalary.Location = New System.Drawing.Point(136, 266)
        Me.txtSalary.MinimumSize = New System.Drawing.Size(4, 24)
        Me.txtSalary.Name = "txtSalary"
        Me.txtSalary.Size = New System.Drawing.Size(99, 23)
        Me.txtSalary.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(23, 241)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 18)
        Me.Label4.TabIndex = 411
        Me.Label4.Text = "Contact No"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.BackColor = System.Drawing.Color.White
        Me.txtMobileNo.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtMobileNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtMobileNo.Location = New System.Drawing.Point(136, 235)
        Me.txtMobileNo.MinimumSize = New System.Drawing.Size(4, 24)
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Size = New System.Drawing.Size(309, 23)
        Me.txtMobileNo.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbInactive)
        Me.GroupBox1.Controls.Add(Me.rbActive)
        Me.GroupBox1.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(136, 300)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(235, 54)
        Me.GroupBox1.TabIndex = 407
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Status"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(23, 272)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(46, 18)
        Me.Label17.TabIndex = 408
        Me.Label17.Text = "Salary"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(23, 212)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 18)
        Me.Label6.TabIndex = 403
        Me.Label6.Text = "City"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(23, 182)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 18)
        Me.Label7.TabIndex = 400
        Me.Label7.Text = "Address"
        '
        'txtCity
        '
        Me.txtCity.BackColor = System.Drawing.Color.White
        Me.txtCity.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCity.Location = New System.Drawing.Point(136, 206)
        Me.txtCity.MinimumSize = New System.Drawing.Size(4, 24)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(309, 23)
        Me.txtCity.TabIndex = 4
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAddress.Location = New System.Drawing.Point(136, 176)
        Me.txtAddress.MinimumSize = New System.Drawing.Size(4, 24)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(308, 23)
        Me.txtAddress.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(23, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 18)
        Me.Label5.TabIndex = 397
        Me.Label5.Text = "Age"
        '
        'txtAge
        '
        Me.txtAge.BackColor = System.Drawing.Color.White
        Me.txtAge.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAge.Location = New System.Drawing.Point(136, 144)
        Me.txtAge.MinimumSize = New System.Drawing.Size(4, 24)
        Me.txtAge.Name = "txtAge"
        Me.txtAge.Size = New System.Drawing.Size(75, 23)
        Me.txtAge.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(23, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 18)
        Me.Label2.TabIndex = 396
        Me.Label2.Text = "F / H Name"
        '
        'txtFatherName
        '
        Me.txtFatherName.BackColor = System.Drawing.Color.White
        Me.txtFatherName.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtFatherName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtFatherName.Location = New System.Drawing.Point(136, 115)
        Me.txtFatherName.MinimumSize = New System.Drawing.Size(4, 24)
        Me.txtFatherName.Name = "txtFatherName"
        Me.txtFatherName.Size = New System.Drawing.Size(308, 23)
        Me.txtFatherName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(23, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 18)
        Me.Label1.TabIndex = 395
        Me.Label1.Text = "Name"
        '
        'lblID
        '
        Me.lblID.BackColor = System.Drawing.Color.Black
        Me.lblID.ForeColor = System.Drawing.Color.White
        Me.lblID.Location = New System.Drawing.Point(286, 46)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(23, 14)
        Me.lblID.TabIndex = 394
        Me.lblID.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Trebuchet MS", 10.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(359, 488)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(86, 47)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Trebuchet MS", 10.75!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(267, 488)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 47)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "&Submit"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.Font = New System.Drawing.Font("Trebuchet MS", 14.75!, System.Drawing.FontStyle.Bold)
        Me.lblCaption.ForeColor = System.Drawing.Color.White
        Me.lblCaption.Location = New System.Drawing.Point(10, 7)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(236, 26)
        Me.lblCaption.TabIndex = 0
        Me.lblCaption.Text = "Add / Update Employee"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(136, 85)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(308, 23)
        Me.txtName.TabIndex = 0
        '
        'StripPanel
        '
        Me.StripPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.StripPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.StripPanel.Controls.Add(Me.lblCaption)
        Me.StripPanel.Location = New System.Drawing.Point(0, 0)
        Me.StripPanel.Name = "StripPanel"
        Me.StripPanel.Size = New System.Drawing.Size(1017, 43)
        Me.StripPanel.TabIndex = 392
        '
        'frmAddEmp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SlateGray
        Me.ClientSize = New System.Drawing.Size(1073, 684)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblReason)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.lblLeavingDate)
        Me.Controls.Add(Me.txtLeavingDate)
        Me.Controls.Add(Me.txtSalary)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtMobileNo)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtAge)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtFatherName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.StripPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmAddEmp"
        Me.Text = "frmAddEmp"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StripPanel.ResumeLayout(False)
        Me.StripPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbInactive As System.Windows.Forms.RadioButton
    Friend WithEvents rbActive As System.Windows.Forms.RadioButton
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents lblLeavingDate As System.Windows.Forms.Label
    Friend WithEvents txtLeavingDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSalary As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAge As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFatherName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents StripPanel As System.Windows.Forms.Panel
End Class
