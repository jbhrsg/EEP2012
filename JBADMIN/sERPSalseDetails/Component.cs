using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Srvtools;
using JBTool;
using System.Collections;

namespace sERPSalseDetails
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

        //���oERPSalesDetails=>SalesMasterNO�̤j�s��
        public string GetSalesMasterNO(string CustNO)
        {
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open) { connection.Open(); }
            IDbTransaction transaction = connection.BeginTransaction();
            string ItemSeq = "";
            //���oERPSalesDetails=>SalesMasterNO�̤j�s��
            //����~+���+�y����3�X10404001 Group by CustNO
            //�إ߸�Ʈw�s��           
            try
            {
                //���ItemSeq����~+���+�y����3�X10404001
                string sql = "select cast(DATEPART(YEAR,GETDATE())-1911 as nvarchar(3))+RIGHT('00'+cast(DATEPART(MONTH,GETDATE()) as nvarchar(2)),2) " + "\r\n";
                sql = sql + "+(select RIGHT('000'+RTRIM(LTRIM(STR(convert(nvarchar(3),(select IsNull(max(Right(ItemSeq,3)),'')+1" + "\r\n";
                sql = sql + "from ERPSalesDetails where CustNO=" + CustNO + ")),3))),3)) as ItemSeq " + "\r\n";
                DataSet dsItemSeq = this.ExecuteSql(sql, connection, transaction);
                ItemSeq = dsItemSeq.Tables[0].Rows[0]["ItemSeq"].ToString();
            }
            catch { transaction.Rollback(); }
            finally { ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection); }
            return ItemSeq;
        }
        //���o�ذe�g�������=>�����W�[�ضg��?��(�T�w§���@)§�����I�Z=>§�����q����=>�U�U§���@
        public DateTime GetSalesDay(DateTime dt)
        {
            if (dt.DayOfWeek >= DayOfWeek.Friday)//�j��§��������=>�U�U§���@
            {
                dt = dt.AddDays(5); 
            }
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(1);
            }   
            return dt;
        }
        //�s�W�P�f����
        private void ucERPSalseMaster_AfterInsert(object sender, UpdateComponentAfterInsertEventArgs e)
        {
            int SalesMasterNO = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("SalesMasterNO"));//�P�f�N��
            //string ItemSeq = "";//�P��Ǹ�ItemSeq=>����~+���+�y����3�X10404001(�Ȥ�group by)
            string CustNO = ucERPSalseMaster.GetFieldCurrentValue("CustNO").ToString();//�Ȥ�N��              
            string SalesEmployeeID = ucERPSalseMaster.GetFieldCurrentValue("SalesEmployeeID").ToString();//�~�ȭ��u�N��        

            string SalesTypeID = ucERPSalseMaster.GetFieldCurrentValue("SalesTypeID").ToString();//����O
            string DMTypeID = ucERPSalseMaster.GetFieldCurrentValue("DMTypeID").ToString();//���O
            string ViewAreaID = ucERPSalseMaster.GetFieldCurrentValue("ViewAreaID").ToString();//���O�ϰ�
            string GrantTypeID = " ";//�ش��覡=>��������OSalesTypeID1,�g��31=>key�k���P���� ��*,�g�� ��+

            Double CustAmt = Convert.ToDouble(ucERPSalseMaster.GetFieldCurrentValue("CustAmt"));//���`��
            Double OldCustAmt = CustAmt;//��l���`��
            int SalesQty = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("SalesQty"));//����
            int SalesQtyView = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("SalesQtyView"));//���Z             
            int Commission = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("Commission"));//����

            int PublishType = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PublishType"));//�Z�n�覡
            int PublishCount = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PublishCount"));//�Z
            int PresentCount = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PresentCount"));//��
            Double CustPrice = 0;
            if (SalesTypeID.Trim() == "6")//����O6 => ����
            {                                 
                CustPrice = Convert.ToDouble(ucERPSalseMaster.GetFieldCurrentValue("CustPrice"));//�ȳ��                
            }
            else
            {
                //�Ȥ���=>�Ȥ��`��/����/�Z��
                CustPrice = Math.Floor(CustAmt / SalesQty / PublishCount);//�L����˥h=>�ѤU���B�[�`�b�̫�@��
            }

            //�ضg���i�ର�Ŧr�ꪺ�w�]
            int PresentWNewsCount = 0;
            if (ucERPSalseMaster.GetFieldCurrentValue("PresentWNewsCount") != DBNull.Value)
            {
                PresentWNewsCount = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("PresentWNewsCount"));//�ضg��
            }
            int SubCount = PublishCount + PresentCount;//�Z+��
            int sumCount = PublishCount + PresentCount + PresentWNewsCount;//�Z+��+�ضg��=>�`����(�n�]������)
            string SalesDescr = ucERPSalseMaster.GetFieldCurrentValue("SalesDescr").ToString();//�P�f�Ƶ�
            string Remark1 = ucERPSalseMaster.GetFieldCurrentValue("Remark1").ToString();//����

            string SalesDate = "";//�P�f���+1 =>104/05/01 , InvoiceYM=>104/05
            DateTime ContinueDate = DateTime.Parse("1900/01/01");//�s�n���
            DateTime ComputerDate = DateTime.Parse("1900/01/01");//�p����
            DateTime StartDate = DateTime.Parse("1900/01/01");//��l���
            string sJumpDate = "";
            string[] JumpDate = { };
            if (PublishType == 1)//�s�n
            {
                ContinueDate = DateTime.Parse(ucERPSalseMaster.GetFieldCurrentValue("ContinueDate").ToString());//�s�Z���
                ComputerDate = ContinueDate;//�p����
                StartDate = ContinueDate;//��l���
            }
            else//���n
            {
                //sJumpDate = "2015/05/01,2015/05/03,2015/05/05,2015/05/10,2015/05/16,2015/05/22";
                //JumpDate = sJumpDate.Split(',');
                sJumpDate = ucERPSalseMaster.GetFieldCurrentValue("JumpDate").ToString();//���Z���
                JumpDate = sJumpDate.Split('\n');
                ContinueDate = DateTime.Parse(JumpDate[0]);
                ComputerDate = ContinueDate;//�p����
                StartDate = ContinueDate;//��l���
            }
            //�g�����
            string sWeekendDate = "";
            string[] WeekendDate = { };
            sWeekendDate = ucERPSalseMaster.GetFieldCurrentValue("WeekendDate").ToString();
            WeekendDate = sWeekendDate.Split('\n');
           
            //�w�]��=>�D�~
            int CustLines = 0;//�Ȧ�
            Double OfficePrice = 0;//�����
            int OfficeLines = 0;//����
            int OfficeAmt = 0;

            string NewsTypeID="";
            string NewsAreaID="";
            string NewsPublishID="";
            int Sections=0;
            if (SalesTypeID.Trim() == "6")//����O6 => ����
            {
                CustLines = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("CustLines"));//�Ȥ���              
                OfficePrice = Convert.ToDouble(ucERPSalseMaster.GetFieldCurrentValue("OfficePrice"));
                OfficeLines = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("OfficeLines"));
                OfficeAmt = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("OfficeAmt"));//ú����
                NewsTypeID = ucERPSalseMaster.GetFieldCurrentValue("NewsTypeID").ToString();
                NewsAreaID = ucERPSalseMaster.GetFieldCurrentValue("NewsAreaID").ToString();
                NewsPublishID = ucERPSalseMaster.GetFieldCurrentValue("NewsPublishID").ToString();
                Sections = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("Sections"));
            }
            string InvoiceYMPoint = (DateTime.Parse(ucERPSalseMaster.GetFieldCurrentValue("InvoiceYMPoint").ToString())).ToString("yyyy/MM/dd"); ;//���b����I
            string CreateBy = ucERPSalseMaster.GetFieldCurrentValue("CreateBy").ToString();
            string CreateDate = (DateTime.Parse(ucERPSalseMaster.GetFieldCurrentValue("CreateDate").ToString())).ToString("yyyy/MM/dd HH:mm");
            //�o������
            int IsAcceptePaper = 0;
            if (ucERPSalseMaster.GetFieldCurrentValue("IsAcceptePaper") != DBNull.Value)
            {
                IsAcceptePaper = Convert.ToInt32(ucERPSalseMaster.GetFieldCurrentValue("IsAcceptePaper"));
            }

            for (int i = 0; i < sumCount; i++)//�`����
            {
                //����(�D�~�ְT) �Z?�Ѻ��?��,���᳣����*,+(�����e�g��)�g�� �� +     
                //���� key?��? =>2+5=>��1,2�Ѻ�� ��=>�ť�
                //�����W�[�ضg��?��(�T�w§���@)§�����I�Z=>§�����q����=>�U�U§���@               
                    if (i < SubCount)//�Z+��
                    {                        
                        if (PublishType == 1)//�s�Z
                        {
                            ComputerDate = StartDate.AddDays(i);//�P�f���+1 =>104/05/01 , InvoiceYM=>104/05
                        }
                        else//���Z
                        {
                            ComputerDate = DateTime.Parse(JumpDate[i]);
                        }
                        //=>��������OSalesTypeID1,�g��31=>�t�O�u�b�ش���key�k���P���� ��*,�g�� ��+
                        if (i >= PublishCount)//����>=�Z��(���e���ش����Ŧr��)
                        {
                            if (SalesTypeID == "1")//�D�~�ְT
                            {
                                GrantTypeID = "*";
                            }
                            else if (SalesTypeID == "31")//�g��
                            {
                                GrantTypeID = "+";
                            }
                        }
                    }
                    else//�ضg��
                    {
                        if (SalesTypeID.Trim() == "6")//����O6 => ����
                        {
                            GrantTypeID = " ";
                        }
                        else
                        {
                            GrantTypeID = "+";//(�����e�g��)�g�� �� +
                        }
                        ComputerDate = DateTime.Parse(WeekendDate[i - SubCount]);
                        ////�Ĥ@���g��
                        //if (i == SubCount)
                        //{
                        //    ComputerDate = GetSalesDay(StartDate);//�s�n����}�l��
                        //}
                        //else ComputerDate = GetSalesDay(ComputerDate.AddDays(1));
                    }
                   
                    if (i >= PublishCount)//����>=�Z�� ���n����
                    {                        
                        CustPrice = 0;
                        //����
                        if (SalesTypeID.Trim() == "6")
                        {
                            Commission = 0;//����
                            OfficePrice = 0;//�����                            
                            OfficeAmt = 0;//ú����                            
                            CustAmt = 0;//�`�B
                        }
                    }
                    
                    CustAmt = CustPrice * SalesQty;//�`�B 
                    //����
                    if (SalesTypeID.Trim() == "6")
                    {
                        CustAmt = Math.Round(CustPrice * CustLines);//���`�B(�|�ˤ��J)
                    }
                    OldCustAmt = OldCustAmt - CustAmt;
                    if (SalesTypeID.Trim() != "6" && i == PublishCount - 1)//�n�����̫�@��
                    {
                        CustAmt = CustAmt +OldCustAmt;
                    }
                SalesDate = ComputerDate.ToString("yyyy/MM/dd");
                //�إ߸�Ʈw�s��
                IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
                //��s�u���A������open�ɡA�}�ҳs��
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                //�}�ltransaction
                IDbTransaction transaction = connection.BeginTransaction();

                
                //------------------------------------------------------------------------------------------------------------------

                try
                {
                    string SQL = "exec procInsertERPSalseDetails " +sumCount+","+ SalesMasterNO + ",'" + CustNO + "','" + SalesEmployeeID + "','" +
                        SalesTypeID + "','" + DMTypeID + "','" + ViewAreaID + "','" + SalesDate + "','" + GrantTypeID + "'," +
                        CustPrice + "," + SalesQty + "," + SalesQtyView + "," + CustAmt + "," + Commission + "," +
                        PublishCount + "," + PresentCount + "," + PresentWNewsCount + ",'" + SalesDescr + "','" + Remark1 + "'," +
                        CustLines + "," + OfficePrice + "," + OfficeLines + "," + OfficeAmt + ",'" + NewsTypeID + "','" +
                        NewsAreaID + "','" + NewsPublishID + "'," + Sections + ",'" + InvoiceYMPoint + "','" + CreateBy + "','" + CreateDate + "'," + IsAcceptePaper;
                    this.ExecuteSql(SQL, connection, transaction);
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

            }

        }
        // �ק�o������, �ק�[0800JOB].dbo.Publishing �X�Z���A ,�Z�n�_�W
        public object[] Update0800JOBPublishing(object[] objParam)
        {
            //encodeURIComponent
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
            string SalesTypeID = parm[1];//�s����
            string IsAcceptePaper = parm[2];

            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());

            string js = string.Empty;

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sq1 = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN0118;User ID=JBDBsql;Password=J3554436B";//Test

            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //�ק�o������
                string SQL2 = "exec procUpdateERPCustomersIsAcceptePaper '" + CustNO + "','" + SalesTypeID + "'," + IsAcceptePaper;
                cmd = new SqlCommand(SQL2, conn);
                cmd.ExecuteNonQuery();

                sq1 = "exec [192.168.10.70].[0800JOB].dbo.procUpdate0800JOBPublishing '" + CustNO+ "'";//
                //sq1 = "exec [192.168.10.70].[0800JOB0118].dbo.procUpdate0800JOBPublishing " + CustNO;//Test
                cmd = new SqlCommand(sq1, conn);
                cmd.ExecuteNonQuery();

            }
            catch
            {
            }
            finally
            {
                conn.Dispose();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }

        private void ucERPSalseDetails_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucERPSalseDetails.SetFieldValue("LastUpdateDate", DateTime.Now);//�����
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());
            ucERPSalseDetails.SetFieldValue("LastUpdateBy", LoginUser);//�����
        }

        private void ucERPSalseDetails_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {           
            ucERPSalseDetails.SetFieldValue("CreateDate", DateTime.Now);//�����
        }

        private void ucERPSalseMaster_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPSalseMaster.SetFieldValue("CreateDate", DateTime.Now);//�����
            if (ucERPSalseMaster.GetFieldCurrentValue("Is0800Id").ToString() == "False")
            {
                ucERPSalseMaster.SetFieldValue("0800Id", DBNull.Value);
            }
        }
        //�N��ƶ�����
        public object[] ShowToDoCount(object[] objParam)
        {
            string SalesEmployeeID = (string)objParam[0];
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
                string SQL = "exec procDisplaySalesToDoListCount '" + SalesEmployeeID + "'";
                DataSet ds= this.ExecuteSql(SQL, connection, transaction);
                string cnt = ds.Tables[0].Rows[0]["sCount"].ToString();                
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                js = cnt;
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
        //�s�W�P�f����(�ɵn�ˬd)
        public object[] CheckERPSalseDetailsLast(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];          
            string sJumpDate = parm[1].Replace('\n', ',');         

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
                string SQL = "exec procCheckERPSalseDetailsLast " + SalesMasterNO + ",'"  +sJumpDate + "'";
                DataSet dsCheck = this.ExecuteSql(SQL, connection, transaction);
                js = JsonConvert.SerializeObject(dsCheck.Tables[0], Formatting.Indented);
                this.ExecuteSql(SQL, connection, transaction);
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
        //�s�W�P�f����(�ɵn)
        public object[] InsertERPSalseDetailsLast(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string CustNO = parm[1];
            string DMTypeID = parm[2];
            string ViewAreaID = parm[3];
            string SalesQty = parm[4];
            string SalesQtyView = parm[5];
            string SalesDescr = parm[6];
            string Remark1 = parm[7];
            string YMPoint = parm[8];
            string sJumpDate = parm[9].Replace('\n', ',');//�Z�n���      
            string sJumpDate2 = parm[10].Replace('\n', ',');//�g�����                       
            string CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string LoginUser = SrvGL.GetUserName(userid.ToLower());

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
            {   //�ش�
                string SQL = "exec procInsertERPSalseDetailsLast '"+YMPoint+"','*'," + SalesMasterNO + ",'" + CustNO + "','" + DMTypeID + "','" +
                      ViewAreaID + "'," + SalesQty + "," + SalesQtyView + ",'" + sJumpDate + "','" + LoginUser + "','" + CreateDate + "','" + SalesDescr + "','" + Remark1 + "'";
                this.ExecuteSql(SQL, connection, transaction);
                //�g��
                if (sJumpDate2.Trim() != "")
                {
                    string SQL2 = "exec procInsertERPSalseDetailsLast '" + YMPoint + "','+'," + SalesMasterNO + ",'" + CustNO + "','" + DMTypeID + "','" +
                          ViewAreaID + "'," + SalesQty + "," + SalesQtyView + ",'" + sJumpDate2 + "','" + LoginUser + "','" + CreateDate + "','" + SalesDescr + "','" + Remark1 + "'";
                    this.ExecuteSql(SQL2, connection, transaction);
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
            return new object[] { 0, js };
        }

        //���o�t���ܼ�InvoiceYMPoint���b��
        public object[] GetInvoiceYMPoint(object[] objParam)
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
                string sql = " select isnull((select top 1 CategoryValue from SYS_Variable where Category='SalesDetailsInvoiceYMPoint' and CategoryValue>Convert(nvarchar(10),getdate(),111) order by CategoryValue),Convert(nvarchar(10),getdate(),111)) as CategoryValue";
                DataSet InvoiceYMPoint = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = InvoiceYMPoint.Tables[0].Rows[0]["CategoryValue"].ToString();
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
        //�P�f���ӿ�ܩʳ]���L��
        public object[] UpdateSalseDetailsIsActive(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string CustNO = parm[2];
            string depositSeq = parm[3];
            string sType = parm[4];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower()); 
            string js = string.Empty;

            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;
            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=sa;Password=J3554436B";

            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //IDbTransaction trans = conn.BeginTransaction();
                //���� JB-DB\SQL2008.NJB.JBADMIN.dbo.procPostToNjbDeposit �w�s�{��
                sql = "EXEC procUpdateERPSalseDetailsIsActive '" + SalesMasterNO + "','" + ItemSeq + "','" + sType + "','" + username + "'";
                //sql = "EXEC [60.250.52.107,3225].JBADMIN.dbo.procUpdateERPSalseDetailsIsActive '" + SalesMasterNO + "','" + ItemSeq + "','" + sType + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();              
            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Dispose();                
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }
       
        //�ˬd�O�_�����Ȥ�
        public object[] CheckNullCustNO(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];           
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
                string sql = " select Isnull((select CustShortName from View_ERPCustomers where CustNO='" + CustNO + "'),'') as CustShortName";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                js = sCustNO.Tables[0].Rows[0]["CustShortName"].ToString();
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
        //�P�f���ӿ�ܩʶפJ��F�t��
        public object[] ImportSalesDetails(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string SalesMasterNO = parm[0];
            string ItemSeq = parm[1];
            string userid = GetClientInfo(ClientInfoType.LoginUser).ToString();
            string username = SrvGL.GetUserName(userid.ToLower());            
            string js = string.Empty;

           //
            SqlCommand cmd;
            SqlConnection conn;
            string connetionString = null;
            string sql = null;

            connetionString = "Data Source=192.168.10.60;Initial Catalog=JBADMIN;User ID=JBDBsql;Password=J3554436B";//���n����
            //connetionString = "Data Source=211.78.84.42;Initial Catalog=JBADMIN;User ID=sa;Password=J3554436B";
            conn = new SqlConnection(connetionString);
            //IDbConnection conn = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                //IDbTransaction trans = conn.BeginTransaction();
                //���� JB-DB\SQL2008.NJB.JBADMIN.dbo.procPostToNjbDeposit �w�s�{��
                sql = "EXEC procImportSalesDetails '" + SalesMasterNO + "','" + ItemSeq + "','" + username + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //this.ExecuteSql(sql, conn,trans);
                //trans.Commit();
            }
            catch
            {
                //transaction.Rollback();
            }
            finally
            {
                conn.Dispose();  
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), conn);
            }
            return new object[] { 0, js };
        }
        //�P�f���ӥ����R��
        public object[] DeleteSalesDetails(object[] objParam)
        {
            string SalesMasterNO = objParam[0].ToString();
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
                string SQL = "exec procDeleteSalesDetails " + SalesMasterNO;
                this.ExecuteSql(SQL, connection, transaction);
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

        private void ucERPSalseDetails_AfterModify(object sender, UpdateComponentAfterModifyEventArgs e)
        {

        }
        //�o��Ȥ᪺�o������
        public object[] GetIsAcceptePaper(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CustNO = parm[0];
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
                string sql = " select IsAcceptePaper,IndustryType,ERPCustomerID from View_ERPCustomers where CustNO='" + CustNO + "' ";
                DataSet sCustNO = this.ExecuteSql(sql, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
                //js = sCustNO.Tables[0].Rows[0]["IsAcceptePaper"].ToString();
                js = JsonConvert.SerializeObject(sCustNO.Tables[0], Formatting.Indented);

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

        // ���o�K�Q��SalesTypeID=31������έp��T
        public object[] GeERPSalseDetailsInfo(object[] objParam)
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
                string SQL = " exec procReportERPSalseDetailsInfo";

                DataSet ds = this.ExecuteSql(SQL, connection, transaction);
                //// Indented�Y�� �N����ഫ��Json�榡
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
        //���o�s��,�Ȥ�N�� �a�X�X�Z�Ȥ�
        public object[] Get0800Id(object[] objParam)
        {
            try
            {
                var Parameter_Input = TheJsonResult.GetParameterObj(objParam);
                var Code = Parameter_Input["Co_de"].ToString().Trim();                
                
                string SQL = @"
select Id,Code
from  View_0800jobPublishing
where Code like '%"+@Code+"%'";

                ArrayList Parameter = new ArrayList();
                Parameter.Add(new SqlParameter("@Code", Code));

                var DataSet = SQL_Tools.GetDataSet(this, SQL, Parameter);

                var ComboboxList = DataSet.Tables[0].AsEnumerable().Select(m => new ComboboxField
                {
                    value = m.Field<Guid>("Id").ToString(),
                    text = m.Field<string>("Code") ?? ""
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



    }
}
