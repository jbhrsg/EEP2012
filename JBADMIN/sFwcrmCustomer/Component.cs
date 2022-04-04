using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;

namespace sFwcrmCustomer
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

        //--------------------------------傑報----------------------------------------------------------------------------------

        private void ucEmployer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployer.SetFieldValue("LastUpdateDate", DateTime.Now);//欄位賦值
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployer.SetFieldValue("LastUpdateBy", LoginUser);//欄位賦值
        }

        private void ucEmployer_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmployerID = ucEmployer.GetFieldCurrentValue("EmployerID").ToString();//客戶代號              
            string EmployerFee = ucEmployer.GetFieldCurrentValue("EmployerFee").ToString();//雇主負擔           
            string EmployerFee2 = ucEmployer.GetFieldCurrentValue("EmployerFee2").ToString();//雇主代扣        
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "FWCRM";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();


            try
            {               
                    string SQL = "exec dbo.procUpdateEmployerFee '" + @EmployerID + "','" + EmployerFee + "','" + LoginUser + "'";
                    this.ExecuteSql(SQL, connection, transaction);
               
                    string SQL2 = "exec dbo.procUpdateEmployerFee2 '" + @EmployerID + "','" + EmployerFee2 + "','" + LoginUser + "'";
                    this.ExecuteSql(SQL2, connection, transaction);

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);

            }
        }

        //--------------------------------傑信----------------------------------------------------------------------------------

        private void ucEmployerJS_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployerJS.SetFieldValue("LastUpdateDate", DateTime.Now);//欄位賦值
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployerJS.SetFieldValue("LastUpdateBy", LoginUser);//欄位賦值
        }

        private void ucEmployerJS_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmployerID = ucEmployerJS.GetFieldCurrentValue("EmployerID").ToString();//客戶代號              
            string EmployerFee = ucEmployerJS.GetFieldCurrentValue("EmployerFee").ToString();//雇主負擔           
            string EmployerFee2 = ucEmployerJS.GetFieldCurrentValue("EmployerFee2").ToString();//雇主代扣        
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "FWCRMJS";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();


            try
            {
                string SQL = "exec dbo.procUpdateEmployerFee '" + @EmployerID + "','" + EmployerFee + "','" + LoginUser + "'";
                this.ExecuteSql(SQL, connection, transaction);

                string SQL2 = "exec dbo.procUpdateEmployerFee2 '" + @EmployerID + "','" + EmployerFee2 + "','" + LoginUser + "'";
                this.ExecuteSql(SQL2, connection, transaction);

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);

            }
        }

        //--------------------------------傑信家服----------------------------------------------------------------------------------

        private void ucEmployerJSCare_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployerJSCare.SetFieldValue("LastUpdateDate", DateTime.Now);//欄位賦值
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployerJSCare.SetFieldValue("LastUpdateBy", LoginUser);//欄位賦值
        }

        private void ucEmployerJSCare_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmployerID = ucEmployerJSCare.GetFieldCurrentValue("EmployerID").ToString();//客戶代號              
            string EmployerFee = ucEmployerJSCare.GetFieldCurrentValue("EmployerFee").ToString();//雇主負擔           
            string EmployerFee2 = ucEmployerJSCare.GetFieldCurrentValue("EmployerFee2").ToString();//雇主代扣        
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "FWCRMJSCare";
            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();


            try
            {
                string SQL = "exec dbo.procUpdateEmployerFee '" + @EmployerID + "','" + EmployerFee + "','" + LoginUser + "'";
                this.ExecuteSql(SQL, connection, transaction);

                string SQL2 = "exec dbo.procUpdateEmployerFee2 '" + @EmployerID + "','" + EmployerFee2 + "','" + LoginUser + "'";
                this.ExecuteSql(SQL2, connection, transaction);

                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);

            }
        }
    }
}
