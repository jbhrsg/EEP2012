using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sERPSalesToDoList
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
            //修改本次內容,新增下次複訪日期------------------------------------------------------------------------
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDate = parm[1];//本次複訪日期   
            string CallNote = parm[2];//本次複訪內容
            string NewRecallDate = parm[3];//下次複訪日            
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
                sql = "exec procInsertERPCustomerToDoNotesbyEasy '" + CustNO + "','" + CallNote + "','" + NextCallDate + "','',0,'" + username + "',3,'" + NewRecallDate + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Commit();
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
                sql = "exec procInsertERPCustomerToDoNotesbyToDo2 '" + CustNO + "','','" + NextCallDateAdd + "','" + NextCallTimeAdd + "',0,'" + username + "','" + PostType+ "','" + SalesID+"'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Commit();
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

       
    }
}
