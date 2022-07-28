using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sHRMAttendOverTimeMealQuery
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
                //string sql = "select a.IsSys,a.UserID,c.NAME_C,b.BANK_CODE,t.ACCOUNT_NO,sum(a.CheckAmt) as CheckAmt,c.IDNO from ERPDelayLunchApply a " + "\r\n";
                //sql = sql + " inner join [JBHR_EEP].dbo.HRM_BASE_BASE c on a.UserID=c.EMPLOYEE_CODE" + "\r\n";
                //sql = sql + " inner join [JBHR_EEP].dbo.HRM_EMPLOYEE_ACCOUNT t on c.EMPLOYEE_ID=t.EMPLOYEE_ID" + "\r\n";
                //sql = sql + " inner join [JBHR_EEP].dbo.HRM_BANK b on t.BANK_ID=b.BANK_ID" + "\r\n";
                //sql = sql + " where flowflag='Z' and a.YearMonth='" + YearMonth + "'" + "\r\n";
                //sql = sql + " group by c.NAME_C,b.BANK_CODE,t.ACCOUNT_NO,c.IDNO,a.IsSys,a.UserID  order by a.IsSys,a.UserID" + "\r\n";
                string sql = "select m.EmployeeID,c.NAME_C,b.BANK_CODE,t.ACCOUNT_NO,sum(d.MealTotalNTD) as CheckAmt,c.IDNO from HRMAttendOverTimeApplyDetails d " + "\r\n";
                sql = sql + " inner join HRMAttendOverTimeApplyMaster m on d.OverTimeNO=m.OverTimeNO" + "\r\n";
                sql = sql + " inner join [JBHR_EEP].dbo.HRM_BASE_BASE c on m.EmployeeID=c.EMPLOYEE_ID" + "\r\n";
                sql = sql + " inner join [JBHR_EEP].dbo.HRM_EMPLOYEE_ACCOUNT t on c.EMPLOYEE_ID=t.EMPLOYEE_ID" + "\r\n";
                sql = sql + " inner join [JBHR_EEP].dbo.HRM_BANK b on t.BANK_ID=b.BANK_ID" + "\r\n";
                sql = sql + " inner join  EIPHRSYS.dbo.SYS_TODOHIS h on h.FORM_PRESENTATION = 'OverTimeNO=' + CHAR(39) + m.OverTimeNO + CHAR(39) and h.STATUS = 'Z' and SUBSTRING(CONVERT(varchar, h.UPDATE_DATE), 1, 7) ='" + YearMonth + "'" + "\r\n";
                sql = sql + " where flowflag='Z' and d.MealTotalNTD <> ''" + "\r\n";
                sql = sql + " group by c.NAME_C,b.BANK_CODE,t.ACCOUNT_NO,c.IDNO,m.EmployeeID order by m.EmployeeID" + "\r\n";
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
