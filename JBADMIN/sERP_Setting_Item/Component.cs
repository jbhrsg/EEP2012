using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;

namespace sERP_Setting_Item
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

        //判斷類別名稱是否重複
        public object[] CheckDuplicate_ItemTypeName(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string ItemTypeName = aParam[0].Trim();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  [ItemTypeName] FROM [ItemType] where ItemTypeName='" + ItemTypeName + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        //判斷物品名稱是否重複
        public object[] CheckDuplicate_ItemName(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string ItemName = aParam[0].Trim();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  [ItemName] FROM [Item] where ItemName='" + ItemName + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }

        //傳回該類別的物品名稱(判斷該類有無物品)
        public object[] CheckItems(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string ItemTypeID = aParam[0].Trim();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection("JBERP");
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT  [ItemID] FROM [Item] where ItemTypeID='" + ItemTypeID + "'";
                DataTable tb = this.ExecuteSql(sql, connection, transaction).Tables[0];
                js = JsonConvert.SerializeObject(tb, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection("JBERP", connection);
            }
            return new object[] { 0, js };
        }
    }
}
