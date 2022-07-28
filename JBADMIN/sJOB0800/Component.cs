using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;
using JBTool;

namespace sJOB0800
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
        // ���o�I���έp��T
        public object[] GePublishingRecordInfo(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string sCust = parm[2];
            string aAccount = parm[3];
            //�إ߸�Ʈw�s��

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //���n����
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";//localhost

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
        // ���o�I���έp��T-���e
        public object[] GePublishingRecordInfoData(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string sCust = parm[2];
            string aAccount = parm[3];
            string iType = parm[4];
            //�إ߸�Ʈw�s��

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //���n����
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingRecordData '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "','" + iType + "'";

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
        // ���o�s�i�έp
        public object[] GePublishingCount(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string IndustryId = parm[2];
            string CityId = parm[3];
            string TownId = parm[4];
            //�إ߸�Ʈw�s��

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //���n����
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingCount 1,'" + SDate + "','" + EDate + "','','" + IndustryId + "','" + CityId + "','" + TownId + "','','',''";

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
        // ���o�I���έp��T-���e
        public object[] GePublishingCountData(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string IndustryId = parm[2];
            string CityId = parm[3];
            string TownId = parm[4];
            string TeamID = "";
            string iYear = parm[5];
            string iMonth = parm[6];
            string SalesID = parm[7];
           
            if (SalesID == "null")
            {
                SalesID = "0";
            }
            //�إ߸�Ʈw�s��

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //���n����
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingRecord '" + SDate + "','" + EDate + "','" + sCust + "','" + aAccount + "'";
                string SQL = " exec procDisplayPublishingCount 2,'" + SDate + "','" + EDate + "','" + TeamID + "','" + IndustryId + "','" + CityId + "','" + TownId + "','" + iYear+"','"+iMonth+"','"+SalesID+"'";

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

        // ���o���ļs�i�Z�n��T
        public object[] GePublishingList(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];            
            //�إ߸�Ʈw�s��

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //���n����
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingList '" + SDate + "','" + EDate  + "'";
                string SQL = " exec procDisplayPublishingList '" + SDate + "','" + EDate + "'";

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

        // ���ļs�i�Z�n��T - �ץXExcel�U��
        public object[] PublishingListAutoExcel(object[] objParam)
        {
            //var ParameterInput = TheJsonResult.GetParameterObj(objParam);
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];

            string js = string.Empty;

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
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
                //���n����
                //string SQL = " exec [192.168.10.70].[0800JOB].dbo.procDisplayPublishingList '" + SDate + "','" + EDate  + "'";
                string SQL = " exec procDisplayPublishingListExcel '" + SDate + "','" + EDate + "'";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };           
        }


        public object[] PublishingAutoExcel(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string SDate = parm[0];
            string EDate = parm[1];
            string IndustryId = parm[2];
            string CityId = parm[3];
            string TownId = parm[4];

            //�إ߸�Ʈw�s��
            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
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
                string SQL = " exec procDisplayPublishingCount 3,'" + SDate + "','" + EDate + "','','" + IndustryId + "','" + CityId + "','" + TownId + "','','',''";

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
                ReleaseConnection(sLoginDB, connection);
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };

        }

        // ���ofb���K���T
        public object[] ProcessPublishingJobFB(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string IsMark = parm[0];
            string MarkDate = "";
            if (parm[1].ToString() != "")
            {
                 MarkDate=DateTime.Parse(parm[1].ToString()).ToShortDateString();
            }
            string cCode = parm[2];
            string siAutoKey = parm[3];
            string Type = parm[4];

            //�إ߸�Ʈw�s��

            string sLoginDB = "JOB0800";
            //�إ߸�Ʈw�s��
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            IDbConnection connection = (IDbConnection)AllocateConnection(sLoginDB);
            //IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                //Type 1�d��,3�аO
                string SQL = " exec procProcessPublishingJobFB " + Type + ",'" + cCode + "','" + IsMark + "','" + MarkDate + "','" + siAutoKey + "'";//localhost
                if (Type == "1")
                {
                    DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                    //// Indented�Y�� �N����ഫ��Json�榡
                    js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                }
                else
                {
                    this.ExecuteSql(SQL, connection, transaction);
                }
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
        // �Nfb���K���T�ץXExcel , Type = 2�ץXExcel
        public object[] PublishingJobFBExcel(object[] objParam)
        {
            string js = string.Empty;
            string[] parm = objParam[0].ToString().Split(',');
            string IsMark = parm[0];
            string siAutoKey = parm[1];

            //�إ߸�Ʈw�s��
            string sLoginDB = "JOB0800";
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
            {//Type 1�d��,2�ץXExcel,3�аO
                string SQL = " exec procProcessPublishingJobFB 2,'','','','" + siAutoKey + "'";

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
