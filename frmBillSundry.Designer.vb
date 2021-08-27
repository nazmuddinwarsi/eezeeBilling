<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillSundry
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
        Me.optPrevious = New System.Windows.Forms.RadioButton()
        Me.optTaxable = New System.Windows.Forms.RadioButton()
        Me.optBill = New System.Windows.Forms.RadioButton()
        Me.lblID = New System.Windows.Forms.Label()
        Me.frameOf = New System.Windows.Forms.GroupBox()
        Me.optBasic = New System.Windows.Forms.RadioButton()
        Me.optPer = New System.Windows.Forms.RadioButton()
        Me.optLow = New System.Windows.Forms.RadioButton()
        Me.optAmt = New System.Windows.Forms.RadioButton()
        Me.frameRoundOff = New System.Windows.Forms.GroupBox()
        Me.optUp = New System.Windows.Forms.RadioButton()
        Me.optAuto = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optSub = New System.Windows.Forms.RadioButton()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optAdd = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkRoundOff = New System.Windows.Forms.CheckBox()
        Me.cmbNature = New System.Windows.Forms.ComboBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.StripPanel = New System.Windows.Forms.Panel()
        Me.frameOf.SuspendLayout()
        Me.frameRoundOff.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.StripPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'optPrevious
        '
        Me.optPrevious.AutoSize = True
        Me.optPrevious.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optPrevious.ForeColor = System.Drawing.Color.White
        Me.optPrevious.Location = New System.Drawing.Point(6, 97)
        Me.optPrevious.Name = "optPrevious"
        Me.optPrevious.Size = New System.Drawing.Size(183, 22)
        Me.optPrevious.TabIndex = 56
        Me.optPrevious.TabStop = True
        Me.optPrevious.Text = "Previous BillSundry Amount"
        Me.optPrevious.UseVisualStyleBackColor = True
        '
        'optTaxable
        '
        Me.optTaxable.AutoSize = True
        Me.optTaxable.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optTaxable.ForeColor = System.Drawing.Color.White
        Me.optTaxable.Location = New System.Drawing.Point(6, 45)
        Me.optTaxable.Name = "optTaxable"
        Me.optTaxable.Size = New System.Drawing.Size(118, 22)
        Me.optTaxable.TabIndex = 54
        Me.optTaxable.TabStop = True
        Me.optTaxable.Text = "Taxable Amount"
        Me.optTaxable.UseVisualStyleBackColor = True
        '
        'optBill
        '
        Me.optBill.AutoSize = True
        Me.optBill.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optBill.ForeColor = System.Drawing.Color.White
        Me.optBill.Location = New System.Drawing.Point(6, 71)
        Me.optBill.Name = "optBill"
        Me.optBill.Size = New System.Drawing.Size(115, 22)
        Me.optBill.TabIndex = 55
        Me.optBill.TabStop = True
        Me.optBill.Text = "Net Bill Amount"
        Me.optBill.UseVisualStyleBackColor = True
        '
        'lblID
        '
        Me.lblID.BackColor = System.Drawing.Color.White
        Me.lblID.Location = New System.Drawing.Point(135, 392)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(38, 25)
        Me.lblID.TabIndex = 66
        Me.lblID.Visible = False
        '
        'frameOf
        '
        Me.frameOf.Controls.Add(Me.optPrevious)
        Me.frameOf.Controls.Add(Me.optBill)
        Me.frameOf.Controls.Add(Me.optTaxable)
        Me.frameOf.Controls.Add(Me.optBasic)
        Me.frameOf.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.frameOf.ForeColor = System.Drawing.Color.White
        Me.frameOf.Location = New System.Drawing.Point(0, 62)
        Me.frameOf.Name = "frameOf"
        Me.frameOf.Size = New System.Drawing.Size(199, 133)
        Me.frameOf.TabIndex = 55
        Me.frameOf.TabStop = False
        Me.frameOf.Text = "of"
        '
        'optBasic
        '
        Me.optBasic.AutoSize = True
        Me.optBasic.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optBasic.ForeColor = System.Drawing.Color.White
        Me.optBasic.Location = New System.Drawing.Point(6, 19)
        Me.optBasic.Name = "optBasic"
        Me.optBasic.Size = New System.Drawing.Size(104, 22)
        Me.optBasic.TabIndex = 53
        Me.optBasic.TabStop = True
        Me.optBasic.Text = "Basic Amount"
        Me.optBasic.UseVisualStyleBackColor = True
        '
        'optPer
        '
        Me.optPer.AutoSize = True
        Me.optPer.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optPer.ForeColor = System.Drawing.Color.White
        Me.optPer.Location = New System.Drawing.Point(6, 46)
        Me.optPer.Name = "optPer"
        Me.optPer.Size = New System.Drawing.Size(90, 22)
        Me.optPer.TabIndex = 54
        Me.optPer.Text = "Percentage"
        Me.optPer.UseVisualStyleBackColor = True
        '
        'optLow
        '
        Me.optLow.AutoSize = True
        Me.optLow.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optLow.ForeColor = System.Drawing.Color.White
        Me.optLow.Location = New System.Drawing.Point(6, 71)
        Me.optLow.Name = "optLow"
        Me.optLow.Size = New System.Drawing.Size(104, 22)
        Me.optLow.TabIndex = 55
        Me.optLow.TabStop = True
        Me.optLow.Text = "Always Lower"
        Me.optLow.UseVisualStyleBackColor = True
        '
        'optAmt
        '
        Me.optAmt.AutoSize = True
        Me.optAmt.Checked = True
        Me.optAmt.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optAmt.ForeColor = System.Drawing.Color.White
        Me.optAmt.Location = New System.Drawing.Point(6, 20)
        Me.optAmt.Name = "optAmt"
        Me.optAmt.Size = New System.Drawing.Size(124, 22)
        Me.optAmt.TabIndex = 53
        Me.optAmt.TabStop = True
        Me.optAmt.Text = "Absolute Amount"
        Me.optAmt.UseVisualStyleBackColor = True
        '
        'frameRoundOff
        '
        Me.frameRoundOff.Controls.Add(Me.optLow)
        Me.frameRoundOff.Controls.Add(Me.optUp)
        Me.frameRoundOff.Controls.Add(Me.optAuto)
        Me.frameRoundOff.Enabled = False
        Me.frameRoundOff.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.frameRoundOff.ForeColor = System.Drawing.Color.White
        Me.frameRoundOff.Location = New System.Drawing.Point(12, 202)
        Me.frameRoundOff.Name = "frameRoundOff"
        Me.frameRoundOff.Size = New System.Drawing.Size(199, 97)
        Me.frameRoundOff.TabIndex = 71
        Me.frameRoundOff.TabStop = False
        Me.frameRoundOff.Text = "Type"
        Me.frameRoundOff.Visible = False
        '
        'optUp
        '
        Me.optUp.AutoSize = True
        Me.optUp.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optUp.ForeColor = System.Drawing.Color.White
        Me.optUp.Location = New System.Drawing.Point(6, 45)
        Me.optUp.Name = "optUp"
        Me.optUp.Size = New System.Drawing.Size(103, 22)
        Me.optUp.TabIndex = 54
        Me.optUp.TabStop = True
        Me.optUp.Text = "Always Upper"
        Me.optUp.UseVisualStyleBackColor = True
        '
        'optAuto
        '
        Me.optAuto.AutoSize = True
        Me.optAuto.Checked = True
        Me.optAuto.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optAuto.ForeColor = System.Drawing.Color.White
        Me.optAuto.Location = New System.Drawing.Point(6, 19)
        Me.optAuto.Name = "optAuto"
        Me.optAuto.Size = New System.Drawing.Size(86, 22)
        Me.optAuto.TabIndex = 53
        Me.optAuto.TabStop = True
        Me.optAuto.Text = "Automatic"
        Me.optAuto.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.frameOf)
        Me.GroupBox2.Controls.Add(Me.optPer)
        Me.GroupBox2.Controls.Add(Me.optAmt)
        Me.GroupBox2.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(217, 140)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(199, 195)
        Me.GroupBox2.TabIndex = 70
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Type"
        Me.GroupBox2.Visible = False
        '
        'optSub
        '
        Me.optSub.AutoSize = True
        Me.optSub.Checked = True
        Me.optSub.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optSub.ForeColor = System.Drawing.Color.White
        Me.optSub.Location = New System.Drawing.Point(96, 19)
        Me.optSub.Name = "optSub"
        Me.optSub.Size = New System.Drawing.Size(99, 22)
        Me.optSub.TabIndex = 54
        Me.optSub.TabStop = True
        Me.optSub.Text = "Substractive"
        Me.optSub.UseVisualStyleBackColor = True
        '
        'txtValue
        '
        Me.txtValue.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtValue.Location = New System.Drawing.Point(104, 83)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(78, 23)
        Me.txtValue.TabIndex = 72
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optSub)
        Me.GroupBox1.Controls.Add(Me.optAdd)
        Me.GroupBox1.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(12, 140)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(199, 56)
        Me.GroupBox1.TabIndex = 69
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Type"
        Me.GroupBox1.Visible = False
        '
        'optAdd
        '
        Me.optAdd.AutoSize = True
        Me.optAdd.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.optAdd.ForeColor = System.Drawing.Color.White
        Me.optAdd.Location = New System.Drawing.Point(6, 19)
        Me.optAdd.Name = "optAdd"
        Me.optAdd.Size = New System.Drawing.Size(75, 22)
        Me.optAdd.TabIndex = 53
        Me.optAdd.Text = "Additive"
        Me.optAdd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(13, 111)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 18)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "Round Off"
        Me.Label1.Visible = False
        '
        'chkRoundOff
        '
        Me.chkRoundOff.AutoSize = True
        Me.chkRoundOff.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.chkRoundOff.ForeColor = System.Drawing.Color.White
        Me.chkRoundOff.Location = New System.Drawing.Point(104, 112)
        Me.chkRoundOff.Name = "chkRoundOff"
        Me.chkRoundOff.Size = New System.Drawing.Size(197, 22)
        Me.chkRoundOff.TabIndex = 67
        Me.chkRoundOff.Text = "Round Off Bill Sundry Amount"
        Me.chkRoundOff.UseVisualStyleBackColor = True
        Me.chkRoundOff.Visible = False
        '
        'cmbNature
        '
        Me.cmbNature.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbNature.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNature.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbNature.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.cmbNature.FormattingEnabled = True
        Me.cmbNature.Location = New System.Drawing.Point(715, 57)
        Me.cmbNature.Name = "cmbNature"
        Me.cmbNature.Size = New System.Drawing.Size(252, 26)
        Me.cmbNature.TabIndex = 62
        Me.cmbNature.Visible = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Trebuchet MS", 10.75!, System.Drawing.FontStyle.Bold)
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(335, 351)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(81, 38)
        Me.btnExit.TabIndex = 65
        Me.btnExit.Text = "&Cancel"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(621, 57)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 18)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Under"
        Me.Label8.Visible = False
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Trebuchet MS", 10.75!, System.Drawing.FontStyle.Bold)
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(248, 351)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(81, 38)
        Me.btnLogin.TabIndex = 63
        Me.btnLogin.Text = "&Save"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.txtName.Location = New System.Drawing.Point(104, 57)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(252, 23)
        Me.txtName.TabIndex = 59
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(13, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 18)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "Default Value"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Trebuchet MS", 9.75!)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(13, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 18)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Name"
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.Font = New System.Drawing.Font("Trebuchet MS", 14.75!, System.Drawing.FontStyle.Bold)
        Me.lblCaption.ForeColor = System.Drawing.Color.White
        Me.lblCaption.Location = New System.Drawing.Point(12, 7)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(163, 26)
        Me.lblCaption.TabIndex = 0
        Me.lblCaption.Text = "Discount Master"
        '
        'StripPanel
        '
        Me.StripPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.StripPanel.Controls.Add(Me.lblCaption)
        Me.StripPanel.Location = New System.Drawing.Point(0, 0)
        Me.StripPanel.Name = "StripPanel"
        Me.StripPanel.Size = New System.Drawing.Size(473, 43)
        Me.StripPanel.TabIndex = 58
        '
        'frmBillSundry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SlateGray
        Me.ClientSize = New System.Drawing.Size(875, 565)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.frameRoundOff)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkRoundOff)
        Me.Controls.Add(Me.cmbNature)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StripPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmBillSundry"
        Me.Text = "frmBillSundry"
        Me.frameOf.ResumeLayout(False)
        Me.frameOf.PerformLayout()
        Me.frameRoundOff.ResumeLayout(False)
        Me.frameRoundOff.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StripPanel.ResumeLayout(False)
        Me.StripPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents optPrevious As System.Windows.Forms.RadioButton
    Friend WithEvents optTaxable As System.Windows.Forms.RadioButton
    Friend WithEvents optBill As System.Windows.Forms.RadioButton
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents frameOf As System.Windows.Forms.GroupBox
    Friend WithEvents optBasic As System.Windows.Forms.RadioButton
    Friend WithEvents optPer As System.Windows.Forms.RadioButton
    Friend WithEvents optLow As System.Windows.Forms.RadioButton
    Friend WithEvents optAmt As System.Windows.Forms.RadioButton
    Friend WithEvents frameRoundOff As System.Windows.Forms.GroupBox
    Friend WithEvents optUp As System.Windows.Forms.RadioButton
    Friend WithEvents optAuto As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optSub As System.Windows.Forms.RadioButton
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optAdd As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkRoundOff As System.Windows.Forms.CheckBox
    Friend WithEvents cmbNature As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents StripPanel As System.Windows.Forms.Panel
End Class
