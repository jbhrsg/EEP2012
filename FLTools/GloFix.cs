using System;
using System.Data;
using Srvtools;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;

namespace FLTools
{
    public class GloFix
    {
        public const string secGroups = "SELECT DISTINCT GROUPS.GROUPID FROM GROUPS INNER JOIN GROUPMENUS ON GROUPS.GROUPID=GROUPMENUS.GROUPID WHERE GROUPS.ISROLE='Y' AND GROUPS.GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID IN (SELECT USERID FROM USERMENUS WHERE MENUID='{0}')) AND GROUPMENUS.MENUID='{0}' AND GROUPMENUS.GROUPID IN (SELECT GROUPID FROM GROUPS WHERE ISROLE='Y')";
        public const string secUsers = "SELECT DISTINCT USERGROUPS.USERID FROM USERGROUPS INNER JOIN USERMENUS on USERGROUPS.USERID=USERMENUS.USERID WHERE USERGROUPS.GROUPID IN (SELECT GROUPID FROM GROUPMENUS WHERE MENUID='{0}' AND GROUPID IN (SELECT GROUPID FROM GROUPS WHERE ISROLE='Y')) AND USERMENUS.MENUID='{0}'";

        public static bool IsNumeric(Type dataType)
        {
            string type = dataType.ToString().ToLower();
            if (type == "system.uint" || type == "system.uint16" || type == "system.uint32" || type == "system.uint64"
              || type == "system.int" || type == "system.int16" || type == "system.int32" || type == "system.int64"
              || type == "system.single" || type == "system.double" || type == "system.decimal")
            {
                return true;
            }
            return false;
        }

        public static string ShowParallelMessage(string sendToIds)
        {
            List<string> sendToUsers = new List<string>();
            List<string> sendToRoles = new List<string>();
            string[] toIds = sendToIds.Split(';');
            for (int i = 0; i < toIds.Length; i++)
            {
                if (toIds[i].IndexOf(':') != -1)
                {
                    sendToUsers.Add(toIds[i].Substring(0, toIds[i].IndexOf(':')));
                }
                else
                {
                    sendToRoles.Add(toIds[i]);
                }
            }

            Hashtable dicGroups = new Hashtable();
            Hashtable dicUsers = new Hashtable();

            // 1.取出所有的RoleId和RoleName及Role以下的UserID填入sendToUsers
            string sqlRoles = "";
            foreach (string role in sendToRoles)
            {
                sqlRoles += "'" + role + "',";
            }
            if (sqlRoles != "")
            {
                sqlRoles = sqlRoles.Substring(0, sqlRoles.LastIndexOf(','));

                DataTable tGroups = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID in(" + sqlRoles + ") AND ISROLE='Y'" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tGroups = ((DataSet)ret1[1]).Tables[0];
                }
                else if (ret1 != null && (int)ret1[0] == 1)
                {
                    throw new Exception((string)ret1[1]);
                }
                foreach (DataRow row in tGroups.Rows)
                {
                    dicGroups.Add(row["GROUPID"].ToString(), row["GROUPNAME"].ToString());

                    DataTable tHoldUsers = null;

                    object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + row["GROUPID"].ToString() + "')" });
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    else if (ret2 != null && (int)ret2[0] == 1)
                    {
                        throw new Exception((string)ret2[1]);
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            sendToUsers.Add(holdRow["USERID"].ToString());
                        }
                    }
                }
            }
            // 2.取出所有的UserId和UserName
            string sqlUsers = "";
            foreach (string user in sendToUsers)
            {
                sqlUsers += "'" + user + "',";
            }
            if (sqlUsers != "")
            {
                sqlUsers = sqlUsers.Substring(0, sqlUsers.LastIndexOf(','));
                DataTable tUsers = null;

                object[] ret3 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID in(" + sqlUsers + ")" });
                if (ret3 != null && (int)ret3[0] == 0)
                {
                    tUsers = ((DataSet)ret3[1]).Tables[0];
                }
                else if (ret3 != null && (int)ret3[0] == 1)
                {
                    throw new Exception((string)ret3[1]);
                }

                foreach (DataRow row in tUsers.Rows)
                {
                    dicUsers.Add(row["USERID"].ToString(), row["USERNAME"].ToString());
                }
            }
            // 3.组Message
            string allRoles = "";
            IEnumerator enumerRoles = dicGroups.GetEnumerator();
            while (enumerRoles.MoveNext())
            {
                allRoles += ((DictionaryEntry)enumerRoles.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerRoles.Current).Value.ToString() + ") ";
            }
            if (allRoles != "")
                allRoles = allRoles.Substring(0, allRoles.LastIndexOf(' '));
            else
                allRoles = "null";

            string allUsers = "";
            IEnumerator enumerUsers = dicUsers.GetEnumerator();
            while (enumerUsers.MoveNext())
            {
                allUsers += ((DictionaryEntry)enumerUsers.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerUsers.Current).Value.ToString() + ") ";
            }
            if (allUsers != "")
                allUsers = allUsers.Substring(0, allUsers.LastIndexOf(' '));
            else
                allUsers = "null";

            string message = "";
            if (allRoles == "null")
            {
                string m = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "WaitMessage2", false);
                message += string.Format(m, allUsers);
            }
            else
            {
                string m = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "WaitMessage", false);
                message += string.Format(m, allRoles, allUsers);
            }

          

            return message;
        }

        public static string ShowParallelMessage(string sendToIds, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            List<string> sendToUsers = new List<string>();
            List<string> sendToRoles = new List<string>();
            string[] toIds = sendToIds.Split(';');
            for (int i = 0; i < toIds.Length; i++)
            {
                if (toIds[i].IndexOf(':') != -1)
                {
                    sendToUsers.Add(toIds[i].Substring(0, toIds[i].IndexOf(':')));
                }
                else
                {
                    sendToRoles.Add(toIds[i]);
                }
            }

            Hashtable dicGroups = new Hashtable();
            Hashtable dicUsers = new Hashtable();

            // 1.取出所有的RoleId和RoleName及Role以下的UserID填入sendToUsers
            string sqlRoles = "";
            foreach (string role in sendToRoles)
            {
                sqlRoles += "'" + role + "',";
            }
            if (sqlRoles != "")
            {
                sqlRoles = sqlRoles.Substring(0, sqlRoles.LastIndexOf(','));

                DataTable tGroups = null;

                object[] ret1 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID in(" + sqlRoles + ") AND ISROLE='Y'" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tGroups = ((DataSet)ret1[1]).Tables[0];
                }
                foreach (DataRow row in tGroups.Rows)
                {
                    dicGroups.Add(row["GROUPID"].ToString(), row["GROUPNAME"].ToString());

                    DataTable tHoldUsers = null;

                    object[] ret2 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + row["GROUPID"].ToString() + "')" });
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            sendToUsers.Add(holdRow["USERID"].ToString());
                        }
                    }
                }
            }
            // 2.取出所有的UserId和UserName
            string sqlUsers = "";
            foreach (string user in sendToUsers)
            {
                sqlUsers += "'" + user + "',";
            }
            if (sqlUsers != "")
            {
                sqlUsers = sqlUsers.Substring(0, sqlUsers.LastIndexOf(','));
                DataTable tUsers = null;

                object[] ret3 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID in(" + sqlUsers + ")" });
                if (ret3 != null && (int)ret3[0] == 0)
                {
                    tUsers = ((DataSet)ret3[1]).Tables[0];
                }

                foreach (DataRow row in tUsers.Rows)
                {
                    dicUsers.Add(row["USERID"].ToString(), row["USERNAME"].ToString());
                }
            }
            // 3.组Message
            string allRoles = "";
            IEnumerator enumerRoles = dicGroups.GetEnumerator();
            while (enumerRoles.MoveNext())
            {
                allRoles += ((DictionaryEntry)enumerRoles.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerRoles.Current).Value.ToString() + ") ";
            }
            if (allRoles != "")
                allRoles = allRoles.Substring(0, allRoles.LastIndexOf(' '));
            else
                allRoles = "null";

            string allUsers = "";
            IEnumerator enumerUsers = dicUsers.GetEnumerator();
            while (enumerUsers.MoveNext())
            {
                allUsers += ((DictionaryEntry)enumerUsers.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerUsers.Current).Value.ToString() + ") ";
            }
            if (allUsers != "")
                allUsers = allUsers.Substring(0, allUsers.LastIndexOf(' '));
            else
                allUsers = "null";

            string message = "";
            SYS_LANGUAGE lan = (SYS_LANGUAGE)(clientInfo[0] as object[])[0];
            string m = SysMsg.GetSystemMessage(lan, "FLTools", "GloFix", "WaitMessage", false);
            message += string.Format(m, allRoles, allUsers);

            return message;
        }

        public static string ShowNotifyMessage(string sendToIds)
        {
            List<string> sendToUsers = new List<string>();
            List<string> sendToRoles = new List<string>();
            string[] toIds = sendToIds.Split(';');
            for (int i = 0; i < toIds.Length; i++)
            {
                if (toIds[i].IndexOf(':') != -1)
                {
                    sendToUsers.Add(toIds[i].Substring(0, toIds[i].IndexOf(':')));
                }
                else
                {
                    sendToRoles.Add(toIds[i]);
                }
            }

            Hashtable dicGroups = new Hashtable();
            Hashtable dicUsers = new Hashtable();

            // 1.取出所有的RoleId和RoleName及Role以下的UserID填入sendToUsers
            string sqlRoles = "";
            foreach (string role in sendToRoles)
            {
                sqlRoles += "'" + role + "',";
            }
            if (sqlRoles != "")
            {
                sqlRoles = sqlRoles.Substring(0, sqlRoles.LastIndexOf(','));
                DataTable tGroups = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID in(" + sqlRoles + ") AND ISROLE='Y'" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tGroups = ((DataSet)ret1[1]).Tables[0];
                }
                else if (ret1 != null && (int)ret1[0] == 1)
                {
                    throw new Exception((string)ret1[1]);
                }
                foreach (DataRow row in tGroups.Rows)
                {
                    dicGroups.Add(row["GROUPID"].ToString(), row["GROUPNAME"].ToString());

                    DataTable tHoldUsers = null;

                    object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + row["GROUPID"].ToString() + "')" });
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    else if (ret2 != null && (int)ret2[0] == 1)
                    {
                        throw new Exception((string)ret2[1]);
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            sendToUsers.Add(holdRow["USERID"].ToString());
                        }
                    }
                }
            }
            // 2.取出所有的UserId和UserName
            string sqlUsers = "";
            foreach (string user in sendToUsers)
            {
                sqlUsers += "'" + user + "',";
            }
            if (sqlUsers != "")
            {
                sqlUsers = sqlUsers.Substring(0, sqlUsers.LastIndexOf(','));
                DataTable tUsers = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID in(" + sqlUsers + ")" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tUsers = ((DataSet)ret1[1]).Tables[0];
                }
                if (ret1 != null && (int)ret1[0] == 1)
                {
                    throw new Exception((string)ret1[1]);
                }

                foreach (DataRow row in tUsers.Rows)
                {
                    dicUsers.Add(row["USERID"].ToString(), row["USERNAME"].ToString());
                }
            }
            // 3.组Message
            string allRoles = "";
            IEnumerator enumerRoles = dicGroups.GetEnumerator();
            while (enumerRoles.MoveNext())
            {
                allRoles += ((DictionaryEntry)enumerRoles.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerRoles.Current).Value.ToString() + ") ";
            }
            if (allRoles != "")
                allRoles = allRoles.Substring(0, allRoles.LastIndexOf(' '));

            string allUsers = "";
            IEnumerator enumerUsers = dicUsers.GetEnumerator();
            while (enumerUsers.MoveNext())
            {
                allUsers += ((DictionaryEntry)enumerUsers.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerUsers.Current).Value.ToString() + ") ";
            }
            if (allUsers != "")
                allUsers = allUsers.Substring(0, allUsers.LastIndexOf(' '));

            string message = "";
            string m = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "NotifyUsers");
            message += string.Format(m, allRoles, allUsers);

            return message;
        }

        public static string ShowNotifyMessage(string sendToIds, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            List<string> sendToUsers = new List<string>();
            List<string> sendToRoles = new List<string>();
            string[] toIds = sendToIds.Split(';');
            for (int i = 0; i < toIds.Length; i++)
            {
                if (toIds[i].IndexOf(':') != -1)
                {
                    sendToUsers.Add(toIds[i].Substring(0, toIds[i].IndexOf(':')));
                }
                else
                {
                    sendToRoles.Add(toIds[i]);
                }
            }

            Hashtable dicGroups = new Hashtable();
            Hashtable dicUsers = new Hashtable();

            // 1.取出所有的RoleId和RoleName及Role以下的UserID填入sendToUsers
            string sqlRoles = "";
            foreach (string role in sendToRoles)
            {
                sqlRoles += "'" + role + "',";
            }
            if (sqlRoles != "")
            {
                sqlRoles = sqlRoles.Substring(0, sqlRoles.LastIndexOf(','));
                DataTable tGroups = null;

                object[] ret1 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID in(" + sqlRoles + ") AND ISROLE='Y'" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tGroups = ((DataSet)ret1[1]).Tables[0];
                }
                foreach (DataRow row in tGroups.Rows)
                {
                    dicGroups.Add(row["GROUPID"].ToString(), row["GROUPNAME"].ToString());

                    DataTable tHoldUsers = null;

                    object[] ret2 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + row["GROUPID"].ToString() + "')" });
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            sendToUsers.Add(holdRow["USERID"].ToString());
                        }
                    }
                }
            }
            // 2.取出所有的UserId和UserName
            string sqlUsers = "";
            foreach (string user in sendToUsers)
            {
                sqlUsers += "'" + user + "',";
            }
            if (sqlUsers != "")
            {
                sqlUsers = sqlUsers.Substring(0, sqlUsers.LastIndexOf(','));
                DataTable tUsers = null;

                object[] ret1 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID in(" + sqlUsers + ")" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tUsers = ((DataSet)ret1[1]).Tables[0];
                }

                foreach (DataRow row in tUsers.Rows)
                {
                    dicUsers.Add(row["USERID"].ToString(), row["USERNAME"].ToString());
                }
            }
            // 3.组Message
            string allRoles = "";
            IEnumerator enumerRoles = dicGroups.GetEnumerator();
            while (enumerRoles.MoveNext())
            {
                allRoles += ((DictionaryEntry)enumerRoles.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerRoles.Current).Value.ToString() + ") ";
            }
            if (allRoles != "")
                allRoles = allRoles.Substring(0, allRoles.LastIndexOf(' '));

            string allUsers = "";
            IEnumerator enumerUsers = dicUsers.GetEnumerator();
            while (enumerUsers.MoveNext())
            {
                allUsers += ((DictionaryEntry)enumerUsers.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerUsers.Current).Value.ToString() + ") ";
            }
            if (allUsers != "")
                allUsers = allUsers.Substring(0, allUsers.LastIndexOf(' '));

            string message = "";
            SYS_LANGUAGE lan = (SYS_LANGUAGE)(clientInfo[0] as object[])[0];
            string m = SysMsg.GetSystemMessage(lan, "FLClientControls", "FLWizard", "NotifyUsers");
            message += string.Format(m, allRoles, allUsers);

            return message;
        }

        public static string ShowPlusMessage(string sendToIds)
        {
            return ShowNotifyMessage(sendToIds);
        }

        public static string ShowPlusMessage(string sendToIds, object[] clientInfo)
        {
            return ShowNotifyMessage(sendToIds, clientInfo);
        }


        public static string ShowMessage(string sendToIds, bool isWeb)
        {
            string message = "";//最后返回的消息
            string[] toIds = sendToIds.Split(';');
            string messageModule = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "SendToMan", isWeb);
            string messageModule1 = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "SendToMan1", isWeb);
            string sign = isWeb ? "</br>" : "\n\r";
            for (int i = 0; i < toIds.Length; i++)
            {
                string[] parts = toIds[i].Split('|');
                string role = "", users = "";
                if (parts[1].IndexOf(':') != -1) //U001:UserId
                {
                    role = "null";
                    string userid = parts[1].Substring(0, parts[1].IndexOf(':'));
                    DataTable tUsers = null;

                    object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID='" + userid + "'" });
                    if (ret1 != null && (int)ret1[0] == 0)
                    {
                        tUsers = ((DataSet)ret1[1]).Tables[0];
                    }
                    else if (ret1 != null && (int)ret1[0] == 1)
                    {
                        throw new Exception((string)ret1[1]);
                    }
                    if (tUsers.Rows.Count > 0)
                    {
                        users = tUsers.Rows[0]["USERID"].ToString() + "(" + tUsers.Rows[0]["USERNAME"].ToString() + ")";
                    }
                }
                else //R01
                {
                    string groupid = parts[1];
                    DataTable tGroups = null;

                    object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID='" + groupid + "' AND ISROLE='Y'" });
                    if (ret1 != null && (int)ret1[0] == 0)
                    {
                        tGroups = ((DataSet)ret1[1]).Tables[0];
                    }
                    else if (ret1 != null && (int)ret1[0] == 1)
                    {
                        throw new Exception((string)ret1[1]);
                    }
                    if (tGroups.Rows.Count > 0)
                    {
                        role = tGroups.Rows[0]["GROUPID"].ToString() + "(" + tGroups.Rows[0]["GROUPNAME"].ToString() + ")";
                    }

                    DataTable tHoldUsers = null;

                    object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + groupid + "')" });
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    else if (ret2 != null && (int)ret2[0] == 1)
                    {
                        throw new Exception((string)ret2[1]);
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            users += holdRow["USERID"].ToString() + "(" + holdRow["USERNAME"].ToString() + ") ";
                        }
                    }
                }
                if (role == "null")
                {
                    message += string.Format(messageModule1, parts[0], users) + sign;
                }
                else
                {
                    message += string.Format(messageModule, parts[0], role, users) + sign;
                }
            }
            return message;
        }

        public static string ShowMessage(string sendToIds, bool isWeb, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string message = "";//最后返回的消息
            string[] toIds = sendToIds.Split(';');
            SYS_LANGUAGE lan = (SYS_LANGUAGE)(clientInfo[0] as object[])[0];
            string messageModule = SysMsg.GetSystemMessage(lan, "FLClientControls", "FLWizard", "SendToMan", isWeb);
            string messageModule1 = SysMsg.GetSystemMessage(lan, "FLClientControls", "FLWizard", "SendToMan1", isWeb);
            string sign = isWeb ? "</br>" : "\r\n";
            for (int i = 0; i < toIds.Length; i++)
            {
                string[] parts = toIds[i].Split('|');
                string role = "", users = "";
                if (parts[1].IndexOf(':') != -1) //U001:UserId
                {
                    role = "null";
                    string userid = parts[1].Substring(0, parts[1].IndexOf(':'));
                    DataTable tUsers = null;

                    object[] ret1 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID='" + userid + "'" });
                    
                    if (ret1 != null && (int)ret1[0] == 0)
                    {
                        tUsers = ((DataSet)ret1[1]).Tables[0];
                    }
                    if (ret1 != null && (int)ret1[0] == 1)
                    {
                        throw new Exception((string)ret1[1]);
                    }
                    if (tUsers.Rows.Count > 0)
                    {
                        users = tUsers.Rows[0]["USERID"].ToString() + "(" + tUsers.Rows[0]["USERNAME"].ToString() + ")";
                    }
                }
                else //R01
                {
                    string groupid = parts[1];
                    DataTable tGroups = null;

                    object[] ret1 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID='" + groupid + "' AND ISROLE='Y'" });
                  
                    if (ret1 != null && (int)ret1[0] == 0)
                    {
                        tGroups = ((DataSet)ret1[1]).Tables[0];
                    }
                    else if (ret1 != null && (int)ret1[0] == 1)
                    {
                        throw new Exception((string)ret1[1]);
                    }
                    if (tGroups.Rows.Count > 0)
                    {
                        role = tGroups.Rows[0]["GROUPID"].ToString() + "(" + tGroups.Rows[0]["GROUPNAME"].ToString() + ")";
                    }

                    DataTable tHoldUsers = null;

                    object[] ret2 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + groupid + "')" });
                    
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    else if (ret2 != null && (int)ret2[0] == 1)
                    {
                        throw new Exception((string)ret2[1]);
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            users += holdRow["USERID"].ToString() + "(" + holdRow["USERNAME"].ToString() + ") ";
                        }
                    }
                }
                if (role == "null")
                {
                    message += string.Format(messageModule1, parts[0], users) + sign;
                }
                else
                {
                    message += string.Format(messageModule, parts[0], role, users) + sign;
                }
            }
            if (message.EndsWith("\n"))
                message = message.Remove(message.LastIndexOf("\n"));
            return message;
        }


        public static string ShowMessage2(string sendToIds)
        {
            if (sendToIds == "")
                return "wait for others to check!";
            string[] toIds = sendToIds.Split(';');
            List<string> sendToUsers = new List<string>();
            List<string> sendToRoles = new List<string>();
            List<string> sendToManager = new List<string>();
            for (int i = 0; i < toIds.Length; i++)
            {
                string[] parts = toIds[i].Split('|');
                sendToManager.Add(parts[0]);
                if (parts[1].IndexOf(':') != -1)
                {
                    sendToUsers.Add(parts[1].Substring(0, parts[1].IndexOf(':')));
                }
                else
                {
                    sendToRoles.Add(parts[1]);
                }
            }
            Hashtable dicGroups = new Hashtable();
            Hashtable dicUsers = new Hashtable();
            // 1.取出所有的RoleId和RoleName及Role以下的UserID填入sendToUsers
            string sqlRoles = "";
            foreach (string role in sendToRoles)
            {
                sqlRoles += "'" + role + "',";
            }
            if (sqlRoles != "")
            {
                sqlRoles = sqlRoles.Substring(0, sqlRoles.LastIndexOf(','));
                DataTable tGroups = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID in(" + sqlRoles + ") AND ISROLE='Y'" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tGroups = ((DataSet)ret1[1]).Tables[0];
                }
                else if (ret1 != null && (int)ret1[0] == 1)
                {
                    throw new Exception((string)ret1[1]);
                }
                foreach (DataRow row in tGroups.Rows)
                {
                    dicGroups.Add(row["GROUPID"].ToString(), row["GROUPNAME"].ToString());

                    DataTable tHoldUsers = null;

                    object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + row["GROUPID"].ToString() + "')" });
                    if (ret2 != null && (int)ret2[0] == 0)
                    {
                        tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                    }
                    else if (ret2 != null && (int)ret2[0] == 1)
                    {
                        throw new Exception((string)ret2[1]);
                    }
                    if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                    {
                        foreach (DataRow holdRow in tHoldUsers.Rows)
                        {
                            sendToUsers.Add(holdRow["USERID"].ToString());
                        }
                    }
                }
            }

            // 2.取出所有的UserId和UserName
            string sqlUsers = "";
            foreach (string user in sendToUsers)
            {
                sqlUsers += "'" + user + "',";
            }
            if (sqlUsers != "")
            {
                sqlUsers = sqlUsers.Substring(0, sqlUsers.LastIndexOf(','));

                DataTable tUsers = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID in(" + sqlUsers + ")" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tUsers = ((DataSet)ret1[1]).Tables[0];
                }
                else if (ret1 != null && (int)ret1[0] == 1)
                {
                    throw new Exception((string)ret1[1]);
                }
                foreach (DataRow row in tUsers.Rows)
                {
                    dicUsers.Add(row["USERID"].ToString(), row["USERNAME"].ToString());
                }
            }

            // 3.组Message
            string allManager = "";
            foreach (string manager in sendToManager)
            {
                allManager += manager + " ";
            }
            if (allManager != "")
                allManager = allManager.Substring(0, allManager.LastIndexOf(' '));

            string allRoles = "";
            IEnumerator enumerRoles = dicGroups.GetEnumerator();
            while (enumerRoles.MoveNext())
            {
                allRoles += ((DictionaryEntry)enumerRoles.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerRoles.Current).Value.ToString() + ") ";
            }
            if (allRoles != "")
                allRoles = allRoles.Substring(0, allRoles.LastIndexOf(' '));

            string allUsers = "";
            IEnumerator enumerUsers = dicUsers.GetEnumerator();
            while (enumerUsers.MoveNext())
            {
                allUsers += ((DictionaryEntry)enumerUsers.Current).Key.ToString() + "(" + ((DictionaryEntry)enumerUsers.Current).Value.ToString() + ") ";
            }
            if (allUsers != "")
                allUsers = allUsers.Substring(0, allUsers.LastIndexOf(' '));

            string message = "";
            string m = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "FLWizard", "SendToManager");
            message += string.Format(m, allManager, allRoles, allUsers);

            return message;
        }

        public static string ShowMessage3(string sendToIds, bool isWeb)
        {
            string message = "";//最后返回的消息
            string[] roles = sendToIds.Split(';');
            string messageModule = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLTools", "GloFix", "WaitMessage1");
            string sign = isWeb ? "</br>" : "\r\n";
            for (int i = 0; i < roles.Length; i++)
            {
                string role = roles[i], users = "";
                string groupid = roles[i];
                DataTable tGroups = null;

                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME, ISROLE FROM GROUPS WHERE GROUPID='" + groupid + "' AND ISROLE='Y'" });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    tGroups = ((DataSet)ret1[1]).Tables[0];
                }
                else if (ret1 != null && (int)ret1[0] == 1)
                {
                    throw new Exception((string)ret1[1]);
                }
                if (tGroups.Rows.Count > 0)
                {
                    role = tGroups.Rows[0]["GROUPID"].ToString() + "(" + tGroups.Rows[0]["GROUPNAME"].ToString() + ")";
                }

                DataTable tHoldUsers = null;

                object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERID, USERNAME FROM USERS WHERE USERID IN (SELECT USERID FROM USERGROUPS WHERE GROUPID='" + groupid + "')" });
                if (ret2 != null && (int)ret2[0] == 0)
                {
                    tHoldUsers = ((DataSet)ret2[1]).Tables[0];
                }
                else if (ret2 != null && (int)ret2[0] == 1)
                {
                    throw new Exception((string)ret2[1]);
                }
                if (tHoldUsers != null && tHoldUsers.Rows.Count > 0)
                {
                    foreach (DataRow holdRow in tHoldUsers.Rows)
                    {
                        users += holdRow["USERID"].ToString() + "(" + holdRow["USERNAME"].ToString() + ") ";
                    }
                }
                message += string.Format(messageModule, role, users) + sign;
            }
            return message;
        }

        public static bool IsFlowItem(string Name)
        {
            if (Name == "Submit" || Name == "Approve" || Name == "Return"
                || Name == "Reject" || Name == "Notify" || Name == "FlowDelete"
                || Name == "Plus" || Name == "Pause" || Name == "Comment")
            {
                return true;
            }
            return false;
        }

        public static bool IsFlowState(string Name)
        {
            if (Name == "Normal" || Name == "Insert" || Name == "Modify"
                || Name == "Inquery" || Name == "Prepare")
            {
                return true;
            }
            return false;
        }

        public static string DateTimeString(DateTime date)
        {
            string year = date.Year.ToString();
            string month = dformat(date.Month.ToString());
            string day = dformat(date.Day.ToString());
            string hour = dformat(date.Hour.ToString());
            string minute = dformat(date.Minute.ToString());
            string second = dformat(date.Second.ToString());

            return year + month + day + hour + minute + second;
        }

        private static string dformat(string datePart)
        {
            return (datePart.Length == 2) ? datePart : "0" + datePart;
        }

        public static string GetFlowDesc(string param, bool isParamListId)
        {
            if (isParamListId)
            {
                string sql = "SELECT FLOW_DESC FROM SYS_TODOLIST WHERE LISTID='" + param + "'";
                object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
                if (ret1 != null && (int)ret1[0] == 0)
                {
                    if (((DataSet)ret1[1]).Tables[0].Rows.Count > 0)
                        return ((DataSet)ret1[1]).Tables[0].Rows[0][0].ToString();
                }
            }
            else
            {
                string file = param; // +".xoml";
                //object[] obj = CliUtils.CallFLMethod("GetFLDescription", new object[] { file });
                object[] obj = CliUtils.CallFLMethod("GetFLDescription", new object[] { file });
                if (Convert.ToInt16(obj[0]) == 0)
                {
                    return obj[1].ToString();
                }
            }
            return "";
        }


        public static bool IsCurrentActivity(string flowpath, string listID)
        {
            DataTable currentTable = GetCurrentTable(listID);
            if (currentTable != null)
            {
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    //加签返回的flowpath值不一样
                    string currentPath = currentTable.Rows[i]["FLOWPATH"].ToString();
                    if (flowpath.Split(';')[1] == currentPath.Split(';')[1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static DataTable GetCurrentTable(string listID)
        {
            string sql = "SELECT SENDTO_KIND, SENDTO_ID, FLOWPATH FROM SYS_TODOLIST WHERE ((PLUSROLES IS NOT NULL AND PLUSROLES <> '' AND STATUS in ('A', 'AA')) OR ((PLUSROLES IS NULL OR PLUSROLES = '') AND STATUS NOT IN ('A', 'AA'))) AND LISTID ='" + listID + "'";//加签有问题
            object[] ret = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (ret != null && (int)ret[0] == 0)
            {
                return ((DataSet)ret[1]).Tables[0];
            }
            else
            {
                return null;
            }
        }


        public static string GetFlowSql(string user, ESqlMode sqlMode)
        {
            string connectMark = "";

            String dbAlias = CliUtils.fLoginDB;
            String dbType = String.Empty;
            object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            if (xx[0].ToString() == "0")
                dbAlias = xx[1].ToString();
            object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { dbAlias });

            if (myRet != null && myRet[0].ToString() == "0")
            {
                switch (myRet[1].ToString())
                {
                    case "1":
                    case "2":
                        connectMark = "+"; break;
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                        connectMark = "||"; break;
                }
            }
            string dtString = DateTimeString(DateTime.Now);
            string sql = "";
            switch (sqlMode)
            {
                case ESqlMode.ToDoList:
                    #region ToDoList
                    if (myRet[1].ToString() == "5")
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, CONCAT(SYS_TODOLIST.UPDATE_DATE,' ',SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1' AND ((SYS_TODOLIST.SENDTO_ID IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y') OR SYS_TODOLIST.SENDTO_ID IN (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "'))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'F')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    else if (myRet[1].ToString() == "4" && myRet[2].ToString() == "0" || myRet[1].ToString() == "6")
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1'AND SYS_TODOLIST.STATUS<>'F' AND ((SYS_TODOLIST.SENDTO_ID IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y') OR SYS_TODOLIST.SENDTO_ID IN (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "'))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'F')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    else if (myRet[1].ToString() == "2")
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1'AND SYS_TODOLIST.STATUS<>'F' AND ((SYS_TODOLIST.SENDTO_ID IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y') OR SYS_TODOLIST.SENDTO_ID IN (SELECT ROLE_ID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "'))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'F')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    else
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE ((SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS<>'F' AND ((SYS_TODOLIST.SENDTO_ID IN ((SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')  UNION (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "')))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'F'))) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    #endregion
                    break;

                case ESqlMode.ToDoHis:
                    string formName = "(select top 1 FORM_NAME FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "'ORDER BY SYS_TODOHIS.UPDATE_DATE desc,SYS_TODOHIS.UPDATE_TIME desc) as FORM_NAME";
                    string webFormName = "(select top 1 WEBFORM_NAME FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "'ORDER BY SYS_TODOHIS.UPDATE_DATE desc,SYS_TODOHIS.UPDATE_TIME desc) as WEBFORM_NAME";

                    #region ToDoHis
                    if (myRet[1].ToString() == "5")
                        sql = "SELECT SYS_TODOLIST.LISTID, SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, CONCAT(SYS_TODOLIST.UPDATE_DATE,' ',SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE EXISTS (SELECT SYS_TODOHIS.LISTID FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "') AND SYS_TODOLIST.STATUS <> 'F' AND ((SYS_TODOLIST.SENDTO_KIND='1' AND SYS_TODOLIST.SENDTO_ID NOT IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_KIND='2' AND SYS_TODOLIST.SENDTO_ID<>'" + user + "')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    else if (myRet[1].ToString() == "1")
                        sql = "SELECT SYS_TODOLIST.LISTID, SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, " + formName + ", SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO," + webFormName + ", (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE EXISTS (SELECT SYS_TODOHIS.LISTID FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "') AND SYS_TODOLIST.STATUS <> 'F' AND ((SYS_TODOLIST.SENDTO_KIND='1' AND SYS_TODOLIST.SENDTO_ID NOT IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_KIND='2' AND SYS_TODOLIST.SENDTO_ID<>'" + user + "')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    else if (myRet[1].ToString() == "3")
                    {
                        formName = "(SELECT FORM_NAME FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "' AND SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME = (SELECT DISTINCT MAX(SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID and SYS_TODOHIS.USER_ID='" + user + "') AND ROWNUM <= 1) AS FORM_NAME";
                        webFormName = "(SELECT WEBFORM_NAME FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "' AND SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME = (SELECT DISTINCT MAX(SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID and SYS_TODOHIS.USER_ID='" + user + "') AND ROWNUM <= 1) AS WEBFORM_NAME";

                        sql = "SELECT SYS_TODOLIST.LISTID, SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, " + formName + ", SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO," + webFormName + ", (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE EXISTS (SELECT SYS_TODOHIS.LISTID FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "') AND SYS_TODOLIST.STATUS <> 'F' AND ((SYS_TODOLIST.SENDTO_KIND='1' AND SYS_TODOLIST.SENDTO_ID NOT IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_KIND='2' AND SYS_TODOLIST.SENDTO_ID<>'" + user + "')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    }
                    //else if (myRet[1].ToString() == "4" || myRet[1].ToString() == "6")
                    //{
                    //    String sFormName = "(SELECT DISTINCT FORM_NAME MAX(SYS_TODOHIS.UPDATE_DATE), MAX(SYS_TODOHIS.UPDATE_TIME) FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "'";
                    //    String sWebFormName = "";

                    //    sql = "SELECT SYS_TODOLIST.LISTID, SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME,SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO,SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                    //        "WHERE EXISTS (SELECT SYS_TODOHIS.LISTID FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "') AND SYS_TODOLIST.STATUS <> 'F' AND ((SYS_TODOLIST.SENDTO_KIND='1' AND SYS_TODOLIST.SENDTO_ID NOT IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_KIND='2' AND SYS_TODOLIST.SENDTO_ID<>'" + user + "')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    //}
                    else
                    {
                        formName = "(SELECT FORM_NAME FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "' AND SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME = (SELECT DISTINCT MAX(SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID and SYS_TODOHIS.USER_ID='" + user + "')) AS FORM_NAME";
                        webFormName = "(SELECT WEBFORM_NAME FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "' AND SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME = (SELECT DISTINCT MAX(SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID and SYS_TODOHIS.USER_ID='" + user + "')) AS WEBFORM_NAME";

                        sql = "SELECT SYS_TODOLIST.LISTID, SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, " + formName + ", SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO," + webFormName + ", (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE EXISTS (SELECT SYS_TODOHIS.LISTID FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "') AND SYS_TODOLIST.STATUS <> 'F' AND ((SYS_TODOLIST.SENDTO_KIND='1' AND SYS_TODOLIST.SENDTO_ID NOT IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_KIND='2' AND SYS_TODOLIST.SENDTO_ID<>'" + user + "')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    }
                    #endregion
                    break;
                case ESqlMode.Notify:
                    #region Notify
                    if (myRet[1].ToString() == "5")
                    {
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, CONCAT(SYS_TODOLIST.UPDATE_DATE,' ',SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS='F' AND ((SYS_TODOLIST.SENDTO_ID IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_ID IN  (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "') AND (SYS_TODOLIST.S_USER_ID<>'" + user + "'))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'NF' AND SYS_TODOLIST.STATUS<>'NR')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    }
                    else if (myRet[1].ToString() == "4" && myRet[2].ToString() == "0" || myRet[1].ToString() == "6")
                    {
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS='F' AND ((SYS_TODOLIST.SENDTO_ID IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y'))  OR (SYS_TODOLIST.SENDTO_ID IN (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "') AND (SYS_TODOLIST.S_USER_ID<>'" + user + "'))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'NF' AND SYS_TODOLIST.STATUS<>'NR')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    }
                    else if (myRet[1].ToString() == "2")
                    {
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS='F' AND ((SYS_TODOLIST.SENDTO_ID IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y'))  OR (SYS_TODOLIST.SENDTO_ID IN (SELECT ROLE_ID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "') AND (SYS_TODOLIST.S_USER_ID<>'" + user + "'))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'NF' AND SYS_TODOLIST.STATUS<>'NR')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    }
                    else
                    {
                        sql = "SELECT SYS_TODOLIST.LISTID,SYS_TODOLIST.FLOW_ID, SYS_TODOLIST.FLOW_DESC, SYS_TODOLIST.APPLICANT, SYS_TODOLIST.S_USER_ID, SYS_TODOLIST.S_STEP_ID, SYS_TODOLIST.S_STEP_DESC, SYS_TODOLIST.D_STEP_ID, SYS_TODOLIST.D_STEP_DESC, SYS_TODOLIST.EXP_TIME, SYS_TODOLIST.URGENT_TIME, SYS_TODOLIST.TIME_UNIT, SYS_TODOLIST.USERNAME, SYS_TODOLIST.FORM_NAME, SYS_TODOLIST.NAVIGATOR_MODE, SYS_TODOLIST.FLNAVIGATOR_MODE, SYS_TODOLIST.PARAMETERS, SYS_TODOLIST.SENDTO_KIND, SYS_TODOLIST.SENDTO_ID, SYS_TODOLIST.SENDTO_NAME, SYS_TODOLIST.FLOWIMPORTANT, SYS_TODOLIST.FLOWURGENT, SYS_TODOLIST.STATUS, SYS_TODOLIST.FORM_TABLE, SYS_TODOLIST.FORM_KEYS, SYS_TODOLIST.FORM_PRESENTATION, SYS_TODOLIST.FORM_PRESENT_CT, SYS_TODOLIST.REMARK, SYS_TODOLIST.PROVIDER_NAME, SYS_TODOLIST.VERSION, SYS_TODOLIST.EMAIL_ADD, SYS_TODOLIST.EMAIL_STATUS, SYS_TODOLIST.VDSNAME, SYS_TODOLIST.SENDBACKSTEP, SYS_TODOLIST.LEVEL_NO, SYS_TODOLIST.WEBFORM_NAME, (SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS UPDATE_WHOLE_TIME, SYS_TODOLIST.FLOWPATH, SYS_TODOLIST.PLUSAPPROVE, SYS_TODOLIST.PLUSROLES, SYS_TODOLIST.MULTISTEPRETURN, SYS_TODOLIST.ATTACHMENTS FROM SYS_TODOLIST " +
                            "WHERE (SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS='F' AND (SYS_TODOLIST.SENDTO_ID IN ((SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')  union (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "')) AND (SYS_TODOLIST.S_USER_ID<>'" + user + "')) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'NF' AND SYS_TODOLIST.STATUS<>'NR')) ORDER BY SYS_TODOLIST.UPDATE_DATE,SYS_TODOLIST.UPDATE_TIME";
                    }
                    #endregion
                    break;
                case ESqlMode.ToDoListStatist:
                    #region ToDoListStatist
                    sql = "SELECT SYS_TODOLIST.FLOW_DESC,COUNT(SYS_TODOLIST.LISTID) AS TODOLIST_COUNT FROM SYS_TODOLIST WHERE ((SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS<>'F' AND ((SYS_TODOLIST.SENDTO_ID IN ((SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "') AND ISROLE='Y')  UNION (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "')))) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'F'))) GROUP BY SYS_TODOLIST.FLOW_DESC";
                    #endregion
                    break;
                case ESqlMode.ToDoHisStatist:
                    #region ToDoHisStatist
                    sql = "SELECT SYS_TODOLIST.FLOW_DESC,COUNT(SYS_TODOLIST.LISTID) AS TODOHIS_COUNT FROM SYS_TODOLIST WHERE EXISTS (SELECT SYS_TODOHIS.LISTID FROM SYS_TODOHIS WHERE SYS_TODOHIS.LISTID = SYS_TODOLIST.LISTID AND SYS_TODOHIS.USER_ID ='" + user + "') AND SYS_TODOLIST.STATUS <> 'F' AND ((SYS_TODOLIST.SENDTO_KIND='1' AND SYS_TODOLIST.SENDTO_ID NOT IN (SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')) OR (SYS_TODOLIST.SENDTO_KIND='2' AND SYS_TODOLIST.SENDTO_ID<>'" + user + "')) GROUP BY SYS_TODOLIST.FLOW_DESC";
                    #endregion
                    break;
                case ESqlMode.NotifyStatist:
                    #region NotifyStatist
                    sql = "SELECT SYS_TODOLIST.FLOW_DESC,COUNT(SYS_TODOLIST.LISTID) AS NOTIFY_COUNT FROM SYS_TODOLIST WHERE (SYS_TODOLIST.SENDTO_KIND = '1' AND SYS_TODOLIST.STATUS='F' AND (SYS_TODOLIST.SENDTO_ID IN ((SELECT GROUPID FROM GROUPS WHERE GROUPID IN (SELECT GROUPID FROM USERGROUPS WHERE USERID='" + user + "')  AND ISROLE='Y')  union (SELECT ROLE_ID AS GROUPID FROM SYS_ROLES_AGENT WHERE SYS_TODOLIST.SENDTO_ID=SYS_ROLES_AGENT.ROLE_ID AND (SYS_ROLES_AGENT.FLOW_DESC='*' OR SYS_ROLES_AGENT.FLOW_DESC IS NULL OR SYS_TODOLIST.FLOW_DESC=SYS_ROLES_AGENT.FLOW_DESC) AND AGENT='" + user + "' AND START_DATE " + connectMark + " START_TIME<='" + dtString + "' AND END_DATE " + connectMark + " END_TIME>='" + dtString + "')) AND (SYS_TODOLIST.S_USER_ID<>'" + user + "')) OR (SYS_TODOLIST.SENDTO_KIND = '2' AND SYS_TODOLIST.SENDTO_ID='" + user + "' AND SYS_TODOLIST.STATUS<>'NF' AND SYS_TODOLIST.STATUS<>'NR')) GROUP BY SYS_TODOLIST.FLOW_DESC";
                    #endregion
                    break;
                case ESqlMode.FlowRunOver:
                    #region FlowRunOver
                    if (myRet[1].ToString() == "5")
                        sql = "SELECT LISTID, FLOW_DESC,D_STEP_ID, FORM_NAME,WEBFORM_NAME,FORM_PRESENTATION,FORM_PRESENT_CT,REMARK, CONCAT(UPDATE_DATE,' ',UPDATE_TIME) AS UPDATE_WHOLE_TIME,ATTACHMENTS,VDSNAME FROM SYS_TODOHIS WHERE LISTID IN (SELECT DISTINCT LISTID FROM SYS_TODOHIS WHERE USER_ID='" + user + "') AND STATUS='Z' ORDER BY UPDATE_WHOLE_TIME DESC";
                    else
                        sql = "SELECT LISTID, FLOW_DESC,D_STEP_ID, FORM_NAME,WEBFORM_NAME,FORM_PRESENTATION,FORM_PRESENT_CT,REMARK, (UPDATE_DATE " + connectMark + " ' ' " + connectMark + " UPDATE_TIME) AS UPDATE_WHOLE_TIME,ATTACHMENTS,VDSNAME FROM SYS_TODOHIS WHERE LISTID IN (SELECT DISTINCT LISTID FROM SYS_TODOHIS WHERE USER_ID='" + user + "') AND STATUS='Z' ORDER BY UPDATE_WHOLE_TIME DESC";
                    #endregion
                    break;
            }
            return sql;
        }
    }
}
