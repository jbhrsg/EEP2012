using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;

namespace sERP_Normal_InvoiceVoidApply
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
        //取註銷單號
        public object[] GetInvoiceVoidNO(object[] objParam)
        {
            string js = string.Empty;
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                //string sql = "SELECT dbo.funReturnInvoiceVoidNO() AS ReturnStr FROM EIPHRSYS.DBO.USERS WHERE UserID='" + UserID + "'";
                string sql = "SELECT 'DE'+convert(nvarchar(4),YEAR(GETDATE()))+" + "\r\n";
                sql = sql + "(" + "\r\n";
                sql = sql + "select right('0000'+LTRIM(STR(" + "\r\n";
                sql = sql + "IsNull(max(CAST(Right(InvoiceVoidNO, 4) as int)),0)+1" + "\r\n";
                sql = sql + ",4)),4) from InvoiceVoidApply" + "\r\n";
                sql = sql + ") " + "\r\n";
                sql = sql + "AS ReturnStr FROM EIPHRSYS.DBO.USERS WHERE UserID='" + UserID + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = ds.Tables[0].Rows[0]["ReturnStr"].ToString(); ;

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js }; ;

        }

        //取使用者組織編號
        public object[] GetUserOrgNOs(object[] objParam)
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;

        }

        //取代理人s
        public object[] GetEmpFlowAgentList(object[] objParam)
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string Flow = parm[1];
                string sql = "SELECT dbo.funRetrunEmpFlowAgentList('" + UserID + "','" + Flow + "') AS ReturnStr FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = ds.Tables[0].Rows[0]["ReturnStr"].ToString(); ;
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

        //Flow作廢單據及銷貨
        public object[] VoidInvoiceDetailsNSalesMaster(object[] objParam)
        {
            object[] res = SrvUtils.GetValue("_usercode", this);
            string UserID = res[1].ToString();

            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string InvoiceVoidNO = dr["InvoiceVoidNO"].ToString();
            string QInvoiceType = dr["QInvoiceType"].ToString();
            string InvoiceNO = dr["InvoiceNO"].ToString();
            string SalesNO = dr["SalesNO"].ToString();
            string VoidNotes = dr["VoidNotes"].ToString();
            string InsGroupID="";
            string APIWebCode="";
            string APIPassword="";
            string TaxNO = "";
            string CreateInvoiceType = "";
            string IsVoidSalesMaster = dr["IsVoidSalesMaster"].ToString();
            string InvoiceDate = "";

            //查公司別，開發票類型
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open){ connection.Open();}
            IDbTransaction transaction = connection.BeginTransaction();
            try{
                //公司別
                string sql0 = "select InsGroupID,InvoiceDate from InvoiceDetails where InvoiceNO='" + InvoiceNO + "'";
                DataSet ds0 = this.ExecuteSql(sql0, connection, transaction);
                InsGroupID=ds0.Tables[0].Rows[0]["InsGroupID"].ToString();
                InvoiceDate = ds0.Tables[0].Rows[0]["InvoiceDate"].ToString();

                //開發票類型
                string sql1 = "select CreateInvoiceType from SalesMaster where SalesNO='" + SalesNO + "'";
                DataSet ds1 = this.ExecuteSql(sql1, connection, transaction);
                CreateInvoiceType = ds1.Tables[0].Rows[0]["CreateInvoiceType"].ToString();

                transaction.Commit();
            }
            catch{transaction.Rollback();
            throw new Exception("查公司別發生錯誤，請洽管理室");
            }
            finally{ReleaseConnection("JBERP", connection);}


            if (InsGroupID != "")
            {//用公司別查APIWebCode,APIPassword,TaxNO
                connection = (IDbConnection)AllocateConnection("JBADMIN");
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                transaction = connection.BeginTransaction();
                try
                {
                    string sql = "SELECT APIWebCode,APIPassword,TaxNO FROM InsGroup  WHERE IsActive=1 and InsGroupID='" + InsGroupID + "'";
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    APIWebCode = ds.Tables[0].Rows[0]["APIWebCode"].ToString();
                    APIPassword = ds.Tables[0].Rows[0]["APIPassword"].ToString();
                    TaxNO = ds.Tables[0].Rows[0]["TaxNO"].ToString();
                    transaction.Commit();
                }
                catch { transaction.Rollback();
                throw new Exception("查APIWebCode，請洽管理室");
                }
                finally { ReleaseConnection("JBADMIN", connection); }
            }



            //作廢電開發票
            if ((QInvoiceType == "98" || QInvoiceType == "99")&& CreateInvoiceType !="2")
            {
                    if (InsGroupID != "" && SalesNO != "" && InvoiceNO != "" && APIWebCode != "" && APIPassword != "" && TaxNO != "")
                    {
                        string ObjParameter1 = InsGroupID + "*" + SalesNO + "*" + InvoiceNO + "*" + TaxNO + "*" + APIWebCode + APIPassword + "*" + "" + "*" + VoidNotes + "*" + UserID + "*" + InvoiceDate;
                        EEPRemoteModule module = new EEPRemoteModule();
                        var obj = module.CallMethod(new object[] { this.ClientInfo[0] }, "sERPSalesInvoices", "procCallApi_Cancel", new object[] { ObjParameter1 });

                        if (obj[1].ToString() == "C0")//成功作廢電開發票
                        {
                            if (IsVoidSalesMaster == "True")//作廢銷貨
                            {
                                string ObjParameter2 = SalesNO + "," + UserID;
                                EEPRemoteModule module2 = new EEPRemoteModule();
                                var obj2 = module2.CallMethod(new object[] { this.ClientInfo[0] }, "sERPSalesInvoices", "procDeleteSales", new object[] { ObjParameter2 });
                                if (obj2[1].ToString() != "OK")
                                {//作廢銷貨失敗
                                    throw new Exception(obj2[1].ToString());
                                }
                            }
                        }
                        else
                        {//作廢電開發票失敗
                            throw new Exception(obj[1].ToString());
                        }
                    }
                    else {
                        throw new Exception("沒有成功作廢發票，請洽管理室");
                    }
            }
            //作廢收據
            else if (QInvoiceType == "97")
            {
                if (SalesNO != "" && InvoiceNO != "")
                {
                    string ObjParameter1 = SalesNO + "," + InvoiceNO + "," + UserID;
                    EEPRemoteModule module = new EEPRemoteModule();
                    var obj = module.CallMethod(new object[] { this.ClientInfo[0] }, "sERPSalesInvoices", "Receipt_Cancel", new object[] { ObjParameter1 });

                    if (obj[1].ToString() == "C00")//成功作廢收據
                    {
                        if (IsVoidSalesMaster == "True")//作廢銷貨
                        {
                            string ObjParameter2 = SalesNO + "," + UserID;
                            EEPRemoteModule module2 = new EEPRemoteModule();
                            var obj2 = module2.CallMethod(new object[] { this.ClientInfo[0] }, "sERPSalesInvoices", "procDeleteSales", new object[] { ObjParameter2 });
                            if (obj2[1].ToString() != "OK")
                            {//作廢銷貨失敗
                                throw new Exception(obj2[1].ToString());
                            }
                        }
                    }
                    else
                    {//作廢收據失敗
                        throw new Exception(obj[1].ToString());
                    }
                }
            }//作廢手開發票
            else if ((QInvoiceType == "98" || QInvoiceType == "99") && CreateInvoiceType == "2")
            {
                if (SalesNO != "" && InvoiceNO != "")
                {
                    string ObjParameter1 = SalesNO + "," + InvoiceNO + "," + UserID;
                    EEPRemoteModule module = new EEPRemoteModule();
                    var obj = module.CallMethod(new object[] { this.ClientInfo[0] }, "sERPSalesInvoices", "HandWriteInvoice_Cancel", new object[] { ObjParameter1 });

                    if (obj[1].ToString() == "C000")//成功作廢手開發票
                    {
                        if (IsVoidSalesMaster == "True")//作廢銷貨
                        {
                            string ObjParameter2 = SalesNO + "," + UserID;
                            EEPRemoteModule module2 = new EEPRemoteModule();
                            var obj2 = module2.CallMethod(new object[] { this.ClientInfo[0] }, "sERPSalesInvoices", "procDeleteSales", new object[] { ObjParameter2 });
                            if (obj2[1].ToString() != "OK")
                            {//作廢銷貨失敗
                                throw new Exception(obj2[1].ToString());
                            }
                        }
                    }
                    else
                    {//作廢收據失敗
                        throw new Exception(obj[1].ToString());
                    }
                }
            }
            else {
                throw new Exception("沒有成功作廢發票(無對應的單據類型)，請洽管理室");
            }
            return ret;
        }
    }
}
