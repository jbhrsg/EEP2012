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

        //���o��´�s��(�p�G�ۤv���b��´��)�B���ݥD�ު���´�s��
        public object[] GetUserOrgNOs(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                //funReturnEmpOrgNOL2--�ڦb����´�Χںު���´(�X�Ӫ���´���W�h��´�O�`�g��)(L2����´)
                //funReturnEmpOrgNOParent--�ڦb����´(�u��)�Χںު���´���W�h��´(���ݪ���´)
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

        //�s�W���ڳ�D�ɩM������
        public object[] InsertWarrantMasterDetails(object[] objParam)
        {
            string js = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string ApplyNO = dr["ApplyNO"].ToString();
            string InvoiceNO = dr["InvoiceNO"].ToString();
            //string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string WarrantNO = GetWarrantNO();//���ڳ渹

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

        //�����ڳ渹
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
                //�����ڦ~��A�̤j���ڳ渹
                string sql = "select top 1 * from WarrantMaster where substring(convert(nvarchar(20),WarrantDate,120),1,7)='" + WarrantYM + "' order by WarrantNO desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                
                WarrantNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["WarrantNO"].ToString();
                if (WarrantNO != "")//��
                {
                    int num = Convert.ToInt32(WarrantNO.Substring(6, 5));
                    WarrantNO = WarrantYM.Replace("-", "") + ((num + 1).ToString("00000")); ;
                }
                else//�S��
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
