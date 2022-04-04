using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using FLCore;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using Microsoft.Win32;
using FLCore.Base;
using System.Reflection;

namespace FLRuntime.Hosting
{
    [Serializable]
    public class FLPersistenceService //: IFLPersistenceService
    {
        private const string GET_FLDEFINITION = "SELECT * FROM SYS_FLDEFINITION WHERE FLTYPENAME='{0}' ORDER BY VERSION DESC";
        private const string INSERT_FLDEFINITION = "INSERT INTO SYS_FLDEFINITION(FLTYPEID,FLTYPENAME,FLDEFINITION,VERSION) VALUES('{0}','{1}','{2}','{3}')";
        private const string DELETE_FLINSTANCESTATE = "DELETE FROM SYS_FLINSTANCESTATE WHERE FLINSTANCEID='{0}'";
        private const string INSERT_FLINSTANCESTATE = "INSERT INTO SYS_FLINSTANCESTATE(FLINSTANCEID,STATE,STATUS) VALUES('{0}',@State,{1})";
        private const string GET_FLINSTANCESTATE = "SELECT * FROM SYS_FLINSTANCESTATE WHERE FLINSTANCEID='{0}'";

        private static string _serverPath = string.Empty;


        public FLPersistenceService()
        {

        }

        public void PersistenceFL(FLInstance flInstance, object[] clientInfo)
        {
            //string sql = sql1 + ";\n" + sql2;
            string sql1 = string.Format(DELETE_FLINSTANCESTATE, flInstance.FLInstanceId);

            String param = "";
            String DBAlias = (clientInfo[0] as object[])[2].ToString();
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            object[] xx = remoteModule.CallMethod(clientInfo, "GLModule", "GetSplitSysDB2", new object[] { DBAlias });
            if (xx[0].ToString() == "0")
                DBAlias = xx[1].ToString();
            //object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { DBAlias });
            IDbConnection conn1 = AllocateWorkFlowConnection(DBAlias, clientInfo);
            if (conn1.GetType().Name == "SqlConnection")
                param = "@State";
            else if (conn1.GetType().Name == "OleDbConnection")
                param = "?";
            else if (conn1.GetType().Name == "OracleConnection")
                param = ":State";
            else if (conn1.GetType().Name == "OdbcConnection")
                param = "?";
            else if (conn1.GetType().Name == "MySqlConnection")
                param = "@State";
            else if (conn1.GetType().Name == "Ifxonnection")
                param = "?";

            String strSet = "SET TEXTSIZE 70000 ";
            String str = "INSERT INTO SYS_FLINSTANCESTATE(FLINSTANCEID,STATE,STATUS) VALUES('{0}'," + param + ",{1})";
            string sql2 = string.Format(str, flInstance.FLInstanceId, 0);

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, flInstance);

            object o = clientInfo[0];
            object[] os = (object[])o;
            string dbAlias = os[2].ToString();

            //DbConnectionType dbConnectionType = DbConnectionType.SqlClient;
            //string connString = GetConnectionString(dbAlias, out dbConnectionType, true);

            if (IsWorkFlowTransactionEnabled(clientInfo))
            {
                IDbConnection conn = AllocateWorkFlowConnection(DBAlias, clientInfo);
                IDbCommand command = AllocateCommand(conn, sql1);
                command.Transaction = GetWorkFlowTransaction(DBAlias, clientInfo);
                command.ExecuteNonQuery();

                if (conn1.GetType().Name == "OleDbConnection")
                {
                    conn.Open();
                    command.CommandText = strSet;
                    command.ExecuteNonQuery();
                }

                command.CommandText = sql2;
                IDataParameter state = AllocateParameter(command, param, DbType.Binary);
                if (conn1.GetType().Name == "OleDbConnection")
                {
                    (state as OleDbParameter).OleDbType = OleDbType.LongVarBinary;
                }
                state.Value = stream.GetBuffer();
                command.Parameters.Add(state);
                command.ExecuteNonQuery();
            }
            else
            {
                var workFlowTransmitterPropagationToken = WorkFlowTransmitterPropagationToken(clientInfo);
                if (workFlowTransmitterPropagationToken != null)
                {
                    using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope(System.Transactions.TransactionInterop.GetTransactionFromTransmitterPropagationToken(workFlowTransmitterPropagationToken)))
                    {
                        using (IDbConnection conn = AllocateConnection(dbAlias, true, clientInfo))
                        {
                            IDbCommand command = AllocateCommand(conn, sql1);
                            command.ExecuteNonQuery();
                            if (conn1.GetType().Name == "OleDbConnection")
                            {
                                conn.Open();
                                command.CommandText = strSet;
                                command.ExecuteNonQuery();
                            }
                            command.CommandText = sql2;
                            IDataParameter state = AllocateParameter(command, param, DbType.Binary);
                            if (conn1.GetType().Name == "OleDbConnection")
                            {
                                (state as OleDbParameter).OleDbType = OleDbType.LongVarBinary;
                            }
                            state.Value = stream.GetBuffer();
                            command.Parameters.Add(state);
                            command.ExecuteNonQuery();
                            ts.Complete();
                        }
                    }
                }
                else
                {
                    using (IDbConnection conn = AllocateConnection(dbAlias, true, clientInfo))
                    {
                        IDbCommand command = AllocateCommand(conn, sql1);
                        command.ExecuteNonQuery();
                        if (conn1.GetType().Name == "OleDbConnection")
                        {
                            conn.Open();
                            command.CommandText = strSet;
                            command.ExecuteNonQuery();
                        }
                        command.CommandText = sql2;
                        IDataParameter state = AllocateParameter(command, param, DbType.Binary);
                        if (conn1.GetType().Name == "OleDbConnection")
                        {
                            (state as OleDbParameter).OleDbType = OleDbType.LongVarBinary;
                        }
                        state.Value = stream.GetBuffer();
                        command.Parameters.Add(state);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public FLInstance DepersistenceFL(Guid flInstanceId, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            String DBAlias = (clientInfo[0] as object[])[2].ToString();
            object[] xx = remoteModule.CallMethod(clientInfo, "GLModule", "GetSplitSysDB2", new object[] { DBAlias });
            if (xx[0].ToString() == "0")
                DBAlias = xx[1].ToString();
            //object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { DBAlias });
            IDbConnection conn1 = AllocateWorkFlowConnection(DBAlias, clientInfo);

            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);

            if (conn1.GetType().Name != "OracleConnection")
            {
                String strSet = "SET TEXTSIZE 70000 ";
                remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { strSet });
            }
            string sql = string.Format(GET_FLINSTANCESTATE, flInstanceId.ToString());
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            MemoryStream stream = null;
            if (objs != null && (int)objs[0] == 0 && objs[1] is DataSet)
            {
                DataSet ds = (DataSet)objs[1];
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    byte[] buffer = (byte[])ds.Tables[0].Rows[0]["STATE"];
                    stream = new MemoryStream(buffer);
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new Exception(objs[1].ToString());
            }

            if (stream != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    object o = formatter.Deserialize(stream);

                    if (o != null)
                    {
                        return (FLInstance)o;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string PersistenceFLDefinition(Guid flDefinitionId, string flTypeName, string flDefinitionString, object[] clientInfo)
        {
            return Global.GetFLTypeId(flDefinitionId, flTypeName, flDefinitionString, clientInfo);
        }

        public void DeleteFL(Guid flInstanceId, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(DELETE_FLINSTANCESTATE, flInstanceId.ToString());

            //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "1")
            {
                throw new Exception(objs[1].ToString());
            }
        }



        #region ------- 要操作二进值 --------

        const bool WORKFLOW_TRANSACTION_ENABLE = true;
        const string WORKFLOW_COMMAND_NAME = "EEP_WorkFlow";

        private bool IsWorkFlowTransactionEnabled(object[] clientInfo)
        {
            return WorkFlowTransmitterPropagationToken(clientInfo) == null && WORKFLOW_TRANSACTION_ENABLE;
        }

        private byte[] WorkFlowTransmitterPropagationToken(object[] clientInfo)
        {
            object[] info = (object[])clientInfo[0];
            if (info[10] != null && info[10] is byte[])
            {
                return (byte[])info[10];
            }
            return null;
        }

        private static String GetSplitSysDBSD(String DBName, String DeveloperID)
        {
            string dbname = "";
            if (Srvtools.DbConnectionSet.GetDbConn(DBName, DeveloperID) != null)
                if (Srvtools.DbConnectionSet.GetDbConn(DBName, DeveloperID).SplitSystemTable)
                    dbname = Srvtools.DbConnectionSet.GetSystemDatabase(DeveloperID);// GetSplitSysDB(DBName);
                else
                    dbname = DBName;
            else
                dbname = GetSplitSysDB(DBName);

            return dbname;
        }

        private IDbConnection AllocateWorkFlowConnection(string DBName, object[] clientInfo)
        {
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            string dbname = GetSplitSysDBSD(DBName, developerID);
            //string dbname = GetSplitSysDB(DBName);
            string userID = ((object[])clientInfo[0])[1].ToString();
            ClientType ct = ClientType.ctNone;
            IDbConnection conn = Srvtools.DbConnectionSet.WaitForConnection(dbname, developerID, ref ct, userID
                           , "GLModule.Component", WORKFLOW_COMMAND_NAME, DateTime.Now);
            if (conn == null)
            {
                string sMess = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "Srvtools", "DataModule", "msg_AllocateConnectionTimeOutEx");
                throw new Exception(sMess);
            }
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        private IDbTransaction GetWorkFlowTransaction(string DBName, object[] clientInfo)
        {
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            string dbname = GetSplitSysDBSD(DBName, developerID);
           // string dbname = GetSplitSysDB(DBName);
            string userID = ((object[])clientInfo[0])[1].ToString();
            //string developerID = null;
            IDbTransaction transaction = Srvtools.DbConnectionSet.GetTransaction(dbname, developerID, userID, "GLModule.Component", WORKFLOW_COMMAND_NAME);
            return transaction;
        }


        private static IDbCommand AllocateCommand(IDbConnection connection, string sql)
        {
            if (connection is SqlConnection)
            {
                return new SqlCommand(sql, (SqlConnection)connection);
            }
            else if (connection is OdbcConnection)
            {
                return new OdbcCommand(sql, (OdbcConnection)connection);
            }
            else if (connection is OracleConnection)
            {
                return new OracleCommand(sql, (OracleConnection)connection);
            }
            else if (connection is OleDbConnection)
            {
                return new OleDbCommand(sql, (OleDbConnection)connection);
            }
            else if (connection.GetType().Name == "MySqlConnection")
            {
                String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDbCommand command = (IDbCommand)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlCommand");
                command.Connection = connection;
                command.CommandText = sql;

                return command;
            }
            else if (connection.GetType().Name == "IfxConnection")
            {
                String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDbCommand command = (IDbCommand)assembly.CreateInstance("IBM.Data.Informix.IfxCommand");
                command.Connection = connection;
                command.CommandText = sql;

                return command;
            }
            else return null;
        }


        //20100526 
        private static IDbConnection AllocateConnection(String DbAlias, Boolean SysDB, object[] clientInfo)
        {
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            string dbname = SysDB ? GetSplitSysDBSD(DbAlias, developerID) : DbAlias;
            //string dbname = SysDB ? GetSplitSysDB(DbAlias) : DbAlias;

            Srvtools.DbConnectionSet.DbConnection db = Srvtools.DbConnectionSet.GetDbConn(dbname);
            IDbConnection aConn = db.CreateConnection();
            if (aConn.State == ConnectionState.Closed)
                aConn.Open();
            return aConn;
        }

        //private static IDbConnection AllocateConnection(DbConnectionType connectionType, string connectionString)
        //{
        //    if (connectionType == DbConnectionType.SqlClient)
        //    {
        //        return new SqlConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.Odbc)
        //    {
        //        return new OdbcConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.OracleClient)
        //    {
        //        return new OracleConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.OleDb)
        //    {
        //        return new OleDbConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.MySQL)
        //    {
        //        String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

        //        Assembly assembly = Assembly.LoadFrom(s);
        //        IDbConnection connection = (IDbConnection)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlConnection");
        //        connection.ConnectionString = connectionString;
        //        return connection;
        //    }
        //    else if (connectionType == DbConnectionType.Informix)
        //    {
        //        String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);

        //        Assembly assembly = Assembly.LoadFrom(s);
        //        IDbConnection connection = (IDbConnection)assembly.CreateInstance("IBM.Data.Informix.IfxConnection");
        //        connection.ConnectionString = connectionString;
        //        return connection;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        private static IDataParameter AllocateParameter(IDbCommand command, string parameterName, DbType dbType)
        {
            if (command is SqlCommand)
            {
                return new SqlParameter(parameterName, dbType);
            }
            else if (command is OdbcCommand)
            {
                return new OdbcParameter(parameterName, dbType);
            }
            else if (command is OracleCommand)
            {
                return new OracleParameter(parameterName, dbType);
            }
            else if (command is OleDbCommand)
            {
                return new OleDbParameter(parameterName, dbType);
            }
            else if (command.GetType().Name == "MySqlCommand")
            {
                String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDataParameter parameter = (IDataParameter)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlParameter");
                parameter.ParameterName = parameterName;
                parameter.DbType = dbType;

                return parameter;
            }
            else if (command.GetType().Name == "IfxCommand")
            {
                String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDataParameter parameter = (IDataParameter)assembly.CreateInstance("IBM.Data.Informix.IfxParameter");
                parameter.ParameterName = parameterName;
                parameter.DbType = dbType;

                return parameter;
            }
            else return null;
        }


        private static string GetSplitSysDB(string alias)
        {
            String s = SystemFile.DBFile;

            if (File.Exists(s))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(s);
                XmlNode node = xml.SelectSingleNode(string.Format("InfolightDB/DataBase/{0}", alias));
                if (node != null)
                {
                    if (node.Attributes["Master"] != null && node.Attributes["Master"].Value.Trim() == "1")
                    {
                        XmlNode nodesys = xml.SelectSingleNode("InfolightDB/SystemDB");
                        if (nodesys != null)
                        {
                            string sysdb = nodesys.InnerText.Trim();
                            XmlNode nodecheck = xml.SelectSingleNode(string.Format("InfolightDB/DataBase/{0}", sysdb));
                            if (nodecheck != null)
                            {
                                return sysdb;
                            }
                            else
                            {
                                throw new Exception("SystemDB does not exsit in db list");
                            }
                        }
                        else
                        {
                            throw new Exception("SystemDB is Empty");
                        }
                    }
                    else
                    {
                        return alias;
                    }
                }
                else
                {
                    throw new Exception(string.Format("EEPAlias:{0} does not exsit", alias));
                }
            }
            else
            {
                throw new Exception(string.Format("{0} does not exsit", s));
            }

        }

        private static string GetConnectionString(string alias, out DbConnectionType dbConnectionType, bool b)
        {
            alias = b ? GetSplitSysDB(alias) : alias;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(SystemFile.DBFile);

            XmlNode node = xmlDoc.FirstChild.FirstChild.SelectSingleNode(alias);

            string DbString = "";
            string Pwd = "";
            if (node != null)
            {
                DbString = node.Attributes["String"].Value.Trim();
                Pwd = GetPwdString(node.Attributes["Password"].Value.Trim());
            }
            if (DbString.Length > 0 && Pwd.Length > 0 && Pwd != String.Empty)
            {
                if (DbString[DbString.Length - 1] != ';')
                    DbString = DbString + ";Password=" + Pwd;
                else
                    DbString = DbString + "Password=" + Pwd;
            }

            string value = "1";
            if (node != null)
            {
                value = node.Attributes["Type"].Value;
                if (value == "1")
                {
                    dbConnectionType = DbConnectionType.SqlClient;
                }
                else if (value == "2")
                {
                    dbConnectionType = DbConnectionType.OleDb;
                }
                else if (value == "3")
                {
                    dbConnectionType = DbConnectionType.OracleClient;
                }
                else if (value == "4")
                {
                    dbConnectionType = DbConnectionType.Odbc;
                }
                else if (value == "5")
                {
                    dbConnectionType = DbConnectionType.MySQL;
                }
                else if (value == "6")
                {
                    dbConnectionType = DbConnectionType.Informix;
                }
                else
                {
                    dbConnectionType = DbConnectionType.SqlClient;
                }
            }
            else
            {
                dbConnectionType = DbConnectionType.SqlClient;
            }

            return DbString;
        }

        private static string GetPwdString(string password)
        {
            string sRet = "";
            for (int i = 0; i < password.Length; i++)
            {
                sRet = sRet + (char)(((int)(password[password.Length - 1 - i])) ^ password.Length);
            }
            return sRet;
        }

        #endregion
    }
}
