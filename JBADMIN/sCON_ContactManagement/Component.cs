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
using System.IO;
using JBTool;
using TheExcelFileImport;

namespace sCON_ContactManagement
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
        //�[�p���H��s��
        public object[] procAddContactToGroup(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CENTER_ID = parm[0].ToString();
            string ContactIDStr = parm[1].ToString();
            string UserID = parm[2].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procAddContactToGroup " + CENTER_ID + ",'" + ContactIDStr + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        //�����p���H�s��
        public object[] procRemoveContactFromGroup(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CENTER_ID = parm[0].ToString();
            string ContactIDStr = parm[1].ToString();
            string UserID = parm[2].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procRemoveContactFromGroup " + CENTER_ID + ",'" + ContactIDStr + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        //���X����
        public object[] procRemoveContactFromActivity(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CENTER_ID = parm[0].ToString();
            string ContactIDStr = parm[1].ToString();
            string UserID = parm[2].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procRemoveContactFromActivity " + CENTER_ID + ",'" + ContactIDStr + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        //�[�p���H�s��ñ
        public object[] procAddContactsLabel(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CENTER_ID = parm[0].ToString();
            string LABEL_ID = parm[1].ToString();
            string LABELVALUE = parm[2].ToString();
            string ContactIDStr = parm[3].ToString();
            string UserID = parm[4].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procAddContactsLabel " + CENTER_ID + "," + LABEL_ID + ",'" + LABELVALUE + "','" + ContactIDStr + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
                //string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                //Indented�Y�� �N����ഫ��Json�榡
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        public object[] ExcelFileImport(object[] objParam)
        {

            //�^��
            var theResult = new Dictionary<string, object>();
            theResult.Add("IsOK", false);
            theResult.Add("Msg", "���~�F��");

            try
            {
                //�Ѽ�
                var Parameter = (Dictionary<string, object>)HandlerHelper.DeserializeObject(objParam[0].ToString());

                //�ɮ�
                var aMemoryStream = (MemoryStream)HandlerHelper.DeserializeObject(objParam[1].ToString());

                //�פJ�@�~���G
                var aExcelFileImportResult = new ExcelFileImport
                {
                    FileStream = aMemoryStream,
                    HealderParameter = Parameter,
                    theDataModule = this,
                    CenterID = 2,
                    CreateMan = SrvUtils.GetValue("_username", this)[1].ToString()
                }.GetTheFileImportResult();

                theResult["IsOK"] = aExcelFileImportResult.IsOK;
                theResult["Msg"] = aExcelFileImportResult.ErrorMsg;
                if (aExcelFileImportResult.Result != null) theResult["File"] = (MemoryStream)aExcelFileImportResult.Result;
            }
            catch (TheUserDefinedException ex)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = ex.Message;
            }
            catch (Exception)
            {
                theResult["IsOK"] = false;
                theResult["Msg"] = "������~";
            }
            return new object[] { 0, HandlerHelper.SerializeObject(theResult) };
        }
        //�H�m�W,������X�ˬd�p���H�O�_�w�s�b
        public object[] CheckContactIsExist(object[] objParam)
        {
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string[] parm = objParam[0].ToString().Split(',');
                string CONTACT_NAME = parm[0].ToString();
                string CONTACT_CELLPHONE = parm[1].ToString();
                string sql = "SELECT COUNT(*) AS CNT FROM CON_CONTACT  WHERE CONTACT_NAME = '" + CONTACT_NAME + "' AND CONTACT_CELLPHONE ='" + CONTACT_CELLPHONE + "' AND CONTACT_ISPUBLIC=1";
                DataSet dsTemp = this.ExecuteSql(sql, connection, transaction);
                string cnt = dsTemp.Tables[0].Rows[0]["cnt"].ToString();
                js = JsonConvert.SerializeObject(cnt, Formatting.Indented);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, js };
        }
        //���o�M��������
        public object[] GetSkillHobbyData(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string type = parm[0];
            string listData = "";
            if (parm[1] != "")
            {
                for (var i = 1; i <= parm.Length - 1; i++)
                {
                    if (i == parm.Length - 1)
                        listData = listData + "'" + parm[i] + "'";
                    else
                        listData = listData + "'" + parm[i] + "', ";
                }
            }

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
                var user = GetClientInfo(ClientInfoType.LoginUser);
                var sql = "";

                switch (type)
                {
                    case "SKILL":
                        if (listData != "")
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_SKILL' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID NOT IN (" + listData + ")" + "\n\r";
                            sql = sql + " union " + "\n\r";
                            sql = sql + " select CODE_ID,NAME,SORT,'Y' AS IS_SELECTED from CON_SHARECODE" + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_SKILL' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID IN (" + listData + ")" + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        else
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_SKILL' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        break;
                    case "HOBBY":
                        if (listData != "")
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_HOBBY' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID NOT IN (" + listData + ")" + "\n\r";
                            sql = sql + " union " + "\n\r";
                            sql = sql + " select CODE_ID,NAME,SORT,'Y' AS IS_SELECTED from CON_SHARECODE" + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_HOBBY' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " AND CODE_ID IN (" + listData + ")" + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        else
                        {
                            sql = "select CODE_ID,NAME,SORT,'N' AS IS_SELECTED from CON_SHARECODE " + "\n\r";
                            sql = sql + " WHERE FIELDNAME = 'CONTACT_HOBBY' AND DISPLAY = 'Y' " + "\n\r";
                            sql = sql + " ORDER BY SORT" + "\n\r";
                        }
                        break;
                    default:

                        break;
                }

                DataSet dsSharecode = this.ExecuteSql(sql, connection, transaction);

                //Indented�Y�� �N����ഫ��Json�榡
                js = JsonConvert.SerializeObject(dsSharecode.Tables[0], Formatting.Indented);
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
        private void ucCON_CONTACTUPDATE_BeforeInsert(object sender, UpdateComponentBeforeInsertEventArgs e)
        {
            ucCON_CONTACTUPDATE.SetFieldValue("CREATE_DATE", DateTime.Now);
            ucCON_CONTACTUPDATE.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }

        private void ucCON_CONTACTUPDATE_BeforeModify(object sender, UpdateComponentBeforeModifyEventArgs e)
        {
            ucCON_CONTACTUPDATE.SetFieldValue("UPDATE_DATE", DateTime.Now);
        }
        public object[] DeleteContact(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string CONIDStr = parm[0].ToString();
            string USERID = parm[1].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "Exec procDeleteContactBatch '" + CONIDStr + "','" + USERID + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        public object[] UpdateContactLabelQty(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split(',');
            string CONTACT_ID = parm[0].ToString();
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
                string sql = "Exec UpdateContactLabelQty '" + CONTACT_ID + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        public object[] procConcordContact(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string ContactIDs = parm[0].ToString();
            string ContactName = parm[1].ToString();
            string ConIDStr = parm[2].ToString();
            string UserID = parm[3].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procConcordContact '" + ContactIDs + "','" + ContactName + "','" + ConIDStr + "','" + UserID + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
        //�N������p���H�[�J���ʤ�
        public object[] procAddContactToActivity(object[] objParam)
        {
            string[] parm = objParam[0].ToString().Split('*');
            string ACTIVITY_ID = parm[0].ToString();
            string CONIDSTR = parm[1].ToString();
            string CENTER_ID_FROM = parm[2].ToString();
            string USERID = parm[3].ToString();
            string js = string.Empty;
            IDbConnection connection = (IDbConnection)AllocateConnection(GetClientInfo(ClientInfoType.LoginDB).ToString());
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            IDbTransaction transaction = connection.BeginTransaction();
            try
            {
                string sql = "EXEC procAddContactToActivity '" + ACTIVITY_ID + "','" + CONIDSTR + "','" + CENTER_ID_FROM + "','" + USERID + "'";
                this.ExecuteSql(sql, connection, transaction);
            }
            catch
            {
                transaction.Rollback();
                return new object[] { 0, false };
            }
            finally
            {
                transaction.Commit();
                ReleaseConnection(GetClientInfo(ClientInfoType.LoginDB).ToString(), connection);
            }
            return new object[] { 0, true };
        }
    }
}
