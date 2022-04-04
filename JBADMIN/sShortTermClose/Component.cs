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

namespace sShortTermClose
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
        //¼È­É´Ú³æµ²®×
        public object[] PutShortTermEnd(object[] objParam)
        {
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
            string[] parm = objParam[0].ToString().Split(',');
            string ShortTermNO = parm[0].ToString();
            string DiffAmt = parm[1].ToString();
            string EndReason = parm[2].ToString();
            string CashType = parm[3].ToString();
            IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            IDbTransaction trans = conn.BeginTransaction();
            try
            {
                string sql = "EXEC procPutShortTermEnd  '" + ShortTermNO + "','" + DiffAmt + "','" + EndReason + "','" + CashType + "','" + userName + "'";
                this.ExecuteSql(sql, conn, trans);
            }
            catch
            {
                trans.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                trans.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, true };
        }

       
    }
}
