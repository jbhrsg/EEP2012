using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Srvtools;
using System.Data.Sql;
using System.Data;

namespace sERPContractAlter
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
            return "COA" + Year;
        }

        public object[] UpdateERPContract(object[] objParam)
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
                    //string user_id = GetClientInfo(ClientInfoType.LoginUser).ToString();
                    //string user_name = SrvGL.GetUserName(user_id);
                    string ContractNO = dr["ContractNO"].ToString();
                    string BeginDate = string.Format("{0:yyyy/MM/dd}", dr["BeginDate"]);
                    string EndDate = string.Format("{0:yyyy/MM/dd}", dr["EndDate"]);
                    string PhysicalContractNO = dr["PhysicalContractNO"].ToString();
                    string RemindDays = dr["RemindDays"].ToString();
                    string GuarantyEndDate = string.Format("{0:yyyy/MM/dd}", dr["GuarantyEndDate"]);

                    string sql = "update ERPContract set BeginDate='" + BeginDate + "',EndDate='" + EndDate + "',PhysicalContractNO='" + PhysicalContractNO + "',RemindDays='" + RemindDays + "',GuarantyEndDate='" + GuarantyEndDate + "' where ContractNO='" + ContractNO + "'";
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

        private void ucERPContractAlter_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucERPContractAlter.SetFieldValue("ContractAlterNO", GetContractAlterNO());
        }
        public string GetContractAlterNO()
        {
                string NewContractAlterNO = "";
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
                    string sql = "select top 1 ContractAlterNO  FROM [JBADMIN].[dbo].[ERPContractAlter] order by ContractAlterNO desc";//���̤j�s��
                    string ContractAlterNO=this.ExecuteSql(sql, connection, transaction).Tables[0].Rows[0][0].ToString();
                    transaction.Commit();
                    
                    string year = ContractAlterNO.Substring(3, 4);
                    if (DateTime.Now.Year.ToString() == year)//�P�~�֥[
                    {
                        int number = int.Parse(ContractAlterNO.Substring(7, 5))+1;
                        NewContractAlterNO="COA"+year+number.ToString().PadLeft(5, '0');
                    }
                    else {
                        NewContractAlterNO = "COA" + DateTime.Now.Year.ToString() + "00001";
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
                return NewContractAlterNO;
        }
    }
}
