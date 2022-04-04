using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sERPToDoCustomer
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

        // 修改本次內容,新增下次複訪日期
        public object[] AddCustomerToDoNotes(object[] objParam)
        {
            //新增今天日期維保紀錄或新增下次複訪日期------------------------------------------------------------------------
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDate = parm[1];//本次複訪日期   
            string CallNote = parm[2];//本次複訪內容
            string NewRecallDate = parm[3];//下次複訪日            
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = parm[4];
            string js = string.Empty;

            string sql = null;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "exec procInsertERPCustomerToDoNotesbyEasy '" + CustNO + "','" + CallNote + "','" + NextCallDate + "','',0,'" + username + "',3,'" + NewRecallDate + "'";
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
        // 新增客戶資料後 & 新增下次複訪提醒
        public object[] AddCustomerToDoNotes2(object[] objParam)
        {
            //新增今天日期維保紀錄或新增下次複訪日期------------------------------------------------------------------------
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDate = parm[1];//本次複訪日期   
            string CallNote = parm[2];//本次複訪內容
            string NewRecallDate = parm[3];//下次複訪日            
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = parm[4];
            string js = string.Empty;

            string sql = null;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "exec procInsertERPCustomerToDoNotesbyEasy '" + CustNO + "','" + CallNote + "','" + NextCallDate + "','',0,'" + username + "',4,'" + NewRecallDate + "'";
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
        // 新增下次複訪提醒---增加業務
        public object[] AddCustomerToDoNotesSalse(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDateAdd = parm[1];//下次複訪日期   
            string NextCallTimeAdd = parm[2];//下次複訪時間
            string PostType = parm[3];//客戶等級
            string SalesID = parm[4];//業務
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
            string js = string.Empty;

            string sql = null;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "exec procInsertERPCustomerToDoNotesbyToDo2 '" + CustNO + "','','" + NextCallDateAdd + "','" + NextCallTimeAdd + "',0,'" + username + "','" + PostType + "','" + SalesID + "'";
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

        private void updateCustomerToDoNotes_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            updateCustomerToDoNotes.SetFieldValue("NotesCreateDate", DateTime.Now);//欄位賦值
            updateCustomerToDoNotes.SetFieldValue("UpateDate", DateTime.Now);//欄位賦值
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            updateCustomerToDoNotes.SetFieldValue("UpdateBy", LoginUser);//欄位賦值
        }

        //	1.到期客戶	2.銷貨備註 3.複訪日期
        public object[] getQuerylist(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesID = parm[0];
            string CustNO = parm[1];
            string MinSalesDate = parm[2];
            string MaxSalesDate = parm[3];
            string iSourse = parm[4];

            string js = string.Empty;

            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string SQL = " exec procDisplayToDoCustomer '" + SalesID + "','" + CustNO + "','" + MinSalesDate + "','" + MaxSalesDate + "'," + iSourse + "\r\n";

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        //到期客戶=>刊登提醒 取消 ERPSalesMaster => KeepDaysAlert=0
        public object[] UpdateERPSalesMasterDaysAlert(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string Type = parm[2];           

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
                string SQL = "exec procUpdateERPSalesMasterDaysAlert " + SalesMasterNO+",'"+ItemSeq+"',"+Type;
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
            return new object[] { 0, js };
        }

       

       
        private void ucERPCustomers_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            
        }
        //至銷貨客戶修改1,6,31之電子發票Email
        public object[] procAddCustomerERPPayKind(object[] objParam)
        {
            string sql = null;
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0].ToString();
            string ERPCustomerID = parm[1].ToString();
            string Name = parm[2].ToString();
            string Email = parm[3].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string js = string.Empty;

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = "EXEC procUpdateJBADMINERPPayKindbyMedia '" + CustNO + "','" + ERPCustomerID + "','" +Name + "','" + Email+"'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {                
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };

        }
        private void ucERPCustomers_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
    
        }

      

        


    }
}
