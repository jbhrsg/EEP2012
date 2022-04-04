using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace sERP_Normal_SalesAbnormalApply
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

        //取得組織編號(如果自己有在組織內)、直屬主管的組織編號
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
                //funReturnEmpOrgNOL2--我在的組織或我管的組織(出來的組織的上層組織是總經室)(L2的組織)
                //funReturnEmpOrgNOParent--我在的組織(優先)或我管的組織的上層組織(隸屬的組織)
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

        public string SalesAbnormalApplyNO_GetFixed() {

            return "SAA"+DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
        }

        //新增收款單主檔和明細檔
        public object[] InsertWarrantMasterDetails(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string ApplyNO = dr["ApplyNO"].ToString();
            string InvoiceNO = dr["InvoiceNO"].ToString();
            //string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string WarrantNO = GetWarrantNO();//收款單號

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                if (InvoiceNO !="" && ApplyNO != "" && WarrantNO !="")
                {
                    this.ExecuteCommand("exec procInsertWarrantMasterDetailsFromSalesAbnormalApply '" + ApplyNO + "','" + WarrantNO + "'", connection, transaction);
                }
                transaction.Commit();
                ret[1] = true;
            }
            catch (Exception ex)
            {
                ret[1] = false;
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return ret;
        }

        //取收款單號
        public string GetWarrantNO()
        {
            string WarrantNO = string.Empty;
            string WarrantDate = DateTime.Now.ToString("yyyy-MM-dd");
            string WarrantYM = WarrantDate.Substring(0, 7);
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //取收款年月，最大收款單號
                string sql = "select top 1 * from WarrantMaster where substring(convert(nvarchar(20),WarrantDate,120),1,7)='" + WarrantYM + "' order by WarrantNO desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                
                WarrantNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["WarrantNO"].ToString();
                if (WarrantNO != "")//有
                {
                    int num = Convert.ToInt32(WarrantNO.Substring(6, 5));
                    WarrantNO = WarrantYM.Replace("-", "") + ((num + 1).ToString("00000")); ;
                }
                else//沒有
                {
                    WarrantNO = WarrantYM.Replace("-", "") + "00001";
                }
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return WarrantNO;
        }
    }
}
