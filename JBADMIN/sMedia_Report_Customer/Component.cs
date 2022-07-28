using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sMedia_Report_Customer
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

        public object[] GetReportData(object[] objParam)
        {
            string js = "";
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string CustName = parm[1];
            string SalesID = parm[2];
            string iPeopleCount = parm[3];
            string iPeopleFCount = parm[4];
            string IndustryID = parm[5];
            string CustAreaID = parm[6];
            string SalesTypeID = parm[7];
            string QSDate = parm[8];
            string QEDate = parm[9];

            string sql = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBDBNJB");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                
sql="SELECT top 2180 A.[CustNO],A.[CustName],A.[CustShortName],A.[SalesID],A.[BossName],A.[TaxNO]" + "\r\n";
sql = sql + ",A.[IndustryTypeID],A.[CustTelNO],A.[CustTelNO1],A.[CustCityNO],A.[CustRegion]" + "\r\n";
sql = sql + ",A.[CustPost],A.[CustAddr],A.[CustFaxNO],A.[CustDeptID],A.[CustDeptName]" + "\r\n";
sql = sql + ",A.[CustAreaID],A.[PMID],A.[samA],A.[ContactA],A.[ContactATel],A.[ContactASubTel]" + "\r\n";
sql = sql + ",A.[ContactAMail],A.[ContactAJobID],A.[ContactAIsMail],A.[samB],A.[ContactB]" + "\r\n";
sql = sql + ",A.[ContactBTel],A.[ContactBSubTel],A.[ContactBMail],A.[ContactBJobID]" + "\r\n";
sql = sql + ",A.[ContactBIsMail],A.[samC],A.[BillDeal],A.[BillDealEmail]" + "\r\n";
sql = sql + ",A.[BalanceDay],A.[CustNotes],A.[RecallDate],A.[IsPutInvoice],A.[IsPutPaperInvoice]" + "\r\n";
sql = sql + ",A.[CreateBy],A.[CreateDate],A.[LatelyDayD],A.[LatelyDayP],A.[LatelyDayW]" + "\r\n";
sql = sql + ",A.[LatelyDayN],A.[DealNotesP],A.[DealNotesW],A.[DealNotesN],A.[PayNotes]" + "\r\n";
sql = sql + ",A.[LetterType],A.[LastUpdateBy],A.[LastUpdateDate],A.[IsPublicView]" + "\r\n";
sql = sql + ",A.[IsAcceptePaper],A.[NotAcceptPaper],A.[IsAddLine],A.[PostType]" + "\r\n";
sql = sql + ",A.[IsChangeBank],A.[UserID],A.[HrBankURL],A.[PostedDate]" + "\r\n";
sql = sql + ",A.[PostedMan],A.[DevelopDescr],A.[PostSourceID],A.[IndustryID]" + "\r\n";
sql = sql + ",A.[IndustryType],A.[iPeopleCount],A.[ERPCustomerID],A.[OfficeNo]" + "\r\n";
sql = sql + ",A.[iPeopleFCount],A.[JobWeekendNo],A.[bElecInvoice],A.[ElecInvoiceReason]" + "\r\n";
sql = sql + ",A.[ForeignCompany],A.[ForeignDorm]" + "\r\n";

sql = sql + ",rtrim(ltrim(A.SALESID))+' '+B.SalesName AS SALESIDName" + "\r\n";
sql = sql + ",D.jb_name as IndustryTypeIDName" + "\r\n";
sql = sql + ",E.CityName as CustCityNOName" + "\r\n";
sql = sql + ",A.CustRegion as CustRegionName" + "\r\n";
sql = sql + ",F.CustDeptName as CustDeptIDName" + "\r\n";
sql = sql + ",G.CustAreaName as CustAreaIDName" + "\r\n";
sql = sql + ",H.CustJobName as ContactAJobIDName" + "\r\n";
sql = sql + ",I.CustJobName as ContactBJobIDName" + "\r\n";
sql = sql + ",J.ListContent as PostTypeName" + "\r\n";
sql = sql + ",K.SalesName as PMIDName" + "\r\n";
sql = sql + ",L.jb_name as IndustryIDName" + "\r\n";
sql = sql + ",M.ListContent as IndustryTypeName" + "\r\n";
sql = sql + ",N.ePaperType as IsAcceptePaperName" + "\r\n";

sql = sql + ",dbo.funReturnCustDealDays(LatelyDayD) as DealDays" + "\r\n";
sql = sql + ",(select MAX(NextCallDate) from ERPCustomerToDoNotes where CustNO=A.CustNO " + "\r\n";
sql = sql + "and IsNull(Notes,'')='') as NextCallDate" + "\r\n";
 
sql = sql + "FROM dbo.ERPCustomers as A " + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.ERPSALESMAN as B on (A.SALESID=B.SALESID) " + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.jb_type as D on A.IndustryTypeID=D.jb_type" + "\r\n";
sql = sql + "left join (select distinct CityNO,CityName,Region from [211.78.84.42].JBADMIN.DBO.ERPCity) as E on A.CustCityNO=E.CityNO and A.CustRegion=E.Region" + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.ERPCustDept as F on A.CustDeptID=F.CustDeptID" + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.ERPCustArea G on A.CustAreaID=G.CustAreaID" + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.ERPCustJob H on A.ContactAJobID=H.CustJobID" + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.ERPCustJob I on A.ContactBJobID=I.CustJobID" + "\r\n";
sql = sql + "left join JBADMIN.DBO.ERPReferenceTable J on A.PostType=J.ListID and J.ListCategory='POSTTYPE'" + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.ERPSALESMAN K on A.PMID=K.SALESID" + "\r\n";
sql = sql + "left join [211.78.84.42].JBADMIN.DBO.jb_type as L on A.IndustryID=L.jb_type" + "\r\n";
sql = sql + "left join JBADMIN.DBO.ERPReferenceTable M on A.IndustryType=M.ListID and M.ListCategory='IndustryType'" + "\r\n";
sql = sql + "left join JBADMIN.DBO.ERPPAPERTYPE N on A.IsAcceptePaper=N.ePaperCode" + "\r\n";
sql = sql + "Where 1=1" + "\r\n";
                if (CustNO != "")
                    {
                        sql = sql + "and A.CustNO ='" + CustNO + "'" + "\r\n";
                    }
                if (CustName != "")
                    {
                        sql = sql + "and A.CustName like '%" + CustName + "%'" + "\r\n";
                    }
                if (SalesID != "")
                    {
                        sql = sql + "and A.SalesID ='" + SalesID + "'" + "\r\n";
                    }
                if (iPeopleCount != "")
                    {
                        sql = sql + "and A.iPeopleCount ='" + iPeopleCount + "'" + "\r\n";
                    }
                if (iPeopleFCount != "")
                    {
                        sql = sql + "and A.iPeopleFCount ='" + iPeopleFCount + "'" + "\r\n";
                    }
                if (IndustryID != "")
                    {
                        sql = sql + "and A.IndustryID ='" + IndustryID + "'" + "\r\n";
                    }
                if (CustAreaID != "")
                    {
                        sql = sql + "and A.CustAreaID ='" + CustAreaID + "'" + "\r\n";
                    }
                if (SalesTypeID != "") {
                    if (QSDate != "") {
                         QSDate = QSDate.Substring(0,10);
                     }else{
                        QSDate = "1900/01/01";
                    }
                     if (QEDate != "") {
                         QEDate = QEDate.Substring(0, 10);
                     }else{
                        QEDate = "1900/01/01";
                     }
                     sql = sql +" and A.CustNO in (SELECT Custno FROM DBO.funReturnCustBillSalesDate('" + SalesTypeID + "','" + QSDate + "','" + QEDate + "'))"+ "\r\n";
                }

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBDBNJB", connection);
            }
            return new object[] { 0, js };
        }
    }
}
