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

namespace sFWCRMServiceRecord
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
        //�A�Ȭ�����s�� ,���y���P,�s�X���P => ex: 210001--�褸�~�׫�2�X+4�X�y����
        public string RecordNoFixed()
        {
            //======================================�D�o�s���}�Y======================================
            //���q�O
            string CompanyID = ucFWCRMServiceRecordMaster.GetFieldCurrentValue("CompanyID").ToString().Trim();

            //1���D,2�~�� => ���D => C
            int RecordType = int.Parse(ucFWCRMServiceRecordMaster.GetFieldCurrentValue("RecordType").ToString());
            //2�~��=> 1	��߻� F,2�V�n U,3���� T,4�L�� I
            int NationalityID = int.Parse(ucFWCRMServiceRecordMaster.GetFieldCurrentValue("NationalityID").ToString());
            string NationalTitle = "C";
            if (RecordType == 2)
            {
                if (NationalityID == 1)
                {
                    NationalTitle = "F";
                }
                else if (NationalityID == 2)
                {
                    NationalTitle = "U";
                }
                else if (NationalityID == 3)
                {
                    NationalTitle = "T";
                }
                else if (NationalityID == 4)
                {
                    NationalTitle = "I";
                }

            }
            string sYear = DateTime.Now.Year.ToString().Substring(2,2);
            return CompanyID + NationalTitle + sYear;

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
        //���o�s�ʤ��q�O=>�o�춱�D���
        public object[] GetEmployer(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var EmployerID = Parameter_Input["Employer_ID"].ToString();
                //try { EmployerID = Parameter_Input["EmployerID"].ToString(); }
                //catch (Exception) { EmployerID = ""; }

                string SQL = @"
select distinct EmployerID,EmployerName 
from View_FWCRMServiceRecordEmployer
Where   CompanyID = @CompanyID 
union all
select '0',' --�п��--'
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

        //���o�s�ʤ��q�O,���D=>�o����y���
        public object[] GetNational(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var EmployerID = Parameter_Input["Employer_ID"].ToString();
                var RecordType = Convert.ToInt32(Parameter_Input["RecordType_ID"]);
                var NationalityID = 0;
                if (Parameter_Input["Nationality_ID"].ToString() != "")
                {
                    NationalityID = Convert.ToInt32(Parameter_Input["Nationality_ID"].ToString());
                }

                //try { EmployerID = Parameter_Input["EmployerID"].ToString(); }
                //catch (Exception) { EmployerID = ""; }
                //---������������D1 ,�~��2 => ���y�i�H����-------------
                string SQL = "";
                if (RecordType == 1)
                {
                    SQL = @"
select distinct NationalityID,NationalityText 
from View_FWCRMServiceRecordNationality
Where   CompanyID = @CompanyID and EmployerID=@EmployerID
union all
select '0',' --����--'
Order By NationalityID
";
                }else
                {
                    SQL = @"
select distinct NationalityID,NationalityText 
from View_FWCRMServiceRecordNationality
Where   CompanyID = @CompanyID and EmployerID=@EmployerID
Order By NationalityID
";
                }

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                Parameter.Add(new SqlParameter("@EmployerID", EmployerID));

                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<Int32>("NationalityID").ToString(),
                    text = m.Field<string>("NationalityText") ?? "",
                    selected = (m.Field<Int32>("NationalityID") == NationalityID)
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
        //�a�X�~�ҦW��
        public object[] getEmployeeData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CompanyID = parm[0];
            string EmployerID = parm[1];
            string NationalityID = parm[2];
            string iMonth = parm[3];

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
                string SQL = " exec procDisplayFWCRMServiceRecordEmployee " + CompanyID + ",'" + EmployerID + "','" + NationalityID + "','" + iMonth + "'" + "\r\n";

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
            }
            return new object[] { 0, js };
        }

        //�A�Ȭ��� ����--------------------------------------------------------------------------------
        public object[] ReportFWCRMServiceRecord(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string RecordNo = parm[0].ToString();
            string RecordType = parm[1].ToString();
            int iType = int.Parse(parm[2].ToString());
            int iSort = int.Parse(parm[3].ToString());

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //string sLoginDB = "Hunter";
            ////�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procReportFWCRMServiceRecord '" + RecordNo + "','" + RecordType + "','" + username + "'," + iType + "," + iSort;
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
                //ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        private void ucFWCRMServiceRecordMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucFWCRMServiceRecordMaster.SetFieldValue("CreateDate", DateTime.Now);//�g�J������ɤ���
            ucFWCRMServiceRecordMaster.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���

            //�h������ĤG�X ����ܽs��
            string RecordNo = ucFWCRMServiceRecordMaster.GetFieldCurrentValue("RecordNo").ToString();
            ucFWCRMServiceRecordMaster.SetFieldValue("RecordNoShow", RecordNo.Remove(0, 2));

        }

        private void ucFWCRMServiceRecordMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucFWCRMServiceRecordMaster.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���

        }

        private void ucFWCRMServiceRecordDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucFWCRMServiceRecordDetails.SetFieldValue("CreateDate", DateTime.Now);//�g�J������ɤ���
        }

        private void ucFWCRMServiceRecordDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucFWCRMServiceRecordDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
        }

        
        // ��ISO �������s�W���
        public object[] AddISODocument(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CompanyID = parm[0].ToString();
            string EmployerID = parm[1].ToString();
            string NationalityID = parm[2].ToString();
            string Remark = parm[3].ToString();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());


            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //string sLoginDB = "Hunter";
            ////�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = "exec procInsertISODocumentForm " + CompanyID + ",'" + EmployerID + "'," + NationalityID + ",'" + Remark + "','" + userid + "'";
                this.ExecuteSql(SQL, connection, transaction);
                transaction.Commit();

            }
            catch
            {
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

       




    }
}
