using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Runtime;
using System.Data;
using FLRuntime.Hosting;
using FLCore;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FLCore.Base;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.OleDb;
using Microsoft.Win32;
using System.Xml;
using System.Reflection;
using Srvtools;

namespace FLRuntime
{
    public class Global
    {
        private const string GET_USER = "SELECT * FROM USERS WHERE USERID='{0}'";
        private const string GET_MANAGER_ROLEID = "SELECT ORG_MAN FROM SYS_ORG WHERE ORG_NO IN (SELECT ORG_NO FROM SYS_ORGROLES WHERE ROLE_ID='{0}' AND ORG_KIND='{1}')";
        //"SELECT ROLE_ID FROM SYS_ORGROLES WHERE ORG_NO IN(SELECT ORG_MAN FROM SYS_ORG WHERE ORG_NO IN (SELECT ORG_NO FROM SYS_ORGROLES WHERE ROLE_ID='{0}'))";
        private const string GET_MANAGER_ROLEID2 = "SELECT ORG_MAN FROM SYS_ORG WHERE ORG_NO IN (SELECT UPPER_ORG FROM SYS_ORG WHERE ORG_MAN='{0}' AND ORG_KIND='{1}')";
        //private static string GET_MANAGER_LEVELNO = "SELECT LEVEL_NO FROM SYS_ORG WHERE ORG_NO IN (SELECT ORG_MAN FROM SYS_ORG WHERE ORG_NO IN (SELECT ORG_NO FROM SYS_ORGROLES WHERE ROLE_ID='{0}'))";
        private const string GET_LEVELNO = "SELECT lEVEL_NO FROM SYS_ORG WHERE ORG_MAN='{0}' AND ORG_KIND='{1}'";
        private const string GET_MANAGER_LEVELNO = "SELECT LEVEL_NO FROM SYS_ORG WHERE ORG_NO IN (SELECT ORG_NO FROM SYS_ORGROLES WHERE ROLE_ID='{0}' AND ORG_KIND='{1}')";
        private const string GET_MANAGER_LEVELNO2 = "SELECT LEVEL_NO FROM SYS_ORG WHERE ORG_NO IN (SELECT UPPER_ORG FROM SYS_ORG WHERE ORG_MAN='{0}' AND ORG_KIND='{1}')";
        private const string GET_MANAGER_LEVELNO3 = "SELECT LEVEL_NO FROM SYS_ORG WHERE ORG_MAN='{0}' AND ORG_KIND='{1}'";
        private const string GET_USER_IDS_BY_ROLEID = "SELECT USERID FROM USERGROUPS WHERE GROUPID='{0}'";
        private const string GET_ROLE_ID_BY_ERF_ROLE = "SELECT * FROM {0} WHERE {1}";
        private const string GET_ROLE_IDS_BY_USER_ID = "SELECT GROUPS.GROUPID FROM GROUPS,USERGROUPS WHERE GROUPS.GROUPID = USERGROUPS.GROUPID AND GROUPS.ISROLE = 'Y' AND USERGROUPS.USERID = '{0}'";
        private const string GET_PAR_AGENT = "SELECT PAR_AGENT FROM SYS_ROLES_AGENT WHERE (SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC = '{0}') AND AGENT = '{1}' AND START_DATE {3} START_TIME <= '{2}'  AND END_DATE {3} END_TIME >= '{2}'";
        private const string GET_PLUS_ROLES = "SELECT PLUSROLES FROM SYS_TODOLIST WHERE LISTID='{0}' AND D_STEP_ID='{1}' AND STATUS NOT IN ('F','A','AA')";
        private const string GET_GROUP_NAME = "SELECT GROUPNAME FROM GROUPS WHERE GROUPID='{0}' AND ISROLE='Y'";
        private const string GET_SEND_TO = "SELECT SENDTO_KIND, SENDTO_ID, FLOWPATH FROM SYS_TODOLIST WHERE LISTID='{0}' AND STATUS NOT IN ('F','A','AA')";

        private const string GET_USER_NAME = "SELECT USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='{0}')";
        private const string GET_GROUP_COUNT = "SELECT COUNT(*) FROM GROUPS WHERE GROUPID='{0}' AND ISROLE='Y'";
        private const string MODIFY_FLINSTANCESTATE = "UPDATE SYS_FLINSTANCESTATE SET STATE = @State  WHERE FLINSTANCEID='{0}'";
        private const string MODIFY_TODO = "UPDATE SYS_TODOLIST SET FLOW_ID = '{1}' WHERE LISTID = '{0}';UPDATE SYS_TODOHIS SET FLOW_ID = '{1}' WHERE LISTID = '{0}'";
        private const string GET_FLDEFINITION = "SELECT * FROM SYS_FLDEFINITION WHERE FLTYPENAME='{0}' ORDER BY VERSION DESC";
        private const string INSERT_FLDEFINITION = "INSERT INTO SYS_FLDEFINITION(FLTYPEID,FLTYPENAME,FLDEFINITION,VERSION) VALUES('{0}','{1}',{2},'{3}')";
        private const string DELETE_FLDEFINITION = "DELETE FROM SYS_FLDEFINITION WHERE FLTYPEID NOT IN (SELECT FLOW_ID FROM SYS_TODOLIST)";
        private const string DELETE_FLINSTANCESTATE = "DELETE FROM SYS_FLINSTANCESTATE WHERE FLINSTANCEID NOT IN (SELECT LISTID FROM SYS_TODOLIST)";
        private const string GET_AGENTED = "SELECT ROLE_ID, AGENT, PAR_AGENT FROM SYS_ROLES_AGENT WHERE ROLE_ID='{0}' AND ROLE_ID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='{1}') AND START_DATE {4} START_TIME <= '{2}' AND END_DATE {4} END_TIME >= '{2}' AND (FLOW_DESC='{3}' OR FLOW_DESC IS NULL OR FLOW_DESC='*')";
        private const string GET_AGENTED_N = "SELECT ROLE_ID, AGENT FROM SYS_ROLES_AGENT WHERE ROLE_ID='{0}' AND ROLE_ID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='{1}') AND START_DATE {4} START_TIME <= '{2}' AND END_DATE {4} END_TIME >= '{2}' AND PAR_AGENT='N' AND (FLOW_DESC='{3}' OR FLOW_DESC IS NULL OR FLOW_DESC='*')";
        private const string GET_COMMENTS = "SELECT S_STEP_ID,USER_ID,USERNAME,STATUS,REMARK,ATTACHMENTS,UPDATE_DATE,UPDATE_TIME FROM SYS_TODOHIS Where (LISTID = '{0}') ORDER BY UPDATE_DATE,UPDATE_TIME";

        private const string GET_EXTAPPROVE = "SELECT * FROM SYS_EXTAPPROVE WHERE APPROVEID = '{0}' AND GROUPID = '{1}'";
        private const string GET_EXTAPPROVE2 = "SELECT * FROM SYS_EXTAPPROVE WHERE APPROVEID = '{0}'";

        private const string PARM_ROLE_ID = "GROUPID";
        private const string PARM_UPPER_ORG = "LEVEL_NO";
        private const string PARM_USERNAME = "USERNAME";
        private const string PARM_EMAIL = "EMAIL";
        private const string PARM_MANAGER_ROLEID = "ORG_MAN";
        private const string PARM_LEVEL = "LEVEL_NO";
        private const string PARM_USERID = "USERID";
        private const string PARM_PAR_AGENT = "PAR_AGENT";
        private const string PARM_PLUS_ROLES = "PLUSROLES";
        private const string PARM_GROUP_NAME = "GROUPNAME";
        private const string PARM_SENDTO_ID = "SENDTO_ID";
        private const string PARM_SENDTO_KIND = "SENDTO_KIND";
        private const string PARM_FLOWPATH = "FLOWPATH";
        private const string PARM_AGENT_ROLE_ID = "ROLE_ID";
        private const string PARM_AGENT_AGENT = "AGENT";

        private const string PARM_EXT_ROLE_ID = "ROLEID";
        private const string PARM_MINIMUM = "MINIMUM";
        private const string PARM_MAXIMUM = "MAXIMUM";

        // 创建一个带序列化的FLRuntime
        public static FLRuntime FLRuntime = new FLRuntime(new FLPersistenceService());

        public static WorkflowRuntime WFRuntime = new WorkflowRuntime();

        /// <summary>
        /// 取得是否被代理
        /// </summary>
        /// <param name="roleId">�角色Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="flowDescription">流程表述</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static bool GetIsAgented(string roleId, string userId, string flowDescription, object[] clientInfo)
        {
            bool b = false;
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string connectMark = "";

            String dbAlias = ((object[])clientInfo[0])[2].ToString();
            String dbType = String.Empty;
            //object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            //if (xx[0].ToString() == "0")
            //    dbAlias = xx[1].ToString();
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            dbAlias = GetSplitSysDBSD(dbAlias, developerID);
            IDbConnection conn1 = AllocateConnection(dbAlias, true, clientInfo);
            if (conn1.GetType().Name == "SqlConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OleDbConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OracleConnection")
                connectMark = "||"; 
            else if (conn1.GetType().Name == "OdbcConnection")
                connectMark = "||"; 
            else if (conn1.GetType().Name == "MySqlConnection")
                connectMark = "||"; 
            else if (conn1.GetType().Name == "Ifxonnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "AseConnection")
                connectMark = "||";
            //object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { dbAlias });

            //if (myRet != null && myRet[0].ToString() == "0")
            //{
            //    switch (myRet[1].ToString())
            //    {
            //        case "1":
            //        case "2":
            //            connectMark = "+"; break;
            //        case "3":
            //        case "4":
            //        case "5":
            //        case "6":
            //            connectMark = "||"; break;
            //    }
            //}
            string sql = string.Format(GET_AGENTED_N, roleId, userId, date, flowDescription, connectMark);

            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    string agentRoleId = ds.Tables[0].Rows[0][PARM_AGENT_ROLE_ID].ToString();
                    if (!string.IsNullOrEmpty(agentRoleId))
                    {
                        b = true;
                    }
                }
            }

            return b;
        }

        public static string GetAgent(string roleId, string userId, string flowDescription, object[] clientInfo)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string connectMark = "";

            String dbAlias = ((object[])clientInfo[0])[2].ToString();
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            dbAlias = GetSplitSysDBSD(dbAlias, developerID);
            IDbConnection conn1 = AllocateConnection(dbAlias, true, clientInfo);
            if (conn1.GetType().Name == "SqlConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OleDbConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OracleConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "OdbcConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "MySqlConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "Ifxonnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "AseConnection")
                connectMark = "||";
            //String dbType = String.Empty;
            //object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            //if (xx[0].ToString() == "0")
            //    dbAlias = xx[1].ToString();
            //object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { dbAlias });

            //if (myRet != null && myRet[0].ToString() == "0")
            //{
            //    switch (myRet[1].ToString())
            //    {
            //        case "1":
            //        case "2":
            //            connectMark = "+"; break;
            //        case "3":
            //        case "4":
            //        case "5":
            //        case "6":
            //            connectMark = "||"; break;
            //    }
            //}
            string sql = string.Format(GET_AGENTED, roleId, userId, date, flowDescription, connectMark);

            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    string agent = ds.Tables[0].Rows[0][PARM_AGENT_AGENT].ToString();
                    if (!string.IsNullOrEmpty(agent))
                    {
                        return agent;
                    }
                }
            }

            return null;
        }

        public static List<string> GetAgentUsers(string roleId, List<string> users, string flowDescription, object[] clientInfo)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string connectMark = "";

            String dbAlias = ((object[])clientInfo[0])[2].ToString();
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            dbAlias = GetSplitSysDBSD(dbAlias, developerID);
            IDbConnection conn1 = AllocateConnection(dbAlias, true, clientInfo);
            if (conn1.GetType().Name == "SqlConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OleDbConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OracleConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "OdbcConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "MySqlConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "Ifxonnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "AseConnection")
                connectMark = "||";

            string sql = string.Format(GET_AGENTED, roleId, users[0], date, flowDescription, connectMark);

            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            var list = new List<string>();
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string agent = ds.Tables[0].Rows[i][PARM_AGENT_AGENT].ToString();

                        if (ds.Tables[0].Rows[i][PARM_PAR_AGENT].ToString().ToUpper() == "Y")
                        {
                            list.AddRange(users);
                        }
                        if (!string.IsNullOrEmpty(agent))
                        {
                            list.Add(agent);
                        }
                    }
                }
                else
                {
                    list.AddRange(users);
                }
            }
            var agentUsers = new List<string>();
            foreach (string u in list)
            {
                if (agentUsers.Contains(u))
                    continue;

                agentUsers.Add(u);
            }
            return agentUsers;
        }

        /// <summary>
        /// 取得用户名
        /// </summary>
        /// <param name="userId">�用户Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetUserName(string userId, object[] clientInfo)
        {
            return GetUserName(userId, clientInfo, null);
        }


        public static string GetUserName(string userId, object[] clientInfo, string role)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_USER, userId);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string userName = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    userName = ds.Tables[0].Rows[0][PARM_USERNAME].ToString();
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            if (!string.IsNullOrEmpty(role))
            {
                var roles = GetRoleIdsByUserId(userId, clientInfo);
                if (roles.IndexOf(role) < 0)
                {
                    userName += "(*)";
                }
            }
            return userName;
        }



        /// <summary>
        /// 取得签核意见
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static DataSet GetComments(string flInstanceId, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_COMMENTS, flInstanceId);

            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string userName = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得用户Email
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetUserEmail(string userId, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_USER, userId);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string email = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    email = ds.Tables[0].Rows[0][PARM_EMAIL].ToString();
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return email;
        }

        /// <summary>
        /// 取得管理者
        /// </summary>
        /// <param name="roleId">�角色Id</param>
        /// <param name="orgKind">OrgKind</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetManagerRoleId(string roleId, string orgKind, object[] clientInfo)
        {
            if (roleId == null || roleId == string.Empty)
            {
                return string.Empty;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(GET_MANAGER_ROLEID, roleId, orgKind);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string managerRoleId = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    managerRoleId = ds.Tables[0].Rows[0][PARM_MANAGER_ROLEID].ToString();
                }

                if (managerRoleId.Length == 0)  // add by andy for upper manager
                {
                    sql = string.Format(GET_MANAGER_ROLEID2, roleId, orgKind);
                    //clientInfo[3] = sql;
                    //objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
                    objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

                    if (objs[0].ToString() == "0")
                    {
                        ds = (DataSet)objs[1];
                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            managerRoleId = ds.Tables[0].Rows[0][PARM_MANAGER_ROLEID].ToString();
                        }
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return managerRoleId;
        }

        /// <summary>
        /// 取得职级
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="orgKind">OrgKind</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetLevelNo(string roleId, string orgKind, object[] clientInfo)
        {
            if (roleId == null || roleId == string.Empty)
            {
                return string.Empty;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(GET_LEVELNO, roleId, orgKind);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string levelNo = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    levelNo = ds.Tables[0].Rows[0][PARM_UPPER_ORG] == null || ds.Tables[0].Rows[0][PARM_UPPER_ORG] == DBNull.Value
                        ? string.Empty : ds.Tables[0].Rows[0][PARM_UPPER_ORG].ToString();
                }
                else
                {
                    levelNo = "900cae2f-6266-4b7f-ba8c-483a94e01e93";
                }
            }
            else
            {
                levelNo = "900cae2f-6266-4b7f-ba8c-483a94e01e93";
            }

            return levelNo;
        }

        /// <summary>
        /// 取得管理者职级
        /// </summary>
        /// <param name="roleId">�角色Id</param>
        /// <param name="orgKind">OrgKind</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetManagerLevelNo(string roleId, string orgKind, object[] clientInfo)
        {
            if (roleId == null || roleId == string.Empty)
            {
                return string.Empty;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(GET_MANAGER_LEVELNO, roleId, orgKind);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string managerLevelNo = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    managerLevelNo = ds.Tables[0].Rows[0][PARM_LEVEL].ToString();
                }

                if (managerLevelNo.Length == 0)  // add by andy for upper manager
                {
                    sql = string.Format(GET_MANAGER_LEVELNO2, roleId, orgKind);
                    //clientInfo[3] = sql;
                    //objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
                    objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

                    if (objs[0].ToString() == "0")
                    {
                        ds = (DataSet)objs[1];
                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            managerLevelNo = ds.Tables[0].Rows[0][PARM_LEVEL].ToString();
                        }
                        else  // add by andy for top manager
                        {
                            sql = string.Format(GET_MANAGER_LEVELNO3, roleId, orgKind);
                            //clientInfo[3] = sql;
                            //objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
                            objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

                            if (objs[0].ToString() == "0")
                            {
                                ds = (DataSet)objs[1];
                                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                                {
                                    managerLevelNo = ds.Tables[0].Rows[0][PARM_LEVEL].ToString();
                                }
                            }

                        }
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return managerLevelNo;
        }

        //public static string GetLevelNo(string roleId, string orgKind, object[] clientInfo)
        //{
        //    if (roleId == null || roleId == string.Empty)
        //    {
        //        return string.Empty;
        //    }

        //    EEPRemoteModule remoteModule = new EEPRemoteModule();
        //    var sql = string.Format(GET_MANAGER_LEVELNO3, roleId, orgKind);
        //    //clientInfo[3] = sql;
        //    //objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
        //    var objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
        //    var ds = (DataSet)objs[1];
        //    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
        //    {
        //        return ds.Tables[0].Rows[0][PARM_LEVEL].ToString();
        //    }
        //    return string.Empty;
        //}

        /// <summary>
        /// 取得群组名
        /// </summary>
        /// <param name="roleId">�角色Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetGroupName(string roleId, object[] clientInfo)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return string.Empty;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_GROUP_NAME, roleId);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string groupName = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    groupName = ds.Tables[0].Rows[0][PARM_GROUP_NAME].ToString();
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            sql = string.Format(GET_USER_NAME, roleId);
            objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            string userName = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    userName = ds.Tables[0].Rows[0][PARM_USERNAME].ToString();
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            if (string.IsNullOrEmpty(userName))
            {
                return groupName;
            }
            else
            {
                return groupName + "/" + userName;
            }
        }

        /// <summary>
        /// 取得群组数
        /// </summary>
        /// <param name="roleId">�角色Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static int GetGroupCount(string roleId, object[] clientInfo)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return 0;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_GROUP_COUNT, roleId);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过角色取得用户
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static List<string> GetUsersIdsByRoleId(string roleId, object[] clientInfo)
        {
            List<string> userIds = new List<string>();
            if (roleId == null || roleId == string.Empty)
            {
                return userIds;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(GET_USER_IDS_BY_ROLEID, roleId);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        userIds.Add(row[PARM_USERID].ToString());
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return userIds;
        }

        /// <summary>
        /// 通过用户取得角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static List<string> GetRoleIdsByUserId(string userId, object[] clientInfo)
        {
            List<string> roleIds = new List<string>();
            if (userId == null || userId == string.Empty)
            {
                return roleIds;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(GET_ROLE_IDS_BY_USER_ID, userId);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        roleIds.Add(row[PARM_ROLE_ID].ToString());
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return roleIds;
        }


        public static string GetRoleIdByRefRole(FLInstance flInstance, string sendToField, string tableName, string wherePart, object[] clientInfo)
        {
            return GetRoleIdByRefRole(flInstance, sendToField, tableName, wherePart, clientInfo, false);
        }

        /// <summary>
        /// 通过管理角色取得角色
        /// </summary>
        /// <param name="sendToField">管理角色字段</param>
        /// <param name="tableName">表名</param>
        /// <param name="wherePart">筛选条件/param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetRoleIdByRefRole(FLInstance flInstance, string sendToField, string tableName, string wherePart, object[] clientInfo, bool isUser)
        {
            if (sendToField == null || sendToField == string.Empty)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdIsNull");
                throw new FLException(2, message);
            }

            var eepAlias = ((IFLRootActivity)flInstance.RootFLActivity).EEPAlias;
            var alias = string.Empty;
            if (!string.IsNullOrEmpty(eepAlias))
            {
                alias = (string)((object[])clientInfo[0])[2];
                ((object[])clientInfo[0])[2] = eepAlias;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string where = string.Empty;
            string s = wherePart.ToString();
            string[] ss = s.Split(';');
            foreach (string o in ss)
            {
                where += o.Replace("''", "'");
                where += " and ";
            }
            wherePart = where + " 1=1 ";

            //wherePart = wherePart.Replace("''", "'");

            string sql = string.Format(GET_ROLE_ID_BY_ERF_ROLE, tableName, wherePart);
            clientInfo[3] = sql;//modify by ccm hosttable sql use login db
            object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            //object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            string refRoleId = string.Empty;
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    refRoleId = ds.Tables[0].Rows[0][sendToField].ToString();
                    if (string.IsNullOrEmpty(refRoleId.Trim()))
                    {
                        String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdNotExist");
                        throw new FLException(2, message);
                    }

                    if (!isUser)
                    {
                        int i = GetGroupCount(refRoleId, clientInfo);
                        if (i == 0)
                        {
                            String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdNotExist");
                            throw new FLException(2, message);
                        }
                    }
                }
                else
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdIsNull");
                    throw new FLException(2, message);
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            if (!string.IsNullOrEmpty(alias))
            {
                ((object[])clientInfo[0])[2] = alias;
            }

            return refRoleId;
        }


        public static string GetFormPresentCT(FLInstance flInstance, string keys, string values, string presentFields, object[] clientInfo)
        {
            return GetFormPresentCT(flInstance, keys, values, presentFields, clientInfo, false);
        }

        /// <summary>
        /// �取得PresentCT
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="keys">筛选条件</param>
        /// <param name="values">�筛选值</param>
        /// <param name="presentFields">PresentCT�字段</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetFormPresentCT(FLInstance flInstance, string keys, string values, string presentFields, object[] clientInfo, bool sqluse)
        {
            if (!string.IsNullOrEmpty(presentFields))
            {

                DataSet dataSet = HostTable.GetHostDataSet(flInstance, new object[] { keys, values }, clientInfo);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    DataRow row = dataSet.Tables[0].Rows[0];

                    string[] fields = presentFields.Split(",".ToCharArray());
                    foreach (string field in fields)
                    {
                        if (string.IsNullOrEmpty(field))
                            continue;

                        if (!string.IsNullOrEmpty(keys) && row.Table.Columns.Contains(field))
                        {
                            keys += string.Format(";{0}", field);
                            string value = string.Empty;

                            if (row.Table.Columns[field].DataType == typeof(DateTime) && row[field] != DBNull.Value)
                            {
                                value = ((DateTime)row[field]).ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                value = row[field].ToString();
                                if (sqluse)
                                {
                                    value = value.Replace("'", "''");
                                }
                            }

                            values += string.Format(";{0}={1}", field, value);
                        }
                    }
                }
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string presentCT = values;
            string sql = "select * from COLDEF where TABLE_NAME = '" + tableName + "'";

            var eepAlias = ((IFLRootActivity)flInstance.RootFLActivity).EEPAlias;
            var alias = string.Empty;
            if (!string.IsNullOrEmpty(eepAlias))
            {
                alias = (string)((object[])clientInfo[0])[2];
                ((object[])clientInfo[0])[2] = eepAlias;
            }

            clientInfo[3] = sql;
            object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdDDUse", "", false);

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    string[] dataKeys = keys.Split(';');
                    foreach (string key in dataKeys)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (row["FIELD_NAME"].ToString().ToLower() == key.ToLower())
                            {
                                //presentCT = presentCT.Replace(key, row["CAPTION"].ToString());
                                presentCT = presentCT.Replace(key + "=", row["CAPTION"].ToString() + "=");
                                break;
                            }
                        }
                    }
                    presentCT.Replace("'", "");
                }

            }
            if (!string.IsNullOrEmpty(alias))
            {
                ((object[])clientInfo[0])[2] = alias;
            }
            return presentCT;
        }

        /// <summary>
        /// 取得加签角色
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="step">当前Step</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetPlusRoles(string flInstanceId, string step, object[] clientInfo)
        {
            string plusRoles = string.Empty;
            if (flInstanceId == null || flInstanceId == string.Empty || step == null || step == string.Empty)
            {
                return plusRoles;
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string sql = string.Format(GET_PLUS_ROLES, flInstanceId, step);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    plusRoles = ds.Tables[0].Rows[0][PARM_PLUS_ROLES].ToString();
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return plusRoles;
        }

        /// <summary>
        /// 取得签核角色及当前流程的位置
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="step">当前Step</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        internal static string[] GetSendTo(string flInstanceId, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_SEND_TO, flInstanceId);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    string kind = ds.Tables[0].Rows[0][PARM_SENDTO_KIND].ToString();
                    string id = ds.Tables[0].Rows[0][PARM_SENDTO_ID].ToString();
                    string[] flowpath = ds.Tables[0].Rows[0][PARM_FLOWPATH].ToString().Split(';');

                    if (!string.IsNullOrEmpty(id))
                    {

                        return new string[] { id, kind, flowpath[0], flowpath[1] };
                        //if (kind == "1")
                        //{
                        //    //role
                        //    List<string> users = Global.GetUsersIdsByRoleId(id, clientInfo);
                        //    if (users.Count > 0)
                        //    {
                        //        return new string[] { users[0], id, flowpath[0], flowpath[1] };
                        //    }
                        //    else
                        //    {
                        //        throw new Exception("找不到用户");
                        //    }
                        //}
                        //else
                        //{
                        //    List<string> roles = Global.GetRoleIdsByUserId(id, clientInfo);
                        //    if (roles.Count > 0)
                        //    {
                        //        return new string[] { id, roles[0], flowpath[0], flowpath[1] };
                        //    }
                        //    else
                        //    {
                        //        throw new Exception("找不到角色");
                        //    }
                        //}
                    }
                    else
                    {
                        throw new Exception("ID为空");
                    }


                }
                else
                {
                    return null;
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            throw new Exception();
        }

        /// <summary>
        /// 取得代理者
        /// </summary>
        /// <param name="flowDesc">�流程描述</param>
        /// <param name="userId">用户Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static object GetPARAGENT(string flowDesc, string userId, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string now = DateTime.Now.ToString("yyyyMMddHHmmss");
            string connectMark = "";

            String dbAlias = ((object[])clientInfo[0])[2].ToString();
            String dbType = String.Empty;
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }
            dbAlias = GetSplitSysDBSD(dbAlias, developerID);
            IDbConnection conn1 = AllocateConnection(dbAlias, true, clientInfo);
            if (conn1.GetType().Name == "SqlConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OleDbConnection")
                connectMark = "+";
            else if (conn1.GetType().Name == "OracleConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "OdbcConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "MySqlConnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "Ifxonnection")
                connectMark = "||";
            else if (conn1.GetType().Name == "AseConnection")
                connectMark = "||";
            //object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            //if (xx[0].ToString() == "0")
            //    dbAlias = xx[1].ToString();
            //object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { dbAlias });

            //if (myRet != null && myRet[0].ToString() == "0")
            //{
            //    switch (myRet[1].ToString())
            //    {
            //        case "1":
            //        case "2":
            //            connectMark = "+"; break;
            //        case "3":
            //        case "4":
            //        case "5":
            //        case "6":
            //            connectMark = "||"; break;
            //    }
            //}
            string sql = string.Format(GET_PAR_AGENT, flowDesc, userId, now, connectMark);
            //clientInfo[3] = sql;
            //object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });

            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    object temp = ds.Tables[0].Rows[0][PARM_PAR_AGENT];
                    if (temp == DBNull.Value || temp == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (temp.ToString().ToUpper() == "Y")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }

            return null;
        }


        public static string GetFLTypeId(string flTypeName, string flDefinition, object[] clientInfo)
        {
            return GetFLTypeId(Guid.Empty, flTypeName, flDefinition, clientInfo);
        }

        /// <summary>
        /// 取得流程类型
        /// </summary>
        /// <param name="flTypeName">类型名称</param>
        /// <param name="flDefinition">流程描述</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetFLTypeId(Guid flDefinitionId, string flTypeName, string flDefinition, object[] clientInfo)
        {
            string flTypeId = string.Empty;
            string sql = string.Format(GET_FLDEFINITION, flTypeName);
            int version = -1;

            object o = clientInfo[0];
            object[] os = (object[])o;
            string dbAlias = os[2].ToString();

            DbConnectionType dbConnectionType = DbConnectionType.SqlClient;
            //string connString = GetConnectionString(dbAlias, out dbConnectionType, true);

            IDbConnection conn = AllocateConnection(dbAlias, true, clientInfo);
            //IDbConnection conn = AllocateConnection(dbConnectionType, connString);
            IDbCommand cmd = AllocateCommand(conn, sql);

            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }

            IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                object obj = reader[2];
                if (obj == null || obj == DBNull.Value)
                {
                    continue;
                }
                string definition = obj.ToString();
                if (definition == flDefinition)
                {
                    flTypeId = reader[0].ToString();
                    break;
                }

                version = (reader[3] == null || reader[3] == DBNull.Value) ? -1 : Convert.ToInt32(reader[3]);
            }
            if (dbConnectionType != DbConnectionType.Informix)
                cmd.Cancel();
            reader.Close();
            cmd.Connection.Close();

            if (flTypeId == string.Empty)
            {
                version++;
                flTypeId = flDefinitionId == Guid.Empty ? Guid.NewGuid().ToString() : flDefinitionId.ToString();

                String param = "@FLDEFINITION";
                if (dbConnectionType == DbConnectionType.SqlClient)
                    param = "@FLDEFINITION";
                else if (dbConnectionType == DbConnectionType.OleDb)
                    param = "?";
                else if (dbConnectionType == DbConnectionType.OracleClient)
                    param = ":FLDEFINITION";
                else if (dbConnectionType == DbConnectionType.Odbc)
                    param = "?";
                else if (dbConnectionType == DbConnectionType.MySQL)
                    param = "@FLDEFINITION";
                else if (dbConnectionType == DbConnectionType.Informix)
                    param = "?";

                IDataParameter iParameter = AllocateParameter(cmd, param);
                if (dbConnectionType == DbConnectionType.OleDb)
                {
                    iParameter.ParameterName = "@content";
                    iParameter.Value = flDefinition;
                    (iParameter as OleDbParameter).OleDbType = OleDbType.VarChar;
                    sql = INSERT_FLDEFINITION.Replace("'{3}'", "{3}");
                    sql = string.Format(sql, flTypeId, flTypeName, param, version);
                }
                else if (dbConnectionType == DbConnectionType.SqlClient)
                {
                    iParameter.Value = flDefinition;
                    sql = string.Format(INSERT_FLDEFINITION, flTypeId, flTypeName, param, version);
                }
                else
                {
                    iParameter.Value = System.Text.Encoding.Default.GetBytes(flDefinition);
                    sql = string.Format(INSERT_FLDEFINITION, flTypeId, flTypeName, param, version);
                }


                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }

                cmd.CommandText = DELETE_FLDEFINITION;
                cmd.ExecuteNonQuery();

               // cmd.CommandText = DELETE_FLINSTANCESTATE;
               // cmd.ExecuteNonQuery();

                cmd.CommandText = sql;
                cmd.Parameters.Add(iParameter);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    String str = ex.Message;
                }

                cmd.Connection.Close();
            }

            return flTypeId;
        }

        public static List<string> GetExtApproveRoles(string approveID, string groupID, object value, object[] clientInfo)
        {

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            List<string> roles = new List<string>();
            if (value.Equals(DBNull.Value))
            {
                return roles;
            }

            string sql = string.Format(GET_EXTAPPROVE, approveID, groupID);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string role = row[PARM_EXT_ROLE_ID].ToString();
                        if (role.Length > 0)
                        {
                            object min = row[PARM_MINIMUM];
                            object max = row[PARM_MAXIMUM];
                            Type type = value.GetType();

                            if (!min.Equals(DBNull.Value) && ((IComparable)value).CompareTo(Convert.ChangeType(min, type)) < 0)
                            {
                                continue;
                            }
                            if (!max.Equals(DBNull.Value) && ((IComparable)value).CompareTo(Convert.ChangeType(max, type)) >= 0)
                            {
                                continue;
                            }
                            roles.Add(role);
                        }
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            return roles;

        }


        public static List<string> GetExtApproveRoles(string approveID, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            List<string> roles = new List<string>();
            string sql = string.Format(GET_EXTAPPROVE2, approveID);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "0")
            {
                DataSet ds = (DataSet)objs[1];
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string role = row[PARM_EXT_ROLE_ID].ToString();
                        if (role.Length > 0)
                        {
                            roles.Add(role);
                        }
                    }
                }
            }
            else if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
            return roles;

        }

        /// <summary>
        /// 修改流程定义
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void ModifyFLInstanceDefinition(FLInstance flInstance, object[] clientInfo)
        {
            string flTypeName = flInstance.RootFLActivity.Name;
            string flDefinition = flInstance.GetFLDefinitionXml().InnerXml;
            string flTypeId = Global.GetFLTypeId(flTypeName, flDefinition, clientInfo);


            string sql = MODIFY_FLINSTANCESTATE + MODIFY_TODO;
            sql = string.Format(sql, flInstance.FLInstanceId, flTypeId);


            object o = clientInfo[0];
            object[] os = (object[])o;
            string dbAlias = os[2].ToString();

            DbConnectionType dbConnectionType = DbConnectionType.SqlClient;
            //string connString = GetConnectionString(dbAlias, out dbConnectionType, true);
            if (dbConnectionType == DbConnectionType.OleDb || dbConnectionType == DbConnectionType.Informix
                || dbConnectionType == DbConnectionType.Odbc)
            {
                sql = sql.Replace("@State", "?");
            }
            else if (dbConnectionType == DbConnectionType.OracleClient)
            {
                sql = sql.Replace("@State", ":State");
            }

            IDbConnection conn = AllocateConnection(dbAlias, true, clientInfo);
            //IDbConnection conn = AllocateConnection(dbConnectionType, connString);
            IDbCommand cmd = AllocateCommand(conn, sql);

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, flInstance);

            IDataParameter state = null;
            if (dbConnectionType == DbConnectionType.OleDb)
            {
                state = AllocateParameter(cmd, "?", DbType.Binary);
                (state as OleDbParameter).OleDbType = OleDbType.LongVarBinary;
                state.Value = stream.GetBuffer();
            }
            else if (dbConnectionType == DbConnectionType.OracleClient)
            {
                state = AllocateParameter(cmd, ":State", DbType.Binary);
                (state as OracleParameter).OracleType = OracleType.Blob;
                state.Value = stream.GetBuffer();
            }
            else if (dbConnectionType == DbConnectionType.Odbc)
            {
                state = AllocateParameter(cmd, "?", DbType.Binary);
                (state as OdbcParameter).OdbcType = OdbcType.Binary;
                state.Value = stream.GetBuffer();
            }
            else if (dbConnectionType == DbConnectionType.Informix)
            {
                state = AllocateParameter(cmd, "?", DbType.Binary);
                state.Value = stream.GetBuffer();
            }
            else
            {
                state = AllocateParameter(cmd, "@State", DbType.Binary);
                state.Value = stream.GetBuffer();
            }

            cmd.Parameters.Add(state);

            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }

            if (dbConnectionType == DbConnectionType.Informix || dbConnectionType == DbConnectionType.Odbc)
            {
                cmd.CommandText = String.Format(MODIFY_FLINSTANCESTATE, flInstance.FLInstanceId).Replace("@State", "?");
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                IDbCommand cmd1 = AllocateCommand(conn, String.Format(MODIFY_TODO, flInstance.FLInstanceId, flTypeId));
                if (cmd1.Connection.State == ConnectionState.Closed)
                {
                    cmd1.Connection.Open();
                }
                cmd1.ExecuteNonQuery();
                cmd1.Connection.Close();
            }
            else
            {
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static IDbCommand AllocateCommand(IDbConnection connection, string sql)
        {
            if (connection is SqlConnection)
            {
                return new SqlCommand(sql, (SqlConnection)connection);
            }
            else if (connection is OdbcConnection)
            {
                return new OdbcCommand(sql, (OdbcConnection)connection);
            }
            else if (connection is OracleConnection)
            {
                return new OracleCommand(sql, (OracleConnection)connection);
            }

            else if (connection is OleDbConnection)
            {
                return new OleDbCommand(sql, (OleDbConnection)connection);
            }
            else if (connection.GetType().Name == "MySqlConnection")
            {
                String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);
                Assembly assembly = Assembly.LoadFrom(s);
                IDbCommand cmd = (IDbCommand)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlCommand");
                cmd.Connection = connection;
                cmd.CommandText = sql;
                return cmd;
            }
            else if (connection.GetType().Name == "IfxConnection")
            {
                String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);
                Assembly assembly = Assembly.LoadFrom(s);
                IDbCommand cmd = (IDbCommand)assembly.CreateInstance("IBM.Data.Informix.IfxCommand");
                cmd.Connection = connection;
                cmd.CommandText = sql;
                return cmd;
            }
            else return null;
        }

        private static String GetSplitSysDBSD(String DBName, String DeveloperID)
        {
            string dbname = "";
            if (DbConnectionSet.GetDbConn(DBName, DeveloperID) != null)
                if (DbConnectionSet.GetDbConn(DBName, DeveloperID).SplitSystemTable)
                    dbname = DbConnectionSet.GetSystemDatabase(DeveloperID);// GetSplitSysDB(DBName);
                else
                    dbname = DBName;
            else
                dbname = GetSplitSysDB(DBName);

            return dbname;
        }

        //20100526
        private static IDbConnection AllocateConnection(String DbAlias, Boolean SysDB, object[] clientInfo)
        {
            String developerID = "";
            if (clientInfo != null && clientInfo.Length > 0)
            {
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developerID = (string)info[17];
                }
            }

            string dbname = SysDB ? GetSplitSysDBSD(DbAlias, developerID) : DbAlias;
            //string dbname = SysDB ? GetSplitSysDB(DbAlias) : DbAlias;

            Srvtools.DbConnectionSet.DbConnection db = Srvtools.DbConnectionSet.GetDbConn(dbname, developerID);
            IDbConnection aConn = db.CreateConnection();
            if (aConn.State == ConnectionState.Closed)
                aConn.Open();
            return aConn;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="connectionType"></param>
        ///// <param name="connectionString"></param>
        ///// <returns></returns>
        //private static IDbConnection AllocateConnection(DbConnectionType connectionType, string connectionString)
        //{
        //    if (connectionType == DbConnectionType.SqlClient)
        //    {
        //        return new SqlConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.Odbc)
        //    {
        //        return new OdbcConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.OracleClient)
        //    {
        //        return new OracleConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.OleDb)
        //    {
        //        return new OleDbConnection(connectionString);
        //    }
        //    else if (connectionType == DbConnectionType.MySQL)
        //    {
        //        String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);
        //        Assembly assembly = Assembly.LoadFrom(s);
        //        IDbConnection conn = (IDbConnection)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlConnection");
        //        conn.ConnectionString = connectionString;
        //        return conn;
        //    }
        //    else if (connectionType == DbConnectionType.Informix)
        //    {
        //        String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);
        //        Assembly assembly = Assembly.LoadFrom(s);
        //        IDbConnection conn = (IDbConnection)assembly.CreateInstance("IBM.Data.Informix.IfxConnection");
        //        conn.ConnectionString = connectionString;
        //        return conn;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static IDataParameter AllocateParameter(IDbCommand command, string parameterName, DbType dbType)
        {
            if (command is SqlCommand)
            {
                return new SqlParameter(parameterName, dbType);
            }
            else if (command is OdbcCommand)
            {
                return new OdbcParameter(parameterName, dbType);
            }
            else if (command is OracleCommand)
            {
                return new OracleParameter(parameterName, dbType);
            }
            else if (command is OleDbCommand)
            {
                return new OleDbParameter(parameterName, dbType);
            }
            else if (command.GetType().Name == "MySqlCommand")
            {
                String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDataParameter parameter = (IDataParameter)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlParameter");
                parameter.ParameterName = parameterName;
                parameter.DbType = dbType;

                return parameter;
            }
            else if (command.GetType().Name == "IfxCommand")
            {
                String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDataParameter parameter = (IDataParameter)assembly.CreateInstance("IBM.Data.Informix.IfxParameter");
                parameter.ParameterName = parameterName;
                parameter.DbType = dbType;

                return parameter;
            }
            else return null;
        }

        private static IDataParameter AllocateParameter(IDbCommand command, String parameterName)
        {
            IDataParameter aIDataParameter = null;
            if (command is SqlCommand)
            {
                aIDataParameter = new SqlParameter(parameterName, SqlDbType.Text);
            }
            else if (command is OdbcCommand)
            {
                aIDataParameter = new OdbcParameter(parameterName, OdbcType.Text);
            }
            else if (command is OracleCommand)
            {
                aIDataParameter = new OracleParameter(parameterName, OracleType.Blob);
            }
            else if (command is OleDbCommand)
            {
                aIDataParameter = new OleDbParameter(parameterName, OleDbType.Binary);
            }
            else if (command.GetType().Name == "MySqlCommand")
            {
                String s = string.Format("{0}\\MySql.Data.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDataParameter parameter = (IDataParameter)assembly.CreateInstance("MySql.Data.MySqlClient.MySqlParameter");
                parameter.ParameterName = parameterName;
                parameter.DbType = DbType.String;

                return parameter;
            }
            else if (command.GetType().Name == "IfxCommand")
            {
                String s = string.Format("{0}\\IBM.Data.Informix.dll", EEPRegistry.Server);

                Assembly assembly = Assembly.LoadFrom(s);
                IDataParameter parameter = (IDataParameter)assembly.CreateInstance("IBM.Data.Informix.IfxParameter");
                parameter.ParameterName = parameterName;
                parameter.DbType = DbType.String;

                return parameter;
            }
            return aIDataParameter;
        }

        //private static string _serverPath;
        //private static string GetServerPath()
        //{
        //    if (_serverPath == null || _serverPath.Length == 0)
        //    {
        //        RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\infolight\\eep.net");
        //        String s = (String)rk.GetValue("Server Path");
        //        rk.Close();

        //        if (s[s.Length - 1] != '\\') s = s + "\\";

        //        _serverPath = s;
        //    }
        //    return _serverPath;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        private static string GetSplitSysDB(string alias)
        {
            String s = SystemFile.DBFile;

            if (File.Exists(s))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(s);
                XmlNode node = xml.SelectSingleNode(string.Format("InfolightDB/DataBase/{0}", alias));
                if (node != null)
                {
                    if (node.Attributes["Master"] != null && node.Attributes["Master"].Value.Trim() == "1")
                    {
                        XmlNode nodesys = xml.SelectSingleNode("InfolightDB/SystemDB");
                        if (nodesys != null)
                        {
                            string sysdb = nodesys.InnerText.Trim();
                            XmlNode nodecheck = xml.SelectSingleNode(string.Format("InfolightDB/DataBase/{0}", sysdb));
                            if (nodecheck != null)
                            {
                                return sysdb;
                            }
                            else
                            {
                                throw new Exception("SystemDB does not exsit in db list");
                            }
                        }
                        else
                        {
                            throw new Exception("SystemDB is Empty");
                        }
                    }
                    else
                    {
                        return alias;
                    }
                }
                else
                {
                    throw new Exception(string.Format("EEPAlias:{0} does not exsit", alias));
                }
            }
            else
            {
                throw new Exception(string.Format("{0} does not exsit", s));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="dbConnectionType"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetConnectionString(string alias, out DbConnectionType dbConnectionType, bool b)
        {
            alias = b ? GetSplitSysDB(alias) : alias;

            String xmlName = SystemFile.DBFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlName);

            XmlNode node = xmlDoc.FirstChild.FirstChild.SelectSingleNode(alias);

            string DbString = "";
            string Pwd = "";
            if (node != null)
            {
                DbString = node.Attributes["String"].Value.Trim();
                Pwd = GetPwdString(node.Attributes["Password"].Value.Trim());
            }
            if (DbString.Length > 0 && Pwd.Length > 0 && Pwd != string.Empty)
            {
                if (DbString[DbString.Length - 1] != ';')
                    DbString = DbString + ";Password=" + Pwd;
                else
                    DbString = DbString + "Password=" + Pwd;
            }

            string value = "1";
            if (node != null)
            {
                value = node.Attributes["Type"].Value;
                if (value == "1")
                {
                    dbConnectionType = DbConnectionType.SqlClient;
                }
                else if (value == "2")
                {
                    dbConnectionType = DbConnectionType.OleDb;
                }
                else if (value == "3")
                {
                    dbConnectionType = DbConnectionType.OracleClient;
                }
                else if (value == "4")
                {
                    dbConnectionType = DbConnectionType.Odbc;
                }
                else if (value == "5")
                {
                    dbConnectionType = DbConnectionType.MySQL;
                }
                else if (value == "6")
                {
                    dbConnectionType = DbConnectionType.Informix;
                }
                else
                {
                    dbConnectionType = DbConnectionType.SqlClient;
                }
            }
            else
            {
                dbConnectionType = DbConnectionType.SqlClient;
            }

            return DbString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string GetPwdString(string password)
        {
            string sRet = "";
            for (int i = 0; i < password.Length; i++)
            {
                sRet = sRet + (char)(((int)(password[password.Length - 1 - i])) ^ password.Length);
            }
            return sRet;
        }
    }
}
