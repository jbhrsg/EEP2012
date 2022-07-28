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

namespace sDeptCoordination
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

        //ñ�ֽs��
        public string NoFixed()
        {
            return string.Format("DC{0:yyyy}", DateTime.Now.Date);
        }

        //�|ñ����ɶ�
        private void ucCoordinationCountersignBeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            var dt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
            ucDeptCoordinationCountersign.SetFieldValue("CountersignDate", dt);
        }

        private void ucCoordinationCountersignBeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            var dt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
            ucDeptCoordinationCountersign.SetFieldValue("CreateDate", dt);
        }

        //�[ñ���ListID�s�JDeptCoordinationMaster
        public object[] InserFlowID(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow rowparm = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            var _DeptCoordinationNO = rowparm["DeptCoordinationNO"].ToString(); //ñ�ֽs��
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
                string sql = "SELECT LISTID FROM View_SYS_TODOLIST_Petition WHERE FORM_KEYS='DeptCoordinationNO' AND FORM_TABLE='DeptCoordinationMaster' ";
                sql = sql + "AND FORM_PRESENTATION LIKE'%" + _DeptCoordinationNO + "%'";
                //sql = sql + "AND FORM_PRESENTATION ='" + _PetitionNOValue + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _listid = ds.Tables[0].Rows[0]["LISTID"].ToString();
                    var updatesql = "UPDATE DeptCoordinationMaster SET FlowListid='" + _listid + "'";
                    updatesql = updatesql + " WHERE DeptCoordinationNO='" + _DeptCoordinationNO + "'";

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

        //�D��ñ�ֳ̦h�ĤG��
        public object[] InserMangSignNum(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow rowparm = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            var _DeptCoordinationNO = rowparm["DeptCoordinationNO"].ToString(); //�p����s��            
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
                int _MangSignNum = 0;
                string sql = "SELECT MangSignNum FROM DeptCoordinationMaster WHERE DeptCoordinationNO='" + _DeptCoordinationNO + "' ";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                var _cnt = ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MangSignNum"].ToString()))
                    {
                        _MangSignNum = Convert.ToInt16(ds.Tables[0].Rows[0]["MangSignNum"]);
                        _MangSignNum += 1;
                    }
                    else
                        _MangSignNum = 1;
                }

                if (_MangSignNum <= 2)
                {
                    var updatesql = "UPDATE DeptCoordinationMaster SET MangSignNum='" + _MangSignNum + "'";
                    updatesql = updatesql + " WHERE DeptCoordinationNO='" + _DeptCoordinationNO + "'";

                    this.ExecuteSql(updatesql, connection, transaction);
                    transaction.Commit(); // �T�{���
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
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
                string sql = "SELECT Distinct LISTID FROM View_SYS_TODOLIST_Petition WHERE FORM_KEYS='DeptCoordinationNO' AND FORM_TABLE='DeptCoordinationMaster' ";
                sql = sql + "AND FORM_PRESENTATION LIKE'%" + _PetitionNO + "%'";
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
    }
}
