using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sERPDelayLunchApply
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
        //���o�������B,�q�\����,�а�����
        public object[] getLunchData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EMPLOYEE_CODE = parm[0];
            string beginDate = parm[1];
            string endDate = parm[2];
            string NotCheckDate = "";
            int DelayLunchID = int.Parse(parm[4].ToString());
            if (DelayLunchID == 0)//0=>�ӽ� ,!=0�ӿ���o�ӽи��
            {
                NotCheckDate = "'" + parm[3].Replace("\n", "','") + "'"; //�϶��ư����
            }
           
            var js = string.Empty;
          
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
                //-------ApplyTotal=>�����`���B
                //-------ApplyEat=>�\�O 
                //-------ApplyAbsent=>�а� : ���W���е����ɶ���1200�I �� �}�l�ɶ��ФU��1300��������ӽ�	
                   string sql = "select COUNT(*)*60 as ApplyTotal" + "\r\n";

                   if (DelayLunchID == 0)//ApplyDate�϶��ư����B
                    {
                        //�s�W��
                        sql = sql + " ,(Select COUNT(*)*60 from [JBHR_EEP].dbo.HRM_ATTEND_ATTEND h " + "\r\n";
                        sql = sql + " left join [JBHR_EEP].dbo.HRM_ATTEND_ROTE r on h.ROTE_ID = r.ROTE_ID " + "\r\n";
                        sql = sql + " where h.EMPLOYEE_ID=c.EMPLOYEE_ID and r.ROTE_ID not in (select ROTE_ID from [JBHR_EEP].dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";
                       sql = sql + " and h.ATTEND_DATE in ( " + NotCheckDate + " ) ) as ApplyDate " + "\r\n";//�϶��ư����B
                    }
                    else
                    {
                        //�ӿ�f��=>��ӽЮɪ����B
                        sql = sql + " ,(Select ApplyDate from ERPDelayLunchApply" + "\r\n";
                        sql = sql + " where DelayLunchID="+DelayLunchID+" ) as ApplyDate " + "\r\n";
                    }

                   sql = sql + ",(select count(distinct Convert(nvarchar(10),o.Adate,111))*60 from [192.168.1.41].JBePortal.dbo.EmpOrderFood o where c.EMPLOYEE_CODE=o.EmpNum " + "\r\n";
                   sql = sql + " and o.UnitPrice!=0 and o.Adate between '" + beginDate + "' and '" + endDate + "' ) as ApplyEat" + "\r\n";
                   sql = sql + ",(select count(distinct Convert(nvarchar(10),ABSENT_DATE,111))*60 from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";
                   sql = sql + " where EMPLOYEE_ID=c.EMPLOYEE_ID and ABSENT_DATE between '" + beginDate + "' and '" + endDate + "' and (END_TIME>=1200 and BEGIN_TIME<=1300) ) as ApplyAbsent" + "\r\n";
                   sql = sql + " from [JBHR_EEP].dbo.HRM_ATTEND_ATTEND a" + "\r\n";
                   sql = sql + " left join [JBHR_EEP].dbo.HRM_ATTEND_ROTE b on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                   sql = sql + " inner join  [JBHR_EEP].dbo.HRM_BASE_BASE c on a.EMPLOYEE_ID=c.EMPLOYEE_ID" + "\r\n";
                   sql = sql + " where c.EMPLOYEE_CODE='" + EMPLOYEE_CODE + "'" + "\r\n";
                   sql = sql + " and a.ATTEND_DATE between '" + beginDate + "' and '" + endDate + "'" + "\r\n";
                   sql = sql + " and a.ATTEND_DATE not in (select NotCheckDate from ERPDelayLunchDate) " + "\r\n";
                   sql = sql + " and b.ROTE_ID not in (select ROTE_ID from [JBHR_EEP].dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";
                   sql = sql + " group by c.EMPLOYEE_CODE,c.EMPLOYEE_ID " + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
        }
        //�s�W�϶��ư����	
        private void ucERPDelayLunchApply_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            int DelayLunchID = Convert.ToInt32(ucERPDelayLunchApply.GetFieldCurrentValue("DelayLunchID"));//�~�\�O�N��
            string sNotCheckDate = ucERPDelayLunchApply.GetFieldCurrentValue("NotCheckDate").ToString().Replace("\n", ","); ;//�϶��ư����
            string CreateBy = ucERPDelayLunchApply.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucERPDelayLunchApply.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");
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
                string SQL = "exec procInsertERPDelayLunchApplyDate " + DelayLunchID + ",'" + sNotCheckDate + "','" + CreateBy + "','" + CreateDate + "'";
                this.ExecuteSql(SQL, connection, transaction);
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


        //-------------�ӽЮ��ˬd---------------------------------------------------------------------------------------------------------------------
        //1.�ˬd�O�_�b�]�w���W�椺 ERPDelayLunchEmp (500�W��) , ERPDelayLunchEmpAuto (�t�Φ۰ʲ��ͦW��)
        public object[] checkLunchEmp(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string userid = parm[0];
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
                string sql = " select (select COUNT(*) from ERPDelayLunchEmp where EmpID='" + userid + "')+" + "\r\n";
                sql = sql + " (select COUNT(*) from ERPDelayLunchEmpAuto where EmpID='" + userid + "') as cnt";

                DataSet dsERPDelayLunchEmp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsERPDelayLunchEmp.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
                dsERPDelayLunchEmp.Dispose();

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

        //2.�P�_�~�\�O�ӽЪ��ɬq���O�_�w���s�b���~�\�O���
        public object[] checkLunchData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string DelayLunchID = parm[0];
            string userid = parm[1];
            string beginDate = parm[2];
            string endDate = parm[3];
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
                string sql = " select COUNT(*) AS cnt from ERPDelayLunchApply" + "\r\n";
                sql = sql + " where UserID = '" + userid + "'" + "\r\n";
                sql = sql + " and BeginDate <= '" + endDate + "'" + "\r\n";
                sql = sql + " and EndDate >= '" + beginDate + "'" + "\r\n";
                sql = sql + " and (flowflag='Z' or flowflag='N' or flowflag='P')";//Z ����,N �s�y�{�إ� ,P �y�{�L�{��
                if (DelayLunchID != "0")
                    sql = sql + " and DelayLunchID <> " + DelayLunchID;
                DataSet dsERPDelayLunchApply = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsERPDelayLunchApply.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
                dsERPDelayLunchApply.Dispose();
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
        //3.�ˬd�϶��ư���� �O�_�b �_�l��� �P ������� ����
        public object[] checkNotCheckDate(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');    
            string userid = parm[0];
            string beginDate = parm[1];
            string endDate = parm[2];
            string NotCheckDate = "'" + parm[3].Replace("\n", "','") + "'"; //�϶��ư����
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
                string sql = " select COUNT(*) as cnt" + "\r\n";
                sql = sql + " from [JBHR_EEP].dbo.HRM_ATTEND_ATTEND a" + "\r\n";
                sql = sql + " left join [JBHR_EEP].dbo.HRM_ATTEND_ROTE b on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql = sql + " inner join  [JBHR_EEP].dbo.HRM_BASE_BASE c on a.EMPLOYEE_ID=c.EMPLOYEE_ID" + "\r\n";
                sql = sql + " where c.EMPLOYEE_CODE='" + userid + "' ";
                sql = sql + " and a.ATTEND_DATE between '" + beginDate + "' and '" + endDate + "' and a.ATTEND_DATE in (" + NotCheckDate + ")";
                DataSet dsERPDelayLunchApply = this.ExecuteSql(sql, connection, transaction);
                int cnt = int.Parse(dsERPDelayLunchApply.Tables[0].Rows[0]["cnt"].ToString());

                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
                transaction.Commit();
                dsERPDelayLunchApply.Dispose();
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
        //3.1.�����϶��ư���� �P �q�\���� �� �а����� �O�_����
        public object[] checkDateduplicate(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string userid = parm[0];
            string beginDate = parm[1];
            string endDate = parm[2];
            string NotCheckDate = "'" + parm[3].Replace("\n", "','") + "'"; //�϶��ư����
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
                string sql = " DECLARE @Dates nvarchar(Max)" + "\r\n";
                sql = sql + " DECLARE @Date2s nvarchar(Max)" + "\r\n";
                sql = sql + " Set @Dates=''" + "\r\n";
                sql = sql + " Set @Date2s=''" + "\r\n";
                sql = sql + " select @Dates=Convert(nvarchar(10),o.Adate,111) + ',' + @Dates" + "\r\n";
                sql = sql + " from [192.168.1.41].JBePortal.dbo.EmpOrderFood o where o.EmpNum='" + userid + "'" + "\r\n";
                sql = sql + " and o.UnitPrice!=0 and o.Adate between '" + beginDate + "' and '" + endDate + "' and o.Adate in (" + NotCheckDate + ")";
                sql = sql + " group by o.Adate" + "\r\n";

                sql = sql + " select @Date2s=Convert(nvarchar(10),ABSENT_DATE,111) + ',' + @Date2s" + "\r\n";
                sql = sql + " from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL where EMPLOYEE_ID='" + userid + "'" + "\r\n";
                sql = sql + " and ABSENT_DATE between '" + beginDate + "' and '" + endDate + "' and (END_TIME>=1200 and BEGIN_TIME<=1300) and ABSENT_DATE in (" + NotCheckDate + ")";
                sql = sql + " group by ABSENT_DATE" + "\r\n";

                sql = sql + " select @Dates as Dates,@Date2s as Date2s";
                DataSet dsDate = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(dsDate.Tables[0], Formatting.Indented);//Tables

                transaction.Commit();
                dsDate.Dispose();
                
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

        //-----�f���ˬd----------------------------------------------------------------------------------------------------------------------     
        //1.�ˬdñ�֪��~��O�_���T=>�즹��ñ�֪� iType => 1 ���~�� 2 �W�Ӧ~��  �O�_�w����   
        public object[] checkLunchApplyClose(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
            string iType = parm[1];//1 ���~�� 2 �W�Ӧ~��  �O�_�w���� 
            int iMonth=0;
            if (iType == "2") iMonth = -1;
                
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
                string sql = " select Isnull((select top 1 IsClose from ERPDelayLunchApply where YearMonth=Left(Convert(nvarchar(10)," + "\r\n";
                sql = sql + " DateAdd(MONTH,"+iMonth+",Cast(LEFT('"+@YearMonth+"',4)+'/'+RIGHT('"+@YearMonth+"',2)+'/01' as datetime)),112),6)),1) as IsClose" + "\r\n";               
                DataSet dsHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction);
                string IsClose = dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["IsClose"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(IsClose, Formatting.Indented);
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
       


       
    }
}
