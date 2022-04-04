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
        //Flow�а����\=>�s�W��ƨ�JBHR_EEP
        //1.�s�W �а������ HRM_ATTEND_ABSENT_MINUS , �а���Ʃ����� HRM_ATTEND_ABSENT_MINUS_DETAIL , �а���R������ HRM_ATTEND_ABSENT_TRANS 
        //2.�ק�o������� HRM_ATTEND_ABSENT_PLUS => �а��R�P�ɼ� ABSENT_HOURS , �o���Ѿl�ɼ� REST_HOURS 
        public object procDeleteHRM_ATTEND_ABSENT_MINUS(object[] objParam)
        {
            object[] ret = new object[] { 0, 0 };
           
            DataRow drDara = (DataRow)objParam[0]; // ���o�y�{��ƪ���Ƥ��e
            int AbsentMinusID = Convert.ToInt32(drDara["AbsentMinusID"].ToString());
            var TotalHours = drDara["TotalHours"].ToString();//�`�ɼ�            

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
                //Delete HRM_ATTEND_ABSENT_MINUS , HRM_ATTEND_ABSENT_MINUS_DETAIL 
                //�q HRM_ATTEND_ABSENT_TRANS ���o ABSENT_PLUS_ID  
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
                transaction.Commit(); // �T�{���
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



    }
}
