using System;
using System.Collections.Generic;
using System.Text;
using FLTools.ComponentModel;
using FLCore;
using System.Xml;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web;
using System.Data;
using System.Collections;
using System.Linq;

namespace FLRuntime
{
    public static class Email
    {
        private static string _sender = "SendToEmailSender";
        private static string _flowName = "SendToEmailFlowName";
        private static string _activityName = "SendToEmailActivityName";
        //private static string _content = "SendToEmailContent";
        private static string _description = "SendToEmailDescription";
        private static string _dateTime = "SendToEmailDateTime";
        private static string _hyperLink = "SendToEmailHyperLink";
        private static string _hyperLink2 = "SendToEmailHyperLink2";
        private static string _overTime = "InstanceOverTime";
        //private static string _overTime2 = "InstanceOverTime2";
        private static string _header = "CommentTableHeader";
        private static string _status = "InstanceStatus";

        //private static SmtpClient _client;
        //private static MailMessage _message;

        /// <summary>
        /// 取得签核意见
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetComments(string flInstanceId, object[] clientInfo)
        {
            string comments = string.Empty;
            DataSet ds = Global.GetComments(flInstanceId, clientInfo);
            if (ds != null)
            {
                Hashtable statusTable = new Hashtable();
                string status = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _status);
                string[] ss1 = status.Split(",".ToCharArray());
                foreach (string s1 in ss1)
                {
                    if (string.IsNullOrEmpty(s1))
                    {
                        continue;
                    }

                    string[] ss2 = s1.Split(":".ToCharArray());
                    if (ss2.Length != 2)
                    {
                        continue;
                    }
                    statusTable.Add(ss2[0], ss2[1]);
                }


                string header = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _header);
                string[] columns = header.Split(",".ToCharArray());

                comments += "<TR>";
                foreach (string column in columns)
                {
                    if (string.IsNullOrEmpty(column))
                        continue;

                    comments += string.Format("<TD>{0}</TD>", column);
                }
                comments += "</TR>";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    comments += "<TR>";
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        object obj = row[column];
                        if (column.ColumnName.Trim().ToUpper() == "STATUS")
                        {
                            if (obj != null && obj != DBNull.Value)
                            {
                                if (statusTable.ContainsKey(obj.ToString()))
                                {
                                    obj = statusTable[obj.ToString()];
                                }
                            }
                        }
                        else if (column.ColumnName.Trim().ToUpper() == "REMARK")
                        {
                            if (obj != null && obj != DBNull.Value)
                            {
                                obj = InitRemark(obj.ToString());
                            }
                        }

                        if (obj == null || obj == DBNull.Value || obj.ToString() == string.Empty)
                        {
                            obj = "&nbsp;";
                        }

                        comments += string.Format("<TD>{0}</TD>", obj);
                    }
                    comments += "</TR>";
                }
            }

            return comments;
        }


        private static string EncryptParameters(string paramters, string publicKey)
        {
            var info = Encoding.UTF8.GetBytes(paramters);
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(info);
            var param = hash.Concat(info).ToArray();

            var encryptCodes = Convert.FromBase64String(publicKey);
            for (int i = 0; i < param.Length; i++)
            {
                var encryptCode = encryptCodes[i % encryptCodes.Length];
                param[i] += encryptCode;
            }
            return Convert.ToBase64String(param);
        }

        public static string GetUrl(FLInstance flInstance, object[] clientInfo, string userID, IFLBaseActivity nextFLActivity, FLNavigatorMode mode)
        {
            string webUrl = flInstance.GetWebUrl();
            if (webUrl != null && webUrl != "0" && webUrl.IndexOf("/MainPage_Flow.aspx") > 0)
            {
                var databaseType = Srvtools.DbConnectionSet.GetDbConn((string)((object[])clientInfo[0])[2], (string)((object[])clientInfo[0])[17]).DbType.ToString().Replace("ct", string.Empty);
                webUrl = webUrl.Substring(0, webUrl.IndexOf("/MainPage_Flow.aspx")) + "/MainPage_Flow.aspx";
                var key = Srvtools.PublicKey.GetEncryptKey(userID, Global.GetUserName(userID, clientInfo), (string)((object[])clientInfo[0])[2], (string)((object[])clientInfo[0])[6], databaseType, (string)((object[])clientInfo[0])[5]);
                var listID = flInstance.FLInstanceId.ToString();
                IEventWaiting currentFLActivity = (IEventWaiting)flInstance.CurrentFLActivity;
                string flowPath = (currentFLActivity == null ? string.Empty : currentFLActivity.Name) + ";" + nextFLActivity.Name;
                var param = EncryptParameters(string.Format("{{\"listID\":\"{0}\", \"flowPath\":\"{1}\", \"mode\":\"{2}\"}}", listID, flowPath, mode), key);
                return string.Format("href='{0}?key={1}&param={2}'", webUrl, HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(param));
            }
            else
            {
                return string.Empty;
            }
        }

        private static string GetButtons(FLInstance flInstance, object[] clientInfo, EmailSetting setting, string userID, IEventWaiting nextFLActivity)
        {
            var buttons = new StringBuilder();
            string webUrl = flInstance.GetWebUrl();
            if (webUrl != null && webUrl != "0" && webUrl.IndexOf("/MainPage_Flow.aspx") > 0)
            {
                var databaseType = Srvtools.DbConnectionSet.GetDbConn((string)((object[])clientInfo[0])[2], (string)((object[])clientInfo[0])[17]).DbType.ToString().Replace("ct", string.Empty);
                webUrl = webUrl.Substring(0, webUrl.IndexOf("/MainPage_Flow.aspx")) + "/FlowEmail.aspx";
                var key = Srvtools.PublicKey.GetEncryptKey(userID, Global.GetUserName(userID, clientInfo), (string)((object[])clientInfo[0])[2], (string)((object[])clientInfo[0])[6], databaseType, (string)((object[])clientInfo[0])[5]);

                buttons.Append("<br/>");
                var listID = flInstance.FLInstanceId.ToString();
                IEventWaiting currentFLActivity = (IEventWaiting)flInstance.CurrentFLActivity;
                string flowPath = (currentFLActivity == null ? string.Empty : currentFLActivity.Name) + ";" + nextFLActivity.Name;
                var titles = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLClientControls", "FLNavigator", "NavText").Split(';');
                if (setting.ButtonApprove)
                {
                    var param = EncryptParameters(string.Format("{{\"listID\":\"{0}\", \"type\":\"{1}\", \"flowPath\":\"{2}\"}}", listID, "approve", flowPath), key);
                    buttons.Append(string.Format("<a href='{0}?key={1}&param={2}' style='margin:15px 15px 15px 5px'  target='_blank'>{3}</a>", webUrl, HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(param), titles[17]));
                }
                if (setting.ButtonReturn)
                {
                    var param = EncryptParameters(string.Format("{{\"listID\":\"{0}\", \"type\":\"{1}\", \"flowPath\":\"{2}\"}}", listID, "return", flowPath), key);
                    buttons.Append(string.Format("<a href='{0}?key={1}&param={2}' style='margin:15px'  target='_blank'>{3}</a>", webUrl, HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(param), titles[18]));
                }
                if (setting.ButtonReject)
                {
                    if (nextFLActivity.FLNavigatorMode == FLNavigatorMode.Submit)
                    {
                        var param = EncryptParameters(string.Format("{{\"listID\":\"{0}\", \"type\":\"{1}\", \"flowPath\":\"{2}\"}}", listID, "reject", flowPath), key);
                        buttons.Append(string.Format("<a href='{0}?key={1}&param={2}' style='margin:15px'  target='_blank'>{3}</a>", webUrl, HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(param), titles[19]));
                    }
                }
            }

            return buttons.ToString();
        }

        private static string GetPresentation(string presenationCT)
        {
            if (!string.IsNullOrEmpty(presenationCT))
            {
                StringBuilder builder = new StringBuilder();
                string[] presents = presenationCT.Split(';');
                foreach (string present in presents)
                {
                    string[] keyandvalue = present.Split('=');
                    if (keyandvalue.Length > 1)
                    {
                        builder.Append("<TR>");
                        builder.Append("<TD>");
                        builder.Append(keyandvalue[0]);
                        builder.Append("</TD>");
                        builder.Append("<TD COLSPAN=7>");
                        builder.Append(keyandvalue[1]);
                        builder.Append("</TD>");
                        builder.Append("</TR>");
                    }
                }
                return builder.ToString();
            }
            return string.Empty;
        }

        private static string GetBodyHtml(FLInstance flInstance, object[] keyValues, object[] clientInfo)
        {
            var root = flInstance.RootFLActivity as IFLRootActivity;
            var bodyField = root.BodyField;
            if (!string.IsNullOrEmpty(bodyField))
            {
                if (flInstance._hostDataSet == null)
                {
                    flInstance._hostDataSet = HostTable.GetHostDataSet(flInstance, keyValues, clientInfo);
                }
                if (flInstance._hostDataSet.Tables[0].Columns.Contains(bodyField))
                {
                    var bodyHtml = flInstance._hostDataSet.Tables[0].Rows[0][bodyField].ToString();
                    return string.Format("<TR><TD></TD><TD COLSPAN=7>{0}</TD></TR>", bodyHtml);
                }
            }
            return string.Empty;
        }

        private static string GetSubjectHtml(FLInstance flInstance, FLActivity flActivity, object[] clientInfo)
        {
            var status = "N";
            if (flActivity is FLNotifyActivity)
            {
                status = "F";
            }
            else
            {
                if (flInstance.IsPlusApprove)
                {
                    status = "A";
                }
                else if (flInstance.IsReturn)
                {
                    status = "NR";
                }
                else if (flInstance.IsRetake)
                {
                    status = "NF";
                }
            }
            var message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLDesigner", "FLDesigner", "Item3");
            var messages = message.Split(',');
            foreach (var m in messages)
            {
                var textValue = m.Split(':');
                if (textValue[0] == status)
                {
                    return "[" + textValue[1] + "]";
                }
            }

            return string.Empty;
        }

        private static bool GetMultiStepReturn(FLInstance flInstance, FLActivity nextFLActivity)
        {
            if (nextFLActivity is IFLApproveBranchActivity)
            {
                string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                nextFLActivity = flInstance.RootFLActivity.GetFLActivityByName(parentName);
            }
            return flInstance.GetMultiStepReturn(nextFLActivity);
        }

        private static void AddEmail(List<string> sendToes, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                string[] mails = email.Split(';');
                foreach (string mail in mails)
                {
                    if (!string.IsNullOrEmpty(mail))
                    {
                        sendToes.Add(mail);
                    }
                }
            }
        }

        private static bool IsSendToSelf(IEventWaiting activity, object[] clientInfo)
        {
            string userId = ((object[])clientInfo[0])[1].ToString();
            if (activity.SendToKind == SendToKind.Applicate || activity.SendToKind == SendToKind.RefUser || activity.SendToKind == SendToKind.User)
            {
                return userId == activity.SendToId;
            }
            else
            {
                List<string> users = Global.GetUsersIdsByRoleId(activity.SendToId, clientInfo);
                return  users.Contains(userId);
            }
        }

        private static void SendPush(List<string> userIDs,  string flowName, string activityName, string description, string sender, string content, string listID, string flowPath)
        {
            var setting = PushSetting.LoadSetting();
            var subjectList = new List<string>();
            if (setting.Active && !string.IsNullOrEmpty(setting.PushService) && userIDs.Count > 0)
            {
                if (setting.SubjectFlowName)
                {
                    subjectList.Add(flowName);
                }
                if (setting.SubjectActivityName)
                {
                    subjectList.Add(activityName);
                }
                if (setting.SubjectDescription)
                {
                    subjectList.Add(description);
                }
                if (setting.SubjectSender)
                {
                    subjectList.Add(sender);
                }
                if (setting.SubjectContent)
                {
                    subjectList.Add(content);
                }

                var bodyList = new List<string>();
                if (setting.BodySender)
                {
                    bodyList.Add(sender);
                }
                if (setting.BodyFlowName)
                {
                    bodyList.Add(flowName);
                }
                if (setting.BodyActivityName)
                {
                    bodyList.Add(activityName);
                }
                if (setting.BodyContent)
                {
                    bodyList.Add(content);
                }
                if (setting.BodyDescription)
                {
                    bodyList.Add(description);
                }
                if (setting.BodyDatetime)
                {
                    bodyList.Add(DateTime.Now.ToString());
                }

                var db = Srvtools.DbConnectionSet.GetDbConn(Srvtools.DbConnectionSet.GetSystemDatabase(null));
                var regIDs = new List<string>();
                var tokenIDs = new List<string>();
                using (var connection = db.CreateConnection())
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = string.Format("SELECT * FROM UserDevices WHERE UserID IN ({0})", string.Join(",", userIDs.Select(c => string.Format("'{0}'", c))));
                    var adpater = Srvtools.DBUtils.CreateDbDataAdapter(command);
                    var dataSet = new DataSet();
                    adpater.Fill(dataSet);
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[i]["RegID"].ToString()))
                        {
                            regIDs.Add(dataSet.Tables[0].Rows[i]["RegID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[i]["TokenID"].ToString()))
                        {
                            tokenIDs.Add(dataSet.Tables[0].Rows[i]["TokenID"].ToString());
                        }
                    }

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/handler/PushHandler.ashx", setting.PushService));

                    string param = string.Format("server_api_key={0}&p12_file_path={1}&p12_file_password={2}&apn_type=debug&subject={3}&body={4}&regIDs={5}&tokenIDs={6}&listID={7}&flowpath={8}",
                        setting.APIKey, setting.P12FileName, setting.FilePassword, HttpUtility.UrlEncode(string.Join(" ", subjectList)), HttpUtility.UrlEncode(string.Join(" ", bodyList)), string.Join(";", regIDs), string.Join(";", tokenIDs), listID, flowPath);
                    byte[] bs = Encoding.ASCII.GetBytes(param);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = bs.Length;
                    using (Stream reqStream = req.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                        reqStream.Close();
                    }

                    var res = req.GetResponse();
                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    string result = sr.ReadToEnd();
                }
            }
        }

    
        public static void SendTo(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            SendTo(flInstance, flInstanceParms, keyValues, clientInfo, null, null, null);
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void SendTo(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo, List<string> sendid, List<string> sendtype, List<string> activities)
        {
            List<FLActivity> nextFLActivities = flInstance.NextFLActivities;

            if (activities != null)
            {
                nextFLActivities.Clear();
                foreach (string act in activities)
                {
                    FLActivity activity = flInstance.RootFLActivity.GetFLActivityByName(act);
                    if (activity != null)
                    {
                        nextFLActivities.Add(activity);
                    }
                }
            }


            List<string> tmpList = new List<string>();
            foreach (FLActivity flActivity in nextFLActivities)
            {
                if (!(flActivity is IEventWaiting) && !(flActivity is IFLNotifyActivity))
                {
                    continue;
                }

                if (flActivity is IEventWaiting)
                {
                    IEventWaiting nextFLActivity = (IEventWaiting)flActivity;
                    if (nextFLActivity.SendEmail)
                    {
                        tmpList.Add(nextFLActivity.Name);
                    }
                }
                else if(flActivity is IFLNotifyActivity)
                {
                    IFLNotifyActivity nextFLActivity = (IFLNotifyActivity)flActivity;
                    if (nextFLActivity.SendEmail)
                    {
                        tmpList.Add(nextFLActivity.Name);
                    }
                }
            }
            if (tmpList.Count == 0)
                return;

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);

            var setting = EmailSetting.LoadSetting(clientInfo);

            var active = setting.Activie;
            if (!active)
            {
                return;
            }

            var sendFrom = setting.SendFrom;
            var password = setting.Password;
            var smtp = setting.SMTP;
            var enableSSL = setting.EnableSSL;
            var port = setting.Port;

            foreach (FLActivity flActivity in nextFLActivities)
            {
                List<string> sendToes = new List<string>();
                if (!(flActivity is IEventWaiting) && !(flActivity is IFLNotifyActivity))
                {
                    continue;
                }

                bool sm = false;
                if (flActivity is IEventWaiting)
                {
                    if (!flInstance.IsPlusApprove && (flInstance.RootFLActivity as IFLRootActivity).SkipForSameUser && IsSendToSelf(flActivity as IEventWaiting, clientInfo))
                    {
                        continue;
                    }
                    sm = ((IEventWaiting)flActivity).SendEmail;
                }
                else if(flActivity is IFLNotifyActivity)
                {
                    sm = ((IFLNotifyActivity)flActivity).SendEmail;
                }

                if (sm)
                {
                    FLActivity nextFLActivity = flActivity;
                    int plusApprove = 0;
                    if (nextFLActivity is IFLStandActivity)
                    {
                        plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity).PlusApprove);
                    }
                    else if (nextFLActivity is IFLApproveActivity)
                    {
                        plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity).PlusApprove);
                    }
                    else if (nextFLActivity is IFLApproveBranchActivity)
                    {
                        FLActivity approve = flInstance.RootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)nextFLActivity).ParentActivity);
                        plusApprove = Convert.ToInt32(((IFLApproveActivity)approve).PlusApprove);
                    }

                    string users = string.Empty;
                    string email = string.Empty;
                    List<string> roleIds = new List<string>();
                    List<string> userIds = new List<string>();
                    SendToKind sk = SendToKind.Applicate;
                    string sr = string.Empty;
                    string sf = string.Empty;
                    string su = string.Empty;
                    string fn = string.Empty;
                    string wfn = string.Empty;
                    NavigatorMode nm = NavigatorMode.Normal;
                    FLNavigatorMode fnm = FLNavigatorMode.Notify;
                    if (flActivity is IEventWaiting)
                    {
                        sk = ((IEventWaiting)flActivity).SendToKind;
                        sr = ((IEventWaiting)flActivity).SendToRole;
                        su = ((IEventWaiting)flActivity).SendToUser;
                        sf = ((IEventWaiting)flActivity).SendToField;
                        wfn = string.IsNullOrEmpty(((IEventWaiting)flActivity).WebFormName)
                            ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                            : ((IEventWaiting)flActivity).WebFormName;
                        nm = ((IEventWaiting)flActivity).NavigatorMode;
                        fnm = ((IEventWaiting)flActivity).FLNavigatorMode;
                    }
                    else if (flActivity is IFLNotifyActivity)
                    {
                        sk = ((IFLNotifyActivity)flActivity).SendToKind;
                        sr = ((IFLNotifyActivity)flActivity).SendToRole;
                        su = ((IFLNotifyActivity)flActivity).SendToUser;
                        sf = ((IFLNotifyActivity)flActivity).SendToField;
                        wfn = string.IsNullOrEmpty(((IFLNotifyActivity)flActivity).WebFormName)
                                     ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                                     : ((IFLNotifyActivity)flActivity).WebFormName;
                        nm = ((IFLNotifyActivity)flActivity).NavigatorMode;
                        fnm = ((IFLNotifyActivity)flActivity).FLNavigatorMode;
                    }

                    // string roleId = string.Empty;
                    string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;
                    if (activities != null)
                    {
                        int index = activities.IndexOf(flActivity.Name);
                        string id = sendid[index];
                        string type = sendtype[index];
                        if (type == "1")
                        {
                            roleIds.Add(id);
                        }
                        else
                        {
                            if (id.Trim().Length > 0)
                            {
                                userIds.Add(id);
                            }
                        }
                    }
                    else
                    {

                        if (flInstance.IsPlusApprove)
                        {
                            string q = flInstanceParms[8].ToString();
                            string[] qq = q.Split(";".ToCharArray());
                            foreach (string r in qq)
                            {
                                if (!string.IsNullOrEmpty(r))
                                {
                                    if (r.StartsWith("U:"))
                                    {
                                        userIds.Add(r.Substring(2));
                                    }
                                    else
                                    {
                                        roleIds.Add(r);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (sk == SendToKind.Applicate)
                            {
                                userIds.Add(flInstance.Creator);
                            }
                            else if (sk == SendToKind.Role)
                            {
                                string q = sr;
                                string[] qq = q.Split(";".ToCharArray());

                                // roleId = qq[0].Trim();
                                roleIds.Add(qq[0].Trim());
                            }
                            else if (sk == SendToKind.ApplicateManager)
                            {
                                if (nextFLActivity is IFLApproveBranchActivity && !string.IsNullOrEmpty(flInstance.R))
                                {
                                    // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                                    roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(flInstance.CreateRole))
                                    {
                                        roleIds.Add(Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo));
                                    }
                                    else
                                    {
                                        List<string> roles = Global.GetRoleIdsByUserId(flInstance.Creator, clientInfo);
                                        if (roles.Count > 0)
                                        {
                                            roleIds.Add(Global.GetManagerRoleId(roles[0], orgKind, clientInfo));
                                        }
                                    }
                                }
                            }
                            else if (sk == SendToKind.Manager)
                            {
                                if (flInstance.FLDirection == FLDirection.GoToBack)
                                {
                                    // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                                    roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(flInstance.R))
                                    {
                                        // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                                        roleIds.Add(Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo));
                                    }
                                    else
                                    {
                                        roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                                    }
                                }
                            }
                            else if (sk == SendToKind.RefRole)
                            {
                                if (nextFLActivity is FLStandActivity && ((ISupportFLDetailsActivity)nextFLActivity).SendToId2 != string.Empty)
                                {
                                    // roleId = ((ISupportFLDetailsActivity)nextFLActivity).SendToId2;
                                    roleIds.Add(((ISupportFLDetailsActivity)nextFLActivity).SendToId2);
                                }
                                else
                                {
                                    if (nextFLActivity is FLNotifyActivity)
                                    {
                                        var roles = Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo, true);
                                        roleIds.AddRange(roles.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                                    }
                                    else
                                    {
                                        // roleId = Global.GetRoleIdByRefRole(sf, tableName, keyValues[1].ToString(), clientInfo);
                                        roleIds.Add(Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo));
                                    }

                                }
                            }
                            else if (sk == SendToKind.RefManager)
                            {
                                if (flInstance.FLDirection == FLDirection.GoToBack)
                                {
                                    // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                                    roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                                }
                                else
                                {
                                    if (nextFLActivity is IFLApproveBranchActivity && !string.IsNullOrEmpty(flInstance.R))
                                    {
                                        // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                                        roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                                    }
                                    else
                                    {
                                        string sendToField = sf;
                                        string values = keyValues[1].ToString();

                                        string s = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                                        // roleId = Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo);
                                        roleIds.Add(Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo));
                                    }
                                }
                            }
                            else if (sk == SendToKind.RefUser)
                            {
                                string id = Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo, true);

                                if (!string.IsNullOrEmpty(id))
                                {
                                    string[] listusers = id.Split(';');
                                    foreach (string user in listusers)
                                    {
                                        if (user.Trim().Length > 0)
                                        {
                                            userIds.Add(user);
                                        }
                                    }
                                }
                            }
                            else if (sk == SendToKind.User)
                            {
                                string[] listusers = su.Split(';');
                                foreach (string user in listusers)
                                {
                                    if (user.Trim().Length > 0)
                                    {
                                        userIds.Add(user);
                                    }
                                }
                            }

                            else if (flActivity is IFLNotifyActivity & sk == SendToKind.AllRoles)
                            {
                                foreach (string r in flInstance.RL)
                                {
                                    if (string.IsNullOrEmpty(r))
                                        continue;

                                    string[] rr = r.Split(":".ToCharArray());
                                    if (rr[0] == "R")
                                    {
                                        roleIds.Add(rr[1]);
                                    }
                                    else
                                    {
                                        roleIds.Add(r);
                                    }

                                    roleIds.Add(string.Format("U:{0}", flInstance.Creator));
                                }
                            }
                            else if (flActivity is IFLNotifyActivity & sk == SendToKind.LastUser)
                            {
                                if (flInstance.RL.Count > 0)
                                {
                                    for (int i = flInstance.RL.Count - 1; i >= 0; i--)
                                    {
                                        string r = flInstance.RL[i];
                                        if (string.IsNullOrEmpty(r))
                                            continue;

                                        string[] rr = r.Split(":".ToCharArray());
                                        if (rr[0] == "R")
                                        {
                                            roleIds.Add(rr[1]);
                                        }
                                        else
                                        {
                                            roleIds.Add(r);
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    roleIds.Add(string.Format("U:{0}", flInstance.Creator));
                                }

                            }
                            else
                            {
                                // roleId = flInstanceParms[5].ToString();
                                roleIds.Add(flInstanceParms[5].ToString());
                            }
                        }
                    }

 
                    //if (roleId != null && roleId != string.Empty)
                    //{
                    //    userIds = Global.GetUsersIdsByRoleId(roleId, clientInfo);
                    //}
                    List<string> tempIds = new List<string>();
                    var mailApproveLevel = ((IFLRootActivity)flInstance.RootFLActivity).MailApproveLevel;
                    var buttonEnable = string.IsNullOrEmpty(mailApproveLevel);
                  
                    foreach (string r in roleIds)
                    {
                        string[] rr = r.Split(":".ToCharArray());
                        if (rr.Length > 1)
                        {
                            tempIds.Add(rr[1]);
                        }
                        else
                        {
                            List<string> userofrole = Global.GetUsersIdsByRoleId(r, clientInfo);
                            if (userofrole.Count > 0)
                            {
                             
                                if (!string.IsNullOrEmpty(mailApproveLevel))
                                {
                                    var levelNo = Global.GetLevelNo(r, ((IFLRootActivity)flInstance.RootFLActivity).OrgKind, clientInfo);
                                    if (string.Compare(levelNo, mailApproveLevel) >= 0)
                                    {
                                        buttonEnable = true;
                                    }
                                }

                                tempIds.AddRange(Global.GetAgentUsers(r, userofrole, flInstance.RootFLActivity.Description, clientInfo));
                                //string agent = Global.GetAgent(r, userofrole[0], flInstance.RootFLActivity.Description, clientInfo);
                                //if (!string.IsNullOrEmpty(agent))
                                //{
                                //    object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                                //    if (parAgent != null && Convert.ToBoolean(parAgent))
                                //    {
                                //        tempIds.AddRange(userofrole);
                                //    }
                                //    tempIds.Add(agent);
                                //}
                                //else
                                //{
                                //    tempIds.AddRange(userofrole);
                                //}
                            }
                        }
                    }
                    foreach (string u in tempIds)
                    {
                        if (userIds.Contains(u))
                            continue;

                        userIds.Add(u);
                    }






                    Dictionary<string, string> userEmails = new Dictionary<string, string>();

                    foreach (string userId in userIds)
                    {
                        if (users.Length != 0)
                        {
                            users += ",";
                        }
                        users += userId;

                        email = Global.GetUserEmail(userId, clientInfo);
                        if (email != null && email != string.Empty)
                        {
                            AddEmail(sendToes, email);
                            userEmails[userId] = email;
                        }
                    }


                    if (sendToes.Count == 0)
                    {
                        continue;
                    }
                    string body = string.Empty;

                    if(setting.BodyActivityDescription)
                    {
                        body += "<span>" + nextFLActivity.Description + "</span>";
                    }

                    body += "<TABLE BORDER=1>";

                    if (setting.BodySender)
                    {
                        body += "<TR>";
                        body += "<TD WIDTH=150>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _sender);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += string.Format("[{0}]{1}", sUserId, sUserName);
                        body += "</TD>";
                        body += "</TR>";
                    }

                    if (setting.BodyFlowName)
                    {
                        body += "<TR>";
                        body += "<TD>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _flowName);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += string.IsNullOrEmpty(flowDesc) ? "&nbsp;" : flowDesc;
                        body += "</TD>";
                        body += "</TR>";
                    }

                    if (setting.BodyActivityName)
                    {
                        body += "<TR>";
                        body += "<TD>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _activityName);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += nextFLActivity.Name;
                        body += "</TD>";
                        body += "</TR>";
                    }

                    string presenationCT = null;
                    if (setting.BodyContent)
                    {
                        string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                        string keys = keyValues[0].ToString();
                        string presenation = keyValues[1].ToString();
                        presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

                        body += GetPresentation(presenationCT);
                        body += GetBodyHtml(flInstance, keyValues, clientInfo);
                    }
                    string remark = string.Empty;
                    if (flInstance.FLFlag == 'X')
                    {
                        remark = (flActivity is FLNotifyActivity && (flActivity as FLNotifyActivity).Parameters == "Reject") ? "Reject" : "Reject(system)";
                    }
                    else
                    {
                        remark = flInstanceParms[4].ToString();
                    }

                    //string remark = flInstance.FLFlag == 'X' ? "Reject(system)" : flInstanceParms[4].ToString();
                    remark = InitRemark(remark);
                    if (setting.BodyDescription)
                    {
                        
                        //added by lily 2009-5-11 如果審核意見中有換行會報錯
                        body += "<TR>";
                        body += "<TD>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _description);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += string.IsNullOrEmpty(remark) ? "&nbsp;" : remark;
                        body += "</TD>";
                        body += "</TR>";
                    }

                    if (setting.BodyDatetime)
                    {
                        body += "<TR>";
                        body += "<TD>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _dateTime);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += DateTime.Now.ToString();
                        body += "</TD>";
                        body += "</TR>";
                    }

                    if (setting.BodyHyperlink)
                    {
                        string webUrl = flInstance.GetWebUrl();
                        if (webUrl != null && webUrl != "0" && !string.IsNullOrEmpty(wfn))
                        {
                            if (webUrl.IndexOf("/MainPage_Flow.aspx") > 0)
                            {
                                //在下面替换
                            }
                            else
                            {
                                List<object> objs = new List<object>();
                                if (wfn.IndexOf(".web", StringComparison.OrdinalIgnoreCase) == 0 && wfn.Length > 4)
                                {
                                    wfn = wfn.Substring(4);
                                }
                                string[] webFormNames = wfn.Split('.');
                                if (webFormNames.Length >= 2)
                                {
                                    objs.Add(HttpUtility.UrlEncode(webFormNames[0]));
                                    objs.Add(HttpUtility.UrlEncode(webFormNames[1]) + ".aspx");
                                    objs.Add(HttpUtility.UrlEncode(flInstance.FLInstanceId.ToString()));
                                    objs.Add(HttpUtility.UrlEncode((flInstance.CurrentFLActivity == null ? string.Empty : flInstance.CurrentFLActivity.Name) + ";" + nextFLActivity.Name));
                                    objs.Add(HttpUtility.UrlEncode(keyValues[1].ToString()));
                                    objs.Add(HttpUtility.UrlEncode(flInstance.IsPlusApprove ? "0" : ((int)nm).ToString()));
                                    objs.Add(HttpUtility.UrlEncode(flInstance.IsPlusApprove ? "7" : ((int)fnm).ToString()));
                                    objs.Add(HttpUtility.UrlEncode(users));
                                    objs.Add(plusApprove);
                                    objs.Add(flInstance.IsPlusApprove ? "A" : "N");
                                    objs.Add(roleIds.Count > 0 ? roleIds[0] : string.Empty);
                                    objs.Add(GetMultiStepReturn(flInstance, nextFLActivity) ? "1" : "0");
                                    //objs.Add(flInstanceParms.Length >= 10 ? HttpUtility.UrlEncode(flInstanceParms[9].ToString()): string.Empty);
                                    objs.Add(string.Empty);
                                    if (webUrl != null && webUrl != string.Empty)
                                    {
                                        webUrl = string.Format(webUrl, objs.ToArray());

                                        if(setting.AutoLogin)
                                        {
                                            webUrl += "&AutoLogin=true";
                                        }
                                    }
                                }
                            }

                            body += "<TR>";
                            body += "<TD>";
                            body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink);
                            body += "</TD>";
                            body += "<TD  COLSPAN=7>";
                            body += "<a href='" + webUrl + "' target='_blank'>" + SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink2) + "</a>";
                            body += "</TD>";
                            body += "</TR>";
                        }
                    }

                    if (setting.BodyComment)
                    {
                        body += GetComments(flInstance.FLInstanceId.ToString(), clientInfo);
                    }

                    body += "</TABLE>";

                    //---------------------------------------------------------------

                    var subjectHtml = GetSubjectHtml(flInstance, flActivity, clientInfo);
                    string subject = subjectHtml;

                    if (setting.SubjectFlowName)
                    {
                        subject += flowDesc + "-";
                    }

                    if (setting.SubjectActivityName)
                    {
                        subject += nextFLActivity.Name;
                    }

                    if (setting.SubjectDescription)
                    {
                        if (subject != subjectHtml)
                        {
                            subject += ",";
                        }

                        subject += remark;
                    }

                    if (setting.SubjectSender)
                    {
                        if (subject != subjectHtml)
                        {
                            subject += ",";
                        }

                        subject += sUserId;
                        subject += "(" + sUserName + ")";
                    }

                    if (setting.SubjectContent)
                    {
                        if (subject != subjectHtml)
                        {
                            subject += ",";
                        }
                        if (string.IsNullOrEmpty(presenationCT))
                        {
                            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                            string keys = keyValues[0].ToString();
                            string presenation = keyValues[1].ToString();
                            presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);
                        }
                        subject += presenationCT;
                    }

                    SendPush(userIds, flowDesc, nextFLActivity.Name, remark, string.Format("[{0}]{1}", sUserId, sUserName), presenationCT, flInstance.FLInstanceId.ToString(), (flInstance.CurrentFLActivity == null ? string.Empty : flInstance.CurrentFLActivity.Name) + ";" + nextFLActivity.Name);

                    if (subject.Length == 0 && body.Length == 0)
                    {
                        return;
                    }

                   

                    foreach (var userEmail in userEmails)
                    {
                        var client = CreateSmtpClient(smtp, sendFrom, password, enableSSL, port);
                        
                        MailMessage message = new MailMessage();
                        message.SubjectEncoding = Encoding.UTF8;
                        message.BodyEncoding = Encoding.UTF8;
                        message.From = new MailAddress(sendFrom, "Workflow", System.Text.Encoding.UTF8);
                        message.To.Add(new MailAddress(userEmail.Value, userEmail.Value, Encoding.UTF8));

                        //添加button，用户分开发送
                        message.IsBodyHtml = true;
                        message.Subject = subject.Replace("\r", "").Replace("\n", " ");
                        if (nextFLActivity is IEventWaiting)
                        {
                            var mode = fnm;
                            if (flInstance.IsPlusApprove)
                            {
                                mode = FLNavigatorMode.Notify;//无法定位就不报错了
                            }

                            message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IEventWaiting, mode));
                            if (buttonEnable)
                            {
                                message.Body += GetButtons(flInstance, clientInfo, setting, userEmail.Key, nextFLActivity as IEventWaiting);
                            }
                        }
                        else
                        {
                            message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IFLBaseActivity, fnm));
                        }
                        //_message = message;

                        Thread thread = new Thread(new ParameterizedThreadStart(SendMail));
                        DateTime d1 = DateTime.Now;
                        thread.Start(new object[] { client, message });
                        DateTime d2 = DateTime.Now;
                        //Thread.Sleep(200);
                    }                    

                    //Console.Write("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX:{0}", ((TimeSpan)(d2 - d1)).TotalMilliseconds);

                    //try
                    //{
                    //    //DateTime d1 = DateTime.Now;
                    //    //client.Send(message);
                    //    //DateTime d2 = DateTime.Now;

                    //    //Console.Write("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX:{0}", ((TimeSpan)(d2 - d1)).TotalMilliseconds);
                    //}
                    //catch
                    //{

                    //}
                }
            }
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主筛选条件</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="flActivity">Activity</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void SendTo2(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, string roleId, FLActivity flActivity, object[] clientInfo)
        {
            List<string> sendToes = new List<string>();

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);

            var setting = EmailSetting.LoadSetting(clientInfo);

            var active = setting.Activie;
            if (!active)
            {
                return;
            }

            var sendFrom = setting.SendFrom;
            var password = setting.Password;
            var smtp = setting.SMTP;
            var enableSSL = setting.EnableSSL;
            var port = setting.Port;


            if (!(flActivity is IEventWaiting) && !(flActivity is IFLNotifyActivity))
            {
                return;
            }

            bool sm = false;
            if (flActivity is IEventWaiting)
            {
                sm = ((IEventWaiting)flActivity).SendEmail;
            }
            else if (flActivity is IFLNotifyActivity)
            {
                sm = ((IFLNotifyActivity)flActivity).SendEmail;
            }

            if (sm)
            {
                FLActivity nextFLActivity = flActivity;
                int plusApprove = 0;
                if (nextFLActivity is IFLStandActivity)
                {
                    plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity).PlusApprove);
                }
                else if (nextFLActivity is IFLApproveActivity)
                {
                    plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity).PlusApprove);
                }
                else if (nextFLActivity is IFLApproveBranchActivity)
                {
                    FLActivity approve = flInstance.RootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)nextFLActivity).ParentActivity);
                    plusApprove = Convert.ToInt32(((IFLApproveActivity)approve).PlusApprove);
                }

                string users = string.Empty;
                string email = string.Empty;
                List<string> roleIds = new List<string>();
                List<string> userIds = new List<string>();
                SendToKind sk = SendToKind.Applicate;
                string sr = string.Empty;
                string su = string.Empty;
                string sf = string.Empty;
                string fn = string.Empty;
                string wfn = string.Empty;
                NavigatorMode nm = NavigatorMode.Normal;
                FLNavigatorMode fnm = FLNavigatorMode.Notify;
                if (flActivity is IEventWaiting)
                {
                    sk = ((IEventWaiting)flActivity).SendToKind;
                    sr = ((IEventWaiting)flActivity).SendToRole;
                    su = ((IEventWaiting)flActivity).SendToUser;
                    sf = ((IEventWaiting)flActivity).SendToField;
                    wfn = string.IsNullOrEmpty(((IEventWaiting)flActivity).WebFormName)
                        ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                        : ((IEventWaiting)flActivity).WebFormName;
                    nm = ((IEventWaiting)flActivity).NavigatorMode;
                    fnm = ((IEventWaiting)flActivity).FLNavigatorMode;
                }
                else if (flActivity is IFLNotifyActivity)
                {
                    sk = ((IFLNotifyActivity)flActivity).SendToKind;
                    sr = ((IFLNotifyActivity)flActivity).SendToRole;
                    su = ((IFLNotifyActivity)flActivity).SendToUser;
                    sf = ((IFLNotifyActivity)flActivity).SendToField;
                    wfn = string.IsNullOrEmpty(((IFLNotifyActivity)flActivity).WebFormName)
                                 ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                                 : ((IFLNotifyActivity)flActivity).WebFormName;
                    nm = ((IFLNotifyActivity)flActivity).NavigatorMode;
                    fnm = ((IFLNotifyActivity)flActivity).FLNavigatorMode;
                }
                //ccm   2012/11/07-------------------------------------------------------------------------------------------------------------------------------------
                if (flInstance.IsPlusApprove)
                {
                    string q = flInstanceParms[8].ToString();
                    string[] qq = q.Split(";".ToCharArray());
                    foreach (string r in qq)
                    {
                        if (!string.IsNullOrEmpty(r))
                        {
                            if (r.StartsWith("U:"))
                            {
                                userIds.Add(r.Substring(2));
                            }
                            else
                            {
                                roleIds.Add(r);
                            }
                        }
                    }
                }
                //-------------------------------------------------------------------------------------------------------------------------------------
                else if (sk == SendToKind.Applicate)
                {
                    userIds.Add(flInstance.Creator);
                }
                else
                {
                    string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;
                    if (sk == SendToKind.Role)
                    {
                        string q = sr;
                        string[] qq = q.Split(";".ToCharArray());

                        //roleId = qq[0].Trim();
                        roleIds.Add(qq[0].Trim());
                    }
                    else if (sk == SendToKind.ApplicateManager)
                    {
                        if (!string.IsNullOrEmpty(flInstance.CreateRole))
                        {
                            //roleId = Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo);
                            roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(flInstance.CreateRole))
                            {
                                roleIds.Add(Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo));
                            }
                            else
                            {
                                List<string> roles = Global.GetRoleIdsByUserId(flInstance.Creator, clientInfo);
                                if (roles.Count > 0)
                                {
                                    roleIds.Add(Global.GetManagerRoleId(roles[0], orgKind, clientInfo));
                                }
                            }
                        }
                    }
                    else if (sk == SendToKind.Manager)
                    {
                        roleIds.Add(flInstanceParms[5].ToString());


                        //if (flInstance.FLDirection == FLDirection.GoToBack)
                        //{
                        //    // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                        //    roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                        //}
                        //else
                        //{
                        //    if (string.IsNullOrEmpty(flInstance.R))
                        //    {
                        //        // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                        //        roleIds.Add(Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo));
                        //    }
                        //    else
                        //    {
                        //        roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                        //    }
                        //}
                    }
                    else if (sk == SendToKind.RefRole)
                    {
                        if (nextFLActivity is FLStandActivity && ((ISupportFLDetailsActivity)nextFLActivity).SendToId2 != string.Empty)
                        {
                            // roleId = ((ISupportFLDetailsActivity)nextFLActivity).SendToId2;
                            roleIds.Add(((ISupportFLDetailsActivity)nextFLActivity).SendToId2);
                        }
                        else
                        {
                            // roleId = Global.GetRoleIdByRefRole(sf, tableName, keyValues[1].ToString(), clientInfo);
                            roleIds.Add(Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo));
                        }
                    }
                    else if (sk == SendToKind.RefManager)
                    {
                        if (flInstance.FLDirection == FLDirection.GoToBack)
                        {
                            // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                            roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                        }
                        else
                        {
                            if (nextFLActivity is IFLApproveBranchActivity && !string.IsNullOrEmpty(flInstance.R))
                            {
                                // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                                roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                            }
                            else
                            {
                                string sendToField = sf;
                                string values = keyValues[1].ToString();

                                string s = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                                // roleId = Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo);
                                roleIds.Add(Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo));
                            }
                        }
                    }
                    else if (sk == SendToKind.RefUser)
                    {
                        string id = Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo, true);

                        if (!string.IsNullOrEmpty(id))
                        {
                            string[] listusers = id.Split(';');
                            foreach (string user in listusers)
                            {
                                if (user.Trim().Length > 0)
                                {
                                    userIds.Add(user);
                                }
                            }
                        }
                    }
                    else if (sk == SendToKind.User)
                    {
                        string[] listusers = su.Split(';');
                        foreach (string user in listusers)
                        {
                            if (user.Trim().Length > 0)
                            {
                                userIds.Add(user);
                            }
                        }
                    }
                    else
                    {
                        //roleId = flInstanceParms[5].ToString();
                        roleIds.Add(flInstanceParms[5].ToString());
                    }
                }
                List<string> tempIds = new List<string>();
                var mailApproveLevel = ((IFLRootActivity)flInstance.RootFLActivity).MailApproveLevel;
                var buttonEnable = string.IsNullOrEmpty(mailApproveLevel);
                foreach (string r in roleIds)
                {
                    string[] rr = r.Split(":".ToCharArray());
                    if (rr.Length > 1)
                    {
                        tempIds.Add(rr[1]);
                    }
                    else
                    {
                        List<string> userofrole = Global.GetUsersIdsByRoleId(r, clientInfo);
                        if (userofrole.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(mailApproveLevel))
                            {
                                var levelNo = Global.GetLevelNo(r, ((IFLRootActivity)flInstance.RootFLActivity).OrgKind, clientInfo);
                                if (string.Compare(levelNo, mailApproveLevel) >= 0)
                                {
                                    buttonEnable = true;
                                }
                            }
                            tempIds.AddRange(Global.GetAgentUsers(r, userofrole, flInstance.RootFLActivity.Description, clientInfo));
                            //string agent = Global.GetAgent(r, userofrole[0], flInstance.RootFLActivity.Description, clientInfo);
                            //if (!string.IsNullOrEmpty(agent))
                            //{
                            //    object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                            //    if (parAgent != null && Convert.ToBoolean(parAgent))
                            //    {
                            //        tempIds.AddRange(userofrole);
                            //    }
                            //    tempIds.Add(agent);
                            //}
                            //else
                            //{
                            //    tempIds.AddRange(userofrole);
                            //}
                        }
                    }
                }
                foreach (string u in tempIds)
                {
                    if (userIds.Contains(u))
                        continue;

                    userIds.Add(u);
                }
                Dictionary<string, string> userEmails = new Dictionary<string, string>();
                foreach (string userId in userIds)
                {
                    if (users.Length != 0)
                    {
                        users += ",";
                    }
                    users += userId;

                    email = Global.GetUserEmail(userId, clientInfo);
                    if (email != null && email != string.Empty)
                    {
                        AddEmail(sendToes, email);
                        userEmails[userId] = email;
                    }
                }

                if (sendToes.Count == 0)
                {
                    return;
                }

                string body = string.Empty;

                if(setting.BodyActivityDescription)
                {
                    body += "<span>" + nextFLActivity.Description + "</span>";
                }


                body += "<TABLE BORDER=1>";

                if (setting.BodySender)
                {
                    body += "<TR>";
                    body += "<TD WIDTH=150>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _sender);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += string.Format("[{0}]{1}", sUserId, sUserName);
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyFlowName)
                {
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _flowName);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += string.IsNullOrEmpty(flowDesc) ? "&nbsp;" : flowDesc;
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyActivityName)
                {
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _activityName);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += nextFLActivity.Name;
                    body += "</TD>";
                    body += "</TR>";
                }

                string presenationCT = null;
                if (setting.BodyContent)
                {
                    string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                    string keys = keyValues[0].ToString();
                    string presenation = keyValues[1].ToString();
                    presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

                    body += GetPresentation(presenationCT);
                    body += GetBodyHtml(flInstance, keyValues, clientInfo);
                }
                string remark = flInstanceParms[4].ToString();
                //意見內容有換行會報錯
                remark = InitRemark(remark);
                if (setting.BodyDescription)
                {
                   
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _description);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += string.IsNullOrEmpty(remark) ? "&nbsp;" : remark;
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyDatetime)
                {
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _dateTime);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += DateTime.Now.ToString();
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyHyperlink)
                {
                    string webUrl = flInstance.GetWebUrl();
                    if (webUrl != null && webUrl != "0" && !string.IsNullOrEmpty(wfn))
                    {
                        if (webUrl.IndexOf("/MainPage_Flow.aspx") > 0)
                        {
                            //在下面替换
                        }
                        else
                        {
                            List<object> objs = new List<object>();
                            if (wfn.IndexOf(".web", StringComparison.OrdinalIgnoreCase) == 0 && wfn.Length > 4)
                            {
                                wfn = wfn.Substring(4);
                            }
                            string[] webFormNames = wfn.Split('.');
                            if (webFormNames.Length >= 2)
                            {
                                objs.Add(HttpUtility.UrlEncode(webFormNames[0]));
                                objs.Add(HttpUtility.UrlEncode(webFormNames[1]) + ".aspx");
                                objs.Add(HttpUtility.UrlEncode(flInstance.FLInstanceId.ToString()));
                                objs.Add(HttpUtility.UrlEncode((flInstance.CurrentFLActivity == null ? string.Empty : flInstance.CurrentFLActivity.Name) + ";" + nextFLActivity.Name));
                                objs.Add(HttpUtility.UrlEncode(keyValues[1].ToString()));
                                objs.Add(HttpUtility.UrlEncode(((int)nm).ToString()));
                                objs.Add(HttpUtility.UrlEncode(((int)fnm).ToString()));
                                objs.Add(HttpUtility.UrlEncode(users));
                                objs.Add(plusApprove);
                                objs.Add(plusApprove == 1 ? "A" : "");
                                objs.Add(roleId);
                                objs.Add(GetMultiStepReturn(flInstance, nextFLActivity) ? "1" : "0");
                                objs.Add(flInstanceParms.Length >= 10 ? HttpUtility.UrlEncode(flInstanceParms[9].ToString()) : string.Empty);

                                if (webUrl != null && webUrl != string.Empty)
                                {
                                    webUrl = string.Format(webUrl, objs.ToArray());

                                    if (setting.AutoLogin)
                                    {
                                        webUrl += "&AutoLogin=true";
                                    }
                                }
                            }
                        }

                        body += "<TR>";
                        body += "<TD>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += "<a href='" + webUrl + "' target='_blank'>" + SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink2) + "</a>";
                        body += "</TD>";
                        body += "</TR>";
                    }
                }

                if (setting.BodyComment)
                {
                    body += GetComments(flInstance.FLInstanceId.ToString(), clientInfo);
                }

                body += "</TABLE>";

                //---------------------------------------------------------------
                var subjectHtml =SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _overTime);
                string subject = subjectHtml;
               

                if (setting.SubjectFlowName)
                {
                    subject += flowDesc + "-";
                }

                if (setting.SubjectActivityName)
                {
                    subject += nextFLActivity.Name;
                }

                if (setting.SubjectDescription)
                {
                   
                    if (subject != subjectHtml)
                    {
                        subject += ",";
                    }

                    subject += remark;
                }

                if (setting.SubjectSender)
                {
                    if (subject != subjectHtml)
                    {
                        subject += ",";
                    }

                    subject += sUserId;
                    subject += "(" + sUserName + ")";
                }

                if (setting.SubjectContent)
                {
                    if (subject != subjectHtml)
                    {
                        subject += ",";
                    }
                    if (string.IsNullOrEmpty(presenationCT))
                    {
                        string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                        string keys = keyValues[0].ToString();
                        string presenation = keyValues[1].ToString();
                        presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);
                    }
                    subject += presenationCT;
                }

                SendPush(userIds, flowDesc, nextFLActivity.Name, remark, string.Format("[{0}]{1}", sUserId, sUserName), presenationCT, null, null);

                if (subject.Length == 0 && body.Length == 0)
                {
                    return;
                }      

                foreach (var userEmail in userEmails)
                {
                    var client = CreateSmtpClient(smtp, sendFrom, password, enableSSL, port);

                    MailMessage message = new MailMessage();
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Priority = MailPriority.High;
                    message.BodyEncoding = Encoding.UTF8;
                    message.From = new MailAddress(sendFrom, "Workflow", System.Text.Encoding.UTF8);
                    message.To.Add(new MailAddress(userEmail.Value, userEmail.Value, Encoding.UTF8));
                    message.IsBodyHtml = true;
                    message.Subject = subject.Replace("\r", "").Replace("\n", " ");
                    if (nextFLActivity is IEventWaiting)
                    {
                        message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IEventWaiting, fnm));
                        if (buttonEnable)
                        {
                            message.Body += GetButtons(flInstance, clientInfo, setting, userEmail.Key, nextFLActivity as IEventWaiting);
                        }
                    }
                    else
                    {
                        message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IFLBaseActivity, fnm));
                    }
                    //_message = message;

                    Thread thread = new Thread(new ParameterizedThreadStart(SendMail));
                    DateTime d1 = DateTime.Now;
                    thread.Start(new object[] { client, message });
                    DateTime d2 = DateTime.Now;
                    //Thread.Sleep(200);
                }
            }
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主筛选条件</param>
        /// <param name="roleId">角色Id</param>
        /// <param name="flActivity">Activity</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void SendTo3(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, FLActivity flActivity, object[] clientInfo)
        {
           
            List<string> sendToes = new List<string>();

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);

            var setting = EmailSetting.LoadSetting(clientInfo);

            var active = setting.Activie;
            if (!active)
            {
                return;
            }

            var sendFrom = setting.SendFrom;
            var password = setting.Password;
            var smtp = setting.SMTP;
            var enableSSL = setting.EnableSSL;
            var port = setting.Port;

            FLActivity nextFLActivity = flActivity;
            int plusApprove = 0;
            if (nextFLActivity is IFLStandActivity)
            {
                plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity).PlusApprove);
            }
            else if (nextFLActivity is IFLApproveActivity)
            {
                plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity).PlusApprove);
            }
            else if (nextFLActivity is IFLApproveBranchActivity)
            {
                FLActivity approve = flInstance.RootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)nextFLActivity).ParentActivity);
                plusApprove = Convert.ToInt32(((IFLApproveActivity)approve).PlusApprove);
            }

            string users = string.Empty;
            string email = string.Empty;
            SendToKind sk = SendToKind.Applicate;
            string sr = string.Empty;
            string sf = string.Empty;
            string fn = string.Empty;
            string wfn = string.Empty;
            NavigatorMode nm = NavigatorMode.Normal;
            FLNavigatorMode fnm = FLNavigatorMode.Notify;
            if (flActivity is IEventWaiting)
            {
                sk = ((IEventWaiting)flActivity).SendToKind;
                sr = ((IEventWaiting)flActivity).SendToRole;
                sf = ((IEventWaiting)flActivity).SendToField;
                wfn = string.IsNullOrEmpty(((IEventWaiting)flActivity).WebFormName)
                    ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                    : ((IEventWaiting)flActivity).WebFormName;
                nm = ((IEventWaiting)flActivity).NavigatorMode;
                //fnm = ((IEventWaiting)flActivity).FLNavigatorMode;
            }
            else if (flActivity is IFLNotifyActivity)
            {
                sk = ((IFLNotifyActivity)flActivity).SendToKind;
                sr = ((IFLNotifyActivity)flActivity).SendToRole;
                sf = ((IFLNotifyActivity)flActivity).SendToField;
                wfn = string.IsNullOrEmpty(((IFLNotifyActivity)flActivity).WebFormName)
                             ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                             : ((IFLNotifyActivity)flActivity).WebFormName;
                nm = ((IFLNotifyActivity)flActivity).NavigatorMode;
                //fnm = ((IFLNotifyActivity)flActivity).FLNavigatorMode;
            }

            List<string> userIds = new List<string>();

            string s = flInstanceParms[8].ToString();
            string[] ss = s.Split(';');
            foreach (string id in ss)
            {
                if (string.IsNullOrEmpty(id))
                    continue;

                string[] ss1 = id.Split(':');
                if (ss1.Length <= 1)
                {
                    List<string> userofrole = Global.GetUsersIdsByRoleId(ss1[0], clientInfo);
                    if (userofrole.Count > 0)
                    {
                        userIds.AddRange(Global.GetAgentUsers(ss1[0], userofrole, flInstance.RootFLActivity.Description, clientInfo));
                        //string agent = Global.GetAgent(ss1[0], userofrole[0], flInstance.RootFLActivity.Description, clientInfo);
                        //if (!string.IsNullOrEmpty(agent))
                        //{
                        //    object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                        //    if (parAgent != null && Convert.ToBoolean(parAgent))
                        //    {
                        //        userIds.AddRange(userofrole);
                        //    }
                        //    userIds.Add(agent);
                        //}
                        //else
                        //{
                        //    userIds.AddRange(userofrole);
                        //}
                    }
                }
                else
                {
                    userIds.Add(ss1[0]);
                }
            }
            Dictionary<string, string> userEmails = new Dictionary<string, string>();
            foreach (string userId in userIds)
            {
                if (users.Length != 0)
                {
                    users += ",";
                }
                users += userId;

                email = Global.GetUserEmail(userId, clientInfo);
                if (email != null && email != string.Empty)
                {
                    AddEmail(sendToes, email);
                    userEmails[userId] = email;
                }
            }

            if (sendToes.Count == 0)
            {
                return;
            }
            string body = string.Empty;
            if (setting.BodyActivityDescription)
            {
                body += "<span>" + nextFLActivity.Description + "</span>";
            }

            body += "<TABLE BORDER=1>";

            if (setting.BodySender)
            {
                body += "<TR>";
                body += "<TD WIDTH=150>";
                body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _sender);
                body += "</TD>";
                body += "<TD  COLSPAN=7>";
                body += string.Format("[{0}]{1}", sUserId, sUserName);
                body += "</TD>";
                body += "</TR>";
            }

            if (setting.BodyFlowName)
            {
                body += "<TR>";
                body += "<TD>";
                body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _flowName);
                body += "</TD>";
                body += "<TD  COLSPAN=7>";
                body += string.IsNullOrEmpty(flowDesc) ? "&nbsp;" : flowDesc;
                body += "</TD>";
                body += "</TR>";
            }

            if (setting.BodyActivityName)
            {
                body += "<TR>";
                body += "<TD>";
                body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _activityName);
                body += "</TD>";
                body += "<TD  COLSPAN=7>";
                body += nextFLActivity.Name;
                body += "</TD>";
                body += "</TR>";
            }

            string presenationCT = null;
            if (setting.BodyContent)
            {
                string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                string keys = keyValues[0].ToString();
                string presenation = keyValues[1].ToString();
                presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

                body += GetPresentation(presenationCT);
                body += GetBodyHtml(flInstance, keyValues, clientInfo);
            }

            string remark = flInstance.IsReturn == true ? "Return(system)" : (flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString());
            //意見內容有換行會報錯
            remark = InitRemark(remark);
            if (setting.BodyDescription)
            {
              
                body += "<TR>";
                body += "<TD>";
                body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _description);
                body += "</TD>";
                body += "<TD  COLSPAN=7>";
                body += string.IsNullOrEmpty(remark) ? "&nbsp;" : remark;
                body += "</TD>";
                body += "</TR>";
            }

            if (setting.BodyDatetime)
            {
                body += "<TR>";
                body += "<TD>";
                body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _dateTime);
                body += "</TD>";
                body += "<TD  COLSPAN=7>";
                body += DateTime.Now.ToString();
                body += "</TD>";
                body += "</TR>";
            }

            if (setting.BodyHyperlink)
            {
                string webUrl = flInstance.GetWebUrl();
                if (webUrl != null && webUrl != "0" && !string.IsNullOrEmpty(wfn))
                {
                    if (webUrl.IndexOf("/MainPage_Flow.aspx") > 0)
                    {
                        //在下面替换
                    }
                    else
                    {
                        List<object> objs = new List<object>();
                        if (wfn.IndexOf(".web", StringComparison.OrdinalIgnoreCase) == 0 && wfn.Length > 4)
                        {
                            wfn = wfn.Substring(4);
                        }
                        string[] webFormNames = wfn.Split('.');
                        if (webFormNames.Length >= 2)
                        {
                            objs.Add(HttpUtility.UrlEncode(webFormNames[0]));
                            objs.Add(HttpUtility.UrlEncode(webFormNames[1]) + ".aspx");
                            objs.Add(HttpUtility.UrlEncode(flInstance.FLInstanceId.ToString()));
                            objs.Add(HttpUtility.UrlEncode((flInstance.CurrentFLActivity == null ? string.Empty : flInstance.CurrentFLActivity.Name) + ";" + nextFLActivity.Name));
                            objs.Add(HttpUtility.UrlEncode(keyValues[1].ToString()));
                            objs.Add(HttpUtility.UrlEncode(((int)nm).ToString()));
                            objs.Add(HttpUtility.UrlEncode(((int)fnm).ToString()));
                            objs.Add(HttpUtility.UrlEncode(users));
                            objs.Add(plusApprove);
                            objs.Add(plusApprove == 1 ? "A" : "");
                            objs.Add(string.Empty);//roleid
                            objs.Add("0");
                            objs.Add(flInstanceParms.Length >= 10 ? HttpUtility.UrlEncode(flInstanceParms[9].ToString()) : string.Empty);

                            if (webUrl != null && webUrl != string.Empty)
                            {
                                webUrl = string.Format(webUrl, objs.ToArray());

                                if (setting.AutoLogin)
                                {
                                    webUrl += "&AutoLogin=true";
                                }
                            }
                        }
                    }

                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += "<a href='" + webUrl + "' target='_blank'>" + SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink2) + "</a>";
                    body += "</TD>";
                    body += "</TR>";
                }
            }

            if (setting.BodyComment)
            {
                body += GetComments(flInstance.FLInstanceId.ToString(), clientInfo);
            }

            body += "</TABLE>";

            //---------------------------------------------------------------
            var subjectHtml = GetSubjectHtml(flInstance, new FLNotifyActivity(), clientInfo);
            string subject = subjectHtml;
            if (setting.SubjectFlowName)
            {
                subject += flowDesc + "-";
            }

            if (setting.SubjectActivityName)
            {
                subject += nextFLActivity.Name;
            }

            if (setting.SubjectDescription)
            {
                
                if (subject != subjectHtml)
                {
                    subject += ",";
                }

                subject += remark;
            }

            if (setting.SubjectSender)
            {
                if (subject != subjectHtml)
                {
                    subject += ",";
                }

                subject += sUserId;
                subject += "(" + sUserName + ")";
            }

            if (setting.SubjectContent)
            {
                if (subject != subjectHtml)
                {
                    subject += ",";
                }
                if (string.IsNullOrEmpty(presenationCT))
                {
                    string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                    string keys = keyValues[0].ToString();
                    string presenation = keyValues[1].ToString();
                    presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);
                }
                subject += presenationCT;
            }
            SendPush(userIds, flowDesc, nextFLActivity.Name, remark, string.Format("[{0}]{1}", sUserId, sUserName), presenationCT, string.Empty, string.Empty);

            if (subject.Length == 0 && body.Length == 0)
            {
                return;
            }

            foreach (var userEmail in userEmails)
            {
                var client = CreateSmtpClient(smtp, sendFrom, password, enableSSL, port);

                MailMessage message = new MailMessage();
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.From = new MailAddress(sendFrom, "Workflow", System.Text.Encoding.UTF8);
                message.To.Add(new MailAddress(userEmail.Value, userEmail.Value, Encoding.UTF8));
                message.IsBodyHtml = true;
                message.Subject = subject.Replace("\r", "").Replace("\n", " ");

                var role = (string)flInstanceParms[5];
                //催单更改地址
                if (!string.IsNullOrEmpty(role))
                {

                    DataSet dataSet = HostTable.GetHostDataSetByIdAndPath2("SYS_TODOLIST", flInstance.FLInstanceId.ToString(), nextFLActivity.Name, clientInfo);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        var flowpath = dataSet.Tables[0].Rows[0]["FLOWPATH"].ToString();
                        flInstance.CurrentFLActivity = flInstance.RootFLActivity.GetFLActivityByName(flowpath.Split(';')[0]);
                        fnm = FLNavigatorMode.Approve;
                    }
                }


                message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IFLBaseActivity, fnm));
                //_message = message;

                Thread thread = new Thread(new ParameterizedThreadStart(SendMail));
                DateTime d1 = DateTime.Now;
                thread.Start(new object[] { client, message });
                DateTime d2 = DateTime.Now;
                //Thread.Sleep(200);
            }
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void SendTo4(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
           
            List<string> sendToes = new List<string>();

            List<FLActivity> nextFLActivities = flInstance.NextFLActivities;
            List<string> tmpList = new List<string>();
            foreach (FLActivity flActivity in nextFLActivities)
            {
                if (flActivity is IEventWaiting)
                {
                    IEventWaiting nextFLActivity = (IEventWaiting)flActivity;
                    tmpList.Add(nextFLActivity.Name);
                }
                else if (flActivity is IFLNotifyActivity)
                {
                    IFLNotifyActivity nextFLActivity = (IFLNotifyActivity)flActivity;
                    tmpList.Add(nextFLActivity.Name);
                }
            }
            if (tmpList.Count == 0)
                return;

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);

            var setting = EmailSetting.LoadSetting(clientInfo);

            var active = setting.Activie;
            if (!active)
            {
                return;
            }

            var sendFrom = setting.SendFrom;
            var password = setting.Password;
            var smtp = setting.SMTP;
            var enableSSL = setting.EnableSSL;
            var port = setting.Port;

            foreach (FLActivity flActivity in nextFLActivities)
            {
                string body = string.Empty;
                sendToes.Clear();
                if (!(flActivity is IEventWaiting) && !(flActivity is IFLNotifyActivity))
                {
                    continue;
                }

                FLActivity nextFLActivity = flActivity;
                int plusApprove = 0;
                if (nextFLActivity is IFLStandActivity)
                {
                    plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity).PlusApprove);
                }
                else if (nextFLActivity is IFLApproveActivity)
                {
                    plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity).PlusApprove);
                }
                else if (nextFLActivity is IFLApproveBranchActivity)
                {
                    FLActivity approve = flInstance.RootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)nextFLActivity).ParentActivity);
                    plusApprove = Convert.ToInt32(((IFLApproveActivity)approve).PlusApprove);
                }

                string users = string.Empty;
                string email = string.Empty;
                List<string> roleIds = new List<string>();
                List<string> userIds = new List<string>();
                SendToKind sk = SendToKind.Applicate;
                string sr = string.Empty;
                string su = string.Empty;
                string sf = string.Empty;
                string fn = string.Empty;
                string wfn = string.Empty;
                NavigatorMode nm = NavigatorMode.Normal;
                FLNavigatorMode fnm = FLNavigatorMode.Notify;
                if (flActivity is IEventWaiting)
                {
                    sk = ((IEventWaiting)flActivity).SendToKind;
                    sr = ((IEventWaiting)flActivity).SendToRole;
                    su = ((IEventWaiting)flActivity).SendToUser;
                    sf = ((IEventWaiting)flActivity).SendToField;
                    wfn = string.IsNullOrEmpty(((IEventWaiting)flActivity).WebFormName)
                        ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                        : ((IEventWaiting)flActivity).WebFormName;
                    nm = ((IEventWaiting)flActivity).NavigatorMode;
                    fnm = ((IEventWaiting)flActivity).FLNavigatorMode;
                }
                else if (flActivity is IFLNotifyActivity)
                {
                    sk = ((IFLNotifyActivity)flActivity).SendToKind;
                    sr = ((IFLNotifyActivity)flActivity).SendToRole;
                    su = ((IFLNotifyActivity)flActivity).SendToUser;
                    sf = ((IFLNotifyActivity)flActivity).SendToField;
                    wfn = string.IsNullOrEmpty(((IFLNotifyActivity)flActivity).WebFormName)
                                 ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                                 : ((IFLNotifyActivity)flActivity).WebFormName;
                    nm = ((IFLNotifyActivity)flActivity).NavigatorMode;
                    fnm = ((IFLNotifyActivity)flActivity).FLNavigatorMode;
                }
                nm = NavigatorMode.Modify;// return always set navigator mode to modify;
                if (sk == SendToKind.Applicate)
                {
                    userIds.Add(flInstance.Creator);
                }
                else
                {
                    // string roleId = string.Empty;
                    string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;
                    if (flInstance.IsPlusApprove)
                    {
                        string q = flInstanceParms[8].ToString();
                        string[] qq = q.Split(";".ToCharArray());
                        foreach (string r in qq)
                        {
                            if (!string.IsNullOrEmpty(r))
                            {
                                if (r.StartsWith("U:"))
                                {
                                    userIds.Add(r.Substring(2));
                                }
                                else
                                {
                                    roleIds.Add(r);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (sk == SendToKind.Role)
                        {
                            string q = sr;
                            string[] qq = q.Split(";".ToCharArray());

                            // roleId = qq[0].Trim();
                            roleIds.Add(qq[0].Trim());
                        }
                        else if (sk == SendToKind.ApplicateManager)
                        {
                            if (flInstance.FLDirection == FLDirection.GoToBack)
                            {
                                // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                                roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(flInstance.CreateRole))
                                {
                                    roleIds.Add(Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo));
                                }
                                else
                                {
                                    List<string> roles = Global.GetRoleIdsByUserId(flInstance.Creator, clientInfo);
                                    if (roles.Count > 0)
                                    {
                                        roleIds.Add(Global.GetManagerRoleId(roles[0], orgKind, clientInfo));
                                    }
                                }
                            }
                        }
                        else if (sk == SendToKind.Manager)
                        {
                            if (flInstance.FLDirection == FLDirection.GoToBack)
                            {
                                // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                                roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                            }
                            else
                            {
                                // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                                roleIds.Add(Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo));
                            }
                        }
                        else if (sk == SendToKind.RefRole)
                        {
                            if (nextFLActivity is FLStandActivity && ((ISupportFLDetailsActivity)nextFLActivity).SendToId2 != string.Empty)
                            {
                                // roleId = ((ISupportFLDetailsActivity)nextFLActivity).SendToId2;
                                roleIds.Add(((ISupportFLDetailsActivity)nextFLActivity).SendToId2);
                            }
                            else
                            {
                                // roleId = Global.GetRoleIdByRefRole(sf, tableName, keyValues[1].ToString(), clientInfo);
                                roleIds.Add(Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo));
                            }
                        }
                        else if (sk == SendToKind.RefManager)
                        {
                            if (flInstance.FLDirection == FLDirection.GoToBack)
                            {
                                // roleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
                                roleIds.Add(((IEventWaitingExecute)nextFLActivity).RoleId);
                            }
                            else
                            {
                                if (nextFLActivity is IFLApproveBranchActivity &&
                                    ((FLApproveActivity)flInstance.RootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)nextFLActivity).ParentActivity)).I > 1)
                                {
                                    // roleId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                                    roleIds.Add(Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo));
                                }
                                else
                                {
                                    string sendToField = sf;
                                    string values = keyValues[1].ToString();

                                    string s = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                                    // roleId = Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo);
                                    roleIds.Add(Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo));
                                }
                            }
                        }
                        else if (sk == SendToKind.RefUser)
                        {
                            string id = Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo, true);

                            if (!string.IsNullOrEmpty(id))
                            {
                                string[] listusers = id.Split(';');
                                foreach (string user in listusers)
                                {
                                    if (user.Trim().Length > 0)
                                    {
                                        userIds.Add(user);
                                    }
                                }
                            }
                        }
                        else if (sk == SendToKind.User)
                        {
                            string[] listusers = su.Split(';');
                            foreach (string user in listusers)
                            {
                                if (user.Trim().Length > 0)
                                {
                                    userIds.Add(user);
                                }
                            }
                        }


                        else if (flActivity is IFLNotifyActivity & sk == SendToKind.AllRoles)
                        {
                            foreach (string r in flInstance.RL)
                            {
                                if (string.IsNullOrEmpty(r))
                                    continue;

                                string[] rr = r.Split(":".ToCharArray());
                                if (rr[0] == "R")
                                {
                                    roleIds.Add(rr[1]);
                                }
                                else
                                {
                                    roleIds.Add(r);
                                }

                                roleIds.Add(string.Format("U:{0}", flInstance.Creator));
                            }
                        }
                        else
                        {
                            // roleId = flInstanceParms[5].ToString();
                            roleIds.Add(flInstanceParms[5].ToString());
                        }
                    }

                    //if (roleId != null && roleId != string.Empty)
                    //{
                    //    userIds = Global.GetUsersIdsByRoleId(roleId, clientInfo);
                    //}
 
                }
                var mailApproveLevel = ((IFLRootActivity)flInstance.RootFLActivity).MailApproveLevel;
                var buttonEnable = string.IsNullOrEmpty(mailApproveLevel);
                List<string> tempIds = new List<string>();
                foreach (string r in roleIds)
                {
                    string[] rr = r.Split(":".ToCharArray());
                    if (rr.Length > 1)
                    {
                        tempIds.Add(rr[1]);
                    }
                    else
                    {
                        List<string> userofrole = Global.GetUsersIdsByRoleId(r, clientInfo);
                        if (userofrole.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(mailApproveLevel))
                            {
                                var levelNo = Global.GetLevelNo(r, ((IFLRootActivity)flInstance.RootFLActivity).OrgKind, clientInfo);
                                if (string.Compare(levelNo, mailApproveLevel) >= 0)
                                {
                                    buttonEnable = true;
                                }
                            }
                            tempIds.AddRange(Global.GetAgentUsers(r, userofrole, flInstance.RootFLActivity.Description, clientInfo));
                            //string agent = Global.GetAgent(r, userofrole[0], flInstance.RootFLActivity.Description, clientInfo);
                            //if (!string.IsNullOrEmpty(agent))
                            //{
                            //    object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                            //    if (parAgent != null && Convert.ToBoolean(parAgent))
                            //    {
                            //        tempIds.AddRange(userofrole);
                            //    }
                            //    tempIds.Add(agent);
                            //}
                            //else
                            //{
                            //    tempIds.AddRange(userofrole);
                            //}
                        }
                    }
                }
                foreach (string u in tempIds)
                {
                    if (userIds.Contains(u))
                        continue;

                    userIds.Add(u);
                }
                Dictionary<string, string> userEmails = new Dictionary<string, string>();
                foreach (string userId in userIds)
                {
                    if (users.Length != 0)
                    {
                        users += ",";
                    }
                    users += userId;

                    email = Global.GetUserEmail(userId, clientInfo);
                    if (email != null && email != string.Empty)
                    {
                        AddEmail(sendToes, email);
                        userEmails[userId] = email;
                    }
                }

                if (sendToes.Count == 0)
                {
                    continue;
                }

                if (setting.BodyActivityDescription)
                {
                    body += "<span>" + nextFLActivity.Description + "</span>";
                }

                body += "<TABLE BORDER=1>";

                if (setting.BodySender)
                {
                    body += "<TR>";
                    body += "<TD WIDTH=150>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _sender);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += string.Format("[{0}]{1}", sUserId, sUserName);
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyFlowName)
                {
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _flowName);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += string.IsNullOrEmpty(flowDesc) ? "&nbsp;" : flowDesc;
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyActivityName)
                {
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _activityName);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += nextFLActivity.Name;
                    body += "</TD>";
                    body += "</TR>";
                }

                string presenationCT = null;
                if (setting.BodyContent)
                {
                    string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                    string keys = keyValues[0].ToString();
                    string presenation = keyValues[1].ToString();
                    presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

                    body += GetPresentation(presenationCT);
                    body += GetBodyHtml(flInstance, keyValues, clientInfo);
                }
                string remark = flInstance.FLFlag == 'X' ? "Reject(system)" : flInstanceParms[4].ToString();
                //意見信息有換行報錯
                remark = InitRemark(remark);
                if (setting.BodyDescription)
                {
                   
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _description);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += string.IsNullOrEmpty(remark) ? "&nbsp;" : remark;
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyDatetime)
                {
                    body += "<TR>";
                    body += "<TD>";
                    body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _dateTime);
                    body += "</TD>";
                    body += "<TD  COLSPAN=7>";
                    body += DateTime.Now.ToString();
                    body += "</TD>";
                    body += "</TR>";
                }

                if (setting.BodyHyperlink)
                {
                    string webUrl = flInstance.GetWebUrl();
                    if (webUrl != null && webUrl != "0" && !string.IsNullOrEmpty(wfn))
                    {
                        if (webUrl.IndexOf("/MainPage_Flow.aspx") > 0)
                        {
                            //在下面替换
                        }
                        else
                        {
                            List<object> objs = new List<object>();
                            if (wfn.IndexOf(".web", StringComparison.OrdinalIgnoreCase) == 0 && wfn.Length > 4)
                            {
                                wfn = wfn.Substring(4);
                            }
                            string[] webFormNames = wfn.Split('.');
                            if (webFormNames.Length >= 2)
                            {
                                objs.Add(HttpUtility.UrlEncode(webFormNames[0]));
                                objs.Add(HttpUtility.UrlEncode(webFormNames[1]) + ".aspx");
                                objs.Add(HttpUtility.UrlEncode(flInstance.FLInstanceId.ToString()));
                                objs.Add(HttpUtility.UrlEncode((flInstance.CurrentFLActivity == null ? string.Empty : flInstance.CurrentFLActivity.Name) + ";" + nextFLActivity.Name));
                                objs.Add(HttpUtility.UrlEncode(keyValues[1].ToString()));
                                objs.Add(HttpUtility.UrlEncode(((int)nm).ToString()));
                                objs.Add(HttpUtility.UrlEncode(((int)fnm).ToString()));
                                objs.Add(HttpUtility.UrlEncode(users));
                                objs.Add(plusApprove);
                                objs.Add(plusApprove == 1 ? "A" : "NR");
                                objs.Add(roleIds.Count > 0 ? roleIds[0] : string.Empty);
                                objs.Add(GetMultiStepReturn(flInstance, nextFLActivity) ? "1" : "0");
                                objs.Add(flInstanceParms.Length >= 10 ? HttpUtility.UrlEncode(flInstanceParms[9].ToString()) : string.Empty);

                                if (webUrl != null && webUrl != string.Empty)
                                {
                                    webUrl = string.Format(webUrl, objs.ToArray());

                                    if (setting.AutoLogin)
                                    {
                                        webUrl += "&AutoLogin=true";
                                    }
                                }
                            }
                        }

                        body += "<TR>";
                        body += "<TD>";
                        body += SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink);
                        body += "</TD>";
                        body += "<TD  COLSPAN=7>";
                        body += "<a href='" + webUrl + "' target='_blank'>" + SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", _hyperLink2) + "</a>";
                        body += "</TD>";
                        body += "</TR>";
                    }
                }

                if (setting.BodyComment)
                {
                    body += GetComments(flInstance.FLInstanceId.ToString(), clientInfo);
                }

                body += "</TABLE>";

                //---------------------------------------------------------------

                var subjectHtml = GetSubjectHtml(flInstance, flActivity, clientInfo);
                string subject = subjectHtml;

                if (setting.SubjectFlowName)
                {
                    subject += flowDesc + "-";
                }

                if (setting.SubjectActivityName)
                {
                    subject += nextFLActivity.Name;
                }

                if (setting.SubjectDescription)
                {
                    
                    if (subject != subjectHtml)
                    {
                        subject += ",";
                    }

                    subject += remark;
                }

                if (setting.SubjectSender)
                {
                    if (subject != subjectHtml)
                    {
                        subject += ",";
                    }

                    subject += sUserId;
                    subject += "(" + sUserName + ")";
                }

                if (setting.SubjectContent)
                {
                    if (subject != subjectHtml)
                    {
                        subject += ",";
                    }
                    if (string.IsNullOrEmpty(presenationCT))
                    {
                        string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
                        string keys = keyValues[0].ToString();
                        string presenation = keyValues[1].ToString();
                        presenationCT = Global.GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);
                    }
                    subject += presenationCT;
                }
                SendPush(userIds, flowDesc, nextFLActivity.Name, remark, string.Format("[{0}]{1}", sUserId, sUserName), presenationCT, string.Empty, string.Empty);

                if (subject.Length == 0 && body.Length == 0)
                {
                    return;
                }

               
                foreach (var userEmail in userEmails)
                {
                    var client = CreateSmtpClient(smtp, sendFrom, password, enableSSL, port);

                    MailMessage message = new MailMessage();
                    message.SubjectEncoding = Encoding.UTF8;
                    message.BodyEncoding = Encoding.UTF8;
                    message.From = new MailAddress(sendFrom, "Workflow", System.Text.Encoding.UTF8);
                    message.To.Add(new MailAddress(userEmail.Value, userEmail.Value, Encoding.UTF8));
                    message.IsBodyHtml = true;
                    message.Subject = subject.Replace("\r", "").Replace("\n", " ");
                    if (nextFLActivity is IEventWaiting)
                    {
                        message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IEventWaiting, fnm));
                        if (buttonEnable)
                        {
                            message.Body += GetButtons(flInstance, clientInfo, setting, userEmail.Key, nextFLActivity as IEventWaiting);
                        }
                    }
                    else
                    {
                        message.Body = body.Replace("href='" + flInstance.GetWebUrl() + "'", GetUrl(flInstance, clientInfo, userEmail.Key, nextFLActivity as IFLBaseActivity, fnm));
                    }
                    //_message = message;

                    Thread thread = new Thread(new ParameterizedThreadStart(SendMail));
                    DateTime d1 = DateTime.Now;
                    thread.Start(new object[] { client, message });
                    DateTime d2 = DateTime.Now;
                    //Thread.Sleep(200);
                }

                //Console.Write("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX:{0}", ((TimeSpan)(d2 - d1)).TotalMilliseconds);

                //try
                //{
                //    //DateTime d1 = DateTime.Now;
                //    //client.Send(message);
                //    //DateTime d2 = DateTime.Now;

                //    //Console.Write("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX:{0}", ((TimeSpan)(d2 - d1)).TotalMilliseconds);
                //}
                //catch
                //{

                //}
            }
        }

        public static void SendToForPlusReturn(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            //call sendto
            List<string> ids = new List<string>();
            List<string> types = new List<string>();
            List<string> activity = new List<string>();

            string[] sendto = Global.GetSendTo(flInstance.FLInstanceId.ToString(), clientInfo);
            if (sendto != null)
            {
                ids.Add(sendto[0]);
                types.Add(sendto[1]);
                activity.Add(sendto[3]);
                SendTo(flInstance, flInstanceParms, keyValues, clientInfo, ids, types, activity);
            }
        }

        private static SmtpClient CreateSmtpClient(string smtp, string sendFrom, string password, bool enableSSL, int port)
        {
            SmtpClient client = new SmtpClient(smtp);
            client.Timeout = 60000;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential(sendFrom, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = enableSSL;
            if (port != 0)
            {
                client.Port = port;
            }
            return client;
        }


        /// <summary>
        /// 发送Email
        /// </summary>
        private static void SendMail(object data)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            SmtpClient _client = (SmtpClient)((object[])data)[0];
            MailMessage _message = (MailMessage)((object[])data)[1];
            StringBuilder title = new StringBuilder("To:");
            foreach (MailAddress address in _message.To)
	        {
		        title.Append(address.Address);
                title.Append(";");
	        }
            if(title.Length> 64)
            {
                title.Remove(64, title.Length - 64);
            }
            _message.Subject = _message.Subject.Replace("\r", "").Replace("\n", " ");
            string description = _message.Subject;
            if(description.Length > 128)
            {
                description = description.Remove(128, description.Length - 128);
            }
            try
            {
                _client.Send(_message);
                if (SysEEPLogService.Enable)
                {
                    SysEEPLog log = new SysEEPLog(null, SysEEPLog.LogStyleType.Email, SysEEPLog.LogTypeType.Normal
                        , DateTime.Now, title.ToString(), description );
                    log.Log();
                }
            }
            catch
            {
                if (SysEEPLogService.Enable)
                {
                    SysEEPLog log = new SysEEPLog(null, SysEEPLog.LogStyleType.Email, SysEEPLog.LogTypeType.Warning
                        , DateTime.Now, title.ToString() , description);
                    log.Log();
                }
            }
        }

        /// <summary>
        /// 取得密码
        /// </summary>
        /// <param name="s">加密后的密码</param>
        /// <returns></returns>
        private static string GetPwdString(string s)
        {
            string sRet = "";
            for (int i = 0; i < s.Length; i++)
            {
                sRet = sRet + (char)(((int)(s[s.Length - 1 - i])) ^ s.Length);
            }
            return sRet;
        }

        private static String InitRemark(String source)
        {
            String remark = source;
            remark = remark.Replace("\r\n", "<BR/>");
            remark = remark.Replace("gimonnhu", "?");
            return remark;
        }
    }

    public class EmailSetting
    {
        public bool Activie { get; set; }
        public string SendFrom { get; set; }
        public string Password { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }

        public bool SubjectSender { get; set; }
        public bool SubjectFlowName { get; set; }
        public bool SubjectActivityName { get; set; }
        public bool SubjectDescription { get; set; }
        public bool SubjectContent { get; set; }

        public bool BodySender { get; set; }
        public bool BodyFlowName { get; set; }
        public bool BodyActivityName { get; set; }
        public bool BodyDescription { get; set; }
        public bool BodyContent { get; set; }
        public bool BodyDatetime { get; set; }
        public bool BodyHyperlink { get; set; }
        public bool BodyComment { get; set; }
        public bool BodyActivityDescription { get; set; }

        public bool ButtonApprove { get; set; }
        public bool ButtonReturn { get; set; }
        public bool ButtonReject { get; set; }

        public bool AutoLogin { get; set; }

        public static EmailSetting LoadSetting(object[] clientInfo)
        {
            var setting = new EmailSetting();
            XmlDocument doc = new XmlDocument();
            string xmlFileName = string.Format("{0}\\Workflow.xml", EEPRegistry.Server);
            if (File.Exists(xmlFileName))
            {
                doc.Load(xmlFileName);
                XmlNode nodeActive = doc.ChildNodes[0].SelectSingleNode("Active");
                if (nodeActive != null)
                {
                    setting.Activie = Convert.ToBoolean(nodeActive.InnerText);
                }

                XmlNode nodeEmail = doc.ChildNodes[0].SelectSingleNode("Email");
                if (nodeEmail != null)
                {
                    setting.SendFrom = nodeEmail.InnerText;
                }
                XmlNode nodePassword = doc.ChildNodes[0].SelectSingleNode("Password");
                if (nodePassword != null)
                {
                    setting.Password = GetPwdString(nodePassword.InnerText);
                }
                XmlNode nodeSMTP = doc.ChildNodes[0].SelectSingleNode("SMTP");
                if (nodeSMTP != null)
                {
                    setting.SMTP = nodeSMTP.InnerText;
                }
                XmlNode nodeSSL = doc.ChildNodes[0].SelectSingleNode("EnableSSL");
                if (nodeSSL != null)
                {
                    setting.EnableSSL = Convert.ToBoolean(nodeSSL.InnerText);
                }
                XmlNode nodePort = doc.ChildNodes[0].SelectSingleNode("Port");
                if (nodePort != null && !string.IsNullOrEmpty(nodePort.InnerText))
                {
                    setting.Port = Convert.ToInt32(nodePort.InnerText);
                }

                var developer = string.Empty;
                object[] info = (object[])clientInfo[0];
                if (info != null && info.Length > 17)
                {
                    developer = (string)info[17];
                }
                //cloud
                //var dbconnection 
                if (!string.IsNullOrEmpty(developer))
                {
                    var db = Srvtools.DbConnectionSet.GetDbConn(Srvtools.DbConnectionSet.GetSystemDatabase(null));
                    using (var connection = db.CreateConnection())
                    {
                        connection.Open();
                        var command = connection.CreateCommand();

                        var solution = (string)((object[])clientInfo[0])[6];
                        var parameters = AddParameters(new string[] { "UserID", "SolutionID" }, new object[] { developer, solution }, command);
                        command.CommandText = string.Format("SELECT * FROM SYS_SDSOLUTIONS WHERE UserID = {0} AND SolutionID = {1}", parameters);
                        var adpater = Srvtools.DBUtils.CreateDbDataAdapter(command);
                        var dataSet = new DataSet();
                        adpater.Fill(dataSet);
                        
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            var servers = dataSet.Tables[0].Rows[0]["servers"].ToString().Split(';');
                            foreach (var server in servers)
                            {
                                var keyValues = server.Split('=');
                                if (keyValues.Length == 2)
                                {
                                    var values = keyValues[1].Split(',');
                                    if (keyValues[0] == "MailSubject")
                                    {
                                        setting.SubjectActivityName = values.Contains("ActivityName");
                                        setting.SubjectContent = values.Contains("Content");
                                        setting.SubjectDescription = values.Contains("Description");
                                        setting.SubjectFlowName = values.Contains("FlowName");
                                        setting.SubjectSender = values.Contains("Sender");
                                    }
                                    if (keyValues[0] == "MailBody")
                                    {
                                        setting.BodyActivityDescription = values.Contains("ActivityDescription");
                                        setting.BodyActivityName = values.Contains("ActivityName");
                                        setting.BodyComment = values.Contains("Comment");
                                        setting.BodyContent = values.Contains("Content");
                                        setting.BodyDatetime = values.Contains("DateTime");
                                        setting.BodyDescription = values.Contains("Description");
                                        setting.BodyFlowName = values.Contains("FlowName");
                                        setting.BodyHyperlink = values.Contains("HyperLink");
                                        setting.BodySender = values.Contains("Sender");
                                    }
                                    if (keyValues[0] == "MailButton")
                                    {
                                        setting.ButtonApprove = values.Contains("Approve");
                                        setting.ButtonReject = values.Contains("Return");
                                        setting.ButtonReturn = values.Contains("Reject");
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    #region Body
                    XmlNode nodeActivityDescription = doc.ChildNodes[0].SelectSingleNode("ActivityDescription");
                    if ((nodeActivityDescription != null && Convert.ToBoolean(nodeActivityDescription.InnerText)))
                    {
                        setting.BodyActivityDescription = true;
                    }

                    XmlNode nodeSender = doc.ChildNodes[0].SelectSingleNode("Sender");
                    if ((nodeSender != null && Convert.ToBoolean(nodeSender.InnerText)) || nodeSender == null)
                    {
                        setting.BodySender = true;
                    }

                    XmlNode nodeFlowName = doc.ChildNodes[0].SelectSingleNode("FlowName");
                    if ((nodeFlowName != null && Convert.ToBoolean(nodeFlowName.InnerText)) || nodeFlowName == null)
                    {
                        setting.BodyFlowName = true;
                    }

                    XmlNode nodeActivityName = doc.ChildNodes[0].SelectSingleNode("ActivityName");
                    if ((nodeActivityName != null && Convert.ToBoolean(nodeActivityName.InnerText)) || nodeActivityName == null)
                    {
                        setting.BodyActivityName = true;
                    }


                    XmlNode nodeContent = doc.ChildNodes[0].SelectSingleNode("Content");
                    if ((nodeContent != null && Convert.ToBoolean(nodeContent.InnerText)) || nodeContent == null)
                    {
                        setting.BodyContent = true;
                    }

                    XmlNode nodeDescription = doc.ChildNodes[0].SelectSingleNode("Description");
                    if ((nodeDescription != null && Convert.ToBoolean(nodeDescription.InnerText)) || nodeDescription == null)
                    {
                        setting.BodyDescription = true;
                    }

                    XmlNode nodeDateTime = doc.ChildNodes[0].SelectSingleNode("DateTime");
                    if ((nodeDateTime != null && Convert.ToBoolean(nodeDateTime.InnerText)) || nodeDateTime == null)
                    {
                        setting.BodyDatetime = true;
                    }

                    XmlNode nodeHyperLink = doc.ChildNodes[0].SelectSingleNode("HyperLink");
                    if ((nodeHyperLink != null && Convert.ToBoolean(nodeHyperLink.InnerText)) || nodeHyperLink == null)
                    {
                        setting.BodyHyperlink = true;
                    }

                    XmlNode nodeComment = doc.ChildNodes[0].SelectSingleNode("Comment");
                    if ((nodeComment != null && Convert.ToBoolean(nodeComment.InnerText)) || nodeComment == null)
                    {
                        setting.BodyComment = true;
                    }
                    #endregion

                    #region Subject
                    XmlNode nodeFlowName2 = doc.ChildNodes[0].SelectSingleNode("FlowName2");
                    if ((nodeFlowName2 != null && Convert.ToBoolean(nodeFlowName2.InnerText)) || nodeFlowName2 == null)
                    {
                        setting.SubjectFlowName = true;
                    }

                    XmlNode nodeActivityName2 = doc.ChildNodes[0].SelectSingleNode("ActivityName2");
                    if ((nodeActivityName2 != null && Convert.ToBoolean(nodeActivityName2.InnerText)) || nodeActivityName2 == null)
                    {
                        setting.SubjectActivityName = true;
                    }

                    XmlNode nodeDescription2 = doc.ChildNodes[0].SelectSingleNode("Description2");
                    if ((nodeDescription2 != null && Convert.ToBoolean(nodeDescription2.InnerText)) || nodeDescription2 == null)
                    {
                        setting.SubjectDescription = true;
                    }

                    XmlNode nodeSender2 = doc.ChildNodes[0].SelectSingleNode("Sender2");
                    if ((nodeSender2 != null && Convert.ToBoolean(nodeSender2.InnerText)) || nodeSender2 == null)
                    {
                        setting.SubjectSender = true;
                    }

                    XmlNode nodeContent2 = doc.ChildNodes[0].SelectSingleNode("Content2");
                    if ((nodeContent2 != null && Convert.ToBoolean(nodeContent2.InnerText)) || nodeContent2 == null)
                    {
                        setting.SubjectContent = true;
                    }
                    #endregion

                    #region Button
                    XmlNode nodeApproveButton = doc.ChildNodes[0].SelectSingleNode("ApproveButton");
                    if ((nodeApproveButton != null && Convert.ToBoolean(nodeApproveButton.InnerText)))
                    {
                        setting.ButtonApprove = true;
                    }
                    XmlNode nodeReturnButton = doc.ChildNodes[0].SelectSingleNode("ReturnButton");
                    if ((nodeReturnButton != null && Convert.ToBoolean(nodeReturnButton.InnerText)))
                    {
                        setting.ButtonReturn = true;
                    }
                    XmlNode nodeRejectButton = doc.ChildNodes[0].SelectSingleNode("RejectButton");
                    if ((nodeRejectButton != null && Convert.ToBoolean(nodeRejectButton.InnerText)))
                    {
                        setting.ButtonReject = true;
                    } 
                    #endregion

                    XmlNode nodeAutoLogin = doc.ChildNodes[0].SelectSingleNode("AutoLogin");
                    if (nodeAutoLogin != null && Convert.ToBoolean(nodeAutoLogin.InnerText))
                    {
                        setting.AutoLogin = true;
                    }
                }

                //not cloud
            }
            return setting;
        }

        public static object[] AddParameters(IEnumerable<string> keys, IEnumerable<object> values, IDbCommand command)
        {
            var parameters = new List<object>();
            var keyArray = keys.ToArray();
            var valueArray = values.ToArray();
            for (int i = 0; i < keyArray.Length && i < valueArray.Length; i++)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = keyArray[i];
                parameter.Value = valueArray[i];
                command.Parameters.Add(parameter);

                if (command is System.Data.OracleClient.OracleCommand)
                {
                    parameters.Add(string.Format(":{0}", keyArray[i]));
                }
                else
                {
                    parameters.Add(string.Format("@{0}", keyArray[i]));
                }

            }
            return parameters.ToArray();
        }

        private static string GetPwdString(string s)
        {
            string sRet = "";
            for (int i = 0; i < s.Length; i++)
            {
                sRet = sRet + (char)(((int)(s[s.Length - 1 - i])) ^ s.Length);
            }
            return sRet;
        }
    }

    public class PushSetting
    {
        public bool Active { get; set; }
        public string PushService { get; set; }
        public string APIKey { get; set; }
        public string P12FileName{ get; set; }
        public string FilePassword { get; set; }

        public bool SubjectSender { get; set; }
        public bool SubjectFlowName { get; set; }
        public bool SubjectActivityName { get; set; }
        public bool SubjectDescription { get; set; }
        public bool SubjectContent { get; set; }

        public bool BodySender { get; set; }
        public bool BodyFlowName { get; set; }
        public bool BodyActivityName { get; set; }
        public bool BodyDescription { get; set; }
        public bool BodyContent { get; set; }
        public bool BodyDatetime { get; set; }
        public bool BodyHyperlink { get; set; }
        public bool BodyComment { get; set; }
        public bool BodyActivityDescription { get; set; }

        public static PushSetting LoadSetting()
        {
            var setting = new PushSetting();
            string xmlFileName = string.Format("{0}\\WorkflowPush.xml", EEPRegistry.Server);
            if (File.Exists(xmlFileName))
            {
                var doc = new XmlDocument();
                doc.Load(xmlFileName);

                foreach (XmlNode node in doc.ChildNodes[0].ChildNodes)
                {
                    var propertyName = node.Name;

                    var property = setting.GetType().GetProperty(propertyName);
                    if (property != null)
                    {
                        if (property.PropertyType == typeof(bool))
                        {
                            property.SetValue(setting, bool.Parse(node.InnerText), null);
                        }
                        else
                        {
                            property.SetValue(setting, node.InnerText, null);
                        }
                    }
                }
            }
            return setting;
        }
    }
}
