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
        /// <summary>Combobox�θ��</summary>
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
        //�Ǧ^�n�J�̥i�Ϊ���������
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

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }

        //�����ˬd=>��Ƥ��i������
        public object[] checkLockYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0].ToString();
            string YM = DateTime.Parse(parm[1].ToString()).Year.ToString() + DateTime.Parse(parm[1].ToString()).Month.ToString().PadLeft(2,'0');


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
                string sql = " select count(*) as iCount from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + " and LockYM='"+YM+"'";
                DataSet dsIsActive = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
            ucglCostCenterUser.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���

        }

        private void ucglCostCenterUser_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglCostCenterUser.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���

        }




    }
}
