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

namespace sglCompany
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
        //傳回登入者可用的成本中心
        public object[] GetCostCenter(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var UserID = Parameter_Input["User_ID"].ToString();
                //try { EmployerID = Parameter_Input["EmployerID"].ToString(); }
                //catch (Exception) { EmployerID = ""; }

                string SQL = @"
select g.CostCenterID,c.CostCenterName from glCostCenterUser g
	inner join glCostCenter c on g.CostCenterID=c.CostCenterID
where g.UserID=@UserID
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@UserID", UserID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("CostCenterID").ToString(),
                    text = m.Field<string>("CostCenterName") ?? "",
                    selected = (m.Field<string>("CostCenterID") == "")
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

        //鎖檔檢查=>資料不可為失效
        public object[] checkLockYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string YM = DateTime.Parse(parm[1].ToString()).Year.ToString() + DateTime.Parse(parm[1].ToString()).Month.ToString().PadLeft(2,'0');


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
                string sql = " select count(*) as iCount from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + " and LockYM='"+YM+"'";
                DataSet dsIsActive = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = dsIsActive.Tables[0].Rows[0]["iCount"].ToString();
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

        private void ucglCostCenterUser_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucglCostCenterUser.SetFieldValue("LastUpdateDate", DateTime.Now);//寫入日期的時分秒

        }

        private void ucglCostCenterUser_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglCostCenterUser.SetFieldValue("LastUpdateDate", DateTime.Now);//寫入日期的時分秒

        }




    }
}
