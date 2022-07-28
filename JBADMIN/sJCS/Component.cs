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

namespace sJCS1
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

      
        //�J��H���W�U
        public object[] procReportRoomerList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string DormID = parm[0].ToString();
            string CustID = parm[1].ToString();
            string TranType = parm[2].ToString();
            string InOutStatus = parm[3].ToString();
            string RoomID = parm[4].ToString();   

            string js = string.Empty;
            string sLoginDB = DormID;

            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
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
                string SQL = "exec procReportRoomerList '" + CustID + "','" + TranType + "','" + InOutStatus + "','" + RoomID + "'";
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }

        //���i�X���Ƥp�󵥩�10���H���W�U
        public object[] procReportRoomerCardDetails(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string DormID = parm[0].ToString();
            string CustID = parm[1].ToString();
            string sYM = parm[2].ToString();           

            string js = string.Empty;
            string sLoginDB = DormID;

            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
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
                string SQL = "exec procReportRoomerCardDetails '" + sYM + "','" + CustID + "'" ;
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }

        //�U�ж��J��Ȥ�έp
        public object[] procReportRoomCustList(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string DormID = parm[0].ToString();
            string CustID = parm[1].ToString();
            string RoomID = parm[2].ToString();

            string js = string.Empty;
            string sLoginDB = DormID;

            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
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
                string SQL = "exec procReportRoomCustList '" + CustID + "','" + RoomID + "'";
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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, js };
        }


    }
}
