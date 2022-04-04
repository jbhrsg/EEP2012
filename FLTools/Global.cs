using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.OleDb;
using FLCore;
using FLCore.Base;
using Srvtools;
using System.Reflection;

namespace FLTools
{
    public static class Global
    {
        //public static IDbDataAdapter AllocateDataAdapter(IDbConnection connection, string sQL)
        //{
        //    if (connection is SqlConnection)
        //    {
        //        return new SqlDataAdapter(sQL, (SqlConnection)connection);
        //    }
        //    else if (connection is OdbcConnection)
        //    {
        //        return new OdbcDataAdapter(sQL, (OdbcConnection)connection);
        //    }
        //    else if (connection is OracleConnection)
        //    {
        //        return new OracleDataAdapter(sQL, (OracleConnection)connection);
        //    }
        //    else if (connection is OleDbConnection)
        //    {
        //        return new OleDbDataAdapter(sQL, (OleDbConnection)connection);
        //    }
        //    else if (connection.GetType().Name == "MySqlConnection")
        //    {
        //        String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

        //        Assembly assembly = Assembly.LoadFrom(s);
        //        IDbDataAdapter dataadapter = (IDbDataAdapter)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlDataAdapter");
        //        dataadapter.SelectCommand.Connection = connection;
        //        dataadapter.SelectCommand.CommandText = sQL;

        //        return dataadapter;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public static IDbCommand AllocateCommand(IDbConnection connection, string sQL)
        //{
        //    if (connection is SqlConnection)
        //    {
        //        return new SqlCommand(sQL, (SqlConnection)connection);
        //    }
        //    else if (connection is OdbcConnection)
        //    {
        //        return new OdbcCommand(sQL, (OdbcConnection)connection);
        //    }
        //    else if (connection is OracleConnection)
        //    {
        //        return new OracleCommand(sQL, (OracleConnection)connection);
        //    }
        //    else if (connection is OleDbConnection)
        //    {
        //        return new OleDbCommand(sQL, (OleDbConnection)connection);
        //    }
        //    else if (connection.GetType().Name == "MySqlConnection")
        //    {
        //        String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

        //        Assembly assembly = Assembly.LoadFrom(s);
        //        IDbCommand command = (IDbCommand)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlCommand");
        //        command.Connection = connection;
        //        command.CommandText = sQL;

        //        return command;
        //    }
        //    else return null;
        //}

        //public static IDbConnection AllocateConnection(DbConnectionType connectionType, string connectionString)
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
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public static string[] GetRoles(IDbConnection connection, string wherePart)
        //{
        //    List<string> list = new List<string>();
        //    String sql = "";
        //    String connectMark = "+";
        //    if (connection is SqlConnection)
        //        connectMark = "+";
        //    else if (connection is OdbcConnection)
        //        connectMark = "+";
        //    else if (connection is OracleConnection)
        //        connectMark = "||";
        //    else if (connection is OleDbConnection)
        //        connectMark = "+";

        //    if (connection.GetType().Name == "MySqlConnection")
        //        sql = "select CONCAT(GROUPID,' ; ',GROUPNAME) from GROUPS where ISROLE='Y'";
        //    else
        //        sql = "select GROUPID " + connectMark + " ' ; ' " + connectMark + " GROUPNAME from GROUPS where ISROLE='Y'";
        //    sql = string.Format(sql, wherePart);

        //    IDbCommand command = AllocateCommand(connection, sql);
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    while (reader.Read())
        //    {
        //        list.Add(reader[0].ToString());
        //    }

        //    connection.Close();

        //    return list.ToArray();
        //}

        //public static string[] GetOrgKind(IDbConnection connection, string wherePart)
        //{
        //    List<string> list = new List<string>();

        //    string sql = "select ORG_KIND from SYS_ORGKIND";
        //    sql = string.Format(sql, wherePart);

        //    IDbCommand command = AllocateCommand(connection, sql);
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    while (reader.Read())
        //    {
        //        list.Add(reader[0].ToString());
        //    }

        //    connection.Close();

        //    return list.ToArray();
        //}

        //public static string[] GetRefRoles(IDbConnection connection, string tableName)
        //{
        //    List<string> list = new List<string>();

        //    string sql = "select * from {0} where 1 > 1";
        //    sql = string.Format(sql, tableName);

        //    IDbDataAdapter adapter = AllocateDataAdapter(connection, sql);
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);            

        //    connection.Close();

        //    DataColumnCollection columns = ds.Tables[0].Columns;
        //    foreach (DataColumn c in columns)
        //    {
        //        list.Add(c.ColumnName);
        //    }                   

        //    return list.ToArray();
        //}

        //public static string[] GetOrgLevel(IDbConnection connection, string wherePart)
        //{
        //    List<string> list = new List<string>();
        //    String sql = "";
        //    String connectMark = "+";
        //    if (connection is SqlConnection)
        //        connectMark = "+";
        //    else if (connection is OdbcConnection)
        //        connectMark = "+";
        //    else if (connection is OracleConnection)
        //        connectMark = "||";
        //    else if (connection is OleDbConnection)
        //        connectMark = "+";

        //    if (connection.GetType().Name == "MySqlConnection")
        //        sql = "select CONCAT(LEVEL_NO,' ; ',LEVEL_DESC) from SYS_ORGLEVEL where 1=1";
        //    else
        //        sql = "select LEVEL_NO " + connectMark + " ' ; ' " + connectMark + " LEVEL_DESC from SYS_ORGLEVEL where 1=1";
        //    sql = string.Format(sql, wherePart);

        //    IDbCommand command = AllocateCommand(connection, sql);
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //    while (reader.Read())
        //    {
        //        list.Add(reader[0].ToString());

        //    }

        //    connection.Close();

        //    return list.ToArray();
        //}
    }
}
