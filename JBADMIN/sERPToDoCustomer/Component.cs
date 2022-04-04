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

        // �ק糧�����e,�s�W�U���ƳX���
        public object[] AddCustomerToDoNotes(object[] objParam)
        {
            //�s�W���Ѥ�����O�����ηs�W�U���ƳX���------------------------------------------------------------------------
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDate = parm[1];//�����ƳX���   
            string CallNote = parm[2];//�����ƳX���e
            string NewRecallDate = parm[3];//�U���ƳX��            
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
        // �s�W�Ȥ��ƫ� & �s�W�U���ƳX����
        public object[] AddCustomerToDoNotes2(object[] objParam)
        {
            //�s�W���Ѥ�����O�����ηs�W�U���ƳX���------------------------------------------------------------------------
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string NextCallDate = parm[1];//�����ƳX���   
            string CallNote = parm[2];//�����ƳX���e
            string NewRecallDate = parm[3];//�U���ƳX��            
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
            updateCustomerToDoNotes.SetFieldValue("NotesCreateDate", DateTime.Now);//�����
            updateCustomerToDoNotes.SetFieldValue("UpateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            updateCustomerToDoNotes.SetFieldValue("UpdateBy", LoginUser);//�����
        }

        //	1.����Ȥ�	2.�P�f�Ƶ� 3.�ƳX���
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

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string SQL = " exec procDisplayToDoCustomer '" + SalesID + "','" + CustNO + "','" + MinSalesDate + "','" + MaxSalesDate + "'," + iSourse + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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

        //����Ȥ�=>�Z�n���� ���� ERPSalesMaster => KeepDaysAlert=0
        public object[] UpdateERPSalesMasterDaysAlert(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string Type = parm[2];           

            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
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
        //�ܾP�f�Ȥ�ק�1,6,31���q�l�o��Email
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
            //�}�ltransaction
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
