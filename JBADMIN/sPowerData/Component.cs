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
using System.Collections;
using JBTool;

namespace sPowerData
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
        //================================================================================================================================//
        /// <summary>Combobox用資料</summary>
        public class ComboboxField
        {
            public string text { get; set; }

            public string value { get; set; }

            public bool selected { get; set; }

            public ComboboxField()
            {
                selected = false;
            }
        }
        //取得連動公司別=>得到雇主資料
        public object[] GetEmployer(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var EmployerID = "";
                //try { EmployerID = Parameter_Input["EmployerID"].ToString(); }
                //catch (Exception) { EmployerID = ""; }

                string SQL = @"
select distinct EmployerID,EmployerName 
from View_EmployeeLeave
Where   CompanyID = @CompanyID 
Order By EmployerName
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("EmployerID").ToString(),
                    text = m.Field<string>("EmployerName") ?? "",
                    selected = (m.Field<string>("EmployerID") == EmployerID)
                }).ToList();

                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }

        //取得連動公司別=>得到居住宿舍
        public object[] GetDorm(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var DormID = "";
                //try { EmployerID = Parameter_Input["EmployerID"].ToString(); }
                //catch (Exception) { EmployerID = ""; }

                string SQL = @"
select '' as DormID,' ---請選擇---' as DormName
union 
select distinct Cast(v.Dorm as nvarchar),v.RoomName+' (單價:'+Cast(v.PowerQty as nvarchar(10))+')'
from View_EmployeePowerLive v
Where   CompanyID = @CompanyID 
order by DormName
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("DormID").ToString(),
                    text = m.Field<string>("DormName") ?? "",
                    selected = (m.Field<string>("DormID") == DormID)
                }).ToList();

                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }

        //===============================將外勞費用刪除=======================================================
        public object[] DeleteEmpFees(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            int CompanyID = int.Parse(parm[0].ToString());//公司別
            string sEmpID = (string)parm[1];

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "FWCRM";
            if (CompanyID == 2)//公司別 傑報人力1,傑信管理2
            {
                sLoginDB = "FWCRMJS";
            }
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procDeleteEmpFees '" + sEmpID + "'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        //================================================================================================================================//

        //選擇居住宿舍帶出房間
        public object[] getDormIDData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompID = parm[0];           
            string DormID = parm[1];           

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
                string SQL = " exec procDisplayPowerDormIDData " +CompID+",'"+ DormID + "'" + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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
        //選擇房間帶出外勞資料,用公司別與居住宿舍關聯
        public object[] getRoomIDData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompID = parm[0];
            string DormID = parm[1];
            string RoomID = parm[2];

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
                string SQL = " exec procDisplayPowerRoomIDData " +CompID+",'"+ DormID + "','" + RoomID + "'" + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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

        //新增電費資料
        private void ucPowerMaster_AfterApplied(object sender, EventArgs e)
        {
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            string UserID = ucPowerMaster.GetFieldCurrentValue("UserID").ToString();
            string CreateBy = ucPowerMaster.GetFieldCurrentValue("CreateBy").ToString();

            //------------------------------------------------------------------------------------------------------------------

            try
            {
                string SQL = "exec procInsertPowerData " + "'" + UserID + "','" + CreateBy + "'";
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
        }

        //檢查費用年月是否已結帳
        public object[] CheckYearMonth(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0];

            string js = string.Empty;

            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "FWCRM";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select Isnull((select IsDormFeeEE from FWCRM.dbo.ARSetUpMaster where YearMonth='" + YearMonth + "' ),0) as IsClose" + "\r\n";
                DataSet dsYearMonth = this.ExecuteSql(sql, connection, transaction);
                string IsClose = dsYearMonth.Tables[0].Rows[0]["IsClose"].ToString();
                transaction.Commit();
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(IsClose, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);

            }
            return new object[] { 0, js };
        }
        //外勞費用立帳
        public object[] EmployeeFeesData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string PowerID = parm[0];           
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
                string SQL = "exec procDisplayEmployeeFeesbyEEP " + PowerID;
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);

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

        //宿舍收支表 報表--------------------------------------------------------------------------------
        public object[] ReportFWCRMDormFee(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string YearMonth = parm[1].ToString();
            
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
                string SQL = "exec procReportFWCRMDormFeeDetail " + CompanyID + ",'" + YearMonth + "'";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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
        //===============================將外勞費用設成已轉出Excel, IsExcel=1 =======================================================
        public object[] UpdateFeeIsExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string YearMonth = parm[0].ToString();
            string YearMonth2 = parm[1].ToString();

            string js = string.Empty;
            //建立資料庫連結
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "FWCRMJSCare";
           
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procUpdateEmployeeFeesBank '" + YearMonth+"','"+YearMonth2+"'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }





    }
}
