using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_hymeetingHRLecture
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


        public object[] procHRLectureCreateSales(object[] objParam)
        {
            //string js = string.Empty;
            bool js;
            string ok = string.Empty;
            string sql = string.Empty;
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string InsGroupID = parm[0].ToString();
            string AutoKey = parm[1].ToString();
            string UserName = parm[2].ToString();
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                sql = "EXEC JBADMIN.dbo.procHRLectureCreateSales '" + InsGroupID + "','" + AutoKey + "','" + UserName + "'";
                this.ExecuteSql(sql, connection, transaction);
                transaction.Commit(); //當使用 transaction 時,需增加此Command
                js = true;
            }
            catch
            {
                transaction.Rollback();
                js = false;
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }

        //public object[] SelectCustomer(object[] objParam)
        //{
        //    string js;
        //    DataSet ds = new DataSet() ;
        //    DataTable dt;
        //    DataTable dt1;
        //    string sql = string.Empty;
        //    string sql1 = string.Empty;
            
        //    string[] parm = objParam[0].ToString().Split(',');
        //    List<String> whereList = new List<string>();
        //    List<String> whereList1 = new List<string>();
        //    string whereStr = string.Empty;
        //    string whereStr1 = string.Empty;

        //    string QInvoiceTypeID = parm[0].ToString().Trim();
        //    string TaxNO = parm[1].ToString().Trim();
        //    string Name = parm[2].ToString().Trim();
        //    Name = (Name.Length > 4) ? Name.Substring(0, 4) : Name;
        //    string Telephone = parm[3].ToString().Trim();
        //    string Cellphone = parm[4].ToString().Trim();
        //    string Company = parm[5].ToString().Trim();
        //    Company = (Company.Length > 4) ? Company.Substring(0, 4) : Company;

        //    IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }
        //    //開始transaction
        //    IDbTransaction transaction = connection.BeginTransaction();
        //    try
        //    {
        //        if (QInvoiceTypeID == "98") {//三聯
        //            if (TaxNO != "")
        //            {
        //                sql = "select * from Customer where TaxNO='" + TaxNO + "'";//and CustomerTypeID='1'
        //            }
        //        }
        //        else if (QInvoiceTypeID == "99")//二聯
        //        {

        //            //抓客戶ID
        //            if(Name!=""){
        //                whereList.Add("CustomerName like '%" + Name + "%'");
        //            }
        //            if(Telephone!=""){
        //                whereList.Add("TelNO like '" + Telephone + "'");
        //            }
        //            if(Cellphone!=""){
        //                whereList.Add("TelNO like '" + Cellphone + "'");
        //            }
        //            whereStr = string.Join(" or ", whereList);
        //            if (whereList.Count > 0)
        //            {
        //                sql = "select * from Customer where (" + whereStr + ") and CustomerTypeID='2'";
        //            }

        //            //抓雇主的客戶ID
        //            if (TaxNO != "")
        //            {
        //                whereList1.Add("TaxNO like '" + TaxNO + "'");
        //            }
        //            if (Company != "")
        //            {
        //                whereList1.Add("CustomerName like '%" + Company + "%'");
        //            }
        //            whereStr1 = string.Join(" or ", whereList1);
        //            if (whereList1.Count > 0){
        //                sql1 = "select * from Customer where (" + whereStr1 + ") and CustomerTypeID='1'";
        //            }
        //        }

        //        if (sql != string.Empty)
        //        {
        //             dt = this.ExecuteSql(sql, connection, transaction).Tables[0];
        //             dt.TableName = "tb1";
        //             ds.Tables.Add(dt.Copy());
        //        }
        //        if (sql1 != string.Empty) {
        //             dt1 = this.ExecuteSql(sql1, connection, transaction).Tables[0];
        //             dt1.TableName = "tb2";
        //             ds.Tables.Add(dt1.Copy());
        //        }
        //        if (sql != string.Empty || sql1 != string.Empty){
        //            transaction.Commit();
        //        }
        //        js = JsonConvert.SerializeObject(ds, Formatting.Indented);
        //    }
        //    catch(Exception ex)
        //    {
        //        transaction.Rollback();
        //        return new object[] { 0, false };
        //    }
        //    finally
        //    {
        //        ReleaseConnection("JBERP", connection);
        //    }
        //    return new object[] { 0, js };
        //}

        //public object[] procHRLectureCreateCustomer(object[] objParam)
        //{
        //    string js=string.Empty;
        //    string sql = string.Empty;
        //    string sql1 = string.Empty;
        //    string[] parm = objParam[0].ToString().Split(',');

        //    string AutoKey = parm[0].ToString().Trim();
        //    string UserName = parm[1].ToString();
        //    string Who = parm[2].ToString();

        //    IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }
        //    //開始transaction
        //    IDbTransaction transaction = connection.BeginTransaction();
        //    try
        //    {

        //        sql = "EXEC JBADMIN.dbo.procHRLectureCreateCustomer '" + AutoKey + "','" + UserName + "','" + Who + "'";
        //        int result = this.ExecuteCommand(sql, connection, transaction);
        //        transaction.Commit(); //當使用 transaction 時,需增加此Command

        //        if (result == 1)//有insert會回傳1
        //        {
        //            sql1 = "select top 1 * from JBERP.dbo.Customer order by CreateDate desc";
        //            DataTable tb = this.ExecuteSql(sql1, connection, transaction).Tables[0];
        //            js = JsonConvert.SerializeObject(tb, Formatting.Indented);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        transaction.Rollback();
        //        return new object[] { 0, false };
        //    }
        //    finally
        //    {
        //        ReleaseConnection("JBADMIN", connection);
        //    }
        //    return new object[] { 0, js };
        //}
    }
}
