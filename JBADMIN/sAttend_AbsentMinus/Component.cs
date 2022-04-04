using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace sAttend_AbsentMinus
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

        //取得部門資料
        public object[] getDeptInfo(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EMPLOYEE_CODE = parm[0];
            var js = string.Empty;
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
                string sql = "Select JBHR_EEP.dbo.funReturnDeptInfo(DEPT_ID,1) AS DEPT_CODE,JBHR_EEP.dbo.funReturnDeptInfo(DEPTC_ID,2) AS DEPT_CNAME"
                    + " From JBHR_EEP.dbo.[dtHRM_BaseAndBasetts_Employed](GetDate()) where EMPLOYEE_CODE='" + EMPLOYEE_CODE + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
        }
        //計算請假時數/天
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

            string sLoginDB = "JBHR_EEP";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY" + "\r\n";
                sql = sql + "from HRM_ATTEND_HOLIDAY" + "\r\n";
                sql = sql + "where HOLIDAY_ID=" + holidayID + "\r\n";

                DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);

                holidayUnit = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_UNIT"].ToString();    //假別單位
                minnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MIN_NUM"].ToString());  //請假最小單位數
                absentUnit = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["ABSENT_UNIT"].ToString());  //請假間隔最小單位數
                maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());  //年度最大可休時數(年)
                sex = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["SEX"].ToString(); //指定性別
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
                sql = sql + "D.D_WORK_HRS AS UPPER_D_WORK_HRS " + "\r\n";
                sql = sql + "from HRM_ATTEND_ATTEND A" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE between '" + beginDate + "' and '" + endDate + "'" + "\r\n";
                if (includeHoliday != "Y")
                    sql = sql + "and B.ROTE_CODE <> '00' " + "\r\n";

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
                                if (Row1["IS_NORMAL_ABSENT"].ToString() == "Y")  //請假單是否參考
                                {
                                    if (roteCode == "00" && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                                    {
                                        Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                                    }
                                    else if (roteCode != "00" && Row1["IS_NORMAL_OVERTIME"].ToString() == "Y")   //平日是否參考	5
                                    {
                                        Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                                    }
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
                //ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }
        //計薪年月(SALARY_YYMM) && 請起時間(ON_TIME) && 請迄時間(OFF_TIME)
        public object[] getSalaryYYMM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string beginDate = parm[1];

            string js = string.Empty;
            string rote_on_time = "";
            string rote_off_time = "";
            string salaryYYMM = "";

            string sLoginDB = "JBHR_EEP";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

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

                sql = "select distinct case when day(ATTEND_DATE) > (SELECT ATTEND_CLOSE_DAY FROM HRM_SYSTEM_SALARY_CONFIG ";
                sql = sql + "where COMPANY_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'COMPANY_ID')) then LEFT(CONVERT(varchar,dateadd(mm,1,ATTEND_DATE),112),6) else LEFT(CONVERT(varchar,ATTEND_DATE,112),6) end as SALARY_YYMM " + "\r\n";
                sql = sql + "from HRM_ATTEND_DATA_LOCK" + "\r\n";
                sql = sql + "where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",ATTEND_DATE,'GROUP_ID')" + "\r\n";
                sql = sql + "and ATTEND_DATE >= '" + beginDate + "'" + "\r\n";
                sql = sql + " ORDER BY SALARY_YYMM DESC";

                DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);

                sql = "select case when day(convert(datetime,'" + beginDate + "')) > (select ATTEND_CLOSE_DAY from HRM_SYSTEM_SALARY_CONFIG ";
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
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

        //6. 判斷請假剩餘時數
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

            DateTime retday;
            string firstYearDay, lastYearDay;

            string js = string.Empty;

            string sLoginDB = "JBHR_EEP";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select B.CHECK_REST_HOUR,SUM(A.REST_HOURS) as REST_HOURS from dbo.HRM_ATTEND_ABSENT_PLUS A " + "\r\n";
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


                    sql = "select TOP 1 A.MAX_NUM,A.CHECK_REST_HOUR,B.HOLIDAY_ID,ISNULL(B.AUTO_CREATE,'N') AS AUTO_CREATE " + "\r\n";
                    sql = sql + "from HRM_ATTEND_HOLIDAY A" + "\r\n";
                    sql = sql + "LEFT JOIN HRM_ATTEND_HOLIDAY B ON A.HOLIDAY_KIND_ID = B.HOLIDAY_KIND_ID" + "\r\n";
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

                    //判斷檢查剩餘時數 =='Y' 且 年度最大可休時數(年) > 0)
                    if (autoCreate == "Y" && checkRestHour == "Y" && maxnum > 0)
                    {
                        sql = "insert into HRM_ATTEND_ABSENT_PLUS " + "\r\n";
                        sql = sql + "select '" + employeeID + "','" + firstYearDay + "','" + lastYearDay + "'," + plusHolidayID + "," + maxnum + ",0,0," + maxnum + ",'','','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
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
                            sql = sql + " select '" + employeeID + "','" + firstYearDay + "','" + lastYearDay + "'," + holidayID + "," + maxnum + ",0,0," + maxnum + ",'','','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                            sql = sql + "declare @absentPlusID int " + "\r\n";
                            sql = sql + "select @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                            sql = sql + "insert into HRM_ATTEND_ABSENT_TRANS ";
                            sql = sql + " select @absentPlusID,null, null, " + maxnum + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                        }
                        this.ExecuteSql(sql, connection, transaction);
                    }
                    dsHRM_ATTEND_ABSENT_PLUS.Dispose();

                    sql = "select B.CHECK_REST_HOUR,SUM(A.REST_HOURS) as REST_HOURS from dbo.HRM_ATTEND_ABSENT_PLUS A " + "\r\n";
                    sql = sql + "left join HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                    sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                    sql = sql + "and ('" + beginDate + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + endDate + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                    sql = sql + "and B.HOLIDAY_FLAG = '+'" + "\r\n";
                    sql = sql + "group by B.CHECK_REST_HOUR";

                    dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction);

                    //判斷檢查剩餘時數 =='Y' 且無得假資料
                    if (checkRestHour == "Y" && dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows.Count == 0)
                    {
                        DataRow aRow = dsHRM_ATTEND_ABSENT_PLUS.Tables[0].NewRow();
                        aRow["CHECK_REST_HOUR"] = "Y";
                        aRow["REST_HOURS"] = 0;
                        dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows.Add(aRow);
                    }
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }
        //修改判斷請假日期是否已有鎖檔紀錄
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

            string sLoginDB = "JBHR_EEP";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        //Flow請假成功=>新增資料到JBHR_EEP
        //1.新增 請假資料檔 HRM_ATTEND_ABSENT_MINUS , 請假資料明細檔 HRM_ATTEND_ABSENT_MINUS_DETAIL , 請假對沖明細檔 HRM_ATTEND_ABSENT_TRANS 
        //2.修改得假資料檔 HRM_ATTEND_ABSENT_PLUS => 請假沖銷時數 ABSENT_HOURS , 得假剩餘時數 REST_HOURS 
        public object procInsertHRM_ATTEND_ABSENT_MINUS(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };

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

            DataRow drDara = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            int absentMinusID = Convert.ToInt32(drDara["ABSENT_MINUS_ID"].ToString());
            var employeeID = drDara["EMPLOYEE_ID"].ToString();
            var beginDate = drDara["BEGIN_DATE"].ToString();
            var endDate = drDara["END_DATE"].ToString();
            var beginTime = drDara["BEGIN_TIME"].ToString();
            var endTime = drDara["END_TIME"].ToString();
            var holidayID = drDara["HOLIDAY_ID"].ToString();

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
                var sql = "";
                var detailSql = "";

                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                sql = "select USERNAME from JBHR_EEP.dbo.USERS where USERID= '" + userid + "'";
                DataSet dsUSERS = this.ExecuteSql(sql, connection, transaction);
                string userName = dsUSERS.Tables[0].Rows[0]["USERNAME"].ToString();

                sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY" + "\r\n";
                sql = sql + "from JBHR_EEP.dbo.HRM_ATTEND_HOLIDAY" + "\r\n";
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
                sql = sql + "from JBHR_EEP.dbo.HRM_ATTEND_ATTEND A" + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_ATTEND_ROTE D on D.ROTE_ID = JBHR_EEP.dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE between '" + Convert.ToDateTime(beginDate).ToShortDateString() + "' and '" + Convert.ToDateTime(endDate).ToShortDateString() + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                //請假沖銷明細資料(dbo.HRM_ATTEND_ABSENT_TRANS)
                sql = "select A.*  from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS A  " + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_ATTEND_HOLIDAY B on A.HOLIDAY_ID = B.HOLIDAY_ID " + "\r\n";
                sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + "and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from JBHR_EEP.dbo.HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")" + "\r\n";
                sql = sql + "and ('" + Convert.ToDateTime(beginDate).ToShortDateString() + "'" + " between A.BEGIN_DATE and A.END_DATE or '" + Convert.ToDateTime(endDate).ToShortDateString() + "' between A.BEGIN_DATE and A.END_DATE)" + "\r\n";
                sql = sql + "and REST_HOURS >0" + "\r\n";
                sql = sql + "and B.HOLIDAY_FLAG = '+'" + "\r\n";
                sql = sql + "order by A.BEGIN_DATE,A.END_DATE" + "\r\n";

                DataSet dsHRM_ATTEND_ABSENT_PLUS = this.ExecuteSql(sql, connection, transaction);

                //Add HRM_ATTEND_ABSENT_MINUS

                detailSql = "Insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS(EMPLOYEE_ID,BEGIN_DATE,END_DATE,BEGIN_TIME,END_TIME," + "\r\n";
                detailSql = detailSql + "ABSENT_DATE_TIME_BEGIN,ABSENT_DATE_TIME_END,HOLIDAY_ID,TOTAL_HOURS," + "\r\n";
                detailSql = detailSql + "TOTAL_DAY,SALARY_YYMM,MEMO,ABSENT_NO,NOT_ALLOW_MODIFY," + "\r\n";
                detailSql = detailSql + "NOT_CALCULATE,SYSCREATE,IS_IMPORT,FLOWFLAG,CREATE_MAN," + "\r\n";
                detailSql = detailSql + "CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                detailSql = detailSql + "select m.EMPLOYEE_ID,m.BEGIN_DATE,m.END_DATE,m.BEGIN_TIME,m.END_TIME," + "\r\n";
                detailSql = detailSql + "m.ABSENT_DATE_TIME_BEGIN,m.ABSENT_DATE_TIME_END,m.HOLIDAY_ID,m.TOTAL_HOURS," + "\r\n";
                detailSql = detailSql + "m.TOTAL_DAY,m.SALARY_YYMM,m.MEMO,'','N'," + "\r\n";
                detailSql = detailSql + "'N','N','N','Z','" + userName + "'," + "\r\n";
                detailSql = detailSql + "GETDATE(),'" + userName + "'," + "GETDATE() from HRM_ATTEND_ABSENT_MINUS m where ABSENT_MINUS_ID=" + absentMinusID + "\r\n";
                detailSql = detailSql + " declare @absentMinusID int " + "\r\n";
                detailSql = detailSql + " SET @absentMinusID = SCOPE_IDENTITY() " + "\r\n";

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
                        string restSql = "select * from JBHR_EEP.dbo.HRM_ATTEND_ROTE_REST" + "\r\n";
                        restSql = restSql + "where ROTE_ID = " + roteID;
                        //DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
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

                        detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL(ABSENT_MINUS_ID,EMPLOYEE_ID,ABSENT_DATE,HOLIDAY_ID,ABSENT_HOURS,BEGIN_TIME," + "\r\n";
                        detailSql = detailSql + "END_TIME,ABSENT_DATE_TIME_BEGIN,ABSENT_DATE_TIME_END,SALARY_YYMM)" + "\r\n";
                        detailSql = detailSql + "select @absentMinusID,'" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "'," + holidayID + "," + hours + ",'" + beginabsentTime + "','" + endabsentTime + "','" + beginabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "',JBHR_EEP.dbo.funReturnSalaryYYMM('" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "')" + "\r\n";

                        //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                        foreach (DataRow drRestHours in dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                        {
                            string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                            if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                            {
                                detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS(ABSENT_PLUS_ID,ABSENT_MINUS_ID,ABSENT_DATE,ABSENT_HOURS,CREATE_MAN," + "\r\n";
                                detailSql = detailSql + "CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                                break;
                            }
                            else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                            {
                                detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS(ABSENT_PLUS_ID,ABSENT_MINUS_ID,ABSENT_DATE,ABSENT_HOURS,CREATE_MAN," + "\r\n";
                                detailSql = detailSql + "CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + "," + absentMinusID + ",'" + absentDate.ToString("yyyy-MM-dd") + "'," + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS -" + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                                drRestHours["REST_HOURS"] = 0;
                            }
                        }
                    }
                }
                this.ExecuteSql(detailSql, connection, transaction);
                transaction.Commit(); // 確認交易
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

        //跑flow時請假時數審核
        public object flowCheckHours(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            //string Today = System.DateTime.Today.ToShortDateString(); //取得今天日期
            decimal upperManagerHour = 0;
            decimal absentHours = decimal.Parse(dr["TOTAL_HOURS"].ToString()); // 取得請假時數
            string employeeID = dr["EMPLOYEE_ID"].ToString();

            string sLoginDB = "JBHR_EEP";
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

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




    }
}
