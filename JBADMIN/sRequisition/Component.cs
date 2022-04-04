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

namespace sRequisition
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
        public string GetRequisitFixed()
        {
            DateTime datetime = DateTime.Today;
            return "PAY"+((datetime.Year) - 1911).ToString().Trim();
        }

        private void ucRequisition_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucRequisition.SetFieldValue("CreateDate", DateTime.Now);
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
        public object PutFeeToShortTermMinusDetails(object[] objParam)
        {
            //���o�ϥΪ̩m�W
            object[] res = SrvUtils.GetValue("_username", this);
            string username = res[1].ToString();
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0];
            string RequisitionNO = dr["RequisitionNO"].ToString();
            int BillType = 2;  //�дڳ�
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                IDbTransaction trans = conn.BeginTransaction();
                string sql = "EXEC PutFeeToShortTermMinusDetails " + BillType + ",'" + RequisitionNO + "','" + username + "'";
                this.ExecuteSql(sql, conn, trans);
                trans.Commit();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return ret;

        }
        public object[] IsSignWithNotes(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string RequisitionNO = dr["RequisitionNO"].ToString();
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
                string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='RequisitionNO=''" + RequisitionNO + "'''";
                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                if (cnt == "0")
                    ret[1] = false;//�~��y�{
                else
                    ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            //ret[1] = true;
            return ret; // �Ǧ^��: �L
        }
        public object[] GetSignCount(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string RequisitionNO = parm[0];
                string sql = "SELECT COUNT(*) AS CNT FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='RequisitionNO=''" + RequisitionNO + "'''";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["CNT"].ToString();
                if (cnt == "0")
                    ret[1] = 0;
                else
                    ret[1] = 1;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
        }

        public object[] GetSignNotesData(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string RequisitionNO = parm[0];
                string sql = "SELECT S_STEP_ID,USERNAME,REMARK,UPDATEDATE FROM View_SYS_TODOHIS_REMARK WHERE FORM_PRESENTATION='RequisitionNO=''" + RequisitionNO + "''' ORDER BY UPDATEDATE ASC";
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
            //return ret;
            return new object[] { 0, js }; ;
        }
        //�ˬd�ϥΪ̬O�_�ݩ�ǤJ���s��
        public object[] CheckApplyEmpIsGroupID(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string ApplyEmpID = parm[0];
                string GroupID = parm[1];
                string sql = "Select TOP 1 dbo.funReturnEmpIsGroupID('" + ApplyEmpID + "','" + GroupID + "') AS STR  FROM EIPHRSYS.dbo.Users WHERE UserID='" + ApplyEmpID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                js = dsTemp.Tables[0].Rows[0]["STR"].ToString();
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
        //�P�B
        public object[] procSyncEmployeeVendor(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string UserID = parm[0].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC dbo.procSyncEmployeeVendor " +"'"+ UserID+"'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();

                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
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
            return new object[] { 0, true };
        }

        //======================================================�]�w�Ǧ^�ثe�����q�O�B�ǲ����e��T=========================================================================//
        //�]�w�Ǧ^�ثe�����q�O�B�ǲ����e��T
        public object[] getRequisitionVoucher(object[] objParam)
        {
            string RequisitionNO = (string)objParam[0];

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
                string sql = "exec procDisplayRequisitionVoucherD '" + RequisitionNO + "'" + "\r\n";
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

        //=====================  �N�дڳ��T�g�J�Ȧs�ǲ���  ===========================================================================================================//        
        public object[] InsertRequisitionVoucher(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string RequisitionNO = parm[0].ToString();
            string VoucherMVoucherDate = parm[1].ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "exec dbo.procInsertRequisitionVoucherD '" + RequisitionNO + "','" + VoucherMVoucherDate + "','" + userid + "','" + username + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit();

                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
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
            return new object[] { 0, true };
        }
        //=====================  glVoucher�s�W�e�g�J��T  ===========================================================================================================//        

        private void ucglVoucherMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherMaster.SetFieldValue("CreateDate", DateTime.Now);//�g�J������ɤ���
            //����ǲ�������~���g�J �ǲ��~��VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//�ǲ����
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());
        }

        private void ucglVoucherDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("Acno", ucglVoucherDetails.GetFieldCurrentValue("Acno").ToString().Trim());//�h�ť�
            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
            //�Y���U��2 => �hShow���B�[-��
            int BorrowLendType = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString());
            int AmtShow = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("AmtShow").ToString());
            if (BorrowLendType == 2)
            {
                ucglVoucherDetails.SetFieldValue("Amt", -AmtShow);
            }
            else ucglVoucherDetails.SetFieldValue("Amt", AmtShow);
        }

        private void ucglVoucherMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //����ǲ�������~���g�J �ǲ��~��VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//�ǲ����
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());
        }

        private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("Acno", ucglVoucherDetails.GetFieldCurrentValue("Acno").ToString().Trim());//�h�ť�
            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
            //�Y���U��2 => �hShow���B�[-��
            int BorrowLendType = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString());
            int AmtShow = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("AmtShow").ToString());
            if (BorrowLendType == 2)
            {
                ucglVoucherDetails.SetFieldValue("Amt", -AmtShow);
            }
            else ucglVoucherDetails.SetFieldValue("Amt", AmtShow);        
        }



    }
}
