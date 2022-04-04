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
            //�P�_HUT_JobAssignNew�O�_�����=>�M�w�s�W'�ק�
            //n.AssignID=6���A��=>��ܲ��X�\�L,�R��HUT_JobAssignNew���           
            string UserID = ucJobAssignLogs.GetFieldCurrentValue("UserID").ToString();
            int JobID = Convert.ToInt32(ucJobAssignLogs.GetFieldCurrentValue("JobID"));
            string LoginUser = ucJobAssignLogs.GetFieldCurrentValue("CreateBy").ToString();  
            string sql = "exec procInsertJobAssignNew '" + UserID + "'," + JobID + ",'" + LoginUser + "'";
            this.ExecuteSql(sql, ucJobAssignLogs.conn, ucJobAssignLogs.trans);                   
        }
        private void ucJobAssignLogs_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //�R����Y�٦���ƶ��ק�HUT_JobAssignNew�����,�S���h�R��HUT_JobAssignNew���
            string UserID = ucJobAssignLogs.GetFieldOldValue("UserID").ToString();
            int JobID = Convert.ToInt32(ucJobAssignLogs.GetFieldOldValue("JobID"));
            string LoginUser = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "exec procUpdateJobAssignNew '" + UserID + "'," + JobID + ",'" + LoginUser + "'";
            this.ExecuteSql(sql, ucJobAssignLogs.conn, ucJobAssignLogs.trans);
        }           

    }
       
}
