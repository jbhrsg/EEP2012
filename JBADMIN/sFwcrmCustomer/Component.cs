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

        //--------------------------------�ǳ�----------------------------------------------------------------------------------

        private void ucEmployer_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployer.SetFieldValue("LastUpdateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployer.SetFieldValue("LastUpdateBy", LoginUser);//�����
        }

        private void ucEmployer_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmployerID = ucEmployer.GetFieldCurrentValue("EmployerID").ToString();//�Ȥ�N��              
            string EmployerFee = ucEmployer.GetFieldCurrentValue("EmployerFee").ToString();//���D�t��           
            string EmployerFee2 = ucEmployer.GetFieldCurrentValue("EmployerFee2").ToString();//���D�N��        
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "FWCRM";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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

        //--------------------------------�ǫH----------------------------------------------------------------------------------

        private void ucEmployerJS_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployerJS.SetFieldValue("LastUpdateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployerJS.SetFieldValue("LastUpdateBy", LoginUser);//�����
        }

        private void ucEmployerJS_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmployerID = ucEmployerJS.GetFieldCurrentValue("EmployerID").ToString();//�Ȥ�N��              
            string EmployerFee = ucEmployerJS.GetFieldCurrentValue("EmployerFee").ToString();//���D�t��           
            string EmployerFee2 = ucEmployerJS.GetFieldCurrentValue("EmployerFee2").ToString();//���D�N��        
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "FWCRMJS";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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

        //--------------------------------�ǫH�a�A----------------------------------------------------------------------------------

        private void ucEmployerJSCare_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucEmployerJSCare.SetFieldValue("LastUpdateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucEmployerJSCare.SetFieldValue("LastUpdateBy", LoginUser);//�����
        }

        private void ucEmployerJSCare_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            string EmployerID = ucEmployerJSCare.GetFieldCurrentValue("EmployerID").ToString();//�Ȥ�N��              
            string EmployerFee = ucEmployerJSCare.GetFieldCurrentValue("EmployerFee").ToString();//���D�t��           
            string EmployerFee2 = ucEmployerJSCare.GetFieldCurrentValue("EmployerFee2").ToString();//���D�N��        
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

            string sLoginDB = "FWCRMJSCare";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
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
