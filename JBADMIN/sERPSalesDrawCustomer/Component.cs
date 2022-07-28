using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using TheMailTool;
using System.Data;
using Newtonsoft.Json;

namespace sERPSalesDrawCustomer
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

        public void GGGGGGGG()
        {
            JbMailManager gggg = new JbMailManager
            {
                Smtp = new JbSmtp
                {
                    Host = "",
                    Port = 25,
                    IsCredentials = true,
                    CredentialsType = AuthenticationType.GGSAPI,
                    Account = "",
                    PassWord = "",
                    EnableSsl = true                    
                },
                IsStop = false,
                IsTestMode = false,
                AccountForTest = ""
            };

            JbMail gMail_1 = new JbMail
            {
                PrimaryKey = "",
                Subject = "",
                Content = "",
                FromAddress = "",
                FromDisplayName = "",
                ToAddress = "",
                ToDisplayName = "",
                Priority = System.Net.Mail.MailPriority.Normal,
                AttachmentList = new List<JbAttachments>()
            };


            JbMail gMail_2 = new JbMail
            {
                PrimaryKey = "",
                Subject = "",
                Content = "",
                FromAddress = "",
                FromDisplayName = "",
                ToAddress = "",
                ToDisplayName = "",
                Priority = System.Net.Mail.MailPriority.Normal,
                AttachmentList = new List<JbAttachments>()
            };

            List<JbMail> allMail = new List<JbMail>();
            allMail.Add(gMail_1);
            allMail.Add(gMail_2);

            var result = gggg.SendMail(allMail);

            JbMailSendResult re = new JbMailSendResult {
                //IsOK = result[0].IsOK,

                
            };

                //JbMailSendResult
        }

        public object[] GetCustNOData(object[] objParam)
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
                string CustNO = parm[0];
                string sql = "SELECT d.SalesMasterNO,d.ItemSeq,d.CustNO,m.CustShortName,d.SalesID,d.SalesEmployeeID,d.SalesTypeID,";
                sql += "d.DMTypeID,d.SalesDate,Right(DATENAME(Weekday,d.SalesDate),1) as dWeekday,d.InvoiceYM,d.GrantTypeID,d.CustPrice,d.SalesQty,d.SalesQtyView,";
                sql += "d.CustAmt,d.Commission,d.PublishCount,d.PresentCount,d.PresentWNewsCount,d.SalesDescr,d.SalesDescrDate,";
                sql += "d.SalesDescrAlert,d.CustLines,d.OfficePrice,d.OfficeLines,d.OfficeAmt,d.NewsTypeID,d.NewsAreaID,";
                sql += "d.NewsPublishID,d.Sections,d.IsActive,d.IsSetInvoice,d.SalesOutLine,d.IsImport,d.CreateBy,";
                sql += "d.CreateDate,d.IsTransSys,d.InvoiceYMPoint,d.depositSeq,d.ViewAreaID,a.ViewAreaName,d.ContractDescr,d.IsInvoice,d.LastUpdateBy,d.LastUpdateDate,Isnull(d.depositOV,'') as depositOV,d.AFrameXY,d.BFrameXY,d.CFrameXY,'' as ButtonABC,case when(AFrameXY IS not null and AFrameXY!='')then AFrameXY when(BFrameXY IS not null and BFrameXY!='')then BFrameXY when(CFrameXY IS not null and CFrameXY!='')then CFrameXY end as FrameXY,t.SalesTypeName";
                sql += " FROM dbo.[ERPSalesDetails] d";
                sql += " inner join ERPSalesMaster m on d.SalesMasterNO=m.SalesMasterNO";
                sql += " left join ERPViewArea a on d.ViewAreaID=a.ViewAreaID";
                sql += " left join dbo.ERPCustomers c on d.CustNO=c.CustNO";
                sql += " left join ERPSalesType t on d.SalesTypeID = t.SalesTypeID";
                sql += " where d.CustNO ='" + CustNO + "'";
                sql += "order by SalesDate";
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
    }

}
