Imports System.Collections.Generic
Imports System.Text

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
Imports System.Data.Odbc
Imports System.Windows.Forms
Namespace DataConn
    Public Class Conn
        Public Declare Function GetActiveWindow Lib "user32" () As System.IntPtr

        'Public _connString As String = "Server=COMPUTER-PC;Database=AromaStocks;User ID='aroma';Password='aromatyk';Trusted_Connection=True;"
        'Public _connString As String = "Server=MANOJ\SQLEXPRESS;Database=AromaStocks;User ID='aroma';Password='aromatyk';Trusted_Connection=True;"
        Public _connString As String = "DSN=eezeeBilling_RK;Database=eezeeBilling_RK;User ID='';Password=''"


        'Public cnn As Data.Odbc.OdbcConnection = New Data.Odbc.OdbcConnection(_connString)
        Public cnn As Data.Odbc.OdbcConnection = New Data.Odbc.OdbcConnection(_connString)
        Public cnn2 As Data.Odbc.OdbcConnection = New Data.Odbc.OdbcConnection(_connString)
        Public Tran As Data.Odbc.OdbcTransaction
        Public msgBoxTitle As String = "Welcome to eezee Billing"
        Public LoginType, strSQL, strUpdate, strBind As String
        Public UserLoginID As Integer
        Public SRVRDate, SRVRTIME

        Public Sub OpenConn()
            If cnn.State = ConnectionState.Closed Then
                cnn.Open()
            End If
        End Sub
        Public Sub CloseConn()
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Sub
        Function SQLquote(ByVal s)
            If s Is DBNull.Value Then
                SQLquote = "NULL"
            Else
                SQLquote = "'" & Replace(s, "'", "''") & "'"
            End If
        End Function
        Public Function CheckExpDate(ByVal dt As String, ByVal cID As Integer, ByVal bID As Integer) As Integer
            Dim strSQL As String
            Dim wDate As Date
            Dim dMonth, dYear As Integer
            strSQL = "Select dm, dy from Para Where MasterCompanyID = " & Val(cID) & " AND MasterBranchID = " & Val(bID)
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("dm")) Then
                    dMonth = Decrypt(dr.Item("dm"))
                Else
                    dr.Close()
                    cmd.Connection.Close()
                    Return 1 ' 1 DATABASE CONFIGURATION PROBLEM
                End If
                If Not IsDBNull(dr.Item("dy")) Then
                    dYear = Decrypt(dr.Item("dy"))
                Else
                    dr.Close()
                    cmd.Connection.Close()
                    Return 1 ' 1 DATABASE CONFIGURATION PROBLEM
                End If
                wDate = dYear & "-" & dMonth & "-01"
            Else
                dr.Close()
                cmd.Connection.Close()
                Return 1 ' 1 DATABASE CONFIGURATION PROBLEM
            End If
            dr.Close()
            cmd.Connection.Close()
            Dim iDay As Integer
            If Month(GetDate(dt)) >= Val(dMonth) And Year(GetDate(dt)) >= Val(dYear) Then
                Return -1 ' 0 FOR SOFTWARE HAS BEEN EXPIRED
            End If
            iDay = DateDiff(DateInterval.Day, CDate(GetDate(dt)), CDate(wDate))
            If iDay < 10 Then
                Return 2 ' 2 SOFTWARE IS GOING TO BE EXPIRED
            End If
            If Val(dMonth) > 12 Then
                Return 1 ' 1 DATABASE CONFIGURATION PROBLEM
            End If
            If Val(dMonth) = 0 Or Val(dYear) = 0 Then
                Return 1 ' 1 DATABASE CONFIGURATION PROBLEM
            End If


        End Function
        Public Function Encrypt(ByVal StrPword) As String
            On Error Resume Next
            Dim i, ct As Integer
            Dim letter, enc, strHexvalappend, strHexval As String
            enc = ""
            For i = 1 To Len(StrPword)
                letter = Mid(StrPword, i, 1)
                enc = enc & Chr(Asc(letter) + i + 3)
            Next

            For ct = 1 To Len(enc)
                strHexvalappend = Hex(Asc(Mid(enc, ct, 1)))
                strHexval = strHexval & strHexvalappend
            Next
            Encrypt = StrReverse(strHexval)
        End Function
        Public Function Decrypt(ByVal strDecoded_Pword As String) As String
            On Error Resume Next
            Dim i, ct As Integer
            Dim letter, dec, StrValappend, strVal As String
            dec = ""
            strDecoded_Pword = StrReverse(strDecoded_Pword)

            For ct = 1 To Len(strDecoded_Pword) Step 2
                StrValappend = Chr(Val("&H" & (Mid(strDecoded_Pword, ct, 2))))
                strVal = strVal & StrValappend
            Next
            strDecoded_Pword = strVal

            For i = 1 To Len(strDecoded_Pword)
                letter = Mid(strDecoded_Pword, i, 1)
                dec = dec & Chr(Asc(letter) - i - 3)
            Next
            Decrypt = dec
        End Function
        Function SQLSearch(ByVal s)
            If s Is DBNull.Value Then
                SQLSearch = "NULL"
            Else
                SQLSearch = "'%" & Replace(s, "'", "''") & "%'"
            End If
        End Function
        Function CrystalSearch(ByVal s)
            If s Is DBNull.Value Then
                CrystalSearch = "NULL"
            Else
                CrystalSearch = "'*" & Replace(s, "'", "''") & "*'"
            End If
        End Function
        Public Function RoundUp(ByVal Value As Double) As Double
            Dim Temp As Double
            Temp = Int(Value)
            If Temp <> Value Then
                Temp = Temp + 1
            End If
            RoundUp = Temp
        End Function
        Public Function RoundDown(ByVal Value As Double) As Double
            Dim Temp As Double
            Temp = Int(Value)
            If Temp <> Value Then
                Temp = Temp - 1
            End If
            RoundDown = Temp
        End Function
        Public Function FullRound(Value As Double) As Integer
            'Dim decimalpoints  As Double Math.Abs(value - Math.Floor(value))
            Dim retValue As Integer = 0
            Dim decimalpoints As Double = Math.Abs(Value - Math.Floor(Value))
            If (decimalpoints > 0.5) Then
                retValue = Math.Round(Value)
            Else
                retValue = Math.Floor(Value)
            End If
            Return retValue
        End Function
        Public Function Num2(ByVal SReal)
            Num2 = Format(Val(SReal), "#0.00")
        End Function
        Function GetDate(ByVal DateTxt As String)
            If DateTxt <> "" Then
                Dim dDate As Date = DateTxt
                GetDate = dDate.Year & "-" & Format(dDate.Month, "00") & "-" & dDate.Day
            End If
        End Function
        Public Function GetUserAccess(ByVal ModuleID As Integer, ByVal sType As String) As Boolean
            Dim strSQL As String
            strSQL = "Select " & sType & " from UserRight Where UserID=" & Val(MDI.lblUserLoginID.Text) & " AND ModuleID=" & Val(ModuleID)
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If dr.Item(sType) = "False" Then
                    Return False
                Else
                    Return True
                End If
            End If

        End Function
        Public Sub Check_Insert_Product(ByVal Company_ID As Integer, ByVal Branch_ID As Integer, ByVal Prod_ID As Integer, ByVal SM_ID As Integer)
            Try

                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Open()
                Dim cmd As New OdbcCommand("{call CheckAndInsertProduct (?,?,?,?)}", cnn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@CompID", OdbcType.SmallInt).Value = Company_ID
                cmd.Parameters.Add("@BranchID", OdbcType.SmallInt).Value = Branch_ID
                cmd.Parameters.Add("@ProdID", OdbcType.SmallInt).Value = Prod_ID
                cmd.Parameters.Add("@SMID", OdbcType.SmallInt).Value = SM_ID
                cmd.ExecuteNonQuery()
                cmd.Connection.Close()
            Catch ex As Exception
                ErrMsgBox(ex.Message.ToString())
            End Try
        End Sub
        Public Function GetBuyerDues(ByVal Company_ID As Integer, ByVal Branch_ID As Integer, ByVal fldDate As String) As Double
            Try

                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Open()
                Dim cmd As New OdbcCommand("{call GetBuyerTotalDues (?,?,?)}", cnn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@CompID", OdbcType.SmallInt).Value = Company_ID
                cmd.Parameters.Add("@BranchID", OdbcType.SmallInt).Value = Branch_ID
                cmd.Parameters.Add("@fldDate", OdbcType.Date).Value = fldDate
                'cmd.Parameters.Add("@SMID", OdbcType.SmallInt).Value = SM_ID
                'cmd.ExecuteNonQuery()
                Dim iVal As Double = cmd.ExecuteScalar()
                cmd.Connection.Close()
                Return iVal
            Catch ex As Exception
                ErrMsgBox("Something went Wrong  !")
            End Try
        End Function
        Public Sub GetBuyerStatement(ByVal Company_ID As Integer, ByVal Branch_ID As Integer, ByVal Account_ID As Integer, ByVal From_Date As String, ByVal To_Date As String)
            Try

                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Open()
                Dim cmd As New OdbcCommand("{call GetBuyerOpeningBalance (?,?,?,?,?)}", cnn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@CompID", OdbcType.SmallInt).Value = Company_ID
                cmd.Parameters.Add("@BranchID", OdbcType.SmallInt).Value = Branch_ID
                cmd.Parameters.Add("@AccountID", OdbcType.SmallInt).Value = Account_ID
                cmd.Parameters.Add("@fromDate", OdbcType.Date).Value = From_Date
                cmd.Parameters.Add("@toDate", OdbcType.Date).Value = To_Date
                'cmd.Parameters.Add("@SMID", OdbcType.SmallInt).Value = SM_ID
                cmd.ExecuteNonQuery()
                cmd.Connection.Close()
            Catch ex As Exception
                ErrMsgBox("Something went Wrong  !")
            End Try
        End Sub
        Public Function GetBuyerDueByID(ByVal Company_ID As Integer, ByVal Branch_ID As Integer, ByVal Account_ID As Integer, ByVal fldDate As String) As Double
            Try

                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Open()
                Dim cmd As New OdbcCommand("{call GetTotalDuesByID (?,?,?,?)}", cnn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@CompID", OdbcType.SmallInt).Value = Company_ID
                cmd.Parameters.Add("@BranchID", OdbcType.SmallInt).Value = Branch_ID
                cmd.Parameters.Add("@AccountID", OdbcType.SmallInt).Value = Account_ID
                cmd.Parameters.Add("@fldDate", OdbcType.Date).Value = fldDate
                cmd.ExecuteNonQuery()
                Dim iVal As Double = cmd.ExecuteScalar()
                cmd.Connection.Close()
                Return iVal
            Catch ex As Exception
                ErrMsgBox("Something went Wrong  !")
            End Try
        End Function
        Public Sub GetBuyerDuesReport(ByVal Company_ID As Integer, ByVal Branch_ID As Integer, ByVal fldDate As String)
            Try

                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Open()
                Dim cmd As New OdbcCommand("{call GenerateDues (?,?,?)}", cnn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@fldDate", OdbcType.Date).Value = fldDate
                cmd.Parameters.Add("@CompID", OdbcType.SmallInt).Value = Company_ID
                cmd.Parameters.Add("@BranchID", OdbcType.SmallInt).Value = Branch_ID

                cmd.ExecuteNonQuery()
                cmd.Connection.Close()
            Catch ex As Exception
                ErrMsgBox("Something went Wrong  !")
            End Try
        End Sub
        Public Sub GetSupplierStatement(ByVal Company_ID As Integer, ByVal Branch_ID As Integer, ByVal Account_ID As Integer, ByVal From_Date As String, ByVal To_Date As String)
            Try

                If cnn.State = ConnectionState.Open Then cnn.Close()
                cnn.Open()
                Dim cmd As New OdbcCommand("{call GetSupplierOpeningBalance (?,?,?,?,?)}", cnn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@CompID", OdbcType.SmallInt).Value = Company_ID
                cmd.Parameters.Add("@BranchID", OdbcType.SmallInt).Value = Branch_ID
                cmd.Parameters.Add("@AccountID", OdbcType.SmallInt).Value = Account_ID
                cmd.Parameters.Add("@fromDate", OdbcType.Date).Value = From_Date
                cmd.Parameters.Add("@toDate", OdbcType.Date).Value = To_Date
                'cmd.Parameters.Add("@SMID", OdbcType.SmallInt).Value = SM_ID
                cmd.ExecuteNonQuery()
                cmd.Connection.Close()
            Catch ex As Exception
                ErrMsgBox("Something went Wrong  !")
            End Try
        End Sub
        Public Function GetStock(ByVal CompanyID As Integer, ByVal BranchID As Integer, ByVal ProdID As Integer, ByVal SMID As Integer) As Decimal
            Dim strSQL As String
            Dim iQty As Decimal
            'strSQL = "SELECT dbo.ProductStock.Qty FROM dbo.ProductStock Where MasterCompanyID = " & Val(CompanyID) _
            '    & " AND MasterBranchID = " & Val(BranchID) & " AND ProductID=" & Val(ProdID) _
            '    & " AND SMID = " & Val(SMID)
            strSQL = "SELECT PS.BQty FROM dbo.rptStock PS Where ID=" & Val(ProdID)
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("BQty")) Then
                    iQty = dr.Item("BQty")
                Else
                    iQty = 0
                End If
            Else
                iQty = 0
            End If
            Return iQty
        End Function

        Public Function CheckInStock(ByVal ProdID As Integer) As Boolean
            Dim strSQL As String
            strSQL = "Select ProductID from ProductStock Where ProductID=" & Val(ProdID) _
            & " AND MasterCompanyID = " & Val(MDI.lblMasterCompanyID.Text) _
            & " AND MasterBranchID = " & Val(MDI.lblMasterBranchID.Text)
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                Return False
                Return True
            End If
        End Function
        Public Function GetID(ByVal InvNo As String, ByVal InvDate As String) As Integer
            Dim strSQL As String
            Dim iNum As Integer
            strSQL = "Select ID from SaleHeader Where InvoiceNo=" & SQLquote(InvNo) _
            & " AND InvoiceDate= '" & InvDate & "'"
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(strSQL, cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                iNum = dr.Item(0)
            End If
            Return iNum
        End Function
        Public Function returnDataTable(ByVal strCmd As String) As DataTable
            OpenConn()
            Dim da As New OdbcDataAdapter(strCmd, cnn)
            Dim dt As New System.Data.DataTable()
            da.Fill(dt)
            CloseConn()
            Return dt
        End Function
        'Public Function executeSQL(ByVal cmdtext As String) As Integer
        '    Try
        '        OpenConn()
        '        Dim cmd As New OdbcCommand(cmdtext, cnn)
        '        cmd.Transaction = Tran
        '        Return cmd.ExecuteNonQuery()
        '    Catch e As Exception
        '        Throw e
        '    End Try
        'End Function

        Public Function executeSQL(ByVal cmdtext As String) As Integer
            Try
                OpenConn()
                Dim cmd As New OdbcCommand(cmdtext, cnn)
                Return cmd.ExecuteNonQuery()
                CloseConn()
            Catch e As Exception
                Throw e
            End Try
        End Function
        Public Function returnDataset(ByVal query As String) As DataSet
            OpenConn()
            Dim ds As New DataSet()
            Dim da As New OdbcDataAdapter(query, cnn)
            da.Fill(ds)
            Return ds
            CloseConn()
        End Function
        Public Sub ErrMsgBox(ByVal sString As String)
            MessageBox.Show(sString, msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub
        Public Sub InfoMsgBox(ByVal sString As String)
            MessageBox.Show(sString, msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub
        Public Function validateCombo(ByVal txt As ComboBox) As Boolean
            If Not txt.Items.Contains(txt.Text.ToString) Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Function CheckDuplicate(ByVal sSQL As String) As Boolean
            Dim cmd As Data.Odbc.OdbcCommand
            Dim dr As Data.Odbc.OdbcDataReader
            cmd = New Data.Odbc.OdbcCommand(sSQL, cnn)
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            dr = cmd.ExecuteReader
            If dr.Read Then
                dr.Close()
                cmd.Connection.Close()
                Return True
            Else
                dr.Close()
                cmd.Connection.Close()
                Return False
            End If
        End Function
        Public Sub GetServerDateTime()
            Dim cmdDate As Data.Odbc.OdbcCommand
            Dim drDate As Data.Odbc.OdbcDataReader
            Dim sSQL As String
            sSQL = "Select DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) AS SRVRDATE, LTRIM(RIGHT(CONVERT(VARCHAR(20), GETDATE(), 100), 7)) AS SRVRTIME"
            cmdDate = New Data.Odbc.OdbcCommand(sSQL, cnn)
            If cmdDate.Connection.State = 1 Then cmdDate.Connection.Close()
            cmdDate.Connection.Open()
            cmdDate.Transaction = Tran
            drDate = cmdDate.ExecuteReader
            If drDate.Read Then
                SRVRDate = drDate.Item("SRVRDATE")
                SRVRTIME = drDate.Item("SRVRTIME")
            End If
            drDate.Close()
            'cmdDate.Connection.Close()
        End Sub
        Public Sub GenerateLog(ByVal FormType As String, ByVal Operation As String, ByVal PostID As String, ByVal PostName As String)
            Dim strSQL As String
            GetServerDateTime()
            strSQL = "INSERT INTO tblLog (FormType, Operation, ID, PostName, uID, eDate) VALUES (" & SQLquote(FormType) _
            & "," & SQLquote(Operation) & "," & Val(PostID) & "," & SQLquote(PostName) & "," & Val(UserLoginID) & ",'" & GetDate(SRVRDate) & " " & SRVRTIME & "')"
            executeSQL(strSQL)
        End Sub
        Public Sub UPDATE_MY_STOCK()
            Dim strSQL, strS As String
            Dim iQty, iID, nQty As Integer
            Dim cmdBranch, cmdS As Data.Odbc.OdbcCommand
            Dim drBranch, drS As Data.Odbc.OdbcDataReader

            strSQL = "Select ProductID, Qty from ProductStock"
            cmdBranch = New Data.Odbc.OdbcCommand(strSQL, cnn2)
            If cmdBranch.Connection.State = 1 Then cmdBranch.Connection.Close()
            cmdBranch.Connection.Open()
            drBranch = cmdBranch.ExecuteReader
            Do While drBranch.Read
                iQty = drBranch.Item("Qty") : iID = drBranch.Item("ProductID")
                strS = "Select BQty From rptStock WHERE ID = " & Val(iID)
                cmdS = New Data.Odbc.OdbcCommand(strS, cnn)
                If cmdS.Connection.State = 1 Then cmdS.Connection.Close()
                cmdS.Connection.Open()
                drS = cmdS.ExecuteReader
                If drS.Read Then
                    nQty = drS.Item("BQty")
                End If
                drS.Close() : cmdS.Connection.Close()
                strS = "UPDATE ProductStock SET Qty = " & Val(nQty) & " WHERE ProductID = " & Val(iID)
                executeSQL(strS)
            Loop
            drBranch.Close() : cmdBranch.Connection.Close()
        End Sub
        Public Function IsFormOpen(ByVal frm As Form) As Boolean
            If Application.OpenForms.OfType(Of Form).Contains(frm) Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Sub ShowForm(ByVal f As Form)
            'If IsFormOpen(f) = True Then
            '    f.Close()
            'End If
            f.MinimizeBox = False
            f.MaximizeBox = False
            f.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            f.MdiParent = MDI
            f.ControlBox = False
            f.Dock = DockStyle.Fill
            f.Show()
            ShowOnTop(f)
        End Sub
        Public Sub ShowOnTop(ByVal frm As Form)
            For Each f As Form In MDI.MdiChildren
                If f.Name.Equals(frm.Name) Then
                    f.Activate()
                    Exit For
                End If
            Next
        End Sub
#Region "Bind"
        Public Function BindMonth() As DataTable
            strSQL = "Select MonthID, fldMonth from tblMonths ORDER BY SNo"
            OpenConn()
            Dim da As New OdbcDataAdapter(strSQL, cnn)
            Dim dt As New System.Data.DataTable()
            da.Fill(dt)
            CloseConn()
            Return dt
            da = Nothing : dt = Nothing
        End Function
        Public Function BindEmployee() As DataTable
            strSQL = "Select ID, EmpName from tblEmployee WHERE Active = 1 ORDER BY EmpName"
            OpenConn()
            Dim da As New OdbcDataAdapter(strSQL, cnn)
            Dim dt As New System.Data.DataTable()
            da.Fill(dt)
            CloseConn()
            Return dt
            da = Nothing : dt = Nothing
        End Function
        Public Function GetMonthHoliday(ByVal _BranchID As Integer, ByVal _MonthID As Integer) As Integer
            strSQL = "Select Holiday from tblMonthHoliday Where SessionID=" & Val(_BranchID) & " AND MonthID=" & Val(_MonthID)
            OpenConn()
            Dim cmd As New Data.Odbc.OdbcCommand(strSQL, cnn)
            Dim dr As Data.Odbc.OdbcDataReader
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim iNo As Integer
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item("Holiday")) Then iNo = dr.Item("Holiday") Else iNo = 0
            Else
                iNo = 0
            End If
            dr.Close() : cmd.Connection.Close()
            Return iNo
        End Function
        Public Function GetMonthDays(ByVal _MonthID As Integer) As Integer
            'Dim strSession = MDI.lblSessionName.Text
            Dim Year1, Year2, iYear, iDate As String
            'Year2 = strSession.Substring(7, 4)
            'Year1 = strSession.Substring(0, 4)
            iYear = Year(Date.Today())
            'If Val(_MonthID) > 3 Then
            '    iYear = Year1
            'Else
            '    iYear = Year2
            'End If
            iDate = iYear & "-" & _MonthID & "-01"

            'iii = 12
            ''        strBind = "Declare @date DateTime, @iCount int"
            'Dim tDate As Date = Now.Date
            'Dim iDate As String = tDate.Year & "-" & _MonthID & "-" & tDate.Date

            'SET @date = '2016-02-01'
            strBind = "SELECT DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,'" & iDate & "'),0)))"
            Dim cmd As New Data.Odbc.OdbcCommand(strBind, cnn)
            Dim dr As Data.Odbc.OdbcDataReader
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim iNo As Integer
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item(0)) Then iNo = dr.Item(0) Else iNo = 0
            Else
                iNo = 0
            End If
            dr.Close() : cmd.Connection.Close()
            Return iNo
        End Function
        Public Function GetMonthHoliday(ByVal _MonthID As Integer) As Integer
            strBind = "SELECT Holiday From tblMonthHoliday Where SessionID=" & Val(MDI.lblMasterBranchID.Text) & " AND MonthID=" & Val(_MonthID)
            Dim cmd As New Data.Odbc.OdbcCommand(strBind, cnn)
            Dim dr As Data.Odbc.OdbcDataReader
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim iNo As Integer
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item(0)) Then iNo = dr.Item(0) Else iNo = 0
            Else
                iNo = 0
            End If
            dr.Close() : cmd.Connection.Close()
            Return iNo
        End Function
        Public Function GetPresentDays(ByVal _MonthID As Integer, ByVal _EmpID As Integer) As Double
            strBind = "SELECT Holiday From tblMonthHoliday Where SessionID=" & Val(MDI.lblMasterBranchID.Text) & " AND MonthID=" & Val(_MonthID)
            strBind = "SELECT SUM(iCount) AS iCount FROM (SELECT Status, COUNT(Status) AS iNo, CASE WHEN Status = 'FULL DAY' THEN COUNT(Status) * 1 ELSE CASE WHEN Status = 'HALF DAY' THEN COUNT(Status) * 0.5 ELSE 0 END END AS iCount" _
            & " FROM dbo.tblEmpAttendance WHERE (BranchID = " & Val(MDI.lblMasterBranchID.Text) & ") AND (EmpID = " & Val(_EmpID) & ") AND (MONTH(fldDate) = " & Val(_MonthID) & ") GROUP BY Status) AS t"
            Dim cmd As New Data.Odbc.OdbcCommand(strBind, cnn)
            Dim dr As Data.Odbc.OdbcDataReader
            If cmd.Connection.State = 1 Then cmd.Connection.Close()
            cmd.Connection.Open()
            Dim iNo As Double
            dr = cmd.ExecuteReader
            If dr.Read Then
                If Not IsDBNull(dr.Item(0)) Then iNo = dr.Item(0) Else iNo = 0
            Else
                iNo = 0
            End If
            dr.Close() : cmd.Connection.Close()
            Return iNo
        End Function
#End Region
#Region "INSERT - UPDATE"
        Public Function UpdateMonthHoliday(ByVal _BranchID As Integer, ByVal _MonthID As Integer, ByVal _Holiday As Integer) As Boolean
            strUpdate = "Select MonthID from tblMonthHoliday Where BranchID=" & Val(_BranchID) & " AND MonthID=" & Val(_MonthID)
            If CheckDuplicate(strUpdate) = True Then
                strUpdate = "UPDATE tblMonthHoliday SET Holiday=" & Val(_Holiday) & " Where SessionID=" & Val(_BranchID) & " AND MonthID=" & Val(_MonthID)
            Else
                strUpdate = "INSERT INTO tblMonthHoliday (SessionID, MonthID, Holiday) VALUES (" & Val(_BranchID) & "," & Val(_MonthID) & "," & Val(_Holiday) & ")"
            End If
            executeSQL(strUpdate)
            GenerateLog("MONTH - HOLIDAY", "EDIT", 0, MDI.lblUserLoginID.Text)
            Return True
        End Function
#End Region
#Region "CHECK"
        Public Function CheckInEmpSal(ByVal _BranchID As Integer, ByVal _MonthID As Integer) As Boolean
            strUpdate = "Select ID from tblEmpSalary Where BranchID=" & Val(_BranchID) & " AND SalMonth=" & Val(_MonthID)
            Return CheckDuplicate(strUpdate) = True
        End Function
#End Region
        Public Function IsNumericTextbox(ByVal sender As TextBox, ByVal KeyChar As Char) As Boolean
            'set TRUE: cause a exception when the keychar is not Allowed into vars: allowedChars, allowedOneChar, allowedExceptionChar
            Dim UseThrowDebuggy As Boolean = False

            Dim allowedChars As String = "0123456789"
            Dim allowedOnceChar As Char() = {"."}
            Dim allowedExceptionChar As Keys() = {Keys.Back}

            Dim idxAllowedNotFound As Integer
            Dim idxCountOne As Integer = 0

            idxAllowedNotFound = allowedChars.IndexOf(KeyChar)
            If idxAllowedNotFound = True Then
                'AllowedOnce
                For Each _c As Char In allowedOnceChar
                    If _c = KeyChar Then
                        'Count Check
                        For Each _cc As Char In sender.Text
                            If _c = _cc Then idxCountOne += 1
                        Next
                        If idxCountOne = 0 Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                Next
                'Exceptions
                For i As Integer = 0 To allowedExceptionChar.Count - 1
                    If Asc(KeyChar) = Convert.ToUInt32(allowedExceptionChar(i)) Then Return False
                Next
                'Not Throw
                If UseThrowDebuggy = False Then
                    If Char.IsNumber(KeyChar) Then
                        Return False
                    Else
                        Return True
                    End If
                End If
                'Outside to end for throw
            Else
                'AllowedChars
                Return False
            End If

            Dim _kc As String = ControlChars.NewLine & "Char: " & KeyChar & ControlChars.NewLine & "Asc: " & Asc(KeyChar) & ControlChars.NewLine
            Throw New Exception("UseThrowDebuggy found a unknow KeyChar: " & _kc)
        End Function
    End Class
End Namespace
