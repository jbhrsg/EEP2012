using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace _HRM_Attend_Share
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

        public object[] checkAttendDataLock(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string employeeID = parm[0];
            string beginDate = parm[1];
            string endDate = parm[2];

            string js = string.Empty;


            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) AS cnt from HRM_ATTEND_DATA_LOCK " + "\r\n";
                sql = sql + " where GROUP_ID = dbo.fnHRM_BASE_BASETTS_FieldName('" + employeeID + "'";
                sql = sql + ",HRM_ATTEND_DATA_LOCK.ATTEND_DATE,'GROUP_ID')" + "\r\n";
                if (beginDate == endDate)
                    sql = sql + " and ATTEND_DATE = '" + beginDate + "'";
                else
                    sql = sql + " and ATTEND_DATE BETWEEN '" + beginDate + "' AND '" + endDate + "'";

                DataSet dsHRM_ATTEND_DATA_LOCK = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsHRM_ATTEND_DATA_LOCK.Tables[0].Rows[0]["cnt"].ToString();

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection("JBHR_EEP", connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] getDialogData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string type = parm[0];
            string listData = "";
            if (parm[1] != "")
            {
                for (var i = 1; i <= parm.Length - 1; i++)
                {
                    if (i == parm.Length - 1)
                        listData = listData + "'" + parm[i] + "'";
                    else
                        listData = listData + "'" + parm[i] + "', ";
                }
            }

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection("JBHR_EEP");

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                var user = GetClientInfo(ClientInfoType.LoginUser);
                var sql = "";

                switch (type)
                {
                    case "Employee":
                        if (listData != "")
                        {
                            sql = "Select B.EMPLOYEE_ID,B.EMPLOYEE_CODE,B.NAME_C,'N' AS IS_SELECTED From [dbo].[HRM_BASE_BASE] as B Inner Join ";
                            sql = sql + " (Select EMPLOYEE_ID From [dbo].[dtHRM_BASE_BASEIO_DateAndMan](null,GetDate()) Where ACTION_TYPE = 1 )";
                            sql = sql + " as BIO on B.EMPLOYEE_ID = BIO.EMPLOYEE_ID ";
                            sql = sql + " WHERE B.EMPLOYEE_CODE+'-'+B.NAME_C NOT IN (" + listData + ")";
                            sql = sql + " union ";
                            sql = sql + " Select B.EMPLOYEE_ID,B.EMPLOYEE_CODE,B.NAME_C,'Y' AS IS_SELECTED From [dbo].[HRM_BASE_BASE] as B Inner Join ";
                            sql = sql + " (Select EMPLOYEE_ID From [dbo].[dtHRM_BASE_BASEIO_DateAndMan](null,GetDate()) Where ACTION_TYPE = 1 )";
                            sql = sql + " as BIO on B.EMPLOYEE_ID = BIO.EMPLOYEE_ID ";
                            sql = sql + " WHERE B.EMPLOYEE_CODE+'-'+B.NAME_C IN (" + listData + ")";
                        }
                        else
                        {
                            sql = "Select B.EMPLOYEE_ID,B.EMPLOYEE_CODE,B.NAME_C,'N' AS IS_SELECTED From [dbo].[HRM_BASE_BASE] as B Inner Join ";
                            sql = sql + " (Select EMPLOYEE_ID From [dbo].[dtHRM_BASE_BASEIO_DateAndMan](null,GetDate()) Where ACTION_TYPE = 1 )";
                            sql = sql + " as BIO on B.EMPLOYEE_ID = BIO.EMPLOYEE_ID ";
                        }
                        break;

                    case "Dept":
                        if (listData != "")
                        {
                            sql = "select DEPT_ID,DEPT_CODE,DEPT_CNAME,'N' AS IS_SELECTED from HRM_DEPT ";
                            sql = sql + " WHERE GETDATE() BETWEEN BEGIN_EFFECTIVE_DATE AND END_EFFECTIVE_DATE ";
                            sql = sql + " AND DEPT_CODE+'-'+DEPT_CNAME NOT IN (" + listData + ")";
                            sql = sql + " union ";
                            sql = sql + " select DEPT_ID,DEPT_CODE,DEPT_CNAME,'Y' AS IS_SELECTED from HRM_DEPT";
                            sql = sql + " WHERE GETDATE() BETWEEN BEGIN_EFFECTIVE_DATE AND END_EFFECTIVE_DATE ";
                            sql = sql + " AND DEPT_CODE+'-'+DEPT_CNAME IN (" + listData + ")";
                            sql = sql + " ORDER BY DEPT_CODE";
                        }
                        else
                        {
                            sql = "select DEPT_ID,DEPT_CODE,DEPT_CNAME,'N' AS IS_SELECTED from HRM_DEPT ";
                            sql = sql + " WHERE GETDATE() BETWEEN BEGIN_EFFECTIVE_DATE AND END_EFFECTIVE_DATE ";
                            sql = sql + " ORDER BY DEPT_CODE";
                        }
                        break;
                    case "Rote":
                        if (listData != "")
                        {
                            sql = "select ROTE_ID,ROTE_CODE,ROTE_CNAME,SEQ,'N' AS IS_SELECTED from HRM_ATTEND_ROTE ";
                            sql = sql + " WHERE ROTE_CODE+'-'+ROTE_CNAME NOT IN (" + listData + ")";
                            sql = sql + " union ";
                            sql = sql + " select ROTE_ID,ROTE_CODE,ROTE_CNAME,SEQ,'Y' AS IS_SELECTED from HRM_ATTEND_ROTE";
                            sql = sql + " WHERE ROTE_CODE+'-'+ROTE_CNAME IN (" + listData + ")";
                            sql = sql + " ORDER BY SEQ";
                        }
                        else
                        {
                            sql = "select ROTE_ID,ROTE_CODE,ROTE_CNAME,SEQ,'N' AS IS_SELECTED from HRM_ATTEND_ROTE ";
                            sql = sql + " ORDER BY SEQ";
                        }
                        break;
                    default:

                        break;
                }

                DataSet dsInsuranc = this.ExecuteSql(sql, connection, transaction);

                //Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(dsInsuranc.Tables[0], Formatting.Indented);
                transaction.Commit();
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
    }
}
