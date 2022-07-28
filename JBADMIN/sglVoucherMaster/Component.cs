using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using JBTool;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace sglVoucherMaster
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
        //�ǲ��s��=> ex: A1050108010--A�|��,B�|�p+����~+��+��+3�X�y����=>A1051025006
        public string VoucherNoFixed()
        {
            //======================================�D�o�ǲ��s���}�Y======================================
            string CompanyID = ucglVoucherMaster.GetFieldCurrentValue("CompanyID").ToString();

            string VoucherNoTitle = "A";
            //VoucherID=>1 A , 2 A ,3 B
            int VoucherID = int.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherID").ToString());
            if (VoucherID == 3)
            {
                VoucherNoTitle = "B";
            }
                            
            DateTime VoucherDate = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString());
            return CompanyID+VoucherNoTitle + (VoucherDate.Year - 1911).ToString().Trim() + VoucherDate.Month.ToString().PadLeft(2, '0') + VoucherDate.Day.ToString().PadLeft(2, '0');

        }

        //================================================================================================================================//
        /// <summary>Combobox�θ��</summary>
        public class ComboboxField
        {
            public string text { get; set; }

            public string value { get; set; }

            public bool selected { get; set; }

            public ComboboxField()
            {
                selected = false;
            }
        }
        //���o�s�ʤ��q�O=>���	���Ӹ��
        public object[] GetAcno(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var Acno = "";
                try { Acno = Parameter_Input["Ac_no"].ToString(); }
                catch (Exception) { Acno = ""; }

                string SQL = @"
Select	distinct Acno
From	glAccountItem
Where   CompanyID = @CompanyID 
Order By Acno
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("Acno").ToString(),
                    text = m.Field<string>("Acno") ?? "",
                    selected = (m.Field<string>("Acno") == Acno)
                }).ToList();

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //���o�s�ʤ��q�O=>���(�]�t�п��)	���Ӹ��
        public object[] GetAcnoAll(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var Acno = "";
                try { Acno = Parameter_Input["Ac_no"].ToString(); }
                catch (Exception) { Acno = ""; }

                string SQL = @"
select ' ---�п��---' as AcnoText,'0' as Acno
union all
Select	distinct Acno,Acno
From	glAccountItem
Where   CompanyID = @CompanyID 
Order By Acno
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("Acno").ToString(),
                    text = m.Field<string>("AcnoText") ?? "",
                    selected = (m.Field<string>("Acno") == Acno)
                }).ToList();

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //���o�s�ʬ�ة��Ӹ��
        public object[] GetSubAcno(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var Acno = Parameter_Input["Ac_no"].ToString();
                var SubAcno = "";
                try { SubAcno = Parameter_Input["Sub_Acno"].ToString(); }
                catch (Exception) { SubAcno = ""; }
               
                string SQL = @"
Select	SubAcno,
		SubAcno+':'+AcnoName as AcnoName
From	glAccountItem
Where   CompanyID = @CompanyID and Acno=@Acno
Order By SubAcno,AcnoName
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                Parameter.Add(new SqlParameter("@Acno", Acno));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("SubAcno").ToString(),
                    text = m.Field<string>("AcnoName") ?? "",
                    selected = (m.Field<string>("SubAcno") == SubAcno)
                }).ToList();

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //���o�s�ʺK�X�N�����
        public object[] GetDescribeID(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var Acno = Parameter_Input["Ac_no"].ToString();
                var DescribeID = "";
                try { DescribeID = Parameter_Input["Describe_ID"].ToString(); }
                catch (Exception) { DescribeID = ""; }

                string SQL = @"
select '' as DescribeID,'---�п��---' as Describe
union all
Select	DescribeID,
		DescribeID+':'+Describe as Describe
From	glDescribe
Where   CompanyID = @CompanyID and Acno=@Acno
Order By DescribeID,Describe
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                Parameter.Add(new SqlParameter("@Acno", Acno));
                //foreach (SqlParameter aParameter in Parameter) if (aParameter.Value == null) aParameter.Value = DBNull.Value;

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("DescribeID").ToString(),
                    text = m.Field<string>("Describe") ?? "",
                    selected = (m.Field<string>("DescribeID") == DescribeID)
                }).ToList();

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //���o�`�Τ������
        public object[] GetrOftenUsed(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
                var Acno = Parameter_Input["Ac_no"].ToString();
                var OftenUsedEntryID = "";
                try { OftenUsedEntryID = Parameter_Input["OftenUsedEntry_ID"].ToString(); }
                catch (Exception) { OftenUsedEntryID = ""; }

                string SQL = @"
select '' as OftenUsedEntryID,'---�п��---' as UsedName
union all
select Cast(AutoKey as nvarchar(5)),UsedName
from glOftenUsedEntryM 
Where   CompanyID = @CompanyID and Acno=@Acno
order by OftenUsedEntryID
";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@CompanyID", CompanyID));
                Parameter.Add(new SqlParameter("@Acno", Acno));

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<string>("OftenUsedEntryID").ToString(),
                    text = m.Field<string>("UsedName") ?? "",
                    selected = (m.Field<string>("OftenUsedEntryID") == OftenUsedEntryID)
                }).ToList();

                //�w�]�Ĥ@��
                if (ComboboxList.Count > 0 && !ComboboxList.Any(m => m.selected == true)) ComboboxList[0].selected = true;

                //�^��
                return new object[] { 0, JsonConvert.SerializeObject(ComboboxList, Formatting.Indented) };
            }
            catch (Exception)
            {
                return new object[] { 0, JsonConvert.SerializeObject(new ArrayList(), Formatting.Indented) };
            }

        }
        //��ܱ`�Τ��� =>�a�X�ǲ���T
        public object[] BindOftenUsedEntry(object[] objParam)
        {
            string OftenUsedEntryID = (string)objParam[0];
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
                string sql = "select d.iAutoKey,d.AutoKey,m.CompanyID,d.Item,d.BorrowLendType,d.Acno,d.SubAcno,i.AcnoName as SubAcnoText,d.CostCenterID,d.DescribeID,d.Describe,d.LastUpdateBy,d.LastUpdateDate " + "\r\n";
                sql = sql + " from glOftenUsedEntryD d "+ "\r\n";
                sql = sql + " inner join glOftenUsedEntryM m on d.AutoKey=m.AutoKey" + "\r\n";
                sql = sql + " left join glAccountItem i on d.Acno=i.Acno and d.SubAcno=i.SubAcno and i.CompanyID=m.CompanyID " + "\r\n";
                sql = sql + " where d.AutoKey=" + OftenUsedEntryID + " order by BorrowLendType" + "\r\n";
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

        //�O�_�n��������=>��Acno,SubAcno��
        public object[] GetCostCenterID(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
            var Acno = Parameter_Input["Ac_no"].ToString();
            var SubAcno = "";
            try { SubAcno = Parameter_Input["Sub_Acno"].ToString(); }
            catch (Exception) { SubAcno = ""; }

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
                string sql = " select Isnull((select bCostCenterID from glAccountItem where CompanyID=" + CompanyID + "";
                sql = sql + " and Acno='" + Acno + "' and SubAcno='" + SubAcno + "' ),0) as bCostCenterID" + "\r\n";
                DataSet dsAccountItem = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsAccountItem.Tables[0].Rows[0]["bCostCenterID"].ToString();
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
        //�s�W���Ӯ��ˬd  => ���+�����ˬd        
        public object[] GetDetailData(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            var CompanyID = Convert.ToInt32(Parameter_Input["Company_ID"]);
            var Acno = Parameter_Input["Ac_no"].ToString();
            var SubAcno = Parameter_Input["Sub_Acno"].ToString(); 

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
                string sql = " select COUNT(*) as iCount from glAccountItem where CompanyID=" + CompanyID + "";
                sql = sql + " and Acno='" + Acno + "' and SubAcno='" + SubAcno + "'" + "\r\n";
                DataSet dsAccountItem = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsAccountItem.Tables[0].Rows[0]["iCount"].ToString();
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
        //=====================  glVoucher�s�W�e�g�J��T  ===========================================================================================================//        
        private void ucglVoucherMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {           
            ucglVoucherMaster.SetFieldValue("CreateDate", DateTime.Now);//�g�J������ɤ���
            //����ǲ�������~���g�J �ǲ��~��VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//�ǲ����
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());

            //�h������Ĥ@�X ����ܶǲ��s��
            string VoucherNo = ucglVoucherMaster.GetFieldCurrentValue("VoucherNo").ToString();
            ucglVoucherMaster.SetFieldValue("VoucherNoShow", VoucherNo.Remove(0, 1));
        }
       
        private void ucglVoucherDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("Acno", ucglVoucherDetails.GetFieldCurrentValue("Acno").ToString().Trim());//�h�ť�
            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
            //�Y���U��2 => �hShow���B�[-��
            int BorrowLendType = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString());
            int AmtShow = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("AmtShow").ToString());
            if (BorrowLendType == 2)
            {
                ucglVoucherDetails.SetFieldValue("Amt", -AmtShow);
            }
            else ucglVoucherDetails.SetFieldValue("Amt", AmtShow);
        }
      
        //==================================================InsertglBalance==============================================================================//
        ////�gglBalance�`�b =>  VoucherNo + Item       
        private void InsertglBalance()
        {
            int CompanyID = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("CompanyID").ToString());
            string VoucherNo = ucglVoucherDetails.GetFieldCurrentValue("VoucherNo").ToString();
            string Item = ucglVoucherDetails.GetFieldCurrentValue("Item").ToString();

            string BorrowLendType = ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString();//1��,2�U(�s)

            string CreateBy = ucglVoucherDetails.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucglVoucherDetails.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");

            string sql = "";
            //----���p�� Detail�D�� AutoKey => ���`�b���=>�[�W���B,�L��ƫh�s�W
            //������ => �ק�----------------------------------------------------------------------------------------------------------------------
            if (BorrowLendType == "1")
            {
                sql = sql + " update glBalance set glBalance.BorrowAmt=b.BorrowAmt" + "\r\n";
            }
            else sql = sql + " update glBalance set glBalance.LendAmt=b.LendAmt" + "\r\n";

            sql = sql + "+(Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and Item=g.Item), " + "\r\n";
            sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v " + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where  g.VoucherNo='" + VoucherNo + "' and g.Item=" + Item + "\r\n";//--(�ק�glBalance�`�b)
            //�L���� => �s�W----------------------------------------------------------------------------------------------------------------------
            sql = sql + " Insert into glBalance(CompanyID,BalanceYear,VoucherType,Acno,SubAcno,CostCenterID,iMonth,BorrowAmt,LendAmt,BudgetAmt,CreateBy,CreateDate,LastUpdateBy,LastUpdateDate)" + "\r\n";
            sql = sql + " select v.CompanyID,v.VoucherYear,u.VoucherType,g.Acno,g.SubAcno,g.CostCenterID,month(v.VoucherDate)," + "\r\n";
            if (BorrowLendType == "1")
            {
                sql = sql + " g.Amt,0,0,'" + @CreateBy + "','" + @CreateDate + "','" + @CreateBy + "','" + @CreateDate + "'" + "\r\n";
            }
            else sql = sql + " 0,g.Amt,0,'" + @CreateBy + "','" + @CreateDate + "','" + @CreateBy + "','" + @CreateDate + "'" + "\r\n";

            sql = sql + " from glVoucherMaster v" + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo" + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " left join glBalance b on v.CompanyID=b.CompanyID  and v.VoucherYear=b.BalanceYear" + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID" + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' and g.Item=" + Item + " and b.AutoKey is null" + "\r\n";//--(�LglBalance�`�b���)

            //�R��glBalance=> �ק��ɶU���B����0���`�b��� --------------------------------------------------------------------------------------------
            sql = sql + " Delete from glBalance where BorrowAmt=0 and LendAmt=0" + "\r\n";

            this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherMaster.trans);
        }

        //==================================================UpdateglBalance==============================================================================//

        private void ucglVoucherMaster_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            //����ǲ�������~���g�J �ǲ��~��VoucherYear
            int iYear = DateTime.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherDate").ToString()).Year;//�ǲ����
            ucglVoucherMaster.SetFieldValue("VoucherYear", iYear.ToString());
        }
        //======================================================DeleteglBalance==========================================================================//
        ////�R��glBalance�`�b     
        private void DeleteglBalance()
        {
            //��ܦ�������
            if (ucglVoucherDetails.GetFieldOldValue("AutoKey") != null)
            {
                string AutoKey = ucglVoucherDetails.GetFieldOldValue("AutoKey").ToString();//Detail�D��
                int VoucherID = int.Parse(ucglVoucherDetails.GetFieldOldValue("VoucherID").ToString());//�ǲ����O =>��l���
                string BorrowLendType = ucglVoucherDetails.GetFieldOldValue("BorrowLendType").ToString();//1��,2�U =>��l���

                string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
                string CreateBy = SrvGL.GetUserName(userid.ToLower());

                string sql = "";

                //--------���p�� Detail�D�� AutoKey => ���B�(�ק�glBalance�`�b)
                if (BorrowLendType == "1")
                {
                    sql = sql + " update glBalance set glBalance.BorrowAmt=b.BorrowAmt" + "\r\n";
                }
                else sql = sql + " update glBalance set glBalance.LendAmt=b.LendAmt" + "\r\n";

                sql = sql + "-(Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and Item=g.Item), " + "\r\n";
                sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
                sql = sql + " from glVoucherMaster v " + "\r\n";
                sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
                sql = sql + " left join glVoucherTypeUnion u on u.VoucherID=" + VoucherID + "\r\n";
                sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
                sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
                sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
                sql = sql + " where  g.AutoKey=" + AutoKey + "\r\n";

                this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);
            }

        }
        //=====================================================Detail Insert===========================================================================//

        //1��h=>���s�WMaster,����Detail=>�s�WglBalance �g�b �ǲ����ӷs�W��
        //�ǲ����ӷs�W��=>�s�WglBalance
        private void ucglVoucherDetails_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
          InsertglBalance();
        }
        //=====================================================Detail Modify===========================================================================//
        //Detail�ק�e=>�ק���B      
        private void ucglVoucherDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucglVoucherDetails.SetFieldValue("Acno", ucglVoucherDetails.GetFieldCurrentValue("Acno").ToString().Trim());//�h�ť�
            ucglVoucherDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//�g�J������ɤ���
            //�Y���U��2 => �hShow���B�[-��
            int BorrowLendType = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("BorrowLendType").ToString());
            int AmtShow = int.Parse(ucglVoucherDetails.GetFieldCurrentValue("AmtShow").ToString());
            if (BorrowLendType == 2)
            {
                ucglVoucherDetails.SetFieldValue("Amt", -AmtShow);
            }
            else ucglVoucherDetails.SetFieldValue("Amt", AmtShow);           

            DeleteglBalance();
        }
        //Detail�ק��=>�s�W
        private void ucglVoucherDetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
           InsertglBalance();           
        }
        
        //======================================================Detail Delete=========================================================================//
        //�R���e      
        private void ucglVoucherDetails_BeforeDelete(object sender, UpdateComponentBeforeDeleteEventArgs e)
       {
           string AutoKey = ucglVoucherDetails.GetFieldOldValue("AutoKey").ToString();
           string VoucherNo = ucglVoucherDetails.GetFieldOldValue("VoucherNo").ToString();
           string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
           string CreateBy = SrvGL.GetUserName(userid.ToLower());

           string sql = " exec procInsertglVoucherDetailsDel " + AutoKey + ",'" + VoucherNo + "','" + CreateBy + "'" + "\r\n";
           this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);

            DeleteglBalance();
        }

        //======================================================Master Update=========================================================================//
        //�D�ɸ�ƭץ� (�ǲ�����ץ�,�i�������P) 1.�e=>�ק�(����B) 2.��=>�s�W(�w������=>�[���B),�L���� => �s�W        
        private void ucglVoucherMaster_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {
            ReduceBalance();
        }
        //======================================================ReduceBalance==========================================================================//
        ////�ק�glBalance�`�b =>  (���q�O�B�ǲ����O�ץ�)
        private void ReduceBalance()
        {
            string VoucherNo = ucglVoucherMaster.GetFieldCurrentValue("VoucherNo").ToString();
            int VoucherID = int.Parse(ucglVoucherMaster.GetFieldOldValue("VoucherID").ToString());//�ǲ����O =>��l���
            int NewVoucherID = int.Parse(ucglVoucherMaster.GetFieldCurrentValue("VoucherID").ToString());//�ǲ����O =>�s���

            int CompanyID = int.Parse(ucglVoucherMaster.GetFieldOldValue("CompanyID").ToString());//���q�O =>��l���

            DateTime OldVoucherDate = DateTime.Parse(ucglVoucherMaster.GetFieldOldValue("VoucherDate").ToString());
            string iMonth = OldVoucherDate.Month.ToString().Trim();

            string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());
            //���� => �ק�(����B)----------------------------------------------------------------------------------------------------------------------          
            string sql = "update glBalance set glBalance.BorrowAmt=b.BorrowAmt - isnull( (select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=1 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LendAmt=b.LendAmt - isnull( ( select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=2 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v " + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on u.VoucherID="+VoucherID + "\r\n";
            sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
            sql = sql + " and  b.iMonth=" + iMonth + " and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' " + "\r\n";

            //���q�O�B�ǲ����O���ܧ�=>�ק�glVoucherDetails���
            if (VoucherID != NewVoucherID)
            {
                sql = sql + " update glVoucherDetails set VoucherID=" + NewVoucherID + ",CompanyID=" + CompanyID + "\r\n";
                sql = sql + " from glVoucherMaster v inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
                sql = sql + " where g.VoucherNo='" + VoucherNo + "' " + "\r\n";
            }

            //�R��glBalance=> �ק��ɶU���B����0���`�b��� --------------------------------------------------------------------------------------------
            sql = sql + " Delete from glBalance where BorrowAmt=0 and LendAmt=0" + "\r\n";

            //�s���
            //�w������=>�[���B----------------------------------------------------------------------------------------------------------------------          
            sql = sql + "update glBalance set glBalance.BorrowAmt=b.BorrowAmt + isnull( (select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=1 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LendAmt=b.LendAmt + isnull( ( select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=2 and Item=g.Item),0), " + "\r\n";
            sql = sql + " glBalance.LastUpdateDate='" + CreateDate + "',glBalance.LastUpdateBy='" + CreateBy + "'" + "\r\n";
            sql = sql + " from glVoucherMaster v " + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo " + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " inner join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear " + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID " + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' " + "\r\n";

            //�L���� => �s�W----------------------------------------------------------------------------------------------------------------------
            sql = sql + " Insert into glBalance(CompanyID,BalanceYear,VoucherType,Acno,SubAcno,CostCenterID,iMonth,BorrowAmt,LendAmt,BudgetAmt,CreateBy,CreateDate,LastUpdateBy,LastUpdateDate)" + "\r\n";
            sql = sql + " select v.CompanyID,v.VoucherYear,u.VoucherType,g.Acno,g.SubAcno,g.CostCenterID,month(v.VoucherDate)," + "\r\n";
            sql = sql + " isnull((Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=1 and Item=g.Item),0)," + "\r\n";
            sql = sql + " isnull((Select Amt from glVoucherDetails where VoucherNo=v.VoucherNo and BorrowLendType=2 and Item=g.Item),0),0,'" + @CreateBy + "','" + @CreateDate + "','" + @CreateBy + "','" + @CreateDate + "'"+"\r\n";
            sql = sql + " from glVoucherMaster v" + "\r\n";
            sql = sql + " inner join glVoucherDetails g on v.VoucherNo=g.VoucherNo" + "\r\n";
            sql = sql + " left join glVoucherTypeUnion u on g.VoucherID=u.VoucherID" + "\r\n";
            sql = sql + " left join glBalance b on v.CompanyID=b.CompanyID and v.VoucherYear=b.BalanceYear" + "\r\n";
            sql = sql + " and MONTH(v.VoucherDate)=b.iMonth and g.Acno=b.Acno and g.SubAcno=b.SubAcno and g.CostCenterID=b.CostCenterID" + "\r\n";
            sql = sql + " and b.VoucherType=u.VoucherType" + "\r\n";
            sql = sql + " where g.VoucherNo='" + VoucherNo + "' and b.AutoKey is null" + "\r\n";//--(�LglBalance�`�b���)

            this.ExecuteCommand(sql, ucglVoucherMaster.conn, ucglVoucherMaster.trans);
        }
        //======================================================Grid Master�R���ˬd=========================================================================//
        //1.�O�_�w����
        public object[] CheckDeleteglVoucherMaster(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            string VoucherDate = DateTime.Parse(Parameter_Input["Voucher_Date"].ToString()).ToShortDateString();
            int CompanyID = int.Parse(Parameter_Input["Company_ID"].ToString());

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
                string sql = " select count(*) as cnt from glVoucherLockYM where IsActive=1 and CompanyID=" + CompanyID + " and LockYM=Left(CONVERT(nvarchar(10),Cast('" + VoucherDate + "' as datetime),112),6)";
                DataSet dsglVoucherDetails = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsglVoucherDetails.Tables[0].Rows[0]["cnt"].ToString();
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
        //2.�O�_�̫�@���~�i�H�R��
        public object[] CheckDeleteglVoucherMaster2(object[] objParam)
        {
            var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
            string VoucherNo = Parameter_Input["Voucher_No"].ToString();

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
                string sql = " select COUNT(*) as cnt from SYSAUTONUM where AUTOID='VoucherNo' and FIXED+RIGHT(REPLICATE('0',3)+CAST(CURRNUM as NVARCHAR),3)='" + VoucherNo + "'";
                DataSet dsglVoucherDetails = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsglVoucherDetails.Tables[0].Rows[0]["cnt"].ToString();
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
        private void ucglVoucherMaster_BeforeDelete(object sender, UpdateComponentBeforeDeleteEventArgs e)
        {
            string VoucherNo = ucglVoucherMaster.GetFieldOldValue("VoucherNo").ToString();
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string CreateBy = SrvGL.GetUserName(userid.ToLower());

            string sql = " exec procInsertglVoucherMasterDel '" + VoucherNo + "','" + CreateBy + "'" + "\r\n";
            this.ExecuteCommand(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);

            DeleteglBalance();
        }
        private void ucglVoucherMaster_AfterDelete(object sender, UpdateComponentAfterDeleteEventArgs e)
        {            
            ////�O�_�̫�@��
            //string sql = " select COUNT(*) as cnt from SYSAUTONUM where AUTOID='VoucherNo' and FIXED+RIGHT(REPLICATE('0',3)+CAST(CURRNUM as NVARCHAR),3)='" + VoucherNo + "'";
            //DataSet dsglVoucherDetails = this.ExecuteSql(sql, ucglVoucherDetails.conn, ucglVoucherDetails.trans);
            //string cnt = dsglVoucherDetails.Tables[0].Rows[0]["cnt"].ToString();

            //if (cnt == "1")
            //{
            //    //�ק� SYSAUTONUM �s��
            //    string sql2 = " update SYSAUTONUM set CURRNUM=CURRNUM-1 from SYSAUTONUM where AUTOID='VoucherNo' and FIXED=LEFT('" + VoucherNo + "',9)" + "\r\n";

            //    this.ExecuteCommand(sql2, ucglVoucherDetails.conn, ucglVoucherDetails.trans);
            //}          
        }
        //======================================================�]�w�Ǧ^�ثe�����q�O�B�ǲ����O=========================================================================//
        //�]�w�Ǧ^�ثe�����q�O�B�ǲ����O
        public object[] getglVoucherSet(object[] objParam)
        {
            string UserID = (string)objParam[0];

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
                string sql = " select CompanyID,VoucherID from glVoucherSet where UserID='" +UserID+"'"+ "\r\n";           
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
            //return new object[] { 0, true };
        }

        //======================================================�K�X�N�� => �o�줺�e=========================================================================//
        public object[] GetDescribeText(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');

            int CompanyID = int.Parse(parm[0].ToString());
            string DetailAcno = (string)parm[1];
            string DescribeID = (string)parm[2];
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
                string sql = " select Describe from glDescribe where CompanyID=" + CompanyID + " and Acno='" + DetailAcno + "' and DescribeID='" + DescribeID + "'" +"\r\n";
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
            //return new object[] { 0, true };
        }

        //====================================================== ���ӥN�� => �o�줺�e (��ܦbGrid��)=========================================================================//
        public object[] GetAcnoNameText(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');

            int CompanyID = int.Parse(parm[0].ToString());
            string Acno = (string)parm[1];
            string SubAcno = (string)parm[2];
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
                string sql = " select AcnoName from glAccountItem where CompanyID=" + CompanyID + " and Acno='" + Acno + "' and SubAcno='" + SubAcno + "'" + "\r\n";
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
            //return new object[] { 0, true };
        }


        
        

       

       

       
       

        

       



    }
}
