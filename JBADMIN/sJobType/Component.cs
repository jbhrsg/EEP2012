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
using System.IO;
using System.Xml;



namespace sJobType
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
        //todo:test
        public object[] CheckJobType(object[] objParam)
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
                string JB_TYPE = objParam[0].ToString();
                //string sql = "SELECT COUNT(*) AS CNT FROM [60.250.52.106,1433].JBADMIN.dbo.ERPCustomers WHERE CustNO = '" + CustNO + "'";
                string sql = "SELECT COUNT(*) AS CNT FROM [60.250.52.106,1433].NJB.dbo.JB_TYPE WHERE JB_TYPE = '" + JB_TYPE + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Newtonsoft.Json.Formatting.Indented);
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
        public string GetXMLAsString(XmlDocument myxml)
        {

            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            myxml.WriteTo(tx);

            string str = sw.ToString();// 
            return str;
        }
        public object[] procCALLAPI(object[] objParam)
        {
            string invoicexml = string.Empty;
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split('*');
            string UserID = parm[0].ToString();
            string JbType = "A00";
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            //IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Select ApiXml From NJB.DBO.JB_TYPE WHERE JB_TYPE='" + JbType + "'";
                DataSet ds = this.ExecuteSql(sql, connection,null);
                invoicexml = ds.Tables[0].Rows[0]["ApiXml"].ToString();
            }
            //catch
            //{
            //    transaction.Rollback();
            //}
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            string hastax = "1";
            //string rentid = "84211021";
            //string source = "jbj84ob8F8E9B85F";
            string rentid = "84211021";
            string source = "jbj84ob8F8E9B85F";
            cetustek.InvoiceAPIService o = new cetustek.InvoiceAPIService();
            string message = o.CreateInvoiceV3(invoicexml, hastax, rentid, source);
            js = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.Indented);
            return new object[] { 0, js };
            //return message;
        }
    }
}
