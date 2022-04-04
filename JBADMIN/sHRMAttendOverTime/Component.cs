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
                string sql = "Select DEPT_ID,dbo.funReturnDeptInfo(DEPTC_ID,2) AS DEPT_CNAME,"+
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
            decimal hours, minutes, totalHours;

            TimeSpan ts;
            string roteCode = "";
            List<TheTimeRange> Reduce = new List<TheTimeRange>();
            TheTimeRange aOverTime = new TheTimeRange();

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
                string sql = "select A.ROTE_ID," + "\r\n";
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

                    if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
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

                    //�p�⥿�`�W�Z�ɶ�
                    if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
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
                    }
                    if (dsHRM_ATTEND_ATTEND.Tables[0].Rows[0]["ROTE_CODE"].ToString() != "00")
                    {
                        //���ȥ𮧮ɶ�
                        string restSql = "select * from HRM_ATTEND_ROTE_REST" + "\r\n";
                        restSql = restSql + "where ROTE_ID = " + roteID;
                        DataTable dtRest = this.ExecuteSql(restSql, connection, transaction).Tables[0];
                        foreach (DataRow Row1 in dtRest.Rows)
                        {
                            restBeginTime = DateAndTimeMerger(overtimeDate, Row1["REST_BEGIN_TIME"].ToString());//1200
                            restEndTime = DateAndTimeMerger(overtimeDate, Row1["REST_END_TIME"].ToString());//1300
                            ts = restEndTime - restBeginTime;//1                       
                        }
                    }
                                    
                    //�p��[�Z�ɶ�
                    Reduce = RangeCheck(Reduce);
                    var ReduceMinute = MinuteOfRange(Reduce, aOverTime);
                    var TotalMinute = (int)new TimeSpan(aOverTime.End.Ticks).Subtract(new TimeSpan(aOverTime.Begin.Ticks)).Duration().TotalMinutes;

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
                detailSql = detailSql + " select m.EmployeeID,d.OverTimeDate,d.BeginTime,d.OverTimeDateTimeBegin,d.EndTime,d.OverTimeDateTimeEnd,'9999/12/31'," + "\r\n";
                detailSql = detailSql + "d.TotalHours,d.OverTimeHours,d.RestHours,d.OverTimeCauseID,m.OverTimeRoteID,m.OverTimeDeptID,d.OverTimeNO,[dbo].funReturnOVERTIMESALARY_YYMM(1,d.OverTimeDate)," + "\r\n";
                detailSql = detailSql + "'" + userName + "',GETDATE(),'" + userName + "',GETDATE()" + "\r\n";
                detailSql = detailSql + " from HRMAttendOverTimeApplyMaster m inner join HRMAttendOverTimeApplyDetails d on m.OverTimeNO=d.OverTimeNO where m.OverTimeNO='" + OverTimeNO + "'\r\n";
                detailSql = detailSql + " insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS " + "\r\n";
                detailSql = detailSql + " select employeeID,max(OverTimeDate),dateadd(yy,datediff(yy,0,max(OverTimeDate))+1,-1),27,sum(RestHours),null,0,sum(RestHours),'',m.OverTimeNO,'Overtime','Y','" + "\r\n";
                detailSql = detailSql + userName + "',GETDATE(),'" + userName + "',GETDATE()" + "\r\n";
                detailSql = detailSql + " from HRMAttendOverTimeApplyMaster m inner join HRMAttendOverTimeApplyDetails d on m.OverTimeNO=d.OverTimeNO where RestHours!=0 and m.OverTimeNO='" + OverTimeNO + "' group by employeeID,m.OverTimeNO "+"\r\n";
                detailSql = detailSql + "declare @absentPlusID int " + "\r\n";
                detailSql = detailSql + "set @absentPlusID = SCOPE_IDENTITY()" + "\r\n";
                detailSql = detailSql + "insert into JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS ";
                detailSql = detailSql + "select @absentPlusID,null, null,sum(RestHours),'" + userName + "',getdate()" + ",'" + userName + "',getdate() " + "\r\n";
                detailSql = detailSql + " from HRMAttendOverTimeApplyMaster m inner join HRMAttendOverTimeApplyDetails d on m.OverTimeNO=d.OverTimeNO where RestHours!=0 and m.OverTimeNO='" + OverTimeNO + "'\r\n";

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
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeBegin", DateAndTimeMerger(overtimeDate, beginTime));
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("OverTimeDateTimeEnd", DateAndTimeMerger(overtimeDate, endTime));
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("TotalHours", overtimeHours + restHours);

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucHRMAttendOverTimeApplyDetails.SetFieldValue("CreateBy", LoginUser);//�����
        }

        private void ucHRMAttendOverTimeApplyMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucHRMAttendOverTimeApplyMaster.SetFieldValue("CreateBy", LoginUser);//�����
            ucHRMAttendOverTimeApplyMaster.SetFieldValue("EmployeeText", LoginUser);//�����
        }

    }
}
