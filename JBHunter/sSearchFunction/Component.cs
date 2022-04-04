using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;

namespace sSearchFunction
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

        private void ucJobAssignLogs_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {                               
            //判斷HUT_JobAssignNew是否有資料=>決定新增'修改
            //n.AssignID=6不適任=>表示移出餐盤,刪除HUT_JobAssignNew資料           
            string UserID = ucJobAssignLogs.GetFieldCurrentValue("UserID").ToString();
            int JobID = Convert.ToInt32(ucJobAssignLogs.GetFieldCurrentValue("JobID"));
            string LoginUser = ucJobAssignLogs.GetFieldCurrentValue("CreateBy").ToString();  
            string sql = "exec procInsertJobAssignNew '" + UserID + "'," + JobID + ",'" + LoginUser + "'";
            this.ExecuteSql(sql, ucJobAssignLogs.conn, ucJobAssignLogs.trans);                   
        }
        private void ucJobAssignLogs_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //刪除後若還有資料須修改HUT_JobAssignNew的資料,沒有則刪除HUT_JobAssignNew資料
            string UserID = ucJobAssignLogs.GetFieldOldValue("UserID").ToString();
            int JobID = Convert.ToInt32(ucJobAssignLogs.GetFieldOldValue("JobID"));
            string LoginUser = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "exec procUpdateJobAssignNew '" + UserID + "'," + JobID + ",'" + LoginUser + "'";
            this.ExecuteSql(sql, ucJobAssignLogs.conn, ucJobAssignLogs.trans);
        }           

    }
       
}
