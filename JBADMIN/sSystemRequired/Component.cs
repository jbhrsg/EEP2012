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

namespace sSystemRequired
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

        //簽核編號
        public string NoFixed()
        {
            return string.Format("SR{0:yyyy}", DateTime.Now.Date);
        }

        //取得需求部門主管
        public object[] GetOrgMang(object[] objParam)
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
                string _ORGNO = parm[0];
                string sql = "SELECT ORG_MAN,UPPER_ORG FROM EIPHRSYS.dbo.sys_org ";
                sql = sql + " WHERE (Upper_Org='10000' OR Upper_Org='13000'  OR  ORG_NO='10000' OR ORG_NO='99999')";
                sql = sql + " AND ORG_NO='" + _ORGNO + "'";
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
            return new object[] { 0, js }; // 傳回值: 無
        }

        //取得需求部門主管
        public object[] GetOrgMangID(object[] objParam)
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
                string _ORGNO = parm[0];
                string sql = "SELECT *  FROM View_UsersGROUPS  WHERE  GROUPID IN  (SELECT ORG_MAN FROM View_SYSORG ";
                sql = sql + " WHERE ORG_NO='" + _ORGNO + "' UNION ALL";
                sql = sql + " SELECT A.ORG_MAN FROM View_SYSORG A INNER JOIN  View_SYSORG B ON A.ORG_NO=B.UPPER_ORG";
                sql = sql + " WHERE A.UPPER_ORG='" + _ORGNO + "' GROUP BY A.ORG_MAN )";
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
            return new object[] { 0, js }; // 傳回值: 無
        }

        //總經理專案簽核
        public object[] PresidentSign(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow rowparm = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            var _SysRequiredNo = rowparm["SysRequiredNo"].ToString(); //聯絡單編號            
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
                string sql = "SELECT RequiredOrgParent,RequiredType FROM SystemRequired WHERE SysRequiredNo='" + _SysRequiredNo + "' ";
                sql = sql + " AND RequiredOrgParent='10000' AND RequiredType='A'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                var _cnt = ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
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

        private void ucSystemRequiredBeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            var dt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
            ucSystemRequired.SetFieldValue("CreateDate", dt);
        }

        //取得階層職稱
        public object[] GetCheckId(object[] objParam)
        {
            string js = string.Empty;
            object[] ret = new object[] { 0, 0 };               
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
                string _SysRequiredNo = parm[0];
                List<string> _UserID = new List<string>();
                string _UserIDs = "";
                string sql = "WITH SYS_ORG_CTE AS (SELECT ORG_NO,UPPER_ORG,ORG_MAN";
                sql = sql + " FROM View_SYSORG WHERE UPPER_ORG='" + _SysRequiredNo + "' UNION ALL";
                sql = sql + " SELECT A.ORG_NO, A.UPPER_ORG, A.ORG_MAN FROM View_SYSORG A";
                sql = sql + " INNER JOIN SYS_ORG_CTE B ON A.UPPER_ORG = B.ORG_NO) ";
                sql = sql + " SELECT B.USERID,B.USERNAME FROM View_SysOrgUsers B WHERE EXISTS (SELECT A.ORG_NO FROM SYS_ORG_CTE A WHERE A.ORG_NO=B.ORG_NO)";
                sql = sql + " OR B.ORG_NO='" + _SysRequiredNo + "' GROUP BY B.USERID,B.USERNAME";               
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        _UserID.Add("'" + ds.Tables[0].Rows[i]["USERID"].ToString() + "'");
                    }
                    _UserIDs = String.Join(",", _UserID);
                    ret[1] = _UserIDs;
                }
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

        //表單查詢
        public object[] GetSystemRequired(object[] objParam)
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
                string DateB = parm[0];
                string DateE = parm[1];
                string _UserId = parm[2];
                string ApplyOrgNO = parm[3];
                string RequiredType = parm[4];
                string Flowtype = parm[5];
                string sql = "SELECT * FROM SystemRequired WHERE flowflag NOT IN ('','N')";
                if (DateB != "")
                {
                    sql = sql +  " AND ApplyDate BETWEEN '" + DateB + "' AND '" + DateE + "'";
                }
                if (_UserId != "")
                {
                    sql = sql +  " AND ApplyEmpID='" + _UserId + "'";
                }
                if (ApplyOrgNO != "")
                {
                    sql = sql +  " AND ApplyOrg_NO='" + ApplyOrgNO + "'";
                }
                if (RequiredType != "")
                {                    
                    sql = sql + " AND RequiredType='" + RequiredType + "'";
                }
                if (Flowtype != "")
                {
                    sql = sql + " AND flowflag='" + Flowtype + "'";
                }
                sql = sql + " ORDER BY SysRequiredNo";
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
            return new object[] { 0, js }; // 傳回值: 無
        }
    }
}
