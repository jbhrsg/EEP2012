using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMGetNo
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
        //�u�渹�X=> ex: FWW1050001----����~+�y���X(3)
        public string WorkNoFixed()
        {
            //return string.Format("O{0:yyMMdd}", DateTime.Now.Date);
            DateTime datetime = DateTime.Today;
            return "FWA" + (datetime.Year - 1911).ToString().Trim();//FWW
        }
        
        private void ucFWCRMWorkNo_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucFWCRMWorkNo.SetFieldValue("CreateDate", DateTime.Now);//�����
        }

        //�J�ҽT�{�渹�X=> ex: FWC1050001----����~+�y���X(3)
        public string IndateNoFixed()
        {
            DateTime datetime = DateTime.Today;
            return "FWS" + (datetime.Year - 1911).ToString().Trim();//FWC
        }

        private void ucFWCRMIndateNo_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucFWCRMIndateNo.SetFieldValue("CreateDate", DateTime.Now);//�����

        }
        //���͸u�u�����
        public object[] ReportWorkNo(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string WorkNo = parm[0];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
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
                string sql = "select top 1 m.WorkNo,m.EmployerID,r.EmployerName,r.ContactName,r.ContactTel,r.ContactEmail,r.EmployerAddress " + "\r\n";
                sql = sql + "from FWCRMWorkNo m  " + "\r\n";
                sql = sql + "inner join dbo.View_FWCRMEmployer r on m.EmployerID=r.EmployerID  " + "\r\n";            
                sql = sql + " where m.WorkNo='" + WorkNo + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);

                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {

                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        //���ͤJ�ҳ����
        public object[] ReportIndateNo(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string IndateNo = parm[0];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
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
                string sql = "select o.OrderNo,m.IndateNo,r.EmployerName,dbo.funReturnReferenceData('nationality',o.NationalityID) as NationalityText " + "\r\n";
                sql = sql + ",dbo.funReturnFWCRMIndateNoReport('11') as AssetLocation  " + "\r\n";
                sql = sql + "from FWCRMIndateNo m  " + "\r\n";
                sql = sql + "inner join FWCRMOrders o on m.OrderNo=o.OrderNo  " + "\r\n";
                sql = sql + "inner join dbo.View_FWCRMEmployer r on o.EmployerID=r.EmployerID   " + "\r\n";
                sql = sql + " where m.IndateNo='" + IndateNo + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);

                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {

                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }



    }
}
