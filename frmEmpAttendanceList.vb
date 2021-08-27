Imports System
Imports System.Data
Imports System.Data.Odbc
Public Class frmEmpAttendanceList
    Dim cnn As New DataConn.Conn
    Private Sub frmEmpAttendanceList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetBounds(0, 0, MDI.Width, MDI.Height)
        Me.MdiParent = MDI
        '==============================
        Grid.Width = MDI.ClientSize.Width - MDI.Panel.Width
        Grid.Height = Me.Height - 100
        StripPanel.Width = MDI.Width
        ToolStrip1.Cursor = Cursors.Hand
        btnAdd.Image = MDI.ImageList1.Images(0)
        btnEdit.Image = MDI.ImageList1.Images(1)
        btnDelete.Image = MDI.ImageList1.Images(2)
        btnSearch.Image = MDI.ImageList1.Images(3)
        btnPrint.Image = MDI.ImageList1.Images(4)
        btnExit.Image = MDI.ImageList1.Images(5)
        '=============================
        txtFromDate.Text = DateAdd(DateInterval.Month, -1, Date.Today())
        txtToDate.Text = Date.Today()
        '=============================
        Dim da As New OdbcDataAdapter
        Dim ds As New DataSet

        BindGrid()
    End Sub
    Private Sub BindGrid()
        If txtFromDate.Text <> "" Then
            Dim reader As OdbcDataReader
            Dim cmd As Data.Odbc.OdbcCommand = New Data.Odbc.OdbcCommand
            cmd.Connection = cnn.cnn
            cmd.CommandText = "{call GET_Emp_Attendance(?,?,?)}"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@SessionID", OdbcType.Int)
            cmd.Parameters("@SessionID").Value = Val(MDI.lblMasterBranchID.Text)

            cmd.Parameters.Add("@FromDate", OdbcType.Date)
            cmd.Parameters("@FromDate").Value = cnn.GetDate(txtFromDate.Text)

            cmd.Parameters.Add("@ToDate", OdbcType.Date)
            cmd.Parameters("@ToDate").Value = cnn.GetDate(txtToDate.Text)

            If cnn.cnn.State = ConnectionState.Open Then cnn.cnn.Close()
            cnn.cnn.Open()
            reader = cmd.ExecuteReader()
            Dim dt As DataTable = New DataTable()
            dt.Load(reader)
            Grid.DataSource = dt
            Grid.Columns(0).Visible = False
            Grid.Columns(1).HeaderText = "Emp Name"
            Grid.Columns(2).HeaderText = "Father Name"
            Grid.Columns(3).HeaderText = "Mobile No"
            cnn.cnn.Close()
        End If
    End Sub
    Private Sub FormRes()
        Grid.Width = MDI.ClientSize.Width - MDI.Panel.Width
        Grid.Height = Me.Height - 100
        StripPanel.Width = Me.Width
    End Sub

    Private Sub btnSearch2_Click(sender As Object, e As EventArgs) Handles btnSearch2.Click
        BindGrid()
        frameSearch.Visible = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If MDI.lblAccessType.Text <> "A" Then
            If cnn.GetUserAccess(13, "fldAdd") = False Then
                cnn.ErrMsgBox("You are not Authorised to do this Operation  !")
                Exit Sub
            End If
        End If
        cnn.ShowForm(frmEmpAttendance)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        frameSearch.Visible = True
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnCancel2_Click(sender As Object, e As EventArgs) Handles btnCancel2.Click
        frameSearch.Visible = False
    End Sub
End Class