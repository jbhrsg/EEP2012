using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace sJBRecruitEmpSalary
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
        public object[] GetCustomerID(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var SYearMonth = (Parameter_Input["SYearMonth"]);
                var EYearMonth = (Parameter_Input["EYearMonth"]);
                try { }
                catch (Exception) {  }
                string SQL = @" Select DISTINCT A.CustomerID,B.CustomerShortName 
                                From JBRecruit.dbo.PayMaster A,JBRecruit.dbo.Customer B
                                Where  A.CustomerID=B.CustomerID  And Substring(A.Yearmonth,1,6) >= @SYearMonth AND Substring(A.Yearmonth,1,6) <= @EYearMonth
                                Order By  CustomerShortName";  
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@SYearMonth", SYearMonth));
                Parameter.Add(new SqlParameter("@EYearMonth", EYearMonth));
                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("CustomerID"),
                    text = m.Field<string>("CustomerShortName") ?? ""
                }).ToList();
                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = false;
                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        public object[] GetCustomerID_Period(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var SYearMonth = (Parameter_Input["SYearMonth"]);
                var EYearMonth = (Parameter_Input["EYearMonth"]);
                try { }
                catch (Exception) { }
                string SQL = @" Select DISTINCT A.CustomerID,B.CustomerShortName 
                                From  JBRecruit.dbo.PayMaster A,JBRecruit.dbo.Customer B
                                Where  A.CustomerID=B.CustomerID  And Substring(A.Yearmonth,1,6) >= @SYearMonth AND Substring(A.Yearmonth,1,6) <= @SYearMonth
                                Order By  CustomerShortName";
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@SYearMonth", SYearMonth));
                Parameter.Add(new SqlParameter("@EYearMonth", EYearMonth));
                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("CustomerID"),
                    text = m.Field<string>("CustomerShortName") ?? ""
                }).ToList();
                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = false;
                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //取得薪資代號
        public object[] GetSaryID(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var SYearMonth = (Parameter_Input["SYearMonth"]);
                var EYearMonth = (Parameter_Input["EYearMonth"]);
                try { }
                catch (Exception) { }
                string SQL = @" SELECT '0000' AS SaryID,'  選擇薪項  ' AS SaryName
                                UNION
                                SELECT  DISTINCT A.SaryID,C.SaryName  
                                FROM  JBRECRUIT.DBO.PAYDETAILS A,JBRECRUIT.DBO.PAYMASTER B,JBRECRUIT.DBO.SALARYBASE C
                                WHERE A.PAYID=B.PAYID  AND A.SARYID=C.SARYID  AND SUBSTRING(B.YEARMONTH,1,6) >= @SYearMonth AND SUBSTRING(B.YEARMONTH,1,6) <= @EYearMonth
                                ORDER BY SARYNAME";
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@SYearMonth", SYearMonth));
                Parameter.Add(new SqlParameter("@EYearMonth", EYearMonth));
                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("SaryID"),
                     text = m.Field<string>("SaryName") ?? ""
                }).ToList();
                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = false;
                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        } //取得出勤代號
        public object[] GetDutyID(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var SYearMonth = (Parameter_Input["SYearMonth"]);
                var EYearMonth = (Parameter_Input["EYearMonth"]);
                try { }
                catch (Exception) { }
                string SQL = @" SELECT '0000' AS SaryID,'  選擇差勤  ' AS DutyName
                                UNION
                                SELECT  DISTINCT A.SaryID,C.DutyName 
                                FROM  JBRECRUIT.DBO.PAYDETAILS A,JBRECRUIT.DBO.PAYMASTER B,JBRECRUIT.DBO.SALARYBASE C
                                WHERE A.PAYID=B.PAYID  AND A.SARYID=C.SARYID  AND SUBSTRING(B.YEARMONTH,1,6) >= @SYearMonth AND SUBSTRING(B.YEARMONTH,1,6) <= @EYearMonth
                                      AND C.DutyName Is not Null   
                                ORDER BY DutyName";
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@SYearMonth", SYearMonth));
                Parameter.Add(new SqlParameter("@EYearMonth", EYearMonth));
                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("SaryID"),
                    text = m.Field<string>("DutyName") ?? ""
                }).ToList();
                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = false;
                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //取得條件薪資代號
        public object[] GetSaryID_Period(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var SYearMonth = (Parameter_Input["SYearMonth"]);
                var EYearMonth = (Parameter_Input["EYearMonth"]);
                var CustomerID = (Parameter_Input["CustomerID"]);
                try { }
                catch (Exception) { }
                string SQL = @" SELECT '0000' AS SaryID,'  選擇薪項  ' AS SaryName
                                UNION
                                SELECT  DISTINCT A.SaryID,C.SaryName  
                                FROM  JBRECRUIT.DBO.PAYDETAILS A,JBRECRUIT.DBO.PAYMASTER B,JBRECRUIT.DBO.SALARYBASE C
                                WHERE A.PAYID=B.PAYID  AND A.SARYID=C.SARYID  AND SUBSTRING(B.YEARMONTH,1,6) >= @SYearMonth  And  SUBSTRING(B.YEARMONTH,1,6) <= @EYearMonth
                                AND B.CustomerID=@CustomerID ORDER BY SARYNAME";
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@SYearMonth", SYearMonth));
                Parameter.Add(new SqlParameter("@EYearMonth", EYearMonth));
                Parameter.Add(new SqlParameter("@CustomerID", CustomerID));
                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("SaryID"),
                    text = m.Field<string>("SaryName") ?? ""
                }).ToList();
                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = false;
                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //取得條件差勤代號
        public object[] GetDutyID_Period(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var SYearMonth = (Parameter_Input["SYearMonth"]);
                var EYearMonth = (Parameter_Input["EYearMonth"]);
                var CustomerID = (Parameter_Input["CustomerID"]);
                try { }
                catch (Exception) { }
                string SQL = @" SELECT '0000' AS SaryID,'  選擇差勤  ' AS DutyName
                                UNION
                                SELECT  DISTINCT A.SaryID,C.DutyName   
                                FROM  JBRECRUIT.DBO.PAYDETAILS A,JBRECRUIT.DBO.PAYMASTER B,JBRECRUIT.DBO.SALARYBASE C
                                WHERE A.PAYID=B.PAYID  AND A.SARYID=C.SARYID  AND SUBSTRING(B.YEARMONTH,1,6) >= @SYearMonth  And  SUBSTRING(B.YEARMONTH,1,6) <= @EYearMonth
                                AND C.DutyName Is Not Null 
                                AND B.CustomerID=@CustomerID ORDER BY DutyNAME";
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@SYearMonth", SYearMonth));
                Parameter.Add(new SqlParameter("@EYearMonth", EYearMonth));
                Parameter.Add(new SqlParameter("@CustomerID", CustomerID));
                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);
                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("SaryID"),
                    text = m.Field<string>("DutyName") ?? ""
                }).ToList();
                //預設第一筆
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = false;
                //回傳
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //取得薪資代號    
        public object[] GetSaryID_Select(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string SYearMonth = parm[0];
                string EYearMonth = parm[1];
                string CustomerID = parm[2];
                string sql = @"SELECT DISTINCT A.SaryID,C.SaryName,D.SaryPayTypeName,C.SaryPayType
                               FROM JBRECRUIT.DBO.PAYDETAILS A,JBRECRUIT.DBO.PAYMASTER B,JBRECRUIT.DBO.SALARYBASE C,JBRECRUIT.DBO.SARYPAYTYPE D
                               WHERE A.PAYID=B.PAYID  AND A.SARYID=C.SARYID AND C.SaryPayType = D.SaryPayType AND SUBSTRING(B.YEARMONTH,1,6) >= '" + SYearMonth +"' And  SUBSTRING(B.YEARMONTH,1,6) <= '"+EYearMonth+
                             "' AND B.CustomerID = '"+ CustomerID+"' ORDER BY C.SaryPayType,A.SaryID";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js }; 
        }
        //取得差勤代號    
        public object[] GetDutyID_Select(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBADMIN");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string SYearMonth = parm[0];
                string EYearMonth = parm[1];
                string CustomerID = parm[2];
                string sql = @"SELECT DISTINCT A.SaryID,C.DutyName
                               FROM JBRECRUIT.DBO.PAYDETAILS A,JBRECRUIT.DBO.PAYMASTER B,JBRECRUIT.DBO.SALARYBASE C
                               WHERE A.PAYID=B.PAYID  AND A.SARYID=C.SARYID AND SUBSTRING(B.YEARMONTH,1,6) >= '" + SYearMonth + "' And  SUBSTRING(B.YEARMONTH,1,6) <= '" + EYearMonth +
                               "' AND B.CustomerID = '" + CustomerID + "' AND C.DutyName Is not null  ORDER BY A.SaryID";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection("JBADMIN", connection);
            }
            return new object[] { 0, js };
        }
        /// <summary>Combobox用資料</summary>
        public class ComboboxField
        {
            public string text { get; set; }

            public string value { get; set; }

            public bool selected { get; set; }

            public ComboboxField()
            {
                selected = false;
            }
        }
    }
}
