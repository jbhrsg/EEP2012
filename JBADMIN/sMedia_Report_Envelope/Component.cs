using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sMedia_Report_Envelope
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

        public object[] GetERPCustomers(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            List<string> CustNOList=new  List<string>();
            string sCustNO = "";
            string[] arrCustNO = CustNO.Split('*');

            for (int i = 0; i < arrCustNO.Length; i++)
            {
                CustNOList.Add("'" + arrCustNO[i] + "'");
            }
            sCustNO = String.Join(",", CustNOList.ToArray());
            

            string sql = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                sql = sql + "SELECT distinct CustNO,CustName,CustPost,CustAddr,ContactA,lt.LetterTypeName" + "\r\n";
                sql = sql + "FROM ERPCustomers" + "\r\n";
                sql = sql + "left join dbo.ERPLetterType lt on lt.LetterTypeID=ERPCustomers.[LetterType]" + "\r\n";
                sql = sql + "where CustNO in(" + sCustNO + ")" + "\r\n";
                sql = sql + "order by CustNO" + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch (Exception ex)
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



        public object[] GetERPCustomers_VisitRecord(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            List<string> CustNOList=new  List<string>();
            string sCustNO = "";
            string[] arrCustNO = CustNO.Split('*');

            for (int i = 0; i < arrCustNO.Length; i++)
            {
                CustNOList.Add("'" + arrCustNO[i].Trim() + "'");
            }
            sCustNO = String.Join(",", CustNOList.ToArray());
            

            string sql = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {

sql = sql + "SELECT  a.CustNO,a.CustName,a.CustAddr,a.CustTelNO,a.IndustryID,jt.jb_name,a.IndustryType,mi.[ListContent] as IndustryName,a.SalesID,b.SalesName,a.SalesID,b.SalesName" + "\r\n";
sql = sql + ",a.ContactA,a.ContactAMail,a.ContactAJobID,a.ContactASubTel,c.CustJobName" + "\r\n";
sql = sql + ",a.ContactB,a.ContactBMail,a.ContactBJobID,a.ContactBSubTel,c1.CustJobName as CustJobNameB" + "\r\n";
sql = sql + ",a.iPeopleCount,a.iPeopleFCount" + "\r\n";
sql = sql + ",case LatelyDayP when '1900-01-01 00:00:00.000' then null else LatelyDayP end as LatelyDayP" + "\r\n";
sql = sql + ",case LatelyDayW when '1900-01-01 00:00:00.000' then null else LatelyDayW end as LatelyDayW" + "\r\n";
sql = sql + ",case LatelyDayN when '1900-01-01 00:00:00.000' then null else LatelyDayN end as LatelyDayN" + "\r\n";
sql = sql + ",(select top 1 ISNULL('實訪日'+convert(varchar, NotesCreateDate, 111)+' - ','')+ISNULL(Notes,'') from ERPCustomerToDoNotes where CustNO=a.CustNO and (Notes is not null and Notes !='') order by NotesCreateDate desc) as Notes" + "\r\n";
sql = sql + ",(select top 1 '男:'+convert(nvarchar,ISNULL(cn.male,0))+' 女:'+convert(nvarchar,ISNULL(cn.female,0))+' 國別:'+n.NationalityName from dbo.ERPCustNationality cn left join dbo.ERPNationality n on cn.NationalityNo=n.NationalityNo where cn.CustNO=a.CustNO and cn.NationalityNo='01') as [fs]" + "\r\n";
sql = sql + ",(select top 1 '男:'+convert(nvarchar,ISNULL(cn.male,0))+' 女:'+convert(nvarchar,ISNULL(cn.female,0))+' 國別:'+n.NationalityName from dbo.ERPCustNationality cn left join dbo.ERPNationality n on cn.NationalityNo=n.NationalityNo where cn.CustNO=a.CustNO and cn.NationalityNo='02') as [ts]" + "\r\n";
sql = sql + ",(select top 1 '男:'+convert(nvarchar,ISNULL(cn.male,0))+' 女:'+convert(nvarchar,ISNULL(cn.female,0))+' 國別:'+n.NationalityName from dbo.ERPCustNationality cn left join dbo.ERPNationality n on cn.NationalityNo=n.NationalityNo where cn.CustNO=a.CustNO and cn.NationalityNo='03') as [is]" + "\r\n";
sql = sql + ",(select top 1 '男:'+convert(nvarchar,ISNULL(cn.male,0))+' 女:'+convert(nvarchar,ISNULL(cn.female,0))+' 國別:'+n.NationalityName from dbo.ERPCustNationality cn left join dbo.ERPNationality n on cn.NationalityNo=n.NationalityNo where cn.CustNO=a.CustNO and cn.NationalityNo='04') as [vs]" + "\r\n";
sql = sql + ",(select COUNT(CustNO) as counts from dbo.ERPCustNationality  where CustNO=a.CustNO group by CustNO) as ncounts" + "\r\n";
sql = sql + ",ForeignCompany,ForeignDorm" + "\r\n";//仲介公司,外勞宿舍
                //sql = sql + ",(select count( from [211.78.84.42].[JBADMIN].dbo.ERPCustNationality cn where cn.CustNO=a.CustNO) as [ncounts]" + "\r\n";
sql = sql + "FROM ERPCustomers a" + "\r\n";
sql = sql + "left join DBO.ERPCUSTJOB as C on (A.ContactAJobID =C.CustJobID)" + "\r\n";
sql = sql + "left join DBO.ERPCUSTJOB as C1 on (A.ContactBJobID =C1.CustJobID)" + "\r\n";
sql = sql + "left join DBO.jb_type as jt on a.IndustryID=jt.jb_type" + "\r\n";
sql = sql + "left join [ERPReferenceTable] mi on a.IndustryType=mi.[ListID] and mi.[ListCategory]='IndustryType' and mi.[IsActive]=1" + "\r\n";
sql = sql + "left join DBO.ERPSALESMAN as B on (A.SALESID=B.SALESID)" + "\r\n";
sql = sql + "where CustNO in(" + sCustNO + ")" + "\r\n";
sql = sql + "order by CustNO" + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch (Exception ex)
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


        public object[] GetERPCustomers_VisitCustList(object[] objParam)
        {
            string js = "";

            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            List<string> CustNOList=new  List<string>();
            string sCustNO = "";
            string[] arrCustNO = CustNO.Split('*');

            for (int i = 0; i < arrCustNO.Length; i++)
            {
                CustNOList.Add("'" + arrCustNO[i] + "'");
            }
            sCustNO = String.Join(",", CustNOList.ToArray());
            

            string sql = "";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {

sql = sql + "SELECT  a.CustNO,a.CustName,a.CustFaxNO,a.ContactA,c.CustJobName,a.ContactAMail" + "\r\n";
sql = sql + ",case LatelyDayP when '1900-01-01 00:00:00.000' then null else left(convert(varchar,LatelyDayP,120),10) end as LatelyDayP" + "\r\n";
sql = sql + ",case LatelyDayN when '1900-01-01 00:00:00.000' then null else left(convert(varchar,LatelyDayN,120),10) end as LatelyDayN" + "\r\n";
sql = sql + ",a.CustAddr" + "\r\n";
sql = sql + "FROM ERPCustomers a" + "\r\n";
sql = sql + "left join DBO.ERPCUSTJOB as C on (A.ContactAJobID =C.CustJobID)" + "\r\n";
sql = sql + "where CustNO in(" + sCustNO + ")" + "\r\n";
sql = sql + "order by CustNO" + "\r\n";

                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch (Exception ex)
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
    }
}
