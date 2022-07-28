using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Data.Sql;

namespace sPO_Normal_PRPOIQC
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

        public string ReturnGetFixed()
        {
            string Year = DateTime.Now.Year.ToString();
            return "PO" + Year;
        }

        //自動起單 for 請購單新增時(沒用到)
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string PONO = "";

            //1.抓RoleID
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //抓主管角色ID
                string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + userid + "' and ORG_DESC !='福委會'";
                //抓部門的角色ID
                string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)//是主管
                {
                    RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                    ds.Dispose();
                }
                else
                {//不是主管
                    ds = this.ExecuteSql(sql1, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//是部門下屬
                    {
                        RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                    }
                    ds.Dispose();
                }
                transaction.Commit(); // 確認交易
            }
            catch
            {
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }

            //2.抓PONO
            if (RoleID != "")
            {
                connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                transaction = connection.BeginTransaction();
                try
                {
                    string sql = "SELECT Top 1 [PONO] FROM [JBADMIN].[dbo].[POMaster] ORDER BY PONO DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    PONO = ds.Tables[0].Rows[0]["PONO"].ToString();
                    transaction.Commit(); // 確認交易
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }

            //3.起單
            if (RoleID != "" && PONO != "")//有RoleID，有PONO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    "D:\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sPO_Normal_PRPOIQC.POMaster",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                    },

                    new object[]{
                    "PONO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "PONO='"+ PONO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
                    }
                });
                    ret[1] = true;
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
            }

            return ret;
        }

        //手動起單
        public object[] FlowStartUpByHand(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            string PONO = aParam[1].ToString();
            string RoleID = "";

            //1.抓RoleID
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //抓主管角色ID
                string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + userid + "' and ORG_DESC !='福委會'";
                //抓部門的角色ID
                string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)//是主管
                {
                    RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                    ds.Dispose();
                }
                else
                {//不是主管
                    ds = this.ExecuteSql(sql1, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//是部門下屬
                    {
                        RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                    }
                    ds.Dispose();
                }
                transaction.Commit(); // 確認交易
            }
            catch
            {
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }

            //3.起單
            if (RoleID != "" && PONO != "")//有RoleID，有PONO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    object[] ret1=ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    "D:\\INFOLIGHT\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    string.Empty,////空白即可，系統使用
                    0,//是否為重要申請
                    0,//是否為緊急申請
                    "",//提交意見說明
                    RoleID,//申請者的RoleID(角色編號)
                    "sPO_Normal_PRPOIQC.POMaster",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                    0,//系統使用
                    "0",//組織類別編號ex:0公司組織、1福利委員會
                    "" //附件
                    },
                    new object[]{
                    "PONO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                    "PONO='"+ PONO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
                    }
                });
                    ret[1] = true;
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
            }


            return ret;
        }

        //取得組織編號(如果自己有在組織內)、直屬主管的組織編號
        public object[] GetUserOrgNOs(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                //funReturnEmpOrgNOL2--我在的組織或我管的組織(出來的組織的上層組織是總經室)(L2的組織)
                //funReturnEmpOrgNOParent--我在的組織(優先)或我管的組織的上層組織(隸屬的組織)
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

        public object[] GetOrgNO_CostCenterID(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string OrgNO = parm[0];
                string sql = "SELECT distinct cc.[CostCenterID]";
                sql = sql + " FROM [JBADMIN].[dbo].[glCostCenter] cc";
                sql = sql + " left join [EIPHRSYS].[dbo].[SYS_ORG] so on so.TOCOSTCENTERID=cc.CostCenterID";
                sql = sql + " where IsActive=1 and so.org_no='" + OrgNO + "'";
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

        public object[] GetPlanPayDate(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string EndDay = parm[0];
                string DebtorDays = parm[1];
                string AcceptanceDate = parm[2];
                string sql = "select dbo.funReturnAPPlanPayDate ('" + EndDay + "','" + DebtorDays + "','" + AcceptanceDate + "')";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw(ex);
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;

        }

        //新增資料到交貨明細(請採購明細 有填首批交貨日期、首批交貨數量)(採購作業)
        private void ucPODetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //有填首批交貨日期或首批交貨數量
            if (ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString() != "" && (ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "" && ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "0"))
            {
                string PONO = ucPODetails.GetFieldCurrentValue("PONO").ToString();
                string ItemNO = ucPODetails.GetFieldCurrentValue("ItemNO").ToString();
                string FirstDeliveryDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString()).ToString("yyyy/MM/dd");//去除上午下午
                string FirstDeliveryQty = ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString();
                string CreateBy = ucPODetails.GetFieldCurrentValue("CreateBy").ToString();
                string PurPrice = ucPODetails.GetFieldCurrentValue("PurPrice").ToString();
                string PurVendor = ucPODetails.GetFieldCurrentValue("PurVendor").ToString();//採購廠商

                //string CreateDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("CreateDate").ToString()).ToString("yyyy/MM/dd hh:mm:ss");

                //if (FirstDeliveryDate != "" && (FirstDeliveryQty != "" && FirstDeliveryQty != "0"))//有填首批交貨日期或首批交貨數量
                //{
                //抓結帳方式
                string sql00 = "select POPayTypeID from dbo.POMaster where PONO='" + PONO + "'";
                DataTable tb = this.ExecuteSql(sql00, ucPODetails.conn, ucPODetails.trans).Tables[0];

                    string sql0 = "select count(ItemNO) from dbo.PODelivery where PONO='" + PONO + "' and ItemNO='" + ItemNO + "' and DeliveryNO='" + ItemNO.Substring(1, 2) + "'";
                    string result = this.ExecuteSql(sql0, ucPODetails.conn, ucPODetails.trans).Tables[0].Rows[0][0].ToString();
                    //pOPayTypeID
                    if (result == "0" && tb.Rows.Count>0)//沒新增過
                    {
                        if (tb.Rows[0].ItemArray[0].ToString() != "3")//結帳方式非分期付款
                        {
                            string vendAccount = "";
                            string payTermName = "";
                            string sql1 = "select v.VendAccount,p.PayTermName from dbo.Vendors v left join dbo.PayTerm p on p.PayTermID=v.PayTermID where v.VendID='" + PurVendor + "'";
                            DataSet ds = this.ExecuteSql(sql1, ucPODetails.conn, ucPODetails.trans);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                vendAccount = ds.Tables[0].Rows[0]["VendAccount"].ToString();
                                payTermName = ds.Tables[0].Rows[0]["PayTermName"].ToString();
                            }

                            string sql = "insert into dbo.PODelivery(" + "\r\n";
                            sql = sql + "[PONO],[ItemNO],[DeliveryNO],[DeliveryDate],[DeliveryQty],[PurPrice],[CreateBy],[CreateDate],[DebtorDays],[AccountNO],[ReturnQty]) values " + "\r\n";
                            sql = sql + "('" + PONO + "','" + ItemNO + "','" + ItemNO.Substring(1, 2) + "',convert(datetime,'" + FirstDeliveryDate + "', 111),'" + FirstDeliveryQty + "'," + PurPrice + ",'" + CreateBy + "',GETDATE(),'" + payTermName + "','" + vendAccount + "',0)" + "\r\n";
                            this.ExecuteCommand(sql, ucPODetails.conn, ucPODetails.trans);
                        }
                    }
                //}
            }
        }

        //新增資料到交貨明細(請採購明細 有填首批交貨日期、首批交貨數量)(採購作業)(分期付款)
        private void ucPODetails_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string d1 = ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString();
            string d2 = ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString();
            //有填首批交貨日期或首批交貨數量
            if (ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString() != "" && (ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "" && ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "0"))
            {
                string PONO = ucPODetails.GetFieldCurrentValue("PONO").ToString();
                string ItemNO = ucPODetails.GetFieldCurrentValue("ItemNO").ToString();
                string FirstDeliveryDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString()).ToString("yyyy/MM/dd");//去除上午下午
                string FirstDeliveryQty = ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString();
                //string CreateBy = ucPODetails.GetFieldCurrentValue("CreateBy").ToString();
                string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string CreateBy = SrvGL.GetUserName(user_id);

                string PurPrice = ucPODetails.GetFieldCurrentValue("PurPrice").ToString();
                string PurVendor = ucPODetails.GetFieldCurrentValue("PurVendor").ToString();//採購廠商

                //string CreateDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("CreateDate").ToString()).ToString("yyyy/MM/dd hh:mm:ss");

                //if (FirstDeliveryDate != "" && (FirstDeliveryQty != "" && FirstDeliveryQty != "0"))//有填首批交貨日期或首批交貨數量
                //{
                //抓結帳方式
                string sql00 = "select POPayTypeID from dbo.POMaster where PONO='" + PONO + "'";
                DataTable tb = this.ExecuteSql(sql00, ucPODetails.conn, ucPODetails.trans).Tables[0];

                string sql0 = "select count(ItemNO) from dbo.PODelivery where PONO='" + PONO + "' and ItemNO='" + ItemNO + "' and DeliveryNO='" + ItemNO.Substring(1, 2) + "'";
                string result = this.ExecuteSql(sql0, ucPODetails.conn, ucPODetails.trans).Tables[0].Rows[0][0].ToString();

                if (result == "0" && tb.Rows.Count > 0)//沒新增過
                {   
                    if (tb.Rows[0][0].ToString() == "3"){//結帳方式是分期付款
                        string vendAccount = "";
                        string payTermName = "";
                        string sql1 = "select v.VendAccount,p.PayTermName from dbo.Vendors v left join dbo.PayTerm p on p.PayTermID=v.PayTermID where v.VendID='" + PurVendor + "'";
                        DataSet ds = this.ExecuteSql(sql1, ucPODetails.conn, ucPODetails.trans);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            vendAccount = ds.Tables[0].Rows[0]["VendAccount"].ToString();
                            payTermName = ds.Tables[0].Rows[0]["PayTermName"].ToString();
                        }

                        string sql = "insert into dbo.PODelivery(" + "\r\n";
                        sql = sql + "[PONO],[ItemNO],[DeliveryNO],[DeliveryDate],[DeliveryQty],[PurPrice],[CreateBy],[CreateDate],[DebtorDays],[AccountNO],[ReturnQty]) values " + "\r\n";
                        sql = sql + "('" + PONO + "','" + ItemNO + "','" + ItemNO.Substring(1, 2) + "',convert(datetime,'" + FirstDeliveryDate + "', 111),'" + FirstDeliveryQty + "'," + PurPrice + ",'" + CreateBy + "',GETDATE(),'" + payTermName + "','" + vendAccount + "',0)" + "\r\n";
                        this.ExecuteCommand(sql, ucPODetails.conn, ucPODetails.trans);
                    }
                }
                //}
            }
        }


        private void ucPOMaster_AfterApplied(object sender, EventArgs e)
        {
            string PONO = "";
            try
            {
                PONO = ucPOMaster.GetFieldCurrentValue("PONO").ToString();


                string sql3 = "SELECT D_STEP_ID FROM EIPHRSYS.dbo.SYS_TODOLIST WHERE FORM_PRESENTATION = 'PONO=''" + PONO + "'''";
                DataTable tb = this.ExecuteSql(sql3, ucPOMaster.conn, ucPOMaster.trans).Tables[0];
                string d_STEP_ID = "";
                if (tb.Rows.Count > 0)
                {
                    d_STEP_ID = tb.Rows[0][0].ToString();//關卡
                }
                string pOPayTypeID = ucPOMaster.GetFieldCurrentValue("POPayTypeID").ToString();//結帳方式

                //[設值FlagDeliveryEnough]採購作業或交期安排，若交貨安排完成，就把FlagDeliveryEnough=1
                if ((d_STEP_ID == "採購作業" || d_STEP_ID == "請購者交期安排") && pOPayTypeID != "3")
                {
                    string sql0 = "select sum(PurQty) from dbo.PODetails where PONO='" + PONO + "'";//採購數量
                    string result0 = this.ExecuteSql(sql0, ucPOMaster.conn, ucPOMaster.trans).Tables[0].Rows[0][0].ToString();

                    string sql1 = "select sum(DeliveryQty) from dbo.PODelivery where PONO='" + PONO + "'";//交貨數量
                    string result1 = this.ExecuteSql(sql1, ucPOMaster.conn, ucPOMaster.trans).Tables[0].Rows[0][0].ToString();

                    if (String.IsNullOrEmpty(result0) != true && String.IsNullOrEmpty(result1) != true)
                    {
                        if (result0 == result1 && Convert.ToInt32(result0) > 0 && Convert.ToInt32(result1) > 0)//相等就代表交期安排完全
                        {
                            string sql2 = "Update dbo.[POMaster] Set [FlagDeliveryEnough]=1 where PONO='" + PONO + "'" + "\r\n";
                            this.ExecuteCommand(sql2, ucPOMaster.conn, ucPOMaster.trans);
                        }
                    }
                }
                else if (d_STEP_ID == "採購作業" && pOPayTypeID == "3")//採購作業確定分期付款，就交期安排完成
                {
                    string sql2 = "Update dbo.[POMaster] Set [FlagDeliveryEnough]=1 where PONO='" + PONO + "'" + "\r\n";
                    this.ExecuteCommand(sql2, ucPOMaster.conn, ucPOMaster.trans);
                }
            }
            catch//刪除
            {
                PONO = ucPOMaster.GetFieldOldValue("PONO").ToString();
            }
            
        }

        //設備異動單起單、標記已作完此ServerMethod(IsAssetCompleted=1)
        public object[] InsertAssetApplyFromPO_Normal_PRPOIQC(object[] objParam)
        {
            //目的:採購結帳完就可入設備異動單，且起單設備異動單，更新IsAssetCompleted=1(代表有跑此ServerMethod)
            DataRow dr = (DataRow)objParam[0];
            IDbConnection connection;
            IDbTransaction transaction;
            string pONO = dr["PONO"].ToString();
            string pOPayTypeID = dr["POPayTypeID"].ToString();

            int flowDirection = (int)objParam[1];//1前進 2退回
            if (flowDirection == 1)
            {
                //零、把原請購項目新增到交貨明細(當主檔是分期付款 且 採購結帳時 且 全部分期付款已結賬完成)
                if (pOPayTypeID == "3")
                {
                    connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                    if (connection.State != ConnectionState.Open) { connection.Open(); }
                    transaction = connection.BeginTransaction();

                    try
                    {
                        //請採購明細的分期項目的總採購數量
                        string sql4 = "select sum(PurQty) from dbo.PODetails where PONO='" + pONO + "' and ItemID='I99999'";
                        string sumPurQty = this.ExecuteSql(sql4, connection, transaction).Tables[0].Rows[0][0].ToString();

                        //交貨明細的分期項目的已結帳完成的總驗收和退貨數量
                        string sql5 = "select sum(AcceptanceQty)+sum(ReturnQty) from dbo.PODelivery where PONO='" + pONO + "' and (PayWayID !='' and  PayWayID is not null)";
                        string sumAcceptanceQty = this.ExecuteSql(sql5, connection, transaction).Tables[0].Rows[0][0].ToString();

                        string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                        string user_name = SrvGL.GetUserName(user_id);

                        if (String.IsNullOrEmpty(sumPurQty) != true && String.IsNullOrEmpty(sumAcceptanceQty) != true)
                        {
                            //相等代表全部分期付款已結賬完成
                            if (sumPurQty == sumAcceptanceQty && Convert.ToInt32(sumPurQty) > 0 && Convert.ToInt32(sumAcceptanceQty) > 0)
                            {
                                //把原請購項目新增到交貨明細
                                string sql6 = "Insert Into PODelivery (" + "\r\n";
                                sql6 = sql6 + "[PONO],[ItemNO],[DeliveryNO],[DeliveryDate]" + "\r\n";
                                sql6 = sql6 + ",[DeliveryQty]" + "\r\n";//交貨數量
                                sql6 = sql6 + ",[PurPrice],[OtherFee]" + "\r\n";//物品單價、工程金額
                                sql6 = sql6 + ",[AcceptanceQty],[AcceptanceTax],[TotalPrice]" + "\r\n";//驗收數量、驗收稅額、總價
                                sql6 = sql6 + ",[ProofTypeID],[InvoiceNO],[PayWayID],[DebtorDays],[Surveyors]" + "\r\n";//檢附憑據                     
                                sql6 = sql6 + ",[CreateBy],[CreateDate]" + "\r\n";
                                sql6 = sql6 + ",[AcceptanceDate],[AccountNO]" + "\r\n";//驗收日期、匯款帳號

                                sql6 = sql6 + ") select " + "\r\n";
                                sql6 = sql6 + "[PONO],[ItemNO],SUBSTRING(ItemNO,2,2),(select top 1 DeliveryDate from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",[PurQty]" + "\r\n";//採購數量(交貨數量)
                                sql6 = sql6 + ",PurPrice,null" + "\r\n";//採購單價(物品單價)
                                sql6 = sql6 + ",PurQty,PurTax,(PurPrice*PurQty)+PurTax" + "\r\n";//採購數量(驗收數量)、採購稅額(驗收稅額)、採購總價
                                sql6 = sql6 + ",(select top 1 ProofTypeID from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 InvoiceNO from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 PayWayID from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 DebtorDays from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 Surveyors from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",'" + user_name + "',getdate()" + "\r\n";
                                sql6 = sql6 + ",(select top 1 AcceptanceDate from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 AccountNO from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";

                                sql6 = sql6 + "from  PODetails where PONO='" + pONO + "' and ItemID!='I99999'" + "\r\n";
                                this.ExecuteCommand(sql6, connection, transaction);
                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                    }
                }


                //一、抓出要入設備異動單的交貨明細到DataTable裡
                connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                transaction = connection.BeginTransaction();

                try
                {
                    //foreach (DataRow dr0 in tb.Rows)
                    //{
                    string sql = "EXEC procInsertAssetApplyFromPO_Normal_PRPOIQC '" + pONO + "'";
                    this.ExecuteCommand(sql, connection, transaction);
                    //}
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }

                //二、全部新增成功，再起單(起單前抓起單申請者者職稱代號、剛新增的資產異動單號TranNO(where 請購單號 和 未起單))

                //1.抓RoleID
                string RoleID = "";
                string applyUserID = dr["ApplyUserID"].ToString();
                connection = (IDbConnection)AllocateConnection("EIPHRSYS");
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                transaction = connection.BeginTransaction();
                try
                {
                    //string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    //抓主管角色ID
                    string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + applyUserID + "' and ORG_DESC !='福委會'";
                    //抓部門的角色ID
                    string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + applyUserID + "'";
                    DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//是主管
                    {
                        RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                        ds.Dispose();
                    }
                    //不是主管
                    else
                    {
                        ds = this.ExecuteSql(sql1, connection, transaction);
                        if (ds.Tables[0].Rows.Count > 0)//是部屬
                        {
                            RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                        }
                        ds.Dispose();
                    }
                    transaction.Commit(); // 確認交易
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ReleaseConnection("EIPHRSYS", connection);
                }

                //2.抓TranNO
                DataTable dtTranNO = new DataTable();
                if (RoleID != "")
                {
                    connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    try
                    {
                        string sql = "SELECT [TranNO] FROM [AssetApplyMaster] where left(PONO,11)='" + pONO + "' and flowflag is null";// 
                        dtTranNO = this.ExecuteSql(sql, connection, transaction).Tables[0];
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                    }
                }
                //3.起單
                if (RoleID != "" && dtTranNO.Rows.Count > 0)
                {
                    try
                    {
                        string TranNO;
                        foreach (DataRow drow in dtTranNO.Rows)
                        {
                            TranNO = drow["TranNO"].ToString();

                            object[] cInfo = new object[] { this.ClientInfo[0], -1, 1, "" };
                            SrvGL.LogUser(applyUserID, "", "", 1);//userid,username 模擬登入(用請購者起單)
                            ((object[])cInfo[0])[1] = applyUserID;

                            EEPRemoteModule ep = new EEPRemoteModule();
                            //object[] ret = ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                            object[] ret = ep.CallFLMethod(cInfo, "Submit", new object[]{
                            null,
                            new object[]{
                                //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\AssetApply.xoml",
                                "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\AssetApply.xoml",
                                string.Empty,////空白即可，系統使用
                                0,//是否為重要申請
                                0,//是否為緊急申請
                                "",//提交意見說明
                                RoleID,//申請者的RoleID(角色編號)
                                "sAssetApplyMaster.AssetApplyMaster",//Server端的Dll名稱以及對應的InfoCommand的名字，比如S001.InfoCommand1
                                0,//系統使用
                                "0",//組織類別編號ex:0公司組織、1福利委員會
                                "" //附件
                            },
                            new object[]{
                                "TranNO",//TAble的鍵值欄位，如果是多個欄位組合的話，可以以分號隔開，比如："OrderID;CustomerID"
                                "TranNO='"+ TranNO +"'"//+a[0]+b[0] //key值組合，例如："OrderID=10260;CustomerID=‘‘A001’’" （A001左右分別是兩個單引號）
                            }
                            });
                            SrvGL.LogUser(applyUserID, "", "", -1);    //模擬登出

                            if (ret[0].ToString() != "0")//起單不成功，0是成功
                            {
                                throw new Exception(ret[0] + "," + ret[1]);
                            }
                        }//foreach
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                //三、不管是一般物品還是折舊物品，只要跑完此server method就update PODelivery set IsAssetCompleted=1
                connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                transaction = connection.BeginTransaction();
                try
                {
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);

                    string sql = "update PODelivery set IsAssetCompleted=1,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]=getdate() where PONO='" + pONO + "' and (IsAssetCompleted !=1 or IsAssetCompleted is null) and (PayWayID is not null and PayWayID!='') ";//未資產異動單起單關卡處理 且 採購結帳完成 ((TotalPrice!='' and TotalPrice is not null) or TotalPrice=0)
                    this.ExecuteCommand(sql, connection, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            //先註解掉，因為驗收沒完成，會goto(退回) 且目前PODelivery.IsAPCompleted is null
            //退回(把IsAssetCompleted設null，但資產異動單要退回申請者並手動作廢)
            else
            {
                //    connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                //    if (connection.State != ConnectionState.Open) { connection.Open(); }
                //    transaction = connection.BeginTransaction();
                //    try
                //    {
                //        string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //        string user_name = SrvGL.GetUserName(user_id);

                //        string sql = "update PODelivery set IsAssetCompleted=null,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]=getdate() where PONO='" + pONO + "' and IsAssetCompleted=1 and (PayWayID is not null and PayWayID!='') and IsAPCompleted is null ";//已跑過資產異動單起單關卡 且 採購結帳完成 且 未跑過應收帳款處理
                //        this.ExecuteCommand(sql, connection, transaction);
                //        transaction.Commit();
                //    }
                //    catch (Exception ex)
                //    {
                //        transaction.Rollback();
                //        throw (ex);
                //    }
                //    finally
                //    {
                //        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                //    }
            }



            return new object[] { 0, 0 };
        }

        //回傳系統變數(須報價檔三個的總價門檻)
        public object[] sysVariable_Default(object[] objParam) {

            //string js = string.Empty;
            int CategoryValue=0;
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
                string sql = "SELECT  [CategoryValue] from SYS_Variable   where [Category]='DaysNeedInquery'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                CategoryValue =Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, CategoryValue }; ;
        }

        //回傳true false，若true就走請購者交期安排//採購者無交期安排
        public object[] IsPurchaserNotCompleted(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                //string FlagDeliveryEnough = dr["FlagDeliveryEnough"].ToString();
                string PONO = dr["PONO"].ToString();
                string sql = "Select count(PONO) from PODelivery where PONO='"+PONO+"'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int counts = Int32.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                if (counts == 0)//交貨明細 無
                    ret[1] = true;
                else
                    ret[1] = false;
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

        //[回傳true false] 採購數量驗收完成(flow用)
        public object[] IsAllReceived(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "Select ISNULL(sum(PurQty)-(select sum(AcceptanceQty)+sum(ReturnQty) from PODelivery where PONO='"+PONO+"'),-1) from PODetails where PONO='"+PONO+"'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int diff = Int32.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                if (diff==0)
                    ret[1] = true;
                else
                    ret[1] = false;
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

        //應收帳款處理
        public object[] PutPODeliveryToAPDetails(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            int flowDirection = (int)objParam[1];//1前進 2退回
            if (flowDirection == 1)
            {
                string js = string.Empty;
                DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);
                    string PONO = dr["PONO"].ToString();

                    string sql = "EXEC procPutPODeliveryToAPDetails 1,'" + PONO + "','" + user_name + "'";
                    this.ExecuteCommand(sql, connection, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            return ret;

        }

        //財產目錄過帳到資產異動主檔、資產主檔
        public object[] PostIsCatalogue(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            int flowDirection = (int)objParam[1];//1前進 2退回
            if (flowDirection == 1)
            {
                string js = string.Empty;
                DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);
                    string PONO = dr["PONO"].ToString();
                    string IsCatalogue = dr["IsCatalogue"].ToString();
                    string sql = "select [flowflag] from AssetApplyMaster where substring(PONO,1,11)='" + PONO + "'";
                    DataTable tb =this.ExecuteSql(sql, connection, transaction).Tables[0];
                    //如果是財產目錄==1且AssetApplyMaster有,則
                    //更新AssetApplyMaster.IsCatalogue=1
                    //若AssetApplyMaster.Flowflag=='Z'，則更新AssetMaster.IsCatalogue=1
                    if ((IsCatalogue == "True" || IsCatalogue == "1") && tb.Rows.Count > 0)
                    {
                        string sql0 = "update AssetApplyMaster set IsCatalogue=1 where substring(PONO,1,11)='" + PONO + "'";
                        this.ExecuteCommand(sql0, connection, transaction);
                        string flowflag = tb.Rows[0][0].ToString();
                        if (flowflag == "Z")
                        {
                            string sql1 = "update AssetMaster set IsCatalogue=1,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]=GETDATE() where substring(PONO,1,11)='" + PONO + "'";
                            this.ExecuteCommand(sql1, connection, transaction);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            return ret;

        }

        public object[] NullProcedure(object[] objParam)
        {
                    return new object[] { 0, 0 };
        }
        //請購總額
        public object[] PRTotalAmount(object[] objParam)
        {
            
            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "select sum(RegPrice*RegQty) from PODetails where PONO='" + PONO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount <= 3000)
                {
                ret[1] = true;
                }else{
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
            return ret;

        }

        public object[] PRTotalAmount1(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "select sum(RegPrice*RegQty) from PODetails where PONO='" + PONO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 3000 && amount <=10000)
                {
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
            return ret;

        }

        public object[] PRTotalAmount2(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "select sum(RegPrice*RegQty) from PODetails where PONO='" + PONO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 10000)
                {
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
            return ret;

        }

        //交期安排完成，完成傳回true(flow用)
        public object[] DeliveryIsEnough(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string POPayTypeID = dr["POPayTypeID"].ToString();

                if (POPayTypeID != "3")
                {
                    string sql0 = "select sum(PurQty) from dbo.PODetails where PONO='" + PONO + "'";//採購數量
                    string result0 = this.ExecuteSql(sql0, connection, transaction).Tables[0].Rows[0][0].ToString();

                    string sql1 = "select sum(DeliveryQty) from dbo.PODelivery where PONO='" + PONO + "'";//交貨數量
                    string result1 = this.ExecuteSql(sql1, connection, transaction).Tables[0].Rows[0][0].ToString();
                    
                    if (String.IsNullOrEmpty(result0) != true && String.IsNullOrEmpty(result1) != true)
                    {
                        if (result0 == result1 && Convert.ToInt32(result0) > 0 && Convert.ToInt32(result1) > 0)//相等就代表交期安排完全
                        {
                            ret[1] = true;
                        }
                        else {
                            ret[1] = false;
                        }
                    }
                }
                else if (POPayTypeID == "3")//分期付款，就交期安排完成
                {
                    ret[1] = true;
                }
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
            return ret;

        }


        //採購總額
        public object[] POTotalAmount(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "select FLOOR(sum(PurPrice*PurQty)) from PODetails where PONO='" + PONO + "' and ItemID!='I99999'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount <= 3000)
                {
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
            return ret;

        }

        public object[] POTotalAmount1(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "select FLOOR(sum(PurPrice*PurQty)) from PODetails where PONO='" + PONO + "' and ItemID!='I99999'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 3000 && amount <= 10000)
                {
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
            return ret;

        }

        public object[] POTotalAmount2(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // 取得流程資料表的資料內容
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
                string PONO = dr["PONO"].ToString();
                string sql = "select FLOOR(sum(PurPrice*PurQty)) from PODetails where PONO='" + PONO + "' and ItemID!='I99999'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 10000)
                {
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
            return ret;

        }
    }
}
