using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMARMaster
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

        //EEP�~�ҿ�J�~�d�Ҹ��d�����ϥ�
        public object[] procReportARMaster(object[] objParam)
        {
            string ResidenceID = objParam[0].ToString();           
            string js = string.Empty;
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
                string SQL = "exec procReportARMasterEEPMaster '" + ResidenceID + "'";
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                //string SQL = "exec procReportARMasterEEPMaster '" + ResidenceID + "'";
                //DataSet ds = this.ExecuteSql(SQL, connection, transaction);

                //string SQL2 = "exec procReportARMasterEEP '" + ResidenceID + "'";
                //if (ds.Tables[0].Rows[0]["iCount"].ToString() == "0")
                //{
                //    DataSet ds2 = this.ExecuteSql(SQL2, connection, transaction);
                //}
                //else
                //{
                //    DataSet ds2 = this.ExecuteSql(SQL2, connection, transaction);

                //}




            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }



    }
}
