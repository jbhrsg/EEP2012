using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JBTool;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMStickStatus
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
        //�n�J���u���o�X�d���v��
        public object[] getSelectRange(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string UserID = parm[0];

            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " exec procDisplayFWCRMStickStatus '" + UserID + "'" + "\r\n";
                DataSet dsStickStatus = this.ExecuteSql(sql, connection, transaction);
                //string Status = dsStickStatus.Tables[0].Rows[0]["Status"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(dsStickStatus.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        public object[] getOrderData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesID = parm[0];
            string EmployerName = parm[1];
            string OrderNo = parm[2];
            string org_okno = parm[3];
            string OrderDate = parm[4];
            string OrderDate2 = parm[5];
            string NationalityID = parm[6];
            string D_STEP_ID = parm[7];
            string OrderYear = parm[8];

            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string SQL = " exec procDisplayOrderData '" + SalesID + "','" + EmployerName + "','" + OrderNo + "','" + org_okno + "','" + OrderDate + "','" + OrderDate2 + "','" + NationalityID + "','" + D_STEP_ID + "','" + OrderYear + "'" + "\r\n";

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
            }
            return new object[] { 0, js };
        }
        public object[] OrderDataAutoExcel(object[] objParam)
        {
            //var ParameterInput = TheJsonResult.GetParameterObj(objParam);
            string[] parm = objParam[0].ToString().Split(',');
            string SalesID = parm[0];
            string EmployerName = parm[1];
            string OrderNo = parm[2];
            string org_okno = parm[3];
            string OrderDate = parm[4];
            string OrderDate2 = parm[5];
            string NationalityID = parm[6];
            string D_STEP_ID = parm[7];
            string OrderYear = parm[8];

            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            var theResult = new Dictionary<string, object>();

            try
            {
                string SQL = " exec procDisplayOrderDataAutoExcel '" + SalesID + "','" + EmployerName + "','" + OrderNo + "','" + org_okno + "','" + OrderDate + "','" + OrderDate2 + "','" + NationalityID + "','" + D_STEP_ID + "','" + OrderYear + "'" + "\r\n";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();


                theResult.Add("FileStreamOrFileName", NPOIHelper.DataTableToExcel(ds.Tables[0]));

                theResult.Add("IsOK", true);
                theResult.Add("Msg", "���~�T��");
                theResult.Add("FileName", "�o�O�@���ɮ�.xls");

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };

            //var aTable = new DataTable();
            //aTable.Columns.Add("���u�s��");
            //aTable.Columns.Add("���u�m�W");
            //aTable.Rows.Add("0001", "���p��");



            
            //*
           



            //string[] parm = objParam[0].ToString().Split(',');
            //string SalesID = parm[0];
            //string EmployerName = parm[1];
            //string OrderNo = parm[2];
            //string org_okno = parm[3];
            //string OrderDate = parm[4];
            //string OrderDate2 = parm[5];
            //string NationalityID = parm[6];
            //string D_STEP_ID = parm[7];

            //string js = string.Empty;

            ////�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            ////��s�u���A������open�ɡA�}�ҳs��
            //if (connection.State != ConnectionState.Open)
            //{
            //    connection.Open();
            //}
            ////�}�ltransaction
            //IDbTransaction transaction = connection.BeginTransaction();

            //try
            //{
            //    string SQL = " exec procDisplayOrderData '" + SalesID + "','" + EmployerName + "','" + OrderNo + "','" + org_okno + "','" + OrderDate + "','" + OrderDate2 + "','" + NationalityID + "','" + D_STEP_ID + "'" + "\r\n";

            //    DataSet ds = this.ExecuteSql(SQL, connection, transaction);
            //    //// Indented�Y�� �N����ഫ��Json�榡
            //    js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            //    transaction.Commit();
            //}
            //catch
            //{
            //    transaction.Rollback();
            //}
            //finally
            //{
            //    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            //}
            //return new object[] { 0, js };
        }



    }
}
