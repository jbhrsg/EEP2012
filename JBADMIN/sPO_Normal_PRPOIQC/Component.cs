using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data;
using Newtonsoft.Json;
using System.Data.Sql;

namespace sPO_Normal_PRPOIQC
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

        public string ReturnGetFixed()
        {
            string Year = DateTime.Now.Year.ToString();
            return "PO" + Year;
        }

        //�۰ʰ_�� for ���ʳ�s�W��(�S�Ψ�)
        public object[] FlowStartUp(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            //string ParentKey = aParam[1].ToString();
            string RoleID = "";
            string PONO = "";

            //1.��RoleID
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //��D�ި���ID
                string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + userid + "' and ORG_DESC !='�֩e�|'";
                //�쳡��������ID
                string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)//�O�D��
                {
                    RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                    ds.Dispose();
                }
                else
                {//���O�D��
                    ds = this.ExecuteSql(sql1, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//�O�����U��
                    {
                        RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                    }
                    ds.Dispose();
                }
                transaction.Commit(); // �T�{���
            }
            catch
            {
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }

            //2.��PONO
            if (RoleID != "")
            {
                connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                transaction = connection.BeginTransaction();
                try
                {
                    string sql = "SELECT Top 1 [PONO] FROM [JBADMIN].[dbo].[POMaster] ORDER BY PONO DESC ";// 
                    DataSet ds = this.ExecuteSql(sql, connection, transaction);
                    PONO = ds.Tables[0].Rows[0]["PONO"].ToString();
                    transaction.Commit(); // �T�{���
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }

            //3.�_��
            if (RoleID != "" && PONO != "")//��RoleID�A��PONO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    "D:\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sPO_Normal_PRPOIQC.POMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                    },

                    new object[]{
                    "PONO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "PONO='"+ PONO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                    }
                });
                    ret[1] = true;
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
            }

            return ret;
        }

        //��ʰ_��
        public object[] FlowStartUpByHand(object[] objParam) {
            object[] ret = new object[] { 0, 0 };
            string[] aParam = objParam[0].ToString().Split(',');
            string userid = aParam[0].ToString();
            string PONO = aParam[1].ToString();
            string RoleID = "";

            //1.��RoleID
            IDbConnection connection = (IDbConnection)AllocateConnection("EIPHRSYS");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                //��D�ި���ID
                string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + userid + "' and ORG_DESC !='�֩e�|'";
                //�쳡��������ID
                string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + userid + "'";
                DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                if (ds.Tables[0].Rows.Count > 0)//�O�D��
                {
                    RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                    ds.Dispose();
                }
                else
                {//���O�D��
                    ds = this.ExecuteSql(sql1, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//�O�����U��
                    {
                        RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                    }
                    ds.Dispose();
                }
                transaction.Commit(); // �T�{���
            }
            catch
            {
                ret[1] = false;
                return ret;
            }
            finally
            {
                ReleaseConnection("EIPHRSYS", connection);
            }

            //3.�_��
            if (RoleID != "" && PONO != "")//��RoleID�A��PONO
            {
                try
                {
                    EEPRemoteModule ep = new EEPRemoteModule();
                    object[] ret1=ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                    null,
                    new object[]{
                    //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    //"C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    "D:\\INFOLIGHT\\EEP2012\\EEPNetServer\\Workflow\\FL\\PO_Normal_PRPOIQC.xoml",
                    string.Empty,////�ťէY�i�A�t�Ψϥ�
                    0,//�O�_�����n�ӽ�
                    0,//�O�_�����ӽ�
                    "",//����N������
                    RoleID,//�ӽЪ̪�RoleID(����s��)
                    "sPO_Normal_PRPOIQC.POMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                    0,//�t�Ψϥ�
                    "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                    "" //����
                    },
                    new object[]{
                    "PONO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                    "PONO='"+ PONO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                    }
                });
                    ret[1] = true;
                }
                catch
                {
                    ret[1] = false;
                    return ret;
                }
            }


            return ret;
        }

        //���o��´�s��(�p�G�ۤv���b��´��)�B���ݥD�ު���´�s��
        public object[] GetUserOrgNOs(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string UserID = parm[0];
                string sql = "SELECT dbo.funReturnEmpOrgNOL2('" + UserID + "') AS OrgNO, dbo.funReturnEmpOrgNOParent('" + UserID + "')  AS OrgNOParent  FROM EIPHRSYS.dbo.Users WHERE UserID='" + UserID + "'";
                //funReturnEmpOrgNOL2--�ڦb����´�Χںު���´(�X�Ӫ���´���W�h��´�O�`�g��)(L2����´)
                //funReturnEmpOrgNOParent--�ڦb����´(�u��)�Χںު���´���W�h��´(���ݪ���´)
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;

        }

        public object[] GetOrgNO_CostCenterID(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string OrgNO = parm[0];
                string sql = "SELECT distinct cc.[CostCenterID]";
                sql = sql + " FROM [JBADMIN].[dbo].[glCostCenter] cc";
                sql = sql + " left join [EIPHRSYS].[dbo].[SYS_ORG] so on so.TOCOSTCENTERID=cc.CostCenterID";
                sql = sql + " where IsActive=1 and so.org_no='" + OrgNO + "'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;

        }

        public object[] GetPlanPayDate(object[] objParam)
        {
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
                string[] parm = objParam[0].ToString().Split(',');
                string EndDay = parm[0];
                string DebtorDays = parm[1];
                string AcceptanceDate = parm[2];
                string sql = "select dbo.funReturnAPPlanPayDate ('" + EndDay + "','" + DebtorDays + "','" + AcceptanceDate + "')";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw(ex);
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js }; ;

        }

        //�s�W��ƨ��f����(�б��ʩ��� ���񭺧��f����B�����f�ƶq)(���ʧ@�~)
        private void ucPODetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            //���񭺧��f����έ����f�ƶq
            if (ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString() != "" && (ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "" && ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "0"))
            {
                string PONO = ucPODetails.GetFieldCurrentValue("PONO").ToString();
                string ItemNO = ucPODetails.GetFieldCurrentValue("ItemNO").ToString();
                string FirstDeliveryDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString()).ToString("yyyy/MM/dd");//�h���W�ȤU��
                string FirstDeliveryQty = ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString();
                string CreateBy = ucPODetails.GetFieldCurrentValue("CreateBy").ToString();
                string PurPrice = ucPODetails.GetFieldCurrentValue("PurPrice").ToString();
                string PurVendor = ucPODetails.GetFieldCurrentValue("PurVendor").ToString();//���ʼt��

                //string CreateDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("CreateDate").ToString()).ToString("yyyy/MM/dd hh:mm:ss");

                //if (FirstDeliveryDate != "" && (FirstDeliveryQty != "" && FirstDeliveryQty != "0"))//���񭺧��f����έ����f�ƶq
                //{
                //�쵲�b�覡
                string sql00 = "select POPayTypeID from dbo.POMaster where PONO='" + PONO + "'";
                DataTable tb = this.ExecuteSql(sql00, ucPODetails.conn, ucPODetails.trans).Tables[0];

                    string sql0 = "select count(ItemNO) from dbo.PODelivery where PONO='" + PONO + "' and ItemNO='" + ItemNO + "' and DeliveryNO='" + ItemNO.Substring(1, 2) + "'";
                    string result = this.ExecuteSql(sql0, ucPODetails.conn, ucPODetails.trans).Tables[0].Rows[0][0].ToString();
                    //pOPayTypeID
                    if (result == "0" && tb.Rows.Count>0)//�S�s�W�L
                    {
                        if (tb.Rows[0].ItemArray[0].ToString() != "3")//���b�覡�D�����I��
                        {
                            string vendAccount = "";
                            string payTermName = "";
                            string sql1 = "select v.VendAccount,p.PayTermName from dbo.Vendors v left join dbo.PayTerm p on p.PayTermID=v.PayTermID where v.VendID='" + PurVendor + "'";
                            DataSet ds = this.ExecuteSql(sql1, ucPODetails.conn, ucPODetails.trans);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                vendAccount = ds.Tables[0].Rows[0]["VendAccount"].ToString();
                                payTermName = ds.Tables[0].Rows[0]["PayTermName"].ToString();
                            }

                            string sql = "insert into dbo.PODelivery(" + "\r\n";
                            sql = sql + "[PONO],[ItemNO],[DeliveryNO],[DeliveryDate],[DeliveryQty],[PurPrice],[CreateBy],[CreateDate],[DebtorDays],[AccountNO],[ReturnQty]) values " + "\r\n";
                            sql = sql + "('" + PONO + "','" + ItemNO + "','" + ItemNO.Substring(1, 2) + "',convert(datetime,'" + FirstDeliveryDate + "', 111),'" + FirstDeliveryQty + "'," + PurPrice + ",'" + CreateBy + "',GETDATE(),'" + payTermName + "','" + vendAccount + "',0)" + "\r\n";
                            this.ExecuteCommand(sql, ucPODetails.conn, ucPODetails.trans);
                        }
                    }
                //}
            }
        }

        //�s�W��ƨ��f����(�б��ʩ��� ���񭺧��f����B�����f�ƶq)(���ʧ@�~)(�����I��)
        private void ucPODetails_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            string d1 = ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString();
            string d2 = ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString();
            //���񭺧��f����έ����f�ƶq
            if (ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString() != "" && (ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "" && ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString() != "0"))
            {
                string PONO = ucPODetails.GetFieldCurrentValue("PONO").ToString();
                string ItemNO = ucPODetails.GetFieldCurrentValue("ItemNO").ToString();
                string FirstDeliveryDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("FirstDeliveryDate").ToString()).ToString("yyyy/MM/dd");//�h���W�ȤU��
                string FirstDeliveryQty = ucPODetails.GetFieldCurrentValue("FirstDeliveryQty").ToString();
                //string CreateBy = ucPODetails.GetFieldCurrentValue("CreateBy").ToString();
                string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string CreateBy = SrvGL.GetUserName(user_id);

                string PurPrice = ucPODetails.GetFieldCurrentValue("PurPrice").ToString();
                string PurVendor = ucPODetails.GetFieldCurrentValue("PurVendor").ToString();//���ʼt��

                //string CreateDate = DateTime.Parse(ucPODetails.GetFieldCurrentValue("CreateDate").ToString()).ToString("yyyy/MM/dd hh:mm:ss");

                //if (FirstDeliveryDate != "" && (FirstDeliveryQty != "" && FirstDeliveryQty != "0"))//���񭺧��f����έ����f�ƶq
                //{
                //�쵲�b�覡
                string sql00 = "select POPayTypeID from dbo.POMaster where PONO='" + PONO + "'";
                DataTable tb = this.ExecuteSql(sql00, ucPODetails.conn, ucPODetails.trans).Tables[0];

                string sql0 = "select count(ItemNO) from dbo.PODelivery where PONO='" + PONO + "' and ItemNO='" + ItemNO + "' and DeliveryNO='" + ItemNO.Substring(1, 2) + "'";
                string result = this.ExecuteSql(sql0, ucPODetails.conn, ucPODetails.trans).Tables[0].Rows[0][0].ToString();

                if (result == "0" && tb.Rows.Count > 0)//�S�s�W�L
                {   
                    if (tb.Rows[0][0].ToString() == "3"){//���b�覡�O�����I��
                        string vendAccount = "";
                        string payTermName = "";
                        string sql1 = "select v.VendAccount,p.PayTermName from dbo.Vendors v left join dbo.PayTerm p on p.PayTermID=v.PayTermID where v.VendID='" + PurVendor + "'";
                        DataSet ds = this.ExecuteSql(sql1, ucPODetails.conn, ucPODetails.trans);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            vendAccount = ds.Tables[0].Rows[0]["VendAccount"].ToString();
                            payTermName = ds.Tables[0].Rows[0]["PayTermName"].ToString();
                        }

                        string sql = "insert into dbo.PODelivery(" + "\r\n";
                        sql = sql + "[PONO],[ItemNO],[DeliveryNO],[DeliveryDate],[DeliveryQty],[PurPrice],[CreateBy],[CreateDate],[DebtorDays],[AccountNO],[ReturnQty]) values " + "\r\n";
                        sql = sql + "('" + PONO + "','" + ItemNO + "','" + ItemNO.Substring(1, 2) + "',convert(datetime,'" + FirstDeliveryDate + "', 111),'" + FirstDeliveryQty + "'," + PurPrice + ",'" + CreateBy + "',GETDATE(),'" + payTermName + "','" + vendAccount + "',0)" + "\r\n";
                        this.ExecuteCommand(sql, ucPODetails.conn, ucPODetails.trans);
                    }
                }
                //}
            }
        }


        private void ucPOMaster_AfterApplied(object sender, EventArgs e)
        {
            string PONO = "";
            try
            {
                PONO = ucPOMaster.GetFieldCurrentValue("PONO").ToString();


                string sql3 = "SELECT D_STEP_ID FROM EIPHRSYS.dbo.SYS_TODOLIST WHERE FORM_PRESENTATION = 'PONO=''" + PONO + "'''";
                DataTable tb = this.ExecuteSql(sql3, ucPOMaster.conn, ucPOMaster.trans).Tables[0];
                string d_STEP_ID = "";
                if (tb.Rows.Count > 0)
                {
                    d_STEP_ID = tb.Rows[0][0].ToString();//���d
                }
                string pOPayTypeID = ucPOMaster.GetFieldCurrentValue("POPayTypeID").ToString();//���b�覡

                //[�]��FlagDeliveryEnough]���ʧ@�~�Υ���w�ơA�Y��f�w�Ƨ����A�N��FlagDeliveryEnough=1
                if ((d_STEP_ID == "���ʧ@�~" || d_STEP_ID == "���ʪ̥���w��") && pOPayTypeID != "3")
                {
                    string sql0 = "select sum(PurQty) from dbo.PODetails where PONO='" + PONO + "'";//���ʼƶq
                    string result0 = this.ExecuteSql(sql0, ucPOMaster.conn, ucPOMaster.trans).Tables[0].Rows[0][0].ToString();

                    string sql1 = "select sum(DeliveryQty) from dbo.PODelivery where PONO='" + PONO + "'";//��f�ƶq
                    string result1 = this.ExecuteSql(sql1, ucPOMaster.conn, ucPOMaster.trans).Tables[0].Rows[0][0].ToString();

                    if (String.IsNullOrEmpty(result0) != true && String.IsNullOrEmpty(result1) != true)
                    {
                        if (result0 == result1 && Convert.ToInt32(result0) > 0 && Convert.ToInt32(result1) > 0)//�۵��N�N�����w�Ƨ���
                        {
                            string sql2 = "Update dbo.[POMaster] Set [FlagDeliveryEnough]=1 where PONO='" + PONO + "'" + "\r\n";
                            this.ExecuteCommand(sql2, ucPOMaster.conn, ucPOMaster.trans);
                        }
                    }
                }
                else if (d_STEP_ID == "���ʧ@�~" && pOPayTypeID == "3")//���ʧ@�~�T�w�����I�ڡA�N����w�Ƨ���
                {
                    string sql2 = "Update dbo.[POMaster] Set [FlagDeliveryEnough]=1 where PONO='" + PONO + "'" + "\r\n";
                    this.ExecuteCommand(sql2, ucPOMaster.conn, ucPOMaster.trans);
                }
            }
            catch//�R��
            {
                PONO = ucPOMaster.GetFieldOldValue("PONO").ToString();
            }
            
        }

        //�]�Ʋ��ʳ�_��B�аO�w�@����ServerMethod(IsAssetCompleted=1)
        public object[] InsertAssetApplyFromPO_Normal_PRPOIQC(object[] objParam)
        {
            //�ت�:���ʵ��b���N�i�J�]�Ʋ��ʳ�A�B�_��]�Ʋ��ʳ�A��sIsAssetCompleted=1(�N���]��ServerMethod)
            DataRow dr = (DataRow)objParam[0];
            IDbConnection connection;
            IDbTransaction transaction;
            string pONO = dr["PONO"].ToString();
            string pOPayTypeID = dr["POPayTypeID"].ToString();

            int flowDirection = (int)objParam[1];//1�e�i 2�h�^
            if (flowDirection == 1)
            {
                //�s�B�����ʶ��طs�W���f����(��D�ɬO�����I�� �B ���ʵ��b�� �B ���������I�ڤw���㧹��)
                if (pOPayTypeID == "3")
                {
                    connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                    if (connection.State != ConnectionState.Open) { connection.Open(); }
                    transaction = connection.BeginTransaction();

                    try
                    {
                        //�б��ʩ��Ӫ��������ت��`���ʼƶq
                        string sql4 = "select sum(PurQty) from dbo.PODetails where PONO='" + pONO + "' and ItemID='I99999'";
                        string sumPurQty = this.ExecuteSql(sql4, connection, transaction).Tables[0].Rows[0][0].ToString();

                        //��f���Ӫ��������ت��w���b�������`�禬�M�h�f�ƶq
                        string sql5 = "select sum(AcceptanceQty)+sum(ReturnQty) from dbo.PODelivery where PONO='" + pONO + "' and (PayWayID !='' and  PayWayID is not null)";
                        string sumAcceptanceQty = this.ExecuteSql(sql5, connection, transaction).Tables[0].Rows[0][0].ToString();

                        string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                        string user_name = SrvGL.GetUserName(user_id);

                        if (String.IsNullOrEmpty(sumPurQty) != true && String.IsNullOrEmpty(sumAcceptanceQty) != true)
                        {
                            //�۵��N����������I�ڤw���㧹��
                            if (sumPurQty == sumAcceptanceQty && Convert.ToInt32(sumPurQty) > 0 && Convert.ToInt32(sumAcceptanceQty) > 0)
                            {
                                //�����ʶ��طs�W���f����
                                string sql6 = "Insert Into PODelivery (" + "\r\n";
                                sql6 = sql6 + "[PONO],[ItemNO],[DeliveryNO],[DeliveryDate]" + "\r\n";
                                sql6 = sql6 + ",[DeliveryQty]" + "\r\n";//��f�ƶq
                                sql6 = sql6 + ",[PurPrice],[OtherFee]" + "\r\n";//���~����B�u�{���B
                                sql6 = sql6 + ",[AcceptanceQty],[AcceptanceTax],[TotalPrice]" + "\r\n";//�禬�ƶq�B�禬�|�B�B�`��
                                sql6 = sql6 + ",[ProofTypeID],[InvoiceNO],[PayWayID],[DebtorDays],[Surveyors]" + "\r\n";//�˪��̾�                     
                                sql6 = sql6 + ",[CreateBy],[CreateDate]" + "\r\n";
                                sql6 = sql6 + ",[AcceptanceDate],[AccountNO]" + "\r\n";//�禬����B�״ڱb��

                                sql6 = sql6 + ") select " + "\r\n";
                                sql6 = sql6 + "[PONO],[ItemNO],SUBSTRING(ItemNO,2,2),(select top 1 DeliveryDate from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",[PurQty]" + "\r\n";//���ʼƶq(��f�ƶq)
                                sql6 = sql6 + ",PurPrice,null" + "\r\n";//���ʳ��(���~���)
                                sql6 = sql6 + ",PurQty,PurTax,(PurPrice*PurQty)+PurTax" + "\r\n";//���ʼƶq(�禬�ƶq)�B���ʵ|�B(�禬�|�B)�B�����`��
                                sql6 = sql6 + ",(select top 1 ProofTypeID from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 InvoiceNO from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 PayWayID from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 DebtorDays from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 Surveyors from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",'" + user_name + "',getdate()" + "\r\n";
                                sql6 = sql6 + ",(select top 1 AcceptanceDate from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";
                                sql6 = sql6 + ",(select top 1 AccountNO from PODelivery where PONO='" + pONO + "' order by AcceptanceDate desc,DeliveryNO desc)" + "\r\n";

                                sql6 = sql6 + "from  PODetails where PONO='" + pONO + "' and ItemID!='I99999'" + "\r\n";
                                this.ExecuteCommand(sql6, connection, transaction);
                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                    }
                }


                //�@�B��X�n�J�]�Ʋ��ʳ檺��f���Ө�DataTable��
                connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                transaction = connection.BeginTransaction();

                try
                {
                    //foreach (DataRow dr0 in tb.Rows)
                    //{
                    string sql = "EXEC procInsertAssetApplyFromPO_Normal_PRPOIQC '" + pONO + "'";
                    this.ExecuteCommand(sql, connection, transaction);
                    //}
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }

                //�G�B�����s�W���\�A�A�_��(�_��e��_��ӽЪ̪�¾�٥N���B��s�W���겣���ʳ渹TranNO(where ���ʳ渹 �M ���_��))

                //1.��RoleID
                string RoleID = "";
                string applyUserID = dr["ApplyUserID"].ToString();
                connection = (IDbConnection)AllocateConnection("EIPHRSYS");
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                transaction = connection.BeginTransaction();
                try
                {
                    //string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    //��D�ި���ID
                    string sql0 = "SELECT ORG_MAN   FROM [EIPHRSYS].[dbo].[SYS_ORG] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ORG_MAN=u.GROUPID where USERID='" + applyUserID + "' and ORG_DESC !='�֩e�|'";
                    //�쳡��������ID
                    string sql1 = "SELECT  ROLE_ID FROM [EIPHRSYS].[dbo].[SYS_ORGROLES] s inner join [EIPHRSYS].[dbo].[USERGROUPS] u on s.ROLE_ID=u.GROUPID where USERID='" + applyUserID + "'";
                    DataSet ds = this.ExecuteSql(sql0, connection, transaction);
                    if (ds.Tables[0].Rows.Count > 0)//�O�D��
                    {
                        RoleID = ds.Tables[0].Rows[0]["ORG_MAN"].ToString();
                        ds.Dispose();
                    }
                    //���O�D��
                    else
                    {
                        ds = this.ExecuteSql(sql1, connection, transaction);
                        if (ds.Tables[0].Rows.Count > 0)//�O����
                        {
                            RoleID = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                        }
                        ds.Dispose();
                    }
                    transaction.Commit(); // �T�{���
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ReleaseConnection("EIPHRSYS", connection);
                }

                //2.��TranNO
                DataTable dtTranNO = new DataTable();
                if (RoleID != "")
                {
                    connection = AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    transaction = connection.BeginTransaction();
                    try
                    {
                        string sql = "SELECT [TranNO] FROM [AssetApplyMaster] where left(PONO,11)='" + pONO + "' and flowflag is null";// 
                        dtTranNO = this.ExecuteSql(sql, connection, transaction).Tables[0];
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                    }
                }
                //3.�_��
                if (RoleID != "" && dtTranNO.Rows.Count > 0)
                {
                    try
                    {
                        string TranNO;
                        foreach (DataRow drow in dtTranNO.Rows)
                        {
                            TranNO = drow["TranNO"].ToString();

                            object[] cInfo = new object[] { this.ClientInfo[0], -1, 1, "" };
                            SrvGL.LogUser(applyUserID, "", "", 1);//userid,username �����n�J(�ν��ʪ̰_��)
                            ((object[])cInfo[0])[1] = applyUserID;

                            EEPRemoteModule ep = new EEPRemoteModule();
                            //object[] ret = ep.CallFLMethod(this.ClientInfo, "Submit", new object[]{
                            object[] ret = ep.CallFLMethod(cInfo, "Submit", new object[]{
                            null,
                            new object[]{
                                //"C:\\Program Files (x86)\\Infolight\\EEP2012\\EEPNetServer\\Workflow\\FL\\AssetApply.xoml",
                                "C:\\EEP2012\\EEPNetServer\\Workflow\\FL\\AssetApply.xoml",
                                string.Empty,////�ťէY�i�A�t�Ψϥ�
                                0,//�O�_�����n�ӽ�
                                0,//�O�_�����ӽ�
                                "",//����N������
                                RoleID,//�ӽЪ̪�RoleID(����s��)
                                "sAssetApplyMaster.AssetApplyMaster",//Server�ݪ�Dll�W�٥H�ι�����InfoCommand���W�r�A��pS001.InfoCommand1
                                0,//�t�Ψϥ�
                                "0",//��´���O�s��ex:0���q��´�B1�֧Q�e���|
                                "" //����
                            },
                            new object[]{
                                "TranNO",//TAble��������A�p�G�O�h�����զX���ܡA�i�H�H�����j�}�A��p�G"OrderID;CustomerID"
                                "TranNO='"+ TranNO +"'"//+a[0]+b[0] //key�ȲզX�A�Ҧp�G"OrderID=10260;CustomerID=����A001����" �]A001���k���O�O��ӳ�޸��^
                            }
                            });
                            SrvGL.LogUser(applyUserID, "", "", -1);    //�����n�X

                            if (ret[0].ToString() != "0")//�_�椣���\�A0�O���\
                            {
                                throw new Exception(ret[0] + "," + ret[1]);
                            }
                        }//foreach
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                //�T�B���ެO�@�몫�~�٬O���ª��~�A�u�n�]����server method�Nupdate PODelivery set IsAssetCompleted=1
                connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                transaction = connection.BeginTransaction();
                try
                {
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);

                    string sql = "update PODelivery set IsAssetCompleted=1,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]=getdate() where PONO='" + pONO + "' and (IsAssetCompleted !=1 or IsAssetCompleted is null) and (PayWayID is not null and PayWayID!='') ";//���겣���ʳ�_�����d�B�z �B ���ʵ��b���� ((TotalPrice!='' and TotalPrice is not null) or TotalPrice=0)
                    this.ExecuteCommand(sql, connection, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            //�����ѱ��A�]���禬�S�����A�|goto(�h�^) �B�ثePODelivery.IsAPCompleted is null
            //�h�^(��IsAssetCompleted�]null�A���겣���ʳ�n�h�^�ӽЪ̨ä�ʧ@�o)
            else
            {
                //    connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                //    if (connection.State != ConnectionState.Open) { connection.Open(); }
                //    transaction = connection.BeginTransaction();
                //    try
                //    {
                //        string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                //        string user_name = SrvGL.GetUserName(user_id);

                //        string sql = "update PODelivery set IsAssetCompleted=null,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]=getdate() where PONO='" + pONO + "' and IsAssetCompleted=1 and (PayWayID is not null and PayWayID!='') and IsAPCompleted is null ";//�w�]�L�겣���ʳ�_�����d �B ���ʵ��b���� �B ���]�L�����b�ڳB�z
                //        this.ExecuteCommand(sql, connection, transaction);
                //        transaction.Commit();
                //    }
                //    catch (Exception ex)
                //    {
                //        transaction.Rollback();
                //        throw (ex);
                //    }
                //    finally
                //    {
                //        ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                //    }
            }



            return new object[] { 0, 0 };
        }

        //�^�Ǩt���ܼ�(�������ɤT�Ӫ��`�����e)
        public object[] sysVariable_Default(object[] objParam) {

            //string js = string.Empty;
            int CategoryValue=0;
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
                string sql = "SELECT  [CategoryValue] from SYS_Variable   where [Category]='DaysNeedInquery'";
                DataSet ds = this.ExecuteSql(sql, connection, transaction);
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                CategoryValue =Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, CategoryValue }; ;
        }

        //�^��true false�A�Ytrue�N�����ʪ̥���w��//���ʪ̵L����w��
        public object[] IsPurchaserNotCompleted(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                //string FlagDeliveryEnough = dr["FlagDeliveryEnough"].ToString();
                string PONO = dr["PONO"].ToString();
                string sql = "Select count(PONO) from PODelivery where PONO='"+PONO+"'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int counts = Int32.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                if (counts == 0)//��f���� �L
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
            return ret;
        }

        //[�^��true false] ���ʼƶq�禬����(flow��)
        public object[] IsAllReceived(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "Select ISNULL(sum(PurQty)-(select sum(AcceptanceQty)+sum(ReturnQty) from PODelivery where PONO='"+PONO+"'),-1) from PODetails where PONO='"+PONO+"'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                int diff = Int32.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                if (diff==0)
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
            return ret;

        }

        //�����b�ڳB�z
        public object[] PutPODeliveryToAPDetails(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            int flowDirection = (int)objParam[1];//1�e�i 2�h�^
            if (flowDirection == 1)
            {
                string js = string.Empty;
                DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);
                    string PONO = dr["PONO"].ToString();

                    string sql = "EXEC procPutPODeliveryToAPDetails 1,'" + PONO + "','" + user_name + "'";
                    this.ExecuteCommand(sql, connection, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            return ret;

        }

        //�]���ؿ��L�b��겣���ʥD�ɡB�겣�D��
        public object[] PostIsCatalogue(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
            int flowDirection = (int)objParam[1];//1�e�i 2�h�^
            if (flowDirection == 1)
            {
                string js = string.Empty;
                DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                    string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    string user_name = SrvGL.GetUserName(user_id);
                    string PONO = dr["PONO"].ToString();
                    string IsCatalogue = dr["IsCatalogue"].ToString();
                    string sql = "select [flowflag] from AssetApplyMaster where substring(PONO,1,11)='" + PONO + "'";
                    DataTable tb =this.ExecuteSql(sql, connection, transaction).Tables[0];
                    //�p�G�O�]���ؿ�==1�BAssetApplyMaster��,�h
                    //��sAssetApplyMaster.IsCatalogue=1
                    //�YAssetApplyMaster.Flowflag=='Z'�A�h��sAssetMaster.IsCatalogue=1
                    if ((IsCatalogue == "True" || IsCatalogue == "1") && tb.Rows.Count > 0)
                    {
                        string sql0 = "update AssetApplyMaster set IsCatalogue=1 where substring(PONO,1,11)='" + PONO + "'";
                        this.ExecuteCommand(sql0, connection, transaction);
                        string flowflag = tb.Rows[0][0].ToString();
                        if (flowflag == "Z")
                        {
                            string sql1 = "update AssetMaster set IsCatalogue=1,[LastUpdateBy]='" + user_name + "',[LastUpdateDate]=GETDATE() where substring(PONO,1,11)='" + PONO + "'";
                            this.ExecuteCommand(sql1, connection, transaction);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
                }
            }
            return ret;

        }

        public object[] NullProcedure(object[] objParam)
        {
                    return new object[] { 0, 0 };
        }
        //�����`�B
        public object[] PRTotalAmount(object[] objParam)
        {
            
            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "select sum(RegPrice*RegQty) from PODetails where PONO='" + PONO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount <= 3000)
                {
                ret[1] = true;
                }else{
                ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        public object[] PRTotalAmount1(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "select sum(RegPrice*RegQty) from PODetails where PONO='" + PONO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 3000 && amount <=10000)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        public object[] PRTotalAmount2(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "select sum(RegPrice*RegQty) from PODetails where PONO='" + PONO + "'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 10000)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        //����w�Ƨ����A�����Ǧ^true(flow��)
        public object[] DeliveryIsEnough(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string POPayTypeID = dr["POPayTypeID"].ToString();

                if (POPayTypeID != "3")
                {
                    string sql0 = "select sum(PurQty) from dbo.PODetails where PONO='" + PONO + "'";//���ʼƶq
                    string result0 = this.ExecuteSql(sql0, connection, transaction).Tables[0].Rows[0][0].ToString();

                    string sql1 = "select sum(DeliveryQty) from dbo.PODelivery where PONO='" + PONO + "'";//��f�ƶq
                    string result1 = this.ExecuteSql(sql1, connection, transaction).Tables[0].Rows[0][0].ToString();
                    
                    if (String.IsNullOrEmpty(result0) != true && String.IsNullOrEmpty(result1) != true)
                    {
                        if (result0 == result1 && Convert.ToInt32(result0) > 0 && Convert.ToInt32(result1) > 0)//�۵��N�N�����w�Ƨ���
                        {
                            ret[1] = true;
                        }
                        else {
                            ret[1] = false;
                        }
                    }
                }
                else if (POPayTypeID == "3")//�����I�ڡA�N����w�Ƨ���
                {
                    ret[1] = true;
                }
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
            return ret;

        }


        //�����`�B
        public object[] POTotalAmount(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "select FLOOR(sum(PurPrice*PurQty)) from PODetails where PONO='" + PONO + "' and ItemID!='I99999'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount <= 3000)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        public object[] POTotalAmount1(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "select FLOOR(sum(PurPrice*PurQty)) from PODetails where PONO='" + PONO + "' and ItemID!='I99999'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 3000 && amount <= 10000)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }

        public object[] POTotalAmount2(object[] objParam)
        {

            object[] ret = new object[] { 0, 0 };
            string js = string.Empty;
            DataRow dr = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
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
                string PONO = dr["PONO"].ToString();
                string sql = "select FLOOR(sum(PurPrice*PurQty)) from PODetails where PONO='" + PONO + "' and ItemID!='I99999'";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                decimal amount = Convert.ToDecimal(dsTemp.Tables[0].Rows[0][0].ToString());
                if (amount > 10000)
                {
                    ret[1] = true;
                }
                else
                {
                    ret[1] = false;
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return ret;

        }
    }
}
