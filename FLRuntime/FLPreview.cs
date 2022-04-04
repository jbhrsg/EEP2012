using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Xml;
using FLTools.ComponentModel;
using FLCore;

namespace FLRuntime
{
    internal class FLPreview
    {
        public FLPreview(string xomlFile, string ruleFile, Guid flInstanceId, object[] _clientInfo, string _currentAcitivity, DataSet hostDataSet, string _roleID, string _orgKind, object[] keyValues)
        {
            clientInfo = _clientInfo;
            currentAcitivity = _currentAcitivity;
            role = _roleID;
            // orgKind = _orgKind;
            //create or find instance

            if (flInstanceId.Equals(Guid.Empty))
            {
                instance = Global.FLRuntime.CreateFLInstance(flInstanceId, xomlFile, ruleFile, _clientInfo, role, hostDataSet, _orgKind);
                currentAcitivity = instance.RootFLActivity.ChildFLActivities[0].Name;
            }
            else
            {
                instance = Global.FLRuntime.GetFLInstance(flInstanceId, _clientInfo);
                xomlFile = instance.FLDefinitionFile;
            }
            instance.SetKeyValues(keyValues);
            instance._hostDataSet = HostTable.GetHostDataSet(instance, keyValues, _clientInfo);
            if (instance._hostDataSet.Tables[0].Rows.Count == 0)
            {
                instance._hostDataSet = hostDataSet;//设置host表
            }

            message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLDesigner", "FLDesigner", "FLPreview");
            Document.Load(xomlFile); //载入xoml

            //add subflow

            InsertSubFlow(Document.DocumentElement);

        }

        //private string userText;

        //private string roleText;

        private FLInstance instance;
        /// <summary>
        /// 流程实例
        /// </summary>
        public FLInstance Instance
        {
            get { return instance; }
        }

        private object[] clientInfo;
        public object[] ClientInfo
        {
            get { return clientInfo; }
        }

        //private string orgKind;

        public string OrgKind
        {
            get { return ((IFLRootActivity)Instance.RootFLActivity).OrgKind; }
        }

        private string role;

        public string Role
        {
            get { return role; }
        }

        public string User
        {
            get { return (string)(((object[])ClientInfo[0])[(int)ClientInfoType.LoginUser]); }
        }


        private string currentAcitivity;
        public string CurrentAcitivity
        {
            get { return currentAcitivity; }
        }

        private XmlDocument document = new XmlDocument();

        public XmlDocument Document
        {
            get { return document; }
        }

        private DataTable table;
        public DataTable PreviewTable
        {
            get { return table; }
            set { table = value; }
        }

        private enum PreviewType
        {
            Image,
            DataTable
        }

        private enum AcitivityStatus
        {
            Approved,
            PlusApproved,
            Current,
            Wait
        }

        private PreviewType Type;

        const string DESIGNER_FILE = "FLDesigner.exe";

        const string PARALLEL_MODE = "ParallelMode";

        const string X_NAME = "x:Name";

        const string XOML_NAME = "XomlName";

        const string XOML_FIELD = "XomlField";

        const string FLCURRENT = "FLCurrent";

        const string FLPASS = "FLPass";

        const string FLSTAND = "FLStand";

        const string FLSUBFLOW = "FLSubFlow";

        const string PARALLEL_ACTIVITY = "ParallelActivity";

        const string SEQUENCE_ACTIVITY = "SequenceActivity";

        const string INCLUDE_FIRST_ACTIVITY = "IncludeFirstActivity";

        const string XMLNS_X = "http://schemas.microsoft.com/winfx/2006/xaml";

        const string XMLNS = "http://schemas.microsoft.com/winfx/2006/xaml/workflow";

        public Image CreatePreviewImage()
        {
            Type = PreviewType.Image;
            //ReviewPrevious
            ReviewPrevious();

            if (!string.IsNullOrEmpty(CurrentAcitivity))
            {
                //markcurrent
                MarkCurrent();
                //PreviewNext
                PreviewNext();
            }

            RemoveApproveDetail();
            //saveXoml
            string filename = Path.GetRandomFileName();
            string filepath = Path.GetTempPath() + "\\" + filename;


            //string filename = string.Format("{0}\\{1}", EEPRegistry.Server, "flowpreview.xoml");

            //string filename = "c:\\1.xoml";
            Document.Save(filepath);

            //string filenametest = string.Format("{0}\\{1}", EEPRegistry.Server, "flowpreview.xoml");
            //Document.Save(filenametest);


            string designPath = string.Format("{0}\\{1}", EEPRegistry.Server, DESIGNER_FILE);
            if (!File.Exists(designPath))
            {
                designPath = string.Format("{0}\\FLDesigner\\{1}", Path.GetDirectoryName(EEPRegistry.Server), DESIGNER_FILE);
                if (!File.Exists(designPath))
                {
                    throw new Exception("Can not find FLDesigner.exe");
                }
            }

            System.Diagnostics.Process process = System.Diagnostics.Process.Start(designPath, filename);
            process.WaitForExit(30000);

            //      return (Bitmap)SaveImage(filename);
            if (File.Exists(filepath + ".bmp"))
            {
                Bitmap bmp = (Bitmap)Image.FromFile(filepath + ".bmp");
                return bmp;
            }
            return null;
        }

        string message;

        private string strApproved;

        private string strWaiting;

        private string strPlusApproved;

        public DataTable CreatePreviewTable()
        {
            Type = PreviewType.DataTable;
            PreviewTable = new DataTable();

            //PreviewTable.Columns.AddRange(new DataColumn[]{
            //    new DataColumn("Seq"),
            //    new DataColumn("Activity"),
            //    new DataColumn("Role"),
            //    new DataColumn("User"),
            //    new DataColumn("Status")}
            //    );

            string[] listMessage = message.Split(',');
            for (int i = 0; i < 6; i++)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = listMessage[i];
                PreviewTable.Columns.Add(column);
            }
            PreviewTable.Columns.Add(new DataColumn("pid", typeof(string)));
            strApproved = listMessage[6];
            strWaiting = listMessage[7];
            strPlusApproved = listMessage[8];

            //ReviewPrevious
            ReviewPrevious();
            if (!string.IsNullOrEmpty(CurrentAcitivity))
            {
                //markcurrent
                MarkCurrent();
                //PreviewNext
                PreviewNext();
            }

            var lpid = string.Empty;
            var index = 0;
            for (int i = 0; i < PreviewTable.Rows.Count; i++)
            {
                var activityName = PreviewTable.Rows[i][2].ToString();
                var pid = PreviewTable.Rows[i][6].ToString();
                if (!string.IsNullOrEmpty(lpid) && !string.IsNullOrEmpty(pid) && pid == lpid)
                { }
                else
                {
                    index++;
                }
                lpid = pid;
                PreviewTable.Rows[i][0] = index;
               
            }
            PreviewTable.Columns.RemoveAt(6);

            return PreviewTable;
        }

        private XmlNode FindNode(XmlNode node, string name)
        {
            if (node != null)
            {
                XmlAttribute att = node.Attributes[X_NAME];
                if (att != null && att.Value.Equals(name))
                {
                    return node;
                }
                foreach (XmlNode child in node.ChildNodes)
                {
                    XmlNode nd = FindNode(child, name);
                    if (nd != null)
                    {
                        return nd;
                    }
                }
            }
            return null;
        }

        private void AddToTable(string actitvityName, string roleName, string userName, AcitivityStatus status, string parallelID)
        {
            //int seq = PreviewTable.Rows.Count + 1;
            string str = string.Empty;
            switch (status)
            {
                case AcitivityStatus.Approved: str = strApproved; break;
                case AcitivityStatus.Current: str = strWaiting; break;
                case AcitivityStatus.PlusApproved: str = strPlusApproved; break;
            }
            PreviewTable.Rows.Add(new object[] { 0, string.Empty, actitvityName, roleName, userName, str, parallelID });
        }

        private void AddToTable(string detailActitvityName, string roleName, string userName, AcitivityStatus status, string condition, string parallelID)
        {
            string str = string.Empty;
            switch (status)
            {
                case AcitivityStatus.Approved: str = strApproved; break;
                case AcitivityStatus.Current: str = strWaiting; break;
                case AcitivityStatus.PlusApproved: str = strPlusApproved; break;
            }

            //find the other detail
            int index = detailActitvityName.LastIndexOf('_');
            string detailName = index > 0 ? detailActitvityName.Substring(0, index) : detailActitvityName;

            int rowIndex = -1;
            for (int i = 0; i < PreviewTable.Rows.Count; i++)
            {
                string activityName = (string)PreviewTable.Rows[i][2];
                if (activityName.StartsWith(detailName + "_"))
                {
                    PreviewTable.Rows[i][1] = condition;
                    rowIndex = i;
                }
            }
            if (rowIndex == -1)
            {
                PreviewTable.Rows.Add(new object[] { 0, string.Empty, detailActitvityName, roleName, userName, str, parallelID });
            }
            else
            {
                if (rowIndex == PreviewTable.Rows.Count - 1)
                {
                    PreviewTable.Rows.Add(new object[] { 0, condition, detailActitvityName, roleName, userName, str, parallelID });
                }
                else
                {
                    DataRow row = PreviewTable.NewRow();
                    row.ItemArray = new object[] { 0, condition, detailActitvityName, roleName, userName, str, parallelID };
                    PreviewTable.Rows.InsertAt(row, rowIndex + 1);
                }
            }

        }

        //替换内容
        private bool ReplaceName(XmlNode document, string oldName, string newName, AcitivityStatus status)
        {
            XmlNode node = FindNode(document, oldName);
            if (node != null)
            {
                XmlAttribute att = node.Attributes[X_NAME];
                if (!string.IsNullOrEmpty(newName))
                {
                    att.Value = att.Value + "_" + newName; //换名字
                }

                if (status == AcitivityStatus.Current)
                {
                    XmlNode currentNode = node.OwnerDocument.CreateElement(FLCURRENT, node.NamespaceURI);
                    XmlAttribute currentAtt = node.OwnerDocument.CreateAttribute(X_NAME, XMLNS_X);
                    currentAtt.Value = att.Value;
                    currentNode.Attributes.Append(currentAtt);
                    node.ParentNode.InsertAfter(currentNode, node);
                    node.ParentNode.RemoveChild(node);
                }
                else if (status == AcitivityStatus.Approved)
                {
                    XmlNode passNode = node.OwnerDocument.CreateElement(FLPASS, node.NamespaceURI);
                    XmlAttribute passAtt = node.OwnerDocument.CreateAttribute(X_NAME, XMLNS_X);
                    passAtt.Value = att.Value;
                    passNode.Attributes.Append(passAtt);
                    node.ParentNode.InsertAfter(passNode, node);
                    node.ParentNode.RemoveChild(node);
                }
                return true;
            }
            return false;
        }

        private List<string> ApproveDetailNames = new List<string>();

        private List<string> ApproveBranches = new List<string>();

        //FLAprrove插入新节点用
        private bool InsertApproveStand(XmlNode document, string approve, string name)
        {
            if (!ApproveBranches.Contains(name))
            {
                XmlNode node = FindNode(document, approve);
                if (node != null)
                {
                    XmlNode standNode = node.OwnerDocument.CreateElement(FLSTAND, node.NamespaceURI);
                    XmlAttribute att = node.OwnerDocument.CreateAttribute(X_NAME, XMLNS_X);
                    att.Value = name;
                    standNode.Attributes.Append(att);
                    node.ParentNode.InsertBefore(standNode, node);
                    ApproveBranches.Add(name);

                    if (!ApproveDetailNames.Contains(approve))
                    {
                        ApproveDetailNames.Add(approve);
                    }

                    return true;
                }
            }
            return false;
        }

        private bool InsertDetailStand(XmlNode document, string detail, string name, string parallel, string seq)
        {
            XmlNode node = FindNode(document, detail);
            if (node != null)
            {
                XmlNode standNode = node.OwnerDocument.CreateElement(FLSTAND, node.NamespaceURI);
                XmlAttribute attStand = node.OwnerDocument.CreateAttribute(X_NAME, XMLNS_X);
                attStand.Value = name;
                standNode.Attributes.Append(attStand);

                //get detail mode
                if (!string.IsNullOrEmpty(parallel) && parallel.StartsWith(detail))
                {
                    XmlNode nodeParallel = FindNode(document, parallel);
                    if (nodeParallel == null)
                    {
                        //create parallel
                        nodeParallel = node.OwnerDocument.CreateElement(PARALLEL_ACTIVITY, XMLNS);
                        XmlAttribute att = node.OwnerDocument.CreateAttribute(X_NAME, XMLNS_X);
                        att.Value = parallel;
                        nodeParallel.Attributes.Append(att);
                        node.ParentNode.InsertBefore(nodeParallel, node);
                    }
                    XmlNode nodeSeq = FindNode(document, seq);
                    if (nodeSeq == null)
                    {
                        nodeSeq = node.OwnerDocument.CreateElement(SEQUENCE_ACTIVITY, XMLNS);
                        XmlAttribute attSeq = node.OwnerDocument.CreateAttribute(X_NAME, XMLNS_X);
                        attSeq.Value = seq;
                        nodeSeq.Attributes.Append(attSeq);
                        nodeParallel.AppendChild(nodeSeq);
                    }
                    nodeSeq.AppendChild(standNode);
                }
                else
                {
                    node.ParentNode.InsertBefore(standNode, node);
                }

                if (!ApproveDetailNames.Contains(detail))
                {
                    ApproveDetailNames.Add(detail);
                }

                return true;
            }
            return false;

        }

        private void InsertSubFlow(XmlNode node)
        {
            //递归
            if (node.Name.IndexOf(FLSUBFLOW, StringComparison.OrdinalIgnoreCase) != -1)
            {
                string file = string.Empty;
                //替换subflow 
                XmlAttribute att = node.Attributes[XOML_FIELD];
                if (att != null && att.Value != "{x:Null}" && att.Value != "")
                {
                    file = Instance._hostDataSet.Tables[0].Rows[0][att.Value].ToString();
                }
                if (string.IsNullOrEmpty(file))
                {
                    file = node.Attributes[XOML_NAME] == null ? string.Empty : node.Attributes[XOML_NAME].Value;
                }
                if (string.IsNullOrEmpty(file))
                {
                    return;
                }

                string path = Path.GetDirectoryName(Instance.FLDefinitionFile);
                string filepath = Path.Combine(path, file);
                if (!File.Exists(filepath))
                {
                    filepath = Path.Combine(Path.Combine(path, "SubFlows"), file);
                }
                XmlDocument subDocument = new XmlDocument();
                subDocument.Load(filepath);
                InsertSubFlow(subDocument.DocumentElement);

                XmlAttribute attInclude = node.Attributes[INCLUDE_FIRST_ACTIVITY];
                bool include = attInclude != null && attInclude.Value.IndexOf(bool.TrueString, StringComparison.OrdinalIgnoreCase) != -1;

                for (int i = 0; i < subDocument.DocumentElement.ChildNodes.Count; i++)
                {
                    XmlNode subNode = subDocument.DocumentElement.ChildNodes[i];
                    if (i == 0 && !include)
                    {
                        continue;
                    }
                    XmlNode newNode = node.OwnerDocument.ImportNode(subNode, true);
                    node.ParentNode.InsertBefore(newNode, node);
                }
                //foreach (XmlNode subNode in subDocument.DocumentElement.ChildNodes)
                //{
                //    //include first activity


                //    XmlNode newNode = node.OwnerDocument.ImportNode(subNode, true);

                //    node.ParentNode.InsertBefore(newNode, node);
                //}
                node.ParentNode.RemoveChild(node);
            }
            else
            {
                List<XmlNode> childs = new List<XmlNode>();
                foreach (XmlNode child in node.ChildNodes)
                {
                    childs.Add(child);
                    //InsertSubFlow(child);
                }
                foreach (XmlNode child in childs)
                {
                    InsertSubFlow(child);
                }
            }
        }

        private void RemoveApproveDetail()
        {
            foreach (string name in ApproveDetailNames)
            {
                RemoveApproveDetail(Document.DocumentElement, name);
            }
        }

        private void RemoveApproveDetail(XmlNode document, string approve)
        {
            XmlNode node = FindNode(document, approve);
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
            }
        }

        private void AddSendTo(string activityName, SendTo send, string parallelID)
        {
            AddSendTo(activityName, send, AcitivityStatus.Wait, parallelID);
        }

        private void AddSendTo(string activityName, SendTo send, AcitivityStatus status, string parallelID)
        {
            FLActivity activity = Instance.RootFLActivity.GetFLActivityByName(activityName);
            if (activity != null)
            {
                if (activity is IFLApproveBranchActivity)
                {
                    activityName = activityName.Replace("-", "_");
                    InsertApproveStand(Document.DocumentElement, (activity as IFLApproveBranchActivity).ParentActivity, activityName);
                }
                else if (activity is ISupportFLDetailsActivity /*&& !string.IsNullOrEmpty((activity as ISupportFLDetailsActivity).SendToId2)*/)
                {
                    if (!string.IsNullOrEmpty(activity.Location))
                    {
                        var index = activity.Name.LastIndexOf('_');
                        if (index > 0)
                        {
                            var detailName = activity.Name.Substring(0, activity.Name.LastIndexOf('_'));
                            FLActivity locationActivity = Instance.RootFLActivity.GetFLActivityByName(detailName);
                            if (locationActivity != null && locationActivity is FLDetailsActivity)
                            {
                                InsertDetailStand(Document.DocumentElement, detailName, activity.Name, activity.UpperParallel, activity.UpperParallelBranch);
                            }
                        }
                    }
                }
            }

            if (Type == PreviewType.Image)
            {
                if (status != AcitivityStatus.PlusApproved)
                {
                    string newName = "Unknown";
                    //为Activity加上待办者的名字

                    if (!string.IsNullOrEmpty(send.UserID))
                    {
                        string name = Global.GetUserName(send.UserID, ClientInfo);

                        newName = name;
                    }
                    else if (!string.IsNullOrEmpty(send.RoleID))
                    {
                        //andy: 取第一个用户的名字
                        List<string> users = Global.GetUsersIdsByRoleId(send.RoleID, ClientInfo);
                        if (users.Count > 0)
                        {
                            string name = Global.GetUserName(users[0], ClientInfo);
                            newName = name;
                        }
                        else
                        {
                            newName = "Nobody";
                        }
                    }
                    ReplaceName(Document.DocumentElement, activityName, newName, status);
                }
            }
            else
            {
                string roleID = send.RoleID;
                string userID = send.UserID;
                string userName = string.Empty;
                if (!string.IsNullOrEmpty(send.UserID))
                {
                    if (string.IsNullOrEmpty(roleID))
                    {
                        userID = send.UserID;
                        List<string> roles = Global.GetRoleIdsByUserId(userID, clientInfo);
                        if (roles.Count > 0)
                        {
                            roleID = roles[0];
                        }
                    }
                    userName = string.IsNullOrEmpty(userID) ? string.Empty : Global.GetUserName(userID, clientInfo);
                }
                else if (string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(send.RoleID))
                {
                    roleID = send.RoleID;
                    List<string> users = Global.GetUsersIdsByRoleId(send.RoleID, ClientInfo);
                    if (users.Count > 0)
                    {
                        userID = users[0];
                        foreach (string user in users)
                        {
                            if (!string.IsNullOrEmpty(userName))
                            {
                                userName += ",";
                            }
                            userName += Global.GetUserName(user, clientInfo);
                        }
                    }
                }
                string roleName = string.IsNullOrEmpty(roleID) ? string.Empty : Global.GetGroupName(roleID, clientInfo);
                if (roleName.Contains("/"))
                {
                    roleName = roleName.Split('/')[0];
                }
                //string userName = string.IsNullOrEmpty(userID) ? string.Empty : Global.GetUserName(userID, clientInfo);


                if (activity is ISupportFLDetailsActivity && !string.IsNullOrEmpty((activity as ISupportFLDetailsActivity).SendToId2))
                {
                    if (string.IsNullOrEmpty(activity.UpperParallel))
                    {
                        AddToTable(activityName, roleName, userName, status, parallelID);
                    }
                    else
                    {
                        string condtion = "Or";
                        if (activity.IsUpperParallelAnd)
                        {
                            condtion = "And";
                        }
                        AddToTable(activityName, roleName, userName, status, condtion, parallelID);
                    }
                }
                else
                {
                    AddToTable(activityName, roleName, userName, status, parallelID);
                }
            }
        }


        private class SendTo
        {
            public SendTo(string role, string user)
            {
                roleID = role;
                userID = user;
            }

            private string roleID;

            public string RoleID
            {
                get { return roleID; }
                set { roleID = value; }
            }

            private string userID;

            public string UserID
            {
                get { return userID; }
                set { userID = value; }
            }
        }

        private enum SendToType
        {
            Role,
            User
        }

        private enum StatusType
        {
            /// <summary>
            /// 加签
            /// </summary>
            A,
            /// <summary>
            /// 通知
            /// </summary>
            F,
            /// <summary>
            /// 正常
            /// </summary>
            N,
            /// <summary>
            /// 取回
            /// </summary>
            NF,
            /// <summary>
            /// 退回
            /// </summary>
            NR,
            /// <summary>
            /// 验证
            /// </summary>
            V,
            /// <summary>
            /// 废除
            /// </summary>
            X,
            /// <summary>
            /// 结案
            /// </summary>
            Z
        }

        const string SELECT_TODOHIS_SQL = "SELECT S_STEP_ID, D_STEP_ID,STATUS,S_USER_ID,S_ROLE_ID,USER_ID,ROLE_ID FROM SYS_TODOHIS Where (LISTID = '{0}') order by UPDATE_DATE, UPDATE_TIME";

        private void ReviewPrevious()
        {
            //从todohis中取
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(SELECT_TODOHIS_SQL, Instance.FLInstanceId);
            object[] objs = remoteModule.CallMethod(ClientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];
                if (dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0];
                    //计算退回和取回
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        if (row["STATUS"].Equals(StatusType.NF.ToString()) || row["STATUS"].Equals(StatusType.NR.ToString()))
                        {
                            //string activityName = row["STATUS"].Equals(StatusType.NR.ToString()) ? row["D_STEP_ID"].ToString() : row["S_STEP_ID"].ToString();//退回或取回到哪一步


                            string activityName = row["D_STEP_ID"].ToString();
                            //找到那一步
                            int returnIndex = -1;
                            for (int j = 0; j < i; j++)
                            {
                                DataRow previousRow = table.Rows[j];
                                if (previousRow.RowState != DataRowState.Deleted)
                                {
                                    if (previousRow["S_STEP_ID"].Equals(activityName))
                                    {
                                        returnIndex = j;
                                    }
                                }
                            }
                            if (returnIndex == -1)
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    DataRow previousRow = table.Rows[j];
                                    if (previousRow.RowState != DataRowState.Deleted)
                                    {
                                        if (previousRow["S_STEP_ID"].Equals(CurrentAcitivity))
                                        {
                                            returnIndex = j;
                                        }
                                    }
                                }
                            }
                            if (returnIndex != -1) //删除那一步以后的资料
                            {
                                for (int j = returnIndex; j < i; j++)
                                {
                                    DataRow previousRow = table.Rows[j];
                                    if (previousRow.RowState != DataRowState.Deleted)
                                    {
                                        previousRow.Delete();
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        if (row.RowState != DataRowState.Deleted)
                        {
                            if (row["STATUS"].Equals(StatusType.N.ToString()) || row["STATUS"].Equals(StatusType.Z.ToString()))
                            {
                                string activityName = row["S_STEP_ID"].ToString();
                                //string sender = row["S_USER_ID"].ToString();
                                string role = row["S_ROLE_ID"].ToString();
                                if (!string.IsNullOrEmpty(role))//加签也是N 为了跳过
                                {
                                    AddSendTo(activityName, new SendTo(row["S_ROLE_ID"].ToString(), row["S_USER_ID"].ToString()), AcitivityStatus.Approved, string.Empty);
                                }
                                else //加签
                                {
                                    AddSendTo(activityName, new SendTo(row["ROLE_ID"].ToString(), row["USER_ID"].ToString()), AcitivityStatus.Approved, string.Empty);
                                }
                            }
                            else if (row["STATUS"].Equals(StatusType.A.ToString()) )
                            {
                                string activityName = row["S_STEP_ID"].ToString();
                                AddSendTo(activityName, new SendTo(row["S_ROLE_ID"].ToString(), row["S_USER_ID"].ToString()), AcitivityStatus.PlusApproved, string.Empty);
                            }
                        }
                    }
                }
            }
        }

        const string SELECT_TODOLIST_SQL = "SELECT SENDTO_KIND, SENDTO_ID, FLOWPATH FROM SYS_TODOLIST WHERE STATUS !='F' AND STATUS !='A' AND STATUS !='AA' AND (LISTID = '{0}')";

        private void MarkCurrent()
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(SELECT_TODOLIST_SQL, Instance.FLInstanceId);
            object[] objs = remoteModule.CallMethod(ClientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];
                if (dataSet.Tables.Count > 0)
                {
                    DataTable table = dataSet.Tables[0];
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string flowpath = (string)table.Rows[i]["FLOWPATH"];
                        string[] paths = flowpath.Split(';');
                        if (paths.Length == 2)
                        {
                            string id = (string)table.Rows[i]["SENDTO_ID"];
                            string type = (string)table.Rows[i]["SENDTO_KIND"];
                            if (type == "1")
                            {
                                AddSendTo(paths[1], new SendTo(id, ""), AcitivityStatus.Current, string.Empty);
                            }
                            else
                            {
                                AddSendTo(paths[1], new SendTo("", id), AcitivityStatus.Current, string.Empty);
                            }
                            //return;
                        }
                    }
                }
            }
            //     string useid = (string)(((object[])ClientInfo[0])[(int)ClientInfoType.LoginUser]);
            //AddSendTo(CurrentAcitivity, new SendTo(Role, User), AcitivityStatus.Current);
        }

        private void PreviewNext()
        {
            //通过计算

            //flinstance.hostDataSet要改写
            SendTo send = new SendTo(Role, User);
            GetNextActivity(send, CurrentAcitivity);
        }

        //递归
        private void GetNextActivity(SendTo from, string AcitivityName)
        {
            List<FLActivity> activitys = instance.GetNextFLActivities(AcitivityName, from.UserID, from.RoleID);
            var parallelID = Guid.NewGuid().ToString();
            foreach (FLActivity activity in activitys)
            {
                if (activity is IEventWaiting)
                {
                    SendTo send = null;
                    if (activity is ISupportFLDetailsActivity && !string.IsNullOrEmpty((activity as ISupportFLDetailsActivity).SendToId2))
                    {
                        //int index = activity.Name.LastIndexOf('_');
                        //string detailName = index > 0 ? activity.Name.Substring(0, index) : activity.Name;
                        //if (activitys.Count > 1 && !string.IsNullOrEmpty(activity.UpperParallel))
                        //{
                        //    InsertDetailStand(Document.DocumentElement, detailName, activity.Name, activity.UpperParallel, activity.UpperParallelBranch);
                        //}
                        //else
                        //{
                        //    InsertDetailStand(Document.DocumentElement, detailName, activity.Name, null, null);
                        //}
                        send = new SendTo((activity as ISupportFLDetailsActivity).SendToId2, string.Empty);
                    }
                    else
                    {
                        send = GetSendTo(activity as IEventWaiting, from);
                    }
                    AddSendTo(activity.Name, send, parallelID);
                    if (activity is ISupportFLDetailsActivity && !string.IsNullOrEmpty((activity as ISupportFLDetailsActivity).SendToId2))
                    {
                        if (!activity.IsUpperParallelAnd && activitys.IndexOf(activity) > 0)
                        {
                            continue;
                        }
                    }
                    GetNextActivity(send, activity.Name);

                }
                else if (activity is IFLNotifyActivity)//通知
                {
                    SendTo send = GetSendTo(activity as IFLNotifyActivity, from);
                    AddSendTo(activity.Name, send, parallelID);
                }
            }
        }

        //接口不统一
        private SendTo GetSendTo(IFLNotifyActivity activity, SendTo from)
        {
            switch (activity.SendToKind)
            {
                //user
                case SendToKind.Applicate: return new SendTo(string.Empty, Instance.Creator);
                case SendToKind.User:
                    {
                        string user = string.IsNullOrEmpty(activity.SendToUser) ? string.Empty : activity.SendToUser.Split(';')[0].Trim();
                        return new SendTo(string.Empty, user);
                    }
                case SendToKind.RefUser:
                    {
                        string user = Instance._hostDataSet.Tables[0].Rows[0][activity.SendToField].ToString();
                        return new SendTo(string.Empty, user);
                    }
                case SendToKind.Role:
                    {
                        string role = string.IsNullOrEmpty(activity.SendToRole) ? string.Empty : activity.SendToRole.Split(';')[0].Trim();
                        return new SendTo(role, string.Empty);
                    }
                case SendToKind.RefRole:
                    {
                        string role = Instance._hostDataSet.Tables[0].Rows[0][activity.SendToField].ToString();
                        return new SendTo(role, string.Empty);
                    }
                case SendToKind.Manager:
                    {
                        return GetManager(from);
                    }
                case SendToKind.RefManager:
                    {
                        string role = string.Empty;
                        if (activity is FLApproveBranchActivity && !string.IsNullOrEmpty(Instance.R))
                        {
                            role = Instance.R;
                        }
                        else
                        {
                            role = Instance._hostDataSet.Tables[0].Rows[0][activity.SendToField].ToString();
                        }
                        return GetManager(new SendTo(role, string.Empty));
                    }
                case SendToKind.ApplicateManager:
                    {
                        return GetManager(new SendTo(Instance.CreateRole, Instance.Creator));
                    }
            }
            return new SendTo(string.Empty, string.Empty);
        }

        private SendTo GetSendTo(IEventWaiting activity, SendTo from)
        {
            switch (activity.SendToKind)
            {
                //user
                case SendToKind.Applicate: return new SendTo(string.Empty, Instance.Creator);
                case SendToKind.User:
                    {
                        string user = string.IsNullOrEmpty(activity.SendToUser) ? string.Empty : activity.SendToUser.Split(';')[0].Trim();
                        return new SendTo(string.Empty, user);
                    }
                case SendToKind.RefUser:
                    {
                        string user = Instance._hostDataSet.Tables[0].Rows[0][activity.SendToField].ToString();
                        return new SendTo(string.Empty, user);
                    }
                case SendToKind.Role:
                    {
                        string role = string.IsNullOrEmpty(activity.SendToRole) ? string.Empty : activity.SendToRole.Split(';')[0].Trim();
                        return new SendTo(role, string.Empty);
                    }
                case SendToKind.RefRole:
                    {
                        string role = Instance._hostDataSet.Tables[0].Rows[0][activity.SendToField].ToString();
                        return new SendTo(role, string.Empty);
                    }
                case SendToKind.Manager:
                    {
                        string role = string.Empty;
                        if (activity is FLApproveBranchActivity && !string.IsNullOrEmpty(Instance.R))
                        {
                            role = Instance.R;
                        }
                        else
                        {
                            role = from.RoleID;
                        }
                        return GetManager(new SendTo(role, string.Empty));
                    }
                case SendToKind.RefManager:
                    {
                        string role = string.Empty;
                        if (activity is FLApproveBranchActivity && !string.IsNullOrEmpty(Instance.R))
                        {
                            role = Instance.R;
                        }
                        else
                        {
                            role = Instance._hostDataSet.Tables[0].Rows[0][activity.SendToField].ToString();
                        }
                        return GetManager(new SendTo(role, string.Empty));
                    }
                case SendToKind.ApplicateManager:
                    {
                        string role = string.Empty;
                        if (activity is FLApproveBranchActivity && !string.IsNullOrEmpty(Instance.R))
                        {
                            role = Instance.R;
                        }
                        else
                        {
                            role = Instance.CreateRole;
                        }
                        return GetManager(new SendTo(role, string.Empty));
                        //return GetManager(new SendTo(Instance.CreateRole, Instance.Creator));

                    }
            }
            return new SendTo(string.Empty, string.Empty);
        }

        private SendTo GetManager(SendTo send)
        {
            string role = string.Empty;
            if (!string.IsNullOrEmpty(send.RoleID))
            {
                role = send.RoleID;
            }
            else if (!string.IsNullOrEmpty(send.UserID))
            {
                List<string> roleIds = Global.GetRoleIdsByUserId(send.UserID, ClientInfo);
                if (roleIds.Count > 0)
                {
                    role = roleIds[0];
                }
            }
            if (!string.IsNullOrEmpty(role))
            {
                role = Global.GetManagerRoleId(role, OrgKind, ClientInfo);
                return new SendTo(role, string.Empty);
            }
            return new SendTo(string.Empty, string.Empty);
        }
    }
}
