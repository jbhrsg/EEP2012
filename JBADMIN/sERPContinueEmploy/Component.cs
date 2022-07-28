using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

using System.Data;
using Newtonsoft.Json;

namespace sERPContinueEmploy
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
        public string GetContinueEmployNOPrefix(){
            DateTime today1 = DateTime.Today;
            return "INF" + ((today1.Year).ToString()).Trim();
        }

        //產生報表資訊
        public object[] ReportOrders(object[] objParam)
        {
            string parm = objParam[0].ToString();
            string IsRecontract = objParam[1].ToString();
            string ContinueEmployNO = parm;
            //string IsRecontract;
            //if (parm1 == "1") { IsRecontract = "是"; } else if (parm1 == "0") { IsRecontract = "否"; } else { IsRecontract = ""; }
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
                //string sql = "select m.OrderNo,m.OrderType,m.WorkNo,m.EmployerID,r.EmployerName,m.FromOrderNo, " + "\r\n";
                //sql = sql + "m.NationalityID,dbo.funReturnReferenceData('nationality',m.NationalityID) as NationalityText, " + "\r\n";
                //sql = sql + "d.Item,d.PlanIndate,d.PersonQtyOriginal,d.sgn_type,d.sgn_no,d.org_okno,d.Notes, " + "\r\n";
                //sql = sql + "d.Gender,case d.Gender when 1 then '女' else '男' end as GenderText,WorkTime,WorkTimeReason " + "\r\n";
                //sql = sql + "from FWCRMOrders m  " + "\r\n";
                //sql = sql + "inner join FWCRMOrdersDetails d on m.OrderNo=d.OrderNo " + "\r\n";
                //sql = sql + "inner join [60.250.52.107,3225].FWCRM.dbo.Employer r on m.EmployerID=r.EmployerID " + "\r\n";
                //sql = sql + " where m.OrderNo='" + OrderNo + "'" + "\r\n";

//string sql = "SELECT m.ContinueEmployNO,c.title,CreateBy,CreateDate,d.AutoKey,l.lab_cname+''+l.lab_name as labname," + "\r\n";
//sql = sql + "Gender,Country,ImmigrationDate,DueDate,CEConfirmNO" + "\r\n";
//sql = sql + "FROM dbo.[ERPContinueEmployMaster] m" + "\r\n";
//sql = sql + "inner join dbo.[ERPContinueEmployDetail]d on m.ContinueEmployNO = d.ContinueEmployNO" + "\r\n";
//sql = sql + "inner join [192.168.1.40,1433].lab.dbo.cus c on m.Employer = c.[cus_no]" + "\r\n";
//sql = sql + "inner join [192.168.1.40,1433].lab.dbo.lab l on d.LaborName = l.[lab_no]" + "\r\n";

                //string sql = "SELECT m.ContinueEmployNO,Employer,CreateBy,CreateDate,d.AutoKey,LaborName,Gender,Country,ImmigrationDate,DueDate,CEConfirmNO,d.IsRecontract,d.Transfer,d.ReturnHome,d.BackPot,d.LetterClass,d.LetterNO,d.TransferAg FROM dbo.[ERPContinueEmployMaster] m inner join dbo.[ERPContinueEmployDetail]d on m.ContinueEmployNO = d.ContinueEmployNO" + "\r\n";
                string sql = "SELECT m.ContinueEmployNO,d.Employer,m.CreateBy,m.CreateDate,d.AutoKey,LaborName,Gender,Country,ImmigrationDate,DueDate,CEConfirmNO,d.IsRecontract,d.Transfer,d.ReturnHome,d.BackPot,d.LetterClass,d.LetterNO,d.TransferAg,u.USERNAME as SalesName,(SELECT u.USERNAME FROM [EIPHRSYS].[dbo].[USERGROUPS] ug left join [EIPHRSYS].[dbo].[USERS] u on ug.USERID=u.USERID where GROUPID='1071042') as CustomerManager,(SELECT u.USERNAME FROM [EIPHRSYS].[dbo].[USERGROUPS] ug left join [EIPHRSYS].[dbo].[USERS] u on ug.USERID=u.USERID where GROUPID='1070041') as SalesManager FROM dbo.[ERPContinueEmployMaster] m inner join dbo.[ERPContinueEmployDetail]d on m.ContinueEmployNO = d.ContinueEmployNO left join [EIPHRSYS].[dbo].[USERS] u on u.USERID= m.SalesID" + "\r\n";
                sql = sql + " where m.ContinueEmployNO='" + ContinueEmployNO + "' and d.IsRecontract=" + IsRecontract + " order by d.AutoKey asc" + "\r\n";
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

        public object[] XFlowFlag(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string ContinueEmployNO = parm[0];
            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "update [ERPContinueEmployMaster] set FlowFlag='X' where ContinueEmployNO='" + ContinueEmployNO + "'";
                this.ExecuteSql(sql, connection, transaction);

                string sql1 = "select * from [ERPContinueEmployMaster] where ContinueEmployNO='" + ContinueEmployNO + "'";
                DataSet ds=this.ExecuteSql(sql1, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ret[1] = js;
                }
                else
                {
                    ret[1] = true;
                }
                transaction.Commit();
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
            return ret;
        }
        //刪除勞工
        public object[] DeleteDetail(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split('*');
            //string CEConfirmNO = objParam[0].ToString();
            string CEConfirmNO = parm[0];
            string ContinueEmployNO = parm[1];
            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //string sql = "delete from [ERPContinueEmployDetail] where CEConfirmNO in (" + CEConfirmNO + ") and ";
                string sql = "update ERPContinueEmployDetail set DeleteFlag =1 where CEConfirmNO in (" + CEConfirmNO + ") and ContinueEmployNO ='"+ContinueEmployNO+"'";
                this.ExecuteSql(sql, connection, transaction);

                string sql1 = "select * from [ERPContinueEmployDetail] where CEConfirmNO in (" + CEConfirmNO + ") and ContinueEmployNO ='" + ContinueEmployNO + "' and  DeleteFlag =1";
                DataSet ds=this.ExecuteSql(sql1, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ret[1] = js;
                }
                else
                {
                    ret[1] = true;
                }
                transaction.Commit();
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
            return ret;
        }
    }
}
