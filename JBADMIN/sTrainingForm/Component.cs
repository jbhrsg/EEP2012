using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sTrainingForm
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
        //訓練申請表編號
        public string GetTrainingIDFixed()
        {
            DateTime datetime = DateTime.Today;
            return "TR" + (datetime.Year).ToString().Trim();
        }
        //Flow結轉請款單=>新增資料到Requisition
        public object InsertRequisition(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            string TrainingFormID = dr["TrainingFormID"].ToString(); // 取得TrainingFormID
            string CreateBy = dr["CreateBy"].ToString(); // 取得CreateBy
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());            
            IDbConnection conn = this.AllocateConnection("JBADMIN"); // 取得資料連線
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                IDbTransaction trans = conn.BeginTransaction(); // 起始交易
                string sql = "exec procInsertRequisition '" + TrainingFormID + "','" + CreateBy + "','" + username + "'";
                this.ExecuteCommand(sql, conn, trans); // 送出SQL語句
                trans.Commit(); // 確認交易
            }
            finally
            {
                this.ReleaseConnection("JBADMIN", conn); // 保證釋回資料連線
            }
            return ret; // 傳回值: 無
        }
    }
}
