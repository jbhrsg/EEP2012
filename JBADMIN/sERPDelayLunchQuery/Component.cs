using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sERPDelayLunchQuery
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
        //抓取未結案的最小請領年月 
        public object[] GetLunchApplyYM(object[] objParam)
        {
            string js = string.Empty;

            //建立資料庫連結
            System.Data.IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select MIN(YearMonth) as YearMonth from ERPDelayLunchApply where IsClose=0" + "\r\n";
                DataSet dsHRM_ATTEND_OVERTIME_DATA = this.ExecuteSql(sql, connection, transaction);
                string YearMonth = dsHRM_ATTEND_OVERTIME_DATA.Tables[0].Rows[0]["YearMonth"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(YearMonth, Formatting.Indented);
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
        //檢查請領年月是否已完成結帳 
        public object[] checkLunchYMClose(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];

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
                string sql = " select Isnull((select top 1 IsClose from ERPDelayLunchApply where YearMonth='" + YearMonth + "' ),0) as IsClose" + "\r\n";
                DataSet dsDelayLunch = this.ExecuteSql(sql, connection, transaction);
                string IsClose = dsDelayLunch.Tables[0].Rows[0]["IsClose"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
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
        //檢查設定500名單內的人又去訂便當  
        public object[] checkLunchEmpOrder(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];


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
                //求得請領年月的月初、月底
                int iYear = int.Parse(YearMonth.Substring(0, 4));//Convert.ToDateTime(beginDate).Date.Year;
                int iMonth = int.Parse(YearMonth.Substring(4, 2));
                DateTime BeginDate = new DateTime(iYear, iMonth, 1);
                DateTime EndDate = BeginDate.AddMonths(1).AddDays(-1);
                string sBeginDate = BeginDate.ToShortDateString();
                string sEndDate = EndDate.ToShortDateString();

                string sql = " declare @NameC nvarchar(50)" + "\r\n";
                sql = sql + " Set @NameC=''" + "\r\n";
                sql = sql + " select @NameC=b.Name + ',' +@NameC" + "\r\n";
                sql = sql + " from [192.168.1.41].JBePortal.dbo.EmpOrderFood o " + "\r\n";
                sql = sql + " inner join ERPDelayLunchEmp e on o.EmpNum=e.EmpID" + "\r\n";
                sql = sql + " inner join [192.168.1.41].JBePortal.dbo.EmpBase b on o.EmpNum=b.EmpNum" + "\r\n";
                sql = sql + " where o.UnitPrice!=0 and o.Adate between '" + sBeginDate + "' and '" + sEndDate + "'" + "\r\n";
                sql = sql + " group by b.Name" + "\r\n";
                sql = sql + " select @NameC as sNameC" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string sNameC = ds.Tables[0].Rows[0]["sNameC"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(sNameC, Formatting.Indented);
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

        //產生設定誤餐資料
        public object[] InsertDelayLunch(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
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
                //求得請領年月的月初、月底
                int iYear = int.Parse(YearMonth.Substring(0, 4));//Convert.ToDateTime(beginDate).Date.Year;
                int iMonth = int.Parse(YearMonth.Substring(4, 2));                
                DateTime BeginDate = new DateTime(iYear, iMonth, 1);
                DateTime EndDate = BeginDate.AddMonths(1).AddDays(-1);
                string sBeginDate = BeginDate.ToShortDateString();
                string sEndDate = EndDate.ToShortDateString();

                //取得ERPDelayLunchApply目前編號
                string sql = "Select CURRNUM from SYSAUTONUM where (AUTOID = 'DelayLunchID')" + "\r\n";
                int DelayLunchID = int.Parse(this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0]["CURRNUM"].ToString());

                //取得ERPDelayLunchEmp (500名單), ERPDelayLunchEmpAuto (系統產生名單) 數量=> update SYSAUTONUM 編號
                string sql2 = "select (Select COUNT(*) from ERPDelayLunchEmp) as EmpCount,(Select COUNT(*) from ERPDelayLunchEmpAuto) as EmpAutoCount" + "\r\n";
                DataSet ds = this.ExecuteSql(sql2, connection, transaction);
                int EmpCount = int.Parse(ds.Tables[0].Rows[0]["EmpCount"].ToString());
                int EmpAutoCount = int.Parse(ds.Tables[0].Rows[0]["EmpAutoCount"].ToString());

                int iT = DelayLunchID + EmpCount + EmpAutoCount+1;
                //update SYSAUTONUM 編號
                string sql3 = "update SYSAUTONUM set CURRNUM =" + iT + " where (AUTOID = 'DelayLunchID')" + "\r\n";
                this.ExecuteSql(sql3, connection, transaction);

                //ERPDelayLunchApply目前編號+ 500名單
                int iAuto = DelayLunchID + EmpCount;
               
                //先刪除再增(系統產生部分)
                string sqlDel = "delete from ERPDelayLunchApply where IsSys=1 and IsClose=0 and YearMonth='" + YearMonth + "'";
                this.ExecuteSql(sqlDel, connection, transaction);

                //Insert ERPDelayLunchApply from ERPDelayLunchEmp (500名單)
                string sql4 = "insert into ERPDelayLunchApply(DelayLunchID,UserID,BeginDate,EndDate,YearMonth," + "\r\n";
                sql4 = sql4 + "ApplyTotal,ApplyEat,ApplyAbsent,ApplyAmt," + "\r\n";
                sql4 = sql4 + "CheckTotal,CheckEat,CheckAbsent,CheckOther,CheckAmt,CheckOtherMemo," + "\r\n";
                sql4 = sql4 + "IsClose,IsSys,flowflag,CreateBy,CreateDate) " + "\r\n";
                sql4 = sql4 + "select ROW_NUMBER() OVER (ORDER BY EmpID)+" + DelayLunchID + ",c.EMPLOYEE_CODE,'" + sBeginDate + "','" + sEndDate + "','" + YearMonth + "'," + "\r\n";
                sql4 = sql4 + "0,0,0,0," + "\r\n";
                sql4 = sql4 + "0,0,0,0,500,''," + "\r\n";
                sql4 = sql4 + "0,1,'Z','" + userName + "',getDate()" + "\r\n";//flowflag=>Z
                sql4 = sql4 + " from ERPDelayLunchEmp e" + "\r\n";
                sql4 = sql4 + " inner join [JBHR_EEP].dbo.HRM_BASE_BASE c on e.EmpID=c.EMPLOYEE_CODE" + "\r\n";
                sql4 = sql4 + " inner join [JBHR_EEP].dbo.HRM_EMPLOYEE_ACCOUNT t on e.EmpID=t.EMPLOYEE_ID" + "\r\n";
                sql4 = sql4 + " inner join [JBHR_EEP].dbo.HRM_ATTEND_ATTEND a on c.EMPLOYEE_ID=a.EMPLOYEE_ID" + "\r\n";
                sql4 = sql4 + " left join [JBHR_EEP].dbo.HRM_ATTEND_ROTE b on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql4 = sql4 + " where  a.ATTEND_DATE between '" + sBeginDate + "' and '" + sEndDate + "'" + "\r\n";

                //sql4 = sql4 + " and b.ROTE_CODE <> '00' " + "\r\n";
                sql4 = sql4 + " and b.ROTE_ID not in (select ROTE_ID from [JBHR_EEP].dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";

                sql4 = sql4 + " group by e.EmpID,c.EMPLOYEE_CODE" + "\r\n";

                //Insert ERPDelayLunchApply from ERPDelayLunchEmpAuto (系統產生名單)
                sql4 = sql4 + " insert into ERPDelayLunchApply(DelayLunchID,UserID,BeginDate,EndDate,YearMonth," + "\r\n";
                sql4 = sql4 + "ApplyTotal,ApplyEat,ApplyAbsent,ApplyAmt," + "\r\n";
                sql4 = sql4 + "CheckTotal,CheckEat,CheckAbsent,CheckOther,CheckAmt,CheckOtherMemo," + "\r\n";
                sql4 = sql4 + "IsClose,IsSys,flowflag,CreateBy,CreateDate) " + "\r\n";
                sql4 = sql4 + "select ROW_NUMBER() OVER (ORDER BY EmpID)+" + iAuto + ",c.EMPLOYEE_CODE,'" + sBeginDate + "','" + sEndDate + "','" + YearMonth + "'," + "\r\n";
                sql4 = sql4 + "0,0,0,0," + "\r\n";
                sql4 = sql4 + "COUNT(a.ATTEND_DATE)*60," + "\r\n";//CheckTotal期間金額
                sql4 = sql4 + "(select count(distinct Convert(nvarchar(10),o.Adate,111))*60 from [192.168.1.41].JBePortal.dbo.EmpOrderFood o " + "\r\n";//CheckEat訂餐扣款	
                sql4 = sql4 + " where c.EMPLOYEE_CODE=o.EmpNum and o.UnitPrice!=0 and o.Adate between '" + sBeginDate + "' and '" + sEndDate + "' and o.Adate between e.SDate and e.EDate)," + "\r\n";
                sql4 = sql4 + "(select count(distinct Convert(nvarchar(10),ABSENT_DATE,111))*60 from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL " + "\r\n";//CheckAbsent 請假扣款
                sql4 = sql4 + " where EMPLOYEE_ID=c.EMPLOYEE_ID and ABSENT_DATE between '" + sBeginDate + "' and '" + sEndDate + "' and ABSENT_DATE between e.SDate and e.EDate and (END_TIME>=1200 and BEGIN_TIME<=1300)), " + "\r\n";
                sql4 = sql4 + "0,0,''," + "\r\n";//CheckAmt先預設為0
                sql4 = sql4 + "0,1,'Z','" + userName + "',getDate()" + "\r\n";//flowflag=>Z
                sql4 = sql4 + " from ERPDelayLunchEmpAuto e" + "\r\n";
                sql4 = sql4 + " inner join [JBHR_EEP].dbo.HRM_BASE_BASE c on e.EmpID=c.EMPLOYEE_CODE" + "\r\n";
                sql4 = sql4 + " left join [JBHR_EEP].dbo.HRM_EMPLOYEE_ACCOUNT t on c.EMPLOYEE_ID=t.EMPLOYEE_ID" + "\r\n";
                sql4 = sql4 + " inner join [JBHR_EEP].dbo.HRM_ATTEND_ATTEND a on c.EMPLOYEE_ID=a.EMPLOYEE_ID" + "\r\n";
                sql4 = sql4 + " left join [JBHR_EEP].dbo.HRM_ATTEND_ROTE b on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                sql4 = sql4 + " where  a.ATTEND_DATE between '" + sBeginDate + "' and '" + sEndDate + "' and a.ATTEND_DATE between e.SDate and e.EDate" + "\r\n";
                sql4 = sql4 + " and a.ATTEND_DATE not in (select NotCheckDate from ERPDelayLunchDate) " + "\r\n";
                sql4 = sql4 + " and b.ROTE_ID not in (select ROTE_ID from [JBHR_EEP].dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";

                sql4 = sql4 + " group by e.EmpID,c.EMPLOYEE_CODE,c.EMPLOYEE_ID,e.SDate,e.EDate" + "\r\n";

                this.ExecuteSql(sql4, connection, transaction);

                string sql5 = " update ERPDelayLunchApply set CheckAmt = CheckTotal-CheckEat-CheckAbsent where IsClose=0 and IsSys=1 and CheckAmt=0 and YearMonth='" + YearMonth + "'" + "\r\n";
                this.ExecuteSql(sql5, connection, transaction);


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

        //結案誤餐資料
        public object[] UpdateDelayLunch(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
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
                //update SYSAUTONUM 編號
                string sql = "update ERPDelayLunchApply set IsClose =1 where IsClose=0 and YearMonth='" + YearMonth + "'" + "\r\n";
                this.ExecuteSql(sql, connection, transaction);
               
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

       

        //誤餐日期紀錄
        public object[] DelayDateList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
            string UserID = parm[1];
            string iType = parm[2];//1 期間總金額 , 2 訂餐總扣款 , 3請假總扣款 , 4區間排除總金額
            Boolean IsSys = Boolean.Parse(parm[3]);//0 非系統產生 => 關聯Table ERPDelayLunchApply
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
                //求得請領年月的月初、月底
                int iYear = int.Parse(YearMonth.Substring(0, 4));//Convert.ToDateTime(beginDate).Date.Year;
                int iMonth = int.Parse(YearMonth.Substring(4, 2));
                DateTime BeginDate = new DateTime(iYear, iMonth, 1);
                DateTime EndDate = BeginDate.AddMonths(1).AddDays(-1);
                string sBeginDate = BeginDate.ToShortDateString();
                string sEndDate = EndDate.ToShortDateString();

                string sql = "";
                //1 期間總金額 , 2 訂餐總扣款 , 3請假總扣款
                if (iType == "1")
                {
                    sql = " select distinct Convert(nvarchar(10),a.ATTEND_DATE,111) as Adate,DATENAME(DW,a.ATTEND_DATE) as Dateweek" + "\r\n";
                    sql = sql + " from [JBHR_EEP].dbo.HRM_ATTEND_ATTEND a" + "\r\n";
                    sql = sql + " left join [JBHR_EEP].dbo.HRM_ATTEND_ROTE b on A.ROTE_ID = B.ROTE_ID" + "\r\n";
                    sql = sql + " inner join  [JBHR_EEP].dbo.HRM_BASE_BASE c on a.EMPLOYEE_ID=c.EMPLOYEE_ID" + "\r\n";
                    //0 非系統產生 => 關聯Table ERPDelayLunchApply
                    if (IsSys == false)
                    {
                        sql = sql + " inner join  ERPDelayLunchApply p on c.EMPLOYEE_CODE=p.UserID and a.ATTEND_DATE between p.BeginDate and p.EndDate" + "\r\n";
                    }
                    else
                    {
                        sql = sql + " join ERPDelayLunchEmpAuto e on a.EMPLOYEE_ID=e.EmpID" + "\r\n";
                    }
                    sql = sql + " where c.EMPLOYEE_CODE='" + UserID + "' and a.ATTEND_DATE between '" + sBeginDate + "' and '" + sEndDate + "'" + "\r\n";
                    if (IsSys == true)//系統產生
                    {
                        sql = sql + " and a.ATTEND_DATE between e.SDate and e.EDate" + "\r\n";

                    }
                    sql = sql + " and a.ATTEND_DATE not in (select NotCheckDate from ERPDelayLunchDate) " + "\r\n";
                    sql = sql + " and b.ROTE_ID not in (select ROTE_ID from [JBHR_EEP].dbo.HRM_ATTEND_ROTEMAPPING_DETAIL where ROTEMAPPING_CODE = 'OffDay' or ROTEMAPPING_CODE = 'Holidays' or ROTEMAPPING_CODE = 'NationalHoliday') " + "\r\n";

                }
                else if (iType == "2")
                {
                    sql = " select distinct Convert(nvarchar(10),o.Adate,111) as Adate,DATENAME(DW,o.Adate) as Dateweek from [192.168.1.41].JBePortal.dbo.EmpOrderFood o" + "\r\n";
                    //0 非系統產生 => 關聯Table ERPDelayLunchApply
                    if (IsSys == false)
                    {
                        sql = sql + " inner join  ERPDelayLunchApply p on o.EmpNum=p.UserID and o.Adate between p.BeginDate and p.EndDate" + "\r\n";
                    }
                    sql = sql + " where o.EmpNum ='" + UserID + "' and o.Adate between '" + sBeginDate + "' and '" + sEndDate + "' and o.UnitPrice!=0 " + "\r\n";
                }
                else if (iType == "3")
                {
                    sql = " select distinct Convert(nvarchar(10),ABSENT_DATE,111) as Adate,DATENAME(DW,ABSENT_DATE) as Dateweek from  JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL  d" + "\r\n";
                    sql = sql + " inner join JBHR_EEP.dbo.HRM_BASE_BASE b on d.EMPLOYEE_ID=b.EMPLOYEE_ID" + "\r\n";
                    //0 非系統產生 => 關聯Table ERPDelayLunchApply
                    if (IsSys == false)
                    {
                        sql = sql + " inner join  ERPDelayLunchApply p on b.EMPLOYEE_CODE=p.UserID and ABSENT_DATE between p.BeginDate and p.EndDate" + "\r\n";
                    }
                    sql = sql + " where ( b.EMPLOYEE_CODE = '" + UserID + "' and ABSENT_DATE between '" + sBeginDate + "' and '" + sEndDate + "') and" + "\r\n";
                    sql = sql + " (END_TIME>=1200 and BEGIN_TIME<=1300) " + "\r\n";
                }
                else if (iType == "4")
                {
                    if (IsSys == false)//0 非系統產生 => 關聯Table ERPDelayLunchApplyDate
                    {
                        sql = " select distinct Convert(nvarchar(10),d.NotCheckDate,111) as Adate,DATENAME(DW,d.NotCheckDate) as Dateweek from  ERPDelayLunchApplyDate d" + "\r\n";
                        sql = sql + " inner join ERPDelayLunchApply a on d.DelayLunchID=a.DelayLunchID" + "\r\n";
                        sql = sql + " where a.UserID = '" + UserID + "' and a.YearMonth = '" + YearMonth + "'" + "\r\n";                     
                    }
                    else sql = " select '' as Adate,'' as Dateweek" + "\r\n";
                }
               
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

        //產生誤餐匯款格式
        public object[] ReportDelayLunch(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];
            string ExportDate = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
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
                string sql = "select a.IsSys,a.UserID,c.NAME_C,b.BANK_CODE,t.ACCOUNT_NO,sum(a.CheckAmt) as CheckAmt,c.IDNO from ERPDelayLunchApply a " + "\r\n";
                sql = sql + " inner join [JBHR_EEP].dbo.HRM_BASE_BASE c on a.UserID=c.EMPLOYEE_CODE" + "\r\n";
                sql = sql + " inner join [JBHR_EEP].dbo.HRM_EMPLOYEE_ACCOUNT t on c.EMPLOYEE_ID=t.EMPLOYEE_ID" + "\r\n";
                sql = sql + " inner join [JBHR_EEP].dbo.HRM_BANK b on t.BANK_ID=b.BANK_ID" + "\r\n";
                sql = sql + " where flowflag='Z' and a.YearMonth='"+YearMonth+"'" + "\r\n";
                sql = sql + " group by c.NAME_C,b.BANK_CODE,t.ACCOUNT_NO,c.IDNO,a.IsSys,a.UserID  having sum(a.CheckAmt)!=0" + "\r\n";
                sql = sql + " order by a.IsSys,a.UserID" + "\r\n";
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
        
        //期間訂餐金額明細表
        public object[] JBePortalEmpOrderList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string BeginDate = parm[0];
            string EndDate = parm[1];
            string EmpNum = parm[2];//1 期間總金額 , 2 訂餐總扣款 , 3請假總扣款 , 4區間排除總金額
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
                string sql = " select EmpNum,NAME_C,adate,sum(Price)  as Price from View_JBePortalEmpOrder where EmpNum='" + EmpNum + "'" + "\r\n";
                sql = sql + " and adate between '" + BeginDate + "' and '" + EndDate + "' group by EmpNum,NAME_C,adate" + "\r\n";
               
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




    }
}
