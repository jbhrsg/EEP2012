using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Data.SqlClient;


namespace sERP_Normal_Warrant
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
        //轉入未沖銷之發票明細資料
        public object[] SelectInvoiceDetails(object[] objParam)
        {
            string js = string.Empty;
            string[] param = objParam[0].ToString().Split(',');
            string companyCustomerID = param[0];//應收公司客戶
            string insGroupID = param[1];//公司別
            string offsetTypeID = param[2];//沖銷對向
            string salesDateB = param[3].Trim();
            string salesDateE = param[4].Trim();
            //string cashNOItemNO = param[3].Trim();//現金匯款單號
            //string cashNO = "";
            //string ItemNO = "";
            //if (cashNOItemNO != "")
            //{
                //cashNO = cashNOItemNO.Substring(0, cashNOItemNO.Length);
                //ItemNO = cashNOItemNO.Substring(cashNOItemNO.Length - 3);
            //}

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //取得datatable
                string sql = "select id.InvoiceNO,id.SalesDate,id.ARDate,id.SalesTotal,t.InvoiceTypeName," + "\r\n";
                sql = sql + "(select ISNULL(SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0)),0) " + "\r\n";
                sql = sql + "from WarrantDetails d1 where d1.InvoiceNO=id.InvoiceNO) as AcceptedAmount," + "\r\n";
                sql = sql + "id.CustomerID,id.Employer,id.InvoiceDate" + "\r\n";
                sql = sql + "from InvoiceDetails id" + "\r\n";
                sql = sql + "left join InvoiceType t on t.InvoiceTypeID=id.QInvoiceType" + "\r\n";
                //sql = sql + "left join WarrantDetails d on d.InvoiceNO=id.InvoiceNO" + "\r\n";
                //sql = sql + "left join WarrantMaster m on m.WarrantNO=d.WarrantNO" + "\r\n";
                sql = sql + "where id.IsActive=1" + "\r\n";
                if (offsetTypeID == "1")
                {//沖銷對象1公司
                    //if (cashNOItemNO != "")//有現金單號
                    //{
                    //    string sql0 = "select CustomerID from JBADMIN.dbo.CashTakeBackDetails where CashTakeBackNO='" + cashNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                    //    DataTable tb = this.ExecuteSql(sql0, connection, transaction).Tables[0];
                    //    if (tb.Rows.Count > 0)//現金單號有CustomerID
                    //        sql = sql + "and id.CustomerID in (select CustomerID from JBADMIN.dbo.CashTakeBackDetails where CashTakeBackNO='" + cashNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                    //    else//現金單號沒CustomerID
                    //        sql = sql + "and id.CustomerID='" + companyCustomerID + "'" + "\r\n";
                    //}
                    //else//沒現金單號
                        sql = sql + "and id.CustomerID='" + companyCustomerID + "'" + "\r\n";
                }
                else if (offsetTypeID == "2")
                {//沖銷對象2個人
                    //if (cashNOItemNO != "")//有現金單號
                    //{
                    //    string sql0 = "select CustomerID from JBADMIN.dbo.CashTakeBackDetails where CashTakeBackNO='" + cashNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                    //    DataTable tb = this.ExecuteSql(sql0, connection, transaction).Tables[0];
                    //    if (tb.Rows.Count > 0)//現金單號有CustomerID
                    //    {
                    //        if (tb.Rows[0]["CustomerID"].ToString().Substring(0, 1) == "C")//這CustomerID公司底下的員工
                    //        {
                    //            sql = sql + "and id.Employer in (select CustomerID from JBADMIN.dbo.CashTakeBackDetails where CashTakeBackNO='" + cashNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                    //        }
                    //        else//這CustomerID員工
                    //        {
                    //            sql = sql + "and id.CustomerID in (select CustomerID from JBADMIN.dbo.CashTakeBackDetails where CashTakeBackNO='" + cashNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                    //        }
                    //    }
                    //    else//現金單號沒CustomerID
                    //        sql = sql + "and id.Employer='" + companyCustomerID + "'" + "\r\n";
                    //}
                    //else//沒現金單號
                        sql = sql + "and id.Employer='" + companyCustomerID + "'" + "\r\n";
                }
                sql = sql + "and id.insGroupID=" + insGroupID + "\r\n";
                if(salesDateB !=""){
                sql = sql + "and id.SalesDate >='" + salesDateB +"'"+ "\r\n";
                }
                if (salesDateB != "")
                {
                    sql = sql + "and id.SalesDate <='" + salesDateE + "'" + "\r\n";
                }
                sql = sql + "and id.SalesTotal!=ISNULL((select SUM(ISNULL(d1.RecAmount,0)+ISNULL(d1.OthAmount,0)+ISNULL(d1.RebAmount,0)+ISNULL(d1.RetAmount,0)+ISNULL(d1.BadAmount,0))" + "\r\n";
                sql = sql + "from WarrantDetails d1" + "\r\n";
                sql = sql + "where d1.InvoiceNO=id.InvoiceNO),0) " + "\r\n";
                sql = sql + "and id.InvoiceNO not in(select InvoiceNO from InvoiceVoidApply where Flowflag in('N','P'))" + "\r\n";
                sql = sql + "order by id.SalesDate" + "\r\n";
                DataTable tbInvoiceDetails = this.ExecuteSql(sql, connection, transaction).Tables[0];
                //轉成js格式string
                js = JsonConvert.SerializeObject(tbInvoiceDetails, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] {0,js };
        }

        //收款單號字首
        //public string autoNumber1_GetFixed(){
        //    string Prefix = string.Empty;
        //    Prefix = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00");
        //    return Prefix;
        //}

        //取得收款單號
        public object[] GetWarrantNO(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string WarrantDate = aParam[0];
            string js = string.Empty;
            WarrantDate = DateTime.Parse(WarrantDate).ToString("yyyy-MM-dd");//Convert.ToDateTime
            string WarrantYM = WarrantDate.Substring(0, 7);
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //取收款年月，最大收款單號
                string sql = "select top 1 * from WarrantMaster where substring(convert(nvarchar(20),WarrantDate,120),1,7)='" + WarrantYM + "' order by WarrantNO desc";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                string WarrantNO = string.Empty;
                WarrantNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["WarrantNO"].ToString();
                if (WarrantNO != "")//有
                {
                    int num = Convert.ToInt32(WarrantNO.Substring(6, 5));
                    WarrantNO = WarrantYM.Replace("-", "") + ((num + 1).ToString("00000")); ;
                }
                else//沒有
                {
                    WarrantNO = WarrantYM.Replace("-", "") + "00001";
                }
                js = Newtonsoft.Json.JsonConvert.SerializeObject(WarrantNO, Newtonsoft.Json.Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        
        public object[] CheckCashNODuplicate(object[] objParam)
        {
            string js = string.Empty;
            string[] param = objParam[0].ToString().Split(',');
            string CashNO = param[0];
            string ItemNO = param[1];

            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //string sql = "select WarrantNO from WarrantMaster where REPLACE(CashNO, NCHAR(9), N'')=rtrim(ltrim('" + CashNO + "')) and rtrim(ltrim(ItemNO))=rtrim(ltrim('" + ItemNO + "'))" + "\r\n";

                string sql = "select WarrantNO from WarrantMaster where REPLACE(REPLACE(REPLACE(REPLACE(CashNO, NCHAR(9), N''), NCHAR(10), N''), NCHAR(13), N''), N' ', N'')=REPLACE(REPLACE(REPLACE(REPLACE('" + CashNO + "', NCHAR(9), N''), NCHAR(10), N''), NCHAR(13), N''), N' ', N'') and REPLACE(REPLACE(REPLACE(REPLACE(ItemNO, NCHAR(9), N''), NCHAR(10), N''), NCHAR(13), N''), N' ', N'')=REPLACE(REPLACE(REPLACE(REPLACE('" + ItemNO + "', NCHAR(9), N''), NCHAR(10), N''), NCHAR(13), N''), N' ', N'')" + "\r\n";
                DataTable tbInvoiceDetails = this.ExecuteSql(sql, connection, transaction).Tables[0];
                //轉成js格式string
                js = JsonConvert.SerializeObject(tbInvoiceDetails, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }


        public object[] SelectBourseBankID(object[] objParam) {
            string js = string.Empty;
            string[] param = objParam[0].ToString().Split(',');
            string BankRootID = param[0];
            string BankBranchID = param[1];
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select Bourse,BankID from Bank where [BankNO]='" + BankRootID + "' and [BankBranchNO]='" + BankBranchID + "'" + "\r\n";
                DataTable tbBank = this.ExecuteSql(sql, connection, transaction).Tables[0];
                //轉成js格式string
                js = JsonConvert.SerializeObject(tbBank, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }

        public void ucWarrantDetails_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            //若收款金額為0或空白，則刪除
            string WarrantNO = ucWarrantDetails.GetFieldCurrentValue("WarrantNO").ToString();
            string ItemNO = ucWarrantDetails.GetFieldCurrentValue("ItemNO").ToString();
            string RecAmount = ucWarrantDetails.GetFieldCurrentValue("RecAmount").ToString().Trim();//收款金額 其他金額 折讓金額 退貨金額 呆帳金額
            string OthAmount = ucWarrantDetails.GetFieldCurrentValue("OthAmount").ToString().Trim();
            string RebAmount = ucWarrantDetails.GetFieldCurrentValue("RebAmount").ToString().Trim();
            string RetAmount = ucWarrantDetails.GetFieldCurrentValue("RetAmount").ToString().Trim();
            string BadAmount = ucWarrantDetails.GetFieldCurrentValue("BadAmount").ToString().Trim();
            if ((RecAmount == "0" || RecAmount == "") && (OthAmount == "0" || OthAmount == "")  && (RebAmount == "0" || RebAmount == "") && (RetAmount == "0" || RetAmount == "") && (BadAmount == "0" || BadAmount == ""))
            {
                string sql = "delete from [WarrantDetails] where WarrantNO='" + WarrantNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                this.ExecuteCommand(sql, ucWarrantDetails.conn, ucWarrantDetails.trans);
            }
        }

        private void ucWarrantDetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //若收款金額為0或空白，則刪除
            string WarrantNO = ucWarrantDetails.GetFieldCurrentValue("WarrantNO").ToString();
            string ItemNO = ucWarrantDetails.GetFieldCurrentValue("ItemNO").ToString();
            string RecAmount = ucWarrantDetails.GetFieldCurrentValue("RecAmount").ToString().Trim();//收款金額 其他金額 折讓金額 退貨金額 呆帳金額
            string OthAmount = ucWarrantDetails.GetFieldCurrentValue("OthAmount").ToString().Trim();
            string RebAmount = ucWarrantDetails.GetFieldCurrentValue("RebAmount").ToString().Trim();
            string RetAmount = ucWarrantDetails.GetFieldCurrentValue("RetAmount").ToString().Trim();
            string BadAmount = ucWarrantDetails.GetFieldCurrentValue("BadAmount").ToString().Trim();
            if ((RecAmount == "0" || RecAmount == "") && (OthAmount == "0" || OthAmount == "") && (RebAmount == "0" || RebAmount == "") && (RetAmount == "0" || RetAmount == "") && (BadAmount == "0" || BadAmount == ""))
            {
                string sql = "delete from [WarrantDetails] where WarrantNO='" + WarrantNO + "' and ItemNO='" + ItemNO + "'" + "\r\n";
                this.ExecuteCommand(sql, ucWarrantDetails.conn, ucWarrantDetails.trans);
            }
        }

        //把主檔資料寫到到支票明細
        private void ucCheckDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            string CustomerID = ucWarrantMaster.GetFieldCurrentValue("CompanyCustomerID").ToString();
            string InsGroupID = ucWarrantMaster.GetFieldCurrentValue("InsGroupID").ToString();
            string WarrantDate = ucWarrantMaster.GetFieldCurrentValue("WarrantDate").ToString();
            ucCheckDetails.SetFieldValue("CustomerID", CustomerID);
            ucCheckDetails.SetFieldValue("InsGroupID", InsGroupID);
            ucCheckDetails.SetFieldValue("WarrantDate", WarrantDate);
        }

        //把主檔資料更新到支票明細
        private void ucWarrantMaster_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string CustomerID = ucWarrantMaster.GetFieldCurrentValue("CompanyCustomerID").ToString();
            string WarrantDate = Convert.ToDateTime(ucWarrantMaster.GetFieldCurrentValue("WarrantDate")).ToString("yyyy/MM/dd 00:00:00");
            string WarrantNO = ucWarrantMaster.GetFieldCurrentValue("WarrantNO").ToString();

            string sql = "update CheckDetails set WarrantDate='" + WarrantDate + "',CustomerID='" + CustomerID + "' where WarrantNO='" + WarrantNO + "'";
            this.ExecuteCommand(sql, ucWarrantMaster.conn, ucWarrantMaster.trans);
        }
        //-------------------------------------------------------------------------------------------------------
        //沒用
        private void ucWarrantDetails_AfterApply(object sender, UpdateComponentAfterApplyEventArgs e)
        {
            string WarrantNO = ucWarrantDetails.GetFieldCurrentValue("WarrantNO").ToString();
            string ItemNO = ucWarrantDetails.GetFieldCurrentValue("ItemNO").ToString();
            string RecAmount = ucWarrantDetails.GetFieldCurrentValue("RecAmount").ToString();
            if (RecAmount == "0" || RecAmount == "")
            {
                ucWarrantDetails.CurrentRow.Delete();
            }
        }

        //沒用
        private void ucWarrantDetails_AfterApplied(object sender, EventArgs e)
        {
            
        }

        //沒用
        private void ucWarrantDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            //string WarrantNO = ucWarrantDetails.GetFieldCurrentValue("WarrantNO").ToString();
            //string ItemNO = ucWarrantDetails.GetFieldCurrentValue("ItemNO").ToString();
            //string RecAmount = ucWarrantDetails.GetFieldCurrentValue("RecAmount").ToString();
            //if (RecAmount == "0" || RecAmount == "")
            //{
                //ucWarrantDetails.CurrentRow.Delete();
                 //ucWarrantDetails.CurrentRow.ItemArray
            //}
        }

        //沒用
        private void ucWarrantMaster_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)//ucWarrantDetails_AfterInsert在此事件之後執行
        {
        //    string WarrantNO = ucWarrantMaster.GetFieldCurrentValue("WarrantNO").ToString();
        //    string sql = "select sum(RecAmount) as sumRecAmount from [WarrantDetails] where WarrantNO='" + WarrantNO + "'" + "\r\n";
        //    DataTable tb = this.ExecuteSql(sql, ucWarrantMaster.conn, ucWarrantMaster.trans).Tables[0];
        //    var sumRecAmount = tb.Rows[0][0];
        //    if (sumRecAmount != DBNull.Value)
        //    {
        //        string sql1 = "update [WarrantMaster] set RecAmount=" + Convert.ToInt32(sumRecAmount) + " where WarrantNO='" + WarrantNO + "'" + "\r\n";
        //        this.ExecuteCommand(sql1, ucWarrantMaster.conn, ucWarrantMaster.trans);
        //    }
        }

        

        ////沒用
        //private void ucWarrantMaster_AfterApply(object sender, UpdateComponentAfterApplyEventArgs e)
        //{
        //    //string WarrantNO = ucWarrantMaster.GetFieldCurrentValue("WarrantNO").ToString();
        //    //string sql = "select sum(RecAmount) as sumRecAmount from [WarrantDetails] where WarrantNO='" + WarrantNO + "'" + "\r\n";
        //    //DataTable tb = this.ExecuteSql(sql, ucWarrantMaster.conn, ucWarrantMaster.trans).Tables[0];
        //    //var sumRecAmount = tb.Rows[0][0];
        //    //if (sumRecAmount != DBNull.Value)
        //    //{
        //    //    string sql1 = "update [WarrantMaster] set RecAmount=" + Convert.ToInt32(sumRecAmount) + " where WarrantNO='" + WarrantNO + "'" + "\r\n";
        //    //    this.ExecuteCommand(sql1, ucWarrantMaster.conn, ucWarrantMaster.trans);
        //    //}
        //}   
    }
}
