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
namespace sDevice
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
        private void ucDeviceItems_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucDeviceItems.SetFieldValue("CreateDate", DateTime.Now);
            ucDeviceItems.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucDeviceItems_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucDeviceItems.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucDeviceMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucDeviceMaster.SetFieldValue("CreateDate", DateTime.Now);
            ucDeviceMaster.SetFieldValue("LastUpdateDate", DateTime.Now);
        }
        private void ucDeviceMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucDeviceMaster.SetFieldValue("CreateDate", DateTime.Now);
        }
        public object[] CheckDelMaster(object[] objParam)
        {
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
                string ID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM DeviceItems WHERE (DeviceMasterID) = '" + ID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();

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
        }
        public object[] CheckDelItems(object[] objParam)
        {
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
                string ID = objParam[0].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM OutDoor WHERE (DeviceItemsID) = '" + ID + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
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
        }
    }
}
