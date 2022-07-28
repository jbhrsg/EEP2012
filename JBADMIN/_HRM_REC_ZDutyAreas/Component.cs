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
            //���oID�O�_���bREC_ZDutyAreas���Q�ϥΨ�
            string ID = (string)objParam[0];
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCH";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = " select count(*) as iCount from REC_ZDutyAreas where ClassID=" + ID;
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
                ReleaseConnection(sLoginDB, connection);

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
