using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using System.Diagnostics;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using JBTool;

namespace _HRM_Attend_Normal_AbsentMinus
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

        public object[] getSalaryYYMM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string beginDate = parm[1];

            string js = string.Empty;
            string rote_on_time = "";
            string rote_off_time = "";
            string salaryYYMM = "";


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
                string sql = "select TOP 1 B.ON_TIME,B.OFF_TIME from HRM_ATTEND_ATTEND A" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE B ON A.ROTE_ID = B.ROTE_ID " + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE <= '" + beginDate + "'" + "\r\n";
                sql = sql + "and B.ROTE_CODE <>'00' " + "\r\n";
                sql = sql + "order by A.ATTEND_DATE desc" + "\r\n";
                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);
                if (dsHRM_ATTEND_ATTEND.Tables[0].Rows.Count != 0)
                {
                    rote_on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                    rote_off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();
                }
                else
                {
                    rote_on_time = "0000";
                    rote_off_time = "0000";
                }

                sql = "select distinct case when day(ATTEND_DATE) > (SELECT ATTEND_CLOSE_DAY FROM HRM_ATTEND_OVERTIME_CONFIG ";
                sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'COMPANY_ID')) then LEFT(CONVERT(varchar,dateadd(mm,1,ATTEND_DATE),112),6) else LEFT(CONVERT(varchar,ATTEND_DATE,112),6) end as SALARY_YYMM " + "\r\n";
                sql = sql + "from HRM_ATTEND_DATA_LOCK" + "\r\n";
                sql = sql + "where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'GROUP_ID')" + "\r\n";
                sql = sql + "and ATTEND_DATE >= '" + beginDate + "'" + "\r\n";
                sql = sql + " ORDER BY SALARY_YYMM DESC";

                DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);

                sql = "select case when day(convert(datetime,'" + beginDate + "')) > (select ATTEND_CLOSE_DAY from HRM_ATTEND_OVERTIME_CONFIG ";
                sql = sql + " where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",convert(datetime,'" + beginDate + "'),'COMPANY_ID')) then left(convert(varchar,dateadd(mm,1,convert(datetime,'" + beginDate + "')),112),6)";
                sql = sql + " else left(convert(varchar,convert(datetime,'" + beginDate + "'),112),6) end as SALARY_YYMM" + "\r\n";


                DataSet dsAbsentDate = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows.Count != 0)
                {
                    if (Int32.Parse(dsAbsentDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString()) > Int32.Parse(dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["SALARY_YYMM"].ToString()))
                    {
                        //Indented縮排 將資料轉換成Json格式
                        //js = JsonConvert.SerializeObject(dsAbsentDate.Tables[0], Formatting.Indented);
                        salaryYYMM = dsAbsentDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString();
                    }
                    else
                    {
                        sql = sql + "select LEFT(CONVERT(varchar,dateadd(mm,1,convert(datetime,'" + dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["SALARY_YYMM"].ToString() + "'+'01')),112),6) as SALARY_YYMM";
                        DataSet dsAbsentAdd = this.ExecuteSql(sql, connection, transaction);
                        salaryYYMM = dsAbsentAdd.Tables[0].Rows[0]["SALARY_YYMM"].ToString();
                        //js = JsonConvert.SerializeObject(dsAbsentAdd.Tables[0], Formatting.Indented);
                    }
                }
                else
                {
                    //Indented縮排 將資料轉換成Json格式
                    //js = JsonConvert.SerializeObject(dsAbsentDate.Tables[0], Formatting.Indented);
                    salaryYYMM = dsAbsentDate.Tables[0].Rows[0]["SALARY_YYMM"].ToString();
                }

                sql = "select '" + salaryYYMM + "' as SALARY_YYMM,'" + rote_on_time + "' as ON_TIME,'" + rote_off_time + "' as OFF_TIME";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

                transaction.Commit();
                dsHRM_ATTEND_ATTEND.Dispose();
                dsHRM_ATTEND_DATA_LOCK.Dispose();
                dsAbsentDate.Dispose();
                ds.Dispose();
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

        //計算請假時數
        public object[] checkAbsentHours(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string absentMinusID = parm[0];
            string employeeID = parm[1];
            string beginDate = parm[2];
            string endDate = parm[3];
            string beginTime = parm[4];
            string endTime = parm[5];
            string holidayID = parm[6];

            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";
            //string rejectCode = "";
            string holidayUnit = "";
            
            string sex = "";
            string includeHoliday = "";
            string checkRestHour = "";
            decimal absentUnit = 0;
            decimal minnum = 0;
            decimal maxnum = 0;
            decimal i = 0;
            decimal hours = 0;
            decimal totalHours = 0;
            decimal dWorkHours = 0;

            DateTime beginabsentDate, restBeginTime, absentDate;
            DateTime endabsentDate, restEndTime;
            decimal minutes;

            TimeSpan ts;
            string roteCode = "";
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();


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
                string sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY,CHECK_REST_HOUR" + "\r\n";
                sql = sql + "from HRM_ATTEND_HOLIDAY" + "\r\n";
                sql = sql + "where HOLIDAY_ID=" + holidayID + "\r\n";

                DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);

                holidayUnit = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_UNIT"].ToString();    //假別單位
                minnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MIN_NUM"].ToString());  //請假最小單位數
                absentUnit = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["ABSENT_UNIT"].ToString());  //請假間隔最小單位數
                maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());  //年度最大可休時數(年)
                sex = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["SEX"].ToString(); //指定性別
                includeHoliday = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["INCLUDE_HOLIDAY"].ToString();  //包含假日
                checkRestHour = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["CHECK_REST_HOUR"].ToString();  //檢查剩餘時數

                sql = "select A.ATTEND_DATE,A.ROTE_ID," + "\r\n";
                sql = sql + "B.ROTE_CODE,B.ROTE_CNAME," + "\r\n";
                sql = sql + "B.ON_TIME," + "\r\n";
                sql = sql + "B.OFF_TIME," + "\r\n";
                sql = sql + "B.WORK_HRS," + "\r\n";
                sql = sql + "B.YEAR_REST_HRS," + "\r\n";
                sql = sql + "B.D_WORK_HRS," + "\r\n";
                sql = sql + "C.SEX, " + "\r\n";
                sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
                sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
                sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME, " + "\r\n";
                sql = sql + "D.YEAR_REST_HRS AS UPPER_YEAR_REST_HRS," + "\r\n";
                sql = sql + "D.D_WORK_HRS AS UPPER_D_WORK_HRS " + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE between '" + beginDate + "' and '" + endDate + "'" + "\r\n";
                //if (includeHoliday != "Y")
                //    sql = sql + "and B.ROTE_CODE <> '00' " + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                foreach (DataRow dr in dsHRM_ATTEND_ATTEND.Tables[0].Rows)
                {
                    if (dr["ROTE_CODE"].ToString() != "00")
                    {
                        roteCode = dr["ROTE_CODE"].ToString();
                        rote_on_time = dr["ON_TIME"].ToString();
                        rote_off_time = dr["OFF_TIME"].ToString();
                        roteID = dr["ROTE_ID"].ToString();
                        dWorkHours = Convert.ToDecimal(dr["D_WORK_HRS"].ToString());
                    }
                    else
                    {
                        roteCode = dr["UPPER_ROTE_CODE"].ToString();
                        rote_on_time = dr["UPPER_ON_TIME"].ToString();
                        rote_off_time = dr["UPPER_OFF_TIME"].ToString();
                        roteID = dr["UPPER_ROTE_ID"].ToString();
                        dWorkHours = Convert.ToDecimal(dr["UPPER_D_WORK_HRS"].ToString());
                    }
                    absentDate = Convert.ToDateTime(dr["ATTEND_DATE"]).Date;
                    

                    if (absentDate == Convert.ToDateTime(beginDate).Date)
                        if (Convert.ToInt32(beginTime) >= Convert.ToInt32(rote_on_time))
                            beginabsentDate = DateAndTimeMerger(absentDate, beginTime);
                        else
                            beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                    else
                        beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);

                    if (absentDate == Convert.ToDateTime(endDate).Date)
                        if (Convert.ToInt32(endTime) <= Convert.ToInt32(rote_off_time))
                            endabsentDate = DateAndTimeMerger(absentDate, endTime);
                        else
                            endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                    else
                        endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);

                    aOverTime.Begin = beginabsentDate;
                    aOverTime.End = endabsentDate;

                    Reduce.Clear();
                    if (dr["ROTE_CODE"].ToString() != "00" || (dr["ROTE_CODE"].ToString() == "00" && includeHoliday == "Y"))
                    {
                        string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                        restSql = restSql + "where ROTE_ID = " + roteID;
                        DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
                        foreach (DataRow Row1 in dtRest.Rows)
                        {
                            restBeginTime = DateAndTimeMerger(absentDate, Row1["REST_BEGIN_TIME"].ToString());
                            restEndTime = DateAndTimeMerger(absentDate, Row1["REST_END_TIME"].ToString());

                            ts = restEndTime - restBeginTime;
                            if ((beginabsentDate <= restBeginTime) && (endabsentDate >= restEndTime))
                            {
                                if (roteCode == "00" && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                                {
                                    Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                                }
                                else if (roteCode != "00" && Row1["IS_NORMAL_ABSENT"].ToString() == "Y")   //平日是否參考	5
                                {
                                    Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                                }
                            }
                        }

                        //計算每天請假時間
                        Reduce = RangeCheck(Reduce);
                        var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                        var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;

                        minutes = (TotalMinute - ReduceMinute);
                        hours = minutes / 60;
                        
                        i = 0;

                        if (holidayUnit == "1") //小時
                        {
                            hours = hours >= minnum ? hours : minnum;
                            //間隔單位一定要大於零而且請假時間也要大於零
                            while ((absentUnit > 0) && (hours > 0) && (i < hours))
                                i += absentUnit;
                            
                            hours = i;
                        }
                        else
                        {
                            hours = hours / dWorkHours;
                            hours = hours >= minnum ? hours : minnum;
                            //間隔單位一定要大於零而且請假時間也要大於零
                            while ((absentUnit > 0) && (hours > 0) && (i < hours))
                                i += absentUnit;
                            hours = i;
                        }
                    }
                    totalHours = totalHours + hours;
                    hours = 0;
                }

                sql = "select " + totalHours + " as totalHours";
                //    sql = "select '" + rejectCode + "' as rejectCode, 0 as hours, 0 as totalHours" ;
   
                DataSet dsHours = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                dsHRM_ATTEND_HOLIDAY.Dispose();
                dsHRM_ATTEND_ATTEND.Dispose();

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

        public object[] checkPhysiologyleavesID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string holidayID = parm[0];
           
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
                string sql = " select count(*) as cnt from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'Physiologyleave' and HOLIDAY_ID = " + holidayID + "\r\n";
                DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[0].Rows[0]["cnt"].ToString();
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

        public object[] checkHolidaySex(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string holidayID = parm[0];
            string employeeID = parm[1];
            string sex = "";
            string cnt = "1";

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
                string sql = "select SEX from HRM_ATTEND_HOLIDAY where SEX is NOT NULL  and rtrim(ltrim(SEX))<>'' and HOLIDAY_ID = " + holidayID + "\r\n";
                DataSet dHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);
                if (dHRM_ATTEND_HOLIDAY.Tables[0].Rows.Count > 0)
                {
                    sex = dHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["SEX"].ToString();
                    sql = "select count(*) as cnt from HRM_BASE_BASE where EMPLOYEE_ID = '" + employeeID + "' and SEX = '" + sex + "'" + "\r\n";
                    DataSet dsHRM_BASE_BASE = this.ExecuteSql(sql, connection, transaction);
                    cnt = dsHRM_BASE_BASE.Tables[0].Rows[0]["cnt"].ToString();
                    dsHRM_BASE_BASE.Dispose();
                }
                transaction.Commit();
                dHRM_ATTEND_HOLIDAY.Dispose();
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

        public object[] checkAbsentData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string absentMinusID = parm[0];
            string employeeID = parm[1];
            string beginDate = parm[2];
            string endDate = parm[3];
            string beginTime = parm[4];
            string endTime = parm[5];
            string holidayID = parm[6];
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
                string sql = " select COUNT(*) AS cnt from HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";
                sql = sql + " where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + " and ABSENT_DATE BETWEEN '" + beginDate + "'" + " and '" + endDate + "'" + "\r\n";
                sql = sql + " and BEGIN_TIME < '" + endTime + "'" + "\r\n";
                sql = sql + " and END_TIME > '" + beginTime + "'" + "\r\n";
                if (absentMinusID != "0")
                    sql = sql + " and ABSENT_MINUS_ID <> " + absentMinusID;

                DataSet dsHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsHRM_ATTEND_ABSENT_MINUS_DETAIL.Tables[0].Rows[0]["cnt"].ToString();
                dsHRM_ATTEND_ABSENT_MINUS_DETAIL.Dispose();

                if (cnt == "0")
                {
                    sql = " select COUNT(*) AS cnt from HRM_ATTEND_ABSENT_LEAVE_DETAIL " + "\r\n";
                    sql = sql + " where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + " and ABSENT_DATE BETWEEN '" + beginDate + "'" + " and '" + endDate + "'" + "\r\n";
                    sql = sql + " and BEGIN_TIME < '" + endTime + "'" + "\r\n";
                    sql = sql + " and END_TIME > '" + beginTime + "'" + "\r\n";

                    DataSet dsHRM_ATTEND_ABSENT_LEAVE_DETAIL = this.ExecuteSql(sql, connection, transaction);
                    cnt = dsHRM_ATTEND_ABSENT_LEAVE_DETAIL.Tables[0].Rows[0]["cnt"].ToString();
                    dsHRM_ATTEND_ABSENT_MINUS_DETAIL.Dispose();
                }

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

        public object[] checkAbsentRestHours(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string absentMinusID = parm[0];
            string employeeID = parm[1];
            string beginDate = parm[2];
            string endDate = parm[3];
            string beginTime = parm[4];
            string endTime = parm[5];
            string holidayID = parm[6];
            decimal totalHours = decimal.Parse(parm[7]);
            //decimal o_totalHours = decimal.Parse(parm[8]);
            decimal maxnum = 0;
            string checkRestHour = "";
            string plusHolidayID = "";
            string autoCreate = "";
            string physiologyHolidayID = "";
            string familyReunionHolidayID = "";

            DateTime retday;
            string firstYearDay, lastYearDay;

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
                //取得特休代碼資料
                string sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'AnnualPlus'" + "\r\n";
                sql = sql + "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'Physiologyleave'" + "\r\n";
                sql = sql + "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'FamilyReunionLeave'" + "\r\n";
                DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, connection, transaction);
                physiologyHolidayID = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[1].Rows[0]["HOLIDAY_ID"].ToString(); //生理假
                familyReunionHolidayID = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[2].Rows[0]["HOLIDAY_ID"].ToString(); //家庭照顧假
                dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

                sql = "select MAX_NUM from HRM_ATTEND_HOLIDAY WHERE HOLIDAY_ID = " + familyReunionHolidayID + "\r\n";
                string familyMaxHours = this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["MAX_NUM"].ToString();

                sql = "select B.CHECK_REST_HOUR,SUM(A.REST_HOURS) as REST_HOURS, 0 as PHYSIOLOGY_CNT, 0.00 as FAMILY_HOURS, " + familyMaxHours + "as FAMILY_MAX_HOURS" + "\r\n"; ;
                sql = sql + "from dbo.HRM_ATTEND_ABSENT_PLUS A " + "\r\n";
                sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                sql = sql + "and ('" + beginDate + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + endDate + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                sql = sql + "and B.HOLIDAY_FLAG = '+'" + "\r\n";
                sql = sql + "group by B.CHECK_REST_HOUR";

                DataSet dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows.Count == 0)
                {
                    string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                    DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                    string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                    int beginYear = Convert.ToDateTime(beginDate).Date.Year;
                    int endYear = Convert.ToDateTime(endDate).Date.Year;

                    retday = new DateTime(beginYear, 1, 1);
                    firstYearDay = retday.ToShortDateString();

                    retday = new DateTime(beginYear, 12, 31);
                    lastYearDay = retday.ToShortDateString();

                    string sysday = DateTime.Now.ToString();


                    sql = "select TOP 1 case when A.MAX_NUM = 0 then isnull(C.MAX_NUM,0) else A.MAX_NUM end as MAX_NUM," + "\r\n";
                    sql = sql + "A.CHECK_REST_HOUR,B.HOLIDAY_ID,ISNULL(B.AUTO_CREATE,'N') AS AUTO_CREATE " + "\r\n";
                    sql = sql + "from HRM_ATTEND_HOLIDAY A" + "\r\n";
                    sql = sql + "LEFT JOIN HRM_ATTEND_HOLIDAY B ON A.HOLIDAY_KIND_ID = B.HOLIDAY_KIND_ID" + "\r\n";
                    sql = sql + "LEFT JOIN HRM_ATTEND_HOLIDAY C ON B.HOLIDAY_KIND_ID = C.HOLIDAY_KIND_ID and C.HOLIDAY_FLAG = '-' and C.AUTO_CREATE = 'Y'" + "\r\n";
                    sql = sql + "where A.HOLIDAY_ID = " + holidayID + "\r\n";
                    sql = sql + "AND B.HOLIDAY_FLAG = '+' " + "\r\n";

                    DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);

                    if (dsHRM_ATTEND_HOLIDAY.Tables[0].Rows.Count > 0)
                    {
                        maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());
                        checkRestHour = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["CHECK_REST_HOUR"].ToString();
                        plusHolidayID = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_ID"].ToString();
                        autoCreate = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["AUTO_CREATE"].ToString();
                    }

                    //自動產生得假資料="Y"/生理假/家庭照顧假 && 判斷檢查剩餘時數 =='Y' 且 年度最大可休時數(年) > 0)
                    if ((autoCreate == "Y" || holidayID == physiologyHolidayID || holidayID == familyReunionHolidayID) && checkRestHour == "Y" && maxnum > 0)
                    {
                        sql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n";
                        sql = sql + "select '" + employeeID + "','" + firstYearDay + "','" + lastYearDay + "'," + plusHolidayID + "," + maxnum + ",0,0," + maxnum + ",'','','','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                        sql = sql + "declare @absentPlusID int " + "\r\n";
                        sql = sql + "select @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                        sql = sql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                        sql = sql + " select @absentPlusID,null, null, " + maxnum + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                        if (beginYear != endYear)
                        {
                            retday = new DateTime(endYear, 1, 1);
                            firstYearDay = retday.ToShortDateString();

                            retday = new DateTime(endYear, 12, 31);
                            lastYearDay = retday.ToShortDateString();

                            sql = sql + "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n";
                            sql = sql + " select '" + employeeID + "','" + firstYearDay + "','" + lastYearDay + "'," + holidayID + "," + maxnum + ",0,0," + maxnum + ",'','','Y','','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                            sql = sql + "declare @absentPlusID int " + "\r\n";
                            sql = sql + "select @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                            sql = sql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                            sql = sql + " select @absentPlusID,null, null, " + maxnum + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                        }
                        ExecuteCommand(sql, connection, transaction);
                    }
                    dsHRM_ATTEND_ABSENT_PLUS.Dispose();

                    sql = "select B.CHECK_REST_HOUR,SUM(A.REST_HOURS) as REST_HOURS, 0 as PHYSIOLOGY_CNT, 0.00 as FAMILY_HOURS, 0.00 as FAMILY_MAX_HOURS" + "\r\n"; ;
                    sql = sql + "from dbo.HRM_ATTEND_ABSENT_PLUS A " + "\r\n";
                    sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                    sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                    sql = sql + "and ('" + beginDate + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + endDate + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                    sql = sql + "and B.HOLIDAY_FLAG = '+'" + "\r\n";
                    sql = sql + "group by B.CHECK_REST_HOUR";

                    dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction);
                }
                    
                //判斷每月只能請一次生理假
                if (holidayID == physiologyHolidayID)
                {
                    string beginFirstDay = Convert.ToDateTime(beginDate).AddDays(-Convert.ToDateTime(beginDate).Day+1).ToShortDateString();
                    string beginLastDay = Convert.ToDateTime(beginDate).AddMonths(1).AddDays(-Convert.ToDateTime(beginDate).AddMonths(1).Day).ToShortDateString();
                    string endFirstDay = Convert.ToDateTime(endDate).AddDays(-Convert.ToDateTime(endDate).Day + 1).ToShortDateString();
                    string endLastDay = Convert.ToDateTime(endDate).AddMonths(1).AddDays(-Convert.ToDateTime(endDate).AddMonths(1).Day).ToShortDateString();
                    sql = "select count(*) as cnt from HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";
                    sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + "and HOLIDAY_ID = " + holidayID + "\r\n";
                    sql = sql + "and (ABSENT_DATE between '" + beginFirstDay + "' and '" + beginLastDay + "' or ABSENT_DATE between '" + endFirstDay + "' and '" + endLastDay + "')" + "\r\n";
                    sql = sql + "and ABSENT_MINUS_ID <> " + absentMinusID + "\r\n";
                    string cnt = this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["cnt"].ToString();
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows[0]["PHYSIOLOGY_CNT"] = int.Parse(cnt);
                }

                if (holidayID == familyReunionHolidayID)
                {
                    string beginFirstDay = beginDate.Substring(0,4) + "/01/01";
                    string beginLastDay = beginDate.Substring(0, 4) + "/12/31";
                    string endFirstDay = endDate.Substring(0, 4) + "/01/01";
                    string endLastDay = endDate.Substring(0, 4) + "/12/31";
                    sql = "select sum(ABSENT_HOURS) as ABSENT_HOURS from HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";
                    sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + "and HOLIDAY_ID = " + holidayID + "\r\n";
                    sql = sql + "and ABSENT_DATE between '" + beginFirstDay + "' and '" + beginLastDay + "'" + "\r\n";
                    decimal absentHours = decimal.Parse(this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["ABSENT_HOURS"].ToString());
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows[0]["FAMILY_HOURS"] = absentHours;
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows[0]["FAMILY_MAX_HOURS"] = decimal.Parse(familyMaxHours);
                }

                //判斷檢查剩餘時數 =='Y' 且無得假資料
                if (checkRestHour == "Y" && dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows.Count == 0)
                {
                    DataRow aRow = dsHRM_ATTEND_ABSENT_PLUS.Tables[0].NewRow();
                    aRow["CHECK_REST_HOUR"] = "Y";
                    aRow["REST_HOURS"] = 0;
                    aRow["PHYSIOLOGY_CNT"] = 0;
                    aRow["FAMILY_HOURS"] = 0;
                    aRow["FAMILY_MAX_HOURS"] = 0;
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows.Add(aRow);
                }
                transaction.Commit();
                js = JsonConvert.SerializeObject(dsHRM_ATTEND_ABSENT_PLUS.Tables[0], Formatting.Indented);
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
               

        //檢查補休時數
        //補休以4小時為單位, 效期當日起算次月月底休完, 剩餘時數(4的餘數)提供遞延1個月BY 部門補休剩餘時數遞延功能
        public object[] checkOvertimeRestHours(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string holidayID = parm[1];
            decimal absentRestHours = decimal.Parse(parm[2]);
            string endDate = parm[3];
            string cnt = "";
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
                string sql = "select count(*) as cnt from dbo.HRM_ATTEND_OVERTIME_CONFIG A " + "\r\n";
                sql = sql + "where COMPANY_ID = (select dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "','" + endDate + "','COMPANY_ID'))" + "\r\n";
                sql = sql + "and HOLIDAY_ID in (select HOLIDAY_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_FLAG = '+' and HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + "))" + "\r\n"; ;

                DataSet dsHRM_ATTEND_OVERTIME_CONFIG = this.ExecuteSql(sql, connection, transaction);
                if (dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows.Count > 0)
                     cnt = dsHRM_ATTEND_OVERTIME_CONFIG.Tables[0].Rows[0]["cnt"].ToString();
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

        public object flowCheckHours(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            //string Today = System.DateTime.Today.ToShortDateString(); //取得今天日期
            decimal upperManagerHour = 0;
            decimal absentHours = decimal.Parse(dr["TOTAL_HOURS"].ToString()); // 取得請假時數
            string employeeID = dr["EMPLOYEE_ID"].ToString(); 

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
                if (absentHours > upperManagerHour)
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

        private object changeToDateTime(object changeDate)
        {
            string roteChnageDate = changeDate.ToString().Trim();
            if (roteChnageDate.Length == 0) return null;

            DateTime dateTemp = DateTime.Now;
            if (!DateTime.TryParse(roteChnageDate, out dateTemp)) return null;
            else dateTemp = new DateTime(dateTemp.Year, dateTemp.Month, dateTemp.Day);
            return dateTemp;
        }  

        private void ucHRM_ATTEND_ABSENT_MINUS_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            var absentMinusID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("ABSENT_MINUS_ID");
            var employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("EMPLOYEE_ID");
            var beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_DATE"));
            var endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_DATE"));
            var beginTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_TIME").ToString();
            var endTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_TIME").ToString();
            var holidayID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("HOLIDAY_ID");

            ucHRM_ATTEND_ABSENT_MINUS.SetFieldValue("ABSENT_DATE_TIME_BEGIN", DateAndTimeMerger(beginDate, beginTime));
            ucHRM_ATTEND_ABSENT_MINUS.SetFieldValue("ABSENT_DATE_TIME_END", DateAndTimeMerger(endDate, endTime));
        }

        private void ucHRM_ATTEND_ABSENT_MINUS_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";
            string holidayUnit = "";

            string includeHoliday = "";
            decimal absentUnit = 0;
            decimal minnum = 0;
            decimal maxnum = 0;
            decimal i = 0;
            decimal hours = 0;
            decimal dWorkHours = 0;

            DateTime beginabsentDate, restBeginTime, absentDate;
            DateTime endabsentDate, restEndTime;
            decimal minutes;

            TimeSpan ts;
            string roteCode = "";
            string beginabsentTime = "";
            string endabsentTime = "";

            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

            string detailSql = "";
    
            var absentMinusID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("ABSENT_MINUS_ID");
            var employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("EMPLOYEE_ID");
            var beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_DATE"));
            var endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_DATE"));
            var beginTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_TIME").ToString();
            var endTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_TIME").ToString();
            var holidayID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("HOLIDAY_ID");
            
            var o_employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("EMPLOYEE_ID");
            var o_beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("BEGIN_DATE"));
            var o_endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("END_DATE"));
            var o_beginTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("BEGIN_TIME").ToString();
            var o_endTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("END_TIME").ToString();
            var o_holidayID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("HOLIDAY_ID");

            ucHRM_ATTEND_ABSENT_MINUS.SetFieldValue("ABSENT_DATE_TIME_BEGIN", DateAndTimeMerger(beginDate, beginTime));
            ucHRM_ATTEND_ABSENT_MINUS.SetFieldValue("ABSENT_DATE_TIME_END", DateAndTimeMerger(endDate, endTime));

            var sql = "select * from  HRM_ATTEND_ABSENT_MINUS_DETAIL where ABSENT_MINUS_ID = " + absentMinusID;
            DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans).Tables[0];

            //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
            sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
            sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
            sql = sql + "where EMPLOYEE_ID = '" + o_employeeID + "'" + "\r\n";
            sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + o_holidayID + ")" + "\r\n";
            sql = sql + "and ('" + Convert.ToDateTime(o_beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(o_endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
            sql = sql + "and REST_HOURS >0" + "\r\n";
            sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
            sql = sql + "order by A.BEGIN_DATE,A.END_DATE DESC" + "\r\n";

            DataSet drHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
            detailSql = "";
            foreach (DataRow Row in dtHRM_ATTEND_ABSENT_MINUS_DETAIL.Rows)
            {
                hours = decimal.Parse(Row["ABSENT_HOURS"].ToString());
                foreach (DataRow drRestHours in drHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                {
                    string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                    if (decimal.Parse(drRestHours["ABSENT_HOURS"].ToString()) - hours >= 0)
                    {
                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS - " + hours + ",REST_HOURS = REST_HOURS + " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                        drRestHours["ABSENT_HOURS"] = decimal.Parse(drRestHours["ABSENT_HOURS"].ToString()) - hours;
                        break;
                    }
                    else if (decimal.Parse(drRestHours["ABSENT_HOURS"].ToString()) > 0)
                    {
                        detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS - " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                        hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                        drRestHours["REST_HOURS"] = 0;
                    }
                }
            }

            detailSql = detailSql + "delete from  HRM_ATTEND_ABSENT_MINUS_DETAIL where ABSENT_MINUS_ID = " + absentMinusID + "\r\n";
            detailSql = detailSql + "delete from  HRM_ATTEND_ABSENT_TRANS where ABSENT_MINUS_ID = " + absentMinusID + "\r\n";

            drHRM_ATTEND_ABSENT_PLUS.Dispose();
            dtHRM_ATTEND_ABSENT_MINUS_DETAIL.Dispose();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

            sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY" + "\r\n";
            sql = sql + "from HRM_ATTEND_HOLIDAY" + "\r\n";
            sql = sql + "where HOLIDAY_ID=" + holidayID + "\r\n";

            //DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);
            DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            holidayUnit = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_UNIT"].ToString();    //假別單位
            minnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MIN_NUM"].ToString());  //請假最小單位數
            absentUnit = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["ABSENT_UNIT"].ToString());  //請假間隔最小單位數
            maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());  //年度最大可休時數(年)
            includeHoliday = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["INCLUDE_HOLIDAY"].ToString();  //包含假日

            sql = "select A.ATTEND_DATE,A.ROTE_ID," + "\r\n";
            sql = sql + "B.ROTE_CODE,B.ROTE_CNAME," + "\r\n";
            sql = sql + "B.ON_TIME," + "\r\n";
            sql = sql + "B.OFF_TIME," + "\r\n";
            sql = sql + "B.WORK_HRS," + "\r\n";
            sql = sql + "B.YEAR_REST_HRS," + "\r\n";
            sql = sql + "B.D_WORK_HRS," + "\r\n";
            sql = sql + "C.SEX, " + "\r\n";
            sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
            sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
            sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
            sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME, " + "\r\n";
            sql = sql + "D.YEAR_REST_HRS AS UPPER_YEAR_REST_HRS," + "\r\n";
            sql = sql + "D.WORK_HRS AS UPPER_WORK_HRS " + "\r\n";
            sql = sql + "from HRM_ATTEND_ATTEND A" + "\r\n";
            sql = sql + "left join HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
            sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
            sql = sql + "left join HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
            sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
            sql = sql + "and A.ATTEND_DATE between '" + Convert.ToDateTime(beginDate).ToShortDateString() + "' and '" + Convert.ToDateTime(endDate).ToShortDateString() + "'" + "\r\n";

            //DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);
            DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
            sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
            sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
            sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
            sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
            sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
            sql = sql + "and REST_HOURS >0" + "\r\n";
            sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
            sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

            DataSet dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            foreach (DataRow dr in dsHRM_ATTEND_ATTEND.Tables[0].Rows)
            {
                if (dr["ROTE_CODE"].ToString() != "00")
                {
                    roteCode = dr["ROTE_CODE"].ToString();
                    rote_on_time = dr["ON_TIME"].ToString();
                    rote_off_time = dr["OFF_TIME"].ToString();
                    roteID = dr["ROTE_ID"].ToString();
                    dWorkHours = Convert.ToDecimal(dr["WORK_HRS"].ToString());
                }
                else
                {
                    roteCode = dr["UPPER_ROTE_CODE"].ToString();
                    rote_on_time = dr["UPPER_ON_TIME"].ToString();
                    rote_off_time = dr["UPPER_OFF_TIME"].ToString();
                    roteID = dr["UPPER_ROTE_ID"].ToString();
                    dWorkHours = Convert.ToDecimal(dr["UPPER_WORK_HRS"].ToString());
                }

                absentDate = Convert.ToDateTime(dr["ATTEND_DATE"]).Date;

                if (absentDate == Convert.ToDateTime(beginDate).Date)
                    if (Convert.ToInt32(beginTime) >= Convert.ToInt32(rote_on_time))
                    {
                        beginabsentDate = DateAndTimeMerger(absentDate, beginTime);
                        beginabsentTime = beginTime;
                    }
                    else
                    {
                        beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                        beginabsentTime = rote_on_time;
                    }
                else
                {
                    beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                    beginabsentTime = rote_on_time;
                }

                if (absentDate == Convert.ToDateTime(endDate).Date)
                {
                    if (Convert.ToInt32(endTime) <= Convert.ToInt32(rote_off_time))
                    {
                        endabsentDate = DateAndTimeMerger(absentDate, endTime);
                        endabsentTime = endTime;
                    }
                    else
                    {
                        endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                        endabsentTime = rote_off_time;
                    }
                }
                else
                {
                    endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                    endabsentTime = rote_off_time;
                }

                aOverTime.Begin = beginabsentDate;
                aOverTime.End = endabsentDate;

                Reduce.Clear();
                if (dr["ROTE_CODE"].ToString() != "00" || (dr["ROTE_CODE"].ToString() == "00" && includeHoliday == "Y"))
                {
                    string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                    restSql = restSql + "where ROTE_ID = " + roteID;
                    //DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
                    DataTable dtRest = this.ExecuteSql(restSql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans).Tables[0];
                    foreach (DataRow Row1 in dtRest.Rows)
                    {
                        restBeginTime = DateAndTimeMerger(absentDate, Row1["REST_BEGIN_TIME"].ToString());
                        restEndTime = DateAndTimeMerger(absentDate, Row1["REST_END_TIME"].ToString());

                        ts = restEndTime - restBeginTime;
                        if ((beginabsentDate <= restBeginTime) && (endabsentDate >= restEndTime))
                        {
                            if (roteCode == "00" && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                            {
                                Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                            }
                            else if (roteCode != "00" && Row1["IS_NORMAL_ABSENT"].ToString() == "Y")   //平日是否參考	5
                            {
                                Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                            }
                        }
                    }

                    //計算每天請假時間
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;

                    minutes = (TotalMinute - ReduceMinute);
                    hours = minutes / 60;

                    i = 0;

                    if (holidayUnit == "1") //小時
                    {
                        hours = hours >= minnum ? hours : minnum;
                        //間隔單位一定要大於零而且請假時間也要大於零
                        while ((absentUnit > 0) && (hours > 0) && (i < hours))
                            i += absentUnit;

                        hours = i;
                    }
                    else
                    {
                        hours = hours / dWorkHours;
                        hours = hours >= minnum ? hours : minnum;
                        //間隔單位一定要大於零而且請假時間也要大於零
                        while ((absentUnit > 0) && (hours > 0) && (i < hours))
                            i += absentUnit;
                        hours = i;
                    }

                    detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n"; ;
                    detailSql = detailSql + "select " + absentMinusID + ",'" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "'," + holidayID + "," + hours + ",'" + beginabsentTime + "','" + endabsentTime + "','" + beginabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "',dbo.funReturnSalaryYYMM('" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "')" + "\r\n";

                    //detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_MINUS_DETAIL values ( ";
                    //detailSql = detailSql + absentMinusID + ",'" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "'," + holidayID + "," + hours + ",'" + beginabsentTime + "','" + endabsentTime + "','" + beginabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "' )" + "\r\n";

                    //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                    foreach (DataRow drRestHours in dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                    {
                        string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                        if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                        {
                            detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                            detailSql = detailSql + " select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                            detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                            drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                            break;
                        }
                        else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                        {
                            detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                            detailSql = detailSql + " select " + absentPlusID + "," + absentMinusID + "," + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                            detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS -" + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                            hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                            drRestHours["REST_HOURS"] = 0;
                        }
                    }
                }
            }
            drHRM_ATTEND_ABSENT_PLUS.Dispose();
            dsHRM_ATTEND_ATTEND.Dispose();

            this.ExecuteSql(detailSql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);
        }

        private void ucHRM_ATTEND_ABSENT_MINUS_BeforeDelete(object sender, UpdateComponentBeforeDeleteEventArgs e)
        {
            decimal hours;
            var absentMinusID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("ABSENT_MINUS_ID");
            var employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("EMPLOYEE_ID");
            var beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("BEGIN_DATE"));
            var endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("END_DATE"));
            var beginTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("BEGIN_TIME").ToString();
            var endTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("END_TIME").ToString();
            var holidayID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("HOLIDAY_ID");

            var sql = "select * from  HRM_ATTEND_ABSENT_MINUS_DETAIL where ABSENT_MINUS_ID = " + absentMinusID;
            DataTable dtHRM_ATTEND_ABSENT_MINUS_DETAIL = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans).Tables[0];

            //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
            sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
            sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
            sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
            sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
            sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
            sql = sql + "and REST_HOURS >0" + "\r\n";
            sql = sql + "and B.CHECK_REST_HOUR= 'Y'" + "\r\n";
            sql = sql + "order by A.BEGIN_DATE,A.END_DATE DESC" + "\r\n";

            DataSet drHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
            sql = "";
            foreach (DataRow Row in dtHRM_ATTEND_ABSENT_MINUS_DETAIL.Rows)
            {
                hours = decimal.Parse(Row["ABSENT_HOURS"].ToString());
                foreach (DataRow drRestHours in drHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                {
                    string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                    if (decimal.Parse(drRestHours["ABSENT_HOURS"].ToString()) - hours >= 0)
                    {
                        sql = sql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS - " + hours + ",REST_HOURS = REST_HOURS + " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                        drRestHours["ABSENT_HOURS"] = decimal.Parse(drRestHours["ABSENT_HOURS"].ToString()) - hours;
                        break;
                    }
                    else if (decimal.Parse(drRestHours["ABSENT_HOURS"].ToString()) > 0)
                    {
                        sql = sql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS - " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                        hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                        drRestHours["REST_HOURS"] = 0;
                    }
                }
            }

            sql = sql + "delete from  HRM_ATTEND_ABSENT_MINUS_DETAIL where ABSENT_MINUS_ID = " + absentMinusID + "\r\n";
            sql = sql + "delete from  HRM_ATTEND_ABSENT_TRANS where ABSENT_MINUS_ID = " + absentMinusID + "\r\n";
            sql = sql + "delete from  HRM_ATTEND_ABSENT_CREATE where ABSENT_MINUS_ID = " + absentMinusID + "\r\n";
            this.ExecuteCommand(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            //執行刷卡轉出勤
            //var ObjParameter = new { EmployeeID = employeeID, DateFrom = beginDate, DateTo = endDate };
            //EEPRemoteModule remoteobject = new EEPRemoteModule();
            //object[] back = remoteobject.CallMethod(this.ClientInfo, "_HRM_Attend_Normal_AttendCalculate", "AttendCalculate_Single", new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
            //_HRM_Attend_Normal_AttendCalculate.Component aComponent = new _HRM_Attend_Normal_AttendCalculate.Component();
            //aComponent.AttendCalculate_Single(new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
        }

        private void ucHRM_ATTEND_ABSENT_MINUS_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";
            string holidayUnit = "";

            string includeHoliday = "";
            string checkRestHour = "";
            decimal absentUnit = 0;
            decimal minnum = 0;
            decimal maxnum = 0;
            decimal i = 0;
            decimal hours = 0;
            decimal dWorkHours = 0;

            DateTime beginabsentDate, restBeginTime, absentDate;
            DateTime endabsentDate, restEndTime;
            decimal minutes;

            TimeSpan ts;
            string roteCode = "";
            string beginabsentTime = "";
            string endabsentTime = "";

            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

            //var absentMinusID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("ABSENT_MINUS_ID");
            var employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("EMPLOYEE_ID");
            var beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_DATE"));
            var endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_DATE"));
            var beginTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_TIME").ToString();
            var endTime = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_TIME").ToString();
            var holidayID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("HOLIDAY_ID");

            var sql = "";
            var detailSql = "";

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            sql = "select USERNAME from USERS where USERID= '" + userid + "'";
            DataSet dsUSERS = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);
            string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

            var command = ucHRM_ATTEND_ABSENT_MINUS.conn.CreateCommand();
            command.CommandText = "SELECT SCOPE_IDENTITY()";
            command.Transaction = ucHRM_ATTEND_ABSENT_MINUS.trans;
            int absentMinusID = Convert.ToInt32(command.ExecuteScalar());

            var dataset = (DataSet)ucHRM_ATTEND_ABSENT_MINUS.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_ABSENT_MINUS);
            var table = (string)ucHRM_ATTEND_ABSENT_MINUS.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_ABSENT_MINUS);
            DataTable dt = ucHRM_ATTEND_ABSENT_MINUS.GetSchema();

            for (int j = 0; j < dataset.Tables[table].Rows.Count; j++)
            {
                if (dataset.Tables[table].Rows[j].RowState == DataRowState.Added)
                {
                    dataset.Tables[table].Rows[j]["ABSENT_MINUS_ID"] = absentMinusID;
                    dataset.Tables[table].Rows[j]["CREATE_MAN"] = userName;
                    dataset.Tables[table].Rows[j]["UPDATE_MAN"] = userName;
                    dataset.Tables[table].Rows[j]["CREATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dataset.Tables[table].Rows[j]["UPDATE_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                logInfo_HRM_ATTEND_ABSENT_MINUS.Log(dataset.Tables[table].Rows[j], dt, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans, ucHRM_ATTEND_ABSENT_MINUS.SelectCmd.KeyFields);
            }

            sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY,CHECK_REST_HOUR" + "\r\n";
            sql = sql + "from HRM_ATTEND_HOLIDAY" + "\r\n";
            sql = sql + "where HOLIDAY_ID=" + holidayID + "\r\n";

            //DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);
            DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            holidayUnit = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_UNIT"].ToString();    //假別單位
            minnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MIN_NUM"].ToString());  //請假最小單位數
            absentUnit = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["ABSENT_UNIT"].ToString());  //請假間隔最小單位數
            maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());  //年度最大可休時數(年)
            includeHoliday = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["INCLUDE_HOLIDAY"].ToString();  //包含假日
            checkRestHour = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["CHECK_REST_HOUR"].ToString();  //檢查剩餘時數

            sql = "select A.ATTEND_DATE,A.ROTE_ID," + "\r\n";
            sql = sql + "B.ROTE_CODE,B.ROTE_CNAME," + "\r\n";
            sql = sql + "B.ON_TIME," + "\r\n";
            sql = sql + "B.OFF_TIME," + "\r\n";
            sql = sql + "B.WORK_HRS," + "\r\n";
            sql = sql + "B.YEAR_REST_HRS," + "\r\n";
            sql = sql + "B.D_WORK_HRS," + "\r\n";
            sql = sql + "C.SEX, " + "\r\n";
            sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
            sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
            sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
            sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME, " + "\r\n";
            sql = sql + "D.YEAR_REST_HRS AS UPPER_YEAR_REST_HRS," + "\r\n";
            sql = sql + "D.D_WORK_HRS AS UPPER_WORK_HRS " + "\r\n";
            sql = sql + "from HRM_ATTEND_ATTEND A" + "\r\n";
            sql = sql + "left join HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
            sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
            sql = sql + "left join HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
            sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
            sql = sql + "and A.ATTEND_DATE between '" + Convert.ToDateTime(beginDate).ToShortDateString() + "' and '" + Convert.ToDateTime(endDate).ToShortDateString() + "'" + "\r\n";

            //DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);
            DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
            sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
            sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
            sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
            sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
            sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
            sql = sql + "and REST_HOURS >0" + "\r\n";
            sql = sql + "and B.HOLIDAY_FLAG = '+'" + "\r\n";
            sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

            DataSet dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);

            foreach (DataRow dr in dsHRM_ATTEND_ATTEND.Tables[0].Rows)
            {
                if (dr["ROTE_CODE"].ToString() != "00")
                {
                    roteCode = dr["ROTE_CODE"].ToString();
                    rote_on_time = dr["ON_TIME"].ToString();
                    rote_off_time = dr["OFF_TIME"].ToString();
                    roteID = dr["ROTE_ID"].ToString();
                    dWorkHours = Convert.ToDecimal(dr["WORK_HRS"].ToString());
                }
                else
                {
                    roteCode = dr["UPPER_ROTE_CODE"].ToString();
                    rote_on_time = dr["UPPER_ON_TIME"].ToString();
                    rote_off_time = dr["UPPER_OFF_TIME"].ToString();
                    roteID = dr["UPPER_ROTE_ID"].ToString();
                    dWorkHours = Convert.ToDecimal(dr["UPPER_WORK_HRS"].ToString());
                }

                absentDate = Convert.ToDateTime(dr["ATTEND_DATE"]).Date;

                if (absentDate == Convert.ToDateTime(beginDate).Date)
                    if (Convert.ToInt32(beginTime) >= Convert.ToInt32(rote_on_time))
                    {
                        beginabsentDate = DateAndTimeMerger(absentDate, beginTime);
                        beginabsentTime = beginTime;
                    }
                    else
                    {
                        beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                        beginabsentTime = rote_on_time;
                    }
                else
                {
                    beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                    beginabsentTime = rote_on_time;
                }

                if (absentDate == Convert.ToDateTime(endDate).Date)
                {
                    if (Convert.ToInt32(endTime) <= Convert.ToInt32(rote_off_time))
                    {
                        endabsentDate = DateAndTimeMerger(absentDate, endTime);
                        endabsentTime = endTime;
                    }
                    else
                    {
                        endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                        endabsentTime = rote_off_time;
                    }
                }
                else
                {
                    endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                    endabsentTime = rote_off_time;
                }

                aOverTime.Begin = beginabsentDate;
                aOverTime.End = endabsentDate;

                Reduce.Clear();
                if (dr["ROTE_CODE"].ToString() != "00" || (dr["ROTE_CODE"].ToString() == "00" && includeHoliday == "Y"))
                {
                    string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                    restSql = restSql + "where ROTE_ID = " + roteID;
                    //DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
                    DataTable dtRest = this.ExecuteSql(restSql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans).Tables[0];
                    foreach (DataRow Row1 in dtRest.Rows)
                    {
                        restBeginTime = DateAndTimeMerger(absentDate, Row1["REST_BEGIN_TIME"].ToString());
                        restEndTime = DateAndTimeMerger(absentDate, Row1["REST_END_TIME"].ToString());

                        ts = restEndTime - restBeginTime;
                        if ((beginabsentDate <= restBeginTime) && (endabsentDate >= restEndTime))
                        {
                            if (roteCode == "00" && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                            {
                                Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                            }
                            else if (roteCode != "00" && Row1["IS_NORMAL_ABSENT"].ToString() == "Y")   //平日是否參考	5
                            {
                                Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                            }
                        }
                    }

                    //計算每天請假時間
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;

                    minutes = (TotalMinute - ReduceMinute);
                    hours = minutes / 60;

                    i = 0;

                    if (holidayUnit == "1") //小時
                    {
                        hours = hours >= minnum ? hours : minnum;
                        //間隔單位一定要大於零而且請假時間也要大於零
                        while ((absentUnit > 0) && (hours > 0) && (i < hours))
                            i += absentUnit;

                        hours = i;
                    }
                    else
                    {
                        hours = hours / dWorkHours;
                        hours = hours >= minnum ? hours : minnum;
                        //間隔單位一定要大於零而且請假時間也要大於零
                        while ((absentUnit > 0) && (hours > 0) && (i < hours))
                            i += absentUnit;
                        hours = i;
                    }

                    detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n"; ;
                    detailSql = detailSql + "select " + absentMinusID + ",'" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "'," + holidayID + "," + hours + ",'" + beginabsentTime + "','" + endabsentTime + "','" + beginabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "',dbo.funReturnSalaryYYMM('" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "')" + "\r\n";

                    //判斷檢查剩餘時數 == "Y"
                    //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                    if (checkRestHour == "Y")
                    {
                        foreach (DataRow drRestHours in dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                        {
                            string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                            if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                            {
                                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                                break;
                            }
                            else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                            {
                                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS -" + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                                drRestHours["REST_HOURS"] = 0;
                            }
                        }
                    }    //if (checkRestHour == "Y")
                }
            }
            this.ExecuteCommand(detailSql, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans);
        }

        private void ucHRM_ATTEND_ABSENT_MINUS_AfterApplied(object sender, EventArgs e)
        {
            DataTable addedTable = ucHRM_ATTEND_ABSENT_MINUS.CurrentRow.Table.GetChanges(DataRowState.Added);
            DataTable modifiedTable = ucHRM_ATTEND_ABSENT_MINUS.CurrentRow.Table.GetChanges(DataRowState.Modified);
            DataTable deletedTable = ucHRM_ATTEND_ABSENT_MINUS.CurrentRow.Table.GetChanges(DataRowState.Deleted);
            //新增資料
            if (addedTable != null && addedTable.Rows.Count > 0)
            {
                foreach (DataRow drAdd in addedTable.Rows)
                {
                    string employeeID = drAdd["EMPLOYEE_ID"].ToString();
                    string beginDate = drAdd["BEGIN_DATE"].ToString();
                    string endDate = drAdd["END_DATE"].ToString();
                    //執行刷卡轉出勤
                    var ObjParameter = new { EmployeeID = employeeID, DateFrom = beginDate, DateTo = endDate };
                    EEPRemoteModule remoteobject = new EEPRemoteModule();
                    object[] back = remoteobject.CallMethod(this.ClientInfo, "_HRM_Attend_Normal_AttendCalculate", "AttendCalculate_Single", new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
                }
            }

            //修改資料
            if (modifiedTable != null && modifiedTable.Rows.Count > 0)
            {
                var employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("EMPLOYEE_ID");
                var beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("BEGIN_DATE"));
                var endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("END_DATE"));
                //執行刷卡轉出勤
                var ObjParameter = new { EmployeeID = employeeID, DateFrom = beginDate, DateTo = endDate };
                EEPRemoteModule remoteobject = new EEPRemoteModule();
                object[] back = remoteobject.CallMethod(this.ClientInfo, "_HRM_Attend_Normal_AttendCalculate", "AttendCalculate_Single", new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
                foreach (DataRow drModify in modifiedTable.Rows)
                {
                    employeeID = drModify["EMPLOYEE_ID"].ToString();
                    beginDate = drModify["BEGIN_DATE"].ToString();
                    endDate = drModify["END_DATE"].ToString();
                    //執行刷卡轉出勤
                    ObjParameter = new { EmployeeID = employeeID, DateFrom = beginDate, DateTo = endDate };
                    //EEPRemoteModule remoteobject = new EEPRemoteModule();
                    remoteobject.CallMethod(this.ClientInfo, "_HRM_Attend_Normal_AttendCalculate", "AttendCalculate_Single", new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
                }
            }

            //刪除資料
            if (deletedTable != null && deletedTable.Rows.Count > 0)
            {
                var employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("EMPLOYEE_ID");
                var beginDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("BEGIN_DATE"));
                var endDate = changeToDateTime(ucHRM_ATTEND_ABSENT_MINUS.GetFieldOldValue("END_DATE"));
                //執行刷卡轉出勤
                var ObjParameter = new { EmployeeID = employeeID, DateFrom = beginDate, DateTo = endDate };
                EEPRemoteModule remoteobject = new EEPRemoteModule();
                object[] back = remoteobject.CallMethod(this.ClientInfo, "_HRM_Attend_Normal_AttendCalculate", "AttendCalculate_Single", new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
            }
            //_HRM_Attend_Normal_AttendCalculate.Component aComponent = new _HRM_Attend_Normal_AttendCalculate.Component();
            //aComponent.AttendCalculate_Single(new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
        }

        private void ucHRM_ATTEND_ABSENT_MINUS_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            var dataset = (DataSet)ucHRM_ATTEND_ABSENT_MINUS.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_ABSENT_MINUS);
            var table = (string)ucHRM_ATTEND_ABSENT_MINUS.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_ABSENT_MINUS);
            DataTable dt = ucHRM_ATTEND_ABSENT_MINUS.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_HRM_ATTEND_ABSENT_MINUS.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans, ucHRM_ATTEND_ABSENT_MINUS.SelectCmd.KeyFields);
            }

            string employeeID = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("EMPLOYEE_ID").ToString();
            string beginDate = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("BEGIN_DATE").ToString();
            string endDate = ucHRM_ATTEND_ABSENT_MINUS.GetFieldCurrentValue("END_DATE").ToString();

            //執行刷卡轉出勤
            //var ObjParameter = new { EmployeeID = employeeID, DateFrom = beginDate, DateTo = endDate };
            //EEPRemoteModule remoteobject = new EEPRemoteModule();
            //object[] back = remoteobject.CallMethod(this.ClientInfo,"_HRM_Attend_Normal_AttendCalculate", "AttendCalculate_Single", new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
            //_HRM_Attend_Normal_AttendCalculate.Component aComponent = new _HRM_Attend_Normal_AttendCalculate.Component();
            //aComponent.AttendCalculate_Single(new object[] { JsonConvert.SerializeObject(ObjParameter, Formatting.Indented) });
        }

        private void ucHRM_ATTEND_ABSENT_MINUS_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            var dataset = (DataSet)ucHRM_ATTEND_ABSENT_MINUS.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_ABSENT_MINUS);
            var table = (string)ucHRM_ATTEND_ABSENT_MINUS.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ucHRM_ATTEND_ABSENT_MINUS);
            DataTable dt = ucHRM_ATTEND_ABSENT_MINUS.GetSchema();

            for (int i = 0; i < dataset.Tables[table].Rows.Count; i++)
            {
                logInfo_HRM_ATTEND_ABSENT_MINUS.Log(dataset.Tables[table].Rows[i], dt, ucHRM_ATTEND_ABSENT_MINUS.conn, ucHRM_ATTEND_ABSENT_MINUS.trans, ucHRM_ATTEND_ABSENT_MINUS.SelectCmd.KeyFields);
            }
        }

        public object[] FileUpload(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);

                var aResult = new { FileName = "", };

                //【輸入驗證】：前台的DataForm輸入驗證
                var aCheckDictionary = FileUploadFormValidate(Parameter_Input);
                string ErrorMsg = aCheckDictionary.GetFirstErrorMsg();
                if (ErrorMsg.Length > 0) return new object[] { 0, new TheJsonResult { ErrorMsg = ErrorMsg }.ToJsonString() };

                //檔案路徑
                //string FilePathName = string.Format("../JQWebClient/{0}", Parameter_Input["FilePathName"]);
                string FilePathName = Parameter_Input["FilePathName"].ToString();

                //【取得ColumnName】：將前台輸入的資料轉換成ColumnName
                var aHeadList = FileUploadGetHeadList(Parameter_Input);

                //【抓資料】：ColumnName結合成新的DataTable                
                DataTable FileData = NPOIHelper.GetDataTable(FilePathName, aHeadList, G_HeadRowIndex);
                if (FileData == null) throw new Exception("【資料讀取】Error");

                //【欄位驗證】、【欄位轉換】：比對資料庫是否有這個數值，有就將數值轉換(Code->ID)
                if (!DataTableCellValidate(FileData)) throw new Exception("【欄位驗證】Error");
                if (FileData.HasErrors)
                {
                    if (!NPOIHelper.SetErrorMemo(FileData, FilePathName, aHeadList, G_HeadRowIndex)) throw new Exception("【檔案編輯】Error");
                    return new object[] { 0, new TheJsonResult { ErrorMsg = "欄位驗證有錯誤", Result = Parameter_Input["FilePathName"] }.ToJsonString() };
                }

                //【規則驗證】：整體驗證
                if (!DataTableDataValidate(FileData)) throw new Exception("【資料驗證】Error");
                if (FileData.HasErrors)
                {
                    if (!NPOIHelper.SetErrorMemo(FileData, FilePathName, aHeadList, G_HeadRowIndex)) throw new Exception("【檔案編輯】Error");
                    return new object[] { 0, new TheJsonResult { ErrorMsg = "資料驗證有錯誤", Result = Parameter_Input["FilePathName"] }.ToJsonString() };
                }

                //寫入
                if (!DataTableDataInsert(FileData)) throw new Exception("【資料寫入】Error");

                return new object[] { 0, new TheJsonResult { IsOK = true }.ToJsonString() };

            }
            catch (Exception ex)
            {
                return new object[] { 0, new TheJsonResult { ErrorMsg = "執行錯誤" }.ToJsonString() };
            }

        }

        //輸入驗證
        private TheDictionaryCheck FileUploadFormValidate(Dictionary<string, object> InputContent)
        {
            TheDictionaryCheck aCheckDictionary = new TheDictionaryCheck();
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "FilePathName", DisplayName = "檔案路徑" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "EMPLOYEE_ID", DisplayName = "員工編號" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_DATE", DisplayName = "起始請假日期" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_DATE", DisplayName = "截止請假日期" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_TIME", DisplayName = "請假起始時間" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_TIME", DisplayName = "請假截止時間" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "HOLIDAY_ID", DisplayName = "假別代碼" });
            aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "TOTAL_HOURS", DisplayName = "請假時數/天" });
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
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_DATE", DisplayName = "起始請假日期", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_DATE", DisplayName = "截止請假日期", IsRequired = true, IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.DateStr, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "BEGIN_TIME", DisplayName = "請假起始時間", IsSysCheck = true, SystemCheckType = TheDictionaryCheckType.Time48Hours, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "END_TIME", DisplayName = "請假截止時間", IsRequired = true, SystemCheckType = TheDictionaryCheckType.Time48Hours, IsUserCheck = false });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "HOLIDAY_ID", DisplayName = "假別代碼", IsRequired = true, IsUserCheck = true });
                aCheckDictionary.CheckData.Add(new TheDictionaryCheckData { FieldName = "TOTAL_HOURS", DisplayName = "請假時數/天", IsRequired = true, SystemCheckType = TheDictionaryCheckType.Decimal, IsUserCheck = false });
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

                    else if (aCheckData.FieldName == "HOLIDAY_ID")
                    {
                        var aHolidayData = ValidData.HolidayData.FirstOrDefault(m => m.Code == aCheckData.TheValue.ToString());
                        if (aHolidayData != null)
                        {
                            aCheckData.ErrorMsg = "";
                            aCheckData.IsOK = true;
                            aCheckData.TheResult = aHolidayData.ID;
                        }
                        else
                        {
                            aHolidayData = ValidData.HolidayData.FirstOrDefault(m => m.Name == aCheckData.TheValue.ToString());
                            if (aHolidayData != null)
                            {
                                aCheckData.ErrorMsg = "";
                                aCheckData.IsOK = true;
                                aCheckData.TheResult = aHolidayData.ID;
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
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            DataTableCellValidateData aAns = new DataTableCellValidateData();

            string sql = "select EMPLOYEE_ID, EMPLOYEE_CODE, NAME_C from HRM_BASE_BASE" + "\n\r";
            sql = sql + "select HOLIDAY_ID,HOLIDAY_CODE,HOLIDAY_CNAME FROM HRM_ATTEND_HOLIDAY" + "\n\r";

            DataSet DataSet = this.ExecuteSql(sql, connection, transaction);

            aAns.EmployeeData = DataSet.Tables[0].AsEnumerable().Select(m => new EmployeeData { ID = m.Field<string>("EMPLOYEE_ID"), Code = m.Field<string>("EMPLOYEE_CODE"), Name = m.Field<string>("NAME_C") }).ToList();
            aAns.HolidayData = DataSet.Tables[1].AsEnumerable().Select(m => new HolidayData { ID = m.Field<int>("HOLIDAY_ID"), Code = m.Field<string>("HOLIDAY_CODE"), Name = m.Field<string>("HOLIDAY_CNAME") }).ToList();

            return aAns;
        }

        //---------------------------------------資料驗證-----------------------------------------
        private bool DataTableDataValidate(DataTable aTable)
        {
            string employeeID = "";
            string beginDate = "";
            string endDate = "";
            string beginTime = "";
            string endTime = "";
            string holidayID = "";
            string totalHours = "";
            string salaryYYMM = "";
            string totalHoursMsg = "";
            int cnt = 0;

            bool flag = false;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            //1. 判斷請假起始日期不可大於截止日期
            //2. 判斷請假起始時間不可大於截止時間
            //3. 判斷請假日期是否已有鎖檔紀錄
            //4. 判斷請假時數
            //5. 判斷請假資料(EMPLOYEE_ID/ABSENT_DATE_TIME_BEGIN/ABSENT_DATE_TIME_END)申請的時段內是否已有存在的請假資料
            //6. 判斷請假剩餘時數
            try
            {
                foreach (DataRow aRow in aTable.Rows)
                {
                    flag = false;
                    employeeID = aRow["EMPLOYEE_ID"].ToString();
                    beginDate = aRow["BEGIN_DATE"].ToString();
                    endDate = aRow["END_DATE"].ToString();
                    beginTime = aRow["BEGIN_TIME"].ToString();
                    endTime = aRow["END_TIME"].ToString();
                    holidayID = aRow["HOLIDAY_ID"].ToString();
                    totalHours = aRow["TOTAL_HOURS"].ToString();
                    salaryYYMM = aRow["SALARY_YYMM"].ToString();
                    
                    //1. 判斷請假起始日期不可大於截止日期
                    if (DateTime.Parse(beginDate) > DateTime.Parse(endDate))
                    {
                        aRow.SetColumnError("BEGIN_DATE", "請假起始日期不可大於截止日期");
                        flag = true;
                    }

                    //2. 判斷請假起始時間不可大於截止時間
                    if (int.Parse(beginTime) >= int.Parse(endTime))
                    {
                        aRow.SetColumnError("BEGIN_TIME", "請假起始時間不可大於截止時間");
                        flag = true;
                    }

                    //3. 判斷請假日期是否已有鎖檔紀錄
                    string sql = " select COUNT(*) AS cnt from HRM_ATTEND_DATA_LOCK " + "\r\n";
                    sql = sql + " where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                    sql = sql + ",HRM_ATTEND_DATA_LOCK.ATTEND_DATE,'GROUP_ID')" + "\r\n";
                    if (beginDate == endDate)
                        sql = sql + " and ATTEND_DATE = '" + beginDate + "'";
                    else
                        sql = sql + " and ATTEND_DATE BETWEEN '" + beginDate + "' AND '" + endDate + "'";

                    DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);
                    cnt = int.Parse(dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["cnt"].ToString());

                    if (cnt > 0)
                    {
                        aRow.SetColumnError("END_DATE", "此區間請假日期已有鎖檔紀錄");
                        flag = true;
                    }

                    //4. 判斷請假時數
                    var parameters = new List<object>();
                    parameters.Add("0" + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID);
                    var obj = checkAbsentHours(parameters.ToArray());
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString()); 
                    if (int.Parse(dt.Rows[0]["totalHours"].ToString()) != int.Parse(totalHours))
                    {
                        totalHoursMsg = "請假時數不正確(請假時數應為 : " + dt.Rows[0]["totalHours"].ToString() + "小時)";
                        aRow.SetColumnError("TOTAL_HOURS",totalHoursMsg);
                        flag = true;
                    }

                    //5. 判斷請假資料(EMPLOYEE_ID/ABSENT_DATE_TIME_BEGIN/ABSENT_DATE_TIME_END)申請的時段內是否已有存在的請假資料
                    parameters.Clear();
                    parameters.Add("0" + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID);
                    obj = checkAbsentData(parameters.ToArray());
                    cnt = int.Parse(JsonConvert.DeserializeObject<String>(obj[1].ToString()));
                    if (cnt > 0)
                    {
                        totalHoursMsg = totalHoursMsg + "&&" + "申請的時段內已有存在的請假或公出資料";
                        aRow.SetColumnError("TOTAL_HOURS", totalHoursMsg);
                        flag = true;
                    }

                    //6. 判斷請假剩餘時數
                    parameters.Clear();
                    parameters.Add("0" + "," + employeeID + "," + beginDate + "," + endDate + "," + beginTime + "," + endTime + "," + holidayID + "," + totalHours);
                    obj = checkAbsentRestHours(parameters.ToArray());
                    dt = JsonConvert.DeserializeObject<DataTable>(obj[1].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["CHECK_REST_HOUR"].ToString() == "Y")
                        {
                            if (decimal.Parse(dt.Rows[0]["REST_HOURS"].ToString()) < decimal.Parse(totalHours))
                            {
                                totalHoursMsg = totalHoursMsg + "&&" + "剩餘時數不足(剩餘時數 : " + dt.Rows[0]["REST_HOURS"].ToString() + "小時)";
                                aRow.SetColumnError("TOTAL_HOURS", totalHoursMsg);
                                flag = true;
                            }
                        }
                    }

                    if (flag)
                        aRow.RowError = "驗證錯誤";
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
            string detailSql = "";
            int columnCount = 0;

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                sql = "";
                sql = sql + "declare @absentMINUSID int " + "\r\n";
                foreach (DataRow aRow in aTable.Rows)
                {
                    columnCount = aRow.ItemArray.Length;
                    sql = sql + "insert into HRM_ATTEND_ABSENT_MINUS (";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + column.ColumnName + ",";
                    }
                    sql = sql + " ABSENT_DATE_TIME_BEGIN,ABSENT_DATE_TIME_END,NOT_ALLOW_MODIFY,NOT_CALCULATE,SYSCREATE,IS_IMPORT,CREATE_MAN,CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n"; ;
                    sql = sql + " select '";
                    foreach (DataColumn column in aTable.Columns)
                    {
                        sql = sql + (aRow[column].ToString().Trim() == "" ? "null" : aRow[column].ToString().Trim()) + "','";
                    }
                    sql = sql + DateAndTimeMerger(Convert.ToDateTime(aRow["BEGIN_DATE"]).Date, aRow["BEGIN_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','";
                    sql = sql + DateAndTimeMerger(Convert.ToDateTime(aRow["END_DATE"]).Date, aRow["END_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "','";
                    sql = sql + "N','N','N','Y','";
                    sql = sql + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                    
                    sql = sql + "select @absentMINUSID = SCOPE_IDENTITY()" + "\r\n";
                    detailSql = createDetailSql(aRow);
                    sql = sql + detailSql;
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

        private string createDetailSql(DataRow aRow)
        {
            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";
            string holidayUnit = "";

            string includeHoliday = "";
            decimal absentUnit = 0;
            decimal minnum = 0;
            decimal maxnum = 0;
            decimal i = 0;
            decimal hours = 0;
            decimal dWorkHours = 0;

            DateTime beginabsentDate, restBeginTime, absentDate;
            DateTime endabsentDate, restEndTime;
            decimal minutes;

            TimeSpan ts;
            string roteCode = "";
            string beginabsentTime = "";
            string endabsentTime = "";

            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();
            
            string detailSql = "";
            string sql = "";
            
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            string employeeID = aRow["EMPLOYEE_ID"].ToString();
            string beginDate = aRow["BEGIN_DATE"].ToString();
            string endDate = aRow["END_DATE"].ToString();
            string beginTime = aRow["BEGIN_TIME"].ToString();
            string endTime = aRow["END_TIME"].ToString();
            string holidayID = aRow["HOLIDAY_ID"].ToString();
            decimal totalHours = decimal.Parse(aRow["TOTAL_HOURS"].ToString());
            string salaryYYMM = aRow["SALARY_YYMM"].ToString();

            try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY" + "\r\n";
                sql = sql + "from HRM_ATTEND_HOLIDAY" + "\r\n";
                sql = sql + "where HOLIDAY_ID=" + holidayID + "\r\n";

                DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);

                holidayUnit = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_UNIT"].ToString();    //假別單位
                minnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MIN_NUM"].ToString());  //請假最小單位數
                absentUnit = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["ABSENT_UNIT"].ToString());  //請假間隔最小單位數
                maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());  //年度最大可休時數(年)
                includeHoliday = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["INCLUDE_HOLIDAY"].ToString();  //包含假日

                sql = "select A.ATTEND_DATE,A.ROTE_ID," + "\r\n";
                sql = sql + "B.ROTE_CODE,B.ROTE_CNAME," + "\r\n";
                sql = sql + "B.ON_TIME," + "\r\n";
                sql = sql + "B.OFF_TIME," + "\r\n";
                sql = sql + "B.WORK_HRS," + "\r\n";
                sql = sql + "B.YEAR_REST_HRS," + "\r\n";
                sql = sql + "B.D_WORK_HRS," + "\r\n";
                sql = sql + "C.SEX, " + "\r\n";
                sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
                sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
                sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME, " + "\r\n";
                sql = sql + "D.YEAR_REST_HRS AS UPPER_YEAR_REST_HRS," + "\r\n";
                sql = sql + "D.D_WORK_HRS AS UPPER_WORK_HRS " + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE between '" + Convert.ToDateTime(beginDate).ToShortDateString() + "' and '" + Convert.ToDateTime(endDate).ToShortDateString() + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                sql = "select A.*  from HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
                sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                sql = sql + "and REST_HOURS >0" + "\r\n";
                sql = sql + "and B.HOLIDAY_FLAG = '+'" + "\r\n";
                sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

                DataSet dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction);

                foreach (DataRow dr in dsHRM_ATTEND_ATTEND.Tables[0].Rows)
                {
                    if (dr["ROTE_CODE"].ToString() != "00")
                    {
                        roteCode = dr["ROTE_CODE"].ToString();
                        rote_on_time = dr["ON_TIME"].ToString();
                        rote_off_time = dr["OFF_TIME"].ToString();
                        roteID = dr["ROTE_ID"].ToString();
                        dWorkHours = Convert.ToDecimal(dr["WORK_HRS"].ToString());
                    }
                    else
                    {
                        roteCode = dr["UPPER_ROTE_CODE"].ToString();
                        rote_on_time = dr["UPPER_ON_TIME"].ToString();
                        rote_off_time = dr["UPPER_OFF_TIME"].ToString();
                        roteID = dr["UPPER_ROTE_ID"].ToString();
                        dWorkHours = Convert.ToDecimal(dr["UPPER_WORK_HRS"].ToString());
                    }

                    absentDate = Convert.ToDateTime(dr["ATTEND_DATE"]).Date;

                    if (absentDate == Convert.ToDateTime(beginDate).Date)
                        if (Convert.ToInt32(beginTime) >= Convert.ToInt32(rote_on_time))
                        {
                            beginabsentDate = DateAndTimeMerger(absentDate, beginTime);
                            beginabsentTime = beginTime;
                        }
                        else
                        {
                            beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                            beginabsentTime = rote_on_time;
                        }
                    else
                    {
                        beginabsentDate = DateAndTimeMerger(absentDate, rote_on_time);
                        beginabsentTime = rote_on_time;
                    }

                    if (absentDate == Convert.ToDateTime(endDate).Date)
                    {
                        if (Convert.ToInt32(endTime) <= Convert.ToInt32(rote_off_time))
                        {
                            endabsentDate = DateAndTimeMerger(absentDate, endTime);
                            endabsentTime = endTime;
                        }
                        else
                        {
                            endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                            endabsentTime = rote_off_time;
                        }
                    }
                    else
                    {
                        endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                        endabsentTime = rote_off_time;
                    }

                    aOverTime.Begin = beginabsentDate;
                    aOverTime.End = endabsentDate;

                    Reduce.Clear();
                    if (dr["ROTE_CODE"].ToString() != "00" || (dr["ROTE_CODE"].ToString() == "00" && includeHoliday == "Y"))
                    {
                        string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                        restSql = restSql + "where ROTE_ID = " + roteID;
                        DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];

                        foreach (DataRow Row1 in dtRest.Rows)
                        {
                            restBeginTime = DateAndTimeMerger(absentDate, Row1["REST_BEGIN_TIME"].ToString());
                            restEndTime = DateAndTimeMerger(absentDate, Row1["REST_END_TIME"].ToString());

                            ts = restEndTime - restBeginTime;
                            if ((beginabsentDate <= restBeginTime) && (endabsentDate >= restEndTime))
                            {
                                if (roteCode == "00" && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                                {
                                    Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                                }
                                else if (roteCode != "00" && Row1["IS_NORMAL_ABSENT"].ToString() == "Y")   //平日是否參考	5
                                {
                                    Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                                }
                            }
                        }

                        //計算每天請假時間
                        Reduce = RangeCheck(Reduce);
                        var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                        var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;

                        minutes = (TotalMinute - ReduceMinute);
                        hours = minutes / 60;

                        i = 0;

                        if (holidayUnit == "1") //小時
                        {
                            hours = hours >= minnum ? hours : minnum;
                            //間隔單位一定要大於零而且請假時間也要大於零
                            while ((absentUnit > 0) && (hours > 0) && (i < hours))
                                i += absentUnit;

                            hours = i;
                        }
                        else
                        {
                            hours = hours / dWorkHours;
                            hours = hours >= minnum ? hours : minnum;
                            //間隔單位一定要大於零而且請假時間也要大於零
                            while ((absentUnit > 0) && (hours > 0) && (i < hours))
                                i += absentUnit;
                            hours = i;
                        }

                        detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n"; ;
                        detailSql = detailSql + "select @absentMINUSID,'" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "'," + holidayID + "," + hours + ",'" + beginabsentTime + "','" + endabsentTime + "','" + beginabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "',dbo.funReturnSalaryYYMM('" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "')" + "\r\n";

                        //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                        foreach (DataRow drRestHours in dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                        {
                            string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                            if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                            {
                                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + ",@absentMINUSID,'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                                break;
                            }
                            else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                            {
                                detailSql = detailSql + "insert into HRM_ATTEND_ABSENT_TRANS " + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + ",@absentMINUSID,'" + absentDate.ToString("yyyy-MM-dd") + "'," + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS -" + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                                drRestHours["REST_HOURS"] = 0;
                            }
                        }
                    }
                }
                dsUSERS.Dispose();
                dsHRM_ATTEND_HOLIDAY.Dispose();
                dsHRM_ATTEND_ATTEND.Dispose();
                dsHRM_ATTEND_ABSENT_PLUS.Dispose();
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
            return detailSql;
        }

        //--------------------------------------欄位驗證時候的資料-------------
        public class DataTableCellValidateData
        {
            public List<EmployeeData> EmployeeData { get; set; }
            public List<HolidayData> HolidayData { get; set; }
            

            public DataTableCellValidateData()
            {
                EmployeeData = new List<EmployeeData>();
                HolidayData = new List<HolidayData>();
            }
        }

        //人員資料
        public class EmployeeData
        {
            public string ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        //假別代碼資料,假別合併代碼資料
        public class HolidayData
        {
            public int ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class ImportData
        {
            public string EMPLOYEE_ID { get; set; }
            public string BEGIN_DATE { get; set; }
            public string END_DATE { get; set; }
            public string BEGIN_TIME { get; set; }
            public string END_TIME { get; set; }
            public string HOLIDAY_ID { get; set; }
            public string TOTAL_HOURS { get; set; }
            public string SALARY_YYMM { get; set; }
            public string MEMO { get; set; }
            public string ABSENT_NO { get; set; }
        }
    }
}
