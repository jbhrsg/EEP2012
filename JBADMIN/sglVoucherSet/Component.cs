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
            ucglVoucherSet.SetFieldValue("CreateDate", DateTime.Now);//寫入日期的時分秒
        }
        //得到新增時的預設值
        //TypeID 1=> 鎖檔年月 ,2 => 年轉年度        
        public object[] GetLockYM(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);    
            var TypeID = Convert.ToInt32(Parameter_Input["TypeID"]);

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
                string sql = "";
                string cnt = "";
                if (TypeID == 1)//鎖檔年月  => 第一次抓glVoucherMaster最大年月,之後抓glVoucherLockYM 年月 Add 一個月
                {
                    sql = sql + " select Left(convert(nvarchar(10),Isnull(Dateadd(month,1,MAX(LockYM)+'01'), " + "\r\n";
                    sql = sql + " (Select Min(VoucherDate) from glVoucherMaster where CompanyID=" + CompanyID + " )),112),6) as sData from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + "\r\n";

                    DataSet dsData = this.ExecuteSql(sql, connection, transaction);
                    cnt = dsData.Tables[0].Rows[0]["sData"].ToString();
                    transaction.Commit();

                }
                else
                {
                    //取得已轉換的年度最大值
                    sql = "select MAX(ConvertYear) as ConvertYear from glVoucherConvertYear where IsActive=1 and CompanyID=" + CompanyID;
                    DataSet dsConvertYear = this.ExecuteSql(sql, connection, transaction);
                    string ConvertYear = dsConvertYear.Tables[0].Rows[0]["ConvertYear"].ToString();

                    //年轉年度  =>   抓取鎖檔年月中最大的月份 (需為12)
                    sql = " select case Datepart(month,MAX(LockYM)+'01') when 12 then Left(MAX(LockYM),4) else '' end as sData from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + "\r\n";
                    DataSet dsData = this.ExecuteSql(sql, connection, transaction);
                    cnt = dsData.Tables[0].Rows[0]["sData"].ToString();

                    transaction.Commit();
                    dsConvertYear.Dispose();
                    dsData.Dispose();
                    //表示年轉年份已經存在
                    if (ConvertYear == cnt)
                    {
                        cnt = "";
                    }

                }
                //Indented縮排 將資料轉換成Json格式
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
        //=================================================================鎖檔==============================================================

        //修改失效
        public object[] UpdateVoucherLockYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sCompanyID = parm[0]; //公司別
            string sYM = parm[1];

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

        //檢查所有資料不可為失效
        public object[] CheckVoucherLockIsActive(object[] objParam)
        {
            string sCompanyID = (string)objParam[0];
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
                string sql = " select count(*) as iCount from glVoucherLockYM where IsActive=0 and CompanyID=" + sCompanyID;
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
        //控制最後一筆資料才可刪除
        public object[] CheckVoucherLockYM(object[] objParam)
        {
            string sCompanyID = (string)objParam[0];
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
                string sql = " select MAX(LockYM) as MaxLockYM from glVoucherLockYM where IsActive=1 and CompanyID=" + sCompanyID;
                DataSet dsBelong = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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

        //鎖檔年月,月為01時=>檢查去年度是否已年轉
        public object[] CheckAddglVoucherLockYM(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sYM = parm[0];
            string sCompanyID = parm[1];

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
                //先看去年度是否有資料
                string sql = " select Isnull((select COUNT(*) from glVoucherLockYM where IsActive=1 and CompanyID=" + sCompanyID + " and Left(LockYM,4)=Datepart(year,DateAdd(month,-1,'" + sYM + "'+'01'))),0) as iCount";
                DataSet dsCount = this.ExecuteSql(sql, connection, transaction);
                int iCount = int.Parse(dsCount.Tables[0].Rows[0]["iCount"].ToString());
                if (iCount > 0)
                {
                    string sql2 = " select Isnull((select COUNT(*) from glVoucherConvertYear where IsActive=1 and CompanyID=" + sCompanyID + " and ConvertYear=Datepart(year,DateAdd(month,-1,'" + sYM + "'+'01'))),0) as cnt";
                    DataSet dsConvertYear = this.ExecuteSql(sql2, connection, transaction);
                    //// Indented縮排 將資料轉換成Json格式
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
        //=================================================================年轉==============================================================
        //年轉作業
        private void ucglVoucherConvertYear_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            //小於3的會計科目結轉到下年度(實帳戶結轉)
            //大於3的會計科目不結轉,期初為0(虛帳戶歸零)
            int CompanyID = Convert.ToInt32(ucglVoucherConvertYear.GetFieldCurrentValue("CompanyID"));
            string ConvertYear = ucglVoucherConvertYear.GetFieldCurrentValue("ConvertYear").ToString();
            string UserID = ucglVoucherConvertYear.GetFieldCurrentValue("UserID").ToString();
            string CreateBy = ucglVoucherConvertYear.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucglVoucherConvertYear.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");

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
                string sql = " exec procInsertglVoucherMasterByYear " + CompanyID + ",'" + ConvertYear + "','" + UserID + "','" + CreateBy + "','" + CreateDate + "'";
                if (CompanyID == 4)//傑信
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
            //小於3的會計科目結轉到下年度(實帳戶結轉)
            //大於3的會計科目不結轉,期初為0(虛帳戶歸零)
            int CompanyID = Convert.ToInt32(ucglVoucherConvertYear.GetFieldOldValue("CompanyID"));
            string ConvertYear = ucglVoucherConvertYear.GetFieldOldValue("ConvertYear").ToString();          

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
        //新增損益傳票
        private void ucglVoucherLockYM_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            int CompanyID = Convert.ToInt32(ucglVoucherLockYM.GetFieldCurrentValue("CompanyID"));//公司別
            string UserID = ucglVoucherLockYM.GetFieldCurrentValue("UserID").ToString();
            string CreateBy = ucglVoucherLockYM.GetFieldCurrentValue("CreateBy").ToString();
            string VoucherDate = DateTime.Parse(ucglVoucherLockYM.GetFieldCurrentValue("VoucherDate").ToString()).ToShortDateString();//傳票日期
            string YM = ucglVoucherLockYM.GetFieldCurrentValue("LockYM").ToString();//鎖檔年月

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
                string sY = YM.Substring(0, 4);//年                        

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
        //修改損益傳票
        private void ucglVoucherLockYM_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //失效變有效才需更改
            Boolean IsActive = Boolean.Parse(ucglVoucherLockYM.GetFieldCurrentValue("IsActive").ToString());
            if (IsActive == true)
            {
                int CompanyID = Convert.ToInt32(ucglVoucherLockYM.GetFieldCurrentValue("CompanyID"));//公司別
                string UserID = ucglVoucherLockYM.GetFieldCurrentValue("UserID").ToString();
                string CreateBy = ucglVoucherLockYM.GetFieldCurrentValue("CreateBy").ToString();
                string VoucherDate = DateTime.Parse(ucglVoucherLockYM.GetFieldCurrentValue("VoucherDate").ToString()).ToShortDateString();//傳票日期
                string YM = ucglVoucherLockYM.GetFieldCurrentValue("LockYM").ToString();//鎖檔年月

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
                    string sY = YM.Substring(0, 4);//年                        

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
        //重算glBanalce 
        public object[] procInsertglBalanceRepeat(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            int CompanyID = int.Parse(parm[0]);
            string YearMonth = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());
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


        //======================================================設定傳回目前的公司別、傳票內容資訊=========================================================================//
        //設定傳回目前的公司別、傳票內容資訊
        public object[] getPOMasterVoucher(object[] objParam)
        {
            string PONO = (string)objParam[0];

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

        //=====================  將請款單資訊寫入暫存傳票中  ===========================================================================================================//        
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
                //Indented縮排 將資料轉換成Json格式
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
        //=====================  glVoucher新增前寫入資訊  ===========================================================================================================//        

        private void ucglVoucherMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherMaster.SetFieldValue("CreateDate", DateTime.Now);//寫入日期的時分秒
            //抓取傳票日期的年份寫入 傳票年份VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//傳票日期
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());
        }

        private void ucglVoucherDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("Acno", ucglVoucherDetails.GetFieldCurrentValue("Acno").ToString().Trim());//去空白
            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//寫入日期的時分秒
            //若為貸方2 => 則Show金額加-號
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
            //抓取傳票日期的年份寫入 傳票年分VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//傳票日期
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());
        }

        private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("Acno", ucglVoucherDetails.GetFieldCurrentValue("Acno").ToString().Trim());//去空白
            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//寫入日期的時分秒
            //若為貸方2 => 則Show金額加-號
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
