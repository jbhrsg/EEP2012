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
using System.Text.RegularExpressions;

namespace sPetitionMaster
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

        //���o�������
        public object[] GetUserDept(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string EMPLOYEE_CODE = parm[0];
            var js = string.Empty;
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
                string sql = "Select JBHR_EEP.dbo.funReturnDeptInfo(DEPT_ID,1) AS DEPT_CODE,JBHR_EEP.dbo.funReturnDeptInfo(DEPTC_ID,2) AS DEPT_CNAME"
                    + " From JBHR_EEP.dbo.[dtHRM_BaseAndBasetts_Employed](GetDate()) where EMPLOYEE_CODE='" + EMPLOYEE_CODE + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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

        public object[] GetEmpFlowAgentList(object[] objParam)
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
            return new object[] { 0, js }; ;
        }
      
        //ñ�ֽs��
        public string NoFixed()
        {
            return string.Format("PT{0:yyyy}", DateTime.Now.Date);
        }
        
        //ñ�e�D�ަW��
        public object[] GetPetitionList(object[] objParam)
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

                string sql = "SELECT * FROM View_UsersGROUPS WHERE USERID='" + UserID + "'";
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
            return new object[] { 0, js };
        }
        
        //�|ñ����ɶ�
        private void ucPetitionCountersignBeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            //string LoginUser = SrvGL.GetUserName(userid.ToLower());
            //ucPetitionCountersign.SetFieldValue("CreateBy", LoginUser);
            var dt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
            ucPetitionCountersign.SetFieldValue("CountersignDate", dt);
        }

        private void ucPetitionCountersignBeforeInsert(object sender,UpdateComponentBeforeInsertEventArgs e)
        {            
            var dt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
            ucPetitionCountersign.SetFieldValue("CreateDate", dt);
        }

         //�[ñ���ListID�s�JPetitionMaster
        public object[] InserFlowID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow rowparm = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            var _PetitionNO = rowparm["PetitionNO"].ToString(); //ñ�ֽs��
            string _PetitionNOValue = "PetitionNO=" + "'" + _PetitionNO + "'";
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
                string _listid = string.Empty;
                //string _applicant=string.Empty;
                string sql = "SELECT LISTID,APPLICANT FROM View_SYS_TODOLIST_Petition WHERE FORM_KEYS='PetitionNO' AND FORM_TABLE='PetitionMaster' ";
                sql = sql + "AND FORM_PRESENTATION LIKE'%" + _PetitionNO + "%'";
                //sql = sql + "AND FORM_PRESENTATION ='" + _PetitionNOValue + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _listid = ds.Tables[0].Rows[0]["LISTID"].ToString();
                    //_applicant = ds.Tables[0].Rows[0]["APPLICANT"].ToString();
                    var updatesql = "UPDATE PetitionMaster SET FlowListid='" + _listid + "'";//,APPLICANT='" + _applicant + "'
                    updatesql = updatesql + " WHERE PetitionNO='" + _PetitionNO + "'";

                    this.ExecuteSql(updatesql, connection, transaction);
                    transaction.Commit(); // �T�{���
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        //���oListID
        public object[] GetListID(object[] objParam)
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
                string _PetitionNO = parm[0];
                string _listid = string.Empty;
                //string _applicant=string.Empty;
                string sql = "SELECT Distinct LISTID FROM View_SYS_TODOLIST_Petition WHERE FORM_KEYS='PetitionNO' AND FORM_TABLE='PetitionMaster' ";
                sql = sql + "AND FORM_PRESENTATION LIKE'%" + _PetitionNO + "%'";
                //sql = sql + "AND FORM_PRESENTATION ='" + _PetitionNOValue + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    _listid = ds.Tables[0].Rows[0]["LISTID"].ToString();
                //    //_applicant = ds.Tables[0].Rows[0]["APPLICANT"].ToString();
                //    var updatesql = "UPDATE PetitionMaster SET FlowListid='" + _listid + "'";//,APPLICANT='" + _applicant + "'
                //    updatesql = updatesql + " WHERE PetitionNO='" + _PetitionNO + "'";

                //    this.ExecuteSql(updatesql, connection, transaction);
                //    transaction.Commit(); // �T�{���
                //}
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
            return new object[] { 0, js }; // �Ǧ^��: �L
        }

        //�@��d��
        public object[] GetNormalPetition(object[] objParam)
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
                string _UserId = parm[0];
                int _FileLevel = int.Parse(parm[1]);
                string DateB = parm[2];
                string DateE = parm[3];
                string sql = "SELECT * FROM PetitionMaster WHERE EXISTS (SELECT USER_ID FROM View_SYS_TODOHIS_Petition ";
                sql = sql + " WHERE PetitionMaster.FlowListid= View_SYS_TODOHIS_Petition.LISTID";
                if (DateB != "" && DateE != "")
                {
                    sql = sql + " AND PetitionMaster.PetitionDate BETWEEN '" + DateB + "' AND '" + DateE + "'";
                }
                sql = sql + " )";
                sql = sql + " AND PetitionMaster.FileLevel=" + _FileLevel + " AND PetitionMaster.flowflag='Z' ORDER BY PetitionMaster.CreateDate";
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
            return new object[] { 0, js }; // �Ǧ^��: �L
        }

        //���\�αK��d��
        public object[] GetReadPetition(object[] objParam)
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
                string _UserId = parm[0];
                int _FileLevel = int.Parse(parm[1]);
                string DateB = parm[2];
                string DateE = parm[3];
                string sql = "SELECT * FROM PetitionMaster WHERE EXISTS (SELECT USER_ID FROM View_SYS_TODOHIS_Petition ";
                sql = sql + " WHERE PetitionMaster.FlowListid= View_SYS_TODOHIS_Petition.LISTID";
                sql = sql + " AND View_SYS_TODOHIS_Petition.USER_ID='" + _UserId + "' or  PetitionMaster.ReadDataEmpID like '%" + _UserId + "%'";
                if (DateB != "" && DateE != "")
                {
                    sql = sql + " AND PetitionMaster.PetitionDate BETWEEN '" + DateB + "' AND '" + DateE + "'";
                }
                sql = sql + " )";
                sql = sql + " AND PetitionMaster.FileLevel=" + _FileLevel + " AND PetitionMaster.flowflag='Z' ORDER BY PetitionMaster.CreateDate";
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
            return new object[] { 0, js }; // �Ǧ^��: �L
        }

    //    //���o�[ñ�̦W��ηN��
    //    public object[] PlusApproveLists(object[] objParam)
    //    {
    //        string js = string.Empty;
    //        //�إ߸�Ʈw�s��
    //        IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
    //        //��s�u���A������open�ɡA�}�ҳs��
    //        if (connection.State != ConnectionState.Open)
    //        {
    //            connection.Open();
    //        }
    //        //�}�ltransaction
    //        IDbTransaction transaction = connection.BeginTransaction();
    //        try
    //        {
    //            string[] parm = objParam[0].ToString().Split(',');
    //            string FlowListid = parm[0];

    //            string sql = "SELECT USERNAME,REMARK FROM View_SYS_TODOHIS_Petition WHERE LISTID='" + FlowListid + "' AND S_USER_ID='' ";
    //            sql = sql + " AND View_SYS_TODOHIS_Petition.D_STEP_ID=View_SYS_TODOHIS_Petition.S_STEP_ID";
    //            //DataSet ds = this.ExecuteSql(sql, connection, transaction);
    //            //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);                
    //            DataTable dt = this.ExecuteSql(sql, connection, transaction).Tables[0];
    //            js = Newtonsoft.Json.JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
    //            transaction.Commit(); //��ϥ� transaction ��,�ݼW�[��Command
    //        }
    //        catch
    //        {
    //            transaction.Rollback();
    //            return new object[] { 0, false };
    //        }
    //        finally
    //        {
    //            ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
    //        }
    //        return new object[] { 0, js };
    //    }
    }
}
