using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;

namespace sFWCRMOrdersPeopleCount
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

        //產生訂單報表
        public object[] ReportOrdersPeopleCount(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string iYear = parm[0];
            string SalesID = parm[1];
            string WorkTime = parm[2];//工期
            string NationalityID = parm[3];//國籍         
            string EmployerName = parm[4];//雇主         

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string userName = SrvGL.GetUserName(userid);
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
                string sql = "select OrderYear as iYear,o.OrderNo,Left(r.EmployerName,4) as EmployerName,o.SalesID,b.NAME_C,o.NationalityID,y.NationalityName," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and WorkTime=1) as WorkTimeCount1," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and WorkTime=2) as WorkTimeCount2," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and WorkTime=3) as WorkTimeCount3," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and WorkTime=4) as WorkTimeCount4," + "\r\n";
                sql = sql + " Case when D_STEP_ID='國外組挑工' then StatusName when o.flowflag='X' then '作廢' when o.CloseType=1 then '作廢' when o.flowflag='Z' then '結束' else v.D_STEP_ID end as flowflagText," + "\r\n";
                sql = sql + " dbo.funReturnFWCRMReferenceTableData('OrderStatus',o.OrderStatus) as OrderStatusText," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=1) as month1," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=2) as month2," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=3) as month3," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=4) as month4," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=5) as month5," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=6) as month6," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=7) as month7," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=8) as month8," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=9) as month9," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=10) as month10," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=11) as month11," + "\r\n";
                sql = sql + " (select sum(PersonQty) from FWCRMIndateCheck where o.OrderNo=OrderNo and month(PlanIndate2)=12) as month12," + "\r\n";

                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='01') as Plan1," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='02') as Plan2," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='03') as Plan3," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='04') as Plan4," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='05') as Plan5," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='06') as Plan6," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='07') as Plan7," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='08') as Plan8," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='09') as Plan9," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='10') as Plan10," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='11') as Plan11," + "\r\n";
                sql = sql + " (select sum(PersonQtyOriginal) from FWCRMOrdersDetails where o.OrderNo=OrderNo and Right(PlanIndate,2)='12') as Plan12" + "\r\n";

                sql = sql + " from FWCRMOrders o " + "\r\n";
                sql = sql + "inner join dbo.View_FWCRMEmployer r on o.EmployerID=r.EmployerID " + "\r\n";
                sql = sql + "inner join [JBHR_EEP].dbo.HRM_BASE_BASE b on o.SalesID=b.EMPLOYEE_CODE " + "\r\n";
                sql = sql + "inner join FWCRMNationality y on o.NationalityID=y.AutoKey " + "\r\n";
                sql = sql + "left join  dbo.[View_SYS_TODOLIST] v on FLOW_DESC='外勞訂單申請' and STATUS<>'F' and v.BILLNO=o.OrderNo " + "\r\n";
                sql = sql + "left join FWCRMStickStatus s on s.OrderNo=o.OrderNo and s.iAutokey=(select top 1 iAutokey from FWCRMStickStatus where OrderNo=o.OrderNo order by StatusDate desc,iAutokey desc) " + "\r\n";
                sql = sql + "left join FWCRMSetStatus f on s.StatusID=f.iAutoKey " + "\r\n";
                sql = sql + " where OrderYear='" + iYear + "'" + "\r\n";
                if (SalesID != "")
                {
                    sql = sql + " and o.SalesID='" + SalesID + "'" + "\r\n";
                }
                if (WorkTime != "")
                {
                    sql = sql + " and WorkTime='" + WorkTime + "'" + "\r\n";
                }
                if (NationalityID != "")
                {
                    sql = sql + " and o.NationalityID='" + NationalityID + "'" + "\r\n";
                }
                if (EmployerName != "")
                {
                    sql = sql + " and r.EmployerName like '%" + EmployerName + "%'" + "\r\n";
                }
                sql = sql + "group by OrderYear,o.OrderNo,r.EmployerName,o.SalesID,b.NAME_C,o.NationalityID,y.NationalityName,o.OrderStatus,o.flowflag,o.CloseType,v.D_STEP_ID,StatusName " + "\r\n";                

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



    }
}
