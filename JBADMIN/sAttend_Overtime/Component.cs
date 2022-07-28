using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace sAttend_Overtime
{
    public partial class Component : DataModule
    {
        public Component()
        {
            InitializeComponent();
        }

        public Component(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public object[] checkOvertimeData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string overtimeID = parm[0];
            string employeeID = parm[1];
            string overtimeDate = parm[2];
            string beginTime = parm[3];
            string endTime = parm[4];

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) AS cnt from HRM_ATTEND_OVERTIME_DATA " + "\r\n";
                sql = sql + " where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + " and OVERTIME_DATE = '" + overtimeDate + "'" + "\r\n";
                sql = sql + " and BEGIN_TIME < '" + endTime + "'" + "\r\n";
                sql = sql + " and END_TIME > '" + beginTime + "'" + "\r\n";
                if (overtimeID != "0")
                    sql = sql + " and OVERTIME_ID <> " + overtimeID;

                DataSet dsHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBHR_EEP", connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] checkOvertimeHours(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string overtimeID = parm[0];
            string employeeID = parm[1];
            string overtimeDate = parm[2];
            string beginTime = parm[3];
            string endTime = parm[4];

            string on_time = "";
            string off_time = "";
            string otBeginTime = "";
            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";
            string rejectCode = "";
            DateTime beginOvertimeDate, restBeginTime, otBeginTimeDate;
            DateTime endOvertimeDate, restEndTime, roteBeginTime, roteEndTime;
            decimal hours, minutes, totalHours;

            TimeSpan ts;
            string roteCode = "";
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays'" + "\r\n";
                DataTable dtRoteMappingDetail = this.ExecuteSql(sql, connection, transaction).Tables[0];
                dtRoteMappingDetail.PrimaryKey = new DataColumn[] { dtRoteMappingDetail.Columns["ROTE_ID"] };

                sql = "select A.ROTE_ID," + "\r\n";
                sql = sql + "C.ROTE_CODE," + "\r\n";
                sql = sql + "C.ON_TIME," + "\r\n";
                sql = sql + "C.OFF_TIME," + "\r\n";
                sql = sql + "C.OT_BEGIN_TIME," + "\r\n";
                sql = sql + "C.IS_CARD," + "\r\n";
                sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
                sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
                sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME," + "\r\n";
                sql = sql + "B.ON_TIME AS CARD_ON_TIME," + "\r\n";
                sql = sql + "B.OFF_TIME AS CARD_OFF_TIME," + "\r\n";
                sql = sql + "B.CARD_DATE_TIME_ON," + "\r\n";
                sql = sql + "B.CARD_DATE_TIME_OFF," + "\r\n";
                sql = sql + "B.CARD_DATE_TIME_ON_TRAN," + "\r\n";
                sql = sql + "B.CARD_DATE_TIME_OFF_TRAN" + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ATTEND_CARD B on A.EMPLOYEE_ID = B.EMPLOYEE_ID and A.ATTEND_DATE = B.CARD_DATE" + "\r\n"; ;
                sql = sql + "left join HRM_ATTEND_ROTE C on A.ROTE_ID = C.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE = '" + overtimeDate + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_ATTEND.Tables[0].Rows.Count != 0)
                {
                    roteCode = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString();
                    rote_on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                    rote_off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();

                    if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                    {
                        roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString();
                        on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                        off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();
                        otBeginTime = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OT_BEGIN_TIME"].ToString();
                    }
                    else
                    {
                        roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ROTE_ID"].ToString();
                        on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ON_TIME"].ToString();
                        off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_OFF_TIME"].ToString();
                    }
                    //判斷考勤群組參數ComputingCard是否須刷卡為 1 時才需要判斷刷卡資料
                    sql = "SELECT * FROM [dbo].[dt_Attend_GroupParameterValue]('" + employeeID + "','" + overtimeDate + "') where GROUP_PARAMETER_CODE = 'ComputingCard'" + "\r\n";
                    DataSet dsGROUP_PARAMETER = this.ExecuteSql(sql, connection, transaction);
                    dsGROUP_PARAMETER.Dispose();

                    if (dsGROUP_PARAMETER.Tables[0].Rows[0]["ParameterValue"].ToString() == "1")
                    {
                        if (Convert.IsDBNull(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["CARD_ON_TIME"]) && Convert.IsDBNull(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["CARD_OFF_TIME"]))
                            //rejectCode = "";
                            rejectCode = "1";   //申請日期查無出勤刷卡資料
                        else if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["CARD_ON_TIME"].ToString().Length == 0 || dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["CARD_OFF_TIME"].ToString().Length == 0)
                            rejectCode = "2";   //申請日期出勤刷卡資料不完整
                        else if (!(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["CARD_ON_TIME"].ToString().CompareTo(beginTime) <= 0 && dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["CARD_OFF_TIME"].ToString().CompareTo(endTime) >= 0))
                            rejectCode = "3";   //申請時間未在刷卡時段內
                    }
                }
                else
                    rejectCode = "4";   //申請日期尚未產生員工班表

                //判斷若無錯誤訊息--計算加班時數
                if (rejectCode != "4")
                {
                    beginOvertimeDate = DateAndTimeMerger(overtimeDate, beginTime);
                    endOvertimeDate = DateAndTimeMerger(overtimeDate, endTime);
                    otBeginTimeDate = DateAndTimeMerger(overtimeDate, otBeginTime);
                    aOverTime.Begin = beginOvertimeDate;
                    aOverTime.End = endOvertimeDate;

                    //計算正常上班時間
                    //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString());
                    if (drRoteMappingDetail == null) 
                    {
                        roteBeginTime = DateAndTimeMerger(overtimeDate, rote_on_time);
                        roteEndTime = DateAndTimeMerger(overtimeDate, rote_off_time);

                        if (!(beginOvertimeDate >= otBeginTimeDate))
                            rejectCode = "5";   //加班起始時間未在合理時間範圍內
                        else if (!(endOvertimeDate <= roteBeginTime.AddDays(1)))
                            rejectCode = "6";   //加班截止時間未在合理時間範圍內

                        if ((beginOvertimeDate < roteEndTime) && (endOvertimeDate > roteBeginTime))
                        {
                            roteBeginTime = ((beginOvertimeDate <= roteBeginTime) && (endOvertimeDate >= roteBeginTime)) ? roteBeginTime : beginOvertimeDate;
                            roteEndTime = ((beginOvertimeDate <= roteEndTime) && (endOvertimeDate >= roteEndTime)) ? roteEndTime : endOvertimeDate;
                            ts = roteEndTime - roteBeginTime;

                            if (Convert.ToDecimal(ts.TotalMinutes) > 0)
                            {
                                Reduce.Add(new TheTimeRange { Begin = roteBeginTime, End = roteEndTime });
                            }
                        }
                    }

                    string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                    restSql = restSql + "where ROTE_ID = " + roteID;
                    DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
                    foreach (DataRow Row1 in dtRest.Rows)
                    {
                        restBeginTime = DateAndTimeMerger(overtimeDate, Row1["REST_BEGIN_TIME"].ToString());
                        restEndTime = DateAndTimeMerger(overtimeDate, Row1["REST_END_TIME"].ToString());
                        ts = restEndTime - restBeginTime;

                        //if (roteCode == "00" && Row1["IS_HOLIDAY_OVERTIME"].ToString() == "Y")   //假日是否參考
                        if (drRoteMappingDetail != null && Row1["IS_HOLIDAY_OVERTIME"].ToString() == "Y")   //假日是否參考
                        {
                            Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                        }
                        //else if (roteCode != "00" && Row1["IS_NORMAL_OVERTIME"].ToString() == "Y")   //平日是否參考	5
                        else if (drRoteMappingDetail == null && Row1["IS_NORMAL_OVERTIME"].ToString() == "Y")   //平日是否參考	5
                        {
                            //restHours = restHours + Convert.ToDecimal(ts.TotalHours);
                            //restMinutes = restMinutes + Convert.ToDecimal(ts.TotalMinutes);
                            Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                        }
                    }

                    //ts = endOvertimeDate - beginOvertimeDate;
                    //overtimeHours = Convert.ToDecimal(ts.TotalHours);
                    //overtimeMinutes = Convert.ToDecimal(ts.TotalMinutes);

                    //int iMin = Convert.ToInt32((overtimeMinutes - restMinutes) % 30);
                    //minutes = ((overtimeMinutes - restMinutes) - iMin);
                    //hours = minutes / 60;
                    //totalHours = (overtimeMinutes - Convert.ToInt32(overtimeMinutes % 30)) / 60;

                    //minutes = minutes >= 30 ? minutes : 0;
                    //hours = Convert.ToDouble(hours) >= 0.5 ? hours : 0;


                    //計算加班時間
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    //var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks - aOverTime.Begin.Ticks).TotalMinutes;

                    int iMin = Convert.ToInt32((TotalMinute - ReduceMinute) % 30);
                    minutes = ((TotalMinute - ReduceMinute) - iMin);
                    hours = minutes / 60;
                    totalHours = (TotalMinute - Convert.ToInt32(TotalMinute % 30)) / 60;
                    minutes = minutes >= 30 ? minutes : 0;
                    hours = Convert.ToDouble(hours) >= 0.5 ? hours : 0;

                    sql = "select '" + rejectCode + "' as rejectCode," + hours + " as hours," + totalHours + " as totalHours";
                }
                else
                {
                    sql = "select '" + rejectCode + "' as rejectCode, 0 as hours, 0 as totalHours";
                }

                DataSet dsHours = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                js = JsonConvert.SerializeObject(dsHours.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBHR_EEP", connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }
        //加班日期 OnBlur 設定有效日期 && 計薪年月 && 
        public object[] getSalaryYYMM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select distinct case when day(ATTEND_DATE) > (SELECT ATTEND_CLOSE_DAY FROM HRM_SYSTEM_SALARY_CONFIG ";
                sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'COMPANY_ID')) then LEFT(CONVERT(varchar,dateadd(mm,1,ATTEND_DATE),112),6) else LEFT(CONVERT(varchar,ATTEND_DATE,112),6) end as SALARY_YYMM " + "\r\n";
                sql = sql + "from HRM_ATTEND_DATA_LOCK" + "\r\n";
                sql = sql + "where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'GROUP_ID')" + "\r\n";
                sql = sql + "and ATTEND_DATE >= '" + overtimeDate + "'" + "\r\n";
                sql = sql + " ORDER BY SALARY_YYMM DESC";

                DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);

                sql = "select case when day(convert(datetime,'" + overtimeDate + "')) > (select ATTEND_CLOSE_DAY from HRM_SYSTEM_SALARY_CONFIG ";
                sql = sql + " where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",convert(datetime,'" + overtimeDate + "'),'COMPANY_ID')) then left(convert(varchar,dateadd(mm,1,convert(datetime,'" + overtimeDate + "')),112),6)";
                sql = sql + " else left(convert(varchar,convert(datetime,'" + overtimeDate + "'),112),6) end as SALARY_YYMM" + "\r\n";
                sql = sql + ",dbo.funReturnDeptInfo(DEPTC_ID,2) AS DEPTC_CODE From [dtHRM_BaseAndBasetts_Employed](GetDate()) WHERE (EMPLOYEE_CODE = '" + employeeID + "')" + "\r\n";



                DataSet dsOvertimeDate = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows.Count != 0)
                {
                    if (Int32.Parse(dsOvertimeDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString()) > Int32.Parse(dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["SALARY_YYMM"].ToString()))
                    {
                        //Indented縮排 將資料轉換成Json格式
                        js = JsonConvert.SerializeObject(dsOvertimeDate.Tables[0], Formatting.Indented);
                    }
                    else
                    {
                        sql = sql + "select LEFT(CONVERT(varchar,dateadd(mm,1,convert(datetime,'" + dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["SALARY_YYMM"].ToString() + "'+'01')),112),6) as SALARY_YYMM";
                        DataSet dsOvertimeDateAdd = this.ExecuteSql(sql, connection, transaction);
                        js = JsonConvert.SerializeObject(dsOvertimeDateAdd.Tables[0], Formatting.Indented);
                    }
                }
                else
                {
                    //Indented縮排 將資料轉換成Json格式
                    js = JsonConvert.SerializeObject(dsOvertimeDate.Tables[0], Formatting.Indented);
                }

                transaction.Commit();
                dsHRM_ATTEND_DATA_LOCK.Dispose();
                dsOvertimeDate.Dispose();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBHR_EEP", connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }
        //加班班別 && 刷卡轉出勤上下班時間
        public object[] getAttendCard(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select A.ROTE_ID,C.ROTE_CODE,D.ROTE_ID AS UPPER_ROTE_ID,D.ROTE_CODE AS UPPER_ROTE_CODE,B.ON_TIME_TRAN,B.OFF_TIME_TRAN" + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ATTEND_CARD B on A.EMPLOYEE_ID = B.EMPLOYEE_ID and A.ATTEND_DATE = B.CARD_DATE" + "\r\n"; ;
                sql = sql + "left join HRM_ATTEND_ROTE C on A.ROTE_ID = C.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE = '" + overtimeDate + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(dsHRM_ATTEND_ATTEND.Tables[0], Formatting.Indented);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBHR_EEP", connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object flowCheckHours(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            //string Today = System.DateTime.Today.ToShortDateString(); //取得今天日期
            decimal upperManagerHour = 0;
            decimal overtimeHours = decimal.Parse(dr["OVERTIME_HOURS"].ToString()); // 取得加班時數
            decimal restHours = decimal.Parse(dr["REST_HOURS"].ToString()); // 取得補休時數
            string employeeID = dr["EMPLOYEE_ID"].ToString(); // 取得補休時數

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select UPPER_MANAGER_HOUR,* from HRM_SYSTEM_OVERTIME_CONFIG " + "\n\r";
                sql = sql + "where COMPANY_ID = (select dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "',GETDATE(),'COMPANY_ID'))";
                this.ExecuteCommand(sql, connection, transaction); // 送出SQL語句
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                if (ds.Tables[0].Rows[0]["UPPER_MANAGER_HOUR"] != DBNull.Value)
                    upperManagerHour = decimal.Parse(ds.Tables[0].Rows[0]["UPPER_MANAGER_HOUR"].ToString());

                transaction.Commit();
                if (overtimeHours + restHours > upperManagerHour)
                    ret[1] = true;
                else
                    ret[1] = false;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBHR_EEP", connection);
            }
            return ret; // 傳回值: 無
        }

        public object[] checkCompensatoryData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string overtimeID = parm[0];
            string employeeID = parm[1];
            string overtimeDate = parm[2];
            string o_overtimeDate = parm[3];
            string restHours = parm[4];
            string o_restHours = parm[5];
            string editMode = parm[6];
            string sql = "";
            bool flag = true;
            string holidayID;

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select * from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_NO = " + overtimeID;
                DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction).Tables[0];
                if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                {
                    if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                    {
                        sql = "select COMPENSATORY_SALARY_ID from HRM_SYSTEM_ATTEND_CONFIG " + "\r\n";
                        sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + overtimeDate + "','COMPANY_ID')" + "\r\n";
                        DataSet dsHRM_SYSTEM_ATTEND_CONFIG = this.ExecuteSql(sql, connection, transaction);
                        holidayID = dsHRM_SYSTEM_ATTEND_CONFIG.Tables[0].Rows[0]["COMPENSATORY_SALARY_ID"].ToString();
                        dsHRM_SYSTEM_ATTEND_CONFIG.Dispose();

                        //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                        sql = "select * from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID <> ABSENT_MINUS_ID and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString();
                        DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, connection, transaction).Tables[0];

                        //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                        sql = "";
                        foreach (DataRow Row in dtHRM_ATTEND_ABSENT_MINUS_DETAIL.Rows)
                        {
                            var hours = decimal.Parse(Row["ABSENT_HOURS"].ToString());
                            var absentMinusID = Row["ABSENT_MINUS_ID"].ToString();
                            DateTime absentDate = Convert.ToDateTime(Row["ABSENT_DATE"].ToString()).Date;

                            sql = "select * from  HRM_ATTEND_ABSENT_MINUS where ABSENT_MINUS_ID = " + absentMinusID;
                            DataTable dtHRM_ATTEND_ABSENT_MINUS = this.ExecuteSql(sql, connection, transaction).Tables[0];
                            var beginDate = dtHRM_ATTEND_ABSENT_MINUS.Rows[0]["BEGIN_DATE"].ToString();
                            var endDate = dtHRM_ATTEND_ABSENT_MINUS.Rows[0]["END_DATE"].ToString();

                            //得假資料檔得假剩餘時數(REST_HOURS)
                            sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
                            sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                            sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                            sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                            sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                            sql = sql + "and REST_HOURS >0" + "\r\n";
                            //if (editMode == "deleted")
                            sql = sql + "and A.ABSENT_PLUS_ID <> " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                            sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
                            sql = sql + "order by A.BEGIN_DATE,A.END_DATE DESC" + "\r\n";

                            DataSet drABSENT_PLUS = this.ExecuteSql(sql, connection, transaction);

                            foreach (DataRow drRestHours in drABSENT_PLUS.Tables[0].Rows)
                            {
                                string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                                if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                                {
                                    hours = 0;
                                    break;
                                }
                                else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                                    hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                            }

                            //判斷補休時數不夠扣
                            if (editMode == "updated")
                            {
                                if (hours - decimal.Parse(restHours) > 0)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (hours > 0)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                //js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBHR_EEP", connection);
            }
            //return new object[] { 0, js };
            return new object[] { 0, flag };
        }

        private List<TheTimeRange> RangeCheck(List<TheTimeRange> RangeList)
        {
            var OrderList = RangeList.OrderBy(m => m.Begin).ToList();


            if (OrderList.Count == 0) return new List<TheTimeRange>();


            DateTime DateTimeTemp = OrderList.First().Begin;
            for (int i = 0; i < OrderList.Count; i++)
            {
                if (DateTimeTemp > OrderList[i].Begin) OrderList[i].Begin = DateTimeTemp;
                else DateTimeTemp = OrderList[i].Begin;


                if (DateTimeTemp > OrderList[i].End) OrderList[i].End = DateTimeTemp;
                else DateTimeTemp = OrderList[i].End;
            }


            return OrderList.Where(m => m.End > m.Begin).ToList();
        }

        private int MinuteOfRange(List<TheTimeRange> ReduceList, TheTimeRange aRange)
        {
            int Minute = 0;
            foreach (var aRedrce in ReduceList) Minute += MinuteOfRange(aRedrce, aRange);
            return Minute;
        }

        //Rang2在Rang1中所佔的分鐘數
        private int MinuteOfRange(TheTimeRange Range1, TheTimeRange Range2)
        {
            if (Range2.End <= Range1.Begin) return 0;
            else if (Range2.Begin >= Range1.End) return 0;


            DateTime Begin = new DateTime();
            DateTime End = new DateTime();


            if (Range1.Begin >= Range2.Begin) Begin = Range1.Begin;
            else Begin = Range2.Begin;


            if (Range1.End <= Range2.End) End = Range1.End;
            else End = Range2.End;

            return (int)new TimeSpan(End.Ticks).Subtract(new TimeSpan(Begin.Ticks)).Duration().TotalMinutes;
        }

        private DateTime DateAndTimeMerger(object CARD_DATE, object CARD_TIME)
        {
            string aDate = CARD_DATE.ToString().Trim();
            //if (aDate.Length == 0) return null;

            DateTime DateTemp = DateTime.Now;
            //if (!DateTime.TryParse(aDate, out DateTemp)) return null;
            //else
            //    DateTemp = new DateTime(DateTemp.Year, DateTemp.Month, DateTemp.Day);

            DateTemp = DateTime.Parse(aDate);
            string aTime = CARD_TIME.ToString().Trim();
            if (aDate.Length == 0) return DateTemp;

            String pattern = @"^([0-3]\d|4[0-8])([0-5]\d)$";
            if (!Regex.IsMatch(aTime, pattern)) return DateTemp;

            DateTemp = DateTemp.AddHours(int.Parse(aTime.Substring(0, 2)));
            DateTemp = DateTemp.AddMinutes(int.Parse(aTime.Substring(2, 2)));
            return DateTemp;
        }

        private class TheTimeRange
        {
            public DateTime Begin { get; set; }
            public DateTime End { get; set; }
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            string detailSql = "";
            string AbsentPlusSql = "";
            bool flag = true;
            var overtimeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("OVERTIME_ID");
            var overtimeDate = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_DATE");
            var overtimeEffectDate = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_EFFECT_DATE");
            var beginTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("BEGIN_TIME");
            var endTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("END_TIME");
            decimal overtimeHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_HOURS").ToString());
            decimal restHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("REST_HOURS").ToString());
            var employeeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("EMPLOYEE_ID");

            decimal o_restHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("REST_HOURS").ToString());
            var o_overtimeDate = Convert.ToDateTime(ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("OVERTIME_DATE").ToString()).Date;

            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("OVERTIME_DATE_TIME_BEGIN", DateAndTimeMerger(overtimeDate, beginTime));
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("OVERTIME_DATE_TIME_END", DateAndTimeMerger(overtimeDate, endTime));
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("TOTAL_HOURS", overtimeHours + restHours);

            if (restHours != o_restHours)
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
                dsUSERS.Dispose();

                sql = "select * from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_NO = " + overtimeID;
                DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                sql = "select COMPENSATORY_SALARY_ID from HRM_SYSTEM_ATTEND_CONFIG " + "\r\n";
                sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + Convert.ToDateTime(overtimeDate.ToString()).ToShortDateString() + "','COMPANY_ID')" + "\r\n";
                DataSet dsHRM_SYSTEM_ATTEND_CONFIG = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                int holidayID = int.Parse(dsHRM_SYSTEM_ATTEND_CONFIG.Tables[0].Rows[0]["COMPENSATORY_SALARY_ID"].ToString());
                dsHRM_SYSTEM_ATTEND_CONFIG.Dispose();

                if (restHours > o_restHours)
                {
                    if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                    {
                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set TOTAL_HOURS = " + restHours + ",REST_HOURS = " + restHours + " - ABSENT_HOURS,";
                        detailSql = detailSql + "BEGIN_DATE = '" + Convert.ToDateTime(overtimeDate.ToString()).ToShortDateString() + "', END_DATE = '" + Convert.ToDateTime(overtimeEffectDate.ToString()).ToShortDateString() + "'";
                        detailSql = detailSql + " where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                    }
                    else
                    {
                        AbsentPlusSql = createAbsentPlusSql(userName, overtimeID.ToString(), employeeID.ToString(), Convert.ToDateTime(overtimeDate.ToString()).Date, Convert.ToDateTime(overtimeEffectDate.ToString()).Date, beginTime.ToString(), endTime.ToString(), restHours, holidayID);
                        detailSql = detailSql + AbsentPlusSql;
                    }
                }
                else
                {
                    if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                    {

                        if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                        {
                            //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                            sql = "select * from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID <> ABSENT_MINUS_ID and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString();
                            DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                            //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                            sql = "";
                            detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_NO = " + overtimeID + "\r\n";
                            detailSql = detailSql + "delete from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";

                            AbsentPlusSql = createAbsentPlusSql(userName, overtimeID.ToString(), employeeID.ToString(), Convert.ToDateTime(overtimeDate.ToString()).Date, Convert.ToDateTime(overtimeEffectDate.ToString()).Date, beginTime.ToString(), endTime.ToString(), restHours, holidayID);
                            detailSql = detailSql + AbsentPlusSql;

                            this.ExecuteSql(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);

                            detailSql = "";
                            foreach (DataRow Row in dtHRM_ATTEND_ABSENT_MINUS_DETAIL.Rows)
                            {
                                var hours = decimal.Parse(Row["ABSENT_HOURS"].ToString());
                                var absentMinusID = Row["ABSENT_MINUS_ID"].ToString();
                                DateTime absentDate = Convert.ToDateTime(Row["ABSENT_DATE"].ToString()).Date;

                                sql = "select * from  HRM_ATTEND_ABSENT_MINUS where ABSENT_MINUS_ID = " + absentMinusID;
                                DataTable dtHRM_ATTEND_ABSENT_MINUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];
                                var beginDate = dtHRM_ATTEND_ABSENT_MINUS.Rows[0]["BEGIN_DATE"].ToString();
                                var endDate = dtHRM_ATTEND_ABSENT_MINUS.Rows[0]["END_DATE"].ToString();

                                //得假資料檔得假剩餘時數(REST_HOURS)
                                sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
                                sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                                sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                                sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                                sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                                sql = sql + "and REST_HOURS >0" + "\r\n";
                                sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
                                sql = sql + "order by A.BEGIN_DATE,A.END_DATE DESC" + "\r\n";

                                DataSet drABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);

                                foreach (DataRow drRestHours in drABSENT_PLUS.Tables[0].Rows)
                                {
                                    string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();

                                    if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                                    {
                                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                        detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                        detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                        drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                                        hours = 0;
                                        break;
                                    }
                                    else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                                    {
                                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS - " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                        detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                        detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + drRestHours["REST_HOURS"].ToString() + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                        hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                                        drRestHours["REST_HOURS"] = 0;
                                    }
                                }

                                //判斷補休時數不夠扣
                                if (hours > 0)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (!flag)
                            //出現錯誤訊息 ROLLBACK
                            this.ExecuteSql("update HRM_ATTEND_OVERTIME_DATA ERROR", ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    }
                }   //restHours < o_restHours
                this.ExecuteSql(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            }   //restHours != o_restHours
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            var overtimeDate = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_DATE");
            var beginTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("BEGIN_TIME");
            var endTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("END_TIME");
            decimal overtimeHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_HOURS").ToString());
            decimal restHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("REST_HOURS").ToString());
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("OVERTIME_DATE_TIME_BEGIN", DateAndTimeMerger(overtimeDate, beginTime));
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("OVERTIME_DATE_TIME_END", DateAndTimeMerger(overtimeDate, endTime));
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("TOTAL_HOURS", overtimeHours + restHours);
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string AbsentPlusSql = "";
            int holidayID;
            string employeeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("EMPLOYEE_ID").ToString();
            DateTime overtimeDate = Convert.ToDateTime(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_DATE").ToString()).Date;
            string beginTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("BEGIN_TIME").ToString();
            string endTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("END_TIME").ToString();
            decimal restHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("REST_HOURS").ToString());
            DateTime overtimeEffectDate = Convert.ToDateTime(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_EFFECT_DATE").ToString()).Date;

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            sql = "select COMPENSATORY_SALARY_ID from HRM_SYSTEM_ATTEND_CONFIG " + "\r\n";
            sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + overtimeDate.ToString("yyyy-MM-dd") + "','COMPANY_ID')" + "\r\n";
            DataSet dsHRM_SYSTEM_ATTEND_CONFIG = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            holidayID = int.Parse(dsHRM_SYSTEM_ATTEND_CONFIG.Tables[0].Rows[0]["COMPENSATORY_SALARY_ID"].ToString());
            dsHRM_SYSTEM_ATTEND_CONFIG.Dispose();

            var command = ucHRM_ATTEND_OVERTIME_DATA.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucHRM_ATTEND_OVERTIME_DATA.trans;
            int newID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            var table = (string)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            DataTable dt = ucHRM_ATTEND_OVERTIME_DATA.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                if (dataset.Tables[table].Rows[i].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[i]["OVERTIME_ID"] = newID;
                    dataset.Tables[table].Rows[i]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[i]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[i]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            //判斷補休時數大於0, 需產生一筆新的補休得假資料
            if (restHours > 0)
            {
                AbsentPlusSql = createAbsentPlusSql(userName, newID.ToString(), employeeID, overtimeDate, overtimeEffectDate, beginTime, endTime, restHours, holidayID);
                this.ExecuteSql(AbsentPlusSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            }

        }

        private void ucHRM_ATTEND_OVERTIME_DATA_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            var table = (string)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            DataTable dt = ucHRM_ATTEND_OVERTIME_DATA.GetSchema();

            //for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            //{
            //    logInfo_HRM_ATTEND_OVERTIME_DATA.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans, ucHRM_ATTEND_OVERTIME_DATA.SelectCmd.KeyFields);
            //}
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            var table = (string)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            DataTable dt = ucHRM_ATTEND_OVERTIME_DATA.GetSchema();

            //for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            //{
            //    logInfo_HRM_ATTEND_OVERTIME_DATA.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans, ucHRM_ATTEND_OVERTIME_DATA.SelectCmd.KeyFields);
            //}
        }

        private string createAbsentPlusSql(string userName, string newID, string employeeID, DateTime overtimeDate, DateTime overtimeEffectDate, string beginTime, string endTime, decimal restHours, int holidayID)
        {
            string detailSql = "";
            string sql = "";

            if (restHours > 0)
            {
                detailSql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n"; ;
                detailSql = detailSql + "select '" + employeeID + "','" + overtimeDate.ToShortDateString() + "','" + overtimeEffectDate.ToShortDateString() + "'," + holidayID + "," + restHours + ",null,0," + restHours + ",''," + newID + ",'Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                detailSql = detailSql + "declare @absentPlusID int " + "\r\n";
                detailSql = detailSql + "set @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                detailSql = detailSql + "select @absentPlusID,@absentPlusID, null, " + restHours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
            }

            return detailSql;
        }


        //輸入驗證
        private TheDictionaryCheck FileUploadFormValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "FilePathName", DisplayName = "檔案路徑" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "員工編號" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_DATE", DisplayName = "加班日期" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_TIME", DisplayName = "加班起始時間" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_TIME", DisplayName = "加班截止時間" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_HOURS", DisplayName = "加班時數" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "REST_HOURS", DisplayName = "補休時數" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "TOTAL_HOURS", DisplayName = "加班總時數" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SALARY_YYMM", DisplayName = "計薪年月" });

            aCheckDictionary.SetFieldValue(InputContent);
            aCheckDictionary.DoCheck();
            return aCheckDictionary;
        }

        //取得ColumnName
        private Dictionary<string, int> FileUploadGetHeadList(Dictionary<string, object> Parameter_Input)
        {
            Dictionary<string, int> aHeadList = new Dictionary<string, int>();

            foreach (var aProperty in typeof(ImportData).GetProperties())
            {
                if (Parameter_Input[aProperty.Name].ToString() != "")
                    aHeadList[aProperty.Name] = Convert.ToInt32(Parameter_Input[aProperty.Name]);
            }
            return aHeadList;
        }

        //--------------------------------------欄位驗證(順便欄位轉換)----------------------------
        private bool DataTableCellValidate(DataTable aTable)
        {
            try
            {
                var ValidData = GetValidData();

                TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "員工編號", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_DATE", DisplayName = "加班日期", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_TIME", DisplayName = "加班起始時間", IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Time48Hours, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_TIME", DisplayName = "加班截止時間", IsRequired = true, SystemCheckType = TheDictionaryCheckType.Time48Hours, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_HOURS", DisplayName = "加班時數", IsRequired = true, IsUserCheck = false, SystemCheckType = TheDictionaryCheckType.Decimal });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "REST_HOURS", DisplayName = "補休時數", IsRequired = true, IsUserCheck = false, SystemCheckType = TheDictionaryCheckType.Decimal });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "TOTAL_HOURS", DisplayName = "加班總時數", IsRequired = true, SystemCheckType = TheDictionaryCheckType.Decimal, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_CAUSE_ID", DisplayName = "加班原因代碼", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_ROTE_ID", DisplayName = "加班班別代碼", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_DEPT_ID", DisplayName = "加班部門代碼", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_EFFECT_DATE", DisplayName = "加班有效日期", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SALARY_YYMM", DisplayName = "計薪年月", IsRequired = true, SystemCheckType = TheDictionaryCheckType.YearMonth, IsUserCheck = false });

                aCheckDictionary.CheckUserDefined += delegate(TheDictionaryCheckData aCheckData)
                {
                    aCheckData.IsOK = false;
                    if (aCheckData.FieldName == "EMPLOYEE_ID")
                    {
                        var aEmployeeData = ValidData.EmployeeData.FirstOrDefault(m => m.Code == aCheckData.TheValue.ToString());
                        if (aEmployeeData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aEmployeeData.ID;
                        }
                        else aCheckData.ErrorMsg = string.Format("【{0}】找不到對應", aCheckData.DisplayName);
                    }
                    else if (aCheckData.FieldName == "OVERTIME_CAUSE_ID")
                    {
                        var aOvertimeCauseData = ValidData.OverTimeCauseData.FirstOrDefault(m => m.Code == aCheckData.TheValue.ToString());
                        if (aOvertimeCauseData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aOvertimeCauseData.ID;
                        }
                        else
                        {
                            aOvertimeCauseData = ValidData.OverTimeCauseData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                            if (aOvertimeCauseData != null)
                            {
                                aCheckData.ErrorMsg = "";
                                aCheckData.IsOK = true;
                                aCheckData.TheResult = aOvertimeCauseData.ID;
                            }
                            else
                                aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應資料", aCheckData.DisplayName);
                        }
                    }
                    else if (aCheckData.FieldName == "OVERTIME_ROTE_ID")
                    {
                        var aRoteData = ValidData.RoteData.FirstOrDefault(m => m.Code == aCheckData.TheValue.ToString());
                        if (aRoteData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aRoteData.ID;
                        }
                        else
                        {
                            aRoteData = ValidData.RoteData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                            if (aRoteData != null)
                            {
                                aCheckData.ErrorMsg = "";
                                aCheckData.IsOK = true;
                                aCheckData.TheResult = aRoteData.ID;
                            }
                            else
                                aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應資料", aCheckData.DisplayName);
                        }
                    }
                    else if (aCheckData.FieldName == "OVERTIME_DEPT_ID")
                    {
                        var aDeptData = ValidData.DeptData.FirstOrDefault(m => m.Code == aCheckData.TheValue.ToString());
                        if (aDeptData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aDeptData.ID;
                        }
                        else
                        {
                            aDeptData = ValidData.DeptData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                            if (aDeptData != null)
                            {
                                aCheckData.ErrorMsg = "";
                                aCheckData.IsOK = true;
                                aCheckData.TheResult = aDeptData.ID;
                            }
                            else
                                aCheckData.ErrorMsg = string.Format("【{0}】沒有相對應資料", aCheckData.DisplayName);
                        }
                    }
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    Dictionary<string, object> InputContent = new Dictionary<string, object>();
                    foreach (DataColumn theColumn in aTable.Columns)
                    {
                        InputContent.Add(theColumn.Caption, aRow[theColumn.Caption]);
                    }
                    aCheckDictionary.SetFieldValue(InputContent);
                    aCheckDictionary.DoCheck();

                    aCheckDictionary.CheckData.ForEach(delegate(TheDictionaryCheckData aCheckData)
                    {
                        if (aCheckData.TheValue != null)
                        {
                            if (!aCheckData.IsOK)
                                aRow.SetColumnError(aCheckData.FieldName, aCheckData.ErrorMsg);
                            else
                                aRow[aCheckData.FieldName] = aCheckData.TheResult;
                        }
                    });
                    aRow.RowError = aRow.HasErrors ? "驗證錯誤" : "";
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //取的欄位驗證個相關資料
        private DataTableCellValidateData GetValidData()
        {
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            DataTableCellValidateData aAns = new DataTableCellValidateData();

            string sql = "select EMPLOYEE_ID, EMPLOYEE_CODE, NAME_C from HRM_BASE_BASE" + "\n\r";
            sql = sql + "select ROTE_ID,ROTE_CODE,ROTE_CNAME FROM HRM_ATTEND_ROTE" + "\n\r";
            sql = sql + "select OVERTIME_CAUSE_ID,OVERTIME_CAUSE_CODE,OVERTIME_CAUSE_CNAME FROM HRM_ATTEND_OVERTIME_CAUSE" + "\n\r";
            sql = sql + "select DEPT_ID,DEPT_CODE,DEPT_CNAME FROM HRM_DEPT" + "\n\r";

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.EmployeeData = DataSet.Tables[0].AsEnumerable().Select(m => new EmployeeData { ID = m.Field<string>("EMPLOYEE_ID"), Code = m.Field<string>("EMPLOYEE_CODE"), Name = m.Field<string>("NAME_C") }).ToList();
            aAns.RoteData = DataSet.Tables[1].AsEnumerable().Select(m => new RoteData { ID = m.Field<int>("ROTE_ID"), Code = m.Field<string>("ROTE_CODE"), Name = m.Field<string>("ROTE_CNAME") }).ToList();
            aAns.OverTimeCauseData = DataSet.Tables[2].AsEnumerable().Select(m => new OverTimeCauseData { ID = m.Field<int>("OVERTIME_CAUSE_ID"), Code = m.Field<string>("OVERTIME_CAUSE_CODE"), Name = m.Field<string>("OVERTIME_CAUSE_CNAME") }).ToList();
            aAns.DeptData = DataSet.Tables[3].AsEnumerable().Select(m => new DeptData { ID = m.Field<int>("DEPT_ID"), Code = m.Field<string>("DEPT_CODE"), Name = m.Field<string>("DEPT_CNAME") }).ToList();

            return aAns;
        }

        //---------------------------------------資料驗證-----------------------------------------
        private bool DataTableDataValidate(DataTable aTable)
        {
            string employeeID = "";
            string overtimeDate = "";
            string beginTime = "";
            string endTime = "";
            string overtimeHours = "";
            string restHours = "";
            string totalHours = "";
            string overtimeCauseID = "";
            string overtimeRoteID = "";
            string overtimeDeptID = "";
            string overtimeEffectDate = "";
            string salaryYYMM = "";
            string totalHoursMsg = "";
            int cnt = 0;

            bool flag = false;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            //1. 判斷加班起始時間不可大於截止時間
            //2. 加班時數及補休時數只能擇一申請
            //3. 判斷加班日期是否已有鎖檔紀錄
            //4. 判斷加班時數
            //5. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
            try
            {
                foreach (DataRow aRow in aTable.Rows)
                {
                    flag = false;
                    employeeID = aRow["EMPLOYEE_ID"].ToString();
                    overtimeDate = aRow["OVERTIME_DATE"].ToString();
                    beginTime = aRow["BEGIN_TIME"].ToString();
                    endTime = aRow["END_TIME"].ToString();
                    overtimeHours = aRow["OVERTIME_HOURS"].ToString();
                    restHours = aRow["REST_HOURS"].ToString();
                    totalHours = aRow["TOTAL_HOURS"].ToString();
                    //overtimeCauseID = aRow["OVERTIME_CAUSE_ID"].ToString();
                    //overtimeRoteID = aRow["OVERTIME_ROTE_ID"].ToString();
                    //overtimeDeptID = aRow["OVERTIME_DEPT_ID"].ToString();
                    //overtimeEffectDate = aRow["OVERTIME_EFFECT_DATE"].ToString();
                    salaryYYMM = aRow["SALARY_YYMM"].ToString();

                    //1. 判斷加班起始時間不可大於截止時間
                    if (int.Parse(beginTime) >= int.Parse(endTime))
                    {
                        aRow.SetColumnError("BEGIN_TIME", "加班起始時間不可大於截止時間");
                        flag = true;
                    }

                    //3. 判斷加班日期是否已有鎖檔紀錄
                    string sql = " select COUNT(*) AS cnt from HRM_ATTEND_DATA_LOCK " + "\r\n";
                    sql = sql + " where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                    sql = sql + ",HRM_ATTEND_DATA_LOCK.ATTEND_DATE,'GROUP_ID')" + "\r\n";
                    sql = sql + " and ATTEND_DATE = '" + overtimeDate + "'";


                    DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);
                    cnt = int.Parse(dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["cnt"].ToString());

                    if (cnt > 0)
                    {
                        aRow.SetColumnError("END_DATE", "此區間加班日期已有鎖檔紀錄");
                        flag = true;
                    }

                    //4. 判斷加班時數
                    var parameters = new List<object>();
                    parameters.Add("0" + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime);
                    var obj = checkOvertimeHours(parameters.ToArray());
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["rejectCode"].ToString() != "")
                        {
                            switch (dt.Rows[0]["rejectCode"].ToString())
                            {
                                case "1": aRow.SetColumnError("OVERTIME_DATE", "申請日期查無出勤刷卡資料"); break;
                                case "2": aRow.SetColumnError("OVERTIME_DATE", "申請日期出勤刷卡資料不完整"); break;
                                case "3": aRow.SetColumnError("BEGIN_TIME", "申請時間未在刷卡時段內"); break;
                                case "4": aRow.SetColumnError("OVERTIME_DATE", "申請日期查無出勤資料"); break;
                                case "5": aRow.SetColumnError("BEGIN_TIME", "加班起始時間未在合理時間範圍內"); break;
                                case "6": aRow.SetColumnError("END_TIME", "加班截止時間未在合理時間範圍內"); break;
                            }
                            flag = true;
                        }
                        else
                        {
                            if (int.Parse(dt.Rows[0]["hours"].ToString()) == 0)
                            {
                                aRow.SetColumnError("BEGIN_TIME", "申請的時段為上班時間");
                                flag = true;
                            }

                            if (decimal.Parse(dt.Rows[0]["hours"].ToString()) != decimal.Parse(overtimeHours) + decimal.Parse(restHours))
                            {
                                totalHoursMsg = "加班總時數不正確";
                                aRow.SetColumnError("TOTAL_HOURS", totalHoursMsg);
                                flag = true;
                            }
                        }

                    }

                    //5. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
                    parameters.Clear();
                    parameters.Add("0" + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime);
                    obj = checkOvertimeData(parameters.ToArray());
                    cnt = int.Parse(JsonConvert.DeserializeObject<String>(obj[1].ToString()));
                    if (cnt > 0)
                    {
                        totalHoursMsg = totalHoursMsg + "&&" + "申請的時段內已有存在的加班資料";
                        aRow.SetColumnError("TOTAL_HOURS", totalHoursMsg);
                        flag = true;
                    }

                    if (flag)
                        aRow.RowError = "驗證錯誤";
                }
                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection("JBHR_EEP", connection); }
        }

        //---------------------------------------寫入資料庫---------------------------------------
        private bool DataTableDataInsert(DataTable aTable)
        {
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string sql = "";
            string configSql = "";
            string detailSql = "";
            string newID = "@overtimeID";
            int columnCount = 0;
            int holidayID;

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";

                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                sql = "declare @overtimeID int " + "\r\n";
                foreach (DataRow aRow in aTable.Rows)
                {
                    configSql = "select COMPENSATORY_SALARY_ID from HRM_SYSTEM_ATTEND_CONFIG " + "\r\n";
                    configSql = configSql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + aRow["EMPLOYEE_ID"].ToString() + "','" + Convert.ToDateTime(aRow["OVERTIME_DATE"]).ToShortDateString() + "','COMPANY_ID')" + "\r\n";
                    DataSet dsHRM_SYSTEM_ATTEND_CONFIG = this.ExecuteSql(configSql, connection, transaction);
                    holidayID = int.Parse(dsHRM_SYSTEM_ATTEND_CONFIG.Tables[0].Rows[0]["COMPENSATORY_SALARY_ID"].ToString());

                    columnCount = aRow.ItemArray.Length;
                    sql = sql + "insert into HRM_ATTEND_OVERTIME_DATA (";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + column.ColumnName + ",";
                    }
                    sql = sql + " OVERTIME_DATE_TIME_BEGIN,OVERTIME_DATE_TIME_END,NOT_ALLOW_MODIFY,SYSTEM_CREATE,IS_IMPORT,CREATE_MAN,CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                    sql = sql + " select '";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + (aRow[column].ToString().Trim() == "" ? "null" : aRow[column].ToString().Trim()) + "','";
                    }
                    sql = sql + DateAndTimeMerger(Convert.ToDateTime(aRow["OVERTIME_DATE"]).Date, aRow["BEGIN_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','";
                    sql = sql + DateAndTimeMerger(Convert.ToDateTime(aRow["OVERTIME_DATE"]).Date, aRow["END_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','";
                    sql = sql + "N','N','Y','";
                    sql = sql + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                    sql = sql + "select @overtimeID = SCOPE_IDENTITY()" + "\r\n";
                    detailSql = createAbsentPlusSql(userName, newID, aRow["EMPLOYEE_ID"].ToString(), Convert.ToDateTime(aRow["OVERTIME_DATE"]).Date, Convert.ToDateTime(aRow["OVERTIME_EFFECT_DATE"]).Date, aRow["BEGIN_TIME"].ToString(), aRow["END_TIME"].ToString(), decimal.Parse(aRow["REST_HOURS"].ToString()), holidayID);
                    sql = sql + detailSql;
                }
                sql = sql.Replace("'null'", "null");
                this.ExecuteSql(sql, connection, transaction);
                dsUSERS.Dispose();
                //dsHRM_SYSTEM_ATTEND_CONFIG.Dispose();
                transaction.Commit();
                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection("JBHR_EEP", connection); }
        }

        //--------------------------------------欄位驗證時候的資料-------------
        public class DataTableCellValidateData
        {
            public List<EmployeeData> EmployeeData { get; set; }
            public List<RoteData> RoteData { get; set; }
            public List<OverTimeCauseData> OverTimeCauseData { get; set; }
            public List<DeptData> DeptData { get; set; }

            public DataTableCellValidateData()
            {
                EmployeeData = new List<EmployeeData>();
                RoteData = new List<RoteData>();
                OverTimeCauseData = new List<OverTimeCauseData>();
                DeptData = new List<DeptData>();
            }
        }

        //人員資料
        public class EmployeeData
        {
            public string ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        //班別代碼資料
        public class RoteData
        {
            public int ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        //加班原因代碼
        public class OverTimeCauseData
        {
            public int ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        //加班部門代碼
        public class DeptData
        {
            public int ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class ImportData
        {
            public string EMPLOYEE_ID { get; set; }
            public string OVERTIME_DATE { get; set; }
            public string BEGIN_TIME { get; set; }
            public string END_TIME { get; set; }
            public string OVERTIME_EFFECT_DATE { get; set; }
            public string OVERTIME_HOURS { get; set; }
            public string REST_HOURS { get; set; }
            public string TOTAL_HOURS { get; set; }
            public string OVERTIME_CAUSE_ID { get; set; }
            public string OVERTIME_ROTE_ID { get; set; }
            public string OVERTIME_NO { get; set; }
            public string SALARY_YYMM { get; set; }
            public string MEMO { get; set; }
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_BeforeDelete(object sender, UpdateComponentBeforeDeleteEventArgs e)
        {
            string detailSql = "";
            bool flag = true;
            string holidayID = "";
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            var restHours = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("REST_HOURS").ToString();
            var overtimeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("OVERTIME_ID");
            var employeeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("EMPLOYEE_ID");
            var overtimeDate = Convert.ToDateTime(ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("OVERTIME_DATE").ToString()).Date;

            sql = "select * from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_NO = " + overtimeID;
            DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];
            if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
            {
                if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                {
                    sql = "select COMPENSATORY_SALARY_ID from HRM_SYSTEM_ATTEND_CONFIG " + "\r\n";
                    sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + overtimeDate.ToString("yyyy-MM-dd") + "','COMPANY_ID')" + "\r\n";
                    DataSet dsHRM_SYSTEM_ATTEND_CONFIG = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    holidayID = dsHRM_SYSTEM_ATTEND_CONFIG.Tables[0].Rows[0]["COMPENSATORY_SALARY_ID"].ToString();
                    dsHRM_SYSTEM_ATTEND_CONFIG.Dispose();

                    //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                    sql = "select * from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID <> ABSENT_MINUS_ID and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString();
                    DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                    //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                    sql = "";
                    foreach (DataRow Row in dtHRM_ATTEND_ABSENT_MINUS_DETAIL.Rows)
                    {
                        var hours = decimal.Parse(Row["ABSENT_HOURS"].ToString());
                        var absentMinusID = Row["ABSENT_MINUS_ID"].ToString();
                        DateTime absentDate = Convert.ToDateTime(Row["ABSENT_DATE"].ToString()).Date;

                        sql = "select * from  HRM_ATTEND_ABSENT_MINUS where ABSENT_MINUS_ID = " + absentMinusID;
                        DataTable dtHRM_ATTEND_ABSENT_MINUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];
                        var beginDate = dtHRM_ATTEND_ABSENT_MINUS.Rows[0]["BEGIN_DATE"].ToString();
                        var endDate = dtHRM_ATTEND_ABSENT_MINUS.Rows[0]["END_DATE"].ToString();

                        //得假資料檔得假剩餘時數(REST_HOURS)
                        sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
                        sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                        sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                        sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                        sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                        sql = sql + "and REST_HOURS >0" + "\r\n";
                        sql = sql + "and A.ABSENT_PLUS_ID <> " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                        sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
                        sql = sql + "order by A.BEGIN_DATE,A.END_DATE DESC" + "\r\n";

                        DataSet drABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);

                        foreach (DataRow drRestHours in drABSENT_PLUS.Tables[0].Rows)
                        {
                            string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                            if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                            {
                                detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                                hours = 0;
                                break;
                            }
                            else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                            {
                                detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS - " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + drRestHours["REST_HOURS"].ToString() + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                                drRestHours["REST_HOURS"] = 0;
                            }
                        }

                        //判斷補休時數不夠扣
                        if (hours > 0)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_NO = " + overtimeID + "\r\n";
                    detailSql = detailSql + "delete from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                    this.ExecuteSql(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                }
                else
                {
                    //出現錯誤訊息 ROLLBACK
                    this.ExecuteSql("update HRM_ATTEND_OVERTIME_DATA ERROR", ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                }
            }
        }
    }
}

