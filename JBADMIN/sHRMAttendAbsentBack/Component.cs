using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sHRMAttendAbsentBack
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
        //Flow請假成功=>新增資料到JBHR_EEP
        //1.新增 請假資料檔 HRM_ATTEND_ABSENT_MINUS , 請假資料明細檔 HRM_ATTEND_ABSENT_MINUS_DETAIL , 請假對沖明細檔 HRM_ATTEND_ABSENT_TRANS 
        //2.修改得假資料檔 HRM_ATTEND_ABSENT_PLUS => 請假沖銷時數 ABSENT_HOURS , 得假剩餘時數 REST_HOURS 
        public object procDeleteHRM_ATTEND_ABSENT_MINUS(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
           
            DataRow drDara = (DataRow)objParam[0]; // 取得流程資料表的資料內容
            int AbsentMinusID = Convert.ToInt32(drDara["AbsentMinusID"].ToString());
            var TotalHours = drDara["TotalHours"].ToString();//總時數            

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
                //Delete HRM_ATTEND_ABSENT_MINUS , HRM_ATTEND_ABSENT_MINUS_DETAIL 
                //從 HRM_ATTEND_ABSENT_TRANS 取得 ABSENT_PLUS_ID  
                //Delete HRM_ATTEND_ABSENT_TRANS
                //Update HRM_ATTEND_ABSENT_PLUS
                var Sql = "";
                Sql = Sql + "Delete from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS where ABSENT_MINUS_ID= " + AbsentMinusID + "\r\n";
                Sql = Sql + "Delete from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_MINUS_DETAIL where ABSENT_MINUS_ID= " + AbsentMinusID + "\r\n";
                Sql = Sql + " declare @ABSENT_PLUS_ID int " + "\r\n";
                Sql = Sql + " SET @ABSENT_PLUS_ID = (select top 1 ABSENT_PLUS_ID from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS where ABSENT_MINUS_ID= " + AbsentMinusID + ")" + "\r\n";
                Sql = Sql + "Delete from JBHR_EEP.dbo.HRM_ATTEND_ABSENT_TRANS where ABSENT_MINUS_ID= " + AbsentMinusID + "\r\n";
                Sql = Sql + "update JBHR_EEP.dbo.HRM_ATTEND_ABSENT_PLUS set ABSENT_HOURS = ABSENT_HOURS - " + TotalHours + ",REST_HOURS = REST_HOURS + " + TotalHours + " where ABSENT_PLUS_ID = @ABSENT_PLUS_ID" + "\r\n";

                this.ExecuteSql(Sql, connection, transaction);
                transaction.Commit(); // 確認交易
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // 傳回值: 無

        }



    }
}
