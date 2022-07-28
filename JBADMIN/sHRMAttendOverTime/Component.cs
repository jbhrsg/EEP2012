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

namespace sHRMAttendOverTime
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
        //���o�s��=> ex: O10500001----����~+�y���X(5)
        public string OverTimeNOFixed()
        {
            //return string.Format("O{0:yyMMdd}", DateTime.Now.Date);
            DateTime datetime = DateTime.Today;
            return "O" + (datetime.Year-1911).ToString().Trim();
        }
        //�ѵn�J��UserID �� HRM_BASE_BASE ���o EMPLOYEE_ID
        public object[] getEmployeeID(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];           
            var js = string.Empty;

            string sLoginDB = "JBHR_EEP";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select EMPLOYEE_ID from HRM_BASE_BASE where EMPLOYEE_CODE='" + EmployeeID + "'";
                DataSet dsHRM_BASE_BASE = this.ExecuteSql(sql, connection, transaction);
                string EMPLOYEE_ID = dsHRM_BASE_BASE.Tables[0].Rows[0]["EMPLOYEE_ID"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
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
        //���o�������
        public object[] getDeptInfo(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];
            string CreateDate = parm[1];            
            var js = string.Empty;

            string sLoginDB = "JBHR_EEP";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Select DEPT_ID,dbo.funReturnDeptInfo(DEPT_ID,2) AS DEPT_CNAME," +
                   "(select ROTE_ID from HRM_ATTEND_ATTEND  where EMPLOYEE_ID='" + EmployeeID + "' and ATTEND_DATE = '" + CreateDate + "') as ROTE_ID,"+
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
        //���o�[�Z�w�]�����ɶ�
        public object[] getOFF_TIME(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EmployeeID = parm[0];
            string CreateDate = parm[1];
            var js = string.Empty;

            string sLoginDB = "JBHR_EEP";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select r.OFF_TIME from HRM_ATTEND_ATTEND t" +
                   " inner join HRM_ATTEND_ROTE r on t.ROTE_ID=r.ROTE_ID where t.EMPLOYEE_ID='" + EmployeeID + "' and t.ATTEND_DATE = '" + CreateDate + "';";
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
        //�P�_�[�Z�ɼ�
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
            decimal hours, minutes, totalHours, restHours;

            TimeSpan ts;
            string roteCode = "";
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

            string NotCheck = "N";

            string js = string.Empty;


            string sLoginDB = "JBHR_EEP";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //string sql = "select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' " + "\r\n";
                string sql = "select ROTE_ID from HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE != 'OffDay' and ROTEMAPPING_CODE != 'Holidays' and ROTEMAPPING_CODE != 'NationalHoliday' " + "\r\n";

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
                sql = sql + "D.OFF_TIME AS UPPER_OFF_TIME " + "\r\n";       
                sql = sql + "from HRM_ATTEND_ATTEND A " + "\r\n";                
                sql = sql + "left join HRM_ATTEND_ROTE C on A.ROTE_ID = C.ROTE_ID " + "\r\n";
                sql = sql + "left join HRM_ATTEND_ROTE D on D.ROTE_ID = dbo.funReturnRote(A.EMPLOYEE_ID,A.ATTEND_DATE)" + "\r\n";
                sql = sql + "where A.EMPLOYEE_ID='" + employeeID + "'" + "\r\n";
                sql = sql + "and A.ATTEND_DATE = '" + overtimeDate + "'" + "\r\n";

                DataSet dsHRM_ATTEND_ATTEND = this.ExecuteSql(sql, connection, transaction);

                if (dsHRM_ATTEND_ATTEND.Tables[0].Rows.Count != 0)
                {
                    roteCode = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString();//D01�Z�O
                    rote_on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();//0830
                    rote_off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();//1730

                    //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString());
                    if (drRoteMappingDetail == null) 
                    {
                        roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString();
                        on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ON_TIME"].ToString();
                        off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OFF_TIME"].ToString();
                        otBeginTime = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["OT_BEGIN_TIME"].ToString();
                    }
                    else//ROTE_CODE=>00 ����
                    {
                        roteID = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ROTE_ID"].ToString();
                        on_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_ON_TIME"].ToString();
                        off_time = dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["UPPER_OFF_TIME"].ToString();
                    }
                   
                }
                else
                    rejectCode = "4";   //�ӽФ���|�����ͭ��u�Z��

                //�P�_�Y�L���~�T��--�p��[�Z�ɼ�
                if (rejectCode != "4")
                {
                    beginOvertimeDate = DateAndTimeMerger(overtimeDate, beginTime);
                    endOvertimeDate = DateAndTimeMerger(overtimeDate, endTime);
                    otBeginTimeDate = DateAndTimeMerger(overtimeDate, otBeginTime);
                    aOverTime.Begin = beginOvertimeDate;
                    aOverTime.End = endOvertimeDate;

                    //�o�X�а�����O�_���ѵM�a�`��
                    string sql2 = "select count(*) as iCount from HRM_ATTEND_CALENDAR_HOLIDAY where CALENDAR_HOLIDAY_DATE='" + overtimeDate + "' and HOLIDAY_TYPE_ID=5" + "\r\n";
                    DataSet ds = this.ExecuteSql(sql2, connection, transaction);
                    int hiCount = int.Parse(ds.Tables[0].Rows[0]["iCount"].ToString());
                    if (hiCount == 1) NotCheck = "Y";

                    //�p�⥿�`�W�Z�ɶ�
                    //if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                    DataRow drRoteMappingDetail = dtRoteMappingDetail.Rows.Find(dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_ID"].ToString());
                    if (drRoteMappingDetail == null && NotCheck != "Y")
                    {
                        roteBeginTime = DateAndTimeMerger(overtimeDate, rote_on_time);
                        roteEndTime = DateAndTimeMerger(overtimeDate, rote_off_time);

                        if (!(beginOvertimeDate >= otBeginTimeDate || beginOvertimeDate < roteBeginTime))
                            rejectCode = "5";   //�[�Z�_�l�ɶ����b�X�z�ɶ��d��
                        else if (!(endOvertimeDate <= roteBeginTime.AddDays(1)))
                            rejectCode = "6";   //�[�Z�I��ɶ����b�X�z�ɶ��d��

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
                    
                        //���ȥ𮧮ɶ�
                        string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                        restSql = restSql + "where ROTE_ID = " + roteID;
                        DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
                        foreach (DataRow Row1 in dtRest.Rows)
                        {
                            restBeginTime = DateAndTimeMerger(overtimeDate, Row1["REST_BEGIN_TIME"].ToString());
                            restEndTime = DateAndTimeMerger(overtimeDate, Row1["REST_END_TIME"].ToString());
                            ts = restEndTime - restBeginTime;

                            //if (roteCode == "00" && Row1["IS_HOLIDAY_OVERTIME"].ToString() == "Y")   //����O�_�Ѧ�
                            if (drRoteMappingDetail != null && Row1["IS_HOLIDAY_OVERTIME"].ToString() == "Y")   //����O�_�Ѧ�
                            {
                                Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                            }
                            //else if (roteCode != "00" && Row1["IS_NORMAL_OVERTIME"].ToString() == "Y")   //����O�_�Ѧ�	5
                            else if (drRoteMappingDetail == null && Row1["IS_NORMAL_OVERTIME"].ToString() == "Y")   //����O�_�Ѧ�	5
                            {
                                //restHours = restHours + Convert.ToDecimal(ts.TotalHours);
                                //restMinutes = restMinutes + Convert.ToDecimal(ts.TotalMinutes);
                                Reduce.Add(new TheTimeRange { Begin = restBeginTime, End = restEndTime });
                            }
                        }
                    }
                                    
                    //�p��[�Z�ɶ�
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;

                    int iMin = Convert.ToInt32((TotalMinute - ReduceMinute) % 30);
                    minutes = ((TotalMinute - ReduceMinute) - iMin);
                    hours = minutes / 60;
                    restHours = ReduceMinute / 60;//�𮧮ɼ�
                    totalHours = (TotalMinute - Convert.ToInt32(TotalMinute % 30)) / 60;
                    minutes = minutes >= 30 ? minutes : 0;
                    hours = Convert.ToDouble(hours) >= 0.5 ? hours : 0;

                    sql = "select '" + rejectCode + "' as rejectCode," + hours + " as hours," + restHours + " as restHours," + totalHours + " as totalHours";
                }
                else
                {
                    sql = "select '" + rejectCode + "' as rejectCode, 0 as hours, 0 as restHours, 0 as totalHours";
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

        //Rang2�bRang1���Ҧ���������
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
        //3. �P�_�[�Z���(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)�ӽЪ��ɬq���O�_�w���s�b���[�Z���
        public object[] checkOvertimeData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string OverTimeNO = parm[0];
            string employeeID = parm[1];
            string overtimeDate = parm[2];
            string beginTime = parm[3];
            string endTime = parm[4];

            string js = string.Empty;


            string sLoginDB = "JBHR_EEP";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) AS cnt from HRM_ATTEND_OVERTIME_DATA " + "\r\n";
                sql = sql + " where EMPLOYEE_ID = '" + employeeID + "'" + "\r\n";
                sql = sql + " and OVERTIME_DATE = '" + overtimeDate + "'" + "\r\n";
                sql = sql + " and BEGIN_TIME < '" + endTime + "'" + "\r\n";
                sql = sql + " and END_TIME > '" + beginTime + "'" + "\r\n";               

                DataSet dsHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
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
        //4. �P�_�[�Z���(�b�~)
        public object[] checkOnData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string OverTimeNO = parm[0];
            string employeeID = parm[1];
            string overtimeDate = parm[2];
            string beginTime = parm[3];
            string endTime = parm[4];

            string js = string.Empty;


            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) AS cnt from HRMAttendOverTimeApplyMaster m inner join HRMAttendOverTimeApplyDetails d on m.OverTimeNO=d.OverTimeNO " + "\r\n";
                sql = sql + " where m.EmployeeID = '" + employeeID + "'" + "\r\n";
                sql = sql + " and d.OverTimeDate = '" + overtimeDate + "'" + "\r\n";
                sql = sql + " and d.BeginTime < '" + endTime + "'" + "\r\n";
                sql = sql + " and d.EndTime > '" + beginTime + "'" + "\r\n";
                sql = sql + " and (flowflag='N' or flowflag='P')";//N �s�y�{�إ� ,P �y�{�L�{��
                if (OverTimeNO != "0")
                    sql = sql + " and m.OverTimeNO <> '" + OverTimeNO + "'";

                DataSet dsHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
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

        //Flow�[�Z���\=>�s�W��ƨ�JBHR_EEP
        public object procInsertHRMAttendOverTimeApplyMaster(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            var OverTimeNO = drDara["OverTimeNO"].ToString();

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {                
                var detailSql = "";

                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string userName = SrvGL.GetUserName(userid);

                detailSql = "Insert into JBHR_EEP.dbo.HRM_ATTEND_OVERTIME_DATA(EMPLOYEE_ID,OVERTIME_DATE,BEGIN_TIME,OVERTIME_DATE_TIME_BEGIN,END_TIME,OVERTIME_DATE_TIME_END,OVERTIME_EFFECT_DATE," + "\r\n";
                detailSql = detailSql + "TOTAL_HOURS,OVERTIME_HOURS,REST_HOURS,MEMO,OVERTIME_ROTE_ID,OVERTIME_DEPT_ID,OVERTIME_NO,SALARY_YYMM," + "\r\n";
                detailSql = detailSql + "CREATE_MAN,CREATE_DATE,UPDATE_MAN,UPDATE_DATE)" + "\r\n";
                detailSql = detailSql + " select m.EmployeeID,d.OverTimeDate,d.BeginTime,d.OverTimeDateTimeBegin,d.EndTimeTemp,d.OverTimeDateTimeEndTemp,'9999/12/31'," + "\r\n";
                //detailSql = detailSql + "d.TotalHours-d.DinnerHours,case when d.OverTimeHours>0 then d.OverTimeHours-d.DinnerHours else d.OverTimeHours end,case when d.RestHours>0 then d.RestHours-d.DinnerHours else d.RestHours end," + "\r\n";
                detailSql = detailSql + "d.TotalHours,d.OverTimeHours,d.RestHours," + "\r\n";
                detailSql = detailSql + "d.OverTimeCauseID,m.OverTimeRoteID,m.OverTimeDeptID,d.OverTimeNO,[dbo].funReturnOVERTIMESALARY_YYMM(1,d.OverTimeDate)," + "\r\n";
                detailSql = detailSql + "'" + userName + "',GETDATE(),'" + userName + "',GETDATE()" + "\r\n";
                detailSql = detailSql + " from HRMAttendOverTimeApplyMaster m inner join HRMAttendOverTimeApplyDetails d on m.OverTimeNO=d.OverTimeNO where m.OverTimeNO='" + OverTimeNO + "'\r\n";
                detailSql = detailSql + " insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS " + "\r\n";
                detailSql = detailSql + " select EMPLOYEE_ID,OVERTIME_DATE,dbo.funReturnHRM_ATTEND_ABSENT_PLUSEND_DATE(OVERTIME_DATE,EMPLOYEE_ID),27,sum(REST_HOURS),null,0,sum(REST_HOURS),'',OVERTIME_ID,'Overtime','Y','" + "\r\n";
                detailSql = detailSql + userName + "',GETDATE(),'" + userName + "',GETDATE()" + "\r\n";
                detailSql = detailSql + " from JBHR_EEP.dbo.HRM_ATTEND_OVERTIME_DATA where REST_HOURS!=0 and OVERTIME_NO='" + OverTimeNO + "' group by EMPLOYEE_ID,OVERTIME_DATE,OVERTIME_ID,dbo.funReturnHRM_ATTEND_ABSENT_PLUSEND_DATE(OVERTIME_DATE,EMPLOYEE_ID) " + "\r\n";
                //detailSql = detailSql + " declare @OVERTIME_ID int " + "\r\n";
                //detailSql = detailSql + " select Cast(OVERTIME_ID as nvarchar(20)) from JBHR_EEP.dbo.HRM_ATTEND_OVERTIME_DATA where OVERTIME_NO='" + OverTimeNO + "'" + "\r\n";
                detailSql = detailSql + " insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS ";
                detailSql = detailSql + " select ABSENT_PLUS_ID,null, null,REST_HOURS,'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                detailSql = detailSql + " from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS where ABSENT_PLUS_NO in (select Cast(OVERTIME_ID as nvarchar(20)) from JBHR_EEP.dbo.HRM_ATTEND_OVERTIME_DATA where OVERTIME_NO='" + OverTimeNO + "')" + "\r\n"; 

                //detailSql = detailSql + "select @absentPlusID,null, null,sum(RestHours),'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                //detailSql = detailSql + " from HRMAttendOverTimeApplyMaster m inner join HRMAttendOverTimeApplyDetails d on m.OverTimeNO=d.OverTimeNO where m.OverTimeNO='" + OverTimeNO + "' having sum(RestHours)!=0 \r\n";

                this.ExecuteSql(detailSql, connection, transaction);
                transaction.Commit(); // �T�{���
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L

        }
      
        //�O�_���~�ҳ�
        public object[] IsForeignDept(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            //DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');
            string userid = strParam[0];
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "SELECT count(USERID) as counts FROM [EIPHRSYS].[dbo].[USERGROUPS] ug left join GROUPS g on ug.GROUPID=g.groupid left join SYS_ORGROLES orgr on orgr.ROLE_ID=ug.GROUPID left join [SYS_ORG] org on org.ORG_NO=orgr.ORG_NO where (org.ORG_NO like '107%' or GROUPNAME like '%�~��%') and userid='" + userid+"'";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                string counts = ds.Tables[0].Rows[0]["counts"].ToString();
                transaction.Commit(); // �T�{���
                string js = JsonConvert.SerializeObject(counts, Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection("EIPHRSYS", connection);
            }
            return ret; // �Ǧ^��: �L
        }

        //�W��M����w�֡B���֪��[�Z�ɼ�
        public object[] SumOvertime(object[] objParam){
            object[] ret = new object[] { 0, 0 };
            //DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');
            string EmployeeID = strParam[0];
            string aDate = strParam[1];
            string bDate = strParam[2];

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "select t1.OverTimeYYMM,t1.OverTimeYYMM+' ���֥[�Z�ɼ�'+convert(varchar,isnull(SumTotalHours1,0))+' �w�֥[�Z�ɼ�'+convert(varchar,isnull(SumTotalHours2,0)) as yymmss from(SELECT LEFT(CONVERT(NVARCHAR(10),d.OverTimeDate,120),7) as OverTimeYYMM,m.EmployeeID FROM [JBADMIN].[dbo].[HRMAttendOverTimeApplyDetails] as d left join [JBADMIN].[dbo].[HRMAttendOverTimeApplyMaster] as m on m.OverTimeNO=d.OverTimeNO group by LEFT(CONVERT(NVARCHAR(10),d.OverTimeDate,120),7),EmployeeID) as t1 full outer join(SELECT sum([TotalHours]) as SumTotalHours1,LEFT(CONVERT(NVARCHAR(10),d.OverTimeDate,120),7) as OverTimeYYMM,m.EmployeeID FROM [JBADMIN].[dbo].[HRMAttendOverTimeApplyDetails] as d left join [JBADMIN].[dbo].[HRMAttendOverTimeApplyMaster] as m on m.OverTimeNO=d.OverTimeNO where  m.flowflag in ('N','P') group by LEFT(CONVERT(NVARCHAR(10),d.OverTimeDate,120),7),EmployeeID) as t2 on t2.EmployeeID=t1.EmployeeID and t2.OverTimeYYMM=t1.OverTimeYYMM full outer join(SELECT sum([TotalHours]) as SumTotalHours2,LEFT(CONVERT(NVARCHAR(10),d.OverTimeDate,120),7) as OverTimeYYMM,m.EmployeeID FROM [JBADMIN].[dbo].[HRMAttendOverTimeApplyDetails] as d left join [JBADMIN].[dbo].[HRMAttendOverTimeApplyMaster] as m on m.OverTimeNO=d.OverTimeNO where  m.flowflag ='Z' group by LEFT(CONVERT(NVARCHAR(10),d.OverTimeDate,120),7),EmployeeID)as t3 on t3.EmployeeID=t1.EmployeeID and t3.OverTimeYYMM=t1.OverTimeYYMM where t1.EmployeeID='" + EmployeeID + "' and (t1.OverTimeYYMM = '" + aDate + "' or t1.OverTimeYYMM ='" + bDate + "') order by OverTimeYYMM";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                //string counts = ds.Tables[0].Rows[0]["counts"].ToString();
                transaction.Commit(); // �T�{���
                string js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        //���o�Y���q���~�ҦW��
        public object[] GetForeignLabors(object[] objParam){
            object[] ret = new object[] { 0, 0 };
            //DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');
            string EmployerID = strParam[0];

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
detailSql = "SELECT  distinct  er.EmployerID,e.EmployeeID,case (l.description) when '��u' then '(��u)' else '' end+e.EmployeeTcName+'/'+e.EmployeeEnName as EmployeeCEName"+"\r\n";
detailSql = detailSql + "From [192.168.1.41].FWCRM.dbo.Employee e" + "\r\n";
detailSql = detailSql + "inner join [192.168.1.41].FWCRM.dbo.EmployeeLogs l on e.EmployeeID=l.EmployeeID" + "\r\n";
detailSql = detailSql + "inner join [192.168.1.41].FWCRM.dbo.employer er on l.employerid=er.employerid" + "\r\n";
detailSql = detailSql + "where l.EffectDate=(Select Max(EffectDate) from [192.168.1.41].FWCRM.dbo.EmployeeLogs where e.EmployeeID=EmployeeID and IsActive=1)" + "\r\n";
detailSql = detailSql + "and (l.EffectTypeID=1 or l.EffectTypeID=5 ) and er.EmployerID ='" + EmployerID+"'"+"\r\n";

                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                //string counts = ds.Tables[0].Rows[0]["counts"].ToString();
                transaction.Commit(); // �T�{���
                string js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }
        //���o�Y�~�Ҫ��N�妸�ƻP��ڪ��B
        public object[] GetForeignLaborCountsDebt(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            //DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            //var OverTimeNO = drDara["OverTimeNO"].ToString();
            string[] strParam = objParam[0].ToString().Split(',');
            string EmployeeID = strParam[0];

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //string userName = SrvGL.GetUserName(userid);
                detailSql = "SELECT  distinct  h.counts1," + "\r\n";
                detailSql = detailSql + "(Select SUM(f.FeeAmount+f.tFeeAmount)-sum(IsNull(p.FeeAmount,0)) from [192.168.1.41].FWCRM.dbo.ARDetails a" + "\r\n";
                detailSql = detailSql + "inner join [192.168.1.41].FWCRM.dbo.ARMaster m on a.ARMasterID=m.ARMasterID and m.EmployeeID=e.EmployeeID" + "\r\n";
                detailSql = detailSql + "inner join [192.168.1.41].FWCRM.dbo.EmployeeFees f on a.FeeAccountID=f.FeeAccountID and f.EmployeeID=e.EmployeeID" + "\r\n";
                detailSql = detailSql + "left join [192.168.1.41].FWCRM.dbo.EmployeePays p on a.FeeAccountID=p.FeeAccountID " + "\r\n";
                detailSql = detailSql + "where m.YearMonth=(select MAX(YearMonth) from [192.168.1.41].FWCRM.dbo.ARSetUpMaster where IsDormFeeEE=1) and f.EmployeeID=e.EmployeeID) as debt" + "\r\n";
                detailSql = detailSql + "From [192.168.1.41].FWCRM.dbo.Employee e" + "\r\n";
                detailSql = detailSql + "inner join [192.168.1.41].FWCRM.dbo.EmployeeLogs l on e.EmployeeID=l.EmployeeID" + "\r\n";
                detailSql = detailSql + "inner join [192.168.1.41].FWCRM.dbo.employer er on l.employerid=er.employerid left join (" + "\r\n";
                detailSql = detailSql + "select COUNT(AutoKey) as counts1,Employer,ForeignLaborer from JBADMIN.dbo.HRMAttendOverTimeApplyHospital" + "\r\n";
                detailSql = detailSql + "group by Employer,ForeignLaborer) h on h.Employer=er.EmployerID and h.ForeignLaborer=e.EmployeeID" + "\r\n";
                detailSql = detailSql + "where l.EffectDate=(Select Max(EffectDate) from [192.168.1.41].FWCRM.dbo.EmployeeLogs where e.EmployeeID=EmployeeID and IsActive=1)" + "\r\n";
                detailSql = detailSql + "and (l.EffectTypeID=1 or l.EffectTypeID=5 ) and e.EmployeeID ='" + EmployeeID + "'" + "\r\n";

                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                //string counts = ds.Tables[0].Rows[0]["counts"].ToString();
                transaction.Commit(); // �T�{���
                string js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }
        //���o�\�O�ɧU����Ӫ�
        public object[] SelectHRMAttendOverTimeMeal(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                var detailSql = "";
                detailSql = "Select * from HRMAttendOverTimeMeal" + "\r\n";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                transaction.Commit(); // �T�{���
                string js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                ret[1] = js;
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }
        //�Ǧ^�Y���D���Y�~�Ҫ��N�����
        public object[] HospitalDrillDown(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string ForeignLaborer = parm[0];
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "";
                sql = "select h.CreateDate,left(er.EmployerName,4) as EmployerShortName,e.EmployeeTcName+'/'+e.EmployeeEnName as EmployeeCEName,u.USERNAME from JBADMIN.dbo.HRMAttendOverTimeApplyHospital h " + "\r\n";
                sql = sql + "inner join [192.168.1.41].FWCRM.dbo.Employer er on h.Employer=er.EmployerID inner join [192.168.1.41].FWCRM.dbo.Employee e on e.EmployeeID=h.ForeignLaborer " + "\r\n";
                sql = sql + "inner join EIPHRSYS.dbo.USERS u on h.CreateBy=u.USERID where h.ForeignLaborer='" + ForeignLaborer + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
        }

        
//---------------------------------------------------------------------------------------------------------------------------

        private class TheTimeRange
        {
            public DateTime Begin { get; set; }
            public DateTime End { get; set; }
        }
        
//---------------------------------------------------------------------------------------------------------------------------

        private void ucHRMAttendOverTimeApplyDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            var overtimeDate = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("OverTimeDate");
            var beginTime = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("BeginTime");
            var endTime = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("EndTime");

            decimal overtimeHours = decimal.Parse(ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("OverTimeHours").ToString());
            decimal restHours = decimal.Parse(ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("RestHours").ToString());

            string ApplyOvertimeType = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("ApplyOvertimeType").ToString();//�ӽ�����
            //�ӽ������Y�� 4 ���ɥ�P���\�O ,5 ���ɥ� => �h�ק� restHours ���
            //if (ApplyOvertimeType == "4" || ApplyOvertimeType == "5")
            //{
            //    ucHRMAttendOverTimeApplyDetails.SetFieldValue("RestHours", overtimeHours);
            //    ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeHours", 0);
            //}

            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeBegin", DateAndTimeMerger(overtimeDate, beginTime));
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeEnd", DateAndTimeMerger(overtimeDate, endTime));
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("TotalHours", overtimeHours + restHours);

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("CreateBy", LoginUser);//�����

            //�s�W EndTimeTemp , OverTimeDateTimeEndTemp �x�s���� �𮧮ɶ� �����,����Φ���Ƽg�J�H�Ƹ�Ƥ�
            var EndTimeTemp = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("EndTimeTemp");
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeEndTemp", DateAndTimeMerger(overtimeDate, EndTimeTemp));

        }

        private void ucHRMAttendOverTimeApplyMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucHRMAttendOverTimeApplyMaster.SetFieldValue("CreateBy", LoginUser);//�����
            ucHRMAttendOverTimeApplyMaster.SetFieldValue("EmployeeText", LoginUser);//�����
        }

        private void ucHRMAttendOverTimeApplyDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            var overtimeDate = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("OverTimeDate");
            var beginTime = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("BeginTime");
            var endTime = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("EndTime");

            decimal overtimeHours = decimal.Parse(ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("OverTimeHours").ToString());
            decimal restHours = decimal.Parse(ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("RestHours").ToString());

            string ApplyOvertimeType = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("ApplyOvertimeType").ToString().Trim();//�ӽ�����
            ////�ӽ������Y�� ���ɥ�P���\�O 4 �B���ɥ� 5=>�h�ק� restHours ���
            //if (ApplyOvertimeType == "4" || ApplyOvertimeType == "5")
            //{
            //    ucHRMAttendOverTimeApplyDetails.SetFieldValue("RestHours", overtimeHours);
            //    ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeHours", 0);
            //}

            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeBegin", DateAndTimeMerger(overtimeDate, beginTime));
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeEnd", DateAndTimeMerger(overtimeDate, endTime));
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("TotalHours", overtimeHours + restHours);

            //�s�W EndTimeTemp , OverTimeDateTimeEndTemp �x�s���� �𮧮ɶ� �����,����Φ���Ƽg�J�H�Ƹ�Ƥ�
            var EndTimeTemp = ucHRMAttendOverTimeApplyDetails.GetFieldCurrentValue("EndTimeTemp");
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeEndTemp", DateAndTimeMerger(overtimeDate, EndTimeTemp));

       }

        private void ucHRMAttendOverTimeApplyDetails_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {

        }


        //flow �~�ҨƷ~��,�U�ե[�Z���饼�W�L2.9�p�ɰƥD���H�Wñ�֫h�e�H���,3�p�ɦA�����`ñ��
        public object flowSendCheck(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string OverTimeNO = dr["OverTimeNO"].ToString();

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                var detailSql = "";
                detailSql = "select COUNT(*) as icount from HRMAttendOverTimeApplyDetails where OverTimeNO='" + OverTimeNO + "' and TotalHours>=3.0";
                DataSet ds = this.ExecuteSql(detailSql, connection, transaction);
                int icount = int.Parse(ds.Tables[0].Rows[0]["icount"].ToString());
                transaction.Commit(); // �T�{���

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
            return ret; // �Ǧ^��: �L
        }

    }
}
