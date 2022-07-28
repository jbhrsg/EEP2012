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
namespace sglVoucherMasterWin
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
        //傳票編號=> ex: A1050108010--A稅務,B會計+民國年+月+日+3碼流水號=>A1051025006
        public string VoucherNoFixed()
        {            
            //======================================求得傳票編號開頭======================================
            string CompanyID = ucglVoucherMaster.GetFieldCurrentValue("CompanyID").ToString();

            string VoucherNoTitle = "A";
            //VoucherID=>1 A , 2 A ,3 B
            int VoucherID = int.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherID").ToString());
            if (VoucherID == 3)
            {
                VoucherNoTitle = "B";
            }

            DateTime VoucherDate = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString());
            return CompanyID + VoucherNoTitle + (VoucherDate.Year - 1911).ToString().Trim() + VoucherDate.Month.ToString().PadLeft(2, '0') + VoucherDate.Day.ToString().PadLeft(2, '0');

        }

        //=====================  glVoucher新增前寫入資訊  ===========================================================================================================//        
        private void ucglVoucherMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            ucglVoucherMaster.SetFieldValue("CompanyID", ucglVoucherMaster.GetFieldCurrentValue("CompanyID").ToString().Trim());
            ucglVoucherMaster.SetFieldValue("VoucherID", ucglVoucherMaster.GetFieldCurrentValue("VoucherID").ToString().Trim());
            ucglVoucherMaster.SetFieldValue("OftenUsedAcno", "");
            ucglVoucherMaster.SetFieldValue("UserID", userid);
            ucglVoucherMaster.SetFieldValue("CreateBy", CreateBy);

            ucglVoucherMaster.SetFieldValue("CreateDate", DateTime.Now);//寫入日期的時分秒
            //抓取傳票日期的年份寫入 傳票年份VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//傳票日期
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());

            //取得傳票編號=> 去除左邊第一碼 當顯示傳票編號
            string VoucherNo = ucglVoucherMaster.GetFieldCurrentValue("VoucherNo").ToString().Trim();
            //ucglVoucherMaster.SetFieldValue("VoucherNo", VoucherNo);

            ucglVoucherMaster.SetFieldValue("VoucherNoShow", VoucherNo.Remove(0, 1));
        }

        private void ucglVoucherDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("CompanyID", ucglVoucherMaster.GetFieldCurrentValue("CompanyID").ToString().Trim());
            ucglVoucherDetails.SetFieldValue("VoucherID", ucglVoucherMaster.GetFieldCurrentValue("VoucherID").ToString().Trim());

            //DescribeID
            if (ucglVoucherDetails.GetFieldCurrentValue("DescribeID") != DBNull.Value)
            {
                ucglVoucherDetails.SetFieldValue("DescribeID", ucglVoucherDetails.GetFieldCurrentValue("DescribeID").ToString().Trim());
            }
            else ucglVoucherDetails.SetFieldValue("DescribeID", "");

            //CostCenterID
            if (ucglVoucherDetails.GetFieldCurrentValue("CostCenterID") != DBNull.Value)
            {
                ucglVoucherDetails.SetFieldValue("CostCenterID", ucglVoucherDetails.GetFieldCurrentValue("CostCenterID").ToString().Trim());
            }
            else ucglVoucherDetails.SetFieldValue("CostCenterID", "");

            var AcnoSubAcno = ucglVoucherDetails.GetFieldCurrentValue("AcnoSubAcno").ToString().Trim();
            ucglVoucherDetails.SetFieldValue("Acno", AcnoSubAcno.Substring(0,4));
            if (AcnoSubAcno.Length > 4)
            {
                ucglVoucherDetails.SetFieldValue("SubAcno", AcnoSubAcno.Substring(4, AcnoSubAcno.Length-4));
            }
            else ucglVoucherDetails.SetFieldValue("SubAcno", "");
            ucglVoucherDetails.SetFieldValue("CreateDate", DateTime.Now);//寫入日期的時分秒

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

        //==================================================InsertglBalance==============================================================================//
        ////寫glBalance總帳 =>  VoucherNo + Item       
        private void InsertglBalance()
        {
            int CompanyID = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("CompanyID").ToString());
            string VoucherNo = ucglVoucherDetails.GetFieldCurrentValue("VoucherNo").ToString();
            string Item = ucglVoucherDetails.GetFieldCurrentValue("Item").ToString();

            string BorrowLendType = ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString();//1借,2貸(新)

            string CreateBy = ucglVoucherDetails.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucglVoucherDetails.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");

            string sql = "";
            //----關聯後 Detail主鍵 AutoKey => 有總帳資料=>加上金額,無資料則新增
            //有對應 => 修改----------------------------------------------------------------------------------------------------------------------
            if (BorrowLendType == "1")
            {
                sql = sql + " update glBalance set glBalance.BorrowAmt=b.BorrowAmt" + "\r\n";
            }
            else sql = sql + " update glBalance set glBalance.LendAmt=b.LendAmt" + "\r\n";

            sql = sql + "+(Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and Item=g.Item), " + "\r\n";
            sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v " + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where  g.VoucherNo='" + VoucherNo + "' and g.Item=" + Item + "\r\n";//--(修改glBalance總帳)
            //無對應 => 新增----------------------------------------------------------------------------------------------------------------------
            sql = sql + " Insert into glBalance(CompanyID,BalanceYear,VoucherType,Acno,SubAcno,CostCenterID,iMonth,BorrowAmt,LendAmt,BudgetAmt,CreateBy,CreateDate,LastUpdateBy,LastUpdateDate)" + "\r\n";
            sql = sql + " select v.CompanyID,v.VoucherYear,u.VoucherType,g.Acno,g.SubAcno,g.CostCenterID,month(v.VoucherDate)," + "\r\n";
            if (BorrowLendType == "1")
            {
                sql = sql + " g.Amt,0,0,'" + @CreateBy + "','" + @CreateDate + "','" + @CreateBy + "','" + @CreateDate + "'" + "\r\n";
            }
            else sql = sql + " 0,g.Amt,0,'" + @CreateBy + "','" + @CreateDate + "','" + @CreateBy + "','" + @CreateDate + "'" + "\r\n";

            sql = sql + " from glVoucherMaster v" + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo" + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " left join glBalance b on v.CompanyID=b.CompanyID  and v.VoucherYear=b.BalanceYear" + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID" + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' and g.Item=" + Item + " and b.AutoKey is null" + "\r\n";//--(無glBalance總帳資料)

            //刪除glBalance=> 修改後借貸金額都為0的總帳資料 --------------------------------------------------------------------------------------------
            sql = sql + " Delete from glBalance where BorrowAmt=0 and LendAmt=0" + "\r\n";

            this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherMaster.trans);
        }

        //==================================================UpdateglBalance==============================================================================//

        private void ucglVoucherMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //抓取傳票日期的年份寫入 傳票年分VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//傳票日期
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());
        }
        //======================================================DeleteglBalance==========================================================================//
        ////刪除glBalance總帳     
        private void DeleteglBalance()
        {
            //表示有明細檔
            if (ucglVoucherDetails.GetFieldOldValue("AutoKey") != null)
            {
                string AutoKey = ucglVoucherDetails.GetFieldOldValue("AutoKey").ToString();//Detail主鍵
                int VoucherID = int.Parse(ucglVoucherDetails.GetFieldOldValue("VoucherID").ToString());//傳票類別 =>原始資料
                string BorrowLendType = ucglVoucherDetails.GetFieldOldValue("BorrowLendType").ToString();//1借,2貸 =>原始資料

                string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string CreateBy = SrvGL.GetUserName(userid.ToLower());

                string sql = "";

                //--------關聯後 Detail主鍵 AutoKey => 金額減掉(修改glBalance總帳)
                if (BorrowLendType == "1")
                {
                    sql = sql + " update glBalance set glBalance.BorrowAmt=b.BorrowAmt" + "\r\n";
                }
                else sql = sql + " update glBalance set glBalance.LendAmt=b.LendAmt" + "\r\n";

                sql = sql + "-(Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and Item=g.Item), " + "\r\n";
                sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
                sql = sql + " from glVoucherMaster v " + "\r\n";
                sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
                sql = sql + " left join glVoucherTypeUnion u on u.VoucherID=" + VoucherID + "\r\n";
                sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
                sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
                sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
                sql = sql + " where  g.AutoKey=" + AutoKey + "\r\n";

                this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);
            }

        }
        //=====================================================Detail Insert===========================================================================//

        //1對多=>先新增Master,之後Detail=>新增glBalance 寫在 傳票明細新增後
        //傳票明細新增後=>新增glBalance
        private void ucglVoucherDetails_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            InsertglBalance();
        }
        //=====================================================Detail Modify===========================================================================//
        //Detail修改前=>修改金額      
        private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //DescribeID
            if (ucglVoucherDetails.GetFieldCurrentValue("DescribeID") != DBNull.Value)
            {
                ucglVoucherDetails.SetFieldValue("DescribeID", ucglVoucherDetails.GetFieldCurrentValue("DescribeID").ToString().Trim());
            }
            else ucglVoucherDetails.SetFieldValue("DescribeID", "");

            //CostCenterID
            if (ucglVoucherDetails.GetFieldCurrentValue("CostCenterID") != DBNull.Value)
            {
                ucglVoucherDetails.SetFieldValue("CostCenterID", ucglVoucherDetails.GetFieldCurrentValue("CostCenterID").ToString().Trim());
            }
            else ucglVoucherDetails.SetFieldValue("CostCenterID", "");

            var AcnoSubAcno = ucglVoucherDetails.GetFieldCurrentValue("AcnoSubAcno").ToString().Trim();
            ucglVoucherDetails.SetFieldValue("Acno", AcnoSubAcno.Substring(0, 4));
            if (AcnoSubAcno.Length > 4)
            {
                ucglVoucherDetails.SetFieldValue("SubAcno", AcnoSubAcno.Substring(4, AcnoSubAcno.Length - 4));
            }

            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//寫入日期的時分秒
            //若為貸方2 => 則Show金額加-號
            int BorrowLendType = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString());
            int AmtShow = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("AmtShow").ToString());
            if (BorrowLendType == 2)
            {
                ucglVoucherDetails.SetFieldValue("Amt", -AmtShow);
            }
            else ucglVoucherDetails.SetFieldValue("Amt", AmtShow);

            DeleteglBalance();
        }
        //Detail修改後=>新增
        private void ucglVoucherDetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            InsertglBalance();
        }

        //======================================================Detail Delete=========================================================================//
        //刪除前      
        private void ucglVoucherDetails_BeforeDelete(object sender, UpdateComponentBeforeDeleteEventArgs e)
        {
            string AutoKey = ucglVoucherDetails.GetFieldOldValue("AutoKey").ToString();
            string VoucherNo = ucglVoucherDetails.GetFieldOldValue("VoucherNo").ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string sql = " exec procInsertglVoucherDetailsDel " + AutoKey + ",'" + VoucherNo + "','" + CreateBy + "'" + "\r\n";
            this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);

            DeleteglBalance();
        }

        //======================================================Master Update=========================================================================//
        //主檔資料修正 (傳票日期修正,可能月份不同) 1.前=>修改(減金額) 2.後=>新增(已有項目=>加金額),無對應 => 新增        
        private void ucglVoucherMaster_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            ReduceBalance();
        }
        //======================================================ReduceBalance==========================================================================//
        ////修改glBalance總帳 =>  (公司別、傳票類別修正)
        private void ReduceBalance()
        {
            string VoucherNo = ucglVoucherMaster.GetFieldCurrentValue("VoucherNo").ToString();
            int VoucherID = int.Parse(ucglVoucherMaster.GetFieldOldValue("VoucherID").ToString());//傳票類別 =>原始資料
            int NewVoucherID = int.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherID").ToString());//傳票類別 =>新資料

            int CompanyID = int.Parse(ucglVoucherMaster.GetFieldOldValue("CompanyID").ToString());//公司別 =>原始資料

            DateTime OldVoucherDate = DateTime.Parse(ucglVoucherMaster.GetFieldOldValue("VoucherDate").ToString());
            string iMonth = OldVoucherDate.Month.ToString().Trim();

            string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());
            //原資料 => 修改(減金額)----------------------------------------------------------------------------------------------------------------------          
            string sql = "update glBalance set glBalance.BorrowAmt=b.BorrowAmt - isnull( (select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=1 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LendAmt=b.LendAmt - isnull( ( select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=2 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v " + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on u.VoucherID=" + VoucherID + "\r\n";
            sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
            sql = sql + " and  b.iMonth=" + iMonth + " and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' " + "\r\n";

            //公司別、傳票類別有變更=>修改glVoucherDetails資料
            if (VoucherID != NewVoucherID)
            {
                sql = sql + " update glVoucherDetails set VoucherID=" + NewVoucherID + ",CompanyID=" + CompanyID + "\r\n";
                sql = sql + " from glVoucherMaster v inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
                sql = sql + " where g.VoucherNo='" + VoucherNo + "' " + "\r\n";
            }

            //刪除glBalance=> 修改後借貸金額都為0的總帳資料 --------------------------------------------------------------------------------------------
            sql = sql + " Delete from glBalance where BorrowAmt=0 and LendAmt=0" + "\r\n";

            //新資料
            //已有項目=>加金額----------------------------------------------------------------------------------------------------------------------          
            sql = sql + "update glBalance set glBalance.BorrowAmt=b.BorrowAmt + isnull( (select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=1 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LendAmt=b.LendAmt + isnull( ( select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=2 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v " + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' " + "\r\n";

            //無對應 => 新增----------------------------------------------------------------------------------------------------------------------
            sql = sql + " Insert into glBalance(CompanyID,BalanceYear,VoucherType,Acno,SubAcno,CostCenterID,iMonth,BorrowAmt,LendAmt,BudgetAmt,CreateBy,CreateDate,LastUpdateBy,LastUpdateDate)" + "\r\n";
            sql = sql + " select v.CompanyID,v.VoucherYear,u.VoucherType,g.Acno,g.SubAcno,g.CostCenterID,month(v.VoucherDate)," + "\r\n";
            sql = sql + " isnull((Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=1 and Item=g.Item),0)," + "\r\n";
            sql = sql + " isnull((Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=2 and Item=g.Item),0),0,'" + @CreateBy + "','" + @CreateDate + "','" + @CreateBy + "','" + @CreateDate + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v" + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo" + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " left join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear" + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID" + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' and b.AutoKey is null" + "\r\n";//--(無glBalance總帳資料)

            this.ExecuteCommand(sql, ucglVoucherMaster.conn, ucglVoucherMaster.trans);
        }
        //======================================================Grid Master刪除檢查=========================================================================//
        //1.是否已鎖檔
        public object[] CheckDeleteglVoucherMaster(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            string VoucherDate = DateTime.Parse(Parameter_Input["Voucher_Date"].ToString()).ToShortDateString();
            int CompanyID = int.Parse(Parameter_Input["Company_ID"].ToString());

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
                string sql = " select count(*) as cnt from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + " and LockYM=Left(CONVERT(nvarchar(10),Cast('" + VoucherDate + "' as datetime),112),6)";
                DataSet dsglVoucherDetails = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsglVoucherDetails.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
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
        //2.是否最後一筆才可以刪除
        public object[] CheckDeleteglVoucherMaster2(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            string VoucherNo = Parameter_Input["Voucher_No"].ToString();

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
                string sql = " select COUNT(*) as cnt from SYSAUTONUM where AUTOID='VoucherNo' and FIXED+RIGHT(REPLICATE('0',3)+CAST(CURRNUM as NVARCHAR),3)='" + VoucherNo + "'";
                DataSet dsglVoucherDetails = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsglVoucherDetails.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
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
        private void ucglVoucherMaster_BeforeDelete(object sender, UpdateComponentBeforeDeleteEventArgs e)
        {
            string VoucherNo = ucglVoucherMaster.GetFieldOldValue("VoucherNo").ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string sql = " exec procInsertglVoucherMasterDel '" + VoucherNo + "','" + CreateBy + "'" + "\r\n";
            this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);

            DeleteglBalance();
        }
        private void ucglVoucherMaster_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            ////是否最後一筆
            //string sql = " select COUNT(*) as cnt from SYSAUTONUM where AUTOID='VoucherNo' and FIXED+RIGHT(REPLICATE('0',3)+CAST(CURRNUM as NVARCHAR),3)='" + VoucherNo + "'";
            //DataSet dsglVoucherDetails = this.ExecuteSql(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);
            //string cnt = dsglVoucherDetails.Tables[0].Rows[0]["cnt"].ToString();

            //if (cnt == "1")
            //{
            //    //修改 SYSAUTONUM 編號
            //    string sql2 = " update SYSAUTONUM set CURRNUM=CURRNUM-1 from SYSAUTONUM where AUTOID='VoucherNo' and FIXED=LEFT('" + VoucherNo + "',9)" + "\r\n";

            //    this.ExecuteCommand(sql2, ucglVoucherDetails.conn, ucglVoucherDetails.trans);
            //}          
        }
        //======================================================設定傳回目前的公司別、傳票類別=========================================================================//
        //設定傳回目前的公司別、傳票類別
        public object[] getglVoucherSet(object[] objParam)
        {
            string UserID = (string)objParam[0];

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
                string sql = " select CompanyID,VoucherID from glVoucherSet where UserID='" + UserID + "'" + "\r\n";
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

        //======================================================摘碼代號 => 得到內容=========================================================================//
        public object[] GetDescribeText(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');

            int CompanyID = int.Parse(parm[0].ToString());
            string DetailAcno = (string)parm[1];
            string DescribeID = (string)parm[2];
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
                string sql = " select Describe from glDescribe where CompanyID=" + CompanyID + " and Acno='" + DetailAcno + "' and DescribeID='" + DescribeID + "'" + "\r\n";
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

        //====================================================== 明細代號 => 得到內容 (顯示在Grid中)=========================================================================//
        public object[] GetAcnoNameText(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');

            int CompanyID = int.Parse(parm[0].ToString());
            string Acno = (string)parm[1];
            string SubAcno = (string)parm[2];
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
                string sql = " select AcnoName from glAccountItem where CompanyID=" + CompanyID + " and Acno='" + Acno + "' and SubAcno='" + SubAcno + "'" + "\r\n";
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
