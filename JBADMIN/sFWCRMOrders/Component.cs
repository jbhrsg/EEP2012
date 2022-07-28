using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMOrders
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
        //�q�渹�X=> ex: FWA1050001----����~+�y���X(4)
        public string OrderNoFixed()
        {
            int OrderYear = int.Parse(ucFWCRMOrders.GetFieldCurrentValue("OrderYear").ToString());

            DateTime datetime = DateTime.Today;
            return "FWO" + (OrderYear - 1911).ToString().Trim();
        }
        //��ܨӷ��q��=>�a�X�q���T
        public object[] BindFWCRMOrders(object[] objParam)
        {
            string OrderNo = (string)objParam[0];
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select distinct f.OrderNo,d.Item,d.PlanIndate,d.Gender,d.org_okno,d.Notes," + "\r\n";
                   sql = sql + "SUM(d.PersonQtyOriginal) as PersonQtyOriginal,f.NationalityID,f.CreateBy," + "\r\n";
                   sql = sql + "(select Isnull(sum(PersonQty),0) from FWCRMIndateCheck where OrderNo=f.OrderNo) as PersonQtyFinal" + "\r\n";
                   sql = sql + " from FWCRMOrders f inner join FWCRMOrdersDetails d on f.OrderNo=d.OrderNo" + "\r\n";
                   sql = sql + " where f.OrderNo='" + OrderNo + "' " + "\r\n";
                   sql = sql + " group by f.OrderNo,f.NationalityID,f.CreateBy,d.Item,d.PlanIndate,d.Gender,d.org_okno,d.Notes " + "\r\n";
                   //sql = sql + " having SUM(d.PersonQtyOriginal)-SUM(d.PersonQtyFinal)!=0 " + "\r\n";
                   sql = sql + " order by f.OrderNo desc " + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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

        
        //�ˬd�D�u�i�׺��@����
        public object[] ReturnStickStatusCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string OrderNo = parm[0];
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) as cnt from FWCRMStickStatus where OrderNo='" + OrderNo + "'";

                DataSet dsFWCRMStickStatus = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRMStickStatus.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
            //return new object[] { 0, true };
        }    
        private void ucFWCRMIndateCheck_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucFWCRMIndateCheck.SetFieldValue("LastUpdateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucFWCRMIndateCheck.SetFieldValue("LastUpdateBy", LoginUser);//�����
        }

        //���ͭq�����
        public object[] ReportOrders(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string OrderNo = parm[0];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = "select m.OrderNo,m.OrderType,m.WorkNo,m.EmployerID,r.EmployerName,m.FromOrderNo, " + "\r\n";
                sql = sql + "m.NationalityID,dbo.funReturnReferenceData('nationality',m.NationalityID) as NationalityText, " + "\r\n";
                sql = sql + "d.Item,d.PlanIndate,d.PersonQtyOriginal,d.sgn_type,d.sgn_no,d.org_okno,d.Notes, " + "\r\n";
                sql = sql + "d.Gender,case d.Gender when 1 then '�k' else '�k' end as GenderText,WorkTime, " + "\r\n";
                sql = sql + "case WorkTime when 1 then '3�~' when 3 then '3�~' when 6 then '3�~' else WorkTimeReason end as WorkTimeReason,IsOnSite,OnSiteDays," + "\r\n";
                sql = sql + "dbo.funReturnFWCRMHotelFee('" + OrderNo + "',1) as OnSiteDescribe," + "\r\n";
                sql = sql + "dbo.funReturnFWCRMHotelFee('" + OrderNo + "',2) as sTakeType " + "\r\n";
                sql = sql + "from FWCRMOrders m  " + "\r\n";
                sql = sql + "inner join FWCRMOrdersDetails d on m.OrderNo=d.OrderNo " + "\r\n";
                sql = sql + "inner join dbo.View_FWCRMEmployer r on m.EmployerID=r.EmployerID " + "\r\n";
                sql = sql + " where m.OrderNo='" + OrderNo + "'" + "\r\n";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);

                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {

                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }

        //flow �O�_���縹
        public object flowCheckOrg_okno(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string OrderNo = dr["OrderNo"].ToString();

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) as cnt from FWCRMOrdersDetails where OrderNo='" + OrderNo + "' and (Isnull(org_okno,'') ='�ӽФ�' or Isnull(org_okno,'') ='')";

                DataSet dsFWCRM= this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                if (cnt == "0")
                    ret[1] = true;//�~��y�{
                else
                    ret[1] = false;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        //flow �ˬd��
        public object flowChecksup_no(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string OrderNo = dr["OrderNo"].ToString();

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) as cnt from FWCRMOrders where OrderNo='" + OrderNo + "' and Isnull(sup_no,'') =''";

                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                if (cnt == "0")
                    ret[1] = true;//�~��y�{
                else
                    ret[1] = false;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        //flow �O�_����~��
        public object flowChecksupno(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string OrderNo = dr["OrderNo"].ToString();

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                string sql = " select COUNT(*) as cnt from FWCRMOrders where OrderNo='" + OrderNo + "' and Isnull(sup_no,'')!=Isnull(sup_noOld,'')";

                DataSet dsFWCRM = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsFWCRM.Tables[0].Rows[0]["cnt"].ToString();
                transaction.Commit();
                //Indented�Y�� �N����ഫ��Json�榡
                if (cnt == "1")
                    ret[1] = false;//�~��y�{
                else
                    ret[1] = true;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        //flow �~�ȵ��׽T�{
        public object flowCheckClose(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            string CloseDate = dr["CloseDate"].ToString();

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {               
                if (CloseDate != "")
                    ret[1] = true;
                else
                    ret[1] = false;
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret; // �Ǧ^��: �L
        }

        private void ucFWCRMOrders_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            //DateTime datetime = DateTime.Today;
            //ucFWCRMOrders.SetFieldValue("OrderYear", datetime.Year);//�����

        }

        private void ucFWCRMOrders_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //�ק��~��	
            string sup_noOld = ucFWCRMOrders.GetFieldOldValue("sup_no").ToString(); ;//�ק�e
            string sup_no= ucFWCRMOrders.GetFieldCurrentValue("sup_no").ToString(); ;//�ק��
            if (sup_noOld != sup_no)
            {
                //���@�ˤ~�n���
                ucFWCRMOrders.SetFieldValue("sup_noOld", sup_noOld);//�����
            }
        }

      




    }
}
