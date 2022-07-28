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

        // �ק糧�����e,�s�W�U���ƳX���
        public object[] AddCustomerToDoNotes(object[] objParam)
        {
            //�ק糧�����e,�s�W�U���ƳX���------------------------------------------------------------------------
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDate = parm[1];//�����ƳX���   
            string CallNote = parm[2];//�����ƳX���e
            string NewRecallDate = parm[3];//�U���ƳX��            
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

        // �s�W�U���ƳX����---�W�[�~��
        public object[] AddCustomerToDoNotesSalse(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDateAdd = parm[1];//�U���ƳX���   
            string NextCallTimeAdd = parm[2];//�U���ƳX�ɶ�
            string PostType = parm[3];//�Ȥᵥ��
            string SalesID = parm[4];//�~��
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
            updateCustomerToDoNotes.SetFieldValue("NotesCreateDate", DateTime.Now);//�����
            updateCustomerToDoNotes.SetFieldValue("UpateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            updateCustomerToDoNotes.SetFieldValue("UpdateBy", LoginUser);//�����
        }

       
    }
}
