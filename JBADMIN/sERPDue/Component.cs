using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using System.Linq;

namespace sERPDue
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
        public string GetDueNOPrefix(){
            DateTime today1 = DateTime.Today;
            return "DNO" + (today1.Year).ToString().Trim();
        }
        public string GetCEConfirmNOPrefix() {
            DateTime today1 = DateTime.Today;
            return "RE-" + today1.ToString("yyyyMMdd").Substring(0, 6).Trim();
        }
        //�۰ʰ_��
        public object[] JBERPContinueEmployStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string DueNO = aParam[0].ToString();
            string sAutoKey = aParam[1].ToString();
            string sLaborName = aParam[2].ToString();
            string sGender = aParam[3].ToString();
            string sEmployer = aParam[4].ToString();
            string sCountry = aParam[5].ToString();
            string sImmigrationDate = aParam[6].ToString();
            string sDueDate = aParam[7].ToString();
            string sIsRecontract = aParam[8].ToString();
            string sCEConfirmNO = aParam[9].ToString();
            string sTransfer = aParam[10].ToString();
            string sReturnHome = aParam[11].ToString();
            string sBackPot = aParam[12].ToString();
            string sSalesID = aParam[13].ToString();
            string sTransferAg = aParam[14].ToString();

            string[] aAutoKey = sAutoKey.Split('$');
            string[] aLaborName = sLaborName.Split('$');
            string[] aGender = sGender.Split('$');
            string[] aEmployer = sEmployer.Split('$');
            string[] aCountry = sCountry.Split('$');
            string[] aImmigrationDate = sImmigrationDate.Split('$');
            string[] aDueDate = sDueDate.Split('$');
            string[] aIsRecontract = sIsRecontract.Split('$');
            string[] aCEConfirmNO = sCEConfirmNO.Split('$');
            string[] aTransfer = sTransfer.Split('$');
            string[] aReturnHome = sReturnHome.Split('$');
            string[] aBackPot = sBackPot.Split('$');
            string[] aSalesID = sSalesID.Split('$');
            string[] aTransferAg = sTransferAg.Split('$');

            List<string[]> lAutoKey = new List<string[]>(); foreach (string element in aAutoKey) { lAutoKey.Add(element.Split('*')); }
            List<string[]> lLaborName = new List<string[]>(); foreach (string element in aLaborName) { lLaborName.Add(element.Split('*')); }
            List<string[]> lGender = new List<string[]>(); foreach (string element in aGender) { lGender.Add(element.Split('*')); }
            List<string[]> lEmployer = new List<string[]>(); foreach (string element in aEmployer) { lEmployer.Add(element.Split('*')); }
            List<string[]> lCountry = new List<string[]>(); foreach (string element in aCountry) { lCountry.Add(element.Split('*')); }
            List<string[]> lImmigrationDate = new List<string[]>(); foreach (string element in aImmigrationDate) { lImmigrationDate.Add(element.Split('*')); }
            List<string[]> lDueDate = new List<string[]>(); foreach (string element in aDueDate) { lDueDate.Add(element.Split('*')); }
            List<string[]> lIsRecontract = new List<string[]>(); foreach (string element in aIsRecontract) { lIsRecontract.Add(element.Split('*')); }
            List<string[]> lCEConfirmNO = new List<string[]>(); foreach (string element in aCEConfirmNO) { lCEConfirmNO.Add(element.Split('*')); }
            List<string[]> lTransfer = new List<string[]>(); foreach (string element in aTransfer) { lTransfer.Add(element.Split('*')); }
            List<string[]> lReturnHome = new List<string[]>(); foreach (string element in aReturnHome) { lReturnHome.Add(element.Split('*')); }
            List<string[]> lBackPot = new List<string[]>(); foreach (string element in aBackPot) { lBackPot.Add(element.Split('*')); }
            List<string[]> lSalesID = new List<string[]>(); foreach (string element in aSalesID) { lSalesID.Add(element.Split('*')); }
            List<string[]> lTransferAg = new List<string[]>(); foreach (string element in aTransferAg) { lTransferAg.Add(element.Split('*')); }


            for (int i = 0; i < lEmployer.Count; i++)
            {
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
                    //����ContinueEmployNO����4�X
                    string sql = "select top 1 ContinueEmployNO from [JBADMIN].[dbo].[ERPContinueEmployMaster]  order by ContinueEmployNO desc";
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    string ContinueEmployNO = string.Empty;
                    ContinueEmployNO = (ds.Tables[0].Rows.Count == 0) ? "" : ds.Tables[0].Rows[0]["ContinueEmployNO"].ToString();
                    string suffix = "";
                    if (ContinueEmployNO != "")
                    {
                        if (ContinueEmployNO.Substring(3, 4) == DateTime.Today.Year.ToString("0000"))//�P�~��
                        {
                            suffix = (Convert.ToInt32(ContinueEmployNO.Substring(7, 4)) + 1).ToString("0000"); ;//�P�~�P����~��[1
                        }
                        else//���P�~��
                        {
                            suffix = "0001";
                        }
                    }
                    else
                    {
                        suffix = "0001";
                    }
                    //����ContinueEmployNO
                    ContinueEmployNO = "INF"+DateTime.Today.Year.ToString("0000") + suffix;

                    //���ƨ�q����D��
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);
                    string sql2 = "insert into  ERPContinueEmployMaster ([ContinueEmployNO],[CreateBy],[CreateDate],[SalesID],[Employer1])(select '" + ContinueEmployNO + "','" + user_name + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + lSalesID[i][0] + "','" + lEmployer[i][0] + "')";
                    ExecuteCommand(sql2, connection, transaction);

                    //���ƨ�q���������
                    for (int j = 0; j < lAutoKey[i].Length; j++)
                    {
                        string sql3 = "insert into  ERPContinueEmployDetail" + "\r\n";
                        sql3 = sql3 + "([AutoKey],[ContinueEmployNO],[LaborName],[Gender],[Employer],[Country],[ImmigrationDate],[DueDate],[IsRecontract],[CEConfirmNO],[Transfer],[ReturnHome],[BackPot],[TransferAg]) values " + "\r\n";
                        sql3 = sql3 + "('" + lAutoKey[i][j] + "','" + ContinueEmployNO + "','" + lLaborName[i][j] + "','" + lGender[i][j] + "','" + lEmployer[i][j] + "','" + lCountry[i][j] + "','" + lImmigrationDate[i][j] + "','" + lDueDate[i][j] + "','" + lIsRecontract[i][j] + "','" + lCEConfirmNO[i][j] + "','" + lTransfer[i][j] + "','" + lReturnHome[i][j] + "','" + lBackPot[i][j] + "','" + lTransferAg[i][j] + "')" + "\r\n";
                        ExecuteCommand(sql3, connection, transaction);
                    }
                    transaction.Commit(); // �T�{���

                    //�A�_��
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                null,
                new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\ContinueEmploy.xoml",
                    "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\ContinueEmploy.xoml",//�y�{���W�r�A�]�t������|�C//"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\Requisition.xoml"
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    "1071071",//�ӽЪ̪�RoleID(����s��) //�¯ª�����ID
                    "sERPContinueEmploy.ERPContinueEmployMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                },
                new object[]{
                    "ContinueEmployNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "ContinueEmployNO ='"+ ContinueEmployNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                }
                    });
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            ret[1] = true;
            return ret;
        }

        //���ͳ����T
        public object[] ReportOrders(object[] objParam)
        {
            string OrderNo = objParam[0].ToString();
            string sCEConfirmNO = objParam[1].ToString();
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
                string sql = "SELECT m.*,d.*,u.EMAIL,v.FeeAmount from ERPDueFormMaster m " + "\r\n";
                sql = sql + "left join ERPDueFormDetail d on m.DueNO=d.DueNO " + "\r\n";
                sql = sql + "left join [EIPHRSYS].[dbo].[USERS] u on u.USERID=d.SalesID " + "\r\n";
                sql = sql + "left join (" + "\r\n";
                sql = sql + "select * from [192.168.1.41].FWCRM.dbo.View_EmployeeDebt" + "\r\n";
                sql = sql + "union all" + "\r\n";
                sql = sql + "select * from [192.168.1.41].FWCRMJS.dbo.View_EmployeeDebt" + "\r\n";
                sql = sql + ") v on v.ResidenceID=d.ResidenceID" + "\r\n";
                sql = sql + "where m.DueNO = '" + OrderNo + "' and d.CEConfirmNO in (" + sCEConfirmNO + ")" + "\r\n";;
                
                
                DataSet ds = this.ExecuteSql(sql, connection, transaction);

                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);//datatable�নstring
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

        //������~�Ҹ�ƶ��T�{��(ERPDueForm)���
        public object[] SelectLab(object[] objParam){
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string DueYM = parm[0];
            string DueNO = parm[1];
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("lab");//�b���
            IDbConnection connection1 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open){connection.Open();}
            if (connection1.State != ConnectionState.Open){connection1.Open();}

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            IDbTransaction transaction1 = connection1.BeginTransaction();
            try
            {
                //�ˬdDueNO���L�����ɡA���N���ηs�W������
                string sqls = "  select  d.DueNO from ERPDueFormDetail d left join [ERPDueFormMaster] m on m.DueNO=d.DueNO where (m.DeleteFlag is NULL or m.DeleteFlag='') and d.DueNO='" + DueNO + "'";
                DataSet dss = this.ExecuteSql(sqls, connection1, transaction1);
                js = JsonConvert.SerializeObject(dss.Tables[0], Formatting.Indented);
                if (dss.Tables[0].Rows.Count > 0)
                {
                    ret[1]=js;
                }
                else
                {
                    //������~�Ҹ��
                    string sql = "SELECT [lab_cname] as LaborName,[lab_name] as eLaborName,c.title as Employer,c.ename as eEmployer,c.contact as Contact,c.sal_no,e.emp_name as SalesName,s.csex as Gender,n.nat_name as Country,SUBSTRING(l.cas_no,10,1) as FirSec," + "\r\n";
                    sql = sql + "lab_idate as ImmigrationDate,lab_edate as DueDate," + "\r\n";
                    sql = sql + "(select count(cus_no)  FROM [lab] ll where SUBSTRING(ll.lab_edate,1,5) ='" + DueYM + "' and l.cus_no=ll.cus_no and ll.lab_status=1 group by cus_no) as CusEmployees," + "\r\n";
                    sql = sql + "ROW_NUMBER() OVER(partition by l.cus_no order by lab_no) as RowNumber,'" + DueYM + "' as DueYM" + "\r\n";
                    sql = sql + ",'" + DueYM + "'+right('000'+convert(nvarchar(3),DENSE_RANK() OVER (ORDER BY l.cus_no)),3) AS CEConfirmTrackNO" + "\r\n";
                    sql = sql + ",SUBSTRING(cas_no,7,4) AS WaveNO,l.lab_lno" + "\r\n";
                    sql = sql + " FROM [lab] l left join [nat] n on n.nat_no=l.nat_no" + "\r\n";
                    sql = sql + "left join [cus] c on c.cus_no=l.cus_no" + "\r\n";
                    sql = sql + "left join ((select 'M' as sex,'�k' as csex)union(select 'F' as sex,'�k' as csex)) s on s.sex= l.sex" + "\r\n";
                    sql = sql + "left join emp e on e.emp_no = c.sal_no" + "\r\n";        
                    //sql = sql + "left join (" + "\r\n";
                    //sql = sql + "select * from FWCRM.dbo.View_EmployeeDebt" + "\r\n";
                    //sql = sql + "union all" + "\r\n";
                    //sql = sql + "select * from FWCRMJS.dbo.View_EmployeeDebt" + "\r\n";
                    //sql = sql + ") d on d.ResidenceID=l.lab_lno" + "\r\n";
                    sql = sql + "where SUBSTRING(lab_edate,1,5) ='" + DueYM + "' and l.lab_status=1 and (c.cus_no not in( 'A00001','A00002') and  c.cus_no not like 'FC%')" + "\r\n";

                    DataSet ds = this.ExecuteSql(sql, connection, transaction);

                    //�맻��~�Ҹ�ƨ�ERPDueFormDetail
                    string CEConfirmNO1,UserID;
                    string sql2,sql21;
                    string Year, Year1, MonthDay, MonthDay1;
                    DataSet ds1;
                    int counts = 0;
                    counts = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < counts; i++)
                    {
                        sql21 = "select [USERID],[USERNAME] from [EIPHRSYS].[dbo].[USERS] where [USERNAME] ='" + ds.Tables[0].Rows[i]["SalesName"].ToString().Trim() + "'";
                        ds1 = this.ExecuteSql(sql21, connection1, transaction1);
                        UserID = ds1.Tables[0].Rows[0]["USERID"].ToString().Trim();
                        //suffix = suffix + 1;
                        //CEConfirmNO1 = "RE-" + DateTime.Today.Year.ToString("0000") + DateTime.Today.Month.ToString("00") + suffix.ToString("000");
                        CEConfirmNO1 = ds.Tables[0].Rows[i]["DueYM"].ToString().Trim() + '-' + counts.ToString("D3") + '-' + (i+1).ToString("D3") + '(' + ds.Tables[0].Rows[i]["CusEmployees"].ToString().Trim() + '-' + ds.Tables[0].Rows[i]["RowNumber"].ToString().Trim() + '-' + ds.Tables[0].Rows[i]["FirSec"].ToString().Trim()+ ')';
                        Year = (Convert.ToInt32(ds.Tables[0].Rows[i]["ImmigrationDate"].ToString().Trim().Substring(0, 3)) + 1911).ToString();
                        MonthDay = ds.Tables[0].Rows[i]["ImmigrationDate"].ToString().Trim().Substring(3, 4);
                        Year1 = (Convert.ToInt32(ds.Tables[0].Rows[i]["DueDate"].ToString().Trim().Substring(0, 3)) + 1911).ToString();
                        MonthDay1 = ds.Tables[0].Rows[i]["DueDate"].ToString().Trim().Substring(3, 4);
                        sql2 = " insert into ERPDueFormDetail ([DueNO],[AutoKey],[LaborName],[eLaborName],[Employer],[eEmployer],[Contact]" + "\r\n";
                        sql2 = sql2 + ",[Gender],[Country],[ImmigrationDate],[DueDate],[CEConfirmNO],[SalesID],[SalesName],[CEConfirmTrackNO],[WaveNO],[ResidenceID]) values " + "\r\n";
                        sql2 = sql2 + "('" + DueNO + "'," + (Convert.ToInt32(i) + 1) + ",'" + ds.Tables[0].Rows[i]["LaborName"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["eLaborName"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["Employer"].ToString().Trim() + "'," + "\r\n";
                        sql2 = sql2 + "'" + ds.Tables[0].Rows[i]["eEmployer"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["Contact"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["Gender"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["Country"].ToString().Trim() + "'," + "\r\n";
                        sql2 = sql2 + "'" + Year + MonthDay + "','" + Year1 + MonthDay1 + "','" + CEConfirmNO1 + "','" + UserID + "','" + ds.Tables[0].Rows[i]["SalesName"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["CEConfirmTrackNO"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["WaveNO"].ToString().Trim() + "','" + ds.Tables[0].Rows[i]["lab_lno"].ToString().Trim() + "')" + "\r\n";
                        this.ExecuteSql(sql2, connection1, transaction1);
                    }
                    ret[1] = true;
                }
                transaction.Commit();
                transaction1.Commit();
            }
            catch
            {
                transaction.Rollback();
                ret[1]=false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection1);
                ReleaseConnection("lab", connection);
            }
            //return new object[] { 0, js };
            return ret;
        }

        //������~�Ҹ�ƶ�M�T�{�Ѯt�����H���T�{��(ERPDueForm)���
        public object[] SelectLab1(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string DueYM = parm[0];
            string DueNO = parm[1];
            string js = string.Empty;

            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection("lab");
            IDbConnection connection1 = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());

            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            if (connection1.State != ConnectionState.Open) { connection1.Open(); }

            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            IDbTransaction transaction1 = connection1.BeginTransaction();
            try
            {
                //���T�{�ѻP���骺�t�����
                string sql0 = "select ltrim(rtrim(lab_name)) as lab_name,ltrim(rtrim(lab_edate)) as lab_edate from [192.168.1.41].lab.dbo.lab " + "\r\n";
                sql0 = sql0 + "where substring(lab_edate,1,5)='" + DueYM + "' and lab_status=1 except" + "\r\n";
                sql0 = sql0 + "select ltrim(rtrim(d.eLaborName)),convert(varchar,convert(int,Year(d.DueDate))-1911)" + "\r\n";
                sql0 = sql0 + "+CONVERT(VARCHAR,REPLICATE('0',2-LEN(MONTH(DueDate))))+CONVERT(VARCHAR,MONTH(DueDate))" + "\r\n";
                sql0 = sql0 + "+CONVERT(VARCHAR,REPLICATE('0',2-LEN(DAY(DueDate))))+CONVERT(VARCHAR,DAY(DueDate)) " + "\r\n";
                sql0 = sql0 + "from JBADMIN.dbo.ERPDueFormDetail d left join ERPDueFormMaster m on m.DueNO=d.DueNO" + "\r\n";
                sql0 = sql0 + "where (m.DeleteFlag = 'FALSE' or m.DeleteFlag is null or m.DeleteFlag ='') and m.DueYM='" + DueYM + "'" + "\r\n";
                DataSet ds0 = this.ExecuteSql(sql0, connection1, transaction1);
                //�p����ƦA���X����~�ҧ����ơA�A���ERPDueFormDetail
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    string CEConfirmNO1, UserID;
                    string sql2, sql21, sql22,sqlPeopleCounts;
                    string Year, Year1, MonthDay, MonthDay1;
                    DataSet ds21, ds22,dsPeopleCounts;
                    int Autokey;
                    ArrayList lab_nameAList = new ArrayList();
                    string lab_names;
                    int dsCounts,peopleCounts;
                    for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    {
                        lab_nameAList.Add("'" + ds0.Tables[0].Rows[i]["lab_name"] + "'");
                    }
                    lab_names = "(" + String.Join(",", lab_nameAList.ToArray()) + ")";
                    //�ھڮt����Ƽ��X����~�ҧ�����
                    string sql = "SELECT [lab_cname] as LaborName,[lab_name] as eLaborName,c.title as Employer,c.ename as eEmployer,c.contact as Contact,c.sal_no,e.emp_name as SalesName,s.csex as Gender,n.nat_name as Country,SUBSTRING(l.cas_no,10,1) as FirSec," + "\r\n";
                    sql = sql + "lab_idate as ImmigrationDate,lab_edate as DueDate," + "\r\n";
                    sql = sql + "(select count(cus_no)  FROM [lab] ll where SUBSTRING(ll.lab_edate,1,5) ='" + DueYM + "' and l.cus_no=ll.cus_no and ll.lab_status=1 group by cus_no) as CusEmployees," + "\r\n";
                    sql = sql + "ROW_NUMBER() OVER(partition by l.cus_no order by lab_no) as RowNumber,'" + DueYM + "' as DueYM" + "\r\n";
                    sql = sql + ",SUBSTRING(cas_no,7,4) AS WaveNO,l.lab_lno" + "\r\n";
                    sql = sql + " FROM [lab] l left join [nat] n on n.nat_no=l.nat_no" + "\r\n";
                    sql = sql + "left join [cus] c on c.cus_no=l.cus_no" + "\r\n";
                    sql = sql + "left join ((select 'M' as sex,'�k' as csex)union(select 'F' as sex,'�k' as csex)) s on s.sex= l.sex" + "\r\n";
                    sql = sql + "left join emp e on e.emp_no = c.sal_no" + "\r\n";
                    sql = sql + "where SUBSTRING(lab_edate,1,5) ='" + DueYM + "' and l.lab_status=1 and lab_name in " + lab_names + " and (c.cus_no not in( 'A00001','A00002') and  c.cus_no not like 'FC%')\r\n";// and lab_edate='" + ds0.Tables[0].Rows[i]["lab_edate"] + "'" + "\r\n";
                    ds = this.ExecuteSql(sql, connection, transaction);
                    dsCounts = ds.Tables[0].Rows.Count;

                    //�ثeDueNO�����`�H�ơA���F��T�{�ѽs��
                    sqlPeopleCounts = "SELECT count([AutoKey]) as DetailsCounts FROM [JBADMIN].[dbo].[ERPDueFormDetail] where DueNO='" + DueNO + "'";
                    dsPeopleCounts = this.ExecuteSql(sqlPeopleCounts, connection1, transaction1);
                    peopleCounts = Convert.ToInt32(dsPeopleCounts.Tables[0].Rows[0]["DetailsCounts"].ToString().Trim());


                    for (int j = 0; j < dsCounts; j++)
                    {
                        //UserID,AutoKey
                        sql21 = "select [USERID],[USERNAME] from [EIPHRSYS].[dbo].[USERS] where [USERNAME] ='" + ds.Tables[0].Rows[j]["SalesName"].ToString().Trim() + "'";
                        ds21 = this.ExecuteSql(sql21, connection1, transaction1);
                        UserID = ds21.Tables[0].Rows[0]["USERID"].ToString().Trim();
                        sql22 = "SELECT Top 1 [AutoKey] FROM [JBADMIN].[dbo].[ERPDueFormDetail] where DueNO='" + DueNO + "' order by AutoKey desc";
                        ds22 = this.ExecuteSql(sql22, connection1, transaction1);
                        Autokey = Convert.ToInt32(ds22.Tables[0].Rows[0]["AutoKey"].ToString().Trim());
                        Autokey = Autokey + 1;
                        
                        CEConfirmNO1 = ds.Tables[0].Rows[j]["DueYM"].ToString().Trim() + '-' + (peopleCounts+dsCounts).ToString("D3")+'-'+(peopleCounts+j+1).ToString("D3") + '(' + ds.Tables[0].Rows[j]["CusEmployees"].ToString().Trim() + '-' + ds.Tables[0].Rows[j]["RowNumber"].ToString().Trim() + '-' + ds.Tables[0].Rows[j]["FirSec"].ToString().Trim()+ ')';
                        Year = (Convert.ToInt32(ds.Tables[0].Rows[j]["ImmigrationDate"].ToString().Trim().Substring(0, 3)) + 1911).ToString();
                        MonthDay = ds.Tables[0].Rows[j]["ImmigrationDate"].ToString().Trim().Substring(3, 4);
                        Year1 = (Convert.ToInt32(ds.Tables[0].Rows[j]["DueDate"].ToString().Trim().Substring(0, 3)) + 1911).ToString();
                        MonthDay1 = ds.Tables[0].Rows[j]["DueDate"].ToString().Trim().Substring(3, 4);
                        //�s�W�@��
                        sql2 = " insert into ERPDueFormDetail ([DueNO],[AutoKey],[LaborName],[eLaborName],[Employer],[eEmployer],[Contact]" + "\r\n";
                        sql2 = sql2 + ",[Gender],[Country],[ImmigrationDate],[DueDate],[CEConfirmNO],[SalesID],[SalesName],[WaveNO],[ResidenceID]) values " + "\r\n";
                        sql2 = sql2 + "('" + DueNO + "'," + Autokey + ",'" + ds.Tables[0].Rows[j]["LaborName"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["eLaborName"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["Employer"].ToString().Trim() + "'," + "\r\n";
                        sql2 = sql2 + "'" + ds.Tables[0].Rows[j]["eEmployer"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["Contact"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["Gender"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["Country"].ToString().Trim() + "'," + "\r\n";
                        sql2 = sql2 + "'" + Year + MonthDay + "','" + Year1 + MonthDay1 + "','" + CEConfirmNO1 + "','" + UserID + "','" + ds.Tables[0].Rows[j]["SalesName"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["WaveNO"].ToString().Trim() + "','" + ds.Tables[0].Rows[j]["lab_lno"].ToString().Trim() + "')" + "\r\n";
                        this.ExecuteSql(sql2, connection1, transaction1);
                    }
                }
                ret[1] = true;

                transaction.Commit();
                transaction1.Commit();
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection1);
                ReleaseConnection("lab", connection);
            }
            //return new object[] { 0, js };
            return ret;
        }

        //�S�Ψ�
        public object[] SelectERPDueFormDetail(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string DueNO = parm[0];
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try {
                string sql = "select  *,'' as FlowFlag from ERPDueFormDetail where DueNO = '" + DueNO + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
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

        //�ˬd��������O�_����
        public object[] SelectDueYM(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string DueYM = parm[0];
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open){connection.Open();}
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "select  DueYM from ERPDueFormMaster where DueYM='" + DueYM + "' and (DeleteFlag is NULL) or (DeleteFlag =0) or(DeleteFlag ='') ";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ret[1] = js;
                }
                else
                {
                    ret[1] = true;
                }
                transaction.Commit();
            }
            catch{
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally{
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
        }

        //�ˬd�Y�T�{���`�檺�T�{�Ѹ̦��q����y�{���A�A�N����R���T�{���`��
        public object[] OnDelete_dataGridView(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string[] parm = objParam[0].ToString().Split(',');
            string DueNO = parm[0];
            string js = string.Empty;
            //�إ߸�Ʈw�s��
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            //��s�u���A������open�ɡA�}�ҳs��
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            //�}�ltransaction
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "SELECT dd.DueNO,u.* FROM dbo.[ERPDueFormDetail] dd right join(select distinct CEConfirmNO,LaborName,FlowFlag from [ERPContinueEmployDetail] d left join [ERPContinueEmployMaster] m on d.ContinueEmployNO= m.ContinueEmployNO where FlowFlag in('N','P','Z')) u on u.CEConfirmNO=dd.CEConfirmNO where dd.DueNO='"+DueNO+"'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ret[1] = js;
                }
                else
                {
                    ret[1] = true;
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;
        }
    }
}
