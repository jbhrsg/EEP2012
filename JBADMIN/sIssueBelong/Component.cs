using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Xml;

namespace sIssueBelong
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
        public object[] CheckMasterDelete(object[] objParam)
        {
            //���oIssueBelong�O�_���bIssueType���Q�ϥΨ�
            string IssueBelongID = (string)objParam[0];
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
                string sql = " select count(*) as iCount from IssueType where IssueBelongID=" + IssueBelongID;
                DataSet dsBelong = this.ExecuteSql(sql, connection, transaction);                
                //// Indented�Y�� �N����ഫ��Json�榡
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
            }
            return new object[] { 0, js };            
        }
        public object[] CheckDetailDelete(object[] objParam)
        {
            //���oIssueTypeID�O�_���bIssueJob���Q�ϥΨ�
            string IssueTypeID = (string)objParam[0];
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
                string sql = " select count(*) as iCount from IssueJob where IssueTypeID=" + IssueTypeID;
                DataSet dsType = this.ExecuteSql(sql, connection, transaction);                
                //// Indented�Y�� �N����ഫ��Json�榡
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
