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

namespace sglVoucherSet
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

        private void ucglVoucherSet_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucglVoucherSet.SetFieldValue("CreateDate", DateTime.Now);//�g�J������ɤ���
        }
        //�o��s�W�ɪ��w�]��
        //TypeID 1=> ���ɦ~�� ,2 => �~��~��        
        public object[] GetLockYM(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);    
            var TypeID = Convert.ToInt32(Parameter_Input["TypeID"]);

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
                string sql = "";
                string cnt = "";
                if (TypeID == 1)//���ɦ~��  => �Ĥ@����glVoucherMaster�̤j�~��,�����glVoucherLockYM �~�� Add �@�Ӥ�
                {
                    sql = sql + " select Left(convert(nvarchar(10),Isnull(Dateadd(month,1,MAX(LockYM)+'01'), " + "\r\n";
                    sql = sql + " (Select Min(VoucherDate) from glVoucherMaster where CompanyID=" + CompanyID + " )),112),6) as sData from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + "\r\n";

                    DataSet dsData = this.ExecuteSql(sql, connection, transaction);
                    cnt = dsData.Tables[0].Rows[0]["sData"].ToString();
                    transaction.Commit();

                }
                else
                {
                    //���o�w�ഫ���~�׳̤j��
                    sql = "select MAX(ConvertYear) as ConvertYear from glVoucherConvertYear where IsActive=1 and CompanyID=" + CompanyID;
                    DataSet dsConvertYear = this.ExecuteSql(sql, connection, transaction);
                    string ConvertYear = dsConvertYear.Tables[0].Rows[0]["ConvertYear"].ToString();

                    //�~��~��  =>   ������ɦ~�뤤�̤j����� (�ݬ�12)
                    sql = " select case Datepart(month,MAX(LockYM)+'01') when 12 then Left(MAX(LockYM),4) else '' end as sData from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + "\r\n";
                    DataSet dsData = this.ExecuteSql(sql, connection, transaction);
                    cnt = dsData.Tables[0].Rows[0]["sData"].ToString();

                    transaction.Commit();
                    dsConvertYear.Dispose();
                    dsData.Dispose();
                    //��ܦ~��~���w�g�s�b
                    if (ConvertYear == cnt)
                    {
                        cnt = "";
                    }

                }
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
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
        //=================================================================����==============================================================

        //�ק異��
        public object[] UpdateVoucherLockYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sCompanyID = parm[0]; //���q�O
            string sYM = parm[1];

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
                string sql = " update glVoucherLockYM set IsActive=0 where CompanyID=" + sCompanyID + " and LockYM='" + sYM + "'";
                ExecuteCommand(sql, connection, transaction);
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

        //�ˬd�Ҧ���Ƥ��i������
        public object[] CheckVoucherLockIsActive(object[] objParam)
        {
            string sCompanyID = (string)objParam[0];
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
                string sql = " select count(*) as iCount from glVoucherLockYM where IsActive=0 and CompanyID=" + sCompanyID;
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
        //����̫�@����Ƥ~�i�R��
        public object[] CheckVoucherLockYM(object[] objParam)
        {
            string sCompanyID = (string)objParam[0];
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
                string sql = " select MAX(LockYM) as MaxLockYM from glVoucherLockYM where IsActive=1 and CompanyID=" + sCompanyID;
                DataSet dsBelong = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = dsBelong.Tables[0].Rows[0]["MaxLockYM"].ToString();
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

        //���ɦ~��,�묰01��=>�ˬd�h�~�׬O�_�w�~��
        public object[] CheckAddglVoucherLockYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sYM = parm[0];
            string sCompanyID = parm[1];

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
                //���ݥh�~�׬O�_�����
                string sql = " select Isnull((select COUNT(*) from glVoucherLockYM where IsActive=1 and CompanyID=" + sCompanyID + " and Left(LockYM,4)=Datepart(year,DateAdd(month,-1,'" + sYM + "'+'01'))),0) as iCount";
                DataSet dsCount = this.ExecuteSql(sql, connection, transaction);
                int iCount = int.Parse(dsCount.Tables[0].Rows[0]["iCount"].ToString());
                if (iCount > 0)
                {
                    string sql2 = " select Isnull((select COUNT(*) from glVoucherConvertYear where IsActive=1 and CompanyID=" + sCompanyID + " and ConvertYear=Datepart(year,DateAdd(month,-1,'" + sYM + "'+'01'))),0) as cnt";
                    DataSet dsConvertYear = this.ExecuteSql(sql2, connection, transaction);
                    //// Indented�Y�� �N����ഫ��Json�榡
                    js = dsConvertYear.Tables[0].Rows[0]["cnt"].ToString();
                }
                else js = "1";
                transaction.Commit();
                dsCount.Dispose();
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
        //=================================================================�~��==============================================================
        //�~��@�~
        private void ucglVoucherConvertYear_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            //�p��3���|�p��ص����U�~��(��b�ᵲ��)
            //�j��3���|�p��ؤ�����,���쬰0(��b���k�s)
            int CompanyID = Convert.ToInt32(ucglVoucherConvertYear.GetFieldCurrentValue("CompanyID"));
            string ConvertYear = ucglVoucherConvertYear.GetFieldCurrentValue("ConvertYear").ToString();
            string UserID = ucglVoucherConvertYear.GetFieldCurrentValue("UserID").ToString();
            string CreateBy = ucglVoucherConvertYear.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucglVoucherConvertYear.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");

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
                string sql = " exec procInsertglVoucherMasterByYear " + CompanyID + ",'" + ConvertYear + "','" + UserID + "','" + CreateBy + "','" + CreateDate + "'";
                if (CompanyID == 4)//�ǫH
                {
                    sql = " exec procInsertglVoucherMasterByYear4 " + CompanyID + ",'" + ConvertYear + "','" + UserID + "','" + CreateBy + "','" + CreateDate + "'";
                }
                ExecuteCommand(sql, connection, transaction);
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

        private void ucglVoucherConvertYear_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //�p��3���|�p��ص����U�~��(��b�ᵲ��)
            //�j��3���|�p��ؤ�����,���쬰0(��b���k�s)
            int CompanyID = Convert.ToInt32(ucglVoucherConvertYear.GetFieldOldValue("CompanyID"));
            string ConvertYear = ucglVoucherConvertYear.GetFieldOldValue("ConvertYear").ToString();          

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
                string sql = " exec procDeleteglVoucherMasterByYear " + CompanyID + ",'" + ConvertYear + "'";
                ExecuteCommand(sql, connection, transaction);
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
        //�s�W�l�q�ǲ�
        private void ucglVoucherLockYM_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            int CompanyID = Convert.ToInt32(ucglVoucherLockYM.GetFieldCurrentValue("CompanyID"));//���q�O
            string UserID = ucglVoucherLockYM.GetFieldCurrentValue("UserID").ToString();
            string CreateBy = ucglVoucherLockYM.GetFieldCurrentValue("CreateBy").ToString();
            string VoucherDate = DateTime.Parse(ucglVoucherLockYM.GetFieldCurrentValue("VoucherDate").ToString()).ToShortDateString();//�ǲ����
            string YM = ucglVoucherLockYM.GetFieldCurrentValue("LockYM").ToString();//���ɦ~��

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
                string sY = YM.Substring(0, 4);//�~                        

                string sql = "select VoucherID from glVoucherMaster where CompanyID=" + CompanyID + " and VoucherYear='" + sY + "' group by VoucherID"+ "\r\n";
                DataSet dsVoucherID = this.ExecuteSql(sql, connection, transaction);

                string SQL = "";
                foreach (DataRow dr in dsVoucherID.Tables[0].Rows)
                {
                    string VoucherID = dr["VoucherID"].ToString();
                   
                   SQL = SQL+" exec procInsertglVoucherMasterByProfit " + CompanyID + ",'" + VoucherDate + "','" + YM + "','" +
                            VoucherID + "','" + UserID + "','" + CreateBy + "'" + "\r\n";
                    
                }
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
        //�ק�l�q�ǲ�
        private void ucglVoucherLockYM_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //�����ܦ��Ĥ~�ݧ��
            Boolean IsActive = Boolean.Parse(ucglVoucherLockYM.GetFieldCurrentValue("IsActive").ToString());
            if (IsActive == true)
            {
                int CompanyID = Convert.ToInt32(ucglVoucherLockYM.GetFieldCurrentValue("CompanyID"));//���q�O
                string UserID = ucglVoucherLockYM.GetFieldCurrentValue("UserID").ToString();
                string CreateBy = ucglVoucherLockYM.GetFieldCurrentValue("CreateBy").ToString();
                string VoucherDate = DateTime.Parse(ucglVoucherLockYM.GetFieldCurrentValue("VoucherDate").ToString()).ToShortDateString();//�ǲ����
                string YM = ucglVoucherLockYM.GetFieldCurrentValue("LockYM").ToString();//���ɦ~��

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
                    string sY = YM.Substring(0, 4);//�~                        

                    string sql = "select VoucherID from glVoucherMaster where CompanyID=" + CompanyID + " and VoucherYear='" + sY + "' group by VoucherID" + "\r\n";
                    DataSet dsVoucherID = this.ExecuteSql(sql, connection, transaction);

                    string SQL = "";
                    foreach (DataRow dr in dsVoucherID.Tables[0].Rows)
                    {
                        string VoucherID = dr["VoucherID"].ToString();

                        SQL = SQL + " exec procUpdateglVoucherMasterByProfit " + CompanyID + ",'" + VoucherDate + "','" + YM + "','" +
                                VoucherID + "','" + UserID + "','" + CreateBy + "'" + "\r\n";                       
                    }
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
        }
        //����glBanalce 
        public object[] procInsertglBalanceRepeat(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            int CompanyID = int.Parse(parm[0]);
            string YearMonth = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
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
                string SQL = "exec procInsertglBalanceRepeat " + CompanyID + ",'" + YearMonth + "','" + username+"'";
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
            return new object[] { 0, js };
        }


        //======================================================�]�w�Ǧ^�ثe�����q�O�B�ǲ����e��T=========================================================================//
        //�]�w�Ǧ^�ثe�����q�O�B�ǲ����e��T
        public object[] getPOMasterVoucher(object[] objParam)
        {
            string PONO = (string)objParam[0];

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
                string sql = "exec procDisplayPOMasterVoucherD '" + PONO + "'" + "\r\n";
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
        public object[] InsertPOMasterVoucher(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string PONO = parm[0].ToString();
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
                string sql = "exec dbo.procInsertPOMasterVoucherD '" + PONO + "','" + VoucherMVoucherDate + "','" + userid + "','" + username + "'";
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
