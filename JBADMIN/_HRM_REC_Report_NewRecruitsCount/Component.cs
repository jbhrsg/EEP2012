using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace _HRM_REC_Report_NewRecruitsCount
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
        //���έp�� --------------------------------------------------------------------------------
        public object[] procReportNewRecruitsCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string sDate = parm[0].ToString();
            string eDate = parm[1].ToString();
            string SalesTeam = parm[2].ToString();
            string ServiceConsultants = parm[3].ToString();
            string ContactPeople = parm[4].ToString();
            string iType = parm[5].ToString();

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            string sLoginDB = "JBHRIS_DISPATCH";
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
                string SQL = "exec procReportNewRecruitsCount '" + sDate + "','" + eDate + "','" + SalesTeam + "','" + ServiceConsultants + "','" + ContactPeople + "'," + iType;
                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(sLoginDB, connection);
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }


    }
}
