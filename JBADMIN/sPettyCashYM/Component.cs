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

namespace sPettyCashYM
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

        private void ucPettyCashYM_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucPettyCashYM.SetFieldValue("CreateDate", DateTime.Now);
        }

        private void ucPettyCashYM_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucPettyCashYM.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        //檢查合約號碼是否重複
        public object[] CheckYearMonthNODul(object[] objParam)
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
                string YearMonth = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM PettyCashYM WHERE YearMonth = '" + YearMonth + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
    }
}
