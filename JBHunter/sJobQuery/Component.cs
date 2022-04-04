using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections;
using System.Data.SqlClient;

namespace sJobQuery
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

        public object[] AddMenu(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string type = parm[0];
            string UserID = parm[1];
            string JobID = parm[2];         
            string AssignID = parm[3];
            DateTime AssignTime = DateTime.Now;
            if (type == "2")
            {
                AssignTime = DateTime.Parse(parm[4]);
            }
            DateTime CreateDate = DateTime.Now;
            string LoginUser = GetClientInfo(ClientInfoType.LoginUser).ToString();

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
            {   //求目前最大編號
                string sql = " select 'A'+RIGHT('000000000'+RTRIM(LTRIM(Right(max(AssignNO),9)+1)),9) as AssignNO from HUT_JobAssignLogs ";
                DataSet dsAssignNO = this.ExecuteSql(sql, connection, transaction);
                string AssignNO = dsAssignNO.Tables[0].Rows[0]["AssignNO"].ToString();

                string SQL = @"Insert HUT_JobAssignLogs(AssignNO,UserID,JobID,AssignID,AssignTime,CreateBy,CreateDate) Select @AssignNO,@UserID,@JobID,@AssignID,@AssignTime,@LoginUser,@CreateDate";      
                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@AssignNO", AssignNO));
                Parameter.Add(new SqlParameter("@UserID", UserID));
                Parameter.Add(new SqlParameter("@JobID", JobID));
                Parameter.Add(new SqlParameter("@AssignID", AssignID));
                Parameter.Add(new SqlParameter("@AssignTime", AssignTime));
                Parameter.Add(new SqlParameter("@LoginUser", LoginUser));  
                Parameter.Add(new SqlParameter("@CreateDate", CreateDate));                           
               this.ExecuteSql(SQL, connection, transaction, Parameter);
               //n.AssignID=6不適任=>表示移出餐盤,刪除HUT_JobAssignNew資料
               if (AssignID == "6")
               {
                   string SQL2 = " Delete from HUT_JobAssignNew where UserID='" + UserID + "' and JobID=" + JobID;
                   this.ExecuteSql(SQL2, connection, transaction);
               }
               else
               {
                   //判斷HUT_JobAssignNew是否有資料=>決定新增'修改
                   string sql2 = " select count(*) as iCount from HUT_JobAssignNew where UserID='" + UserID + "' and JobID=" + JobID;
                   DataSet dsCount = this.ExecuteSql(sql2, connection, transaction);
                   string iCount = dsCount.Tables[0].Rows[0]["iCount"].ToString();

                   if (iCount == "0")//新增
                   {
                       string SQL2 = @"Insert HUT_JobAssignNew(AssignNO,UserID,JobID,AssignID,AssignTime,LastUpdateBy,LastUpdateDate) Select @AssignNO,@UserID,@JobID,@AssignID,@CreateDate,@LoginUser,@CreateDate";
                       ArrayList Parameter2 = new ArrayList();
                       Parameter2.Add(new SqlParameter("@AssignNO", AssignNO));
                       Parameter2.Add(new SqlParameter("@UserID", UserID));
                       Parameter2.Add(new SqlParameter("@JobID", JobID));
                       Parameter2.Add(new SqlParameter("@AssignID", AssignID));
                       Parameter2.Add(new SqlParameter("@CreateDate", CreateDate));
                       Parameter2.Add(new SqlParameter("@LoginUser", LoginUser));
                       this.ExecuteSql(SQL2, connection, transaction, Parameter2);
                   }
                   else//修改
                   {
                       string SQL2 = @"Update HUT_JobAssignNew set AssignNO=@AssignNO,JobID=@JobID,AssignID=@AssignID,AssignTime=@AssignTime,LastUpdateBy=@LoginUser,LastUpdateDate=@CreateDate where UserID=@UserID and JobID=@JobID";
                       ArrayList Parameter2 = new ArrayList();
                       Parameter2.Add(new SqlParameter("@AssignNO", AssignNO));
                       Parameter2.Add(new SqlParameter("@UserID", UserID));
                       Parameter2.Add(new SqlParameter("@JobID", JobID));
                       Parameter2.Add(new SqlParameter("@AssignID", AssignID));
                       Parameter2.Add(new SqlParameter("@AssignTime", AssignTime));
                       Parameter2.Add(new SqlParameter("@CreateDate", CreateDate));
                       Parameter2.Add(new SqlParameter("@LoginUser", LoginUser));
                       this.ExecuteSql(SQL2, connection, transaction, Parameter2);
                   }
               }
              
               //// Indented縮排 將資料轉換成Json格式
                js = JsonConvert.SerializeObject(AssignNO, Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();                
            }
            finally
            {
                
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        private void ucHUT_JobAssignNew_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {
            //刪除後若還有資料須修改HUT_JobAssignNew的資料,沒有則刪除HUT_JobAssignNew資料
            string UserID = ucHUT_JobAssignNew.GetFieldOldValue("UserID").ToString();
            int JobID = Convert.ToInt32(ucHUT_JobAssignNew.GetFieldOldValue("JobID"));
            string LoginUser = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string sql = "exec procUpdateJobAssignNew '" + UserID + "'," + JobID + ",'" + LoginUser + "'";
            this.ExecuteSql(sql, ucHUT_JobAssignNew.conn, ucHUT_JobAssignNew.trans);
        }
       
    }
}
