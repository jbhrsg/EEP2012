using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using JBTool;

namespace _HRM_Attend_Normal_Overtime
{
    public partial class Component : DataModule
    {
        public const int G_HeadRowIndex = 0;

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
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

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
                sql = sql + " union all ";
                sql = sql + " select EMPLOYEE_ID from HRM_ATTEND_HOLIDAY_OVERTIME_DATA " + "\r\n";
                sql = sql + " where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + " and OVERTIME_DATE = '" + overtimeDate + "'" + "\r\n";
                sql = sql + " and BEGIN_TIME < '" + endTime + "'" + "\r\n";
                sql = sql + " and END_TIME > '" + beginTime + "'" + "\r\n";

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
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
            decimal hours, minutes, totalHours, restHours, workHours;
            workHours = 0;

            TimeSpan ts;
            string roteCode = "";
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();
            int overtimeUnit = 30;
            string isCheckAttendCard = "Y";

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday' or ROTEMAPPING_CODE = 'ChangeHoliday'" + "\r\n";
                DataTable dtRoteMappingDetail = this.ExecuteSql(sql, connection, transaction).Tables[0];
                dtRoteMappingDetail.PrimaryKey = new DataColumn[] { dtRoteMappingDetail.Columns["ROTE_ID"] };

                //判斷若其他加班比例設定有資料，此天不判斷加班起始時間未在合理時間範圍內
                sql = "select count(*) as cnt from HRM_SALARY_OVERTIME_RATEFIXED_MASTER" + "\r\n";
                sql = sql + "where EFFECT_DATE = '" + overtimeDate + "'" + "\r\n";
                sql = sql + "and ROTE_ID = " + overtimeDate + "'" + "\r\n";

                sql = "select A.ACTUAL_ROTE_ID AS ROTE_ID," + "\r\n";
                sql = sql + "C.ROTE_CODE," + "\r\n";
                sql = sql + "C.ON_TIME," + "\r\n";
                sql = sql + "C.OFF_TIME," + "\r\n";
                sql = sql + "C.OT_BEGIN_TIME," + "\r\n";
                sql = sql + "C.IS_CARD," + "\r\n";
                sql = sql + "isnull(C.WORK_HRS,0) as WORK_HRS," + "\r\n";
                sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
                sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
                sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME," + "\r\n";
                sql = sql + "isnull(D.WORK_HRS,0) AS UPPER_WORK_HRS," + "\r\n";
                sql = sql + "isnull(case when B.ON_TIME = '' then null else B.ON_TIME end,E.BEGIN_TIME) AS CARD_ON_TIME," + "\r\n";
                sql = sql + "isnull(case when B.OFF_TIME = '' then null else B.OFF_TIME end,E.END_TIME) AS CARD_OFF_TIME," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_ON,E.ABSENT_DATE_TIME_BEGIN) as CARD_DATE_TIME_ON," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_OFF,E.ABSENT_DATE_TIME_END) as CARD_DATE_TIME_OFF," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_ON_TRAN,E.ABSENT_DATE_TIME_BEGIN) as CARD_DATE_TIME_ON_TRAN," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_OFF_TRAN,E.ABSENT_DATE_TIME_END) as CARD_DATE_TIME_OFF_TRAN" + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ATTEND_CARD B on A.EMPLOYEE_ID = B.EMPLOYEE_ID and A.ATTEND_DATE = B.CARD_DATE" + "\r\n"; ;
                sql = sql + "left join HRM_ATTEND_ROTE C on A.ACTUAL_ROTE_ID = C.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on A.HOLIDAY_ROTE_ID = D.ROTE_ID " + "\r\n";
                //sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ABSENT_LEAVE_DETAIL E on A.EMPLOYEE_ID = E.EMPLOYEE_ID and A.ATTEND_DATE = E.ABSENT_DATE" + "\r\n";   //公出資料
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE = '" + overtimeDate + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_ATTEND.Tables[0].Rows.Count != 0)
                {
                    roteCode = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString();
                    rote_on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                    rote_off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();

                    //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString());
                    if (drRoteMappingDetail == null)
                    {
                        roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString();
                        on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                        off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();
                        otBeginTime = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OT_BEGIN_TIME"].ToString();
                        workHours = decimal.Parse(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["WORK_HRS"].ToString());
                    }
                    else
                    {
                        roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ROTE_ID"].ToString();
                        on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ON_TIME"].ToString();
                        off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_OFF_TIME"].ToString();
                        workHours = decimal.Parse(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_WORK_HRS"].ToString());
                    }

                    //判斷加班參數設定IS_CHECK_ATTEND_CARD是否須是否檢查刷卡資料為 'Y'
                    sql = "SELECT ISNULL(OVERTIME_UNIT,30) as OVERTIME_UNIT,ISNULL(IS_CHECK_ATTEND_CARD,'Y') as IS_CHECK_ATTEND_CARD FROM HRM_ATTEND_OVERTIME_CONFIG" + "\r\n";
                    sql = sql + "WHERE COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + overtimeDate + "','COMPANY_ID')" + "\r\n";
                    DataSet dsHRM_ATTEND_OVERTIME_CONFIG = this.ExecuteSql(sql, connection, transaction);
                    //dsHRM_ATTEND_OVERTIME_CONFIG.Dispose();
                    if (dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows.Count > 0)
                    {
                        overtimeUnit = Convert.ToInt16(Convert.ToDouble(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["OVERTIME_UNIT"].ToString()));   //加班最小單位(分) default -->30
                        isCheckAttendCard = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_ATTEND_CARD"].ToString();  //是否檢查刷卡資料 default -->"Y"
                    }

                    //判斷考勤群組參數ComputingCard是否須刷卡為 1 時才需要判斷刷卡資料
                    sql = "SELECT * FROM [dbo].[dt_Attend_GroupParameterValue]('" + employeeID + "','" + overtimeDate + "') where GROUP_PARAMETER_CODE = 'ComputingCard'" + "\r\n";
                    DataSet dsGROUP_PARAMETER = this.ExecuteSql(sql, connection, transaction);
                    //dsGROUP_PARAMETER.Dispose();
                    if (isCheckAttendCard == "Y")
                    {
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
                    //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00" )
                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString());
                    if (drRoteMappingDetail == null)
                    {
                        roteBeginTime = DateAndTimeMerger(overtimeDate, rote_on_time);
                        roteEndTime = DateAndTimeMerger(overtimeDate, rote_off_time);

                        if (!(endOvertimeDate <= roteBeginTime || beginOvertimeDate >= otBeginTimeDate))
                            rejectCode = "5";   //加班起始時間未在合理時間範圍內
                        else if (!(endOvertimeDate <= roteBeginTime || endOvertimeDate <= roteBeginTime.AddDays(1)))
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

                    //計算加班時間
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    //var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks - aOverTime.Begin.Ticks).TotalMinutes;

                    int iMin = Convert.ToInt32((TotalMinute - ReduceMinute) % overtimeUnit);
                    minutes = ((TotalMinute - ReduceMinute) - iMin);
                    minutes = minutes >= overtimeUnit ? minutes : 0;

                    hours = minutes / 60;
                    totalHours = (TotalMinute - Convert.ToInt32(TotalMinute % overtimeUnit)) / 60M;

                    hours = Convert.ToDouble(hours) >= overtimeUnit / 60 ? hours : 0;
                    restHours = 0;

                    if ((string.Compare(beginTime, on_time) <= 0) && (string.Compare(endTime, off_time) >= 0) && (hours < workHours))
                    {
                        hours = workHours;
                        totalHours = workHours;
                    }


                    if (hours > 0)
                    {
                        sql = "select isnull(sum(REST_HOUR),0) as REST_HOUR from HRM_ATTEND_ROTE_HOLIDAY " + "\r\n";
                        sql = sql + "where ROTE_ID = " + dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString() + "\r\n";
                        sql = sql + " and BEGIN_HOUR <= " + hours.ToString() + "\r\n";
                        DataTable dtHRM_ATTEND_ROTE_HOLIDAY = this.ExecuteSql(sql, connection, transaction).Tables[0];
                        restHours = decimal.Parse(dtHRM_ATTEND_ROTE_HOLIDAY.Rows[0]["REST_HOUR"].ToString());
                        totalHours = totalHours + restHours;
                    }
                    //判斷是否要產生補休資料
                    if (restHours > 0)
                        sql = "select '" + rejectCode + "' as rejectCode," + hours + " as hours," + totalHours + " as totalHours," + restHours + " as restHours, 'Y' as isHoliday";
                    else
                        sql = "select '" + rejectCode + "' as rejectCode," + hours + " as hours," + totalHours + " as totalHours," + restHours + " as restHours, 'N' as isHoliday";
                }
                else
                {
                    sql = "select '" + rejectCode + "' as rejectCode, 0 as hours, 0 as totalHours, 0 as restHours, 'N' as isHoliday";
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] checkRoteOvertimeHours(object[] objParam)
        {
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                object[] returnObj = checkRoteOvertimeHours_Function(objParam, connection, transaction);
                transaction.Commit();
                return returnObj;
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
        }

        public object[] checkRoteOvertimeHours_Function(object[] objParam, IDbConnection connection, IDbTransaction transaction)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string overtimeID = parm[0];
            string employeeID = parm[1];
            string overtimeDate = parm[2];
            string beginTime = parm[3];
            string endTime = parm[4];
            string overtimeRoteID = parm[5];
            string overtimeCauseId = parm[6];

            string on_time = "";
            string off_time = "";
            string otBeginTime = "";
            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";
            string rejectCode = "";
            DateTime beginOvertimeDate, restBeginTime, otBeginTimeDate;
            DateTime endOvertimeDate, restEndTime, roteBeginTime, roteEndTime;
            decimal hours, minutes, totalHours, restHours, workHours, normalOvertimeHour, offpayOvertimeHour, holidayOvertimeHour, nationalHolidayOvertimeHour;
            workHours = 0;
            normalOvertimeHour = 0;
            offpayOvertimeHour = 0;
            holidayOvertimeHour = 0;
            nationalHolidayOvertimeHour = 0;
            string roteMappingCode = "";

            TimeSpan ts;
            string roteCode = "";
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();
            int overtimeUnit = 30;
            string isCheckAttendCard = "Y";
            string isChecknormalOvertimeHour = "Y";
            string isCheckoffpayOvertimeHour = "Y";
            string isCheckholidayOvertimeHour = "Y";
            string isChecknationalHolidayOvertimeHour = "Y";

            string js = string.Empty;

            try
            {
                string sql = "select ROTE_ID,ROTEMAPPING_CODE from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday' or ROTEMAPPING_CODE = 'ChangeHoliday'" + "\r\n";
                DataTable dtRoteMappingDetail = this.ExecuteSql(sql, connection, transaction).Tables[0];
                dtRoteMappingDetail.PrimaryKey = new DataColumn[] { dtRoteMappingDetail.Columns["ROTE_ID"] };

                sql = "select A.ACTUAL_ROTE_ID AS ROTE_ID," + "\r\n";
                sql = sql + "C.ROTE_CODE," + "\r\n";
                sql = sql + "C.ON_TIME," + "\r\n";
                sql = sql + "C.OFF_TIME," + "\r\n";
                sql = sql + "C.OT_BEGIN_TIME," + "\r\n";
                sql = sql + "C.IS_CARD," + "\r\n";
                sql = sql + "isnull(C.WORK_HRS,0) as WORK_HRS," + "\r\n";
                sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
                sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
                sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME," + "\r\n";
                sql = sql + "isnull(D.WORK_HRS,0) AS UPPER_WORK_HRS," + "\r\n";
                sql = sql + "isnull(case when B.ON_TIME = '' then null else B.ON_TIME end,E.BEGIN_TIME) AS CARD_ON_TIME," + "\r\n";
                sql = sql + "isnull(case when B.OFF_TIME = '' then null else B.OFF_TIME end,E.END_TIME) AS CARD_OFF_TIME," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_ON,E.ABSENT_DATE_TIME_BEGIN) as CARD_DATE_TIME_ON," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_OFF,E.ABSENT_DATE_TIME_END) as CARD_DATE_TIME_OFF," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_ON_TRAN,E.ABSENT_DATE_TIME_BEGIN) as CARD_DATE_TIME_ON_TRAN," + "\r\n";
                sql = sql + "isnull(B.CARD_DATE_TIME_OFF_TRAN,E.ABSENT_DATE_TIME_END) as CARD_DATE_TIME_OFF_TRAN," + "\r\n";
                sql = sql + "E.HOLIDAY_ID" + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ATTEND_CARD B on A.EMPLOYEE_ID = B.EMPLOYEE_ID and A.ATTEND_DATE = B.CARD_DATE" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE C on A.ACTUAL_ROTE_ID = C.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on A.HOLIDAY_ROTE_ID = D.ROTE_ID " + "\r\n";
                //sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ABSENT_LEAVE_DETAIL E on A.EMPLOYEE_ID = E.EMPLOYEE_ID and A.ATTEND_DATE = E.ABSENT_DATE" + "\r\n";   //公出資料
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE = '" + overtimeDate + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                sql = "select * from HRM_ATTEND_ROTE where ROTE_ID = " + overtimeRoteID + "\r\n";
                DataTable dtOvertimeRote = this.ExecuteSql(sql, connection, transaction).Tables[0];

                sql = "select count(*) cnt from HRM_ATTEND_HOLIDAY" + "\r\n";
                sql = sql + "left join  HRM_ATTEND_HOLIMAPPING_DETAIL on HRM_ATTEND_HOLIDAY.HOLIDAY_ID=HRM_ATTEND_HOLIMAPPING_DETAIL.HOLIDAY_ID" + "\r\n";
                sql = sql + "where HOLIMAPPING_CODE='ForeignBusinessTrip' and HRM_ATTEND_HOLIDAY.HOLIDAY_ID='" + dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["HOLIDAY_ID"].ToString() + "'" + "\r\n";
                //sql = "select count(*) cnt from HRM_ATTEND_HOLIDAY where HOLIDAY_ID='" + dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["HOLIDAY_ID"].ToString() + "' and HOLIDAY_CNAME='國外出差'";
                DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);
                bool IsForeignLeave = Convert.ToInt32(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["cnt"].ToString()) == 0 ? false : true;

                //SYMTEK國外出差：檢查加班是否為出差第一天或最後一天，是則無法加班
                if (IsForeignLeave)
                {
                    DateTime BeforeOvertimeDate = Convert.ToDateTime(overtimeDate).AddDays(1);
                    DateTime LastOvertimeDate = Convert.ToDateTime(overtimeDate).AddDays(-1);
                    sql = "select CONVERT(VARCHAR(10),BEGIN_DATE,111) BEGIN_DATE,CONVERT(VARCHAR(10),END_DATE,111) END_DATE from HRM_ATTEND_ABSENT_LEAVE where EMPLOYEE_ID='" + employeeID + "' and '" + overtimeDate + "' BETWEEN BEGIN_DATE AND END_DATE" + "\r\n";
                    sql = sql + "select count(*) cnt from HRM_ATTEND_ABSENT_LEAVE where EMPLOYEE_ID='" + employeeID + "' and '" + BeforeOvertimeDate.ToString("yyyy/MM/dd") + "' BETWEEN BEGIN_DATE AND END_DATE" + "\r\n";
                    sql = sql + "select count(*) cnt from HRM_ATTEND_ABSENT_LEAVE where EMPLOYEE_ID='" + employeeID + "' and '" + LastOvertimeDate.ToString("yyyy/MM/dd") + "' BETWEEN BEGIN_DATE AND END_DATE" + "\r\n";

                    DataSet dsHRM_ATTEND_ABSENT_LEAVE = this.ExecuteSql(sql, connection, transaction);

                    if (Convert.ToDateTime(overtimeDate) == Convert.ToDateTime(dsHRM_ATTEND_ABSENT_LEAVE.Tables[0].Rows[0]["BEGIN_DATE"].ToString()) || Convert.ToDateTime(overtimeDate) == Convert.ToDateTime(dsHRM_ATTEND_ABSENT_LEAVE.Tables[0].Rows[0]["END_DATE"].ToString()))
                    {
                        if (dsHRM_ATTEND_ABSENT_LEAVE.Tables[1].Rows[0]["cnt"].ToString() == "0" || dsHRM_ATTEND_ABSENT_LEAVE.Tables[2].Rows[0]["cnt"].ToString() == "0")
                            rejectCode = "8";
                    }
                }
                if (rejectCode == "")
                {
                    if (dsHRM_ATTEND_ATTEND.Tables[0].Rows.Count != 0)
                    {
                        roteCode = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString();
                        rote_on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                        rote_off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();

                        //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                        DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString());
                        if (drRoteMappingDetail == null)
                        {
                            roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString();
                            on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                            off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();
                            otBeginTime = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OT_BEGIN_TIME"].ToString();
                            workHours = decimal.Parse(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["WORK_HRS"].ToString());
                        }
                        else
                        {
                            roteMappingCode = drRoteMappingDetail["ROTEMAPPING_CODE"].ToString();
                            if (dtOvertimeRote.Rows.Count != 0)
                            {
                                roteID = dtOvertimeRote.Rows[0]["ROTE_ID"].ToString();
                                on_time = dtOvertimeRote.Rows[0]["ON_TIME"].ToString();
                                off_time = dtOvertimeRote.Rows[0]["OFF_TIME"].ToString();
                                workHours = decimal.Parse(dtOvertimeRote.Rows[0]["WORK_HRS"].ToString());
                            }
                            else
                            {
                                roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ROTE_ID"].ToString();
                                on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ON_TIME"].ToString();
                                off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_OFF_TIME"].ToString();
                                workHours = decimal.Parse(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_WORK_HRS"].ToString());
                            }
                        }

                        //判斷加班參數設定IS_CHECK_ATTEND_CARD是否須是否檢查刷卡資料為 'Y'
                        sql = "SELECT ISNULL(OVERTIME_UNIT,30) as OVERTIME_UNIT," + "\r\n";
                        sql = sql + "ISNULL(IS_CHECK_ATTEND_CARD,'Y') as IS_CHECK_ATTEND_CARD," + "\r\n";
                        sql = sql + "ISNULL(ALLOW_NORMAL_OVERTIME_HOUR,0) as ALLOW_NORMAL_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(ALLOW_OFFPAY_OVERTIME_HOUR,0) as ALLOW_OFFPAY_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(ALLOW_HOLIDAY_OVERTIME_HOUR,0) as ALLOW_HOLIDAY_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(ALLOW_NATIONAL_HOLIDAY_OVERTIME_HOUR,0) as ALLOW_NATIONAL_HOLIDAY_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(IS_CHECK_NORMAL_OVERTIME_HOUR,'Y') as IS_CHECK_NORMAL_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(IS_CHECK_HOLIDAY_OVERTIME_HOUR,'Y') as IS_CHECK_HOLIDAY_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(IS_CHECK_OFFPAY_OVERTIME_HOUR,'Y') as IS_CHECK_OFFPAY_OVERTIME_HOUR," + "\r\n";
                        sql = sql + "ISNULL(IS_CHECK_NATIONAL_HOLIDAY_OVERTIME_HOUR,'Y') as IS_CHECK_NATIONAL_HOLIDAY_OVERTIME_HOUR" + "\r\n";
                        sql = sql + "FROM HRM_ATTEND_OVERTIME_CONFIG" + "\r\n";
                        sql = sql + "WHERE COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + overtimeDate + "','COMPANY_ID')" + "\r\n";
                        DataSet dsHRM_ATTEND_OVERTIME_CONFIG = this.ExecuteSql(sql, connection, transaction);
                        //dsHRM_ATTEND_OVERTIME_CONFIG.Dispose();
                        if (dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows.Count > 0)
                        {
                            overtimeUnit = Convert.ToInt16(Convert.ToDouble(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["OVERTIME_UNIT"].ToString()));   //加班最小單位(分) default -->30
                            isCheckAttendCard = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_ATTEND_CARD"].ToString();  //是否檢查刷卡資料 default -->"Y"
                            normalOvertimeHour = Convert.ToDecimal(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ALLOW_NORMAL_OVERTIME_HOUR"].ToString());    //平日加班時數限制
                            offpayOvertimeHour = Convert.ToDecimal(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ALLOW_OFFPAY_OVERTIME_HOUR"].ToString());    //休息日時數限制
                            holidayOvertimeHour = Convert.ToDecimal(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ALLOW_HOLIDAY_OVERTIME_HOUR"].ToString());  //例假日時數限制
                            nationalHolidayOvertimeHour = Convert.ToDecimal(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ALLOW_NATIONAL_HOLIDAY_OVERTIME_HOUR"].ToString()); //國定假日時數限制

                            isChecknormalOvertimeHour = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_NORMAL_OVERTIME_HOUR"].ToString();  //是否檢查平日加班時數限制 default -->"Y"
                            isCheckoffpayOvertimeHour = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_OFFPAY_OVERTIME_HOUR"].ToString();  //是否檢查休息日時數限制 default -->"Y"
                            isCheckholidayOvertimeHour = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_HOLIDAY_OVERTIME_HOUR"].ToString();  //是否檢查例假日時數限制 default -->"Y"
                            isChecknationalHolidayOvertimeHour = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_NATIONAL_HOLIDAY_OVERTIME_HOUR"].ToString();  //是否檢查國定假日時數限制 default -->"Y"
                        }

                        //判斷考勤群組參數ComputingCard是否須刷卡為 1 時才需要判斷刷卡資料
                        sql = "SELECT * FROM [dbo].[dt_Attend_GroupParameterValue]('" + employeeID + "','" + overtimeDate + "') where GROUP_PARAMETER_CODE = 'ComputingCard'" + "\r\n";
                        DataSet dsGROUP_PARAMETER = this.ExecuteSql(sql, connection, transaction);
                        //dsGROUP_PARAMETER.Dispose();

                        //SYMTEK國外出差不檢查刷卡資料
                        if (!IsForeignLeave)
                        {
                            if (isCheckAttendCard == "Y")
                            {
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
                        }
                    }
                    else
                        rejectCode = "4";   //申請日期尚未產生員工班表
                }

                //檢查加班原因
                string NotCheck = "";
                object[] OvertimeCause = checkOvertimeCause(new object[] { employeeID + "," + overtimeDate + "," + overtimeCauseId });
                var dtOvertimeCause = JsonConvert.DeserializeObject<DataTable>(OvertimeCause[1].ToString());
                if (dtOvertimeCause.Columns.Count > 1)
                    if (dtOvertimeCause.Rows.Count > 0 && dtOvertimeCause.Columns.Contains("NOT_CHECKCARD"))
                        if (dtOvertimeCause.Rows[0]["NOT_CHECKCARD"].ToString() == "Y") NotCheck = "Y";

                //判斷若無錯誤訊息--計算加班時數
                if (rejectCode == "")
                {
                    beginOvertimeDate = DateAndTimeMerger(overtimeDate, beginTime);
                    endOvertimeDate = DateAndTimeMerger(overtimeDate, endTime);
                    otBeginTimeDate = DateAndTimeMerger(overtimeDate, otBeginTime);
                    aOverTime.Begin = beginOvertimeDate;
                    aOverTime.End = endOvertimeDate;

                    //計算正常上班時間
                    //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00" )
                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString());
                    if (drRoteMappingDetail == null && NotCheck != "Y")
                    {
                        roteBeginTime = DateAndTimeMerger(overtimeDate, rote_on_time);
                        roteEndTime = DateAndTimeMerger(overtimeDate, rote_off_time);

                        if (!(endOvertimeDate <= roteBeginTime || beginOvertimeDate >= otBeginTimeDate))
                            rejectCode = "5";   //加班起始時間未在合理時間範圍內
                        else if (!(endOvertimeDate <= roteBeginTime || endOvertimeDate <= roteBeginTime.AddDays(1)))
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

                    //計算加班時間
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    //var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks - aOverTime.Begin.Ticks).TotalMinutes;

                    int iMin = Convert.ToInt32((TotalMinute - ReduceMinute) % overtimeUnit);
                    minutes = ((TotalMinute - ReduceMinute) - iMin);
                    minutes = minutes >= overtimeUnit ? minutes : 0;

                    hours = minutes / 60;
                    totalHours = (TotalMinute - Convert.ToInt32(TotalMinute % overtimeUnit)) / 60M;

                    hours = Convert.ToDouble(hours) >= overtimeUnit / 60 ? hours : 0;
                    restHours = 0;

                    //判斷加班起迄落在班別上下班起迄區間且加班時數 < 工作時數
                    if ((string.Compare(beginTime, on_time) <= 0) && (string.Compare(endTime, off_time) >= 0) && (hours < workHours))
                    {
                        hours = workHours;
                        totalHours = workHours;
                    }

                    //判斷加班時數限制
                    decimal limitOvertimeHour = 0;
                    switch (roteMappingCode)
                    {
                        case "":   //平日
                            if (isChecknormalOvertimeHour == "Y")
                                limitOvertimeHour = normalOvertimeHour;
                            break;
                        case "OffDay":   //休息日
                            if (isCheckoffpayOvertimeHour == "Y")
                                limitOvertimeHour = offpayOvertimeHour;
                            break;
                        case "Holidays":   //例假日
                            if (isCheckholidayOvertimeHour == "Y")
                                limitOvertimeHour = holidayOvertimeHour;
                            break;
                        case "NationalHoliday":   //國定假日
                            if (isChecknationalHolidayOvertimeHour == "Y")
                                limitOvertimeHour = nationalHolidayOvertimeHour;
                            break;
                    }

                    if (NotCheck == "Y") limitOvertimeHour = 0;

                    if (limitOvertimeHour > 0)
                    {
                        sql = "select isnull(sum(TOTAL_HOURS),0) as TOTAL_HOURS from HRM_ATTEND_OVERTIME_DATA " + "\r\n";
                        sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                        sql = sql + "and OVERTIME_DATE = '" + overtimeDate + "'" + "\r\n";
                        sql = sql + "and OVERTIME_ID <> " + overtimeID + "\r\n";
                        DataTable dtHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction).Tables[0];
                        decimal overtimeHours = decimal.Parse(dtHRM_ATTEND_OVERTIME_DATA.Rows[0]["TOTAL_HOURS"].ToString());
                        if (hours > 0 && (overtimeHours + hours) > limitOvertimeHour)
                            rejectCode = "7";   //加班時數超過限制
                    }

                    if (hours > 0)
                    {
                        sql = "select isnull(sum(REST_HOUR),0) as REST_HOUR from HRM_ATTEND_ROTE_HOLIDAY " + "\r\n";
                        sql = sql + "where ROTE_ID = " + dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString() + "\r\n";
                        sql = sql + " and BEGIN_HOUR <= " + hours.ToString() + "\r\n";
                        DataTable dtHRM_ATTEND_ROTE_HOLIDAY = this.ExecuteSql(sql, connection, transaction).Tables[0];
                        restHours = decimal.Parse(dtHRM_ATTEND_ROTE_HOLIDAY.Rows[0]["REST_HOUR"].ToString());
                        totalHours = totalHours + restHours;
                    }

                    //判斷是否要產生補休資料
                    if (restHours > 0)
                        sql = "select '" + rejectCode + "' as rejectCode," + hours + " as hours," + totalHours + " as totalHours," + restHours + " as restHours, 'Y' as isHoliday";
                    else
                        sql = "select '" + rejectCode + "' as rejectCode," + hours + " as hours," + totalHours + " as totalHours," + restHours + " as restHours, 'N' as isHoliday";
                }
                else
                {
                    sql = "select '" + rejectCode + "' as rejectCode, 0 as hours, 0 as totalHours, 0 as restHours, 'N' as isHoliday";
                }

                DataSet dsHours = this.ExecuteSql(sql, connection, transaction);
                //transaction.Commit();
                js = JsonConvert.SerializeObject(dsHours.Tables[0], Formatting.Indented);
            }
            catch
            {
                //transaction.Rollback();
                return new object[] { 0, false };
            }
            //finally
            //{
            //    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            //}
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] getSalaryYYMM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];

            string js = string.Empty;
            string formYYMM = string.Empty;
            string checkYYMM = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select distinct case when day(ATTEND_DATE) > (SELECT ATTEND_CLOSE_DAY FROM HRM_ATTEND_OVERTIME_CONFIG ";
                sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'COMPANY_ID')) then LEFT(CONVERT(varchar,dateadd(mm,1,ATTEND_DATE),112),6) else LEFT(CONVERT(varchar,ATTEND_DATE,112),6) end as SALARY_YYMM " + "\r\n";
                sql = sql + "from HRM_ATTEND_DATA_LOCK" + "\r\n";
                sql = sql + "where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'GROUP_ID')" + "\r\n";
                sql = sql + "and ATTEND_DATE >= '" + overtimeDate + "'" + "\r\n";
                sql = sql + " ORDER BY SALARY_YYMM DESC";
                DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);

                sql = "select case when day(convert(datetime,'" + overtimeDate + "')) > (select ATTEND_CLOSE_DAY from HRM_ATTEND_OVERTIME_CONFIG ";
                sql = sql + " where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",convert(datetime,'" + overtimeDate + "'),'COMPANY_ID')) then left(convert(varchar,dateadd(mm,1,convert(datetime,'" + overtimeDate + "')),112),6)";
                sql = sql + " else left(convert(varchar,convert(datetime,'" + overtimeDate + "'),112),6) end as SALARY_YYMM" + "\r\n";
                DataSet dsOvertimeDate = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows.Count != 0)
                {
                    if (Int32.Parse(dsOvertimeDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString()) > Int32.Parse(dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["SALARY_YYMM"].ToString()))
                    {
                        //Indented縮排 將資料轉換成Json格式
                        formYYMM = dsOvertimeDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString();
                    }
                    else
                    {
                        sql = "select LEFT(CONVERT(varchar,dateadd(mm,1,convert(datetime,'" + dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["SALARY_YYMM"].ToString() + "'+'01')),112),6) as SALARY_YYMM";
                        DataSet dsOvertimeDateAdd = this.ExecuteSql(sql, connection, transaction);
                        formYYMM = dsOvertimeDateAdd.Tables[0].Rows[0]["SALARY_YYMM"].ToString();
                    }
                }
                else
                {
                    //Indented縮排 將資料轉換成Json格式
                    formYYMM = dsOvertimeDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString();
                }

                //與計薪年月檢查，已有計薪年月時+1
                sql = "select count(*) as cnt from HRM_SALARY_WAGE" + "\r\n";
                sql = sql + "join HRM_SALARY_WAGE_LOCK on HRM_SALARY_WAGE.SALARY_YYMM=HRM_SALARY_WAGE_LOCK.SALARY_YYMM and HRM_SALARY_WAGE_LOCK.SALARY_SEQ=HRM_SALARY_WAGE.SALARY_SEQ" + "\r\n";
                sql = sql + "where SALARY_FLAG='Y' and HRM_SALARY_WAGE.SALARY_YYMM='" + formYYMM + "' and EMPLOYEE_ID='" + employeeID + "'";
                DataSet dsHRM_SALARY_WAGE_LOCK = this.ExecuteSql(sql, connection, transaction);

                if (Int32.Parse(dsHRM_SALARY_WAGE_LOCK.Tables[0].Rows[0]["cnt"].ToString()) > 0)
                    sql = "select LEFT(CONVERT(varchar,dateadd(mm,1,convert(datetime,'" + formYYMM + "'+'01')),112),6) as SALARY_YYMM";
                else
                    sql = "select '" + formYYMM + "' as SALARY_YYMM";

                DataSet dsOvertimeDateNew = this.ExecuteSql(sql, connection, transaction);

                js = JsonConvert.SerializeObject(dsOvertimeDateNew.Tables[0], Formatting.Indented);

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] getEffectDate(object[] objParam)
        {
            string ServerPackageName = this.GetType().Namespace;
            string MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            return new PackageCallMethod.PackageManager(this, ServerPackageName, MethodName, "_HRM_Attend_Normal_Overtime", "getEffectDate_Normal").CallMethod(objParam);
        }

        public object[] getEffectDate_Normal(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];
            string effectDate;

            string js = string.Empty;
            try
            {
                effectDate = Convert.ToDateTime(DateTime.Parse(overtimeDate).AddMonths(1).ToString("yyyy/MM/01")).AddDays(-1).ToString("yyyy/MM/dd");

                js = JsonConvert.SerializeObject(effectDate, Formatting.Indented);
            }
            catch
            {
                return new object[] { 0, false };
            }
            finally
            {
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        public object[] getEffectDate_SYMTEK(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];
            string effectDate;
            string sql;

            string js = string.Empty;
            string arriveDate = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "Select	EMPLOYEE_ID,CONVERT(VARCHAR(10),MIN(BEGIN_DATE),111) BEGIN_DATE,CONVERT(VARCHAR(10),MAX(END_DATE),111) END_DATE" + "\r\n";
                sql = sql + "FROM	HRM_ATTEND_ABSENT_PLUS A" + "\r\n";
                sql = sql + "WHERE	BEGIN_DATE <= '" + overtimeDate + "' AND END_DATE >= '" + overtimeDate + "' " + "\r\n";
                sql = sql + "and A.HOLIDAY_ID = (select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'AnnualPlus') " + "\r\n";
                sql = sql + "and a.MEMO <> '6個月至未滿一年'" + "\r\n";
                sql = sql + "and A.EMPLOYEE_ID = '" + employeeID + "' " + "\r\n";
                sql = sql + "Group by A.EMPLOYEE_ID" + "\r\n";
                DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction).Tables[0];
                if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                    effectDate = dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["END_DATE"].ToString();
                else
                {
                    sql = "select  dateadd(dd,[dbo].[fun_Base_LeaveDaysWithoutPay]('" + employeeID + "','" + overtimeDate + "')-1,ISNULL(C.GROUP_EFFECT_DATE,B.ARRIVE_DATE)) AS ARRIVE_DATE" + "\r\n";
                    sql = sql + "from HRM_BASE_BASE as C" + "\r\n";
                    sql = sql + "left join  [dbo].[dtHRM_BASE_BASEIO_FirstArrive](NULL) AS B ON C.EMPLOYEE_ID = B.EMPLOYEE_ID" + "\r\n";
                    sql = sql + "where C.EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    DataTable dtLeaveDate = this.ExecuteSql(sql, connection, transaction).Tables[0];
                    if (dtLeaveDate.Rows.Count > 0 && !Convert.IsDBNull(dtLeaveDate.Rows[0][0]))
                        arriveDate = string.Format("{0:yyyy\\/MM\\/dd}", dtLeaveDate.Rows[0][0]);
                    dtLeaveDate.Dispose();

                    effectDate = Convert.ToDateTime(overtimeDate).AddYears(1).Year + "/" + arriveDate.Substring(5, 2) + "/" + arriveDate.Substring(8, 2);

                    if (Convert.ToDateTime(effectDate) < Convert.ToDateTime(overtimeDate))
                        effectDate = Convert.ToDateTime(effectDate).Date.AddYears(1).ToString("yyyy/MM/dd");
                }

                js = JsonConvert.SerializeObject(effectDate, Formatting.Indented);
            }
            catch
            {
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        public object[] getAttendCard(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select A.ACTUAL_ROTE_ID as ROTE_ID,C.ROTE_CODE,D.ROTE_ID AS UPPER_ROTE_ID,D.ROTE_CODE AS UPPER_ROTE_CODE,D.ROTE_CNAME AS UPPER_ROTE_CNAME,B.ON_TIME_TRAN,B.OFF_TIME_TRAN," + "\r\n";
                sql = sql + "isnull(A.HOLIDAY_ROTE_ID,A.ACTUAL_ROTE_ID) as HOLIDAY_ROTE_ID," + "\r\n";
                sql = sql + "case when E.ROTEMAPPING_CODE IS NULL then 0 else 1 end as IS_HOLIDAY" + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ATTEND_CARD B on A.EMPLOYEE_ID = B.EMPLOYEE_ID and A.ATTEND_DATE = B.CARD_DATE" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE C on A.ACTUAL_ROTE_ID = C.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on A.HOLIDAY_ROTE_ID = D.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTEMAPPING_DETAIL E on A.ACTUAL_ROTE_ID = E.ROTE_ID and (ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday' or ROTEMAPPING_CODE = 'ChangeHoliday')" + "\r\n";
                //sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                //sql = sql + "left join HRM_ATTEND_ROTEMAPPING_DETAIL E on A.ROTE_ID = E.ROTE_ID" + "\r\n";
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] getIsOvertimeDept(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select top 1 isnull(IS_OVERTIME_DEPT,'N') IS_OVERTIME_DEPT,BTS.DEPTC_ID,DEPTC_CODE from HRM_ATTEND_OVERTIME_CONFIG " + "\r\n";
                sql = sql + "join [dbo].[HRM_BASE_BASETTS] as BTS on HRM_ATTEND_OVERTIME_CONFIG.COMPANY_ID=BTS.COMPANY_ID" + "\r\n";
                sql = sql + "join HRM_DEPTC on BTS.DEPTC_ID=HRM_DEPTC.DEPTC_ID" + "\r\n";
                sql = sql + "Where EMPLOYEE_ID='" + employeeID + "' AND BTS.EFFECT_DATE <= '" + overtimeDate + "'" + "\r\n";
                sql = sql + "Order By BTS.EFFECT_DATE Desc" + "\r\n";

                DataSet dsHRM_ATTEND_OVERTIME_CONFIG = this.ExecuteSql(sql, connection, transaction);

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0], Formatting.Indented);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] CountMonthOvertimeHour(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string overtimeDate = parm[1];
            string overtimeHour = parm[2];
            string checkStatus = parm[3];

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select top 1 isnull(IS_CHECK_MONTH_OVERTIME_HOUR,'N') IS_CHECK_MONTH_OVERTIME_HOUR,isnull(IS_SHOW_MONTH_OVERTIME_HOUR,'N') IS_SHOW_MONTH_OVERTIME_HOUR,ALLOW_MONTH_OVERTIME_HOUR,ATTEND_CLOSE_DAY,MONTH_HOUR_DATE_TYPE" + "\r\n";
                sql = sql + "from HRM_ATTEND_OVERTIME_CONFIG " + "\r\n";
                sql = sql + "join [dbo].[HRM_BASE_BASETTS] as BTS on HRM_ATTEND_OVERTIME_CONFIG.COMPANY_ID=BTS.COMPANY_ID" + "\r\n";
                sql = sql + "Where EMPLOYEE_ID='" + employeeID + "' AND BTS.EFFECT_DATE <= '" + overtimeDate + "'" + "\r\n";
                sql = sql + "Order By BTS.EFFECT_DATE Desc" + "\r\n";
                DataSet dsHRM_ATTEND_OVERTIME_CONFIG = this.ExecuteSql(sql, connection, transaction);

                DateTime OvertimeDate = Convert.ToDateTime(overtimeDate);
                DateTime DayStart;
                //ATTEND_CLOSE_DAY等於31，等於設定是月底
                //日曆類別calendar:日曆天1號開始；Attend:出勤結算日開始
                if (dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["MONTH_HOUR_DATE_TYPE"].ToString() == "calendar" ||
                    dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ATTEND_CLOSE_DAY"].ToString() == "31")
                    DayStart = Convert.ToDateTime(OvertimeDate.Year + "/" + OvertimeDate.Month + "/1");
                else
                    DayStart = Convert.ToDateTime(OvertimeDate.Year + "/" + OvertimeDate.Month + "/" + dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ATTEND_CLOSE_DAY"].ToString()).AddMonths(-1).AddDays(1);
                DateTime DayEnd = DayStart.AddMonths(1).AddDays(-1);

                //sql = "select SUM(TOTAL_HOURS) COUNT_HOURS from HRM_ATTEND_OVERTIME_DATA" + "\r\n";
                //sql = sql + "Where EMPLOYEE_ID='" + employeeID + "' AND OVERTIME_DATE>='" + DayStart.ToString("yyyy/MM/dd") + "' AND OVERTIME_DATE<='" + DayEnd.ToString("yyyy/MM/dd") + "'" + "\r\n";
                sql = sql + "select SUM(COUNT_HOURS) COUNT_HOURS from (" + "\r\n";
                sql = sql + "	select SUM(TOTAL_HOURS) COUNT_HOURS from HRM_ATTEND_OVERTIME_DATA" + "\r\n";
                sql = sql + "	Where EMPLOYEE_ID='" + employeeID + "' AND OVERTIME_DATE>='" + DayStart.ToString("yyyy/MM/dd") + "' AND OVERTIME_DATE<='" + DayEnd.ToString("yyyy/MM/dd") + "'" + "\r\n";
                sql = sql + "	union all" + "\r\n";
                sql = sql + "	select SUM(TOTAL_HOURS) COUNT_HOURS from HRM_ATTEND_OVERTIME_DATA_FLOW" + "\r\n";
                sql = sql + "	Where (FLOWFLAG='N' or FLOWFLAG='P') AND EMPLOYEE_ID='" + employeeID + "' AND OVERTIME_DATE>='" + DayStart.ToString("yyyy/MM/dd") + "' AND OVERTIME_DATE<='" + DayEnd.ToString("yyyy/MM/dd") + "'" + "\r\n";
                sql = sql + ") A" + "\r\n";
                DataSet dsHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction);

                decimal countHours = 0;
                bool result = Decimal.TryParse(dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["COUNT_HOURS"].ToString(), out countHours);
                if (result)
                    countHours = Convert.ToDecimal(dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["COUNT_HOURS"].ToString());

                decimal Count_Month = countHours + Convert.ToDecimal(overtimeHour);

                //檢查加班是否超過46小時
                if (checkStatus == "check")
                {
                    if (dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_CHECK_MONTH_OVERTIME_HOUR"].ToString() == "Y")
                    {
                        if (Count_Month > Convert.ToDecimal(dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["ALLOW_MONTH_OVERTIME_HOUR"].ToString()))
                            return new object[] { 0, false };
                        else
                            return new object[] { 0, true };
                    }
                    else
                        return new object[] { 0, true };
                }
                //顯示目前加班時數
                else if (checkStatus == "show")
                {
                    if (dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["IS_SHOW_MONTH_OVERTIME_HOUR"].ToString() == "Y")
                    {
                        return new object[] { 0, Count_Month };
                    }
                    else
                        return new object[] { 0, true };
                }
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, true };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
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
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select UPPER_MANAGER_HOUR,* from HRM_ATTEND_OVERTIME_CONFIG " + "\n\r";
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
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
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

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
                sql = "select * from HRM_ATTEND_ABSENT_PLUS where (ABSENT_PLUS_TYPE = 'Overtime' or ABSENT_PLUS_TYPE = 'Overtime_12') and ABSENT_PLUS_NO = '" + overtimeID + "'";
                DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction).Tables[0];
                if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                {
                    if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                    {
                        sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL " + "\r\n";
                        sql = sql + "where HOLIMAPPING_CODE = 'CompensationPlus'" + "\r\n";
                        DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                        holidayID = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["HOLIDAY_ID"].ToString();
                        dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

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

                            //補休得假資料檔得假剩餘時數(REST_HOURS)
                            sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
                            sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                            sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                            sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                            sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToString("yyyy/MM/dd") + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToString("yyyy/MM/dd") + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                            sql = sql + "and REST_HOURS >0" + "\r\n";
                            //if (editMode == "deleted")
                            sql = sql + "and A.ABSENT_PLUS_ID <> " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                            sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
                            sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            //return new object[] { 0, js };
            return new object[] { 0, flag };
        }

        public object[] checkOvertimeCause(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string overtimeID = parm[0];
            string overtimeDate = parm[1];
            string overtimeCauseId = parm[2];

            string sql = "";
            bool flag = true;
            string holidayID;

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

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

                sql = sql + "select HOLIDAY_TYPE_ID,OVERTIME_RATE_MASTER_ID from HRM_ATTEND_OVERTIME_CAUSE where OVERTIME_CAUSE_ID='" + overtimeCauseId + "'";

                sql = sql + "Declare	@EmployeeID nvarchar(50)	= '" + overtimeID + "'" + "\r\n";
                sql = sql + "Declare	@EffectDate	date			= '" + overtimeDate + "'	" + "\r\n";
                sql = sql + "Select	CALENDAR_HOLIDAY_DATE,NOT_CHECKCARD,OVERTIME_RATE_MASTER_ID" + "\r\n";
                sql = sql + "From	(" + "\r\n";
                sql = sql + "			Select	Top 1 *" + "\r\n";
                sql = sql + "			From	[dbo].[HRM_ATTEND_BASETTS]						as ATS		" + "\r\n";
                sql = sql + "			Where	ATS.EMPLOYEE_ID = @EmployeeID and EFFECT_DATE <= @EffectDate" + "\r\n";
                sql = sql + "			Order By ATS.EFFECT_DATE Desc" + "\r\n";
                sql = sql + "		) as T" + "\r\n";
                sql = sql + "		Inner Join [dbo].[HRM_ATTEND_CALENDAR_HOLIDAY]	as CAL on T.CALENDAR_ID = CAL.CALENDAR_ID and CAL.CALENDAR_HOLIDAY_DATE = @EffectDate" + "\r\n";
                sql = sql + "		JOIN HRM_ATTEND_OVERTIME_CAUSE ON HRM_ATTEND_OVERTIME_CAUSE.HOLIDAY_TYPE_ID=CAL.HOLIDAY_TYPE_ID" + "\r\n";
                sql = sql + " where OVERTIME_CAUSE_ID='" + overtimeCauseId + "'";

                DataSet dsHRM_ATTEND_OVERTIME_CAUSE = this.ExecuteSql(sql, connection, transaction);

                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                //未填入加班原因
                if (dsHRM_ATTEND_OVERTIME_CAUSE.Tables[0].Rows.Count == 0)
                {
                    dt.Rows.Add(new object[] { "Normal" });
                    js = JsonConvert.SerializeObject(dt, Formatting.Indented);
                }
                //加班原因沒有綁假日
                //else if (string.IsNullOrEmpty(dsHRM_ATTEND_OVERTIME_CAUSE.Tables[0].Rows[0]["HOLIDAY_TYPE_ID"].ToString()))
                //{
                //    dt.Rows.Add(new object[] { "Normal" });
                //    js = JsonConvert.SerializeObject(dt, Formatting.Indented);
                //}
                //有綁假日，但行事曆不符合(like颱風假需檢查)
                else if (!string.IsNullOrEmpty(dsHRM_ATTEND_OVERTIME_CAUSE.Tables[0].Rows[0]["HOLIDAY_TYPE_ID"].ToString()) && dsHRM_ATTEND_OVERTIME_CAUSE.Tables[1].Rows.Count == 0)
                {
                    dt.Rows.Add(new object[] { "NotUse" });
                    js = JsonConvert.SerializeObject(dt, Formatting.Indented);
                }
                else
                {
                    DataTable dtReturn = new DataTable();
                    if (dsHRM_ATTEND_OVERTIME_CAUSE.Tables[1].Rows.Count > 0)
                        dtReturn = dsHRM_ATTEND_OVERTIME_CAUSE.Tables[1];
                    else
                        dtReturn = dsHRM_ATTEND_OVERTIME_CAUSE.Tables[0];

                    //Indented縮排 將資料轉換成Json格式
                    js = JsonConvert.SerializeObject(dtReturn, Formatting.Indented);
                }

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, flag };
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
            string sql = "";
            bool flag = true;
            var overtimeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("OVERTIME_ID");
            var overtimeDate = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_DATE");
            var overtimeEffectDate = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_EFFECT_DATE");
            var beginTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("BEGIN_TIME");
            var endTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("END_TIME");
            decimal overtimeHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_HOURS").ToString());
            decimal restHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("REST_HOURS").ToString());
            var employeeID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("EMPLOYEE_ID");

            string overtimeRoteID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_ROTE_ID").ToString();
            string overtimeCauseId = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_CAUSE_ID").ToString();

            decimal o_restHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("REST_HOURS").ToString());
            var o_overtimeDate = Convert.ToDateTime(ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("OVERTIME_DATE").ToString()).Date;

            var o_beginTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("BEGIN_TIME").ToString();
            var o_endTime = ucHRM_ATTEND_OVERTIME_DATA.GetFieldOldValue("END_TIME").ToString();

            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("OVERTIME_DATE_TIME_BEGIN", DateAndTimeMerger(overtimeDate, beginTime));
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("OVERTIME_DATE_TIME_END", DateAndTimeMerger(overtimeDate, endTime));
            ucHRM_ATTEND_OVERTIME_DATA.SetFieldValue("TOTAL_HOURS", overtimeHours + restHours);

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            dsUSERS.Dispose();

            if (restHours != o_restHours)
            {
                sql = "select * from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_TYPE = 'Overtime' and ABSENT_PLUS_NO = '" + overtimeID + "'";
                DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL " + "\r\n";
                sql = sql + "where HOLIMAPPING_CODE = 'CompensationPlus'" + "\r\n";
                DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                int holidayID = int.Parse(dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
                dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

                if (restHours > o_restHours)
                {
                    if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                    {
                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set TOTAL_HOURS = " + restHours + ",REST_HOURS = " + restHours + " - ABSENT_HOURS,";
                        detailSql = detailSql + "BEGIN_DATE = '" + Convert.ToDateTime(overtimeDate.ToString()).ToString("yyyy/MM/dd") + "', END_DATE = '" + Convert.ToDateTime(overtimeEffectDate.ToString()).ToString("yyyy/MM/dd") + "'";
                        detailSql = detailSql + " where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_TRANS set ABSENT_HOURS = " + restHours + " where ABSENT_MINUS_ID is null and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
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

                        //if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                        //{
                        //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                        sql = "select * from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID <> ABSENT_MINUS_ID and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString();
                        DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                        //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                        sql = "";
                        detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_TYPE = 'Overtime' and ABSENT_PLUS_NO = '" + overtimeID + "'" + "\r\n";
                        detailSql = detailSql + "delete from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";

                        AbsentPlusSql = createAbsentPlusSql(userName, overtimeID.ToString(), employeeID.ToString(), Convert.ToDateTime(overtimeDate.ToString()).Date, Convert.ToDateTime(overtimeEffectDate.ToString()).Date, beginTime.ToString(), endTime.ToString(), restHours, holidayID);
                        detailSql = detailSql + AbsentPlusSql;

                        if (detailSql != "")
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
                            sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToString("yyyy/MM/dd") + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToString("yyyy/MM/dd") + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                            sql = sql + "and REST_HOURS >0" + "\r\n";
                            sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
                            sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

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
                        //}
                        if (!flag)
                            //出現錯誤訊息 ROLLBACK
                            this.ExecuteSql("update HRM_ATTEND_OVERTIME_DATA ERROR", ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    }
                }   //restHours < o_restHours
                if (detailSql != "")
                    this.ExecuteSql(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            }   //restHours != o_restHours

            //(迅得)判斷時間調整_12REST_HOURS
            sql = "select IS_ENABLE from HRM_SYSTEM_PARAMETER_MAPPING_MASTER where PARAMETER_MAPPING_CODE='Overtime12'" + "\r\n";
            DataTable dtMappingEnable = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];
            string MappingEnable = "N";
            if (dtMappingEnable.Rows.Count > 0)
                MappingEnable = dtMappingEnable.Rows[0]["IS_ENABLE"].ToString();

            //取得慰勞假代碼資料--SYMTEK 
            int ConsolationHolidayID = 0;
            sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'ConsolationHoliday'" + "\r\n";
            DataSet dsConsolationHoliday = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            if (dsConsolationHoliday.Tables[0].Rows.Count > 0)
                ConsolationHolidayID = int.Parse(dsConsolationHoliday.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
            dsConsolationHoliday.Dispose();

            if (MappingEnable == "Y" && (Convert.ToInt32(endTime) > 2400 && (Convert.ToDouble(overtimeHours) > 6.5 || Convert.ToInt32(beginTime) > 2400)))
            {
                //一律檢查是否需補休
                //if (Convert.ToInt32(beginTime.ToString()) != Convert.ToInt32(o_beginTime.ToString()) || Convert.ToInt32(endTime.ToString()) != Convert.ToInt32(o_endTime.ToString()))
                {
                    sql = "select * from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_TYPE = 'Overtime_12' and ABSENT_PLUS_NO = '" + overtimeID + "'";
                    DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                    sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL " + "\r\n";
                    sql = sql + "where HOLIMAPPING_CODE = 'CompensationPlus'" + "\r\n";
                    DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    int holidayID = int.Parse(dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
                    dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

                    string newBeginTime = Convert.ToInt32(beginTime.ToString()) < 2400 ? "2400" : beginTime.ToString();

                    //判斷加班時數
                    var parameters = new List<object>();
                    parameters.Add(overtimeID + "," + employeeID + "," + Convert.ToDateTime(overtimeDate).ToString("yyyy/MM/dd") + "," + newBeginTime + "," + endTime + "," + overtimeRoteID + "," + overtimeCauseId);
                    var obj = checkRoteOvertimeHours_Function(parameters.ToArray(), ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());

                    restHours = Convert.ToDecimal(dt.Rows[0]["totalHours"].ToString());
                    //補休最多8小時
                    if (restHours > 8) restHours = 8;

                    if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                        o_restHours = Convert.ToDecimal(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["TOTAL_HOURS"].ToString());
                    else
                        o_restHours = 0;

                    if (restHours > o_restHours)
                    {
                        if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                        {
                            DateTime endDate = Convert.ToDateTime(Convert.ToDateTime(overtimeEffectDate.ToString()).Year + "/12/31");

                            detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set TOTAL_HOURS = " + restHours + ",REST_HOURS = " + restHours + " - ABSENT_HOURS,";
                            detailSql = detailSql + "BEGIN_DATE = '" + Convert.ToDateTime(overtimeDate.ToString()).ToString("yyyy/MM/dd") + "', END_DATE = '" + endDate.ToString("yyyy/MM/dd") + "'";
                            detailSql = detailSql + " where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                            detailSql = detailSql + "update HRM_ATTEND_ABSENT_TRANS set ABSENT_HOURS = " + restHours + " where ABSENT_MINUS_ID is null and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                        }
                        else
                        {
                            if (Convert.ToInt32(endTime) > 2400 && (Convert.ToDouble(overtimeHours) > 6.5 || Convert.ToInt32(beginTime) > 2400))
                            {
                                AbsentPlusSql = createAbsentPlusSql_12(userName, overtimeID.ToString(), employeeID.ToString(), Convert.ToDateTime(overtimeDate.ToString()).Date, Convert.ToDateTime(overtimeEffectDate.ToString()).Date, beginTime.ToString(), endTime.ToString(), restHours, ConsolationHolidayID);
                                detailSql = detailSql + AbsentPlusSql;
                            }
                        }
                    }
                    else
                    {
                        if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
                        {

                            //if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                            //{
                            //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                            sql = "select * from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID <> ABSENT_MINUS_ID and ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString();
                            DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];

                            //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                            sql = "";
                            detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_TYPE = 'Overtime_12' and ABSENT_PLUS_NO = '" + overtimeID + "'" + "\r\n";
                            detailSql = detailSql + "delete from  HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";

                            if (Convert.ToInt32(endTime) > 2400 && (Convert.ToDouble(overtimeHours) > 6.5 || Convert.ToInt32(beginTime) > 2400))
                            {
                                AbsentPlusSql = createAbsentPlusSql_12(userName, overtimeID.ToString(), employeeID.ToString(), Convert.ToDateTime(overtimeDate.ToString()).Date, Convert.ToDateTime(overtimeEffectDate.ToString()).Date, beginTime.ToString(), endTime.ToString(), restHours, ConsolationHolidayID);
                                detailSql = detailSql + AbsentPlusSql;
                            }

                            if (detailSql != "")
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
                                sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToString("yyyy/MM/dd") + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToString("yyyy/MM/dd") + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                                sql = sql + "and REST_HOURS >0" + "\r\n";
                                sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
                                sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

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
                            //}
                            if (!flag)
                                //出現錯誤訊息 ROLLBACK
                                this.ExecuteSql("update HRM_ATTEND_OVERTIME_DATA ERROR", ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                        }
                    }   //restHours < o_restHours
                    if (detailSql != "")
                        this.ExecuteSql(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                }
            }
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
            string overtimeRoteID = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_ROTE_ID").ToString();
            string overtimeCauseId = ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_CAUSE_ID").ToString();
            decimal overtimeHours = decimal.Parse(ucHRM_ATTEND_OVERTIME_DATA.GetFieldCurrentValue("OVERTIME_HOURS").ToString());

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();
            int ConsolationHolidayID = 0;
            dsUSERS.Dispose();

            sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL " + "\r\n";
            sql = sql + "where HOLIMAPPING_CODE = 'CompensationPlus'" + "\r\n";
            DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            holidayID = int.Parse(dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
            dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

            //取得慰勞假代碼資料--SYMTEK 
            sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'ConsolationHoliday'" + "\r\n";
            DataSet dsConsolationHoliday = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            if (dsConsolationHoliday.Tables[0].Rows.Count > 0)
                ConsolationHolidayID = int.Parse(dsConsolationHoliday.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
            dsConsolationHoliday.Dispose();

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
                logInfo_HRM_ATTEND_OVERTIME_DATA.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans, ucHRM_ATTEND_OVERTIME_DATA.SelectCmd.KeyFields);
            }

            //判斷補休時數大於0, 需產生一筆新的補休得假資料
            if (restHours > 0)
            {
                AbsentPlusSql = createAbsentPlusSql(userName, newID.ToString(), employeeID, overtimeDate, overtimeEffectDate, beginTime, endTime, restHours, holidayID);
                this.ExecuteSql(AbsentPlusSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
            }

            //(迅得)加班超過凌晨12點就依時數多加補休時數_12
            sql = "select IS_ENABLE from HRM_SYSTEM_PARAMETER_MAPPING_MASTER where PARAMETER_MAPPING_CODE='Overtime12'" + "\r\n";
            DataTable dtMappingEnable = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];
            string MappingEnable = dtMappingEnable.Rows[0]["IS_ENABLE"].ToString();

            if (MappingEnable == "Y")
            {
                //下班超過凌晨12點並加班超過6.5小時 or 上班下班都為凌晨12點之後者
                if (Convert.ToInt32(endTime) > 2400 && (Convert.ToDouble(overtimeHours) > 6.5 || Convert.ToInt32(beginTime) > 2400))
                {
                    string newBeginTime = Convert.ToInt32(beginTime) < 2400 ? "2400" : beginTime;

                    //判斷加班時數
                    var parameters = new List<object>();
                    parameters.Add("0" + "," + employeeID + "," + overtimeDate.ToString("yyyy/MM/dd") + "," + newBeginTime + "," + endTime + "," + overtimeRoteID + "," + overtimeCauseId);
                    var obj = checkRoteOvertimeHours_Function(parameters.ToArray(), ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());

                    restHours = Convert.ToDecimal(dt.Rows[0]["totalHours"].ToString());
                    //補休最多8小時
                    if (restHours > 8) restHours = 8;

                    string AbsentPlusSql_12 = "";
                    AbsentPlusSql_12 = createAbsentPlusSql_12(userName, newID.ToString(), employeeID, overtimeDate, overtimeEffectDate, beginTime, endTime, restHours, ConsolationHolidayID);
                    this.ExecuteSql(AbsentPlusSql_12, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                }
            }
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            var table = (string)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            DataTable dt = ucHRM_ATTEND_OVERTIME_DATA.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_HRM_ATTEND_OVERTIME_DATA.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans, ucHRM_ATTEND_OVERTIME_DATA.SelectCmd.KeyFields);
            }
        }

        private void ucHRM_ATTEND_OVERTIME_DATA_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            var table = (string)ucHRM_ATTEND_OVERTIME_DATA.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_OVERTIME_DATA);
            DataTable dt = ucHRM_ATTEND_OVERTIME_DATA.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_HRM_ATTEND_OVERTIME_DATA.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans, ucHRM_ATTEND_OVERTIME_DATA.SelectCmd.KeyFields);
            }
        }

        private string createAbsentPlusSql(string userName, string newID, string employeeID, DateTime overtimeDate, DateTime overtimeEffectDate, string beginTime, string endTime, decimal restHours, int holidayID)
        {
            string detailSql = "";

            if (restHours > 0)
            {
                detailSql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n"; ;
                detailSql = detailSql + "select '" + employeeID + "','" + overtimeDate.ToString("yyyy/MM/dd") + "','" + overtimeEffectDate.ToString("yyyy/MM/dd") + "'," + holidayID + "," + restHours + ",null,0," + restHours + ",''," + newID + ",'Overtime','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                detailSql = detailSql + "declare @absentPlusID int " + "\r\n";
                detailSql = detailSql + "set @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                detailSql = detailSql + "select @absentPlusID,null, null, " + restHours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
            }

            return detailSql;
        }

        private string createAbsentPlusSql_12(string userName, string newID, string employeeID, DateTime overtimeDate, DateTime overtimeEffectDate, string beginTime, string endTime, decimal restHours, int holidayID)
        {
            string detailSql = "";

            DateTime endDate = Convert.ToDateTime(overtimeDate.Year + "/12/31");

            detailSql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n"; ;
            detailSql = detailSql + "select '" + employeeID + "','" + overtimeDate.ToString("yyyy/MM/dd") + "','" + endDate.ToString("yyyy/MM/dd") + "'," + holidayID + "," + restHours + ",null,0," + restHours + ",''," + newID + ",'Overtime_12','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
            detailSql = detailSql + "declare @absentPlusID int " + "\r\n";
            detailSql = detailSql + "set @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
            detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
            detailSql = detailSql + "select @absentPlusID,null, null, " + restHours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";

            return detailSql;
        }

        private string createAbsentPlusSql_I(string userName, string newID, string employeeID, DateTime overtimeDate, DateTime overtimeEffectDate, string beginTime, string endTime, decimal restHours, int holidayID)
        {
            string detailSql = "";

            if (restHours > 0)
            {
                detailSql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n"; ;
                detailSql = detailSql + "select '" + employeeID + "','" + overtimeDate.ToString("yyyy/MM/dd") + "','" + overtimeEffectDate.ToString("yyyy/MM/dd") + "'," + holidayID + "," + restHours + ",null,0," + restHours + ",''," + newID + ",'Overtime','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                //detailSql = detailSql + "declare @absentPlusID int " + "\r\n";
                detailSql = detailSql + "set @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                detailSql = detailSql + "select @absentPlusID,null, null, " + restHours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
            }

            return detailSql;
        }

        private string createAbsentPlusSql_I_12(string userName, string newID, string employeeID, DateTime overtimeDate, DateTime overtimeEffectDate, string beginTime, string endTime, decimal restHours, int holidayID)
        {
            string detailSql = "";

            DateTime endDate = Convert.ToDateTime(overtimeDate.Year + "/12/31");

            detailSql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n"; ;
            detailSql = detailSql + "select '" + employeeID + "','" + overtimeDate.ToString("yyyy/MM/dd") + "','" + endDate.ToString("yyyy/MM/dd") + "'," + holidayID + "," + restHours + ",null,0," + restHours + ",''," + newID + ",'Overtime_12','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
            //detailSql = detailSql + "declare @absentPlusID int " + "\r\n";
            detailSql = detailSql + "set @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
            detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
            detailSql = detailSql + "select @absentPlusID,null, null, " + restHours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";

            return detailSql;
        }

        #region =================================匯入Excel功能=====================================
        //Excel檔案匯入
        public object[] ExcelFileImport(object[] objParam)
        {
            //回傳
            var theResult = new Dictionary<string, object>();
            theResult.Add("IsOK", false);
            theResult.Add("Msg", "錯誤了唷");

            try
            {
                //檔案
                var MemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //SheetIndex
                var SheetIndex = (int)(objParam[1]);

                //標題
                var Title = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[2].ToString());

                //參數
                var Parameter = objParam[3].ToString();

                var aExcelFileImportResult = ExcelFileUpload(MemoryStream, SheetIndex, Title);

                theResult["IsOK"] = aExcelFileImportResult.IsOK;
                theResult["Msg"] = aExcelFileImportResult.ErrorMsg;
                if (aExcelFileImportResult.Result != null) theResult["File"] = (MemoryStream)aExcelFileImportResult.Result;
            }
            catch (TheUserDefinedException ex)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = ex.Message;
            }
            catch (Exception)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = "執行錯誤";
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }

        public TheJsonResult ExcelFileUpload(MemoryStream FileStream, int SheetIndex, Dictionary<string, object> Title)
        {
            //【取得ColumnName】：將前台輸入的資料轉換成ColumnName
            var aHeadList = FileUploadGetHeadList(Title);

            //【抓資料】：ColumnName結合成新的DataTable                
            DataTable FileData = NPOIHelper.GetDataTableFromSheet(new MemoryStream(FileStream.ToArray()), SheetIndex, aHeadList);
            if (FileData == null) throw new TheUserDefinedException("【資料讀取】Error");

            //【欄位驗證】、【欄位轉換】：比對資料庫是否有這個數值，有就將數值轉換(Code->ID)
            if (!DataTableCellValidate(FileData)) throw new TheUserDefinedException("【欄位驗證】Error");
            if (FileData.HasErrors)
            {
                return new TheJsonResult
                {
                    IsOK = false,
                    ErrorMsg = "欄位驗證錯誤",
                    Result = NPOIHelper.SetErrorMemoToSheet(FileData, new MemoryStream(FileStream.ToArray()), SheetIndex, aHeadList)
                };
            }

            FileData.Columns.Add("OVERTIME_RATE_MASTER_ID");
            //【規則驗證】：整體驗證
            if (!DataTableDataValidate(FileData)) throw new TheUserDefinedException("【資料驗證】Error");
            if (FileData.HasErrors)
            {
                return new TheJsonResult
                {
                    IsOK = false,
                    ErrorMsg = "資料驗證錯誤",
                    Result = NPOIHelper.SetErrorMemoToSheet(FileData, new MemoryStream(FileStream.ToArray()), SheetIndex, aHeadList)
                };
            }

            //寫入
            if (!DataTableDataInsert(FileData)) throw new Exception("【資料寫入】Error");

            return new TheJsonResult { IsOK = true, ErrorMsg = "執行成功" };
        }

        /// <summary>Row是否有輸入</summary>        
        protected bool DataRowHasValue(DataRow aRow)
        {
            return aRow.ItemArray.Any(m => m.ToString() != "");
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
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_TIME", DisplayName = "加班起始時間", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Time48Hours_60, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_TIME", DisplayName = "加班截止時間", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Time48Hours_60, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_HOURS", DisplayName = "加班時數", IsRequired = true, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Decimal });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "REST_HOURS", DisplayName = "補休時數", IsRequired = true, IsUserCheck = false, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Decimal });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "TOTAL_HOURS", DisplayName = "加班總時數", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Decimal, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_CAUSE_ID", DisplayName = "加班原因代碼", IsRequired = false, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_ROTE_ID", DisplayName = "加班班別代碼", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_DEPT_ID", DisplayName = "加班部門代碼", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "OVERTIME_EFFECT_DATE", DisplayName = "加班有效日期", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "SALARY_YYMM", DisplayName = "計薪年月", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.YearMonth, IsUserCheck = false });

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
                };

                foreach (DataRow aRow in aTable.Rows)
                {
                    //如果都沒有輸入就不用檢測了
                    if (!DataRowHasValue(aRow)) continue;

                    string EmployeeId = "";
                    string OvertimeDate = "";
                    string OvertimeDept = "";
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

                            if (aCheckData.FieldName == "EMPLOYEE_ID")
                                EmployeeId = aCheckData.TheResult.ToString();
                            if (aCheckData.FieldName == "OVERTIME_DATE")
                                OvertimeDate = aCheckData.TheResult.ToString();
                            if (aCheckData.FieldName == "OVERTIME_DEPT_ID")
                                OvertimeDept = aCheckData.TheResult.ToString();
                        }
                    });
                    aRow.RowError = aRow.HasErrors ? "驗證錯誤" : "";

                    if (aRow.HasErrors)
                        continue;
                    ////--------20170616 若驗證錯誤就不進入下方程式
                    //建立資料庫連結
                    IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

                    //當連線狀態不等於open時，開啟連結
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    //開始transaction
                    IDbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        ////決定是否檢查員工配對的編制部門
                        string sql = "select top 1 isnull(IS_OVERTIME_DEPT,'Y') IS_OVERTIME_DEPT,DEPT_ID from HRM_ATTEND_OVERTIME_CONFIG " + "\r\n";
                        sql = sql + "join [dbo].[HRM_BASE_BASETTS] as BTS on HRM_ATTEND_OVERTIME_CONFIG.COMPANY_ID=BTS.COMPANY_ID" + "\r\n";
                        sql = sql + "Where BTS.EFFECT_DATE <= '" + OvertimeDate + "' and BTS.EMPLOYEE_ID='" + EmployeeId + "'" + "\r\n";
                        sql = sql + "Order By BTS.EFFECT_DATE Desc" + "\r\n";
                        sql = sql + "select DEPTC_ID,DEPTC_CODE,DEPTC_CNAME FROM HRM_DEPTC" + "\n\r";
                        sql = sql + "select DEPT_ID,DEPT_CODE,DEPT_CNAME FROM HRM_DEPT" + "\n\r";

                        DataSet dsCheckOvertimeRote = this.ExecuteSql(sql, connection, transaction);

                        if (dsCheckOvertimeRote.Tables[0].Rows[0]["IS_OVERTIME_DEPT"].ToString() == "Y")
                        {
                            DataRow[] check = dsCheckOvertimeRote.Tables[2].Select("DEPT_CODE='" + OvertimeDept + "' OR DEPT_CNAME='" + OvertimeDept + "'");
                            if (check.Count() < 1)
                            {
                                aRow.SetColumnError("OVERTIME_DEPT_ID", "編制部門沒有相對應資料");
                                continue;
                            }

                            //檢查加班部門是否為編制部門
                            if (check[0]["DEPT_ID"].ToString() != dsCheckOvertimeRote.Tables[0].Rows[0]["DEPT_ID"].ToString())
                            {
                                aRow.SetColumnError("OVERTIME_DEPT_ID", "員工不屬於此編制部門");
                                continue;
                            }

                            check = dsCheckOvertimeRote.Tables[1].Select("DEPTC_CODE='" + OvertimeDept + "' OR DEPTC_CNAME='" + OvertimeDept + "'");
                            if (check.Count() < 1)
                                aRow.SetColumnError("OVERTIME_DEPT_ID", "加班部門沒有相對應資料");
                            else
                                aRow["OVERTIME_DEPT_ID"] = check[0]["DEPTC_ID"].ToString();
                        }
                        else
                        {
                            DataRow[] check = dsCheckOvertimeRote.Tables[1].Select("DEPTC_CODE='" + OvertimeDept + "' OR DEPTC_CNAME='" + OvertimeDept + "'");
                            if (check.Count() < 1)
                                aRow.SetColumnError("OVERTIME_DEPT_ID", "加班部門沒有相對應資料");
                            else
                                aRow["OVERTIME_DEPT_ID"] = check[0]["DEPTC_ID"].ToString();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                    }
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
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            DataTableCellValidateData aAns = new DataTableCellValidateData();
            try
            {
                string sql = "select EMPLOYEE_ID, EMPLOYEE_CODE, NAME_C from HRM_BASE_BASE" + "\n\r";
                sql = sql + "select ROTE_ID,ROTE_CODE,ROTE_CNAME FROM HRM_ATTEND_ROTE" + "\n\r";
                sql = sql + "select OVERTIME_CAUSE_ID,OVERTIME_CAUSE_CODE,OVERTIME_CAUSE_CNAME FROM HRM_ATTEND_OVERTIME_CAUSE" + "\n\r";

                DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

                aAns.EmployeeData = DataSet.Tables[0].AsEnumerable().Select(m => new EmployeeData { ID = m.Field<string>("EMPLOYEE_ID"), Code = m.Field<string>("EMPLOYEE_CODE"), Name = m.Field<string>("NAME_C") }).ToList();
                aAns.RoteData = DataSet.Tables[1].AsEnumerable().Select(m => new RoteData { ID = m.Field<int>("ROTE_ID"), Code = m.Field<string>("ROTE_CODE"), Name = m.Field<string>("ROTE_CNAME") }).ToList();
                aAns.OverTimeCauseData = DataSet.Tables[2].AsEnumerable().Select(m => new OverTimeCauseData { ID = m.Field<int>("OVERTIME_CAUSE_ID"), Code = m.Field<string>("OVERTIME_CAUSE_CODE"), Name = m.Field<string>("OVERTIME_CAUSE_CNAME") }).ToList();

                return aAns;
            }
            catch
            {
                return new DataTableCellValidateData();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
        }

        //---------------------------------------資料驗證-----------------------------------------
        private bool DataTableDataValidate(DataTable aTable)
        {
            string employeeID = "";
            string overtimeDate = "";
            string overtimeEffectDate = "";
            string beginTime = "";
            string endTime = "";
            string overtimeHours = "";
            string restHours = "";
            string totalHours = "";
            string salaryYYMM = "";
            string totalHoursMsg = "";
            string overtimeRoteID = "";
            string overtimeCauseId = "";
            int cnt = 0;
            string sql = "";

            bool flag = false;
            bool checkFlag = true;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            string[] groupid = GetClientInfo(ClientInfoType.GroupID).ToString().Split(';');
            string groupList = "'" + string.Join("','", groupid) + "'";

            //判斷群組資料權限
            //1. 判斷加班起始時間不可大於截止時間
            //2. 加班時數及補休時數只能擇一申請
            //3. 判斷加班日期是否已有鎖檔紀錄
            //4. 判斷加班時數
            //5. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
            try
            {
                sql = "select A.EMPLOYEE_ID,A.ATTEND_GROUP_ID" + "\r\n";
                sql = sql + "from [dbo].[dtHRM_BASE_BASETTS_DateAndMan](null,getdate()) A" + "\r\n";
                //sql = sql + "left join HRM_BASE_BASE B on A.EMPLOYEE_ID = B.EMPLOYEE_ID" + "\r\n";

                DataTable dtEmployee = this.ExecuteSql(sql, connection, transaction).Tables[0];
                dtEmployee.PrimaryKey = new DataColumn[] { dtEmployee.Columns["EMPLOYEE_ID"] };

                foreach (DataRow aRow in aTable.Rows)
                {
                    //如果都沒有輸入就不用檢測了
                    if (!DataRowHasValue(aRow)) continue;

                    flag = false;
                    employeeID = aRow["EMPLOYEE_ID"].ToString();
                    overtimeDate = Convert.ToDateTime(aRow["OVERTIME_DATE"].ToString()).ToString("yyyy/MM/dd");
                    overtimeRoteID = aRow["OVERTIME_ROTE_ID"].ToString();
                    beginTime = aRow["BEGIN_TIME"].ToString();
                    endTime = aRow["END_TIME"].ToString();
                    overtimeHours = aRow["OVERTIME_HOURS"].ToString();
                    restHours = aRow["REST_HOURS"].ToString();
                    totalHours = aRow["TOTAL_HOURS"].ToString();
                    //overtimeCauseID = aRow["OVERTIME_CAUSE_ID"].ToString();
                    //overtimeRoteID = aRow["OVERTIME_ROTE_ID"].ToString();
                    //overtimeDeptID = aRow["OVERTIME_DEPT_ID"].ToString();
                    overtimeEffectDate = aRow["OVERTIME_EFFECT_DATE"].ToString();
                    salaryYYMM = aRow["SALARY_YYMM"].ToString();
                    overtimeCauseId = aTable.Columns.Contains("OVERTIME_CAUSE_ID") ? aRow["OVERTIME_CAUSE_ID"].ToString() : "";

                    //判斷群組資料權限
                    if (!groupList.Contains("ATTEND_ADMIN"))
                    {
                        DataRow drEmployee = dtEmployee.Rows.Find(employeeID);
                        if (drEmployee == null || !groupList.Contains(drEmployee["ATTEND_GROUP_ID"].ToString()))
                        {
                            aRow.SetColumnError("EMPLOYEE_ID", "沒有權限作匯入動作");
                            flag = true;
                        }
                    }

                    //1. 判斷加班起始時間不可大於截止時間
                    if (int.Parse(beginTime) >= int.Parse(endTime))
                    {
                        aRow.SetColumnError("BEGIN_TIME", "加班起始時間不可大於截止時間");
                        flag = true;
                    }

                    ////3. 判斷加班日期是否已有鎖檔紀錄
                    //sql = " select COUNT(*) AS cnt from HRM_ATTEND_DATA_LOCK " + "\r\n";
                    //sql = sql + " where ATTEND_DATE = '" + overtimeDate + "'";

                    //DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);
                    //cnt = int.Parse(dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["cnt"].ToString());
                    //dsHRM_ATTEND_DATA_LOCK.Dispose();

                    //if (cnt > 0)
                    //{
                    //    aRow.SetColumnError("OVERTIME_DATE", "此區間加班日期已有鎖檔紀錄");
                    //    flag = true;
                    //}

                    //檢查加班原因
                    object[] OvertimeCause = checkOvertimeCause(new object[] { employeeID + "," + overtimeDate + "," + overtimeCauseId });
                    var dtOvertimeCause = JsonConvert.DeserializeObject<DataTable>(OvertimeCause[1].ToString());
                    if (dtOvertimeCause.Columns.Count == 1)
                    {
                        if (dtOvertimeCause.Rows[0]["Message"].ToString() == "NotUse")
                            aRow.SetColumnError("OVERTIME_CAUSE_ID", "請假原因條件不符合");
                    }
                    else if (dtOvertimeCause.Columns.Count > 1)
                        aRow["OVERTIME_RATE_MASTER_ID"] = dtOvertimeCause.Rows[0]["OVERTIME_RATE_MASTER_ID"].ToString();

                    //3.1. 判斷計薪年月
                    var parameters = new List<object>();
                    parameters.Add(employeeID + "," + overtimeDate);
                    var obj = getSalaryYYMM(parameters.ToArray());
                    DataTable dtsalaryYYMM = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());
                    if (dtsalaryYYMM.Rows.Count > 0)
                    {
                        if (!dtsalaryYYMM.Rows[0]["SALARY_YYMM"].ToString().Equals(salaryYYMM))
                        {
                            totalHoursMsg = "其計薪年月應為 " + dtsalaryYYMM.Rows[0]["SALARY_YYMM"].ToString();
                            aRow.SetColumnError("SALARY_YYMM", totalHoursMsg);
                            flag = true;
                        }
                    }

                    //4. 判斷加班時數
                    parameters = new List<object>();
                    //判斷須以30分鐘為計算單位
                    //if (beginTime.Substring(2, 2) != "30" && beginTime.Substring(2, 2) != "00")
                    //{
                    //    beginTime = beginTime.Substring(0, 2) + String.Format("{0:00}", (int.Parse(beginTime.Substring(2, 2)) + (30 - int.Parse(beginTime.Substring(2, 2)) % 30)));
                    //    if (beginTime.Substring(2, 2) == "60")
                    //        beginTime = String.Format("{0:00}", int.Parse(beginTime.Substring(0, 2)) + 1) + "00";
                    //}

                    //endTime = endTime.Substring(0, 2) + String.Format("{0:00}", (int.Parse(endTime.Substring(2, 2)) - int.Parse(endTime.Substring(2, 2)) % 30));

                    parameters.Add("0" + "," + employeeID + "," + overtimeDate + "," + beginTime + "," + endTime + "," + overtimeRoteID + "," + overtimeCauseId);
                    obj = checkRoteOvertimeHours(parameters.ToArray());
                    if (obj[1].GetType() == typeof(bool) && (bool)obj[1] == false)
                    {
                        aRow.SetColumnError("EMPLOYEE_ID", "找不到出勤班表資料");
                        flag = true;
                        continue;
                    }
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
                                case "7": aRow.SetColumnError("OVERTIME_HOURS", "加班時數共計 : " + dt.Rows[0]["hours"].ToString() + "小時, 超過限制時數"); break;
                                case "8": aRow.SetColumnError("OVERTIME_DATE", "加班時間不可為國外出差第一天或最後一天"); break;
                            }
                            flag = true;
                        }
                        else
                        {
                            if (decimal.Parse(dt.Rows[0]["hours"].ToString()) == 0)
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
                            else if (decimal.Parse(totalHours) != decimal.Parse(overtimeHours) + decimal.Parse(restHours))
                            {
                                totalHoursMsg = "加班總時數應為 " + (decimal.Parse(overtimeHours) + decimal.Parse(restHours)).ToString() + "小時";
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
                        totalHoursMsg = "申請的時段內已有存在的加班資料";
                        aRow.SetColumnError("TOTAL_HOURS", totalHoursMsg);
                        flag = true;
                    }

                    //6. 判斷加班效期不可小於加班日期
                    if (Convert.ToDateTime(overtimeDate).CompareTo(Convert.ToDateTime(overtimeEffectDate)) > 0)
                    {
                        aRow.SetColumnError("OVERTIME_EFFECT_DATE", "加班有效日期不可小於加班日期");
                        flag = true;
                        continue;
                    }

                    if (flag)
                        aRow.RowError = "驗證錯誤";
                }

                DataView view = new System.Data.DataView(aTable);
                DataTable dtEmployeeId = view.ToTable(true, "EMPLOYEE_ID");
                foreach (DataRow aRow in dtEmployeeId.Rows)
                {
                    //如果都沒有輸入就不用檢測了
                    if (aRow["EMPLOYEE_ID"].ToString() == "") continue;

                    DataRow[] drSum = aTable.Select("EMPLOYEE_ID='" + aRow["EMPLOYEE_ID"].ToString() + "'");
                    decimal intSum = 0;
                    for (int i = 0; i < drSum.Count(); i++)
                    {
                        intSum += Convert.ToDecimal(drSum[i]["TOTAL_HOURS"].ToString());
                    }

                    //檢查是否檢查月累計限制
                    object[] MonthOvertimeHour = CountMonthOvertimeHour(new object[] { aRow["EMPLOYEE_ID"].ToString() + "," + overtimeDate + "," + intSum + ",check" });
                    if (!Convert.ToBoolean(MonthOvertimeHour[1]))
                    {
                        drSum[0].SetColumnError("TOTAL_HOURS", "加班時數超過月累計加班時數限制");
                    }
                }

                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //---------------------------------------寫入資料庫---------------------------------------
        private bool DataTableDataInsert(DataTable aTable)
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string sql = "";
            string configSql = "";
            string detailSql = "";
            string newID = "@overtimeID";
            int columnCount = 0;
            int holidayID;
            int ConsolationHolidayID = 0;

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";

                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                //取得補休代碼資料
                configSql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'CompensationPlus'" + "\r\n";
                DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(configSql, connection, transaction);
                holidayID = int.Parse(dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
                dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

                //取得慰勞假代碼資料--SYMTEK 
                configSql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'ConsolationHoliday'" + "\r\n";
                DataSet dsConsolationHoliday = this.ExecuteSql(configSql, connection, transaction);
                if (dsConsolationHoliday.Tables[0].Rows.Count > 0)
                    ConsolationHolidayID = int.Parse(dsConsolationHoliday.Tables[0].Rows[0]["HOLIDAY_ID"].ToString());
                dsConsolationHoliday.Dispose();

                //(迅得)加班超過凌晨12點就依時數多加補休時數_12
                sql = "select IS_ENABLE from HRM_SYSTEM_PARAMETER_MAPPING_MASTER where PARAMETER_MAPPING_CODE='Overtime12'" + "\r\n";
                DataTable dtMappingEnable = this.ExecuteSql(sql, connection, transaction).Tables[0];
                string MappingEnable = dtMappingEnable.Rows[0]["IS_ENABLE"].ToString();

                sql = "declare @overtimeID int " + "\r\n";
                sql = sql + "declare @absentPlusID int" + "\r\n";
                foreach (DataRow aRow in aTable.Rows)
                {
                    //如果都沒有輸入就不用新增
                    if (!DataRowHasValue(aRow)) continue;

                    columnCount = aRow.ItemArray.Length;
                    sql = sql + "insert into HRM_ATTEND_OVERTIME_DATA (";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + column.ColumnName + ",";
                    }
                    sql = sql + " OVERTIME_DATE_TIME_BEGIN,OVERTIME_DATE_TIME_END,NOT_ALLOW_MODIFY,SYSTEM_CREATE,IS_IMPORT,CREATE_EMPLOYEE_ID,CREATE_MAN,CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                    sql = sql + " select '";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + (aRow[column].ToString().Trim() == "" ? "null" : aRow[column].ToString().Trim()) + "','";
                    }
                    sql = sql + DateAndTimeMerger(Convert.ToDateTime(aRow["OVERTIME_DATE"]).Date, aRow["BEGIN_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','";
                    sql = sql + DateAndTimeMerger(Convert.ToDateTime(aRow["OVERTIME_DATE"]).Date, aRow["END_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','";
                    sql = sql + "N','N','Y','";
                    sql = sql + userid + "','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";

                    sql = sql + "select @overtimeID = SCOPE_IDENTITY()" + "\r\n";
                    if (decimal.Parse(aRow["REST_HOURS"].ToString()) > 0)
                    {
                        //取得補休效期
                        var parameters = new List<object>();
                        parameters.Add(aRow["EMPLOYEE_ID"].ToString() + "," + Convert.ToDateTime(aRow["OVERTIME_DATE"]).ToString("yyyy/MM/dd"));
                        var obj = getEffectDate(parameters.ToArray());
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());
                        string overtimeEffectDate = dt.Rows[0]["effectDate"].ToString();

                        if (Convert.ToDateTime(aRow["OVERTIME_EFFECT_DATE"]).Date < Convert.ToDateTime(overtimeEffectDate).Date)
                            overtimeEffectDate = aRow["OVERTIME_EFFECT_DATE"].ToString();

                        detailSql = createAbsentPlusSql_I(userName, newID, aRow["EMPLOYEE_ID"].ToString(), Convert.ToDateTime(aRow["OVERTIME_DATE"]).Date, Convert.ToDateTime(overtimeEffectDate).Date, aRow["BEGIN_TIME"].ToString(), aRow["END_TIME"].ToString(), decimal.Parse(aRow["REST_HOURS"].ToString()), holidayID);
                        sql = sql + detailSql;
                    }

                    if (MappingEnable == "Y")
                    {
                        if (Convert.ToInt32(aRow["END_TIME"].ToString()) > 2400 && (Convert.ToDouble(aRow["OVERTIME_HOURS"].ToString()) > 6.5 || Convert.ToInt32(aRow["BEGIN_TIME"].ToString()) > 2400))
                        {
                            string newBeginTime = Convert.ToInt32(aRow["BEGIN_TIME"].ToString()) < 2400 ? "2400" : aRow["BEGIN_TIME"].ToString();

                            //判斷加班時數
                            var parameters = new List<object>();
                            parameters.Add("0" + "," + aRow["EMPLOYEE_ID"].ToString() + "," + aRow["OVERTIME_DATE"].ToString() + "," + newBeginTime + "," + aRow["END_TIME"].ToString() + "," + aRow["OVERTIME_ROTE_ID"].ToString() + "," + aRow["OVERTIME_CAUSE_ID"].ToString());
                            var obj = checkRoteOvertimeHours_Function(parameters.ToArray(), connection, transaction);
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());

                            Decimal restHours = Convert.ToDecimal(dt.Rows[0]["totalHours"].ToString());
                            //補休最多8小時
                            if (restHours > 8) restHours = 8;

                            string AbsentPlusSql_12 = "";
                            AbsentPlusSql_12 = createAbsentPlusSql_I_12(userName, newID.ToString(), aRow["EMPLOYEE_ID"].ToString(), Convert.ToDateTime(aRow["OVERTIME_DATE"].ToString()), Convert.ToDateTime(aRow["OVERTIME_EFFECT_DATE"].ToString()), aRow["BEGIN_TIME"].ToString(), aRow["END_TIME"].ToString(), restHours, ConsolationHolidayID);
                            //this.ExecuteSql(AbsentPlusSql_12, connection, transaction);
                            sql = sql + AbsentPlusSql_12;
                        }
                    }
                }
                sql = sql.Replace("'null'", "null");
                this.ExecuteSql(sql, connection, transaction);
                dsUSERS.Dispose();
                transaction.Commit();
                return true;
            }
            catch { transaction.Rollback(); return false; }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
        }

        //--------------------------------------欄位驗證時候的資料-------------
        public class DataTableCellValidateData
        {
            public List<EmployeeData> EmployeeData { get; set; }
            public List<RoteData> RoteData { get; set; }
            public List<OverTimeCauseData> OverTimeCauseData { get; set; }
            public List<DeptData> DeptData { get; set; }
            public List<DeptData> DeptData_Dept { get; set; }

            public DataTableCellValidateData()
            {
                EmployeeData = new List<EmployeeData>();
                RoteData = new List<RoteData>();
                OverTimeCauseData = new List<OverTimeCauseData>();
                DeptData = new List<DeptData>();
                DeptData_Dept = new List<DeptData>();
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
            public string OVERTIME_DEPT_ID { get; set; }
            public string OVERTIME_NO { get; set; }
            public string SALARY_YYMM { get; set; }
            public string MEMO { get; set; }
        }

        #endregion

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

            detailSql = "update HRM_ATTEND_ESTIMATE_OVERTIME_DATA set IS_TRANSFER = 'N',OVERTIME_ID = null where IS_TRANSFER = 'Y' and OVERTIME_ID = " + overtimeID + "\r\n";

            sql = "select * from HRM_ATTEND_ABSENT_PLUS where (ABSENT_PLUS_TYPE = 'Overtime' or ABSENT_PLUS_TYPE = 'Overtime_12') and ABSENT_PLUS_NO = '" + overtimeID + "'";
            DataTable dtHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans).Tables[0];
            if (dtHRM_ATTEND_ABSENT_PLUS.Rows.Count > 0)
            {
                if (decimal.Parse(dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_HOURS"].ToString()) > 0)
                {
                    sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL " + "\r\n";
                    sql = sql + "where HOLIMAPPING_CODE = 'CompensationPlus'" + "\r\n";
                    DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                    holidayID = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["HOLIDAY_ID"].ToString();
                    dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

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
                        sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToString("yyyy/MM/dd") + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToString("yyyy/MM/dd") + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
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
                    detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_TYPE = 'Overtime' and ABSENT_PLUS_NO = '" + overtimeID + "'" + "\r\n";
                    detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_TYPE = 'Overtime_12' and ABSENT_PLUS_NO = '" + overtimeID + "'" + "\r\n";
                    detailSql = detailSql + "delete from HRM_ATTEND_ABSENT_TRANS where ABSENT_PLUS_ID = " + dtHRM_ATTEND_ABSENT_PLUS.Rows[0]["ABSENT_PLUS_ID"].ToString() + "\r\n";
                    detailSql = detailSql + "delete from HRM_ATTEND_OVERTIME_CREATE where OVERTIME_ID = '" + overtimeID + "'" + "\r\n";
                    this.ExecuteCommand(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                }
                else
                {
                    //出現錯誤訊息 ROLLBACK
                    this.ExecuteCommand("update HRM_ATTEND_OVERTIME_DATA ERROR", ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
                }
            }
            else
                this.ExecuteCommand(detailSql, ucHRM_ATTEND_OVERTIME_DATA.conn, ucHRM_ATTEND_OVERTIME_DATA.trans);
        }
    }
}
