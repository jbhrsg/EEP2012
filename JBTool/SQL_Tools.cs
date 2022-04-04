using Srvtools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JBTool
{
    public class SQL_Tools
    {
        //SQL連線用
        public static DataSet GetDataSet(DataModule aDataModule, string SQL, ArrayList Parameter)
        {
            DataSet aDataSet = new DataSet();
            IDbConnection connection = (IDbConnection)aDataModule.AllocateConnection(aDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                aDataSet = aDataModule.ExecuteSql(SQL, connection, transaction, Parameter);
                transaction.Commit();
            }
            catch { transaction.Rollback(); aDataSet = null; }
            finally { aDataModule.ReleaseConnection(aDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return aDataSet;
        }

        //SQL執行用
        public static bool ExecuteNonQuery(DataModule aDataModule, string SQL, ArrayList Parameter)
        {
            bool Ans = false;
            IDbConnection connection = (IDbConnection)aDataModule.AllocateConnection(aDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = SQL;
                cmd.CommandTimeout = 90; //90秒保險一下
                cmd.Transaction = transaction;
                foreach (var aParameter in Parameter) cmd.Parameters.Add(aParameter);

                cmd.ExecuteNonQuery();
                transaction.Commit();
                Ans = true;
            }
            catch { transaction.Rollback(); }
            finally { aDataModule.ReleaseConnection(aDataModule.GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return Ans;
        }

        //SQL執行用
        public static int ExecuteNonQuery(string SQL, ArrayList Parameter, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = SQL;
            cmd.CommandTimeout = 90; //90秒保險一下
            cmd.Transaction = transaction;
            foreach (var aParameter in Parameter) cmd.Parameters.Add(aParameter);

            return cmd.ExecuteNonQuery();
        }
    }
}
