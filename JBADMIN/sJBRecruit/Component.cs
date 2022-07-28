using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using JBTool;
using System.Collections;
using System.Data.SqlClient;

namespace sJBRecruit
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

        //�۶Ҩt�Ψ̳̫�ק�i������Ӭd��
        public object[] UserListEEP(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string JQDate1 = (string)parm[0];
            string JQDate2 = (string)parm[1];
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            string sLoginDB = "JBRecruit";
            //�إ߸�Ʈw�s��
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
                string SQL = "exec [dbo].procDisplayUserListEEP '" + JQDate1 + "','" + JQDate2 + "',1";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }

        //�H�~��Ʒj�M �ץX
        public object[] UserListExcel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string JQDate1 = (string)parm[0];
            string JQDate2 = (string)parm[1];

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            string sLoginDB = "JBRecruit";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = "exec [dbo].procDisplayUserListEEP '" + JQDate1 + "','" + JQDate2 + "',2";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "���~�T��");
                theResult.Add("FileName", "�o�O�@���ɮ�.xls");

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
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };

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
        //���o�s�ʫȤ�=>�o��¾�ʸ��
        public object[] GetEmployer(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CustomerID = Convert.ToInt32(Parameter_Input["Customer_ID"]);
                var JobID = Parameter_Input["Job_ID"].ToString();
                //try { EmployerID = Parameter_Input["EmployerID"].ToString(); }
                //catch (Exception) { EmployerID = ""; }

                string SQL = @"
select distinct j.JobID,j.JobName
from [User] u
	left join EmpAccountTemp t on u.UserID=t.UserID
	left join AssignRecord ar on u.UserID = ar.UserID and ar.Autokey=(Select top 1 a.Autokey
							from AssignRecord a
							where a.UserID = u.UserID and a.IsActive = 1 
							order by a.AssignTime desc,a.AutoKey desc)
	inner join Job j on ar.AssignJob = j.JobID
	inner join Customer c on c.CustomerID = j.CustomerID
where  u.CollarType>0 and c.CustomerID!='0' and c.CustomerID = @CustomerID 
union all
select '0',' --�п��--'
Order By j.JobName
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CustomerID", CustomerID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("JobID").ToString(),
                    text = m.Field<string>("JobName") ?? "",
                    selected = (m.Field<string>("JobID") == JobID)
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
        //�g�J�H�~��ƦC��
        public object[] InsertUserDayCollarUser(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CollarType = parm[0];
            string RecruitID = parm[1];
            string Amt = parm[2];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            if (CollarType == "")
            {
                CollarType = "0";
            }
            if (RecruitID == "")
            {
                RecruitID = "0";
            }
            if (Amt == "")
            {
                Amt = "0";
            }
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            string sLoginDB = "JBRecruit";
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
                string SQL = " exec procInsertUserDayCollarUser " + CollarType + "," + RecruitID + "," + Amt + ",'" + username + "'"+"\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }

        private void updateUserDayCollarMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            updateUserDayCollarMaster.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���

        }
        private void updateUserDayCollarMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            updateUserDayCollarMaster.SetFieldValue("CreateDate", DateTime.Now);//�g�J������ɤ���
            updateUserDayCollarMaster.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
        }

        private void updateUserDayCollar_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            updateUserDayCollar.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���

        }
        private void updateUserDayCollar_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            updateUserDayCollar.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
            //updateUserDayCollar.SetFieldValue("MasterAutokey",null);
        }

        private void updateUserDayCollar_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            //�w��MasterAutokey=0�������h�ץ�
            string UserID = GetClientInfo(ClientInfoType.LoginUser).ToString();

            string sql = "";
            sql = "  update UserDayCollarDetail set MasterAutokey=(select Max(Autokey) from UserDayCollarMaster where CreateBy='" + UserID + "') from UserDayCollarDetail where MasterAutokey is null " + "\r\n";

            this.ExecuteCommand(sql, updateUserDayCollar.conn, updateUserDayCollar.trans);
        }

        public object[] TxtUserDayCollarData(object[] objParam)
        {
            string Autokey = objParam[0].ToString();

            string js = string.Empty; //�إ߸�Ʈw�s��
            string sLoginDB = "JBRecruit";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            } //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction(); try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string sql = "exec procTxtUserDayCollarData " + Autokey;
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = 60; //60��
                cmd.Transaction = transaction; var aDataTable = new DataTable();
                aDataTable.Load(cmd.ExecuteReader()); transaction.Commit();
                return new object[] { 0, new TheJsonResult { IsOK = true, Result = aDataTable }.ToJsonString() };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new object[] { 0, new TheJsonResult { IsOK = true, ErrorMsg = ex.Message }.ToJsonString() };
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
        }
        public object[] TxtUserDayCollarDataSmall(object[] objParam)
        {
            string Autokey = objParam[0].ToString();

            string js = string.Empty; //�إ߸�Ʈw�s��
            string sLoginDB = "JBRecruit";
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            } //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction(); try
            {
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string sql = "exec procTxtUserDayCollarDataSmall " + Autokey;
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = 60; //60��
                cmd.Transaction = transaction; var aDataTable = new DataTable();
                aDataTable.Load(cmd.ExecuteReader()); transaction.Commit();
                return new object[] { 0, new TheJsonResult { IsOK = true, Result = aDataTable }.ToJsonString() };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new object[] { 0, new TheJsonResult { IsOK = true, ErrorMsg = ex.Message }.ToJsonString() };
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
        }




    }
}
