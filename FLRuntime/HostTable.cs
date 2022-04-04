using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Data;

namespace FLRuntime
{
    public class HostTable
    {
        private static string UPDATE_HOSTTABLE = @"UPDATE {0} SET FlowFlag='{1}' WHERE 1=1 AND {2}";
        private static string CLEAR_HOSTTABLE = @"UPDATE {0} SET FlowFlag= null WHERE 1=1 AND {1}";
        private static string GET_HOSTDATASET = @"SELECT * FROM {0} WHERE 1=1 AND {1}";
        private static string GET_TIMEOUTDATASET = @"SELECT * FROM {0} WHERE STATUS NOT IN ('F')";
        private static string GET_HOSTDATASET_BY_ID_AND_FLPATH = @"SELECT * FROM {0} WHERE LISTID='{1}' AND FLOWPATH='{2}'";
        private static string GET_HOSTDATASET_BY_ID_AND_FLPATH2 = @"SELECT * FROM {0} WHERE LISTID='{1}' AND FLOWPATH like '%;{2}' AND STATUS <> 'F'";
        private static string UPDATE_HOSTDATASET_BY_ID_AND_FLPATH = @"UPDATE {0} SET SENDTO_ID = '{3}' WHERE LISTID='{1}' AND FLOWPATH='{2}'";

        
        
        /// <summary>
        /// 更新FlowFlag
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void UpdateFLFlag(FLInstance flInstance, object[] keyValues, object[] clientInfo)
        {
            string hostTable = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            if (hostTable == null || hostTable == string.Empty || keyValues.Length == 0)
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTable, keyValues[1].ToString());
                throw new FLException(message);
            }

            var eepAlias = ((IFLRootActivity)flInstance.RootFLActivity).EEPAlias;
            var alias = string.Empty;
            if (!string.IsNullOrEmpty(eepAlias))
            {
                alias = (string)((object[])clientInfo[0])[2];
                ((object[])clientInfo[0])[2] = eepAlias;
            }

            string where = string.Empty;
            string s = keyValues[1].ToString();
            string[] ss = s.Split(";".ToCharArray());
            foreach (object o in ss)
            {
                where += (o.ToString()).Replace("''", "'");
                where += " and ";
            }
            where += " 1=1 ";
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(UPDATE_HOSTTABLE, Qutoe(clientInfo, hostTable), flInstance.FLFlag, where);
            object[] objs =  remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
            if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            if (!string.IsNullOrEmpty(alias))
            {
                ((object[])clientInfo[0])[2] = alias;
            }
        }

        public static void ClearFLFlag(FLInstance flInstance, object[] keyValues, object[] clientInfo)
        {
            string hostTable = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            if (hostTable == null || hostTable == string.Empty || keyValues.Length == 0)
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTable, keyValues[1].ToString());
                throw new FLException(message);
            }

            var eepAlias = ((IFLRootActivity)flInstance.RootFLActivity).EEPAlias;
            var alias = string.Empty;
            if (!string.IsNullOrEmpty(eepAlias))
            {
                alias = (string)((object[])clientInfo[0])[2];
                ((object[])clientInfo[0])[2] = eepAlias;
            }

            string where = string.Empty;
            string s = keyValues[1].ToString();
            string[] ss = s.Split(";".ToCharArray());
            foreach (object o in ss)
            {
                where += (o.ToString()).Replace("''", "'");
                where += " and ";
            }
            where += " 1=1 ";

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(CLEAR_HOSTTABLE, Qutoe(clientInfo, hostTable), where);
            object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
            if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            if (!string.IsNullOrEmpty(alias))
            {
                ((object[])clientInfo[0])[2] = alias;
            }
        }


        /// <summary>
        /// 取得宿主表
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static DataSet GetHostDataSet(FLInstance flInstance, object[] keyValues, object[] clientInfo)
        {
            string hostTable = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            var eepAlias = ((IFLRootActivity)flInstance.RootFLActivity).EEPAlias;
            var alias = string.Empty;
            if (!string.IsNullOrEmpty(eepAlias))
            {
                alias = (string)((object[])clientInfo[0])[2];
                ((object[])clientInfo[0])[2] = eepAlias;
            }
            var dataSet = GetHostDataSet(hostTable, keyValues, clientInfo);
            if (!string.IsNullOrEmpty(alias))
            {
                ((object[])clientInfo[0])[2] = alias;
            }
            return dataSet;
        }

        /// <summary>
        /// 取得宿主表
        /// </summary>
        /// <param name="hostTableName">宿主表名</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static DataSet GetHostDataSet(string hostTableName, object[] keyValues, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            if (hostTableName == null || hostTableName == string.Empty || keyValues.Length == 0)
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTableName, keyValues[1].ToString());
                throw new FLException(message);
            }

            string where = string.Empty;
            string s = keyValues[1].ToString();
            string[] ss = s.Split(";".ToCharArray());
            foreach (object o in ss)
            {
                where += (o.ToString()).Replace("''", "'");
                where += " and ";
            }
            where = where + " 1=1 ";
            string sql = string.Format(GET_HOSTDATASET, Qutoe(clientInfo, hostTableName), where);
            clientInfo[3] = sql;
            object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);

            if (objs[0].ToString() == "0")
            {
                return (DataSet)objs[1];
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 自动签核时取过期的流程数据
        /// </summary>
        /// <param name="hostTableName">宿主表名</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static DataSet GetOverTimeDataSet4AutoApprove(string hostTableName, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            if (string.IsNullOrEmpty(hostTableName))
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTableName, string.Empty);
                throw new FLException(message);
            }

            string sql = string.Format(GET_TIMEOUTDATASET, Qutoe(clientInfo, hostTableName));
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<DataRow> notOverTimeRows = new List<DataRow>();
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        string TIME_UNIT = row["TIME_UNIT"] == null || row["TIME_UNIT"] == DBNull.Value ? string.Empty : row["TIME_UNIT"].ToString();
                        string FLOWURGENT = row["FLOWURGENT"] == null || row["FLOWURGENT"] == DBNull.Value ? string.Empty : row["FLOWURGENT"].ToString();
                        string UPDATE_DATE = row["UPDATE_DATE"] == null || row["UPDATE_DATE"] == DBNull.Value ? string.Empty : row["UPDATE_DATE"].ToString();
                        string UPDATE_TIME = row["UPDATE_TIME"] == null || row["UPDATE_TIME"] == DBNull.Value ? string.Empty : row["UPDATE_TIME"].ToString();
                        string URGENT_TIME = row["URGENT_TIME"] == null || row["URGENT_TIME"] == DBNull.Value ? string.Empty : row["URGENT_TIME"].ToString();
                        string EXP_TIME = row["EXP_TIME"] == null || row["EXP_TIME"] == DBNull.Value ? string.Empty : row["EXP_TIME"].ToString();
                        string DELAY_AUTOAPPROVE = row["LEVEL_NO"] == null || row ["LEVEL_NO"] == DBNull.Value ? string.Empty : row["LEVEL_NO"].ToString();

                        if (!(IsOverTime(TIME_UNIT, FLOWURGENT, UPDATE_DATE, UPDATE_TIME, URGENT_TIME, EXP_TIME)
                            && (DELAY_AUTOAPPROVE.ToUpper() == "AUTO")))
                        {
                            notOverTimeRows.Add(row);
                        }
                    }

                    foreach(DataRow row in notOverTimeRows)
                    {
                        dataSet.Tables[0].Rows.Remove(row);
                    }
                }

                return dataSet;
            }
            else if (objs[1].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 发送Email时取过期的流程数据
        /// </summary>
        /// <param name="hostTableName">宿主表名</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static DataSet GetOverTimeDataSet4SendMail(string hostTableName, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            if (string.IsNullOrEmpty(hostTableName))
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTableName, string.Empty);
                throw new FLException(message);
            }

            string sql = string.Format(GET_TIMEOUTDATASET, Qutoe(clientInfo, hostTableName));
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    List<DataRow> notOverTimeRows = new List<DataRow>();
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        string TIME_UNIT = row["TIME_UNIT"] == null || row["TIME_UNIT"] == DBNull.Value ? string.Empty : row["TIME_UNIT"].ToString();
                        string FLOWURGENT = row["FLOWURGENT"] == null || row["FLOWURGENT"] == DBNull.Value ? string.Empty : row["FLOWURGENT"].ToString();
                        string UPDATE_DATE = row["UPDATE_DATE"] == null || row["UPDATE_DATE"] == DBNull.Value ? string.Empty : row["UPDATE_DATE"].ToString();
                        string UPDATE_TIME = row["UPDATE_TIME"] == null || row["UPDATE_TIME"] == DBNull.Value ? string.Empty : row["UPDATE_TIME"].ToString();
                        string URGENT_TIME = row["URGENT_TIME"] == null || row["URGENT_TIME"] == DBNull.Value ? string.Empty : row["URGENT_TIME"].ToString();
                        string EXP_TIME = row["EXP_TIME"] == null || row["EXP_TIME"] == DBNull.Value ? string.Empty : row["EXP_TIME"].ToString();
                        string DELAY_AUTOAPPROVE = row["LEVEL_NO"] == null || row["LEVEL_NO"] == DBNull.Value ? string.Empty : row["LEVEL_NO"].ToString();

                        if (!(IsOverTime(TIME_UNIT, FLOWURGENT, UPDATE_DATE, UPDATE_TIME, URGENT_TIME, EXP_TIME)))
                        {
                            notOverTimeRows.Add(row);
                        }
                    }

                    foreach (DataRow row in notOverTimeRows)
                    {
                        dataSet.Tables[0].Rows.Remove(row);
                    }
                }

                return dataSet;
            }
            else if (objs[1].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过流程Id和流程路径取数据
        /// </summary>
        /// <param name="hostTableName">宿主表名</param>
        /// <param name="instanceId">流程Id</param>
        /// <param name="flPath">流程路径</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static DataSet GetHostDataSetByIdAndFLPath(string hostTableName, string instanceId, string flPath, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            if (string.IsNullOrEmpty(hostTableName))
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTableName, string.Empty);
                throw new FLException(message);
            }

            string sql = string.Format(GET_HOSTDATASET_BY_ID_AND_FLPATH, Qutoe(clientInfo, hostTableName), instanceId, flPath);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];

                return dataSet;
            }
            else if (objs[1].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            else
            {
                return null;
            }
        }

        public static void UpdateHostDataSetByIdAndFLPath(string hostTableName, string instanceId, string flPath, string sendtoid, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            if (string.IsNullOrEmpty(hostTableName))
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTableName, string.Empty);
                throw new FLException(message);
            }

            string sql = string.Format(UPDATE_HOSTDATASET_BY_ID_AND_FLPATH, Qutoe(clientInfo, hostTableName), instanceId, flPath, sendtoid);
            //clientInfo[3] = sql;

            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {

            }
            else if (objs[1].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

        }


        public static DataSet GetHostDataSetByIdAndPath2(string hostTableName, string instanceId, string path, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            if (string.IsNullOrEmpty(hostTableName))
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "HostTableNotContainRecord"), hostTableName, string.Empty);
                throw new FLException(message);
            }

            string sql = string.Format(GET_HOSTDATASET_BY_ID_AND_FLPATH2, Qutoe(clientInfo, hostTableName), instanceId, path);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];

                return dataSet;
            }
            else if (objs[1].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            else
            {
                return null;
            }
        }

        private static bool _ignoreWeekends = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TIME_UNIT"></param>
        /// <param name="FLOWURGENT"></param>
        /// <param name="UPDATE_DATE"></param>
        /// <param name="UPDATE_TIME"></param>
        /// <param name="URGENT_TIME"></param>
        /// <param name="EXP_TIME"></param>
        /// <returns></returns>
        private static bool IsOverTime(string TIME_UNIT, string FLOWURGENT, string UPDATE_DATE, string UPDATE_TIME, string URGENT_TIME, string EXP_TIME)
        {
            if (TIME_UNIT == "Day" && FLOWURGENT == "1")
            {
                if (Convert.ToDecimal(URGENT_TIME) == Decimal.Zero) return false;
                TimeSpan span = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), _ignoreWeekends, null);
                int overtimes = span.Days - Convert.ToInt32(Convert.ToDecimal(URGENT_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            else if (TIME_UNIT == "Day" && FLOWURGENT == "0")
            {
                if (Convert.ToDecimal(EXP_TIME) == Decimal.Zero) return false;
                TimeSpan span = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), _ignoreWeekends, null);
                int overtimes = span.Days - Convert.ToInt32(Convert.ToDecimal(EXP_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            else if (TIME_UNIT == "Hour" && FLOWURGENT == "1")
            {
                if (Convert.ToDecimal(URGENT_TIME) == Decimal.Zero) return false;
                TimeSpan spanDay = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), _ignoreWeekends, null);
                int spanHour = DateTime.Now.Hour - Convert.ToDateTime(UPDATE_TIME).Hour;
                if (DateTime.Now.Minute < Convert.ToDateTime(UPDATE_TIME).Minute)
                {
                    spanHour -= 1;
                }
                int overtimes = spanDay.Days * 8 + spanHour - Convert.ToInt32(Convert.ToDecimal(URGENT_TIME));
               
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            else if (TIME_UNIT == "Hour" && FLOWURGENT == "0")
            {
                if (Convert.ToDecimal(EXP_TIME) == Decimal.Zero) return false;
                TimeSpan spanDay = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), _ignoreWeekends, null);
                int spanHour = DateTime.Now.Hour - Convert.ToDateTime(UPDATE_TIME).Hour;
                if (DateTime.Now.Minute < Convert.ToDateTime(UPDATE_TIME).Minute)
                {
                    spanHour -= 1;
                }
                int overtimes = spanDay.Days * 8 + spanHour - Convert.ToInt32(Convert.ToDecimal(EXP_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowTime"></param>
        /// <param name="updateTime"></param>
        /// <param name="weekendSensible"></param>
        /// <param name="extDates"></param>
        /// <returns></returns>
        private static  TimeSpan WorkTimeSpan(DateTime nowTime, DateTime updateTime, bool weekendSensible, List<string> extDates)
        {
            TimeSpan span = new TimeSpan();
            if (weekendSensible)
            {
                if (nowTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    nowTime = nowTime.Date.AddSeconds(-1);
                }
                else if (nowTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    nowTime = nowTime.Date.AddDays(-1).AddSeconds(-1);
                }
                if (updateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    updateTime = updateTime.Date.AddDays(2);
                }
                else if (updateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    updateTime = updateTime.Date.AddDays(1);
                }
            }
            span = nowTime - updateTime;
            if (weekendSensible)
            {
                int weekends = span.Days / 7;
                int i = nowTime.DayOfWeek - updateTime.DayOfWeek;
                if (i < 0)
                    weekends++;
                span = span.Subtract(new TimeSpan(2 * weekends, 0, 0, 0));
            }
            int extDays = 0;
            if (extDates == null) return span;
            foreach (string extDate in extDates)
            {
                if (Convert.ToDateTime(extDate).CompareTo(nowTime) < 0
                    && Convert.ToDateTime(extDate).CompareTo(updateTime) > 0)
                {
                    if (weekendSensible)
                    {
                        if (Convert.ToDateTime(extDate).DayOfWeek != DayOfWeek.Saturday
                            && Convert.ToDateTime(extDate).DayOfWeek != DayOfWeek.Sunday)
                        {
                            extDays++;
                        }
                    }
                    else
                    {
                        extDays++;
                    }
                }
            }
            span = span.Subtract(new TimeSpan(extDays, 0, 0, 0));
            return span;
        }

        //public static string GetWherePart(DataRow row, string keys)
        //{
        //    string[] k1 = keys.Split(';');
        //    string where = string.Empty;
        //    foreach (string k in k1)
        //    {
        //        string s = string.Empty;
        //        if (k != null && k.Length > 0)
        //        {
        //            DataColumn c = row.Table.Columns[k];
        //            string type = c.DataType.FullName;

        //            s += k;
        //            s += "=";
        //            s += Mark(type, TransformMarkerInColumnValue(type, row[k]));

        //            if (where.Length > 0)
        //            {
        //                where += " AND ";
        //            }

        //            where += s;
        //        }
        //    }

        //    return where;
        //}

        /// <summary>
        /// 取得Details数据
        /// </summary>
        /// <param name="masterDataSet">主表数据</param>
        /// <param name="keys">筛选条件</param>
        /// <param name="detailsTableName">从表名</param>
        /// <param name="relationKeys">关联条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static DataSet GetDetailsDataSet(FLInstance flInstance, DataSet masterDataSet, string keys, string detailsTableName, string relationKeys, object[] clientInfo)
        {
            if (string.IsNullOrEmpty(relationKeys))
            {
                return null;
            }

            if (string.IsNullOrEmpty(keys))
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "KeysWasNotDefine");
                throw new FLException(3, message);
            }

            string[] k1 = keys.Split(";,".ToCharArray());
            string[] k2 = relationKeys.Split(";,".ToCharArray());
            
            if(k1.Length != k2.Length)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "HostTable", "KeysLengthNotEqualRelation");
                throw new FLException(message);
            }

            int i = 0;
            string where = string.Empty;
            foreach (string k in k1)
            {
                string s = string.Empty;
                if (k != null && k.Length > 0)
                {
                    DataColumn c = masterDataSet.Tables[0].Columns[k];
                    string type = c.DataType.FullName;

                    s += k2[i];
                    s += "=";
                    s += Mark(type, TransformMarkerInColumnValue(type, masterDataSet.Tables[0].Rows[0][k]));

                    if (where.Length > 0)
                    {
                        where += " AND ";
                    }

                    where += s;
                }

                i++;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            var eepAlias = ((IFLRootActivity)flInstance.RootFLActivity).EEPAlias;
            var alias = string.Empty;
            if (!string.IsNullOrEmpty(eepAlias))
            {
                alias = (string)((object[])clientInfo[0])[2];
                ((object[])clientInfo[0])[2] = eepAlias;
            }
            string sql = string.Format(GET_HOSTDATASET, Qutoe(clientInfo, detailsTableName), where);
            clientInfo[3] = sql;
            object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            if (!string.IsNullOrEmpty(alias))
            {
                ((object[])clientInfo[0])[2] = alias;
            }
            if (objs[0].ToString() == "0")
            {
                return (DataSet)objs[1];
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        private static String Mark(String typeName, String type, Object columnValue)
        {
            bool isN = false;
            if (typeName.ToLower() == "NChar".ToLower() || typeName.ToLower() == "NVarChar".ToLower()
                || typeName.ToLower() == "NText".ToLower() || typeName.ToLower() == "NClob".ToLower())
            {
                isN = true;
            }
            if (Type.GetType(type).Equals(typeof(Char)) || Type.GetType(type).Equals(typeof(String)) || Type.GetType(type).Equals(typeof(Guid)))
            {
                if (isN)
                    return "N" + _marker.ToString() + columnValue.ToString() + _marker.ToString();
                else
                    return _marker.ToString() + columnValue.ToString() + _marker.ToString();
            }
            else if (Type.GetType(type).Equals(typeof(Boolean)))
            {
                Boolean b = (Boolean)columnValue;
                if (b)
                    return "1";
                else
                    return "0";
            }
            else if (Type.GetType(type).Equals(typeof(DateTime)))
            {
                DateTime t = Convert.ToDateTime(columnValue);
                string s = t.Year.ToString() + "-" + t.Month.ToString() + "-" + t.Day.ToString() + " "
                    + t.Hour.ToString() + ":" + t.Minute.ToString() + ":" + t.Second.ToString();
                return _marker.ToString() + s + _marker.ToString();
            }
            else if (Type.GetType(type).Equals(typeof(Byte[])))
            {
                StringBuilder builder = new StringBuilder("0x");   // 16进制、Oracle里的Binary没有测试。
                foreach (Byte b in (Byte[])columnValue)
                {
                    string tmp = Convert.ToString(b, 16);
                    if (tmp.Length < 2)
                        tmp = "0" + tmp;
                    builder.Append(tmp);
                }
                return builder.ToString();
            }
            else
            {
                return columnValue.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        private static String Mark(String type, Object columnValue)
        {
            //bool isN = false;
            //if (typeName.ToLower() == "NChar".ToLower() || typeName.ToLower() == "NVarChar".ToLower()
            //    || typeName.ToLower() == "NText".ToLower() || typeName.ToLower() == "NClob".ToLower())
            //{
            //    isN = true;
            //}
            if (Type.GetType(type).Equals(typeof(Char)) || Type.GetType(type).Equals(typeof(String)))
            {
                //if (isN)
                //    return "N" + _marker.ToString() + columnValue.ToString() + _marker.ToString();
                //else
                return _marker.ToString() + columnValue.ToString() + _marker.ToString();
            }
            else if (Type.GetType(type).Equals(typeof(Boolean)))
            {
                Boolean b = (Boolean)columnValue;
                if (b)
                    return "1";
                else
                    return "0";
            }
            else if (Type.GetType(type).Equals(typeof(DateTime)))
            {
                String s = "";
                DateTime t = Convert.ToDateTime(columnValue);
                s = t.Year.ToString() + "-" + t.Month.ToString() + "-" + t.Day.ToString() + " "
                    + t.Hour.ToString() + ":" + t.Minute.ToString() + ":" + t.Second.ToString();
                s = "to_date('" + s + "', 'yyyy-mm-dd hh24:mi:ss')";
                //return _marker.ToString() + s + _marker.ToString();
                return s;
            }
            else if (Type.GetType(type).Equals(typeof(Byte[])))
            {
                StringBuilder builder = new StringBuilder("0x");   // 16进制、Oracle里的Binary没有测试。
                foreach (Byte b in (Byte[])columnValue)
                {
                    string tmp = Convert.ToString(b, 16);
                    if (tmp.Length < 2)
                        tmp = "0" + tmp;
                    builder.Append(tmp);
                }
                return builder.ToString();
            }
            else
            {
                return columnValue.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        private static Object TransformMarkerInColumnValue(String typeName, Object columnValue)
        {
            if (Type.GetType(typeName).Equals(typeof(Char)) || Type.GetType(typeName).Equals(typeof(String)))
            {
                StringBuilder sb = new StringBuilder();
                if (columnValue.ToString().Length > 0)
                {
                    Char[] cVChars = columnValue.ToString().ToCharArray();

                    foreach (Char cVChar in cVChars)
                    {
                        if (cVChar == _marker)
                        { sb.Append(cVChar.ToString()); }

                        sb.Append(cVChar.ToString());
                    }
                }
                return sb.ToString();
            }
            else
            { return columnValue; }
        }

        private static Char _marker = '\'';
        private static String _quotePrefix = "[";
        private static String _quoteSuffix = "]";
        public static String Qutoe(object[] clientInfo, String table_or_column)
        {
            String DBAlias = (clientInfo[0] as object[])[2].ToString();
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { DBAlias });
            if (myRet != null && myRet[0].ToString() == "0")
            {
                switch (myRet[1].ToString())
                {
                    case "1": table_or_column = _quotePrefix + table_or_column + _quoteSuffix; break;
                }
            }
            return table_or_column;
        }
    }
}