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
using JBTool;
using System.Collections;

namespace sERPSalseDetails
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

        //取得ERPSalesDetails=>SalesMasterNO最大編號
        public string GetSalesMasterNO(string CustNO)
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string ItemSeq = "";
            //取得ERPSalesDetails=>SalesMasterNO最大編號
            //民國年+月份+流水號3碼10404001 Group by CustNO
            //建立資料庫連結           
            try
            {
                //獲取ItemSeq民國年+月份+流水號3碼10404001
                string sql = "select cast(DATEPART(YEAR,GETDATE())-1911 as nvarchar(3))+RIGHT('00'+cast(DATEPART(MONTH,GETDATE()) as nvarchar(2)),2) " + "\r\n";
                sql = sql + "+(select RIGHT('000'+RTRIM(LTRIM(STR(convert(nvarchar(3),(select IsNull(max(Right(ItemSeq,3)),'')+1" + "\r\n";
                sql = sql + "from ERPSalesDetails where CustNO=" + CustNO + ")),3))),3)) as ItemSeq " + "\r\n";
                DataSet dsItemSeq = this.ExecuteSql(sql, connection, transaction);
                ItemSeq = dsItemSeq.Tables[0].Rows[0]["ItemSeq"].ToString();
            }
            catch { transaction.Rollback(); }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return ItemSeq;
        }
        //取得贈送週報的日期=>夾報增加贈週報?期(固定禮拜一)禮拜五截稿=>禮拜五訂的話=>下下禮拜一
        public DateTime GetSalesDay(DateTime dt)
        {
            if (dt.DayOfWeek >= DayOfWeek.Friday)//大於禮拜五之後=>下下禮拜一
            {
                dt = dt.AddDays(5); 
            }
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(1);
            }   
            return dt;
        }
        //新增銷貨明細
        private void ucERPSalseMaster_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            int SalesMasterNO = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("SalesMasterNO"));//銷貨代號
            //string ItemSeq = "";//銷售序號ItemSeq=>民國年+月份+流水號3碼10404001(客戶group by)
            string CustNO = ucERPSalseMaster.GetFieldCurrentValue("CustNO").ToString();//客戶代號              
            string SalesEmployeeID = ucERPSalseMaster.GetFieldCurrentValue("SalesEmployeeID").ToString();//業務員工代號        

            string SalesTypeID = ucERPSalseMaster.GetFieldCurrentValue("SalesTypeID").ToString();//交易別
            string DMTypeID = ucERPSalseMaster.GetFieldCurrentValue("DMTypeID").ToString();//版別
            string ViewAreaID = ucERPSalseMaster.GetFieldCurrentValue("ViewAreaID").ToString();//版別區域
            string GrantTypeID = " ";//贈期方式=>夾報交易別SalesTypeID1,週報31=>key法不同夾報 贈*,週報 贈+

            Double CustAmt = Convert.ToDouble(ucERPSalseMaster.GetFieldCurrentValue("CustAmt"));//客總價
            Double OldCustAmt = CustAmt;//原始客總價
            int SalesQty = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("SalesQty"));//單位數
            int SalesQtyView = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("SalesQtyView"));//見刊             
            int Commission = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("Commission"));//佣金

            int PublishType = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PublishType"));//刊登方式
            int PublishCount = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PublishCount"));//刊
            int PresentCount = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PresentCount"));//贈
            Double CustPrice = 0;
            if (SalesTypeID.Trim() == "6")//交易別6 => 報紙
            {                                 
                CustPrice = Convert.ToDouble(ucERPSalseMaster.GetFieldCurrentValue("CustPrice"));//客單價                
            }
            else
            {
                //客戶單價=>客戶總價/單位數/刊期
                CustPrice = Math.Floor(CustAmt / SalesQty / PublishCount);//無條件捨去=>剩下金額加總在最後一筆
            }

            //贈週報可能為空字串的預設
            int PresentWNewsCount = 0;
            if (ucERPSalseMaster.GetFieldCurrentValue("PresentWNewsCount") != DBNull.Value)
            {
                PresentWNewsCount = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PresentWNewsCount"));//贈週報
            }
            int SubCount = PublishCount + PresentCount;//刊+贈
            int sumCount = PublishCount + PresentCount + PresentWNewsCount;//刊+贈+贈週報=>總期數(要跑的次數)
            string SalesDescr = ucERPSalseMaster.GetFieldCurrentValue("SalesDescr").ToString();//銷貨備註
            string Remark1 = ucERPSalseMaster.GetFieldCurrentValue("Remark1").ToString();//註解

            string SalesDate = "";//銷貨日期+1 =>104/05/01 , InvoiceYM=>104/05
            DateTime ContinueDate = DateTime.Parse("1900/01/01");//連登日期
            DateTime ComputerDate = DateTime.Parse("1900/01/01");//計算日期
            DateTime StartDate = DateTime.Parse("1900/01/01");//初始日期
            string sJumpDate = "";
            string[] JumpDate = { };
            if (PublishType == 1)//連登
            {
                ContinueDate = DateTime.Parse(ucERPSalseMaster.GetFieldCurrentValue("ContinueDate").ToString());//連刊日期
                ComputerDate = ContinueDate;//計算日期
                StartDate = ContinueDate;//初始日期
            }
            else//跳登
            {
                //sJumpDate = "2015/05/01,2015/05/03,2015/05/05,2015/05/10,2015/05/16,2015/05/22";
                //JumpDate = sJumpDate.Split(',');
                sJumpDate = ucERPSalseMaster.GetFieldCurrentValue("JumpDate").ToString();//跳刊日期
                JumpDate = sJumpDate.Split('\n');
                ContinueDate = DateTime.Parse(JumpDate[0]);
                ComputerDate = ContinueDate;//計算日期
                StartDate = ContinueDate;//初始日期
            }
            //週報日期
            string sWeekendDate = "";
            string[] WeekendDate = { };
            sWeekendDate = ucERPSalseMaster.GetFieldCurrentValue("WeekendDate").ToString();
            WeekendDate = sWeekendDate.Split('\n');
           
            //預設值=>求才
            int CustLines = 0;//客行
            Double OfficePrice = 0;//社單價
            int OfficeLines = 0;//社行
            int OfficeAmt = 0;

            string NewsTypeID="";
            string NewsAreaID="";
            string NewsPublishID="";
            int Sections=0;
            if (SalesTypeID.Trim() == "6")//交易別6 => 報紙
            {
                CustLines = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("CustLines"));//客戶行數              
                OfficePrice = Convert.ToDouble(ucERPSalseMaster.GetFieldCurrentValue("OfficePrice"));
                OfficeLines = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("OfficeLines"));
                OfficeAmt = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("OfficeAmt"));//繳社價
                NewsTypeID = ucERPSalseMaster.GetFieldCurrentValue("NewsTypeID").ToString();
                NewsAreaID = ucERPSalseMaster.GetFieldCurrentValue("NewsAreaID").ToString();
                NewsPublishID = ucERPSalseMaster.GetFieldCurrentValue("NewsPublishID").ToString();
                Sections = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("Sections"));
            }
            string InvoiceYMPoint = (DateTime.Parse(ucERPSalseMaster.GetFieldCurrentValue("InvoiceYMPoint").ToString())).ToString("yyyy/MM/dd"); ;//結帳日切點
            string CreateBy = ucERPSalseMaster.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucERPSalseMaster.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");
            //發報種類
            int IsAcceptePaper = 0;
            if (ucERPSalseMaster.GetFieldCurrentValue("IsAcceptePaper") != DBNull.Value)
            {
                IsAcceptePaper = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("IsAcceptePaper"));
            }

            for (int i = 0; i < sumCount; i++)//總期數
            {
                //夾報(求才快訊) 刊?天算錢?天,之後都算贈*,+(夾報送週報)週報 贈 +     
                //報紙 key?贈? =>2+5=>第1,2天算錢 贈=>空白
                //夾報增加贈週報?期(固定禮拜一)禮拜五截稿=>禮拜五訂的話=>下下禮拜一               
                    if (i < SubCount)//刊+贈
                    {                        
                        if (PublishType == 1)//連刊
                        {
                            ComputerDate = StartDate.AddDays(i);//銷貨日期+1 =>104/05/01 , InvoiceYM=>104/05
                        }
                        else//跳刊
                        {
                            ComputerDate = DateTime.Parse(JumpDate[i]);
                        }
                        //=>夾報交易別SalesTypeID1,週報31=>差別只在贈期的key法不同夾報 贈*,週報 贈+
                        if (i >= PublishCount)//期數>=刊期(之前的贈期為空字串)
                        {
                            if (SalesTypeID == "1")//求才快訊
                            {
                                GrantTypeID = "*";
                            }
                            else if (SalesTypeID == "31")//週報
                            {
                                GrantTypeID = "+";
                            }
                        }
                    }
                    else//贈週報
                    {
                        if (SalesTypeID.Trim() == "6")//交易別6 => 報紙
                        {
                            GrantTypeID = " ";
                        }
                        else
                        {
                            GrantTypeID = "+";//(夾報送週報)週報 贈 +
                        }
                        ComputerDate = DateTime.Parse(WeekendDate[i - SubCount]);
                        ////第一期週報
                        //if (i == SubCount)
                        //{
                        //    ComputerDate = GetSalesDay(StartDate);//連登日期開始算
                        //}
                        //else ComputerDate = GetSalesDay(ComputerDate.AddDays(1));
                    }
                   
                    if (i >= PublishCount)//期數>=刊期 不要收錢
                    {                        
                        CustPrice = 0;
                        //報紙
                        if (SalesTypeID.Trim() == "6")
                        {
                            Commission = 0;//佣金
                            OfficePrice = 0;//社單價                            
                            OfficeAmt = 0;//繳社價                            
                            CustAmt = 0;//總額
                        }
                    }
                    
                    CustAmt = CustPrice * SalesQty;//總額 
                    //報紙
                    if (SalesTypeID.Trim() == "6")
                    {
                        CustAmt = Math.Round(CustPrice * CustLines);//客總額(四捨五入)
                    }
                    OldCustAmt = OldCustAmt - CustAmt;
                    if (SalesTypeID.Trim() != "6" && i == PublishCount - 1)//要錢的最後一期
                    {
                        CustAmt = CustAmt +OldCustAmt;
                    }
                SalesDate = ComputerDate.ToString("yyyy/MM/dd");
                //建立資料庫連結
                IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                //當連線狀態不等於open時，開啟連結
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                //開始transaction
                IDbTransaction transaction = connection.BeginTransaction();

                
                //------------------------------------------------------------------------------------------------------------------

                try
                {
                    string SQL = "exec procInsertERPSalseDetails " +sumCount+","+ SalesMasterNO + ",'" + CustNO + "','" + SalesEmployeeID + "','" +
                        SalesTypeID + "','" + DMTypeID + "','" + ViewAreaID + "','" + SalesDate + "','" + GrantTypeID + "'," +
                        CustPrice + "," + SalesQty + "," + SalesQtyView + "," + CustAmt + "," + Commission + "," +
                        PublishCount + "," + PresentCount + "," + PresentWNewsCount + ",'" + SalesDescr + "','" + Remark1 + "'," +
                        CustLines + "," + OfficePrice + "," + OfficeLines + "," + OfficeAmt + ",'" + NewsTypeID + "','" +
                        NewsAreaID + "','" + NewsPublishID + "'," + Sections + ",'" + InvoiceYMPoint + "','" + CreateBy + "','" + CreateDate + "'," + IsAcceptePaper;
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
        // 修改發報種類, 修改[0800JOB].dbo.Publishing 出刊狀態 ,刊登起訖
        public object[] Update0800JOBPublishing(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string SalesTypeID = parm[1];//新的值
            string IsAcceptePaper = parm[2];

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sq1 = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//敦緯內網
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN0118;User ID=JBDBsql;Password=J3554436B";//Test

            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //修改發報種類
                string SQL2 = "exec procUpdateERPCustomersIsAcceptePaper '" + CustNO + "','" + SalesTypeID + "'," + IsAcceptePaper;
                cmd = new SqlCommand(SQL2, conn);
                cmd.ExecuteNonQuery();

                sq1 = "exec [192.168.10.70].[0800JOB].dbo.procUpdate0800JOBPublishing '" + CustNO+ "'";//
                //sq1 = "exec [192.168.10.70].[0800JOB0118].dbo.procUpdate0800JOBPublishing " + CustNO;//Test
                cmd = new SqlCommand(sq1, conn);
                cmd.ExecuteNonQuery();

            }
            catch
            {
            }
            finally
            {
                conn.Dispose();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }

        private void ucERPSalseDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucERPSalseDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//欄位賦值
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucERPSalseDetails.SetFieldValue("LastUpdateBy", LoginUser);//欄位賦值
        }

        private void ucERPSalseDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {           
            ucERPSalseDetails.SetFieldValue("CreateDate", DateTime.Now);//欄位賦值
        }

        private void ucERPSalseMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPSalseMaster.SetFieldValue("CreateDate", DateTime.Now);//欄位賦值
            if (ucERPSalseMaster.GetFieldCurrentValue("Is0800Id").ToString() == "False")
            {
                ucERPSalseMaster.SetFieldValue("0800Id", DBNull.Value);
            }
        }
        //代辦事項筆數
        public object[] ShowToDoCount(object[] objParam)
        {
            string SalesEmployeeID = (string)objParam[0];
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
                string SQL = "exec procDisplaySalesToDoListCount '" + SalesEmployeeID + "'";
                DataSet ds= this.ExecuteSql(SQL, connection, transaction);
                string cnt = ds.Tables[0].Rows[0]["sCount"].ToString();                
                //// Indented縮排 將資料轉換成Json格式
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                js = cnt;
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
        //新增銷貨明細(補登檢查)
        public object[] CheckERPSalseDetailsLast(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];          
            string sJumpDate = parm[1].Replace('\n', ',');         

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
                string SQL = "exec procCheckERPSalseDetailsLast " + SalesMasterNO + ",'"  +sJumpDate + "'";
                DataSet dsCheck = this.ExecuteSql(SQL, connection, transaction);
                js = JsonConvert.SerializeObject(dsCheck.Tables[0], Formatting.Indented);
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
        //新增銷貨明細(補登)
        public object[] InsertERPSalseDetailsLast(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string CustNO = parm[1];
            string DMTypeID = parm[2];
            string ViewAreaID = parm[3];
            string SalesQty = parm[4];
            string SalesQtyView = parm[5];
            string SalesDescr = parm[6];
            string Remark1 = parm[7];
            string YMPoint = parm[8];
            string sJumpDate = parm[9].Replace('\n', ',');//刊登日期      
            string sJumpDate2 = parm[10].Replace('\n', ',');//週報日期                       
            string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

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
            {   //贈期
                string SQL = "exec procInsertERPSalseDetailsLast '"+YMPoint+"','*'," + SalesMasterNO + ",'" + CustNO + "','" + DMTypeID + "','" +
                      ViewAreaID + "'," + SalesQty + "," + SalesQtyView + ",'" + sJumpDate + "','" + LoginUser + "','" + CreateDate + "','" + SalesDescr + "','" + Remark1 + "'";
                this.ExecuteSql(SQL, connection, transaction);
                //週報
                if (sJumpDate2.Trim() != "")
                {
                    string SQL2 = "exec procInsertERPSalseDetailsLast '" + YMPoint + "','+'," + SalesMasterNO + ",'" + CustNO + "','" + DMTypeID + "','" +
                          ViewAreaID + "'," + SalesQty + "," + SalesQtyView + ",'" + sJumpDate2 + "','" + LoginUser + "','" + CreateDate + "','" + SalesDescr + "','" + Remark1 + "'";
                    this.ExecuteSql(SQL2, connection, transaction);
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
            return new object[] { 0, js };
        }

        //取得系統變數InvoiceYMPoint結帳日
        public object[] GetInvoiceYMPoint(object[] objParam)
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
                string sql = " select isnull((select top 1 CategoryValue from SYS_Variable where Category='SalesDetailsInvoiceYMPoint' and CategoryValue>Convert(nvarchar(10),getdate(),111) order by CategoryValue),Convert(nvarchar(10),getdate(),111)) as CategoryValue";
                DataSet InvoiceYMPoint = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = InvoiceYMPoint.Tables[0].Rows[0]["CategoryValue"].ToString();
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
        //銷貨明細選擇性設為無效
        public object[] UpdateSalseDetailsIsActive(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string CustNO = parm[2];
            string depositSeq = parm[3];
            string sType = parm[4];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower()); 
            string js = string.Empty;

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//敦緯內網
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=sa;Password=J3554436B";

            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //IDbTransaction trans = conn.BeginTransaction();
                //執行 JB-DB\SQL2008.NJB.JBADMIN.dbo.procPostToNjbDeposit 預存程序
                sql = "EXEC procUpdateERPSalseDetailsIsActive '" + SalesMasterNO + "','" + ItemSeq + "','" + sType + "','" + username + "'";
                //sql = "EXEC [60.250.52.107,3225].JBADMIN.dbo.procUpdateERPSalseDetailsIsActive '" + SalesMasterNO + "','" + ItemSeq + "','" + sType + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();              
            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Dispose();                
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }
       
        //檢查是否有此客戶
        public object[] CheckNullCustNO(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];           
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
                string sql = " select Isnull((select CustShortName from View_ERPCustomers where CustNO='" + CustNO + "'),'') as CustShortName";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                js = sCustNO.Tables[0].Rows[0]["CustShortName"].ToString();
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
        //銷貨明細選擇性匯入行政系統
        public object[] ImportSalesDetails(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());            
            string js = string.Empty;

           //
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;

            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//敦緯內網
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=sa;Password=J3554436B";
            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //IDbTransaction trans = conn.BeginTransaction();
                //執行 JB-DB\SQL2008.NJB.JBADMIN.dbo.procPostToNjbDeposit 預存程序
                sql = "EXEC procImportSalesDetails '" + SalesMasterNO + "','" + ItemSeq + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, conn,trans);
                //trans.Commit();
            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Dispose();  
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }
        //銷貨明細全部刪除
        public object[] DeleteSalesDetails(object[] objParam)
        {
            string SalesMasterNO = objParam[0].ToString();
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
                string SQL = "exec procDeleteSalesDetails " + SalesMasterNO;
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

        private void ucERPSalseDetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {

        }
        //得到客戶的發報種類
        public object[] GetIsAcceptePaper(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
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
                string sql = " select IsAcceptePaper,IndustryType,ERPCustomerID from View_ERPCustomers where CustNO='" + CustNO + "' ";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
                //js = sCustNO.Tables[0].Rows[0]["IsAcceptePaper"].ToString();
                js = JsonConvert.SerializeObject(sCustNO.Tables[0], Formatting.Indented);

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

        // 取得便利報SalesTypeID=31的日期統計資訊
        public object[] GeERPSalseDetailsInfo(object[] objParam)
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
                string SQL = " exec procReportERPSalseDetailsInfo";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented縮排 將資料轉換成Json格式
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
        //================================================================================================================================//
        /// <summary>Combobox用資料</summary>
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
        //取得連動,客戶代號 帶出出刊客戶
        public object[] Get0800Id(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var Code = Parameter_Input["Co_de"].ToString().Trim();                
                
                string SQL = @"
select Id,Code
from  View_0800jobPublishing
where Code like '%"+@Code+"%'";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@Code", Code));

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<Guid>("Id").ToString(),
                    text = m.Field<string>("Code") ?? ""
                }).ToList();

                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }



    }
}
