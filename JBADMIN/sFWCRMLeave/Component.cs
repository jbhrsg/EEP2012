using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace sFWCRMLeave
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
        //離境單號=> ex: L1050108010
        public string LeaveNoFixed()
        {
            string VoucherNoTitle = "FL";

            DateTime VoucherDate = DateTime.Now;
            return VoucherNoTitle + (VoucherDate.Year - 1911).ToString().Trim() + VoucherDate.Month.ToString().PadLeft(2, '0') + VoucherDate.Day.ToString().PadLeft(2, '0');

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
        //======================================================得到雇主名稱資料======================================================
        public object[] GetrEmployerID(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);

                string SQL = @"
select '' as EmployerID,' ---請選擇---' as EmployerName
union all
select EmployerID,EmployerName from View_Employer 
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
                    selected = (m.Field<string>("EmployerID") == "")
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

        //======================================================得到外勞名稱資料======================================================
        public object[] GetrEmployeeID(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var EmployerID = (Parameter_Input["Employer_ID"]).ToString().Trim();

                string SQL = @"
select '' as EmployeeID,' ---請選擇---' as EmployeeTcName
union all
SELECT EmployeeID,EmployeeTcName
	From View_EmployeeLeave where CompanyID = @CompanyID and EmployerID=@EmployerID 
Order By EmployeeID
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                Parameter.Add(new SqlParameter("@EmployerID", EmployerID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("EmployeeID").ToString(),
                    text = m.Field<string>("EmployeeTcName") ?? "",
                    selected = (m.Field<string>("EmployeeID") == "")
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

        //======================================================選擇外勞姓名 => 得到性別、入境日=========================================================================//
        public object[] GetEmployeeIDData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');

            int CompanyID = int.Parse(parm[0].ToString());
            string EmployeeID = (string)parm[1];
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
                string sql = " select GenderText,EffectDate,NationalityText from View_EmployeeLeave where CompanyID=" + CompanyID + " and EmployeeID='" + EmployeeID + "'" + "\r\n";
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
            //return new object[] { 0, true };
        }




    }
}
