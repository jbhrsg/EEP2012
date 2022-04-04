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
        //�V�m�ӽЪ�s��
        public string GetTrainingIDFixed()
        {
            DateTime datetime = DateTime.Today;
            return "TR" + (datetime.Year).ToString().Trim();
        }
        //Flow����дڳ�=>�s�W��ƨ�Requisition
        public object InsertRequisition(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string TrainingFormID = dr["TrainingFormID"].ToString(); // ���oTrainingFormID
            string CreateBy = dr["CreateBy"].ToString(); // ���oCreateBy
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());            
            IDbConnection conn = this.AllocateConnection("JBADMIN"); // ���o��Ƴs�u
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                IDbTransaction trans = conn.BeginTransaction(); // �_�l���
                string sql = "exec procInsertRequisition '" + TrainingFormID + "','" + CreateBy + "','" + username + "'";
                this.ExecuteCommand(sql, conn, trans); // �e�XSQL�y�y
                trans.Commit(); // �T�{���
            }
            finally
            {
                this.ReleaseConnection("JBADMIN", conn); // �O�����^��Ƴs�u
            }
            return ret; // �Ǧ^��: �L
        }
    }
}
