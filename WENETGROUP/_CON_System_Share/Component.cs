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
using JBTool;
using System.Collections;
using System.Data.SqlClient;


namespace _CON_System_Share
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

        public object[] checkRowCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string tableName = parm[0];
            string id = parm[1];

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "exec proc_GetRowCount '" + tableName + "','" + id + "'";

                DataSet dsRowCount = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsRowCount.Tables[0].Rows[0]["cnt"].ToString();

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] checkShareCodeRowCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string fieldName = parm[0];
            string codeID = parm[1];

            string js = string.Empty;

            //建立資料庫連結
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //當連線狀態不等於open時，開啟連結
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //開始transaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "exec proc_GetShareCodeRowCount '" + fieldName + "','" + codeID + "'";

                DataSet dsRowCount = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsRowCount.Tables[0].Rows[0]["cnt"].ToString();

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
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }

        public object[] ExcelGetTitleName(object[] objParam)
        {
            try
            {
                var FilePathName = TheJsonResult.GetParameterStr(objParam);

                if (string.IsNullOrEmpty(FilePathName)) return new object[] { 0, new TheJsonResult { ErrorMsg = "檔案開啟路徑遺失" }.ToJsonString() };

                //FilePathName = string.Format("{0}/{1}", "../JQWebClient", FilePathName);

                var aHeadRowList = NPOIHelper.GetHeadRowList(FilePathName, 0);
                if (aHeadRowList == null) return new object[] { 0, new TheJsonResult { ErrorMsg = "讀取失敗" }.ToJsonString() };

                var OrderList = aHeadRowList.Select(m => new { text = m.Key, value = m.Value }).ToList().OrderBy(m => m.value);
                return new object[] { 0, new TheJsonResult { IsOK = true, Result = OrderList }.ToJsonString() };
            }
            catch (Exception) { return new object[] { 0, new TheJsonResult { ErrorMsg = "執行錯誤" }.ToJsonString() }; }
        }

        //縣市
        public object[] getCity(object[] objParam)
        {
            string SQL = @"
Select	CITY_CODE	as [Value],
		Min(CITY)	as [Text]
From	[dbo].[HRM_ADDRESS]
Group By CITY_CODE
Order By CITY_CODE
";
            var aDataSet = SQL_Tools.GetDataSet(this, SQL, new ArrayList());
            if (aDataSet != null && aDataSet.Tables.Count > 0) return new object[] { 0, new TheJsonResult { IsOK = true, Result = aDataSet.Tables[0] }.ToJsonString() };
            else return new object[] { 0, new TheJsonResult { IsOK = true, ErrorMsg = "執行錯誤" }.ToJsonString() };
        }

        //鄉鎮
        public object[] getCountry(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterStr(objParam);

            string SQL = @"
Select	COUNTRY_CODE	as [Value],
		Min(COUNTRY)	as [Text]
From	[dbo].[HRM_ADDRESS]
Where	CITY_CODE = @City
Group By COUNTRY_CODE
Order By COUNTRY_CODE
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@City", Parameter_Input));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            var aDataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
            if (aDataSet != null && aDataSet.Tables.Count > 0) return new object[] { 0, new TheJsonResult { IsOK = true, Result = aDataSet.Tables[0] }.ToJsonString() };
            else return new object[] { 0, new TheJsonResult { IsOK = true, ErrorMsg = "執行錯誤" }.ToJsonString() };
        }

        //路名
        public object[] getRoad(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterStr(objParam);

            string SQL = @"
Select	ROAD	as [Text],
		ID		as [Value]
From	[dbo].[HRM_ADDRESS]
Where	COUNTRY_CODE = @Country
";
            var Parameter = new ArrayList();
            Parameter.Add(new SqlParameter("@Country", Parameter_Input));
            foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

            var aDataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
            if (aDataSet != null && aDataSet.Tables.Count > 0) return new object[] { 0, new TheJsonResult { IsOK = true, Result = aDataSet.Tables[0] }.ToJsonString() };
            else return new object[] { 0, new TheJsonResult { IsOK = true, ErrorMsg = "執行錯誤" }.ToJsonString() };
        }
    }
}
