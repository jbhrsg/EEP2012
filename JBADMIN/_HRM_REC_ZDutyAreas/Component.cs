using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;

namespace _HRM_REC_ZDutyAreas
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

        private void ucREC_ZDutyAreasClass_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucREC_ZDutyAreasClass.SetFieldValue("LastUpdateDate", DateTime.Now);
            string ID = ucREC_ZDutyAreasClass.GetFieldCurrentValue("ID").ToString();
            ucREC_ZDutyAreasClass.SetFieldValue("SortID", ID);

        }

        private void ucREC_ZDutyAreasClass_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucREC_ZDutyAreasClass.SetFieldValue("LastUpdateDate", DateTime.Now);

        }
        public object[] CheckMasterDelete(object[] objParam)
        {
            //取得ID是否有在REC_ZDutyAreas中被使用到
            string ID = (string)objParam[0];
            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCH";
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
                string sql = " select count(*) as iCount from REC_ZDutyAreas where ClassID=" + ID;
                DataSet dsBelong = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = dsBelong.Tables[0].Rows[0]["iCount"].ToString();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);

            }
            return new object[] { 0, js };
        }
        public object[] CheckDetailDelete(object[] objParam)
        {
            //取得IssueTypeID是否有在IssueJob中被使用到
            string IssueTypeID = (string)objParam[0];
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
                string sql = " select count(*) as iCount from IssueJob where IssueTypeID=" + IssueTypeID;
                DataSet dsType = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = dsType.Tables[0].Rows[0]["iCount"].ToString();
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



    }
}
