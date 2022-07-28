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

namespace sHRMAttendAbsent
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
        //由登入的UserID 到 HRM_BASE_BASE 取得 EMPLOYEE_ID
        public object[] getEmployeeID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];
            var js = string.Empty;

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
                string sql = "select EMPLOYEE_ID from HRM_BASE_BASE where EMPLOYEE_CODE='" + EmployeeID + "'";
                DataSet dsHRM_BASE_BASE = this.ExecuteSql(sql, connection, transaction);
                string EMPLOYEE_ID = dsHRM_BASE_BASE.Tables[0].Rows[0]["EMPLOYEE_ID"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(EMPLOYEE_ID, Formatting.Indented);
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
        }

        //取得部門資料
        public object[] getDeptInfo(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];
            string CreateDate = parm[1];
            var js = string.Empty;

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
                string sql = "Select DEPT_ID,dbo.funReturnDeptInfo(DEPT_ID,2) AS DEPT_CNAME," +
                   "(select ROTE_ID from HRM_ATTEND_ATTEND  where EMPLOYEE_ID='" + EmployeeID + "' and ATTEND_DATE = '" + CreateDate + "') as ROTE_ID," +
                   "(select r.ROTE_CNAME from HRM_ATTEND_ATTEND t" +
                   " inner join HRM_ATTEND_ROTE r on t.ROTE_ID=r.ROTE_ID where t.EMPLOYEE_ID='" + EmployeeID + "' and t.ATTEND_DATE = '" + CreateDate + "') as ROTE_CNAME" +
                   " From dbo.[dtHRM_BaseAndBasetts_Employed](GetDate()) where EMPLOYEE_ID='" + EmployeeID + "'";
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }
        // 得到核示資訊
        public object[] GetSignNotesData(object[] objParam)
        {
            string absentMinusID = objParam[0].ToString();
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
                string sql = "SELECT * FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='AbsentMinusID=''" + absentMinusID + "'''" + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
            return new object[] { 0, js };
        }
        // 取得假別資訊(已請、在途、剩餘)
        public object[] GetHolidayData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];
            string CreateDate = parm[1];
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
                //固定 23特休(得),27補休(得),41事假(得),42病假(得) =>因為第一次請假才會產生 union 得假的部分扣除(23,27,41,42)
                //在途 N 新流程建立 ,P 流程過程中
                string SQL = " select h.HOLIDAY_CNAME,SUM(ABSENT_HOURS) as ABSENT_HOURS," + "\r\n" +
                   "Isnull((Select SUM(TotalHours) from JBADMIN.dbo.HRMAttendAbsentApply a left join HRM_ATTEND_HOLIDAY B on a.HolidayID = B.HOLIDAY_ID where EmployeeID=p.EMPLOYEE_ID" + "\r\n" +
                   " and (flowflag='N' or flowflag='P') and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID=h.HOLIDAY_ID)),0) as WAIT_HOURS," + "\r\n" +
                   "SUM(REST_HOURS)-Isnull((Select SUM(TotalHours) from JBADMIN.dbo.HRMAttendAbsentApply a left join HRM_ATTEND_HOLIDAY B on a.HolidayID = B.HOLIDAY_ID where EmployeeID=p.EMPLOYEE_ID" + "\r\n" +
                   " and (flowflag='N' or flowflag='P') and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID=h.HOLIDAY_ID)),0) as REST_HOURS" + "\r\n" +
                   " from HRM_ATTEND_ABSENT_PLUS p inner join HRM_ATTEND_HOLIDAY h on p.HOLIDAY_ID=h.HOLIDAY_ID" + "\r\n" +
                   " where p.EMPLOYEE_ID='" + EmployeeID + "' and '" + CreateDate + "' between BEGIN_DATE and END_DATE " + "\r\n" +
                   " group by h.HOLIDAY_ID,h.HOLIDAY_CNAME,p.EMPLOYEE_ID" + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
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

            string holidayUnit = "";

            string sex = "";
            string includeHoliday = "";
            string checkRestHour = "";
            decimal absentUnit = 0;
            decimal minnum = 0;
            decimal maxnum = 0;
            decimal hours = 0;
            decimal totalHours = 0;

            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

            string js = string.Empty;

            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHR_EEP";
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

                sql = "select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday'" + "\r\n"; 
                DataTable dtRoteMappingDetail = this.ExecuteSql(sql, connection, transaction).Tables[0];
                dtRoteMappingDetail.PrimaryKey = new DataColumn[] { dtRoteMappingDetail.Columns["ROTE_ID"] };

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
                sql = sql + "left join HRM_ATTEND_ROTE D on A.HOLIDAY_ROTE_ID = D.ROTE_ID" + "\r\n";
                sql = sql + "left join HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE between '" + beginDate + "' and '" + endDate + "'" + "\r\n";
                if (includeHoliday != "Y")
                    sql = sql + "and B.ROTE_ID not in (select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";
                    //sql = sql + " and B.ROTE_CODE <> '00' " + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                foreach (DataRow dr in dsHRM_ATTEND_ATTEND.Tables[0].Rows)
                {
                    var ObjParameter = new object[] { connection, transaction, dr, dtRoteMappingDetail, "D_WORK_HRS", beginDate, beginTime, endDate, endTime, includeHoliday, holidayUnit, minnum, maxnum, absentUnit };
                    object[] listHour = returnAbsentHours(ObjParameter);
                    hours = Convert.ToDecimal(listHour[0]);

                    totalHours = totalHours + hours;
                    //hours = 0;
                }

                sql = "select " + totalHours + " as totalHours," + hours + " as hours";
                //    sql = "select '" + rejectCode + "' as rejectCode, 0 as hours, 0 as totalHours" ;

                DataSet dsHours = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                dsHRM_ATTEND_HOLIDAY.Dispose();
                dsHRM_ATTEND_ATTEND.Dispose();
                dtRoteMappingDetail.Dispose();

                js = JsonConvert.SerializeObject(dsHours.Tables[0], Formatting.Indented);
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
        /// <summary>
        /// 計算時數(請假)
        /// </summary>
        /// <param name="connection">資料庫連線</param>
        /// <param name="transaction">資料庫transaction</param>
        /// <param name="dr">dsHRM_ATTEND_ATTEND.Tables[0].Rows</param>
        /// <param name="dtRoteMappingDetail">dtRoteMappingDetail</param>
        /// <param name="keyName_WORK_HRS">dWorkHours計算的欄位名稱：D_WORK_HRS、WORK_HRS</param>
        /// <param name="beginDate">beginDate</param>
        /// <param name="beginTime">beginTime</param>
        /// <param name="endDate">endDate</param>
        /// <param name="endTime">endTime</param>
        /// <param name="includeHoliday">includeHoliday</param>
        /// <param name="holidayUnit">holidayUnit</param>
        /// <param name="minnum">minnum</param>
        /// <param name="maxnum">maxnum</param>
        /// <param name="absentUnit">absentUnit</param>
        /// <returns>object[hours, beginabsentTime]</returns>
        public object[] returnAbsentHours(object[] objParam)
        {
            IDbConnection dbConnection = (IDbConnection)objParam[0];
            IDbTransaction dbTransaction = (IDbTransaction)objParam[1];
            DataRow dr = (DataRow)objParam[2];
            DataTable dtRoteMappingDetail = (DataTable)objParam[3];
            string keyName_WORK_HRS = (string)objParam[4];
            object beginDate = (object)objParam[5];
            string beginTime = (string)objParam[6];
            object endDate = (object)objParam[7];
            string endTime = (string)objParam[8];
            string includeHoliday = (string)objParam[9];
            string holidayUnit = (string)objParam[10];
            decimal minnum = (decimal)objParam[11];
            decimal maxnum = (decimal)objParam[12];
            decimal absentUnit = (decimal)objParam[13];

            string roteCode = "";
            string rote_on_time = "";
            string rote_off_time = "";
            string roteID = "";

            DateTime beginabsentDate, restBeginTime, absentDate;
            DateTime endabsentDate, restEndTime;
            decimal minutes;
            decimal dWorkHours = 0;

            TheTimeRange aOverTime = new TheTimeRange();
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TimeSpan ts;

            decimal hours = 0;
            decimal i = 0;
            string beginabsentTime = "";
            string endabsentTime = "";

            //if (dr["ROTE_CODE"].ToString() != "00")
            DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dr["ROTE_ID"].ToString());
            if (drRoteMappingDetail == null)
            {
                roteCode = dr["ROTE_CODE"].ToString();
                rote_on_time = dr["ON_TIME"].ToString();
                rote_off_time = dr["OFF_TIME"].ToString();
                roteID = dr["ROTE_ID"].ToString();
                dWorkHours = Convert.ToDecimal(dr[keyName_WORK_HRS].ToString());
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
            else
            {
                endabsentDate = DateAndTimeMerger(absentDate, rote_off_time);
                endabsentTime = rote_off_time;
            }

            aOverTime.Begin = beginabsentDate;
            aOverTime.End = endabsentDate;

            Reduce.Clear();
            

            //if (dr["ROTE_CODE"].ToString() != "00" || (dr["ROTE_CODE"].ToString() == "00" && includeHoliday == "Y"))
            if (drRoteMappingDetail == null || (drRoteMappingDetail != null && includeHoliday == "Y"))
            {
                string restSql = "select * from JBHR_EEP.dbo.HRM_ATTEND_ROTE_REST" + "\r\n";
                restSql = restSql + "where ROTE_ID = " + roteID;
                DataTable dtRest = this.ExecuteSql(restSql, dbConnection, dbTransaction).Tables[0];

                foreach (DataRow Row1 in dtRest.Rows)
                {
                    restBeginTime = DateAndTimeMerger(absentDate, Row1["REST_BEGIN_TIME"].ToString());
                    restEndTime = DateAndTimeMerger(absentDate, Row1["REST_END_TIME"].ToString());

                    ts = restEndTime - restBeginTime;

                    //當請假起始時間(12:30~17:00)落在休息時間(12:00~13:00)內，請假起始時間改由休息起始時間開始判斷(12:00~17:00)
                    if (beginabsentDate >= restBeginTime && beginabsentDate <= restEndTime)
                        beginabsentDate = restBeginTime;
                    //當請假截止時間(08:00~12:30)落在休息時間(12:00~13:00)內，請假截止時間改由休息截止時間開始判斷(08:00~13:00)
                    if (endabsentDate >= restBeginTime && endabsentDate <= restEndTime)
                        endabsentDate = restEndTime;

                    if ((beginabsentDate <= restBeginTime) && (endabsentDate >= restEndTime))
                    {
                        //if (roteCode == "00" && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                        DataRow drRoteMappingDetailRest = dtRoteMappingDetail.Rows.Find(dr["ROTE_ID"].ToString());
                        if (drRoteMappingDetailRest != null && Row1["IS_HOLIDAY_ABSENT"].ToString() == "Y")   //假日是否參考
                        {
                            Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                        }
                        //else if (roteCode != "00" && Row1["IS_NORMAL_ABSENT"].ToString() == "Y")   //平日是否參考	5
                        else if (drRoteMappingDetailRest == null && Row1["IS_NORMAL_ABSENT"].ToString() == "Y")   //平日是否參考	5
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

            return new object[] { hours, beginabsentTime, endabsentTime, beginabsentDate, endabsentDate, absentDate };
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
               // sql = sql + "and B.ROTE_CODE <>'00' " + "\r\n";
                sql = sql + "and B.ROTE_ID not in (select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";
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

        //4. 判斷請假剩餘時數(順便產生得假資料)
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
            string sStatus = parm[8];            
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
                //取得特休代碼資料
                string sql = "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'AnnualPlus'" + "\r\n";
                sql = sql + "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'Physiologyleave'" + "\r\n";
                sql = sql + "select HOLIDAY_ID from HRM_ATTEND_HOLIMAPPING_DETAIL where HOLIMAPPING_CODE = 'FamilyReunionLeave'" + "\r\n";
                DataSet dsHRM_ATTEND_HOLIMAPPING_DETAIL = this.ExecuteSql(sql, connection, transaction);
                physiologyHolidayID = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[1].Rows[0]["HOLIDAY_ID"].ToString(); //生理假
                familyReunionHolidayID = dsHRM_ATTEND_HOLIMAPPING_DETAIL.Tables[2].Rows[0]["HOLIDAY_ID"].ToString(); //家庭照顧假
                dsHRM_ATTEND_HOLIMAPPING_DETAIL.Dispose();

                //-----求得假別在途的總時數---------------
               
                sql = "Select Isnull((Select SUM(TotalHours) from JBADMIN.dbo.HRMAttendAbsentApply a left join HRM_ATTEND_HOLIDAY B on a.HolidayID = B.HOLIDAY_ID " + "\r\n";
                sql = sql + "where EmployeeID='" + employeeID + "' and (flowflag='N' or flowflag='P') and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")),0) as TotalHours";
                decimal WAIT_HOURS = decimal.Parse(this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["TotalHours"].ToString());
                if (sStatus == "1")//修改狀態=>在途=>0
                {
                    WAIT_HOURS = 0;
                }
                //-----求得假別在途的次數---------------
                sql = "Select Isnull((Select COUNT(*) from JBADMIN.dbo.HRMAttendAbsentApply a left join HRM_ATTEND_HOLIDAY B on a.HolidayID = B.HOLIDAY_ID " + "\r\n";
                sql = sql + "where EmployeeID='" + employeeID + "' and (flowflag='N' or flowflag='P') and B.HOLIDAY_KIND_ID = (select HOLIDAY_KIND_ID from HRM_ATTEND_HOLIDAY where HOLIDAY_ID = " + holidayID + ")),0) as OnFlowCount";
                int OnFlowCount = int.Parse(this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["OnFlowCount"].ToString());

                //-----求得假別可請最大時數-在途時數---------------
                sql = "select MAX_NUM from HRM_ATTEND_HOLIDAY WHERE HOLIDAY_ID = " + familyReunionHolidayID + "\r\n";
                decimal familyMaxHours = decimal.Parse(this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["MAX_NUM"].ToString()) - WAIT_HOURS;

                //--------------剩餘時數 REST_HOURS=>須扣除在途的時數------------
                sql = "select B.CHECK_REST_HOUR,SUM(A.REST_HOURS)-" + WAIT_HOURS + " as REST_HOURS, 0 as PHYSIOLOGY_CNT, 0.00 as FAMILY_HOURS, " + familyMaxHours + " as FAMILY_MAX_HOURS" + "\r\n";
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
                    string userName = SrvGL.GetUserName(userid);

                    int beginYear = Convert.ToDateTime(beginDate).Date.Year;
                    int endYear = Convert.ToDateTime(endDate).Date.Year;

                    retday = new DateTime(beginYear, 1, 1);
                    firstYearDay = retday.ToShortDateString();

                    retday = new DateTime(beginYear, 12, 31);
                    lastYearDay = retday.ToShortDateString();

                    string sysday = DateTime.Now.ToString();


                    sql = "select TOP 1 case when A.MAX_NUM = 0 or A.HOLIDAY_ID = 4 then isnull(C.MAX_NUM,0) else A.MAX_NUM end as MAX_NUM," + "\r\n";
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
                        sql = sql + "select '" + employeeID + "','" + firstYearDay + "','" + lastYearDay + "'," + plusHolidayID + "," + maxnum + ",0,0," + maxnum + ",'','','N','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
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
                            sql = sql + " select '" + employeeID + "','" + firstYearDay + "','" + lastYearDay + "'," + holidayID + "," + maxnum + ",0,0," + maxnum + ",'','','N','Y','" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
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

                //判斷每月只能請一次生理假(包含在途的次數=>OnFlowCount)
                if (holidayID == physiologyHolidayID)
                {
                    string beginFirstDay = Convert.ToDateTime(beginDate).AddDays(-Convert.ToDateTime(beginDate).Day + 1).ToShortDateString();
                    string beginLastDay = Convert.ToDateTime(beginDate).AddMonths(1).AddDays(-Convert.ToDateTime(beginDate).AddMonths(1).Day).ToShortDateString();
                    string endFirstDay = Convert.ToDateTime(endDate).AddDays(-Convert.ToDateTime(endDate).Day + 1).ToShortDateString();
                    string endLastDay = Convert.ToDateTime(endDate).AddMonths(1).AddDays(-Convert.ToDateTime(endDate).AddMonths(1).Day).ToShortDateString();
                    sql = "select count(*) as cnt from HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";
                    sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + "and HOLIDAY_ID = " + holidayID + "\r\n";
                    sql = sql + "and (ABSENT_DATE between '" + beginFirstDay + "' and '" + beginLastDay + "' or ABSENT_DATE between '" + endFirstDay + "' and '" + endLastDay + "')" + "\r\n";
                    string cnt = this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["cnt"].ToString();
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows[0]["PHYSIOLOGY_CNT"] = int.Parse(cnt) + OnFlowCount;
                }

                if (holidayID == familyReunionHolidayID)
                {
                    string beginFirstDay = beginDate.Substring(0, 4) + "/01/01";
                    string beginLastDay = beginDate.Substring(0, 4) + "/12/31";
                    string endFirstDay = endDate.Substring(0, 4) + "/01/01";
                    string endLastDay = endDate.Substring(0, 4) + "/12/31";
                    sql = "select Isnull(sum(ABSENT_HOURS),0) as ABSENT_HOURS from HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";
                    sql = sql + "where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                    sql = sql + "and HOLIDAY_ID = " + holidayID + "\r\n";
                    sql = sql + "and ABSENT_DATE between '" + beginFirstDay + "' and '" + beginLastDay + "'" + "\r\n";
                    decimal absentHours = decimal.Parse(this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["ABSENT_HOURS"].ToString());
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows[0]["FAMILY_HOURS"] = absentHours;
                    dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows[0]["FAMILY_MAX_HOURS"] = familyMaxHours;
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }
        //3.1 判斷生理假起始日期須等於截止日期 
        public object[] checkPhysiologyleavesID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string holidayID = parm[0];

            string js = string.Empty;

            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHR_EEP";
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }
        //3.2 判斷假別性別限制
        public object[] checkHolidaySex(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string holidayID = parm[0];
            string employeeID = parm[1];
            string sex = "";
            string cnt = "1";

            string js = string.Empty;

            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHR_EEP";
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        //6. 判斷申請的時段內是否已有存在的請假資料
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
        //7. 判斷請假資料(在途)
        public object[] checkOnData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AbsentMinusID = parm[0];
            string employeeID = parm[1];
            string beginDateTime = parm[2];
            string endDateTime = parm[3];          

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
                string sql = " select COUNT(*) AS cnt from HRMAttendAbsentApply" + "\r\n";
                sql = sql + " where EmployeeID = '" + employeeID + "'" + "\r\n";
                sql = sql + " and AbsentDateTimeBegin < '" + endDateTime + "'" + "\r\n";
                sql = sql + " and AbsentDateTimeEnd > '" + beginDateTime + "'" + "\r\n";
                sql = sql + " and (flowflag='N' or flowflag='P')";//N 新流程建立 ,P 流程過程中
                if (AbsentMinusID != "0")
                    sql = sql + " and AbsentMinusID <> " + AbsentMinusID;
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

        //6.1代理人檢查(是否代理人也有請假)=>另增加 "人資室" 不做檢查
        public object[] checkAgentEmployeeID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string AgentEmployeeID = parm[0];
            string beginDate = parm[1];
            string endDate = parm[2];
            string beginTime = parm[3];
            string endTime = parm[4];
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
                sql = sql + " where EMPLOYEE_ID = '" + AgentEmployeeID + "'" + "\r\n";
                sql = sql + " and ABSENT_DATE BETWEEN '" + beginDate + "'" + " and '" + endDate + "'" + "\r\n";
                sql = sql + " and BEGIN_TIME < '" + endTime + "'" + "\r\n";
                sql = sql + " and END_TIME > '" + beginTime + "'" + "\r\n";               

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

            string holidayUnit = "";

            string includeHoliday = "";
            decimal absentUnit = 0;
            decimal minnum = 0;
            decimal maxnum = 0;

            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

            DataRow drDara = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string absentMinusID = drDara["AbsentMinusID"].ToString();
            string employeeID = drDara["EmployeeID"].ToString();
            string beginDate = drDara["BeginDate"].ToString();
            string endDate = drDara["EndDate"].ToString();
            string beginTime = drDara["BeginTime"].ToString();
            string endTime = drDara["EndTime"].ToString();
            string holidayID = drDara["HolidayID"].ToString();
            string ORG_NO = drDara["ORG_NO"].ToString();

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
                string userName = SrvGL.GetUserName(userid);

                sql = "select HOLIDAY_UNIT,MIN_NUM,ABSENT_UNIT,MAX_NUM,SEX,INCLUDE_HOLIDAY" + "\r\n";
                sql = sql + "from JBHR_EEP.dbo.HRM_ATTEND_HOLIDAY" + "\r\n";
                sql = sql + "where HOLIDAY_ID=" + holidayID + "\r\n";
                DataSet dsHRM_ATTEND_HOLIDAY = this.ExecuteSql(sql, connection, transaction);

                holidayUnit = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["HOLIDAY_UNIT"].ToString();    //假別單位
                minnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MIN_NUM"].ToString());  //請假最小單位數
                absentUnit = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["ABSENT_UNIT"].ToString());  //請假間隔最小單位數
                maxnum = decimal.Parse(dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["MAX_NUM"].ToString());  //年度最大可休時數(年)
                includeHoliday = dsHRM_ATTEND_HOLIDAY.Tables[0].Rows[0]["INCLUDE_HOLIDAY"].ToString();  //包含假日

                sql = "select ROTE_ID from JBHR_EEP.dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday'" + "\r\n";
                DataTable dtRoteMappingDetail = this.ExecuteSql(sql, connection, transaction).Tables[0];
                dtRoteMappingDetail.PrimaryKey = new DataColumn[] { dtRoteMappingDetail.Columns["ROTE_ID"] };

                sql = "select A.ATTEND_DATE,A.ROTE_ID," + "\r\n";
                sql = sql + "B.ROTE_CODE,B.ROTE_CNAME," + "\r\n";
                sql = sql + "Isnull(B.ON_TIME,'0830') as ON_TIME," + "\r\n";
                sql = sql + "Isnull(B.OFF_TIME,'1730') as OFF_TIME," + "\r\n";
                sql = sql + "B.WORK_HRS," + "\r\n";
                sql = sql + "B.YEAR_REST_HRS," + "\r\n";
                sql = sql + "B.D_WORK_HRS," + "\r\n";
                sql = sql + "C.SEX, " + "\r\n";
                sql = sql + "D.ROTE_ID AS UPPER_ROTE_ID," + "\r\n";
                sql = sql + "D.ROTE_CODE AS UPPER_ROTE_CODE," + "\r\n";
                sql = sql + "D.ON_TIME AS UPPER_ON_TIME," + "\r\n";
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME, " + "\r\n";
                sql = sql + "D.YEAR_REST_HRS AS UPPER_YEAR_REST_HRS," + "\r\n";
                sql = sql + "Isnull(D.D_WORK_HRS,0) AS UPPER_D_WORK_HRS " + "\r\n";
                sql = sql + "from JBHR_EEP.dbo.HRM_ATTEND_ATTEND A" + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_ATTEND_ROTE B on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_ATTEND_ROTE D on D.ROTE_ID = JBHR_EEP.dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "left join JBHR_EEP.dbo.HRM_BASE_BASE C on A.EMPLOYEE_ID = C.EMPLOYEE_ID" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                if (includeHoliday != "Y")
                    sql = sql + "and B.ROTE_ID not in (select ROTE_ID from JBHR_EEP.dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";
                //sql = sql + " and B.ROTE_CODE <> '00' " + "\r\n";

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
                detailSql = detailSql + "TOTAL_DAY,MEMO,ABSENT_NO,NOT_ALLOW_MODIFY," + "\r\n";
                detailSql = detailSql + "NOT_CALCULATE,SYSCREATE,IS_IMPORT,FLOWFLAG,CREATE_MAN," + "\r\n";
                detailSql = detailSql + "CREATE_DATE,UPDATE_MAN,UPDATE_DATE,SALARY_YYMM)" + "\r\n";
                detailSql = detailSql + "select m.EmployeeID,m.BeginDate,m.EndDate,m.BeginTime,m.EndTime," + "\r\n";
                detailSql = detailSql + "m.AbsentDateTimeBegin,m.AbsentDateTimeEnd,m.HolidayID,m.TotalHours," + "\r\n";
                detailSql = detailSql + "cast(m.TotalHours/8.0 as decimal(10,2)),m.MEMO,'','N'," + "\r\n";
                detailSql = detailSql + "'N','N','N','" + ORG_NO + "','" + userName + "'," + "\r\n";
                detailSql = detailSql + "GETDATE(),'" + userName + "'," + "GETDATE(),[dbo].funReturnOVERTIMESALARY_YYMM(1,m.BeginDate) from HRMAttendAbsentApply m where AbsentMinusID=" + absentMinusID + "\r\n";
                detailSql = detailSql + " declare @absentMinusID int " + "\r\n";
                detailSql = detailSql + " SET @absentMinusID = SCOPE_IDENTITY() " + "\r\n";

                foreach (DataRow dr in dsHRM_ATTEND_ATTEND.Tables[0].Rows)
                {

                    var ObjParameter = new object[] { connection, transaction, dr, dtRoteMappingDetail, "WORK_HRS", beginDate, beginTime, endDate, endTime, includeHoliday, holidayUnit, minnum, maxnum, absentUnit };
                    object[] listHour = returnAbsentHours(ObjParameter);
                    decimal hours = Convert.ToDecimal(listHour[0]);
                    string beginabsentTime = listHour[1].ToString();
                    string endabsentTime = listHour[2].ToString();
                    DateTime beginabsentDate = Convert.ToDateTime(listHour[3]);
                    DateTime endabsentDate = Convert.ToDateTime(listHour[4]);
                    DateTime absentDate = Convert.ToDateTime(listHour[5]);

                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dr["ROTE_ID"].ToString());
                    //if (dr["ROTE_CODE"].ToString() != "00" || (dr["ROTE_CODE"].ToString() == "00" && includeHoliday == "Y"))
                    if (drRoteMappingDetail == null || (drRoteMappingDetail != null && includeHoliday == "Y"))
                    {
                        detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL(ABSENT_MINUS_ID,EMPLOYEE_ID,ABSENT_DATE,HOLIDAY_ID,ABSENT_HOURS,BEGIN_TIME," + "\r\n";
                        detailSql = detailSql + "END_TIME,ABSENT_DATE_TIME_BEGIN,ABSENT_DATE_TIME_END,SALARY_YYMM)" + "\r\n";
                        detailSql = detailSql + "select @absentMinusID,'" + employeeID + "','" + absentDate.ToString("yyyy-MM-dd") + "'," + holidayID + "," + hours + ",'" + beginabsentTime + "','" + endabsentTime + "','" + beginabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endabsentDate.ToString("yyyy-MM-dd HH:mm:ss") + "',[dbo].funReturnOVERTIMESALARY_YYMM(1,'" + absentDate.ToString("yyyy-MM-dd") + "')" + "\r\n";

                        //更新得假資料檔得假剩餘時數(REST_HOURS) && 請假沖銷時數(ABSENT_HOURS)
                        foreach (DataRow drRestHours in dsHRM_ATTEND_ABSENT_PLUS.Tables[0].Rows)
                        {
                            string absentPlusID = drRestHours["ABSENT_PLUS_ID"].ToString();
                            if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours >= 0)
                            {
                                detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS(ABSENT_PLUS_ID,ABSENT_MINUS_ID,ABSENT_DATE,ABSENT_HOURS,CREATE_MAN," + "\r\n";
                                detailSql = detailSql + "CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + ",@absentMinusID,'" + absentDate.ToString("yyyy-MM-dd") + "'," + hours + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + hours + ",REST_HOURS = REST_HOURS - " + hours + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                drRestHours["REST_HOURS"] = decimal.Parse(drRestHours["REST_HOURS"].ToString()) - hours;
                                break;
                            }
                            else if (decimal.Parse(drRestHours["REST_HOURS"].ToString()) > 0)
                            {                                
                                detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS(ABSENT_PLUS_ID,ABSENT_MINUS_ID,ABSENT_DATE,ABSENT_HOURS,CREATE_MAN," + "\r\n";
                                detailSql = detailSql + "CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                                detailSql = detailSql + "select " + absentPlusID + ",@absentMinusID,'" + absentDate.ToString("yyyy-MM-dd") + "'," + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                                detailSql = detailSql + "update JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS + " + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + ",REST_HOURS = REST_HOURS -" + decimal.Parse(drRestHours["REST_HOURS"].ToString()) + " where ABSENT_PLUS_ID = " + absentPlusID + "\r\n";
                                hours = hours - decimal.Parse(drRestHours["REST_HOURS"].ToString());
                                drRestHours["REST_HOURS"] = 0;
                            }
                        }
                    }
                }
                dsHRM_ATTEND_HOLIDAY.Dispose();
                dsHRM_ATTEND_ATTEND.Dispose();
                dsHRM_ATTEND_ABSENT_PLUS.Dispose();
                dtRoteMappingDetail.Dispose();
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
                
        private void ucHRMAttendAbsentApply_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            var absentMinusID = ucHRMAttendAbsentApply.GetFieldCurrentValue("AbsentMinusID");
            var employeeID = ucHRMAttendAbsentApply.GetFieldCurrentValue("EmployeeID");
            var beginDate = changeToDateTime(ucHRMAttendAbsentApply.GetFieldCurrentValue("BeginDate"));
            var endDate = changeToDateTime(ucHRMAttendAbsentApply.GetFieldCurrentValue("EndDate"));
            var beginTime = ucHRMAttendAbsentApply.GetFieldCurrentValue("BeginTime").ToString();
            var endTime = ucHRMAttendAbsentApply.GetFieldCurrentValue("EndTime").ToString();
            var holidayID = ucHRMAttendAbsentApply.GetFieldCurrentValue("HolidayID");

            ucHRMAttendAbsentApply.SetFieldValue("AbsentDateTimeBegin", DateAndTimeMerger(beginDate, beginTime));
            ucHRMAttendAbsentApply.SetFieldValue("AbsentDateTimeEnd", DateAndTimeMerger(endDate, endTime));

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucHRMAttendAbsentApply.SetFieldValue("CreateBy", LoginUser);//欄位賦值
            ucHRMAttendAbsentApply.SetFieldValue("EmployeeText", LoginUser);//欄位賦值

        }
        private void ucHRMAttendAbsentApply_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            var beginDate = changeToDateTime(ucHRMAttendAbsentApply.GetFieldCurrentValue("BeginDate"));
            var endDate = changeToDateTime(ucHRMAttendAbsentApply.GetFieldCurrentValue("EndDate"));
            var beginTime = ucHRMAttendAbsentApply.GetFieldCurrentValue("BeginTime").ToString();
            var endTime = ucHRMAttendAbsentApply.GetFieldCurrentValue("EndTime").ToString();
            var holidayID = ucHRMAttendAbsentApply.GetFieldCurrentValue("HolidayID");

            ucHRMAttendAbsentApply.SetFieldValue("AbsentDateTimeBegin", DateAndTimeMerger(beginDate, beginTime));
            ucHRMAttendAbsentApply.SetFieldValue("AbsentDateTimeEnd", DateAndTimeMerger(endDate, endTime));
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
                ReleaseConnection(sLoginDB, connection);
            }
            return ret; // 傳回值: 無
        }

        public object[] getEFFECT_DATE(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];
            var js = string.Empty;

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
                string sql = "Select	Top 1 T.EFFECT_DATE From	[JBHR_EEP].[dbo].[HRM_BASE_BASEIO] as T Where	T.EMPLOYEE_ID = '" + EmployeeID + "' and T.ACTION_TYPE = '1' and T.EFFECT_DATE <= GetDate() Order By T.EFFECT_DATE asc";
                DataSet dsHRM_BASE_BASE = this.ExecuteSql(sql, connection, transaction);
                string EFFECT_DATE = dsHRM_BASE_BASE.Tables[0].Rows[0]["EFFECT_DATE"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(EFFECT_DATE, Formatting.Indented);
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
        }

        public object[] GetUserOrgNOs(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;

        }


        //flow 假別>=3天送總經理簽核
        public object flowSendCheck(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string AbsentMinusID = dr["AbsentMinusID"].ToString();

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
                var detailSql = "";
                //flow 假別為=>婚喪喜慶 才送給總經理簽核
                //detailSql = "select COUNT(*) as icount from HRMAttendAbsentApply where AbsentMinusID=" + AbsentMinusID + " and HolidayID between 8 and 16";
                detailSql = "select COUNT(*) as icount from HRMAttendAbsentApply where AbsentMinusID=" + AbsentMinusID + " and TotalHours>=24";

                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                int icount = int.Parse(ds.Tables[0].Rows[0]["icount"].ToString());
                transaction.Commit(); // 確認交易

                if (icount > 0)
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

        //flow 是否通知代理人(請假日期小於今天=>不通知)
        public object flowCheckDate(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string AbsentMinusID = dr["AbsentMinusID"].ToString();

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
                string sql = " select datediff(day,Getdate(),EndDate) as cnt from HRMAttendAbsentApply where AbsentMinusID=" + AbsentMinusID;

                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                int cnt = int.Parse(dsFWCRM.Tables[0].Rows[0]["cnt"].ToString());
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                if (cnt >= 0)
                    ret[1] = true;//繼續流程
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
